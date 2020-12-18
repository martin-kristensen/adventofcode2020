using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Disco.AdventOfCode.Day5
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var input = await File.ReadAllLinesAsync("input.txt");

            var seatInfos = new List<SeatInfo>();
            foreach (var i in input)
            {
                var rowChars = i.Substring(0, 7);
                var colChars = i.Substring(7);

                var rowCharsNumber = GetCharNumber(rowChars, 'F');
                var colCharsNumber = GetCharNumber(colChars, 'L');

                var seatId = CalculateSeatId(rowCharsNumber, colCharsNumber);

                var seatInfo = new SeatInfo
                {
                    SeatChars = i,
                    RowChars = rowChars,
                    RowNumber = rowCharsNumber,
                    ColChars = colChars,
                    ColNumber = colCharsNumber,
                    SeatId = seatId
                };

                seatInfos.Add(seatInfo);
            }

            var seatIds = seatInfos.Select(i => i.SeatId).OrderBy(x => x).ToList();

            var highestSeatId = seatIds.Last();
            Console.WriteLine($"Highest seat ID: {highestSeatId}");


            for (var i = 0; i < seatIds.Count - 1; i++)
            {
                var seatId = seatIds.ElementAt(i);
                var nextId = seatIds.ElementAt(i + 1);

                if (nextId - seatId > 1)
                {
                    Console.WriteLine($"Found it: {seatId + 1}");
                }
            }
        }

        private static int CalculateSeatId(in int rowCharsNumber, in int colCharsNumber)
        {
            return rowCharsNumber * 8 + colCharsNumber;
        }

        private static int GetCharNumber(string chars, char lowerChar)
        {
            var stepCount = (int)Math.Pow(2, chars.Length);

            var steps = new int[stepCount];
            for (var i = 0; i < stepCount; i++)
            {
                steps[i] = i;
            }

            for (var i = 0; i < chars.Length; i++)
            {
                var c = chars[i];

                if (c.Equals(lowerChar))
                {
                    steps = steps.Take(steps.Length / 2).ToArray();
                }
                else
                {
                    steps = steps.Skip(steps.Length / 2).ToArray();
                }
            }

            if (steps.Length != 1)
            {
                throw new Exception("Too many steps left");
            }

            return steps[0];
        }

        public class SeatInfo
        {
            public string SeatChars { get; set; }
            public string RowChars { get; set; }
            public int RowNumber { get; set; }
            public string ColChars { get; set; }
            public int ColNumber { get; set; }
            public int SeatId { get; set; }
        }
    }
}
