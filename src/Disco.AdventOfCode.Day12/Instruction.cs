namespace Disco.AdventOfCode.Day12
{
    public class Instruction
    {
        public char Action { get; }
        public int Units { get; }

        public Instruction(char action, int units)
        {
            Action = action;
            Units = units;
        }
    }
}