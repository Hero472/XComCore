using System;
using System.Collections.Generic;
using XComCore.Entities;

namespace XComCore.Factions
{
    public sealed class Faction : Entity, IFaction
    {
        private readonly HashSet<IEntity> _entities = new HashSet<IEntity>();

        private readonly Dictionary<Type, IFactionProperty> _properties = new Dictionary<Type, IFactionProperty>();

        public new Guid Id { get; }

        public string Name { get; }

        public IEnumerable<IEntity> Entities => _entities;

        public Faction(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
        }

        public Result<Unit, FactionError> AddEntity(IEntity entity)
        {
            if (!_entities.Add(entity))
                return Result.Err(FactionError.EntityAlreadyAdded);

            return Result.Ok(Unit.Value);
        }

        public Result<Unit, FactionError> RemoveEntity(IEntity entity)
        {
            if (!_entities.Remove(entity))
                return Result.Err(FactionError.EntityNotFound);

            return Result.Ok(Unit.Value);
        }

        public bool Contains(IEntity entity)
        {
            return _entities.Contains(entity);
        }

        public Result<Unit, FactionError> AddProperty<T>(T property)
            where T : class, IFactionProperty
        {
            var type = typeof(T);

            if (_properties.ContainsKey(type))
                return Result.Err(FactionError.PropertyAlreadyExists);

            _properties[type] = property;

            return Result.Ok(Unit.Value);
        }

        public Result<Unit, FactionError> RemoveProperty<T>()
            where T : class, IFactionProperty
        {
            if (!_properties.Remove(typeof(T)))
                return Result.Err(FactionError.PropertyNotFound);

            return Result.Ok(Unit.Value);
        }

        public Option<T> GetProperty<T>()
            where T : class, IFactionProperty
        {
            if (_properties.TryGetValue(typeof(T), out var property))
                return Option.Some((T)property);

            return Option.None<T>();
        }

        public bool HasProperty<T>()
            where T : class, IFactionProperty
        {
            return _properties.ContainsKey(typeof(T));
        }
    }
}