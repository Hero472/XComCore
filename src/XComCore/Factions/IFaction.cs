using System.Collections.Generic;
using XComCore.Entities;

namespace XComCore.Factions
{
    public interface IFaction : IEntity
    {
        string Name { get; }

        IEnumerable<IEntity> Entities { get; }

        bool Contains(IEntity entity);

        Result<Unit, FactionError> AddEntity(IEntity entity);

        Result<Unit, FactionError> RemoveEntity(IEntity entity);

        Result<Unit, FactionError> AddProperty<T>(T property)
            where T : class, IFactionProperty;

        Result<Unit, FactionError> RemoveProperty<T>()
            where T : class, IFactionProperty;

        Option<T> GetProperty<T>()
            where T : class, IFactionProperty;

        bool HasProperty<T>()
            where T : class, IFactionProperty;
    }
}