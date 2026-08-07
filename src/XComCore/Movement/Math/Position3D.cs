using System;

namespace XComCore.Movement
{
    public readonly struct Position3D : IEquatable<Position3D>
    {
        public float X { get; }

        public float Y { get; }

        public float Z { get; }

        public Position3D(
            float x,
            float y,
            float z
        )
        {
            X = x;
            Y = y;
            Z = z;
        }

        public float DistanceTo(Position3D other)
        {
            return (other - this).Length;
        }

        public float DistanceSquaredTo(Position3D other)
        {
            Vector3D delta = other - this;

            return
                delta.X * delta.X +
                delta.Y * delta.Y +
                delta.Z * delta.Z;
        }

        public bool Equals(Position3D other)
        {
            return
                X.Equals(other.X) &&
                Y.Equals(other.Y) &&
                Z.Equals(other.Z);
        }

        public override bool Equals(object obj)
        {
            return obj is Position3D other &&
                Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y, Z);
        }

        public static Vector3D operator -(
            Position3D left,
            Position3D right
        )
        {
            return new Vector3D(
                left.X - right.X,
                left.Y - right.Y,
                left.Z - right.Z);
        }

        public static Position3D operator +(
            Position3D position,
            Vector3D vector
        )
        {
            return new Position3D(
                position.X + vector.X,
                position.Y + vector.Y,
                position.Z + vector.Z);
        }

        public static Position3D operator -(
            Position3D position,
            Vector3D vector
        )
        {
            return new Position3D(
                position.X - vector.X,
                position.Y - vector.Y,
                position.Z - vector.Z);
        }

        public static bool operator ==(
            Position3D left,
            Position3D right
        )
        {
            return left.Equals(right);
        }

        public static bool operator !=(
            Position3D left,
            Position3D right
        )
        {
            return !left.Equals(right);
        }

        public void Deconstruct(
            out float x,
            out float y,
            out float z
        )
        {
            x = X;
            y = Y;
            z = Z;
        }

        public override string ToString()
        {
            return $"({X}, {Y}, {Z})";
        }
    }
}