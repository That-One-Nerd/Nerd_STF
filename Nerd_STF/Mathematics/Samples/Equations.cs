namespace Nerd_STF.Mathematics.Samples;

public static class Equations
{
    public static readonly Fill<int> SgnFill = i => i % 2 == 0 ? 1 : -1;

    public static readonly Equation CosWave = x => Mathf.Cos(x);
    public static readonly Equation SinWave = x => Mathf.Sin(x);
    public static readonly Equation SawWave = x => x % 1;
    public static readonly Equation SquareWave = x => x % 2 < 1 ? 1 : 0;

    public static Equation Scale(Equation equ, float value, ScaleType type = ScaleType.Both) => type switch
    {
        ScaleType.X => x => equ(value / x),
        ScaleType.Y => x => x * equ(value),
        ScaleType.Both => x => x * equ(value / x),
        _ => throw new ArgumentException("Unknown scale type " + type)
    };

    public enum ScaleType
    {
        X = 1,
        Y = 2,
        Both = X | Y
    }
}
