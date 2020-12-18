using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Linq;
using System.Xml;

namespace Disco.AdventOfCode.Day4
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var input = await File.ReadAllLinesAsync("input.txt");

            var passportDataList = GetPassportData(input);

            Console.WriteLine($"# of valid passports 1: {passportDataList.Count(x => x.IsValid1)}");
            Console.WriteLine($"# of valid passports 2: {passportDataList.Count(x => x.IsValid2)}");
        }

        private static IEnumerable<PassportData> GetPassportData(string[] input)
        {
            var segmentedInput = new List<List<string>>();
            var segment = new List<string>();
            foreach (var line in input)
            {
                if (string.IsNullOrWhiteSpace(line.Trim()))
                {
                    if (segment.Any())
                    {
                        segmentedInput.Add(segment);
                    }
                    segment = new List<string>();
                }

                segment.Add(line);
            }

            if (segment.Any())
            {
                segmentedInput.Add(segment);
            }

            var passportDatas = new List<PassportData>();
            foreach (var segmentedInputEntry in segmentedInput)
            {
                var passportData = new PassportData();
                foreach (var e in segmentedInputEntry.Where(x => !string.IsNullOrWhiteSpace(x)))
                {
                    var data = e.Split(' ');
                    foreach (var d in data)
                    {
                        var propIdentifier = d.Split(':')[0];
                        var propValue = d.Split(':')[1];

                        SetPropertyValue(passportData, propIdentifier, propValue);
                    }
                }

                passportDatas.Add(passportData);
            }

            return passportDatas;
        }

        private static void SetPropertyValue(PassportData passportData, string propIdentifier, string propValue)
        {
            switch (propIdentifier.ToLower())
            {
                case "byr":
                    passportData.BirthYear = propValue;
                    break;
                case "iyr":
                    passportData.IssueYear = propValue;
                    break;
                case "eyr":
                    passportData.ExpirationYear = propValue;
                    break;
                case "hgt":
                    passportData.Height = propValue;
                    break;
                case "hcl":
                    passportData.HairColor = propValue;
                    break;
                case "ecl":
                    passportData.EyeColor = propValue;
                    break;
                case "pid":
                    passportData.PassportId = propValue;
                    break;
                case "cid":
                    passportData.CountryId = propValue;
                    break;
            }
        }
    }

    public class PassportData
    {
        public string BirthYear { get; set; }
        public string IssueYear { get; set; }
        public string ExpirationYear { get; set; }
        public string Height { get; set; }
        public string HairColor { get; set; }
        public string EyeColor { get; set; }
        public string PassportId { get; set; }
        public string CountryId { get; set; }

        public bool IsValid1 =>
            !string.IsNullOrWhiteSpace(BirthYear)
            && !string.IsNullOrWhiteSpace(IssueYear)
            && !string.IsNullOrWhiteSpace(ExpirationYear)
            && !string.IsNullOrWhiteSpace(Height)
            && !string.IsNullOrWhiteSpace(HairColor)
            && !string.IsNullOrWhiteSpace(EyeColor)
            && !string.IsNullOrWhiteSpace(PassportId);

        public bool IsValid2 =>
            ValidateNumber(BirthYear, 1920, 2002)
            && ValidateNumber(IssueYear, 2010, 2020)
            && ValidateNumber(ExpirationYear, 2020, 2030)
            && ValidateHeight(Height)
            && ValidateHairColor(HairColor)
            && ValidateEyeColor(EyeColor)
            && ValidatePassportId(PassportId);

        private static bool ValidatePassportId(string passportId)
        {
            if (string.IsNullOrWhiteSpace(passportId))
            {
                return false;
            }

            if (passportId.Length != 9)
            {
                return false;
            }

            foreach (var c in passportId)
            {
                if (!char.IsNumber(c))
                {
                    return false;
                }
            }

            return true;
        }

        private static List<string> ValidEyeColors = new List<string>{ "amb", "blu", "brn", "gry", "grn", "hzl", "oth" };
        private static bool ValidateEyeColor(string eyeColor)
        {
            if (string.IsNullOrWhiteSpace(eyeColor))
            {
                return false;
            }

            var validateEyeColor = ValidEyeColors.Contains(eyeColor);
            return validateEyeColor;
        }

        private const string ValidHairColorChars = "abcdef0123456789";
        private static bool ValidateHairColor(string hairColor)
        {
            if (string.IsNullOrWhiteSpace(hairColor))
            {
                return false;
            }

            var firstChar = hairColor.Substring(0, 1);
            if (!firstChar.Equals("#"))
            {
                return false;
            }

            var chars = hairColor.Substring(1);
            if (chars.Length != 6)
            {
                return false;
            }

            foreach (var c in chars)
            {
                if (!ValidHairColorChars.Contains(c))
                {
                    return false;
                }
            }

            return true;
        }

        private static bool ValidateHeight(string height)
        {
            if (string.IsNullOrWhiteSpace(height) || height.Length < 2)
            {
                return false;
            }

            var unit = height.Trim().Substring(height.Length - 2, 2).ToLower();
            if (!unit.Equals("cm") && !unit.Equals("in"))
            {
                return false;
            }

            var number = height.Substring(0, height.Length - 2);

            if (unit.Equals("cm"))
            {
                return ValidateNumber(number, 150, 193);
            }

            return ValidateNumber(number, 59, 76);
        }

        private static bool ValidateNumber(string s, int minValue, int maxValue)
        {
            if (!int.TryParse(s, out var i))
            {
                return false;
            }

            return i >= minValue && i <= maxValue;
        }
    }
}
