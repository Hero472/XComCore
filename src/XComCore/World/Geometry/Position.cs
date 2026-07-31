using System;

namespace XComCore.World.Geometry
{    
    public readonly struct Position : IEquatable<Position>
    {

        public uint X { get; }
        public uint Y { get; }

        public Position(uint x, uint y)
        {
            X = x;
            Y = y;
        }

        public static Position Zero => new Position(0, 0);

        public void Deconstruct(out uint x, out uint y)
        {
            x = X;
            y = Y;
        }

        public override string ToString() => $"({X}, {Y})";

        public bool Equals(Position other)
            => X == other.X && Y == other.Y;

        public override bool Equals(object obj)
            => obj is Position other && Equals(other);

        public override int GetHashCode()
            => HashCode.Combine(X, Y);

        public static bool operator ==(Position left, Position right)
            => left.Equals(right);

        public static bool operator !=(Position left, Position right)
            => !left.Equals(right);

        public Result<Position, CoordinateError> Offset(Offset offset)
        {
            int x = (int)X + offset.X;
            int y = (int)Y + offset.Y;

            if (x < 0 || y < 0)
                return Result.Err(CoordinateError.NegativeCoordinate);

            return Result.Ok(new Position((uint)x, (uint)y));
        }
    }
    
}