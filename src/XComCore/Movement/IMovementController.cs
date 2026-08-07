using XComCore.Movement.Strategies;
using XComCore.Movement.Targets;

namespace XComCore.Movement
{
    public sealed class MovementController<T>
        where T : IMovable
    {
        private IMovementStrategy<T> _strategy;

        public MovementController(IMovementStrategy<T> strategy)
        {
            _strategy = strategy;
        }

        public Result<Unit, MovementError> SetTarget(T movable, IMovementTarget target)
        {
            return _strategy.SetTarget(movable, target);
        }

        public void Update(T movable, float deltaTime)
        {
            _strategy.Update(movable, deltaTime);
        }

        public void Stop(T movable)
        {
            _strategy.Stop(movable);
        }

        public bool HasReachedTarget()
        {
            return _strategy.HasReachedTarget();
        }


        public void ChangeStrategy(IMovementStrategy<T> strategy)
        {
            _strategy = strategy;
        }
    }
}