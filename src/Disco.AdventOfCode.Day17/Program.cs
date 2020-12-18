using System.IO;
using System.Threading.Tasks;

namespace Disco.AdventOfCode.Day17
{
    class Program
    {
        private static readonly bool Render = false;

        static async Task Main(string[] args)
        {
            var input = await File.ReadAllLinesAsync("input.txt");

            //var step1 = new Step1(input, Render);
            //step1.Run();

            var step2 = new Step2(input, Render);
            step2.Run();
        }
    }
}
