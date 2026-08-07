using XComCore.World.Geometry;
using XComCore.World.Grid;

namespace XComCore.World.Algorithms.PathFinding
{
    public interface IPathFinder
    {
        PathResult Search(
            IGrid grid,
            Position start,
            Position goal,
            int movementPoints
        );
    }
}