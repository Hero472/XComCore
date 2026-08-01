using XComCore.World.Geometry;
using XComCore.World.Structures;

namespace XComCore.Tests.World.Structures;

public class StructureTests
{
    [Fact]
    public void Damage_ShouldReduceHealth()
    {
        var structure = new TestStructure(
            new Position(2, 2),
            100
        );

        structure.Damage(30);

        Assert.Equal(
            70,
            structure.Health
        );
    }

    [Fact]
    public void Damage_ShouldNotGoBelowZero()
    {
        var structure = new TestStructure(
            new Position(2, 2),
            100
        );

        structure.Damage(200);

        Assert.Equal(
            0,
            structure.Health
        );
    }

    [Fact]
    public void Repair_ShouldIncreaseHealth()
    {
        var structure = new TestStructure(
            new Position(2, 2),
            100
        );

        structure.Damage(50);

        structure.Repair(20);

        Assert.Equal(
            70,
            structure.Health
        );
    }

    [Fact]
    public void Repair_ShouldNotExceedMaxHealth()
    {
        var structure = new TestStructure(
            new Position(2, 2),
            100
        );

        structure.Repair(50);

        Assert.Equal(
            100,
            structure.Health
        );
    }

    [Fact]
    public void Destroyed_ShouldBecomeTrue_WhenHealthZero()
    {
        var structure = new TestStructure(
            new Position(2, 2),
            100
        );

        structure.Damage(100);

        Assert.True(
            structure.IsDestroyed
        );
    }


    private sealed class TestStructure : Structure
    {
        private readonly IReadOnlyCollection<Offset> _footprint;

        public override IReadOnlyCollection<Offset> FootPrint => _footprint;

        public TestStructure(
            Position origin,
            int maxHealth
        ) : base(origin, maxHealth)
        {
            _footprint = [new Offset(0, 0)];
        }
    }
}