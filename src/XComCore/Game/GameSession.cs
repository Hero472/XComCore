using XComCore.Factions;
using XComCore.World.Grid;

namespace XComCore.Game
{
    public sealed class GameSession
    {
        public IGrid Grid { get; }

        public FactionManager Factions { get; }

        public GameSession(
            IGrid grid,
            FactionManager factions
        )
        {
            Grid = grid;
            Factions = factions;
        }
    }
}