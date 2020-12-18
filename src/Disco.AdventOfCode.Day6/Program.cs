using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Disco.AdventOfCode.Day6
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var input = await File.ReadAllLinesAsync("input.txt");

            var inputStrings = new List<List<string>>();

            var groupStrings = new List<string>();
            foreach (var i in input)
            {
                if (string.IsNullOrWhiteSpace(i))
                {
                    inputStrings.Add(groupStrings);
                    groupStrings = new List<string>();
                    continue;
                }

                groupStrings.Add(i);
            }

            if (groupStrings.Any())
            {
                inputStrings.Add(groupStrings);
            }


            var result1 = inputStrings.Sum(GetGroupCountQuestion1);
            Console.WriteLine($"Answer 1: {result1}");

            var result2 = inputStrings.Sum(GetGroupCountQuestion2);
            Console.WriteLine($"Answer 2: {result2}");
        }

        public static int GetGroupCountQuestion1(List<string> groupStrings)
        {
            var chars = groupStrings.SelectMany(x => x.ToCharArray()).Distinct();

            return chars.Count();
        }

        public static int GetGroupCountQuestion2(List<string> groupStrings)
        {
            var charGroups = groupStrings.SelectMany(x => x.ToCharArray()).GroupBy(x => x);

            var yesCount = 0;
            foreach (var charGroup in charGroups)
            {
                if (charGroup.Count() == groupStrings.Count)
                {
                    yesCount++;
                }
            }

            return yesCount;
        }
    }
}
