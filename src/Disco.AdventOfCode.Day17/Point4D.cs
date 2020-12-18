namespace Disco.AdventOfCode.Day17
{
    public class Point4D : Point3D
    {
        public int W { get; }

        public Point4D(int x, int y, int z, int w) : base(x, y, z)
        {
            W = w;
            Id = GetPointId(x, y, z, w);
        }

        public static string GetPointId(int x, int y, int z, int w)
        {
            return $"{x},{y},{z},{w}";
        }
    }
}