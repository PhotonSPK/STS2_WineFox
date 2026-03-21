using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using STS2_WineFox.Cards.Token;
using STS2_WineFox.Powers;
using STS2RitsuLib.Scaffolding.Content;

namespace STS2_WineFox.Cards.Basic
{
    public class BaseCraft() : WineFoxCard(1, CardType.Skill, CardRarity.Basic, TargetType.Self)
    {
        protected override IEnumerable<string> RegisteredKeywordIds =>
            [WineFoxKeywords.Wood, WineFoxKeywords.Stone];

        public override CardAssetProfile AssetProfile => new(
            Const.Paths.CardBaseCraft,
            Const.Paths.CardBaseCraft
        );

        protected override bool IsPlayable
        {
            get
            {
                var woodAmount = Owner.Creature.Powers.OfType<WoodPower>()
                    .FirstOrDefault()?.Amount ?? 0m;
                var stoneAmount = Owner.Creature.Powers.OfType<StonePower>()
                    .FirstOrDefault()?.Amount ?? 0m;

                return woodAmount >= 2m && stoneAmount >= 2m;
            }
        }

        protected override async Task OnPlay(
            PlayerChoiceContext choiceContext,
            CardPlay play)
        {
            await WineFoxActions.LostMaterial<WoodPower, StonePower>(this, 2m, 2m);

            if (Owner.Creature.CombatState is not { } combatState)
                return;

            // 创建两张 Token 供玩家选择
            var options = new List<CardModel>
            {
                combatState.CreateCard<StonePick>(Owner),
                combatState.CreateCard<StoneSword>(Owner),
                combatState.CreateCard<IronArmor>(Owner)
            };

            var chosen = await CardSelectCmd.FromChooseACardScreen(
                choiceContext, options, Owner, canSkip: false);

            if (chosen == null) return;

            await CardPileCmd.AddGeneratedCardToCombat(chosen, PileType.Hand, false);
        }

        protected override void OnUpgrade()
        {
            EnergyCost.UpgradeBy(-1);
        }
    }
}