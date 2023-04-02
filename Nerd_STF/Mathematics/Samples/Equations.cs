namespace Nerd_STF.Mathematics.Samples;

public static class Equations
{
    public static Equation CosWave => Mathf.Cos;
    public static Equation SinWave => Mathf.Sin;
    public static Equation SawWave => x => x % 1;
    public static Equation SquareWave => x => x % 2 < 1 ? 1 : 0;

    public static Equation FlatLine => x => 0;
    public static Equation XLine => x => x;
}
