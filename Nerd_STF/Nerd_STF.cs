namespace Nerd_STF;

public static class Nerd_STF
{
    public static readonly string[] Contributors =
    {
        MainDeveloper
    };
    public static readonly Dictionary<string, string> PersonalLinks = new()
    {
        { "email", "mailto:kyle@thatonenerd.net" },
        { "website", "https://thatonenerd.net/" }
    };
    public static readonly Dictionary<string, string> LibraryLinks = new()
    {
        { "github", "https://github.com/That-One-Nerd/Nerd_STF" },
        { "nuget", "https://www.nuget.org/packages/Nerd_STF/" }
    };
    public const string MainDeveloper = "That_One_Nerd";
    public const string Version = "2.5.0";
}
