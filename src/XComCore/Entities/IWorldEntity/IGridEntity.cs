using System.Collections.Generic;
using XComCore.Entities;
using XComCore.World.Geometry;

namespace XComCore.World.Entities.IWorldEntity
{
    public interface IGridEntity : IEntity
    {
        Position Origin { get; }

        IReadOnlyCollection<Offset> FootPrint { get; }

        IReadOnlyCollection<Position> OccupiedTiles { get; }
    }
}