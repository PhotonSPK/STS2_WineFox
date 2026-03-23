using STS2_WineFox.Content.Descriptors;
using STS2RitsuLib;

namespace STS2_WineFox.Content
{
    internal static class WineFoxContentRegistrar
    {
        internal static void RegisterAll()
        {
            RitsuLibFramework.CreateContentPack(Const.ModId)
                .Manifest(WineFoxContentManifest.ContentEntries, WineFoxContentManifest.KeywordEntries)
                .Apply();
        }
    }
}
