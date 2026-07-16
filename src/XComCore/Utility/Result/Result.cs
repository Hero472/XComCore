using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace XComCore
{
    public readonly struct SuccessMarker<T> : IEquatable<SuccessMarker<T>>
    {
        public T Value { get; }

        public SuccessMarker(T value) => Value = value;

        public bool Equals(SuccessMarker<T> other) =>
            EqualityComparer<T>.Default.Equals(Value, other.Value);

        public override bool Equals(object? obj) =>
            obj is SuccessMarker<T> other && Equals(other);

        public override int GetHashCode() =>
            Value is null ? 0 : EqualityComparer<T>.Default.GetHashCode(Value);

        public static bool operator ==(SuccessMarker<T> left, SuccessMarker<T> right) =>
            left.Equals(right);

        public static bool operator !=(SuccessMarker<T> left, SuccessMarker<T> right) =>
            !left.Equals(right);
    }

    public readonly struct FailureMarker<T> : IEquatable<FailureMarker<T>>
    {
        public T Value { get; }

        public FailureMarker(T error) => Value = error;

        public bool Equals(FailureMarker<T> other) =>
            EqualityComparer<T>.Default.Equals(Value, other.Value);

        public override bool Equals(object? obj) =>
            obj is FailureMarker<T> other && Equals(other);

        public override int GetHashCode() =>
            Value is null ? 0 : EqualityComparer<T>.Default.GetHashCode(Value);

        public static bool operator ==(FailureMarker<T> left, FailureMarker<T> right) =>
            left.Equals(right);

        public static bool operator !=(FailureMarker<T> left, FailureMarker<T> right) =>
            !left.Equals(right);
    }

    public static class Result
    {
        public static Result<T, E> Success<T, E>(T value) where T : notnull where E : notnull
            => Result<T, E>.Success(value);

        public static Result<T, E> Failure<T, E>(E error) where T : notnull where E : notnull
            => Result<T, E>.Failure(error);
    }

    public readonly struct Result<T, E> : IEquatable<Result<T, E>> 
        where T : notnull 
        where E : notnull
    {
        [MaybeNull] private readonly T _value;
        [MaybeNull] private readonly E _error;
        
        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;

        private Result(T value) 
        {
            if (value is null)
                throw new ArgumentNullException(nameof(value));
            IsSuccess = true;
            _value = value;
            _error = default!;
        }

        private Result(E error)
        {
            if (error is null)
                throw new ArgumentNullException(nameof(error));
            IsSuccess = false;
            _value = default!;
            _error = error;
        }

        public static Result<T, E> Success(T value) => new Result<T, E>(value);
        public static Result<T, E> Failure(E error) => new Result<T, E>(error);

        public static implicit operator Result<T, E>(T value) => Success(value);
        public static implicit operator Result<T, E>(E error) => Failure(error);

        public static implicit operator Result<T, E>(SuccessMarker<T> success) 
            => Success(success.Value);

        public static implicit operator Result<T, E>(FailureMarker<E> failure) 
            => Failure(failure.Value);

        public T Unwrap()
        {
            if (IsFailure)
                throw new InvalidOperationException($"Called Unwrap on a failure result: {_error}");
            return _value!;
        }

        public E UnwrapErr()
        {
            if (IsSuccess)
                throw new InvalidOperationException($"Called UnwrapErr on a success result: {_value}");
            return _error!;
        }
        public T UnwrapOr(T defaultValue) => IsSuccess ? _value! : defaultValue;
        public T UnwrapOrElse(Func<E, T> fallback) => IsSuccess ? _value! : fallback(_error!);
        public E UnwrapErrOr(E defaultValue) => IsFailure ? _error! : defaultValue;
        public E UnwrapErrOrElse(Func<T, E> fallback) => IsFailure ? _error! : fallback(_value!);

        public TResult Match<TResult>(Func<T, TResult> success, Func<E, TResult> failure) =>
            IsSuccess ? success(_value!) : failure(_error!);

        public void Match(Action<T> onSuccess, Action<E> onFailure)
        {
            if (IsSuccess) 
            {
                onSuccess(_value!);
            }
            else 
            {
                onFailure(_error!);
            }
        }

        public Result<TNew, E> MatchResult<TNew>(
            Func<T, Result<TNew, E>> success,
            Func<E, FailureMarker<E>> failure
        ) where TNew : notnull
        {
            return IsSuccess 
                ? success(_value!) 
                : failure(_error!);
        }

        public Result<TNew, E> Map<TNew>(Func<T, TNew> mapper) where TNew : notnull =>
            IsSuccess ? Result<TNew, E>.Success(mapper(_value!)) : Result<TNew, E>.Failure(_error!);

        public Result<TNew, E> Bind<TNew>(Func<T, Result<TNew, E>> binder) where TNew : notnull =>
            IsSuccess ? binder(_value!) : Result<TNew, E>.Failure(_error!);

        public Result<TNew, E> Select<TNew>(Func<T, TNew> selector) where TNew : notnull => 
            Map(selector);

        public Result<TResult, E> SelectMany<TIntermediate, TResult>(
            Func<T, Result<TIntermediate, E>> bind, 
            Func<T, TIntermediate, TResult> project) 
            where TIntermediate : notnull 
            where TResult : notnull
        {
            if (IsFailure) return Result<TResult, E>.Failure(_error!);
            var intermediate = bind(_value!);
            if (intermediate.IsFailure) return Result<TResult, E>.Failure(intermediate._error!);
            return Result<TResult, E>.Success(project(_value!, intermediate._value!));
        }

        public async Task MatchAsync(Func<T, Task> success, Func<E, Task> failure)
        {
            if (IsSuccess) 
                await success(_value!).ConfigureAwait(false);
            else 
                await failure(_error!).ConfigureAwait(false);
        }

        public async Task<TResult> MatchAsync<TResult>(
            Func<T, Task<TResult>> success,
            Func<E, Task<TResult>> failure)
        {
            if (IsSuccess)
                return await success(_value!).ConfigureAwait(false);
            else
                return await failure(_error!).ConfigureAwait(false);
        }

        public bool Equals(Result<T, E> other) =>
            IsSuccess == other.IsSuccess && 
            (IsSuccess 
                ? EqualityComparer<T>.Default.Equals(_value, other._value!) 
                : EqualityComparer<E>.Default.Equals(_error, other._error!)
            );

        public override bool Equals(object? obj) => obj is Result<T, E> other && Equals(other);
        public override int GetHashCode() => IsSuccess ? HashCode.Combine(true, _value) : HashCode.Combine(false, _error);

        public Result<T, E> ToResult()
        {
            return this;
        }

        public static bool operator ==(Result<T, E> left, Result<T, E> right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Result<T, E> left, Result<T, E> right)
        {
            return !(left == right);
        }
    }
}