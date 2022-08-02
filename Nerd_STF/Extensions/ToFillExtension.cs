namespace Nerd_STF.Extensions;

public static class ToFillExtension
{
    public static Fill<T> ToFill<T>(this IEnumerable<T> group) => i => group.ElementAt(i);
}
