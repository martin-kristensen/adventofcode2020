using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Disco.AdventOfCode.Day11
{
    class Program
    {
        public static int Width;
        public static int Height;

        static async Task Main(string[] args)
        {
            var runValidation = true;

            var inputFileName = runValidation ? "validation" : "input";
            var input = await File.ReadAllLinesAsync($"{inputFileName}.txt");

            Width = input.First().Length;
            Height = input.Length;

            var slots1 = input.SelectMany(x => x.ToCharArray()).ToArray();
            var stage1 = new Stage1(Width, Height);
            if (runValidation)
            {
                stage1.RunVisualValidation(slots1);
            }
            stage1.Run(slots1);

            var slots2 = input.SelectMany(x => x.ToCharArray()).ToArray();
            var stage2 = new Stage2(Width, Height);
            if (runValidation)
            {
                stage2.RunVisualValidation(slots2);
            }
            stage2.Run(slots2);
        }
    }
}
