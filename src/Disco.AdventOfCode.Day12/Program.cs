using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Disco.AdventOfCode.Day12
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var input = await File.ReadAllLinesAsync("input.txt");

            var instructions = input.Select(x => new Instruction(char.Parse(x.Substring(0, 1)), int.Parse(x.Substring(1)))).ToList();

            var stage1 = new Stage1();
            stage1.Run(instructions);

            var stage2 = new Stage2();
            stage2.Run(instructions);
        }
    }
}
