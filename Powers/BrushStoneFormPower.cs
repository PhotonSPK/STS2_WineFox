using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using STS2RitsuLib.Scaffolding.Content;

namespace STS2_WineFox.Powers;

public class BrushStoneFormPower : WineFoxPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;

    public override PowerAssetProfile AssetProfile => Icons(Const.Paths.BrushStoneFormPowerIcon);

    protected override async Task OnAfterPlayerTurnStart(
        PlayerChoiceContext choiceContext, Player player)
    {
        if (player.Creature != Owner) return;
        
        Flash();
        var stonesToGain = 2m * Amount;
        if (stonesToGain > 0)
        {
            await PowerCmd.Apply<StonePower>(Owner, stonesToGain, Owner, null);
        }
        await PowerCmd.Apply<BrushStoneFormPower>(Owner, 1m, Owner, null);
    }
}