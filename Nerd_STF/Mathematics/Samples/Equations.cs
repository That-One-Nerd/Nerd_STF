namespace Nerd_STF.Mathematics.Samples;

public static class Equations
{
    public static readonly Fill<int> SgnFill = i => i % 2 == 0 ? 1 : -1;

    public static readonly Equation CosWave = x => Mathf.Cos(x);
    public static readonly Equation SinWave = x => Mathf.Sin(x);
    public static readonly Equation SawWave = x => x % 1;
    public static readonly Equation SquareWave = x => x % 2 < 1 ? 1 : 0;
}
