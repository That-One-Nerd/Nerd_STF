namespace Nerd_STF.Mathematics.Samples;

public static class Fills
{
    public static Fill<int> SignFill => i => i % 2 == 0 ? 1 : -1;
}
