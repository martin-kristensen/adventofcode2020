using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Disco.AdventOfCode.Day10
{
    class Program
    {
        public static long OutcomeCount;
        public static List<int> PrintList = new List<int>();

        static async Task Main(string[] args)
        {
            var input = await File.ReadAllLinesAsync("validation2.txt");

            var adapters = input.Select(int.Parse).Append(0).OrderBy(x => x).ToList();
            adapters.Add(adapters.Last() + 3);

            Run1(adapters);

            //Run2(adapters);
            Run2dot1(adapters);
        }

        public static void Run2dot1(List<int> adapters)
        {
            var pathCounts = new long[adapters.Count];

            for (var i = 0; i < adapters.Count; i++)
            {
                if (i == 0)
                {
                    pathCounts[i] = 1;
                    continue;
                }

                if (adapters[i] - adapters[i - 1] <= 3)
                {
                    pathCounts[i] += pathCounts[i - 1];
                }

                if (i > 1 && adapters[i] - adapters[i - 2] <= 3)
                {
                    pathCounts[i] += pathCounts[i - 2];
                }

                if (i > 2 && adapters[i] - adapters[i - 3] <= 3)
                {
                    pathCounts[i] += pathCounts[i - 3];
                }
            }

            Console.WriteLine($"Answer #2: {pathCounts.Last()}");
        }

        private static void Run2(List<int> adapters)
        {
            foreach (var adapter in adapters.Where(a => a <= 3))
            {
                Traverse(adapter, adapters);
            }

            Console.WriteLine($"Possible combinations: {OutcomeCount}");
        }

        private static void Traverse(int adapter, List<int> adapters)
        {
            //PrintList.Add(adapter);
            var possibleNextAdapters = adapters.Where(a => GetPossibleAdapters(a, adapter)).ToList();

            if (!possibleNextAdapters.Any())
            {
                OutcomeCount++;
                // NOTE: uncomment to show print the traversal
                //Console.WriteLine(string.Join(", ", PrintList));
                //PrintList.RemoveAt(PrintList.Count - 1);
                return;
            }

            foreach (var possibleNextAdapter in possibleNextAdapters)
            {
                Traverse(possibleNextAdapter, adapters);
            }

            //PrintList.RemoveAt(PrintList.Count - 1);
        }

        private static bool GetPossibleAdapters(int a, int adapter)
        {
            return a != adapter && a - adapter <= 3 && a - adapter >= 1;
        }

        private static void Run1(List<int> adapters)
        {
            var prevAdapter = 0;
            var result = new Dictionary<int, int>();
            foreach (var adapter in adapters)
            {
                var i = adapters.IndexOf(adapter);
                if (i != 0)
                {
                    prevAdapter = adapters.ElementAt(i - 1);
                }

                var diff = adapter - prevAdapter;
                if (result.ContainsKey(diff))
                {
                    result[diff]++;
                }
                else
                {
                    result.Add(diff, 1);
                }
            }

            Console.WriteLine($"Answer #1: {result[1] * result[3]}");
        }
    }
}
