using System;
using System.Threading.Tasks;

namespace XComCore
{
    public static class ResultExtensions
    {
        public static async Task<Result<TNext, E>> BindAsync<T, E, TNext>(
            this Result<T, E> result, 
            Func<T, Task<Result<TNext, E>>> binder)
            where T : notnull where E : notnull where TNext : notnull
        {
            if (result.IsFailure) return Result<TNext, E>.Failure(result.Match(_ => default!, e => e));
            return await binder(result.Match(v => v, _ => default!)).ConfigureAwait(false);
        }

        public static bool TryGet<T, E>(this Result<T, E> result, out T value, out E error)
            where T : notnull
            where E : notnull
        {
            value = result.IsSuccess ? result.Match(v => v, _ => default!) : default!;
            error = result.IsFailure ? result.Match(_ => default!, e => e) : default!;
            return result.IsSuccess;
        }

        public static async Task<Result<TNew, E>> MapAsync<T, E, TNew>(
            this Result<T, E> result,
            Func<T, Task<TNew>> mapper)
            where T : notnull
            where E : notnull
            where TNew : notnull
        {
            if (result.IsFailure)
            {
                return Result<TNew, E>.Failure(result.Match(_ => default!, e => e));
            }
            
            var value = result.Match(v => v, _ => default!);
            var newValue = await mapper(value).ConfigureAwait(false);
            return Result<TNew, E>.Success(newValue);
        }


        public static async Task<Result<TNew, E>> MapAsync<T, E, TNew>(
            this Task<Result<T, E>> resultTask,
            Func<T, TNew> mapper)
            where T : notnull
            where E : notnull
            where TNew : notnull
        {
            var result = await resultTask.ConfigureAwait(false);
            return result.Map(mapper);
        }

        public static Result<T, E> ToResult<T, E>(this E error) where T : notnull where E : notnull
            => Result<T, E>.Failure(error);

        public static Result<T, E> ToResult<T, E>(this Option<T> option, E noneError)
            where T : notnull where E : notnull
            => option.Match(
                some: v => Result<T, E>.Success(v),
                none: () => Result<T, E>.Failure(noneError));
    }
}