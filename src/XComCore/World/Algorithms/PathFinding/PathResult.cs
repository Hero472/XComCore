using System.Collections.Generic;
using XComCore.World.Geometry;

namespace XComCore.World.Algorithms.PathFinding
{
    public sealed class PathResult
    {
        public bool Found { get; }

        public IReadOnlyList<Position> Path { get; }

        public int Cost { get; }

        public PathResult(
            bool found,
            IReadOnlyList<Position> path,
            int cost
        )
        {
            Found = found;
            Path = path;
            Cost = cost;
        }
    }
}