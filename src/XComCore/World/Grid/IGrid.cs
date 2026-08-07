using System.Collections.Generic;
using XComCore.World.Entities.IWorldEntity;
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
        void SetMovementCost(Position position, int cost);
        Result<GridTile, GridError> GetTile(Position position);
        bool CanMoveTo(Position position);
        public Result<Unit, GridError> PlaceEntity(IGridEntity entity);
        public Result<Unit, GridError> RemoveEntity(IGridEntity entity);
    }
}