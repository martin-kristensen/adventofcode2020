using System;
using System.Collections.Generic;

namespace Disco.AdventOfCode.Day12
{
    public class Stage1
    {
        public char[] Directions = { 'E', 'S', 'W', 'N' };
        private int _xPosition;
        private int _yPosition;
        private int _directionIndex;

        public void Run(List<Instruction> instructions)
        {
            foreach (var instruction in instructions)
            {
                ExecuteInstruction(instruction);
                Console.WriteLine($"Instruction: {instruction.Action}{instruction.Units}: New state: {Directions[_directionIndex]}, x{_xPosition}, y{_yPosition}");
            }

            Console.WriteLine($"Manhattan distance: {Math.Abs(_xPosition) + Math.Abs(_yPosition)}");
        }

        private void ExecuteInstruction(Instruction instruction)
        {
            switch (instruction.Action)
            {
                case 'R':
                    TurnRight(instruction.Units);
                    break;
                case 'L':
                    TurnLeft(instruction.Units);
                    break;
                case 'N':
                    Move('N', instruction.Units);
                    break;
                case 'E':
                    Move('E', instruction.Units);
                    break;
                case 'S':
                    Move('S', instruction.Units);
                    break;
                case 'W':
                    Move('W', instruction.Units);
                    break;
                case 'F':
                    var currentDirection = GetCurrentDirection();
                    Move(currentDirection, instruction.Units);
                    break;
            }
        }

        private void Move(char direction, int units)
        {
            switch (direction)
            {
                case 'E':
                    _xPosition += units;
                    break;
                case 'S':
                    _yPosition += units;
                    break;
                case 'W':
                    _xPosition -= units;
                    break;
                case 'N':
                    _yPosition -= units;
                    break;
            }
        }

        private char GetCurrentDirection()
        {
            return Directions[_directionIndex];
        }

        private void TurnLeft(int degrees)
        {
            var amount = degrees / 90;
            var i = _directionIndex - amount;
            _directionIndex = i < 0 ? i + 4 : i;
        }

        private void TurnRight(int degrees)
        {
            var amount = degrees / 90;
            _directionIndex = (_directionIndex + amount) % 4;
        }
    }
}