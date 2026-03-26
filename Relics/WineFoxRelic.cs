using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using STS2_WineFox.Commands;
using STS2RitsuLib.Scaffolding.Content;

namespace STS2_WineFox.Relics
{
    public abstract class WineFoxRelic : ModRelicTemplate
    {
        public sealed override Task AfterPlayerTurnStartEarly(PlayerChoiceContext choiceContext, Player player)
        {
            if (Owner == player)
                CraftCmd.ObserveTurnStarted(choiceContext, player);

            return Task.CompletedTask;
        }

        protected static RelicAssetProfile Icons(
            string iconPath,
            string? iconOutlinePath = null,
            string? bigIconPath = null)
        {
            return new(iconPath, iconOutlinePath ?? iconPath, bigIconPath ?? iconPath);
        }
    }
}
