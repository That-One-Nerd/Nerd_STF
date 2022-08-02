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
}
