using System.Collections.Generic;

namespace Disco.AdventOfCode.Day18
{
    public class Stage2 : Stage
    {
        protected override string CalculateParts(List<string> parts)
        {
            HandleOperator("+", parts);
            HandleOperator("*", parts);
            
            return parts[0];
        }

        private void HandleOperator(string mathOperator, List<string> parts)
        {
            while (true)
            {
                var indexOfOperator = parts.IndexOf(mathOperator);
                if (indexOfOperator == -1)
                {
                    break;
                }

                var statement = string.Join("", parts[indexOfOperator - 1], parts[indexOfOperator], parts[indexOfOperator + 1]);

                var result = DataTableCompute(statement);

                parts.RemoveAt(indexOfOperator - 1);
                parts.RemoveAt(indexOfOperator - 1);
                parts.RemoveAt(indexOfOperator - 1);
                parts.Insert(indexOfOperator - 1, result);
            }
        }
    }
}