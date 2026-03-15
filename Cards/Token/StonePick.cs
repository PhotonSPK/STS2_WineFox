using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using STS2_WineFox.Powers;

namespace STS2_WineFox.Cards.Token
{
    public class StonePick() : WineFoxCard(0, CardType.Skill,
        CardRarity.Token, TargetType.Self)
    {
        protected override IEnumerable<string> RegisteredKeywordIds =>
            [WineFoxKeywords.Digging];

        protected override IEnumerable<DynamicVar> CanonicalVars =>
            [new PowerVar<DiggingPower>("Digging", 1m)];

        protected override async Task OnPlay(
            PlayerChoiceContext choiceContext,
            CardPlay play)
        {
            await PowerCmd.Apply<DiggingPower>(Owner.Creature, DynamicVars["Digging"].BaseValue, Owner.Creature, this);
        }

        protected override void OnUpgrade()
        {
            DynamicVars["Digging"].UpgradeValueBy(1m);
        }
    }
}
