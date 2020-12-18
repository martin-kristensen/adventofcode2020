using System;
using System.Collections.Generic;
using System.Linq;

namespace Disco.AdventOfCode.Day17
{
    public class Step2
    {
        private readonly bool _render;
        private Dictionary<string, Point4D> _state;

        public Step2(string[] input, in bool render)
        {
            _render = render;
            _state = new Dictionary<string, Point4D>();

            for (var y = 0; y < input.Length; y++)
            {
                for (var x = 0; x < input[0].Length; x++)
                {
                    if (input[y][x].Equals('#'))
                    {
                        var activePoint = new Point4D(x, y, 0, 0)
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
            RenderState();

            for (var cycleIndex = 0; cycleIndex < 6; cycleIndex++)
            {
                Console.WriteLine($"Cycle #{cycleIndex + 1}");

                var pointsToConsider = new List<Point4D>();
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

                var newState = new Dictionary<string, Point4D>();
                foreach (var pointToConsider in pointsToConsider)
                {
                    var p = _state.ContainsKey(pointToConsider.Id) ? _state[pointToConsider.Id] : pointToConsider;

                    if (GetShouldBeActive(p))
                    {
                        p.Active = true;
                        newState.Add(p.Id, p);
                    }
                }

                _state = newState;
                RenderState();
            }

            Console.WriteLine($"Number of active points: {_state.Count}");
        }

        private bool GetShouldBeActive(Point4D point)
        {
            var neighbours = GetNeighbours(point);
            foreach (var neighbour in neighbours.Where(n => _state.ContainsKey(n.Id)))
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

        private static List<Point4D> GetNeighbours(Point4D point)
        {
            var neighbours = new List<Point4D>();

            for (var w = point.W - 1; w < point.W + 2; w++)
            {
                for (var z = point.Z - 1; z < point.Z + 2; z++)
                {
                    for (var y = point.Y - 1; y < point.Y + 2; y++)
                    {
                        for (var x = point.X - 1; x < point.X + 2; x++)
                        {
                            neighbours.Add(new Point4D(x, y, z, w));
                        }
                    }
                }
            }

            return neighbours;
        }

        private void RenderState()
        {
            if (!_render) return;

            var minX = _state.Min(x => x.Value.X);
            var maxX = _state.Max(x => x.Value.X);
            var minY = _state.Min(x => x.Value.Y);
            var maxY = _state.Max(x => x.Value.Y);
            var minZ = _state.Min(x => x.Value.Z);
            var maxZ = _state.Max(x => x.Value.Z);
            var minW = _state.Min(x => x.Value.W);
            var maxW = _state.Max(x => x.Value.W);

            for (var w = minW; w <= maxW; w++)
            {
                for (var z = minZ; z <= maxZ; z++)
                {
                    Console.WriteLine($"Z={z} W={w} X={minX},{maxX} Y={minY},{maxY}");
                    for (var y = minY; y <= maxY; y++)
                    {
                        for (var x = minX; x <= maxX; x++)
                        {
                            var point = _state.Select(p => p.Value).SingleOrDefault(s => s.Id.Equals(Point4D.GetPointId(x, y, z, w)));
                            Console.Write(point == null ? '.' : '#');
                        }

                        Console.WriteLine();
                    }

                    Console.WriteLine();
                }
            }
        }
    }
}