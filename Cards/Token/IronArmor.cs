using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;   // ← 原生 Power 命名空间（同 StrengthPower）
using STS2RitsuLib.Scaffolding.Content;

namespace STS2_WineFox.Cards.Token
{
    public class IronArmor() : WineFoxCard(
        0, CardType.Power, CardRarity.Token, TargetType.Self,
        showInCardLibrary: false, autoAdd: false)
    {
        protected override IEnumerable<DynamicVar> CanonicalVars =>
            [new("Armor", 5m)];

        public override CardAssetProfile AssetProfile => new(
            Const.Paths.CardIronArmor,
            Const.Paths.CardIronArmor);

        protected override async Task OnPlay(
            PlayerChoiceContext choiceContext,
            CardPlay play)
        {
            await PowerCmd.Apply<PlatingPower>(   // ← 游戏原生覆甲 Power
                Owner.Creature, DynamicVars["Armor"].BaseValue, Owner.Creature, this);
        }

        protected override void OnUpgrade() { }
    }
}