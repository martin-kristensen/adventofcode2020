using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Disco.AdventOfCode.Day16
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var input = await File.ReadAllLinesAsync("input.txt");

            var validationRules = GetValidationRules(input);

            var otherTicketData = GetOtherTicketData(input);

            //Run1(validationRules, otherTicketData);

            var myTicketData = GetMyTicketData(input);

            Run2(validationRules, otherTicketData, myTicketData);
        }

        private static void Run2(List<ValidationRule> validationRules, List<List<int>> ticketData, List<long> myTicketData)
        {
            var validTicketData = new List<List<int>>();
            foreach (var data in ticketData)
            {
                var invalidValues = GetInvalidValues(validationRules, data);
                
                // If there is any invalid values we do not want to process them
                if (!invalidValues.Any())
                {
                    validTicketData.Add(data);
                }
            }

            for (var i = 0; i < validationRules.Count; i++)
            {
                var dataForIndex = validTicketData.Select(x => x[i]).ToArray();

                foreach (var validationRule in validationRules)
                {
                    var indexIsValidForRule = true;
                    foreach (var data in dataForIndex)
                    {
                        if (!validationRule.IsValid(data))
                        {
                            indexIsValidForRule = false;
                            break;
                        }
                    }

                    if (indexIsValidForRule)
                    {
                        validationRule.ValidIndexes.Add(i);
                    }
                }
            }

            while (true)
            {
                var validationRuleWithOnlyOneValidIndex = validationRules.Where(r => r.ValidIndexes.Count.Equals(1)).ToList();

                if (validationRuleWithOnlyOneValidIndex.Count.Equals(validationRules.Count))
                {
                    break;
                }

                foreach (var validationRule in validationRuleWithOnlyOneValidIndex)
                {
                    var index = validationRule.ValidIndexes[0];
                    foreach (var rule in validationRules.Where(r => !r.Name.Equals(validationRule.Name)))
                    {
                        rule.ValidIndexes.Remove(index);
                    }
                }
            }

            foreach (var validationRule in validationRules.OrderBy(x => x.Index))
            {
                Console.WriteLine($"[{validationRule.Index}] {validationRule.Name}");
            }

            var validationRuleIndexesToMultiply = validationRules.Where(r => r.Name.StartsWith("departure")).Select(x => x.Index).ToList();
            var answer = validationRuleIndexesToMultiply.Select(x => myTicketData[x]).Aggregate((x, y) => x * y);

            Console.WriteLine($"Answer #2: {answer}");
        }

        private static void Run1(List<ValidationRule> validationRules, List<List<int>> ticketData)
        {
            var invalidValues = new List<int>();
            foreach (var data in ticketData)
            {
                invalidValues.AddRange(GetInvalidValues(validationRules, data));
            }

            Console.WriteLine($"Ticket scanning error rate: {invalidValues.Sum()}");
        }

        private static List<int> GetInvalidValues(List<ValidationRule> validationRules, List<int> data)
        {
            var result = new List<int>();
            foreach (var value in data)
            {
                if (!validationRules.Any(r => r.IsValid(value)))
                {
                    result.Add(value);
                }


                //foreach (var validationRule in validationRules)
                //{

                //    if (!validationRule.IsValid(value))
                //    {
                //        result.Add(value);
                //    }
                //}
                //foreach (var range in validationRules.SelectMany(x => x.Ranges))
                //{
                //    if (!range.IsInRange(value))
                //    {
                //        result.Add(value);
                //    }
                //}
            }

            return result;
        }

        private static List<long> GetMyTicketData(string[] input)
        {
            var startingIndex = input.ToList().IndexOf("your ticket:") + 1;
            return input[startingIndex].Split(',').Select(long.Parse).ToList();
        }

        private static List<List<int>> GetOtherTicketData(string[] input)
        {
            var startingIndex = input.ToList().IndexOf("nearby tickets:") + 1;

            var ticketData = new List<List<int>>();
            for (var i = startingIndex; i < input.Length; i++)
            {
                ticketData.Add(input[i].Split(',').Select(int.Parse).ToList());
            }

            return ticketData;
        }

        private static List<ValidationRule> GetValidationRules(string[] input)
        {
            var rules = new List<ValidationRule>();

            foreach (var inputRow in input)
            {
                if (string.IsNullOrWhiteSpace(inputRow))
                {
                    break;
                }

                var s1 = inputRow.Split(new[] { ": " }, StringSplitOptions.RemoveEmptyEntries);
                var rangesData = s1[1].Split(new[] { " or " }, StringSplitOptions.RemoveEmptyEntries);

                var ranges = rangesData.Select(x => new ValidationRange { FromInclusive = int.Parse(x.Split('-')[0]), ToInclusive = int.Parse(x.Split('-')[1]) }).ToList();

                rules.Add(new ValidationRule
                {
                    Name = s1[0],
                    Ranges = ranges
                });
            }

            return rules;
        }
    }

    public class ValidationRule
    {
        public string Name { get; set; }
        public List<ValidationRange> Ranges { get; set; }
        public List<int> ValidIndexes { get; set; }

        public int Index => ValidIndexes.Count == 1 ? ValidIndexes[0] : -1;

        public ValidationRule()
        {
            ValidIndexes = new List<int>();
        }

        public bool IsValid(int value)
        {
            return Ranges.Any(r => r.IsInRange(value));
        }
    }

    public class ValidationRange
    {
        public int FromInclusive { get; set; }
        public int ToInclusive { get; set; }

        public bool IsInRange(int data)
        {
            return FromInclusive <= data && data <= ToInclusive;
        }
    }
}
