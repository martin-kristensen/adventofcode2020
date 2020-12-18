using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Disco.AdventOfCode.Day9
{
    class Program
    {
        public static int Preamble = 25;

        static async Task Main(string[] args)
        {
            var input = await File.ReadAllLinesAsync("input.txt");

            var numberStream = input.Select(long.Parse).ToArray();

            var corruptedNumber = GetCorruptedNumber(numberStream);

            Console.WriteLine($"Found a corrupted number: {corruptedNumber}");

            var range = GetCorruptedRange(corruptedNumber, numberStream);

            Console.WriteLine($"Found a corrupted range: {string.Join(", ", range)}");

            var lowest = range.OrderBy(x => x).First();
            var highest = range.OrderBy(x => x).Last();

            Console.WriteLine($"Lowest {lowest} and highest {highest} added is {lowest + highest}");
        }

        private static long[] GetCorruptedRange(long corruptedNumber, long[] numberStream)
        {
            for (var i = 0; i < numberStream.Length; i++)
            {
                var counter = 1;
                while (true)
                {
                    var subset = numberStream.Skip(i).Take(counter).ToArray();
                    var sum = subset.Sum();

                    if (sum > corruptedNumber)
                    {
                        break;
                    }

                    if (sum.Equals(corruptedNumber))
                    {
                        return subset;
                    }

                    counter++;
                }
            }

            return new long[0];
        }

        private static long SummarizeArray(long[] array)
        {
            return array.Sum();
        }

        private static long GetCorruptedNumber(long[] numberStream)
        {
            for (var i = Preamble; i < numberStream.Length; i++)
            {
                var number = numberStream[i];
                var previousNumbers = numberStream.Skip(-Preamble + i).Take(Preamble).ToList();

                var isSumOfAny = IsSumOfAny(number, previousNumbers);

                if (!isSumOfAny)
                {
                    return number;
                }
            }

            return default;
        }

        private static bool IsSumOfAny(long number, List<long> previousNumbers)
        {
            foreach (var previousNumber in previousNumbers)
            {
                var numbersToCheck = previousNumbers.Where(x => !previousNumber.Equals(x));
                foreach (var numberToCheck in numbersToCheck)
                {
                    var sum = numberToCheck + previousNumber;
                    if (sum.Equals(number))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
