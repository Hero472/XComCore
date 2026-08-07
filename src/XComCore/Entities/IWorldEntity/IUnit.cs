using XComCore.Entities;
using XComCore.Factions;
using XComCore.Movement;

namespace XComCore.World.Entities.IWorldEntity
{
    public interface IUnit : IEntity, IMovable
    {
        Option<IFaction> Faction { get; }

        float Health { get; }

        float MaxHealth { get; }

        float Radius { get; }
        new float Speed { get; }

    }
}