using XComCore.World.Geometry;

namespace XComCore.Tests.World.Geometry;

public class OffsetTests
{
    [Fact]
    public void Cardinal_ShouldContainFourDirections()
    {
        Assert.Equal(4, Offset.Cardinal.Length);

        Assert.Contains(Offset.Up, Offset.Cardinal);
        Assert.Contains(Offset.Right, Offset.Cardinal);
        Assert.Contains(Offset.Down, Offset.Cardinal);
        Assert.Contains(Offset.Left, Offset.Cardinal);
    }

    [Fact]
    public void All_ShouldContainEightDirections()
    {
        Assert.Equal(8, Offset.All.Length);

        foreach (var offset in Offset.Cardinal)
        {
            Assert.Contains(offset, Offset.All);
        }

        foreach (var offset in Offset.Diagonal)
        {
            Assert.Contains(offset, Offset.All);
        }
    }

    [Fact]
    public void Up_ShouldBeZeroMinusOne()
    {
        Assert.Equal(0, Offset.Up.X);
        Assert.Equal(-1, Offset.Up.Y);
    }

    [Fact]
    public void Down_ShouldBeZeroPlusOne()
    {
        Assert.Equal(0, Offset.Down.X);
        Assert.Equal(1, Offset.Down.Y);
    }

    [Fact]
    public void Left_ShouldBeMinusOneZero()
    {
        Assert.Equal(-1, Offset.Left.X);
        Assert.Equal(0, Offset.Left.Y);
    }

    [Fact]
    public void Right_ShouldBePlusOneZero()
    {
        Assert.Equal(1, Offset.Right.X);
        Assert.Equal(0, Offset.Right.Y);
    }

    [Fact]
    public void Zero_ShouldBeZeroZero()
    {
        Assert.Equal(0, Offset.Zero.X);
        Assert.Equal(0, Offset.Zero.Y);
    }

    [Fact]
    public void Diagonal_ShouldContainFourDirections()
    {
        Assert.Equal(4, Offset.Diagonal.Length);

        Assert.Contains(new Offset(-1, -1), Offset.Diagonal);
        Assert.Contains(new Offset(1, -1), Offset.Diagonal);
        Assert.Contains(new Offset(1, 1), Offset.Diagonal);
        Assert.Contains(new Offset(-1, 1), Offset.Diagonal);
    }

    [Fact]
    public void Cardinal_ShouldNotContainDiagonalDirections()
    {
        foreach (var diagonal in Offset.Diagonal)
        {
            Assert.DoesNotContain(diagonal, Offset.Cardinal);
        }
    }
}
