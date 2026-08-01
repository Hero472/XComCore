using System.Collections.Generic;
using XComCore.World.Geometry;
using XComCore.World.Grid.Entities;

namespace XComCore.World.Structures
{
    public interface IStructure : IGridEntity
    {
        new Position Origin { get; }
        new IReadOnlyCollection<Position> OccupiedTiles { get; }

        bool IsDestroyed { get; }

        void Damage(int amount);

        void Repair(int amount);
    }
}