using STS2RitsuLib.Scaffolding.Content;

namespace STS2_WineFox.Powers
{
    public abstract class WineFoxPower : ModPowerTemplate
    {
        protected static PowerAssetProfile Icons(string iconPath, string? bigIconPath = null)
        {
            return new(iconPath, bigIconPath ?? iconPath);
        }
    }
}
