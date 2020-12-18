using System.Collections.Generic;
using System.Linq;

namespace Disco.AdventOfCode.Day11
{
    public class Stage1 : Stage
    {
        public Stage1(int width, int height) 
            : base(width, height, 4)
        {
        }

        protected override char[] GetAdjacent(char[] slots, in int x, in int y)
        {
            var leftExists = x - 1 >= 0;
            var topExists = y - 1 >= 0;
            var rightExists = x + 1 < Width;
            var bottomExists = y + 1 < Height;

            var existingAdjacentSlots = new List<char?>
            {
                leftExists && topExists ? slots[GetIndex(x - 1, y - 1)] : (char?) null, // Top-Left
                topExists ? slots[GetIndex(x, y - 1)] : (char?) null, // Top
                rightExists && topExists ? slots[GetIndex(x + 1, y - 1)] : (char?) null, // Top-Right
                leftExists ? slots[GetIndex(x - 1, y)] : (char?) null, // Left
                rightExists ? slots[GetIndex(x + 1, y)] : (char?) null, // Right
                leftExists && bottomExists ? slots[GetIndex(x - 1, y + 1)] : (char?) null, // Bottom-Left
                bottomExists ? slots[GetIndex(x, y + 1)] : (char?) null, // Bottom
                rightExists && bottomExists ? slots[GetIndex(x + 1, y + 1)] : (char?) null // Bottom-Right
            };

            return existingAdjacentSlots.Where(c => c.HasValue).Select(c => c.Value).ToArray();
        }
    }
}
