using XComCore.World.Geometry;
using XComCore.World.Grid.Builders;
using XComCore.World.Entities.IWorldEntity;

namespace XComCore.Tests.World.Grid;

public class GridBuilderTests
{
    [Fact]
    public void Builder_ShouldCreateGrid()
    {
        var grid = new GridBuilder(10, 5)
            .Build();

        Assert.Equal((uint)10, grid.Bounds.Width);
        Assert.Equal((uint)5, grid.Bounds.Height);
    }

    [Fact]
    public void Block_ShouldCreateBlockedTile()
    {
        var position = new Position(2, 3);

        var grid = new GridBuilder(5, 5)
            .Block(position)
            .Build();

        Assert.False(grid.IsWalkable(position));
    }

    [Fact]
    public void Cost_ShouldAssignMovementCost()
    {
        var position = new Position(1, 4);

        var grid = new GridBuilder(5, 5)
            .Cost(position, 5)
            .Build();

        Assert.Equal(
            5,
            grid.GetMovementCost(position)
        );
    }

    [Fact]
    public void Place_ShouldPlaceEntity()
    {
        var entity = new DummyEntity(
            new Position(2, 2)
        );

        var builder = new GridBuilder(5, 5);

        var result = builder.Place(entity);

        Assert.True(result.IsSuccess);

        var grid = builder.Build();

        var tile = grid.GetTile(entity.Origin).Unwrap();

        Assert.True(tile.HasEntity);
    }

    private sealed class DummyEntity : IGridEntity
    {
        public Guid Id { get; } = Guid.NewGuid();
        public Position Origin { get; }

        public IReadOnlyCollection<Offset> FootPrint { get; }

        public IReadOnlyCollection<Position> OccupiedTiles { get; }

        public DummyEntity(Position position)
        {
            Origin = position;

            FootPrint =
            [
                new Offset(0, 0)
            ];

            OccupiedTiles =
            [
                position
            ];
        }
    }
}