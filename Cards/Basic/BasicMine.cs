using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Hooks;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using STS2_WineFox.Powers;
using STS2RitsuLib.Scaffolding.Content;

namespace STS2_WineFox.Cards.Basic
{
    public class BasicMine() : WineFoxCard(1, CardType.Skill,
        CardRarity.Basic, TargetType.Self)
    {
        protected override IEnumerable<string> RegisteredKeywordIds =>
            [WineFoxKeywords.Digging];
        
        protected override IEnumerable<DynamicVar> CanonicalVars =>
        [
            new MineMaterialVar<WoodPower>("Wood", 2m),
            new MineMaterialVar<StonePower>("Stone", 2m),
        ];

        protected override async Task OnPlay(
            PlayerChoiceContext choiceContext,
            CardPlay play)
        {
            var diggingBonus = Owner.Creature.Powers.OfType<DiggingPower>().FirstOrDefault()?.Amount ?? 0m;

            await WineFoxActions.GainMaterials<WoodPower, StonePower>(
                this,
                DynamicVars["Wood"].BaseValue + diggingBonus,
                DynamicVars["Stone"].BaseValue + diggingBonus);
        }

        protected override void OnUpgrade()
        {
            DynamicVars["Wood"].UpgradeValueBy(1m);
            DynamicVars["Stone"].UpgradeValueBy(1m);
        }

        private sealed class MineMaterialVar<TPower>(string name, decimal baseValue) : DynamicVar(name, baseValue)
            where TPower : PowerModel
        {
            public override void UpdateCardPreview(CardModel card, CardPreviewMode previewMode, Creature? target,
                bool runGlobalHooks)
            {
                PreviewValue = card is BasicMine mine
                    ? CalculateAmount(mine, runGlobalHooks)
                    : BaseValue;
            }

            protected override decimal GetBaseValueForIConvertible()
            {
                return _owner is not BasicMine mine
                    ? base.GetBaseValueForIConvertible()
                    : CalculateAmount(mine, CombatManager.Instance.IsInProgress);
            }

            private decimal CalculateAmount(BasicMine card, bool runGlobalHooks)
            {
                if (card.Owner?.Creature == null)
                    return BaseValue;

                var amount = BaseValue +
                             (card.Owner.Creature.Powers.OfType<DiggingPower>().FirstOrDefault()?.Amount ?? 0m);

                if (!runGlobalHooks || card.CombatState == null)
                    return amount;

                return Hook.ModifyPowerAmountGiven(
                    card.CombatState,
                    ModelDb.Power<TPower>(),
                    card.Owner.Creature,
                    amount,
                    card.Owner.Creature,
                    card,
                    out _);
            }
        }
    }
}
