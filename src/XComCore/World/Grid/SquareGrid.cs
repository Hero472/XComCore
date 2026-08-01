using System.Collections.Generic;
using XComCore.World.Geometry;
using XComCore.World.Grid.Entities;

namespace XComCore.World.Grid
{
    public sealed class SquareGrid : IGrid
    {
        private readonly GridTile[] _tiles;

        public IEnumerable<GridTile> Tiles
        {
            get
            {
                foreach (var tile in _tiles)
                    yield return tile;
            }
        }

        public GridBounds Bounds { get; }

        public SquareGrid(uint width, uint height)
        {
            Bounds = new GridBounds(width, height);

            _tiles = new GridTile[width * height];

            for (uint y = 0; y < height; y++)
            {
                for (uint x = 0; x < width; x++)
                {
                    var position = new Position(x, y);

                    _tiles[Index(position)] = new GridTile(position);
                }
            }
        }

        public bool Contains(Position position)
            => Bounds.Contains(position);

        public bool IsWalkable(Position position)
        {
            var tile = GetTile(position);

            return tile.Match(
                success: t => t.Walkable,
                failure: _ => false
            );
        }

        public Result<Unit, GridError> SetWalkable(Position position, bool walkable)
        {
            return GetTile(position).Match<Result<Unit, GridError>>(
                tile =>
                {
                    tile.SetWalkable(walkable);
                    return Result.Ok(Unit.Value);
                },
                error => Result.Err(error)
            );
        }

        public IEnumerable<Position> GetNeighbors(Position position)
        {
            foreach (var offset in Offset.Cardinal)
            {
                var result = position.Offset(offset);

                if (!result.IsSuccess)
                    continue;

                var neighbor = result.Unwrap();

                if (Contains(neighbor))
                    yield return neighbor;
            }
        }

        public int GetMovementCost(Position position)
        {
            return _tiles[Index(position)].MovementCost;
        }

        public void SetMovementCost(Position position, int cost)
        {
            _tiles[Index(position)].SetMovementCost(cost);
        }

        private int Index(Position position)
            => checked((int)(position.Y * Bounds.Width + position.X));

        public Result<GridTile, GridError> GetTile(Position position)
        {
            if (!Contains(position))
                return Result.Err(GridError.OutsideGrid);

            return Result.Ok(_tiles[Index(position)]);
        }

        public bool CanMoveTo(Position position)
        {
            return Contains(position) && IsWalkable(position);
        }

        public Result<Unit, GridError> PlaceEntity(IGridEntity entity)
        {
            // Validate first
            foreach (var position in entity.OccupiedTiles)
            {
                if (!Contains(position))
                    return Result.Err(GridError.OutsideGrid);

                var tile = GetTile(position).Unwrap();

                if (tile.HasEntity)
                    return Result.Err(GridError.Occupied);
            }

            // Place
            foreach (var position in entity.OccupiedTiles)
            {
                GetTile(position)
                    .Unwrap()
                    .PlaceEntity(entity);
            }

            return Result.Ok(Unit.Value);
        }

        public Result<Unit, GridError> RemoveEntity(IGridEntity entity)
        {
            foreach (var position in entity.OccupiedTiles)
            {
                if (!Contains(position))
                    continue;

                GetTile(position)
                    .Unwrap()
                    .RemoveEntity();
            }

            return Result.Ok(Unit.Value);
        }
    }
}