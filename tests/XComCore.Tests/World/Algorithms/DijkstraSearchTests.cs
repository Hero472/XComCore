using XComCore.World.Algorithms.Traversal;
using XComCore.World.Geometry;
using XComCore.World.Grid;

namespace XComCore.Tests.World.Algorithms.Traversal;

public class DijkstraTests
{
    [Fact]
    public void Search_ShouldReturnStartTile()
    {
        var grid = new SquareGrid(5, 5);

        var start = new Position(2, 2);

        var result = DijkstraSearch.Search(
            grid,
            start,
            5
        );

        Assert.True(
            result.Contains(start)
        );

        Assert.Equal(
            0,
            result.CostTo(start)
        );
    }

    [Fact]
    public void Search_ShouldRespectMovementPoints()
    {
        var grid = new SquareGrid(10, 10);

        var result = DijkstraSearch.Search(
            grid,
            new Position(0, 0),
            3
        );

        Assert.True(
            result.Contains(new Position(3, 0))
        );

        Assert.False(
            result.Contains(new Position(4, 0))
        );
    }

    [Fact]
    public void Search_ShouldAvoidBlockedTiles()
    {
        var grid = new SquareGrid(5, 5);

        var blocked = new Position(1, 0);

        grid.SetWalkable(
            blocked,
            false
        );

        var result = DijkstraSearch.Search(
            grid,
            new Position(0, 0),
            5
        );

        Assert.False(
            result.Contains(blocked)
        );
    }

    [Fact]
    public void Search_ShouldUseMovementCost()
    {
        var grid = new SquareGrid(5, 1);

        grid.SetMovementCost(
            new Position(2, 0),
            5
        );

        var result = DijkstraSearch.Search(
            grid,
            new Position(0, 0),
            4
        );

        Assert.False(
            result.Contains(
                new Position(2, 0)
            )
        );

        Assert.True(
            result.Contains(
                new Position(1, 0)
            )
        );
    }

    [Fact]
    public void Search_ShouldReturnOnlyReachableTiles()
    {
        var grid = new SquareGrid(5, 5);

        var result = DijkstraSearch.Search(
            grid,
            new Position(0, 0),
            2
        );

        Assert.True(
            result.Contains(new Position(0, 0))
        );

        Assert.True(
            result.Contains(new Position(1, 0))
        );

        Assert.True(
            result.Contains(new Position(2, 0))
        );

        Assert.False(
            result.Contains(new Position(4, 4))
        );
    }

    [Fact]
    public void Search_ShouldReturnEmpty_WhenStartBlocked()
    {
        var grid = new SquareGrid(5, 5);

        var start = new Position(2, 2);

        grid.SetWalkable(
            start,
            false
        );

        var result = DijkstraSearch.Search(
            grid,
            start,
            5
        );

        Assert.False(
            result.Contains(start)
        );
    }
}