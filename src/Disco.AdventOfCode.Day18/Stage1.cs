using System.Collections.Generic;
using System.Linq;

namespace Disco.AdventOfCode.Day18
{
    public class Stage1 : Stage
    {
        protected override string CalculateParts(List<string> parts)
        {
            while (true)
            {
                var x = parts.Take(3).ToList();
                if (x.Count < 3)
                {
                    break;
                }

                var s = DataTableCompute(string.Join("", x));

                parts.RemoveAt(0);
                parts.RemoveAt(0);
                parts.RemoveAt(0);

                parts.Insert(0, s);
            }

            return parts[0];
        }
    }
}
