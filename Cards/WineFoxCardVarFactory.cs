using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Models;
using STS2_WineFox.Powers;

namespace STS2_WineFox.Cards
{
    internal static class WineFoxCardVarFactory
    {
        internal static Func<CardModel?, CardPreviewMode, Creature?, bool, decimal> StressDoubledDynamicVar(string key)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(key);

            return (card, previewMode, target, runGlobalHooks) =>
            {
                if (card == null)
                    return 0m;

                if (!card.DynamicVars.TryGetValue(key, out var dynamicVar))
                    return 0m;

                if (!runGlobalHooks || card.CombatState == null || card.Owner?.Creature == null)
                    return dynamicVar.BaseValue;

                var hasStress = card.Owner.Creature.Powers
                    .OfType<StressPower>()
                    .Any(power => power.Amount > 0);

                return hasStress ? dynamicVar.BaseValue * 2m : dynamicVar.BaseValue;
            };
        }
    }
}
