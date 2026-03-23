using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Multiplayer;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.Nodes.Screens.CardSelection;
using MegaCrit.Sts2.Core.Nodes.Screens.Overlays;
using MegaCrit.Sts2.Core.ValueProps;
using STS2RitsuLib.Scaffolding.Content;

namespace STS2_WineFox.Cards.Basic
{
    public class BaseCraft() : WineFoxCard(1, CardType.Skill, CardRarity.Basic, TargetType.Self)
    {
        protected override IEnumerable<string> RegisteredKeywordIds =>
            [WineFoxKeywords.Wood, WineFoxKeywords.Stone];

        public override bool GainsBlock => true;

        protected override IEnumerable<DynamicVar> CanonicalVars =>
            [new BlockVar(5, ValueProp.Move)];

        public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Retain];

        public override CardAssetProfile AssetProfile => Art(Const.Paths.CardBaseCraft);

        // 至少有一个配方可以合成时才可打出
        protected override bool IsPlayable =>
            CraftRecipeRegistry.All.Any(r => r.CanCraft(Owner.Creature));

        protected override async Task OnPlay(
            PlayerChoiceContext choiceContext,
            CardPlay play)
        {
            await CreatureCmd.GainBlock(Owner.Creature, DynamicVars.Block, play);
            if (Owner.Creature.CombatState is not { } combatState)
                return;

            // 获取所有可合成的配方，展示选择界面
            var options = CraftRecipeRegistry.CreateOptions(combatState, Owner);

            var prefs = new CardSelectorPrefs(
                new("cards", "STS2_WINE_FOX_CHOOSE_CRAFT"),
                1);

            await choiceContext.SignalPlayerChoiceBegun(PlayerChoiceOptions.None);
            NPlayerHand.Instance?.CancelAllCardPlay();

            var screen = NSimpleCardSelectScreen.Create(options.Select(option => option.Card).ToList(), prefs);
            NOverlayStack.Instance!.Push(screen);

            var chosen = (await screen.CardsSelected()).FirstOrDefault();

            await choiceContext.SignalPlayerChoiceEnded();

            if (chosen == null) return;

            // 找到选中卡对应的配方，消耗对应材料
            var selectedOption = options.FirstOrDefault(option => ReferenceEquals(option.Card, chosen));
            if (selectedOption == null) return;

            await selectedOption.Recipe.ConsumeMaterials(this);

            await CardPileCmd.AddGeneratedCardToCombat(chosen, PileType.Hand, false);
        }

        protected override void OnUpgrade()
        {
            EnergyCost.UpgradeBy(-1);
        }
    }
}
