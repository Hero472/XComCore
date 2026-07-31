using System.Collections.Generic;
using XComCore.World.Geometry;

namespace XComCore.World.Grid
{
    public interface IGrid
    {
        GridBounds Bounds { get; }

        bool Contains(Position position);
        bool IsWalkable(Position position);
        Result<Unit, GridError> SetWalkable(Position position, bool walkable);
        IEnumerable<Position> GetNeighbors(Position position);
        int GetMovementCost(Position position);
        Result<GridTile, GridError> GetTile(Position position);
        bool CanMoveTo(Position position);
    }
}