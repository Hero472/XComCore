using XComCore.World.Entities;
using XComCore.World.Geometry;

namespace XComCore.World.Grid.Builders
{
    public sealed class GridBuilder
    {
        private readonly SquareGrid _grid;

        public GridBuilder(uint width, uint height)
        {
            _grid = new SquareGrid(width, height);
        }

        public GridBuilder Block(Position position)
        {
            _grid.SetWalkable(position, false);
            return this;
        }

        public GridBuilder Block(uint x, uint y)
        {
            return Block(new Position(x, y));
        }

        public GridBuilder Walkable(Position position)
        {
            _grid.SetWalkable(position, true);
            return this;
        }

        public GridBuilder Walkable(uint x, uint y)
        {
            return Walkable(new Position(x, y));
        }

        public GridBuilder Cost(Position position, int cost)
        {
            _grid.SetMovementCost(position, cost);
            return this;
        }

        public GridBuilder Cost(uint x, uint y, int cost)
        {
            return Cost(new Position(x, y), cost);
        }

        public Result<GridBuilder, GridError> Place(IGridEntity entity)
        {
            return _grid.PlaceEntity(entity).Match<Result<GridBuilder, GridError>>(
                _ => Result.Ok(this),
                error => Result.Err(error)
            );
        }

        public IGrid Build()
        {
            return _grid;
        }
    }
}