namespace Nerd_STF.Extensions;

public static class EquationExtension
{
    public static Equation Scale(this Equation equ, float value, ScaleType type = ScaleType.Both) => type switch
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
