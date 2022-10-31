namespace Nerd_STF.Extensions;

public static class EquationExtension
{
    public static Equation Scale(this Equation equ, float value, ScaleType type = ScaleType.Both) => type switch
    {
        ScaleType.X => x => equ(x / value),
        ScaleType.Y => x => value * equ(x),
        ScaleType.Both => x => value * equ(x / value),
        _ => throw new ArgumentException("Unknown scale type " + type)
    };
}
