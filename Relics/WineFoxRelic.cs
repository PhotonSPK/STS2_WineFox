using STS2RitsuLib.Scaffolding.Content;

namespace STS2_WineFox.Relics
{
    public abstract class WineFoxRelic : ModRelicTemplate
    {
        protected static RelicAssetProfile Icons(
            string iconPath,
            string? iconOutlinePath = null,
            string? bigIconPath = null)
        {
            return new(iconPath, iconOutlinePath ?? iconPath, bigIconPath ?? iconPath);
        }
    }
}
