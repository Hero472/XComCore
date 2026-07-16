using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace XComCore
{

    public readonly struct NoneValue { }

    public readonly struct Option<T> : IEquatable<Option<T>> where T : notnull
    {
        [MaybeNull] private readonly T _value;
        public bool IsSome { get; }
        public bool IsNone => !IsSome;

        public static Option<T> None => default;

        private Option(T value)
        {
            if (value is null) throw new ArgumentNullException(nameof(value));
            IsSome = true;
            _value = value;
        }

        public T Value => IsSome ? _value! : throw new InvalidOperationException("Option.None has no value.");

        public static Option<T> Some(T value)
        {
            return new Option<T>(value);
        }

        public static Option<T> From([MaybeNull] T value) =>
            value is null ? None : Some(value);

        public static implicit operator Option<T>(NoneValue _) => default;

        public static implicit operator Option<T>([MaybeNull] T value) =>
            value is null ? None : Some(value);

        public T ValueOr(T fallback) => IsSome ? _value! : fallback;
        public T ValueOr(Func<T> fallbackFactory) => IsSome ? _value! : fallbackFactory();

        public Option<T> IfSome(Action<T> action)
        {
            if (IsSome) action(_value!);
            return this;
        }

        public TResult Match<TResult>(Func<T, TResult> some, Func<TResult> none) =>
            IsSome ? some(_value!) : none();

        public void Match(Action<T> some, Action none)
        {
            if (IsSome) some(_value!);
            else none();
        }

        public Option<TResult> Map<TResult>(Func<T, TResult> mapper) where TResult : notnull =>
            IsSome ? Option<TResult>.Some(mapper(_value!)) : Option<TResult>.None;

        public Option<T> Where(Func<T, bool> predicate)
        {
            if (IsNone || predicate(_value!)) return this;
            return None;
        }

        // LINQ support
        public Option<TResult> Select<TResult>(Func<T, TResult> selector) where TResult : notnull =>
            Map(selector);

        public Option<TResult> SelectMany<TIntermediate, TResult>(
            Func<T, Option<TIntermediate>> bind,
            Func<T, TIntermediate, TResult> project)
            where TIntermediate : notnull
            where TResult : notnull
        {
            if (IsNone) return Option<TResult>.None;
            var intermediate = bind(_value!);
            if (intermediate.IsNone) return Option<TResult>.None;
            return Option<TResult>.Some(project(_value!, intermediate._value!));
        }

        public async Task<TResult> MatchAsync<TResult>(
            Func<T, Task<TResult>> some,
            Func<Task<TResult>> none)
        {
            return IsSome
                ? await some(_value!).ConfigureAwait(false)
                : await none().ConfigureAwait(false);
        }

        public async Task<Option<TResult>> MapAsync<TResult>(
            Func<T, Task<TResult>> mapper) where TResult : notnull
        {
            if (IsNone) return Option<TResult>.None;
            var result = await mapper(_value!).ConfigureAwait(false);
            return Option<TResult>.Some(result);
        }

        public Option<TResult> Bind<TResult>(Func<T, Option<TResult>> binder) where TResult : notnull
        {
            if (IsNone) return Option<TResult>.None;
            return binder(_value!);
        }

        public async Task<Option<TResult>> BindAsync<TResult>(
            Func<T, Task<Option<TResult>>> binder) where TResult : notnull
        {
            if (IsNone) return Option<TResult>.None;
            return await binder(_value!).ConfigureAwait(false);
        }

        [return: MaybeNull]
        public T ToNullable() => IsSome ? _value : default;

        public IEnumerable<T> ToEnumerable()
        {
            if (IsSome) yield return _value!;
        }

        // Equality
        public bool Equals(Option<T> other) =>
            IsSome == other.IsSome && (!IsSome || EqualityComparer<T>.Default.Equals(_value, other._value));

        public override bool Equals(object? obj) => obj is Option<T> other && Equals(other);

        public override int GetHashCode() => IsSome ? HashCode.Combine(true, _value) : 0;

        public static bool operator ==(Option<T> left, Option<T> right) => left.Equals(right);
        public static bool operator !=(Option<T> left, Option<T> right) => !left.Equals(right);
    }

    public static class Option
    {
        public static Option<T> Some<T>(T value) where T : notnull => Option<T>.Some(value);
        public static Option<T> None<T>() where T : notnull => Option<T>.None;
    }
}