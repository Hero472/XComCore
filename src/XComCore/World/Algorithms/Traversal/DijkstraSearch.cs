using System.Collections.Generic;
using XComCore.Collections;
using XComCore.World.Geometry;
using XComCore.World.Grid;

namespace XComCore.World.Algorithms.Traversal
{
    public sealed class DijkstraSearch
    {
        public static ReachableTiles Search(
            IGrid grid,
            Position start,
            int movementPoints)
        {
            var frontier = new PriorityQueue<Position>();
            var costs = new Dictionary<Position, int>();

            frontier.Enqueue(start, 0);
            costs[start] = 0;

            while (frontier.Count > 0)
            {
                var node = frontier.Dequeue();

                var current = node.Item;

                if (node.Priority != costs[current])
                    continue;

                var currentCost = costs[current];

                foreach (var neighbor in grid.GetNeighbors(current))
                {
                    if (!grid.IsWalkable(neighbor))
                        continue;

                    int movementCost = grid.GetMovementCost(neighbor);
                    int nextCost = currentCost + movementCost;

                    if (nextCost > movementPoints)
                        continue;

                    if (costs.TryGetValue(neighbor, out int existingCost) &&
                        existingCost <= nextCost)
                    {
                        continue;
                    }

                    costs[neighbor] = nextCost;
                    frontier.Enqueue(neighbor, nextCost);
                }
            }

            return new ReachableTiles(costs);
        }
    }
}