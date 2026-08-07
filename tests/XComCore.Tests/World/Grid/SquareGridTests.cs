using XComCore.World.Geometry;
using XComCore.World.Grid;
using XComCore.World.Entities.IWorldEntity;

namespace XComCore.Tests.World.Grid;

public class SquareGridTests
{
    [Fact]
    public void Constructor_ShouldCreateCorrectNumberOfTiles()
    {
        var grid = new SquareGrid(5, 5);

        Assert.Equal(
            25,
            grid.Tiles.Count()
        );
    }

    [Fact]
    public void Constructor_ShouldCreateWalkableTiles()
    {
        var grid = new SquareGrid(5, 5);

        foreach (var tile in grid.Tiles)
        {
            Assert.True(tile.Walkable);
        }
    }

    [Fact]
    public void Contains_ShouldReturnTrue()
    {
        var grid = new SquareGrid(5, 5);

        Assert.True(
            grid.Contains(new Position(2, 2))
        );
    }

    [Fact]
    public void Contains_ShouldReturnFalse()
    {
        var grid = new SquareGrid(5, 5);

        Assert.False(
            grid.Contains(new Position(5, 2))
        );
    }

    [Fact]
    public void SetWalkable_ShouldBlockTile()
    {
        var grid = new SquareGrid(5, 5);

        grid.SetWalkable(
            new Position(2, 2),
            false
        );

        Assert.False(
            grid.IsWalkable(new Position(2, 2))
        );
    }

    [Fact]
    public void SetWalkable_ShouldRestoreWalkable()
    {
        var grid = new SquareGrid(5, 5);

        var position = new Position(2, 2);

        grid.SetWalkable(position, false);
        grid.SetWalkable(position, true);

        Assert.True(
            grid.IsWalkable(position)
        );
    }

    [Fact]
    public void IsWalkable_ShouldReturnFalse_WhenOutsideGrid()
    {
        var grid = new SquareGrid(5, 5);

        Assert.False(
            grid.IsWalkable(new Position(10, 10))
        );
    }

    [Fact]
    public void DefaultMovementCost_ShouldBeOne()
    {
        var grid = new SquareGrid(5, 5);

        Assert.Equal(
            1,
            grid.GetMovementCost(new Position(2, 2))
        );
    }

    [Fact]
    public void SetMovementCost_ShouldUpdateTile()
    {
        var grid = new SquareGrid(5, 5);

        var position = new Position(2, 2);

        grid.SetMovementCost(position, 5);

        Assert.Equal(
            5,
            grid.GetMovementCost(position)
        );
    }

    [Fact]
    public void GetTile_ShouldReturnTile()
    {
        var grid = new SquareGrid(5, 5);

        var result = grid.GetTile(new Position(1, 1));

        Assert.True(result.IsSuccess);

        Assert.Equal(
            new Position(1, 1),
            result.Unwrap().Position
        );
    }

    [Fact]
    public void GetTile_ShouldFail_WhenOutsideGrid()
    {
        var grid = new SquareGrid(5, 5);

        var result = grid.GetTile(new Position(10, 10));

        Assert.True(result.IsFailure);

        Assert.Equal(
            GridError.OutsideGrid,
            result.UnwrapErr()
        );
    }

    [Fact]
    public void Center_ShouldHaveFourNeighbors()
    {
        var grid = new SquareGrid(5, 5);

        var neighbors =
            grid.GetNeighbors(new Position(2, 2))
                .ToList();

        Assert.Equal(4, neighbors.Count);
    }

    [Fact]
    public void Corner_ShouldHaveTwoNeighbors()
    {
        var grid = new SquareGrid(5, 5);

        var neighbors =
            grid.GetNeighbors(new Position(0, 0))
                .ToList();

        Assert.Equal(2, neighbors.Count);
    }

    [Fact]
    public void Edge_ShouldHaveThreeNeighbors()
    {
        var grid = new SquareGrid(5, 5);

        var neighbors =
            grid.GetNeighbors(new Position(2, 0))
                .ToList();

        Assert.Equal(3, neighbors.Count);
    }

    [Fact]
    public void PlaceEntity_ShouldOccupyTile()
    {
        var grid = new SquareGrid(5, 5);

        var entity = new DummyEntity(
            new Position(2, 2)
        );

        var result = grid.PlaceEntity(entity);

        Assert.True(result.IsSuccess);

        Assert.True(
            grid.GetTile(new Position(2, 2))
                .Unwrap()
                .HasEntity
        );
    }

    [Fact]
    public void PlaceEntity_ShouldFail_WhenOccupied()
    {
        var grid = new SquareGrid(5, 5);

        var entity1 = new DummyEntity(new Position(2, 2));
        var entity2 = new DummyEntity(new Position(2, 2));

        grid.PlaceEntity(entity1);

        var result = grid.PlaceEntity(entity2);

        Assert.True(result.IsFailure);

        Assert.Equal(
            GridError.Occupied,
            result.UnwrapErr()
        );
    }

    [Fact]
    public void PlaceEntity_ShouldFail_WhenOutsideGrid()
    {
        var grid = new SquareGrid(5, 5);

        var entity = new DummyEntity(
            new Position(10, 10)
        );

        var result = grid.PlaceEntity(entity);

        Assert.True(result.IsFailure);

        Assert.Equal(
            GridError.OutsideGrid,
            result.UnwrapErr()
        );
    }

    [Fact]
    public void RemoveEntity_ShouldFreeTile()
    {
        var grid = new SquareGrid(5, 5);

        var entity = new DummyEntity(
            new Position(2, 2)
        );

        grid.PlaceEntity(entity);

        var result = grid.RemoveEntity(entity);

        Assert.True(result.IsSuccess);

        Assert.False(
            grid.GetTile(new Position(2, 2))
                .Unwrap()
                .HasEntity
        );
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