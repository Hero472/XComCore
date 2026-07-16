namespace XComCore
{
    public static class ResultFunctions
    {
        public static Result<T, E> Ok<T, E>(T value) 
            where T : notnull 
            where E : notnull 
            => Result<T, E>.Success(value);

        public static Result<T, E> Err<T, E>(E error) 
            where T : notnull 
            where E : notnull 
            => Result<T, E>.Failure(error);

        public static FailureMarker<E> Err<E>(E error) 
            where E : notnull 
            => new FailureMarker<E>(error);
    }
}