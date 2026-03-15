using Godot;
using MegaCrit.Sts2.Core.Models;
using STS2_WineFox.Cards.Basic;

namespace STS2_WineFox.Character
{
    // 临时复用 Silent 的完整奖励池，避免在自定义非基础卡不足时奖励生成失败。
    public partial class WineFoxCardPool : CardPoolModel
    {
        public override string Title => "winefox";

        public override string EnergyColorName => "winefox";
        public override string CardFrameMaterialPath => "card_frame_colorless";

        public override Color DeckEntryCardColor => new("d2a15a");
        public override Color EnergyOutlineColor => new("fffd");
        public override bool IsColorless => false;

        protected override CardModel[] GenerateAllCards()
        {
            return
            [
                ModelDb.Card<WineFoxStrike>(),
                ModelDb.Card<WineFoxDefend>(),
                ModelDb.Card<BasicMine>(),
            ];
        }
    }
}
