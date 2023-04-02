namespace Nerd_STF.Helpers;

internal static class MathfHelper
{
    public static bool IsPrimeClassic(int num)
    {
        for (int i = 2; i <= num / 2; i++) if (num % i == 0) return false;
        return true;
    }
}
