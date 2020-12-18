using System;
using System.Collections.Generic;
using System.Linq;

namespace Disco.AdventOfCode.Day17
{
    public class Step1
    {
        private readonly bool _render;
        private Dictionary<string, Point3D> _state;

        public Step1(string[] input, bool render)
        {
            _render = render;
            _state = new Dictionary<string, Point3D>();

            for (var y = 0; y < input.Length; y++)
            {
                for (var x = 0; x < input[0].Length; x++)
                {
                    if (input[y][x].Equals('#'))
                    {
                        var activePoint = new Point3D(x, y, 0)
                        {
                            Active = true
                        };
                        _state.Add(activePoint.Id, activePoint);
                    }
                }
            }
        }

        public void Run()
        {
            RenderState(_state);

            for (var cycleIndex = 0; cycleIndex < 6; cycleIndex++)
            {
                Console.WriteLine($"Cycle #{cycleIndex + 1}");

                var pointsToConsider = new List<Point3D>();
                foreach (var activePoint in _state)
                {
                    var neighbours = GetNeighbours(activePoint.Value);
                    foreach (var neighbour in neighbours)
                    {
                        if (pointsToConsider.SingleOrDefault(x => x.Id.Equals(neighbour.Id)) == null)
                        {
                            pointsToConsider.Add(neighbour);
                        }
                    }
                }

                var newState = new Dictionary<string, Point3D>();
                foreach (var pointToConsider in pointsToConsider)
                {
                    var p = _state.ContainsKey(pointToConsider.Id) ? _state[pointToConsider.Id] : pointToConsider;

                    if (GetShouldBeActive(p))
                    {
                        p.Active = true;
                        newState.Add(p.Id, p);
                    }
                }

                RenderState(newState);
                _state = newState;
            }

            Console.WriteLine($"Number of active points: {_state.Count}");
        }

        private bool GetShouldBeActive(Point3D point)
        {
            var neighbours = GetNeighbours(point);
            foreach (var neighbour in neighbours.Where(neighbour => _state.ContainsKey(neighbour.Id)))
            {
                neighbour.Active = true;
            }

            var activeNeighbourCount = neighbours.Count(x => x.Active.HasValue && x.Active.Value && !x.Id.Equals(point.Id));

            if (point.Active.HasValue && point.Active.Value)
            {
                if (activeNeighbourCount == 2 || activeNeighbourCount == 3)
                {
                    return true;
                }
            }
            else
            {
                if (activeNeighbourCount == 3)
                {
                    return true;
                }
            }

            return false;
        }

        private static List<Point3D> GetNeighbours(Point3D point)
        {
            var neighbours = new List<Point3D>();

            for (var z = point.Z - 1; z < point.Z + 2; z++)
            {
                for (var y = point.Y - 1; y < point.Y + 2; y++)
                {
                    for (var x = point.X - 1; x < point.X + 2; x++)
                    {
                        neighbours.Add(new Point3D(x, y, z));
                    }
                }
            }

            return neighbours;
        }

        private void RenderState(Dictionary<string, Point3D> state)
        {
            if (!_render) return;

            var minX = state.Min(x => x.Value.X);
            var maxX = state.Max(x => x.Value.X);
            var minY = state.Min(x => x.Value.Y);
            var maxY = state.Max(x => x.Value.Y);
            var minZ = state.Min(x => x.Value.Z);
            var maxZ = state.Max(x => x.Value.Z);

            for (var z = minZ; z <= maxZ; z++)
            {
                Console.WriteLine($"Z={z} X={minX},{maxX} Y={minY},{maxY}");
                for (var y = minY; y <= maxY; y++)
                {
                    for (var x = minX; x <= maxX; x++)
                    {
                        var point = state.Select(p => p.Value).SingleOrDefault(s => s.Id.Equals(Point3D.GetPointId(x, y, z)));
                        Console.Write(point == null ? '.' : '#');
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
            }
        }
    }
}
