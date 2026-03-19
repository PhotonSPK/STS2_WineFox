using MegaCrit.Sts2.Core.Entities.Cards;
using STS2RitsuLib.Scaffolding.Content;

namespace STS2_WineFox.Cards.UnCommon

{
        public class PlantTrees() : WineFoxCard(1, CardType.Power, CardRarity.Uncommon, TargetType.Self)
        {
        public override CardAssetProfile AssetProfile => new(
            Const.Paths.CardPlantTrees,
            Const.Paths.CardPlantTrees);
        }
}