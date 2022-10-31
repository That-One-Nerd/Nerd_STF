namespace Nerd_STF.Extensions;

public static class ConversionExtension
{
    public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>
        (this IEnumerable<KeyValuePair<TKey, TValue>> pairs)
        where TKey : notnull
    {
        Dictionary<TKey, TValue> res = new();
        foreach (KeyValuePair<TKey, TValue> pair in pairs) res.Add(pair.Key, pair.Value);
        return res;
    }

    public static Fill<T> ToFill<T>(this T[] arr) => i => arr[i];
    public static Fill<T> ToFill<T>(this T[,] arr, Int2 size) => arr.Flatten(size).ToFill();
    public static Fill2D<T> ToFill2D<T>(this T[,] arr) => (x, y) => arr[x, y];
}
