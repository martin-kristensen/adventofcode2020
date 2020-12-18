using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Disco.AdventOfCode.Day7
{
    class Program
    {
        public static List<Bag> BagsThatCanHoldMyColor = new List<Bag>();
        public static int NumberOfBagsRequired;

        static async Task Main(string[] args)
        {
            var input = await File.ReadAllLinesAsync("input.txt");

            var myBagColor = "shiny gold";

            var bags = GetBags(input);
            PopulateCanHold(bags);

            var bagsWithoutMyColor = bags.Where(b => !b.Color.Equals(myBagColor)).ToList();

            foreach (var bag in bagsWithoutMyColor)
            {
                CanHoldBag(bag, myBagColor);
            }

            Console.WriteLine($"Number of possible bags: {BagsThatCanHoldMyColor.Distinct().Count()}");

            var myBag = bags.Single(b => b.Color.Equals(myBagColor));
            CountNumberOfBagsRequired(myBag, 1);

            Console.WriteLine($"Number of bags required for color '{myBagColor}': {NumberOfBagsRequired}");
        }

        private static void CountNumberOfBagsRequired(Bag bag, int multiplyBy)
        {
            foreach (var bagCanHoldBag in bag.CanHoldBags)
            {
                NumberOfBagsRequired += bagCanHoldBag.Value * multiplyBy;
                CountNumberOfBagsRequired(bagCanHoldBag.Key, bagCanHoldBag.Value * multiplyBy);
            }
        }

        private static bool CanHoldBag(Bag bag, string myBagColor)
        {
            if (bag.CanHoldBags.Select(x => x.Key.Color).Contains(myBagColor))
            {
                BagsThatCanHoldMyColor.Add(bag);
                return true;
            }

            foreach (var bagCanHoldBag in bag.CanHoldBags)
            {
                if (CanHoldBag(bagCanHoldBag.Key, myBagColor))
                {
                    BagsThatCanHoldMyColor.Add(bag);
                    return true;
                }
            }

            return false;
        }

        private static void PopulateCanHold(List<Bag> bags)
        {
            foreach (var bag in bags)
            {
                foreach (var bagCanHoldString in bag.CanHoldStrings)
                {
                    var canHoldBag = bags.Single(b => b.Color.Equals(bagCanHoldString.Key));
                    bag.CanHoldBags.Add(canHoldBag, bagCanHoldString.Value);
                }
            }
        }

        private static List<Bag> GetBags(IEnumerable<string> input)
        {
            var bags = new List<Bag>();
            foreach (var line in input)
            {
                var color = line.Split(new[] {" bags contain "}, StringSplitOptions.RemoveEmptyEntries)[0];
                var canHoldString = line.Split(new[] {" bags contain "}, StringSplitOptions.RemoveEmptyEntries)[1].Replace(" bags", string.Empty).Replace(" bag", string.Empty);

                var canHold = canHoldString.Replace(".", string.Empty).Split(new []{","}, StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim()).ToList();

                bags.Add(new Bag
                {
                    Color = color,
                    CanHoldStrings = GetCanHoldDictionary(canHold)
                });
            }

            return bags;
        }

        private static Dictionary<string, int> GetCanHoldDictionary(List<string> canHold)
        {
            var dictionary = new Dictionary<string, int>();
            foreach (var x in canHold)
            {
                if (!x.Equals("no other"))
                {
                    var count = int.Parse(x.Split(' ')[0]);
                    var key = x.Replace($"{count} ", string.Empty);

                    dictionary.Add(key, count);
                }
            }

            return dictionary;
        }

        public class Bag
        {
            public Bag()
            {
                CanHoldBags = new Dictionary<Bag, int>();
            }

            public string Color { get; set; }

            public Dictionary<string, int> CanHoldStrings { get; set; }

            public Dictionary<Bag, int> CanHoldBags { get; set; }
        }
    }
}
