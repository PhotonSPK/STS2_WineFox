using System.Threading;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Models;

namespace STS2_WineFox.Powers
{
    public static class WineFoxActions
    {
        private static readonly AsyncLocal<int> ManualMaterialBatchDepth = new();

        public static bool IsManualMaterialBatchActive => ManualMaterialBatchDepth.Value > 0;

        /// <summary>
        ///     获得材料的统一入口（木板/圆石/铁锭等）。
        ///     翻倍效果由 HandCrank 的 ModifyPowerAmountGiven 钩子自动处理，无需在此重复检查。
        /// </summary>
        public static async Task GainMaterial<T>(CardModel card, decimal amount)
            where T : PowerModel
        {
            await PowerCmd.Apply<T>(card.Owner.Creature, amount, card.Owner.Creature, card);
        }

        /// <summary>
        ///     批量获得两种材料。若拥有应力，则仅消耗 1 层，并将本次批量获得整体翻倍。
        /// </summary>
        public static async Task GainMaterials<TFirst, TSecond>(CardModel card, decimal firstAmount,
            decimal secondAmount)
            where TFirst : PowerModel
            where TSecond : PowerModel
        {
            var owner = card.Owner.Creature;
            var hasStress = owner.Powers.OfType<StressPower>().Any(power => power.Amount > 0);

            if (!hasStress)
            {
                await PowerCmd.Apply<TFirst>(owner, firstAmount, owner, card);
                await PowerCmd.Apply<TSecond>(owner, secondAmount, owner, card);
                return;
            }

            using (BeginManualMaterialBatch())
            {
                await PowerCmd.Apply<TFirst>(owner, firstAmount * 2, owner, card);
                await PowerCmd.Apply<TSecond>(owner, secondAmount * 2, owner, card);
            }

            await PowerCmd.Apply<StressPower>(owner, -1m, owner, card);
        }

        /// <summary>
        ///     消耗材料的统一入口。applier 传 null，跳过 ModifyPowerAmountGiven 钩子，
        ///     避免 HandCrank 等效果在消耗时触发倍增。
        /// </summary>
        public static async Task LostMaterial<TFirst, TSecond>(CardModel card, decimal firstAmount,
            decimal secondAmount)
            where TFirst : MaterialPower
            where TSecond : MaterialPower
        {
            var owner = card.Owner.Creature;
            
            var firstPower = owner.Powers.OfType<TFirst>()
                .FirstOrDefault(power => power.Amount >= firstAmount);
            var secondPower = owner.Powers.OfType<TSecond>()
                .FirstOrDefault(power => power.Amount >= secondAmount);
            
            if (firstPower == null || secondPower == null) return;
            
            await PowerCmd.ModifyAmount(firstPower, -firstAmount, null, card);
            await PowerCmd.ModifyAmount(secondPower, -secondAmount, null, card);
        }

        private static IDisposable BeginManualMaterialBatch()
        {
            ManualMaterialBatchDepth.Value++;
            return new BatchScope();
        }

        private sealed class BatchScope : IDisposable
        {
            public void Dispose()
            {
                ManualMaterialBatchDepth.Value = Math.Max(0, ManualMaterialBatchDepth.Value - 1);
            }
        }
    }
}