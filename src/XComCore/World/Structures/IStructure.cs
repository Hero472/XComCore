using System.Collections.Generic;
using XComCore.World.Entities;
using XComCore.World.Geometry;

namespace XComCore.World.Structures
{
    public interface IStructure : IGridEntity
    {
        new Position Origin { get; }
        new IReadOnlyCollection<Offset> FootPrint { get; }

        bool IsDestroyed { get; }

        void Damage(int amount);

        void Repair(int amount);
    }
}