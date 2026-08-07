using XComCore.World.Entities.IWorldEntity;
using XComCore.World.Geometry;

namespace XComCore.World.Grid
{
    public sealed class GridTile
    {
        public Position Position { get; }
        public bool Walkable { get; private set; }
        public int MovementCost { get; private set; } = 1;
        public Option<IGridEntity> Entity { get; private set; }
        public bool HasEntity => Entity.IsSome;
        public GridTile(Position position, bool walkable = true)
        {
            Position = position;
            Walkable = walkable;
        }

        public void PlaceEntity(IGridEntity entity)
        {
            Entity = Option.Some(entity);
            Walkable = false;
        }

        public void RemoveEntity()
        {
            Entity = Option.None<IGridEntity>();
            Walkable = true;
        }

        public void SetWalkable(bool walkable)
        {
            Walkable = walkable;
        }

        public void SetMovementCost(int cost)
        {
            MovementCost = cost;
        }

    }
}