using XComCore.World.Geometry;

namespace XComCore.Tests.World.Geometry;

public class GridBoundsTests
{
    [Fact]
    public void Contains_ShouldReturnTrue_WhenInside()
    {
        var bounds = new GridBounds(10, 10);

        Assert.True(
            bounds.Contains(new Position(5, 5))
        );
    }

    [Fact]
    public void Contains_ShouldReturnFalse_WhenOutsideX()
    {
        var bounds = new GridBounds(10, 10);

        Assert.False(
            bounds.Contains(new Position(10, 5))
        );
    }

    [Fact]
    public void Contains_ShouldReturnFalse_WhenOutsideY()
    {
        var bounds = new GridBounds(10, 10);

        Assert.False(
            bounds.Contains(new Position(5, 10))
        );
    }

    [Fact]
    public void Corner_ShouldBeInside()
    {
        var bounds = new GridBounds(10, 10);

        Assert.True(
            bounds.Contains(new Position(9, 9))
        );
    }

    [Fact]
    public void Origin_ShouldBeInside()
    {
        var bounds = new GridBounds(10, 10);

        Assert.True(
            bounds.Contains(new Position(0, 0))
        );
    }

    [Fact]
    public void WidthBoundary_ShouldBeOutside()
    {
        var bounds = new GridBounds(10, 10);

        Assert.False(
            bounds.Contains(new Position(10, 0))
        );
    }

    [Fact]
    public void HeightBoundary_ShouldBeOutside()
    {
        var bounds = new GridBounds(10, 10);

        Assert.False(
            bounds.Contains(new Position(0, 10))
        );
    }

    [Fact]
    public void BottomLeftCorner_ShouldBeInside()
    {
        var bounds = new GridBounds(10, 10);

        Assert.True(
            bounds.Contains(new Position(0, 9))
        );
    }

    [Fact]
    public void TopRightCorner_ShouldBeInside()
    {
        var bounds = new GridBounds(10, 10);

        Assert.True(
            bounds.Contains(new Position(9, 0))
        );
    }
}
