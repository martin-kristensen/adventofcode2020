using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Disco.AdventOfCode.Day14
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Run1(await File.ReadAllLinesAsync("input.txt"));
            Run2(await File.ReadAllLinesAsync("input.txt"));
        }

        private static void Run2(string[] input)
        {
            var memory = new Dictionary<ulong, ulong>();

            var currentMask = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
            foreach (var line in input)
            {
                var parts = line.Split(new[] { " = " }, StringSplitOptions.RemoveEmptyEntries);
                if (parts[0].Equals("mask"))
                {
                    currentMask = parts[1];
                }
                else
                {
                    var memoryAddress = int.Parse(parts[0].Replace("mem[", "").Replace("]", ""));
                    var decimalValue = ulong.Parse(parts[1]);

                    var binaryMemoryAddress = Convert.ToString(memoryAddress, 2).PadLeft(36, '0');
                    //Console.WriteLine(binaryMemoryAddress);
                    //Console.WriteLine(currentMask);

                    var result = new char[binaryMemoryAddress.Length];
                    for (var i = binaryMemoryAddress.Length - 1; i >= 0; i--)
                    {
                        var maskChar = currentMask[i];
                        var givenValue = binaryMemoryAddress[i];

                        if (maskChar.Equals('X'))
                        {
                            result[i] = 'X';
                        }
                        else if (maskChar.Equals('1'))
                        {
                            result[i] = '1';
                        }
                        else // '0'
                        {
                            result[i] = givenValue;
                        }
                    }

                    //Console.WriteLine(result);

                    //Console.WriteLine();

                    var resultAddresses = new List<char[]> {result};

                    for (var i = 0; i < result.Length; i++)
                    {
                        var c = result[i];
                        if (c.Equals('X'))
                        {
                            resultAddresses = Duplicate(resultAddresses);
                            for (var x = 0; x < resultAddresses.Count; x++)
                            {
                                var value = x % 2;
                                resultAddresses[x][i] = char.Parse(value.ToString());
                            }
                        }
                        else
                        {
                            foreach (var resultAddress in resultAddresses)
                            {
                                resultAddress[i] = c;
                            }
                        }
                    }

                    //foreach (var resultAddress in resultAddresses)
                    //{
                    //    Console.WriteLine(resultAddress);
                    //}

                    foreach (var resultAddress in resultAddresses)
                    {
                        var address = Convert.ToUInt64(new string(resultAddress), 2);
                        memory[address] = decimalValue;
                    }
                }

                //Console.WriteLine();
            }

            var sum = memory.Select(x => x.Value).Aggregate<ulong, ulong>(0, (current, memValue) => current + memValue);

            Console.WriteLine($"Memory sum #2: {sum}");
        }

        private static List<char[]> Duplicate(List<char[]> resultAddresses)
        {
            var newList = new List<char[]>();
            foreach (var resultAddress in resultAddresses)
            {
                newList.Add((char[])resultAddress.Clone());
                newList.Add((char[])resultAddress.Clone());
            }

            return newList;
        }

        private static void Run1(string[] input)
        {
            var memory = new ulong[65536];

            var currentMask = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX";

            foreach (var line in input)
            {
                var parts = line.Split(new[] { " = " }, StringSplitOptions.RemoveEmptyEntries);
                if (parts[0].Equals("mask"))
                {
                    currentMask = parts[1];
                }
                else
                {
                    var memoryAddress = int.Parse(parts[0].Replace("mem[", "").Replace("]", ""));
                    var decimalValue = int.Parse(parts[1]);

                    var binaryValue = Convert.ToString(decimalValue, 2).PadLeft(36, '0');
                    //Console.WriteLine(binaryValue);
                    //Console.WriteLine(currentMask);

                    var result = new char[binaryValue.Length];
                    for (var i = binaryValue.Length - 1; i >= 0; i--)
                    {
                        var maskChar = currentMask[i];
                        var givenValue = binaryValue[i];
                        if (maskChar.Equals('X'))
                        {
                            result[i] = givenValue;
                        }
                        else
                        {
                            result[i] = maskChar;
                        }
                    }
                    //Console.WriteLine(result);

                    var memoryValue = Convert.ToUInt64(new string(result), 2);
                    memory[memoryAddress] = memoryValue;
                }
            }

            var sum = memory.Aggregate<ulong, ulong>(0, (current, memValue) => current + memValue);
            Console.WriteLine($"Memory sum #1: {sum}");
        }
    }
}
