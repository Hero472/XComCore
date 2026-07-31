namespace XComCore.World.Geometry
{
    public readonly struct GridBounds
    {
        public uint Width { get; }
        public uint Height { get; }

        public GridBounds(uint width, uint height)
        {
            Width = width;
            Height = height;
        }

        public bool Contains(Position position)
            => position.X < Width &&
            position.Y < Height;
    }
}