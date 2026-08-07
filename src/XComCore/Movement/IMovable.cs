using XComCore.Entities;

namespace XComCore.Movement
{
    public interface IMovable : IEntity
    {
        Position3D Position { get; }

        float Speed { get; }

        void SetPosition(Position3D position);
    }
}