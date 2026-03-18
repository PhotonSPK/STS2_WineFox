using STS2RitsuLib.Scaffolding.Content;

namespace STS2_WineFox.Powers
{
    public class StonePower : MaterialPower
    {
        public override PowerAssetProfile AssetProfile => new(
            "res://images/winefox/sts2_wine_fox_power_stone_power.png",
            "res://images/winefox/sts2_wine_fox_power_stone_power.png");
    }
}
