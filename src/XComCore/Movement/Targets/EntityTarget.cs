using XComCore.Entities;

namespace XComCore.Movement.Targets
{
    public sealed class EntityTarget : IMovementTarget
    {
        public IEntity Entity { get; }

        public EntityTarget(IEntity entity)
        {
            Entity = entity;
        }
    }
}