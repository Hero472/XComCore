using XComCore.World.Geometry;

namespace XComCore.Movement
{
    public interface IGridCoordinateConverter
    {
        Position ToGrid(Position3D worldPosition);

        Position3D ToWorld(Position gridPosition);
    }
}