using XComCore.World.Geometry;

namespace XComCore.World.Grid
{
    public sealed class GridTile
    {
        public Position Position { get; }
        public bool Walkable { get; private set; }
        public int MovementCost { get; private set; } = 1;

        public GridTile(Position position, bool walkable = true)
        {
            Position = position;
            Walkable = walkable;
        }

        public void SetWalkable(bool walkable)
        {
            Walkable = walkable;
        }

    }
}