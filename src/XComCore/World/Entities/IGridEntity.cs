using System.Collections.Generic;
using XComCore.World.Geometry;

namespace XComCore.World.Entities
{
    public interface IGridEntity
    {
        Position Origin { get; }

        IReadOnlyCollection<Offset> FootPrint { get; }

        IReadOnlyCollection<Position> OccupiedTiles { get; }
    }
}