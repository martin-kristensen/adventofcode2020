using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Disco.AdventOfCode.Day15
{
    class Program
    {
        public static int LastSpokenNumber;
        public static Dictionary<int, List<int>> NumberDictionary;
        public static int CurrentIndex;

        static async Task Main(string[] args)
        {
            var input = await File.ReadAllLinesAsync("input.txt");
            var numbers = input[0].Split(',').Select(int.Parse).ToArray();

            Run(2020, numbers);
            Run(30000000, numbers);
        }

        private static void Run(int maxIndex, int[] numbers)
        {
            LastSpokenNumber = 0;
            CurrentIndex = 0;
            NumberDictionary = new Dictionary<int, List<int>>();

            foreach (var number in numbers)
            {
                NumberDictionary[number] = new List<int> { CurrentIndex };
                SpeakNumber(number);
                CurrentIndex++;
            }

            while (CurrentIndex < maxIndex)
            {
                var numberToSpeak = NumberDictionary[LastSpokenNumber].Count <= 1 
                    ? 0 
                    : NumberDictionary[LastSpokenNumber][NumberDictionary[LastSpokenNumber].Count - 1] - NumberDictionary[LastSpokenNumber][NumberDictionary[LastSpokenNumber].Count - 2];

                SpeakNumber(numberToSpeak);

                CurrentIndex++;
            }

            Console.WriteLine($"Answer for maxIndex {maxIndex}: {LastSpokenNumber}");
        }

        private static void SpeakNumber(int number)
        {
            if (NumberDictionary.ContainsKey(number))
            {
                NumberDictionary[number].Add(CurrentIndex);
            }
            else
            {
                NumberDictionary[number] = new List<int> { CurrentIndex };
            }

            LastSpokenNumber = number;

            //Console.WriteLine($"{(CurrentIndex + 1).ToString().PadLeft(MaxIndex.ToString().Length, ' ')}: {number}");
        }
    }
}
