using System;

namespace XComCore.Movement
{
    public readonly struct Vector3D : IEquatable<Vector3D>
    {
        public float X { get; }

        public float Y { get; }

        public float Z { get; }

        public float Length => MathF.Sqrt(X * X + Y * Y + Z * Z);

        public Vector3D(
            float x,
            float y,
            float z
        )
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Vector3D Normalize()
        {
            if (Length == 0f)
                return this;

            return this / Length;
        }

        public static float Dot(
            Vector3D left,
            Vector3D right
        )
        {
            return left.X * right.X + left.Y * right.Y + left.Z * right.Z;
        }

        public bool Equals(Vector3D other)
        {
            return
                X.Equals(other.X) &&
                Y.Equals(other.Y) &&
                Z.Equals(other.Z);
        }

        public override bool Equals(object obj)
        {
            return obj is Vector3D other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y, Z);
        }

        public static Vector3D operator +(
            Vector3D left,
            Vector3D right
        )
        {
            return new Vector3D(left.X + right.X, left.Y + right.Y, left.Z + right.Z);
        }

        public static Vector3D operator -(
            Vector3D left,
            Vector3D right
        )
        {
            return new Vector3D(left.X - right.X, left.Y - right.Y, left.Z - right.Z);
        }

        public static Vector3D operator *(Vector3D vector, float scalar)
        {
            return new Vector3D(
                vector.X * scalar,
                vector.Y * scalar,
                vector.Z * scalar
            );
        }

        public static Vector3D operator /(
            Vector3D vector,
            float scalar
        )
        {
            return new Vector3D(
                vector.X / scalar,
                vector.Y / scalar,
                vector.Z / scalar
            );
        }

        public static bool operator ==(
            Vector3D left,
            Vector3D right
        )
        {
            return left.Equals(right);
        }

        public static bool operator !=(
            Vector3D left,
            Vector3D right
        )
        {
            return !left.Equals(right);
        }

        public override string ToString()
        {
            return $"({X}, {Y}, {Z})";
        }
    }
}