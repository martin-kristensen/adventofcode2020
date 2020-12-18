using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Disco.AdventOfCode.Day2
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var inputPasswords = await File.ReadAllLinesAsync("input.txt");
            var passwordDataList = inputPasswords.Select(x => new PasswordData(x));

            var validCount = 0;
            foreach (var passwordData in passwordDataList)
            {
                //if (ValidatePassword1(passwordData))
                if (ValidatePassword2(passwordData))
                {
                    validCount++;
                }
            }

            Console.WriteLine($"Number of valid passwords: {validCount}");
        }

        private static bool ValidatePassword1(PasswordData passwordData)
        {
            var count = passwordData.Password.Count(x => x.Equals(passwordData.ValidationChar));
            return count >= passwordData.MinCount && count <= passwordData.MaxCount;
        }

        private static bool ValidatePassword2(PasswordData passwordData)
        {
            var char1 = passwordData.Password.ElementAt(passwordData.MinCount - 1);
            var char2 = passwordData.Password.ElementAt(passwordData.MaxCount - 1);

            var charHits = 0;

            if (char1.Equals(passwordData.ValidationChar))
                charHits++;
            if (char2.Equals(passwordData.ValidationChar))
                charHits++;

            return charHits == 1;
        }
    }

    internal class PasswordData
    {
        public PasswordData(string s)
        {
            var parts = s.Split(new[] {":"}, StringSplitOptions.RemoveEmptyEntries);
            Password = parts[1].Trim();
            var policyParts = parts[0].Split(new[] {" "}, StringSplitOptions.RemoveEmptyEntries);
            ValidationChar = char.Parse(policyParts[1]);
            var spanParts = policyParts[0].Split(new[] {"-"}, StringSplitOptions.RemoveEmptyEntries);
            MinCount = int.Parse(spanParts[0]);
            MaxCount = int.Parse(spanParts[1]);
        }

        public int MaxCount { get; set; }

        public int MinCount { get; set; }

        public char ValidationChar { get; set; }

        public string Password { get; set; }
    }
}
