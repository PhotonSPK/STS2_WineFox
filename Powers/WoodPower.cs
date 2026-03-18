using STS2RitsuLib.Scaffolding.Content;

namespace STS2_WineFox.Powers
{
    public class WoodPower : MaterialPower
    {
        public override PowerAssetProfile AssetProfile => new(
            "res://images/winefox/sts2_wine_fox_power_wood_power.png",
            "res://images/winefox/sts2_wine_fox_power_wood_power.png");
    }
}
