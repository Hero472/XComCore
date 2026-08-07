using System;
using System.Collections.Generic;

namespace XComCore.Factions
{
    public sealed class FactionManager
    {
        private readonly Dictionary<Guid, IFaction> _factions = new Dictionary<Guid, IFaction>();
        private readonly Dictionary<(Guid, Guid), FactionRelation> _relations = new Dictionary<(Guid, Guid), FactionRelation>();
        public IEnumerable<IFaction> Factions => _factions.Values;

        public Result<Unit, FactionError> AddFaction(IFaction faction)
        {
            if (!_factions.TryAdd(faction.Id, faction))
                return Result.Err(FactionError.AlreadyRegistered);

            return Result.Ok(Unit.Value);
        }

        public Result<Unit, FactionError> RemoveFaction(IFaction faction)
        {
            if (!_factions.Remove(faction.Id))
                return Result.Err(FactionError.NotRegistered);

            var relationsToRemove = new List<(Guid, Guid)>();

            foreach (var relation in _relations.Keys)
            {
                if (relation.Item1 == faction.Id || relation.Item2 == faction.Id)
                {
                    relationsToRemove.Add(relation);
                }
            }

            foreach (var relation in relationsToRemove)
            {
                _relations.Remove(relation);
            }

            return Result.Ok(Unit.Value);
        }

        public bool Contains(IFaction faction)
        {
            return _factions.ContainsKey(faction.Id);
        }

        public Option<IFaction> Find(Guid id)
        {
            if (_factions.TryGetValue(id, out var faction))
                return Option.Some(faction);

            return Option.None<IFaction>();
        }

        public Result<Unit, FactionError> SetRelation(
            IFaction first,
            IFaction second,
            FactionRelation relation
        )
        {
            if (first == second)
                return Result.Err(FactionError.SameFaction);

            if (!Contains(first) || !Contains(second))
                return Result.Err(FactionError.NotRegistered);

            _relations[(first.Id, second.Id)] = relation;
            _relations[(second.Id, first.Id)] = relation;

            return Result.Ok(Unit.Value);
        }

        public Result<FactionRelation, FactionError> GetRelation(
            IFaction first,
            IFaction second
        )
        {
            if (first == second)
                return Result.Ok(FactionRelation.Self);

            if (!Contains(first) || !Contains(second))
                return Result.Err(FactionError.NotRegistered);

            if (_relations.TryGetValue((first.Id, second.Id), out var relation))
            {
                return Result.Ok(relation);
            }

            return Result.Ok(FactionRelation.Neutral);
        }

        public Result<bool, FactionError> AreAllies(
            IFaction first,
            IFaction second
        )
        {
            return GetRelation(first, second)
                .Map(r => r == FactionRelation.Ally);
        }

        public Result<bool, FactionError> AreEnemies(
            Faction first,
            Faction second
        )
        {
            return GetRelation(first, second).Map(r => r == FactionRelation.Enemy);
        }

    }
}