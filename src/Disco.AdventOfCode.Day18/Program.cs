using System.IO;
using System.Threading.Tasks;

namespace Disco.AdventOfCode.Day18
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var input = await File.ReadAllLinesAsync("input.txt");

            var stage1 = new Stage1();
            stage1.Run(input);

            var stage2 = new Stage2();
            stage2.Run(input);
        }
    }
}
