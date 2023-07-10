namespace Nerd_STF.Extensions;

public static class StringExtension
{
    public static string? GetSection(this string str, string prefix, bool includeFix = true, int startIndex = 0,
        int? endIndex = null)
    {
        endIndex ??= str.Length;

        int start = str.IndexOf(prefix, startIndex);
        if (start == -1 || start > endIndex.Value) return null;

        int end = str.IndexOf(prefix, start + prefix.Length);
        if (end == -1) end = str.Length;
        else if (end > endIndex.Value) end = endIndex.Value;

        if (includeFix)
        {
            start += prefix.Length;
            if (start > end) return null;
        }

        return str[start..end];
    }
    public static string? GetSection(this string str, string prefix, string suffix, bool includeFix = true,
        int startIndex = 0, int? endIndex = null)
    {
        endIndex ??= str.Length;

        int start = str.IndexOf(prefix, startIndex);
        if (start == -1 || start > endIndex.Value) return null;

        int end = str.IndexOf(suffix, start + prefix.Length);
        if (end == -1) return null;
        else if (end > endIndex.Value) end = endIndex.Value;

        if (includeFix) start += prefix.Length;
        else end += suffix.Length;

        if (start > end) return null;

        return str[start..end];
    }

    public static string[] GetSections(this string str, string prefix, bool includeFix = true, int startIndex = 0,
        int? endIndex = null)
    {
        endIndex ??= str.Length;

        List<string> sections = new();
        for (int i = startIndex; i < endIndex && i < str.Length; )
        {
            int start = str.IndexOf(prefix, startIndex);
            if (start == -1 || start > endIndex.Value) break;

            int end = str.IndexOf(prefix, start + prefix.Length);
            if (end == -1) end = str.Length;
            else if (end > endIndex.Value) end = endIndex.Value;

            if (includeFix)
            {
                start += prefix.Length;
                if (start > end) break;
            }

            sections.Add(str[start..end]);
            i = end;
        }

        return sections.ToArray();
    }
    public static string[] GetSections(this string str, string prefix, string suffix, bool includeFix = true, int startIndex = 0,
        int? endIndex = null)
    {
        endIndex ??= str.Length;

        List<string> sections = new();
        for (int i = startIndex; i < endIndex && i < str.Length; )
        {
            endIndex ??= str.Length;

            int start = str.IndexOf(prefix, i);
            if (start == -1 || start > endIndex.Value) break;

            int end = str.IndexOf(suffix, start + prefix.Length);
            if (end == -1) break;
            else if (end > endIndex.Value) end = endIndex.Value;

            if (includeFix) start += prefix.Length;
            else end += suffix.Length;

            if (start > end) break;

            i = end;

            sections.Add(str[start..end]);
        }

        return sections.ToArray();
    }
}
