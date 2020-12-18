using System;
using System.Linq;

namespace Disco.AdventOfCode.Day11
{
    public abstract class Stage
    {
        private readonly int _maxNumberOfOccupiedSeatsForLeaving;
        protected int Width { get; }
        protected int Height { get; }

        protected Stage(int width, int height, int maxNumberOfOccupiedSeatsForLeaving)
        {
            _maxNumberOfOccupiedSeatsForLeaving = maxNumberOfOccupiedSeatsForLeaving;
            Width = width;
            Height = height;
        }

        public void Run(char[] slots)
        {
            var stateChanged = true;

            while (stateChanged)
            {
                slots = Cycle(slots, out stateChanged);
            }

            Console.WriteLine($"Number of occupied seats: {slots.Count(c => c.Equals('#'))}");
        }

        public void RunVisualValidation(char[] slots)
        {
            var stateChanged = false;

            PrintState(slots, stateChanged);

            var newState1 = Cycle(slots, out stateChanged);
            PrintState(newState1, stateChanged);

            var newState2 = Cycle(newState1, out stateChanged);
            PrintState(newState2, stateChanged);

            var newState3 = Cycle(newState2, out stateChanged);
            PrintState(newState3, stateChanged);

            var newState4 = Cycle(newState3, out stateChanged);
            PrintState(newState4, stateChanged);

            var newState5 = Cycle(newState4, out stateChanged);
            PrintState(newState5, stateChanged);

            var newState6 = Cycle(newState5, out stateChanged);
            PrintState(newState6, stateChanged);
        }

        protected char[] Cycle(char[] slots, out bool stateChanged)
        {
            stateChanged = false;
            var newState = new char[slots.Length];

            for (var y = 0; y < Height; y++)
            {
                for (var x = 0; x < Width; x++)
                {
                    var index = GetIndex(x, y);
                    var original = slots[index];

                    var adjacent = GetAdjacent(slots, x, y);

                    var newStateForSlot = GetNewStateForSlot(original, adjacent);
                    if (!stateChanged && !newStateForSlot.Equals(original))
                    {
                        stateChanged = true;
                    }
                    newState[index] = newStateForSlot;
                }
            }

            return newState;
        }

        private char GetNewStateForSlot(in char original, char[] adjacent)
        {
            var occupiedAdjacentSlots = adjacent.Count(c => c.Equals('#'));

            switch (original)
            {
                case 'L':
                    return occupiedAdjacentSlots > 0 ? 'L' : '#';
                case '#':
                    return occupiedAdjacentSlots >= _maxNumberOfOccupiedSeatsForLeaving ? 'L' : '#';
                default:
                    return original;
            }
        }

        protected abstract char[] GetAdjacent(char[] slots, in int x, in int y);

        protected int GetIndex(int x, int y)
        {
            return x + y * Width;
        }

        protected void PrintState(char[] newState1, bool stateChanged)
        {
            Console.WriteLine($"STATE CHANGE: {stateChanged}");
            for (var y = 0; y < Width; y++)
            {
                for (var x = 0; x < Height; x++)
                {
                    Console.Write(newState1[GetIndex(x, y)]);
                }
                Console.WriteLine();
            }

            Console.WriteLine();
        }
    }
}