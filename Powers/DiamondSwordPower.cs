using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using STS2RitsuLib.Scaffolding.Content;

namespace STS2_WineFox.Powers
{
    public class DiamondSwordPower : WineFoxPower
    {
        private int _usedThisTurn;
        private bool _isEchoing;

        public override PowerType Type => PowerType.Buff;
        public override PowerStackType StackType => PowerStackType.Counter;

        public override PowerAssetProfile AssetProfile => new(
            Const.Paths.DiamondSwordPowerIcon,
            Const.Paths.DiamondSwordPowerIcon);
        
        public override Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player)
        {
            if (player.Creature != Owner) return Task.CompletedTask;
            _usedThisTurn = 0;
            return Task.CompletedTask;
        }
        
        public override async Task AfterCardPlayed(
            PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            if (cardPlay.Card.Owner?.Creature != Owner) return;
            if (cardPlay.Card.Type != CardType.Attack) return;
            if (_isEchoing) return;
            if (_usedThisTurn >= (int)Amount) return;

            _usedThisTurn++;
            _isEchoing = true;
            Flash();
            await CardCmd.AutoPlay(choiceContext, cardPlay.Card, cardPlay.Target);
            _isEchoing = false;
        }
    }
}