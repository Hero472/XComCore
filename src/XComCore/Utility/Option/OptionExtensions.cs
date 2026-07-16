namespace XComCore
{
    internal static class OptionExtensions
    {
        public static Option<T> ToOption<T>(this T? value) where T : class =>
            value is null ? Option<T>.None : Option<T>.Some(value);

        public static Option<T> ToOption<T>(this T? value) where T : struct =>
            value.HasValue ? Option<T>.Some(value.Value) : Option<T>.None;
    }   
}