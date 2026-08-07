namespace XComCore.Movement.Targets
{
    public sealed class PositionTarget : IMovementTarget
    {
        public Position3D Position { get; }

        public PositionTarget(Position3D position)
        {
            Position = position;
        }
    }
}