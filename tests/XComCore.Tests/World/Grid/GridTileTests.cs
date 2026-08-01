using XComCore.World.Geometry;
using XComCore.World.Grid;
using XComCore.World.Entities;

namespace XComCore.Tests.World.Grid;

public class GridTileTests
{
    [Fact]
    public void NewTile_ShouldBeWalkable()
    {
        var tile = new GridTile(new Position(0, 0));

        Assert.True(tile.Walkable);
        Assert.False(tile.HasEntity);
        Assert.Equal(1, tile.MovementCost);
    }

    [Fact]
    public void SetWalkable_ShouldChangeState()
    {
        var tile = new GridTile(new Position(0, 0));

        tile.SetWalkable(false);

        Assert.False(tile.Walkable);

        tile.SetWalkable(true);

        Assert.True(tile.Walkable);
    }

    [Fact]
    public void PlaceEntity_ShouldOccupyTile()
    {
        var tile = new GridTile(new Position(1, 1));
        var entity = new DummyEntity(new Position(1, 1));

        tile.PlaceEntity(entity);

        Assert.True(tile.HasEntity);
        Assert.False(tile.Walkable);
        Assert.Equal(entity, tile.Entity);
    }

    [Fact]
    public void RemoveEntity_ShouldRemoveOccupant()
    {
        var tile = new GridTile(new Position(1, 1));
        var entity = new DummyEntity(new Position(1, 1));

        tile.PlaceEntity(entity);
        tile.RemoveEntity();

        Assert.False(tile.HasEntity);
        Assert.True(tile.Walkable);
    }

    [Fact]
    public void SetMovementCost_ShouldUpdateCost()
    {
        var tile = new GridTile(new Position(2, 2));

        tile.SetMovementCost(4);

        Assert.Equal(4, tile.MovementCost);
    }

    private sealed class DummyEntity : IGridEntity
    {
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