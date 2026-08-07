using XComCore.Movement.Targets;

namespace XComCore.Movement.Strategies
{
    public interface IMovementStrategy<in T>
        where T : IMovable
    {
        Result<Unit, MovementError> SetTarget(T movable, IMovementTarget target);
        void Update(T movable, float deltaTime);
        void Stop(T movable);
        bool HasReachedTarget();
    }
}