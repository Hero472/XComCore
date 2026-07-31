namespace XComCore.World.Geometry
{
    public readonly struct Offset
    {
        public int X { get; }

        public int Y { get; }

        public Offset(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static Offset Zero => new Offset(0, 0);
        public static Offset Up => new Offset(0, -1);
        public static Offset Down => new Offset(0, 1);
        public static Offset Left => new Offset(-1, 0);
        public static Offset Right => new Offset(1, 0);

        public static readonly Offset[] Cardinal =
        {
            Up,
            Right,
            Down,
            Left
        };

        public static readonly Offset[] Diagonal =
        {
            new Offset(-1, -1),
            new Offset( 1, -1),
            new Offset( 1,  1),
            new Offset(-1,  1)
        };

        public static readonly Offset[] All =
        {
            Up,
            Right,
            Down,
            Left,

            new Offset(-1, -1),
            new Offset( 1, -1),
            new Offset( 1,  1),
            new Offset(-1,  1)
        };
    }
}