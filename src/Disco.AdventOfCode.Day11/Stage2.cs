using System.Collections.Generic;
using System.Linq;

namespace Disco.AdventOfCode.Day11
{
    public class Stage2 : Stage
    {
        public Stage2(int width, int height)
            : base(width, height, 5)
        {
        }

        protected override char[] GetAdjacent(char[] slots, in int x, in int y)
        {
            var topLeftAvailable = GetTraversalResult(slots, x, y, -1, -1);
            var topAvailable = GetTraversalResult(slots, x, y, 0, -1);
            var topRightAvailable = GetTraversalResult(slots, x, y, 1, -1);
            var leftAvailable = GetTraversalResult(slots, x, y, -1, 0);
            var rightAvailable = GetTraversalResult(slots, x, y, 1, 0);
            var bottomLeftAvailable = GetTraversalResult(slots, x, y, -1, 1);
            var bottomAvailable = GetTraversalResult(slots, x, y, 0, 1);
            var bottomRightAvailable = GetTraversalResult(slots, x, y, 1, 1);

            var adjacentSlotStates = new List<bool>
            {
                topLeftAvailable, topAvailable, topRightAvailable,
                leftAvailable, rightAvailable,
                bottomLeftAvailable, bottomAvailable, bottomRightAvailable
            };

            return adjacentSlotStates.Where(b => !b).Select(b => '#').ToArray();
        }

        // NOTE: returns true if available, otherwise false
        private bool GetTraversalResult(char[] slots, in int x, in int y, int xDirection, int yDirection)
        {
            // Check if on the edge
            var isValid = ValidateNewPosition(x, y, xDirection, yDirection);
            if (!isValid)
            {
                return true;
            }

            var newX = x + xDirection;
            var newY = y + yDirection;
            var index = GetIndex(newX, newY);

            var slot = slots[index];

            if (slot.Equals('#'))
            {
                return false;
            }

            if (slot.Equals('L'))
            {
                return true;
            }

            return GetTraversalResult(slots, newX, newY, xDirection, yDirection);
        }

        private bool ValidateNewPosition(int x, int y, int xDirection, int yDirection)
        {
            if (xDirection < 0 && x + xDirection < 0) // Invalid Left
                return false;

            if (yDirection < 0 && y + yDirection < 0) // Invalid Up
                return false;

            if (xDirection > 0 && x + xDirection >= Width) // Invalid Right
                return false;

            if (yDirection > 0 && y + yDirection >= Height) // Invalid Down
                return false;

            return true;
        }
    }
}