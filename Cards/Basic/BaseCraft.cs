using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using STS2_WineFox.Cards.Token;
using STS2_WineFox.Powers;

namespace STS2_WineFox.Cards.Basic
{
    public class BaseCraft() : WineFoxCard(1, CardType.Skill, CardRarity.Basic, TargetType.Self)
    {
        protected override async Task OnPlay(
            PlayerChoiceContext choiceContext,
            CardPlay play)
        {
            await WineFoxActions.LostMaterial<WoodPower,StonePower>(this, 2m,2m);

            // 获得 StonePick 卡牌加入手牌
            // var stonePick = CardScope.CreateCard<StonePick>(Owner);
            // var result = await CardPileCmd.Add(stonePick, PileType.Hand);
            // CardCmd.PreviewCardPileAdd(result);
        }

        protected override void OnUpgrade()
        {
            EnergyCost.UpgradeBy(-1);
        }
    }
}