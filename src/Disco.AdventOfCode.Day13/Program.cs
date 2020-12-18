using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Disco.AdventOfCode.Day13
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var input = await File.ReadAllLinesAsync("validation.txt");

            Run1(input);

            //Run2_Slow(input);
            var timeStamp = Run2(input);
            Console.WriteLine($"Stage 2 timestamp: {timeStamp}");
        }

        private static long Run2(string[] input)
        {
            var busIds = input[1].Split(",");
            long timeStamp = 0;
            var increaseBy = long.Parse(busIds[0]);

            for (var i = 1; i < busIds.Length; i++)
            {
                if (busIds[i].Equals("x"))
                {
                    continue;
                }

                var busTime = int.Parse(busIds[i]);
                while (true)
                {
                    timeStamp += increaseBy;
                    var departsAfter = (timeStamp + i) % busTime == 0;
                    if (departsAfter)
                    {
                        increaseBy *= busTime;
                        break;
                    }
                }
            }

            return timeStamp;
        }

        private static void Run2_Slow(string[] input)
        {
            var busIds = input[1].Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries).Select(x => x.Equals("x") ? -1 : int.Parse(x)).ToArray();

            long startTimeStamp = 193430980030;//busIds.Length;
            var departureCounter = 0;
            while (true)
            {
                departureCounter = startTimeStamp % busIds[departureCounter] == 0 ? departureCounter + 1 : 0;

                if (departureCounter >= busIds.Length)
                {
                    Console.WriteLine($"Success: {startTimeStamp - busIds.Length + 1} - {startTimeStamp}");
                    break;
                }

                startTimeStamp++;
            }
        }

        private static void Run1(string[] input)
        {
            var earliestTimeStamp = int.Parse(input[0]);
            var busIds = input[1].Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries).Where(x => !x.Equals("x")).Select(int.Parse).ToList();

            var closestTimeStamp = int.MaxValue;
            var closestBusId = 0;
            foreach (var busId in busIds)
            {
                var closestTimeStampForBus = GetClosestTimeStamp(earliestTimeStamp, busId);
                if (closestTimeStampForBus < closestTimeStamp)
                {
                    closestTimeStamp = closestTimeStampForBus;
                    closestBusId = busId;
                }
            }

            Console.WriteLine($"Closest time stamp: {closestTimeStamp}, Bus ID: {closestBusId}, wait time: {closestTimeStamp - earliestTimeStamp} minutes.");
            Console.WriteLine($"Answer: {(closestTimeStamp - earliestTimeStamp) * closestBusId}");
        }

        private static int GetClosestTimeStamp(int earliestTimeStamp, int busId)
        {
            var timeStamp = 0;
            while (timeStamp < earliestTimeStamp)
            {
                timeStamp += busId;
            }

            return timeStamp;
        }
    }
}
