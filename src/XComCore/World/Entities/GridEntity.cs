using System.Collections.Generic;
using System.Linq;
using XComCore.World.Geometry;

namespace XComCore.World.Entities
{
    public abstract class GridEntity : IGridEntity
    {
        public Position Origin { get; private set; }


        public abstract IReadOnlyCollection<Offset> FootPrint { get; }


        public IReadOnlyCollection<Position> OccupiedTiles =>
            FootPrint
                .Select(offset =>
                    Origin.Offset(offset).Unwrap()
                )
                .ToArray();


        protected GridEntity(Position origin)
        {
            Origin = origin;
        }


        public void Move(Position position)
        {
            Origin = position;
        }
    }
}