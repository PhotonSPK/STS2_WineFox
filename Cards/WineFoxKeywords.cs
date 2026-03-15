using MegaCrit.Sts2.Core.Models;
using STS2RitsuLib.Keywords;

namespace STS2_WineFox.Cards
{
    public static class WineFoxKeywords
    {
        public const string Stress = "stress";
        public const string Digging = "digging";
        public const string Wood = "wood";
        public const string Stone = "stone";
        public const string Iron = "iron";

        extension(CardModel card)
        {
            public bool IsStress()
            {
                return card.HasModKeyword(Stress);
            }

            public bool IsDigging()
            {
                return card.HasModKeyword(Digging);
            }

            public bool IsWood()
            {
                return card.HasModKeyword(Wood);
            }

            public bool IsStone()
            {
                return card.HasModKeyword(Stone);
            }

            public bool IsIron()
            {
                return card.HasModKeyword(Iron);
            }
        }
    }
}
