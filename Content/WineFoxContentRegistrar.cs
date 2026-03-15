using STS2_WineFox.Content.Descriptors;
using STS2RitsuLib;

namespace STS2_WineFox.Content
{
    internal static class WineFoxContentRegistrar
    {
        internal static void RegisterAll()
        {
            var content = RitsuLibFramework.GetContentRegistry(Const.ModId);
            var keywords = RitsuLibFramework.GetKeywordRegistry(Const.ModId);

            foreach (var entry in WineFoxContentManifest.ContentEntries)
                entry.Register(content);

            foreach (var entry in WineFoxContentManifest.KeywordEntries)
                entry.Register(keywords);
        }
    }
}
