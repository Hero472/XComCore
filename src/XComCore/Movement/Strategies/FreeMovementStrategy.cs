using XComCore.Movement.Targets;

namespace XComCore.Movement.Strategies
{
    public sealed class FreeMovementStrategy<T> : IMovementStrategy<T>
        where T : IMovable
    {
        private Option<PositionTarget> _target = Option.None<PositionTarget>();


        public Result<Unit, MovementError> SetTarget(T movable, IMovementTarget target)
        {
            if (!(target is PositionTarget positionTarget))
                return Result.Err(MovementError.InvalidTarget);

            _target = Option.Some(positionTarget);

            return Result.Ok(Unit.Value);
        }


        public void Update(T movable, float deltaTime)
        {
            _target.Match(
                some: target =>
                {
                    Position3D current = movable.Position;
                    Position3D goal = target.Position;


                    float distance = current.DistanceTo(goal);
                    float step = movable.Speed * deltaTime;

                    if (distance <= step)
                    {
                        movable.SetPosition(goal);

                        _target = Option.None<PositionTarget>();

                        return;
                    }

                    Vector3D direction = (goal - current).Normalize();
                    Position3D next = current + direction * step;

                    movable.SetPosition(next);
                },

                none: () => { }
            );
        }


        public void Stop(T movable)
        {
            _target = Option.None<PositionTarget>();
        }


        public bool HasReachedTarget()
        {
            return _target.IsNone;
        }
    }
}