using XComCore.World.Entities.IWorldEntity;

namespace XComCore.World.Grid
{
    public static class EntityPlacement
    {
        public static Result<Unit, GridError> CanPlace(
            IGrid grid,
            IGridEntity entity
        )
        {
            foreach(var tile in entity.OccupiedTiles)
            {
                if(!grid.Contains(tile))
                    return Result.Err(GridError.OutsideGrid);

                if(!grid.IsWalkable(tile))
                    return Result.Err(GridError.TileBlocked);
            }

            return Result.Ok(Unit.Value);
        }
    }
}