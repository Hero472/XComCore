using System.Collections.Generic;
using XComCore.World.Geometry;

namespace XComCore.World.Algorithms.Traversal
{
    public sealed class ReachableTiles
    {
        private readonly Dictionary<Position, int> _costs;

        public IReadOnlyDictionary<Position, int> Costs => _costs;

        public ReachableTiles(Dictionary<Position, int> costs)
        {
            _costs = costs;
        }

        public bool Contains(Position position)
            => _costs.ContainsKey(position);

        public int CostTo(Position position)
            => _costs[position];
    }
}