using System.Collections.Generic;
using XComCore.Movement.Targets;
using XComCore.World.Algorithms.PathFinding;
using XComCore.World.Geometry;
using XComCore.World.Grid;

namespace XComCore.Movement.Strategies
{
    public sealed class GridMovementStrategy<T> : IMovementStrategy<T>
        where T : IMovable
    {
        private readonly IGrid _grid;
        private readonly IPathFinder _pathfinder;
        private readonly IGridCoordinateConverter _converter;

        private readonly Queue<Position3D> _path =
            new Queue<Position3D>();


        public GridMovementStrategy(
            IGrid grid,
            IPathFinder pathfinder,
            IGridCoordinateConverter converter
        )
        {
            _grid = grid;
            _pathfinder = pathfinder;
            _converter = converter;
        }


        public Result<Unit, MovementError> SetTarget(
            T movable,
            IMovementTarget target
        )
        {
            if (!(target is PositionTarget positionTarget))
                return Result.Err(MovementError.InvalidTarget);


            Position start =
                _converter.ToGrid(movable.Position);


            Position goal =
                _converter.ToGrid(positionTarget.Position);


            var result = _pathfinder.Search(
                _grid,
                start,
                goal,
                int.MaxValue
            );


            if (!result.Found)
                return Result.Err(MovementError.PathNotFound);


            _path.Clear();


            foreach (var tile in result.Path)
            {
                Position3D world =
                    _converter.ToWorld(tile);

                _path.Enqueue(world);
            }


            // remove current position
            if (_path.Count > 0)
                _path.Dequeue();


            return Result.Ok(Unit.Value);
        }


        public void Update(
            T movable,
            float deltaTime
        )
        {
            if (_path.Count == 0)
                return;


            Position3D target =
                _path.Peek();


            float distance =
                movable.Position.DistanceTo(target);


            float step =
                movable.Speed * deltaTime;


            if (distance <= step)
            {
                movable.SetPosition(target);

                _path.Dequeue();
                return;
            }


            Vector3D direction =
                (target - movable.Position)
                .Normalize();


            movable.SetPosition(
                movable.Position +
                direction * step
            );
        }


        public void Stop(T movable)
        {
            _path.Clear();
        }


        public bool HasReachedTarget()
        {
            return _path.Count == 0;
        }
    }
}