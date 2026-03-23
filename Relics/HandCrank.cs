using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Relics;
using STS2_WineFox.Powers;
using STS2RitsuLib.Scaffolding.Content;

namespace STS2_WineFox.Relics
{
    public class HandCrank : WineFoxRelic
    {
        public override RelicAssetProfile AssetProfile => Icons(Const.Paths.HandCrankRelicIcon);

        public override RelicRarity Rarity => RelicRarity.Starter;

        public override async Task BeforeCombatStart()
        {
            Flash();
            await PowerCmd.Apply<StressPower>(Owner.Creature, 1m, Owner.Creature, null);
        }
    }
}
