using System.Collections.Generic;
using XComCore.World.Geometry;

namespace XComCore.World.Grid.Entities
{
    public interface IGridEntity
    {
        Position Origin { get; }
        IReadOnlyCollection<Position> OccupiedTiles { get; }
    }
}