using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Disco.AdventOfCode.Day3
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var input = await File.ReadAllLinesAsync("input.txt");

            RunTask1(input);

            RunTask2(input);
        }

        private static void RunTask1(string[] input)
        {
            var treeCount = GetTreeCount(input, 3, 1);
            Console.WriteLine($"Number of trees: {treeCount}");
        }

        private static void RunTask2(string[] input)
        {
            var treeCount1 = GetTreeCount(input, 1, 1);
            var treeCount2 = GetTreeCount(input, 3, 1);
            var treeCount3 = GetTreeCount(input, 5, 1);
            var treeCount4 = GetTreeCount(input, 7, 1);
            var treeCount5 = GetTreeCount(input, 1, 2);

            Console.WriteLine($"Number: {treeCount1 * treeCount2 * treeCount3 * treeCount4 * treeCount5}");
        }

        public static int GetTreeCount(string[] input, int rightSteps, int downSteps)
        {
            int x = 0, treeCount = 0;

            for (var i = 0; i < input.Length; i += downSteps)
            {
                treeCount += input[i].ElementAt(x % input[i].Length).Equals('#') ? 1 : 0;
                x += rightSteps;
            }

            return treeCount;
        }
    }
}
