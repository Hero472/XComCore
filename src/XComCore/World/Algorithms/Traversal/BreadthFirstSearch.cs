using System.Collections.Generic;
using XComCore.World.Geometry;
using XComCore.World.Grid;

namespace XComCore.World.Algorithms.Traversal
{
    public sealed class BreadthFirstSearch
    {
        public static ReachableTiles Search(
            IGrid grid,
            Position start,
            int movementPoints
        )
        {
            var frontier = new Queue<Position>();
            var costs = new Dictionary<Position, int>();

            frontier.Enqueue(start);
            costs[start] = 0;

            while (frontier.Count > 0)
            {
                var current = frontier.Dequeue();
                var currentCost = costs[current];

                foreach (var neighbor in grid.GetNeighbors(current))
                {
                    if (!grid.IsWalkable(neighbor))
                        continue;

                    int nextCost = currentCost + grid.GetMovementCost(neighbor);

                    if (nextCost > movementPoints)
                        continue;

                    if (costs.TryGetValue(neighbor, out int existingCost) &&
                        existingCost <= nextCost)
                        continue;

                    costs[neighbor] = nextCost;
                    frontier.Enqueue(neighbor);
                }
            }

            return new ReachableTiles(costs);
        }
    }
}