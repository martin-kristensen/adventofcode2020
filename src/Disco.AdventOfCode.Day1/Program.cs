using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Disco.AdventOfCode.Day1
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var expenseEntries = (await File.ReadAllLinesAsync("Input_1.txt")).Select(int.Parse).ToList();

            //Task1(expenseEntries);

            Task2(expenseEntries);
        }

        private static void Task2(List<int> expenseEntries)
        {
            foreach (var e1 in expenseEntries)
            {
                foreach (var e2 in expenseEntries)
                {
                    if (TryGetMatch(e1 + e2, expenseEntries, out var e3))
                    {
                        Console.WriteLine($"Found match {e1}, {e2}, {e3}");
                        Console.WriteLine($"Answer is {e1 * e2 * e3}");
                        return;
                    }
                }
            }
        }

        private static void Task1(List<int> expenseEntries)
        {
            foreach (var expenseEntry in expenseEntries)
            {
                if (TryGetMatch(expenseEntry, expenseEntries, out var matchingExpenseEntry))
                {
                    Console.WriteLine($"Found match for {expenseEntry}: {matchingExpenseEntry}");
                    Console.WriteLine($"Answer is {expenseEntry * matchingExpenseEntry}");
                    return;
                }
            }
        }

        private static bool TryGetMatch(int expense, List<int> expenses, out int matchingExpenseEntry)
        {
            var match = expenses.SingleOrDefault(x => x + expense == 2020);
            if (match != default)
            {
                matchingExpenseEntry = match;
                return true;
            }

            matchingExpenseEntry = default;
            return false;
        }
    }
}
