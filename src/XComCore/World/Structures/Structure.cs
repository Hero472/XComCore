using System;
using System.Collections.Generic;
using XComCore.World.Geometry;

namespace XComCore.World.Structures
{
    public abstract class Structure : IStructure
    {
        public Position Origin { get; }

        public abstract IReadOnlyCollection<Position> OccupiedTiles { get; }

        public int MaxHealth { get; }

        public int Health { get; private set; }

        public bool IsDestroyed => Health <= 0;

        protected Structure(Position origin, int maxHealth)
        {
            Origin = origin;
            MaxHealth = maxHealth;
            Health = maxHealth;
        }

        public void Damage(int amount)
        {
            Health = Math.Max(0, Health - amount);
        }

        public void Repair(int amount)
        {
            Health = Math.Min(MaxHealth, Health + amount);
        }
    }
}