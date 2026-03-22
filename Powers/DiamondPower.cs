using STS2RitsuLib.Scaffolding.Content;

namespace STS2_WineFox.Powers
{
    public class DiamondPower : MaterialPower
    {
        public override PowerAssetProfile AssetProfile => new(
            Const.Paths.DiamondPowerIcon,
            Const.Paths.DiamondPowerIcon);
    }
}