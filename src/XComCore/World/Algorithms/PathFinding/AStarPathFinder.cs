using System;
using System.Collections.Generic;
using XComCore.Collections;
using XComCore.World.Geometry;
using XComCore.World.Grid;

namespace XComCore.World.Algorithms.PathFinding
{
    public sealed class AStarPathfinder
    {
        public static PathResult Search(
            IGrid grid,
            Position start,
            Position goal,
            int movementPoints
        )
        {
            var frontier = new PriorityQueue<Position>();

            var costs = new Dictionary<Position, int>();
            var cameFrom = new Dictionary<Position, Position>();

            frontier.Enqueue(start, 0);
            costs[start] = 0;

            while (frontier.Count > 0)
            {
                var node = frontier.Dequeue();

                var current = node.Item;

                if (node.Priority != costs[current] + Heuristic(current, goal))
                    continue;

                foreach (var neighbor in grid.GetNeighbors(current))
                {
                    if (!grid.IsWalkable(neighbor))
                        continue;

                    int nextCost = costs[current] + grid.GetMovementCost(neighbor);

                    if (nextCost > movementPoints)
                        continue;

                    if (costs.TryGetValue(neighbor, out int existingCost) &&
                        existingCost <= nextCost)
                    {
                        continue;
                    }

                    costs[neighbor] = nextCost;
                    cameFrom[neighbor] = current;

                    int priority = nextCost + Heuristic(neighbor, goal);

                    frontier.Enqueue(neighbor, priority);
                }
            }

            if (!costs.ContainsKey(goal))
            {
                return new PathResult(
                    false,
                    Array.Empty<Position>(),
                    0
                );
            }

            return new PathResult(
                true,
                ReconstructPath(cameFrom, start, goal),
                costs[goal]
            );
        }

        private static List<Position> ReconstructPath(
            Dictionary<Position, Position> cameFrom,
            Position start,
            Position goal)
        {
            var path = new List<Position>();

            var current = goal;

            path.Add(current);

            while (current != start)
            {
                current = cameFrom[current];
                path.Add(current);
            }

            path.Reverse();

            return path;
        }

        private static int Heuristic(Position a, Position b)
        {
            return Math.Abs((int)a.X - (int)b.X)
                 + Math.Abs((int)a.Y - (int)b.Y);
        }
    }
}