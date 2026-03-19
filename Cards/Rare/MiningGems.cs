using MegaCrit.Sts2.Core.Entities.Cards;
using STS2_WineFox.Powers;
using STS2RitsuLib.Scaffolding.Content;

namespace STS2_WineFox.Cards.Rare
{
    
public class MiningGems() : WineFoxCard(1,CardType.Skill,CardRarity.Rare,TargetType.Self)
{
    public override CardAssetProfile AssetProfile => new(
        Const.Paths.CardMiningGems,
        Const.Paths.CardMiningGems);
    
    protected override bool IsPlayable
    {
        get
        {
            // 检查是否拥有足够的材料
            var diggingpowerAmount = Owner.Creature.Powers.OfType<DiggingPower>()
                .FirstOrDefault()?.Amount ?? 0m;
            
            return diggingpowerAmount >= 1m;
        }
    }
}
}

