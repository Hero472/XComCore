using XComCore.World.Algorithms.PathFinding;
using XComCore.World.Geometry;
using XComCore.World.Grid;

namespace XComCore.Tests.World.Algorithms.PathFinding;

public class AStarPathFinderTests
{
    private readonly AStarPathfinder _pathfinder = new AStarPathfinder(); // Assuming parameterless constructor

    [Fact]
    public void Search_ShouldFindShortestPath()
    {
        var grid = new SquareGrid(5, 5);

        var result = _pathfinder.Search(
            grid,
            new Position(0, 0),
            new Position(4, 4),
            10
        );

        Assert.True(result.Found);

        Assert.Equal(
            new Position(0, 0),
            result.Path[0]
        );

        Assert.Equal(
            new Position(4, 4),
            result.Path[result.Path.Count - 1]
        );

        Assert.Equal(
            8,
            result.Cost
        );
    }

    [Fact]
    public void Search_ShouldAvoidBlockedTiles()
    {
        var grid = new SquareGrid(5, 5);

        grid.SetWalkable(
            new Position(1, 0),
            false
        );

        var result = _pathfinder.Search(
            grid,
            new Position(0, 0),
            new Position(2, 0),
            10
        );

        Assert.True(result.Found);

        Assert.DoesNotContain(
            new Position(1, 0),
            result.Path
        );
    }

    [Fact]
    public void Search_ShouldFail_WhenGoalBlocked()
    {
        var grid = new SquareGrid(5, 5);

        var goal = new Position(4, 4);

        grid.SetWalkable(
            goal,
            false
        );

        var result = _pathfinder.Search(
            grid,
            new Position(0, 0),
            goal,
            20
        );

        Assert.False(result.Found);

        Assert.Empty(result.Path);
    }

    [Fact]
    public void Search_ShouldFail_WhenGoalOutsideGrid()
    {
        var grid = new SquareGrid(5, 5);

        var result = _pathfinder.Search(
            grid,
            new Position(0, 0),
            new Position(10, 10),
            20
        );

        Assert.False(result.Found);

        Assert.Empty(result.Path);
    }

    [Fact]
    public void Search_ShouldRespectMovementPoints()
    {
        var grid = new SquareGrid(10, 10);

        var result = _pathfinder.Search(
            grid,
            new Position(0, 0),
            new Position(9, 9),
            5
        );

        Assert.False(result.Found);
    }

    [Fact]
    public void Search_ShouldPreferLowerMovementCost()
    {
        var grid = new SquareGrid(5, 3);

        // Direct route:
        // (0,0)->(1,0)->(2,0)->(3,0)->(4,0)
        grid.SetMovementCost(
            new Position(1, 0),
            10
        );

        grid.SetMovementCost(
            new Position(2, 0),
            10
        );

        var result = _pathfinder.Search(
            grid,
            new Position(0, 0),
            new Position(4, 0),
            20
        );

        Assert.True(result.Found);

        Assert.DoesNotContain(
            new Position(1, 0),
            result.Path
        );

        Assert.DoesNotContain(
            new Position(2, 0),
            result.Path
        );
    }

    [Fact]
    public void Search_ShouldReturnSingleNode_WhenStartEqualsGoal()
    {
        var grid = new SquareGrid(5, 5);

        var position = new Position(2, 2);

        var result = _pathfinder.Search(
            grid,
            position,
            position,
            0
        );

        Assert.True(result.Found);

        Assert.Single(result.Path);

        Assert.Equal(
            position,
            result.Path[0]
        );

        Assert.Equal(
            0,
            result.Cost
        );
    }
}