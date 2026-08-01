using XComCore.World.Geometry;

namespace XComCore.Tests.World.Geometry;

public class PositionTests
{
    [Fact]
    public void SameCoordinates_ShouldBeEqual()
    {
        var position1 = new Position(1, 1);
        var position2 = new Position(1, 1);

        Assert.Equal(position1, position2);
    }

    [Fact]
    public void DifferentCoordinates_ShouldNotBeEqual()
    {
        var position1 = new Position(1, 1);
        var position2 = new Position(2, 2);

        Assert.NotEqual(position1, position2);
    }

    [Fact]
    public void Offset_ShouldMovePosition()
    {
        var position = new Position(5, 5);

        var result = position.Offset(new Offset(2, -1));

        Assert.True(result.IsSuccess);
        Assert.Equal(new Position(7, 4), result.Unwrap());
    }

    [Fact]
    public void Offset_ShouldFail_WhenNegative()
    {
        var position = new Position(0, 0);

        var result = position.Offset(Offset.Left);

        Assert.True(result.IsFailure);
        Assert.Equal(
            CoordinateError.NegativeCoordinate,
            result.UnwrapErr()
        );
    }

    [Fact]
    public void ZeroOffset_ShouldReturnSamePosition()
    {
        var position = new Position(10, 20);

        var result = position.Offset(Offset.Zero);

        Assert.True(result.IsSuccess);
        Assert.Equal(position, result.Unwrap());
    }

    [Fact]
    public void Deconstruct_ShouldReturnCoordinates()
    {
        var position = new Position(8, 12);

        var (x, y) = position;

        Assert.Equal((uint)8, x);
        Assert.Equal((uint)12, y);
    }

    [Fact]
    public void ToString_ShouldReturnCoordinates()
    {
        var position = new Position(3, 7);

        Assert.Equal("(3, 7)", position.ToString());
    }

    [Fact]
    public void EqualsOperator_ShouldReturnTrue_ForEqualPositions()
    {
        var left = new Position(4, 9);
        var right = new Position(4, 9);

        Assert.True(left == right);
    }

    [Fact]
    public void EqualsOperator_ShouldReturnFalse_ForDifferentPositions()
    {
        var left = new Position(4, 9);
        var right = new Position(9, 4);

        Assert.False(left == right);
    }

    [Fact]
    public void NotEqualsOperator_ShouldReturnTrue_ForDifferentPositions()
    {
        var left = new Position(4, 9);
        var right = new Position(9, 4);

        Assert.True(left != right);
    }

    [Fact]
    public void GetHashCode_ShouldBeEqual_ForEqualPositions()
    {
        var position1 = new Position(2, 5);
        var position2 = new Position(2, 5);

        Assert.Equal(
            position1.GetHashCode(),
            position2.GetHashCode()
        );
    }
}
