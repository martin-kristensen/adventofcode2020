using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Disco.AdventOfCode.Day8
{
    class Program
    {
        public static int Accumulator;
        public static int CurrentInstructionIndex;

        static async Task Main(string[] args)
        {
            var input = await File.ReadAllLinesAsync("input.txt");

            var instructions = input.Select(i => new Instruction { Command = i.Split(' ')[0], Value = int.Parse(i.Split(' ')[1]) }).ToList();

            //Run1(instructions);
            await Run2(instructions);
        }

        private static async Task Run2(List<Instruction> instructions)
        {
            for (var i = 0; i < instructions.Count; i++)
            {
                Reset();
                var input = await File.ReadAllLinesAsync("input.txt");
                var copyOfInstructions = input.Select(i => new Instruction { Command = i.Split(' ')[0], Value = int.Parse(i.Split(' ')[1]) }).ToList();

                if (copyOfInstructions[i].Command.Equals("nop"))
                {
                    copyOfInstructions[i].Command = "jmp";
                }
                else if (copyOfInstructions[i].Command.Equals("jmp"))
                {
                    copyOfInstructions[i].Command = "nop";
                }
                else
                {
                    continue;
                }

                var success = ExecuteInstructions(copyOfInstructions);
                if (success)
                {
                    Console.WriteLine($"Instruction index {i} was faulty. Accumulator = {Accumulator}");
                    break;
                }
            }
        }

        private static void Reset()
        {
            Accumulator = 0;
            CurrentInstructionIndex = 0;
        }

        private static void Run1(List<Instruction> instructions)
        {
            var success = ExecuteInstructions(instructions);

            if (success)
            {
                Console.WriteLine("It works!");
            }
            else
            {
                Console.WriteLine($"Error occurred, Accumulator = {Accumulator}");
            }
        }

        private static bool ExecuteInstructions(List<Instruction> instructions)
        {
            var errorOccurred = false;
            while (true)
            {
                if (CurrentInstructionIndex >= instructions.Count)
                {
                    break;
                }

                if (!RunInstruction(instructions.ElementAt(CurrentInstructionIndex)))
                {
                    errorOccurred = true;
                    break;
                }
            }

            return !errorOccurred;
        }

        private static bool RunInstruction(Instruction instruction)
        {
            if (instruction.Executed)
            {
                return false;
            }

            HandleInstruction(instruction);

            return true;
        }

        private static void HandleInstruction(Instruction instruction)
        {
            switch (instruction.Command)
            {
                case "jmp":
                    CurrentInstructionIndex += instruction.Value;
                    break;
                case "acc":
                    Accumulator += instruction.Value;
                    CurrentInstructionIndex++;
                    break;
                default:
                    CurrentInstructionIndex++;
                    break;
            }

            instruction.Executed = true;
        }

        public class Instruction
        {
            public string Command { get; set; }
            public int Value { get; set; }
            public bool Executed { get; set; }
        }
    }
}
