using System;
using System.Collections.Generic;

namespace Disco.AdventOfCode.Day12
{
    public class Stage2
    {
        private int _boatX;
        private int _boatY;
        private int _wayPointX = 10;
        private int _wayPointY = -1;

        public void Run(List<Instruction> instructions)
        {
            foreach (var instruction in instructions)
            {
                ExecuteInstruction(instruction);
                Console.WriteLine($"Instruction: {instruction.Action}{instruction.Units}: New state: boatX{_boatX}, boatY{_boatY}, wayPointX{_wayPointX}, wayPointY{_wayPointY}");
            }

            Console.WriteLine($"Manhattan distance: {Math.Abs(_boatX) + Math.Abs(_boatY)}");
        }

        private void ExecuteInstruction(Instruction instruction)
        {
            switch (instruction.Action)
            {
                case 'R':
                    RotateWayPointRight(instruction.Units);
                    break;
                case 'L':
                    RotateWayPointLeft(instruction.Units);
                    break;
                case 'N':
                    MoveWayPoint('N', instruction.Units);
                    break;
                case 'E':
                    MoveWayPoint('E', instruction.Units);
                    break;
                case 'S':
                    MoveWayPoint('S', instruction.Units);
                    break;
                case 'W':
                    MoveWayPoint('W', instruction.Units);
                    break;
                case 'F':
                    MoveBoat(instruction.Units);
                    break;
            }
        }

        private void RotateWayPointLeft(in int degrees)
        {
            var amount = degrees / 90 % 4;

            for (var i = 0; i < amount; i++)
            {
                var prevY = _wayPointY;
                _wayPointY = _wayPointX * -1;
                _wayPointX = prevY;
            }
        }

        private void RotateWayPointRight(int degrees)
        {
            var amount = degrees / 90 % 4;

            for (var i = 0; i < amount; i++)
            {
                var prevX = _wayPointX;
                _wayPointX = _wayPointY * -1;
                _wayPointY = prevX;
            }
        }

        private void MoveBoat(int units)
        {
            _boatX += units * _wayPointX;
            _boatY += units * _wayPointY;
        }

        private void MoveWayPoint(char direction, int units)
        {
            switch (direction)
            {
                case 'E':
                    _wayPointX += units;
                    break;
                case 'S':
                    _wayPointY += units;
                    break;
                case 'W':
                    _wayPointX -= units;
                    break;
                case 'N':
                    _wayPointY -= units;
                    break;
            }
        }
    }
}