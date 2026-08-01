using System;
using XComCore.World.Geometry;
using XComCore.World.Entities;

namespace XComCore.World.Structures
{
    public abstract class Structure : GridEntity, IStructure
    {
        public Rotation Rotation { get; private set; }

        public int MaxHealth { get; }

        public int Health { get; private set; }

        public bool IsDestroyed => Health <= 0;


        protected Structure(
            Position origin,
            int maxHealth
        ) : base(origin)
        {
            MaxHealth = maxHealth;
            Health = maxHealth;
        }


        public void Rotate(Rotation rotation)
        {
            Rotation = rotation;
        }


        public void Damage(int amount)
        {
            Health = Math.Max(
                0,
                Health - amount
            );
        }


        public void Repair(int amount)
        {
            Health = Math.Min(
                MaxHealth,
                Health + amount
            );
        }
    }
}