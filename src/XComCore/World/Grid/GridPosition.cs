namespace XComCore.World.Grid {
    
    public readonly record struct GridPosition(uint X, uint Y)
    {
        // Example tactical helper property
        public static GridPosition Zero => new(0, 0);

        // Example tactical math method that works in both .NET and Unity
        public uint DistanceTo(GridPosition other)
        {
            // Manhattan distance calculation (perfect for grid pathfinding)
            return (uint)(Math.Abs((int)X - (int)other.X) + Math.Abs((int)Y - (int)other.Y));
        }
    }
    
}