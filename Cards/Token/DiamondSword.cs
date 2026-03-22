using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using STS2_WineFox.Powers;
using STS2RitsuLib.Scaffolding.Content;

namespace STS2_WineFox.Cards.Token
{
    public class DiamondSword() : WineFoxCard(
        0, CardType.Power, CardRarity.Token, TargetType.Self,
        showInCardLibrary: false, autoAdd: false)
    {
        protected override IEnumerable<DynamicVar> CanonicalVars =>
            [new("Echoes", 1m)];

        public override CardAssetProfile AssetProfile => new(
            Const.Paths.CardDiamondSword,
            Const.Paths.CardDiamondSword);

        protected override async Task OnPlay(
            PlayerChoiceContext choiceContext,
            CardPlay play)
        {
            await PowerCmd.Apply<DiamondSwordPower>(
                Owner.Creature, DynamicVars["Echoes"].BaseValue, Owner.Creature, this);
        }

        protected override void OnUpgrade()
        {
            DynamicVars["Echoes"].UpgradeValueBy(1m); // 1 → 2
        }
    }
}