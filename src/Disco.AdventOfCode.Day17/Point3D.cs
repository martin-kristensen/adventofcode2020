namespace Disco.AdventOfCode.Day17
{
    public class Point3D
    {
        public Point3D(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
            Id = GetPointId(X, Y, Z);
        }

        public int X { get; }
        public int Y { get; }
        public int Z { get; }
        public bool? Active { get; set; }

        public string Id { get; set; }

        public static string GetPointId(int x, int y, int z)
        {
            return $"{x},{y},{z}";
        }
    }
}