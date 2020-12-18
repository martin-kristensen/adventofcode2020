using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Disco.AdventOfCode.Day18
{
    public abstract class Stage
    {
        public void Run(string[] input)
        {
            var answers = new List<long>();
            foreach (var line in input)
            {
                var trimmedLine = line.Replace(" ", string.Empty);

                while (true)
                {
                    var indexOfLastStartingStatement = trimmedLine.LastIndexOf('(');

                    if (indexOfLastStartingStatement == -1)
                    {
                        break;
                    }

                    var statement = "";
                    for (var i = indexOfLastStartingStatement; i < trimmedLine.Length; i++)
                    {
                        statement += trimmedLine[i];
                        if (trimmedLine[i].Equals(')'))
                        {
                            break;
                        }
                    }

                    var result = CalculateStatement(statement);

                    trimmedLine = trimmedLine.Replace(statement, result);
                }

                var answer = CalculateStatement(trimmedLine);

                var convertedAnswer = long.Parse(answer.Split(new[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0]);
                Console.WriteLine(convertedAnswer);
                answers.Add(convertedAnswer);
            }

            Console.WriteLine($"Answer: {answers.Sum()}");
        }

        private string CalculateStatement(string statement)
        {
            var cleanedUpStatement = statement.Replace("(", string.Empty).Replace(")", string.Empty);

            var currentString = string.Empty;
            var parts = new List<string>();
            foreach (var s in cleanedUpStatement)
            {
                if (s.Equals('*') || s.Equals('+'))
                {
                    parts.Add(currentString);
                    currentString = "";
                    parts.Add(s.ToString());
                    continue;
                }

                currentString += s;
            }

            parts.Add(currentString);

            return CalculateParts(parts);
        }

        protected abstract string CalculateParts(List<string> parts);

        protected string DataTableCompute(string statement)
        {
            var s = statement.Contains('.') ? statement : statement + ".0";
            return new DataTable().Compute(s, string.Empty).ToString().Split(new []{"."}, StringSplitOptions.RemoveEmptyEntries)[0];
        }
    }
}