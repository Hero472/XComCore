using System;

namespace XComCore.Entities
{
    public abstract class Entity : IEntity
    {
        public Guid Id { get; } = Guid.NewGuid();
    }
}