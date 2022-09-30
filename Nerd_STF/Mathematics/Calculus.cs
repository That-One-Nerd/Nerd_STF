namespace Nerd_STF.Mathematics;

public static class Calculus
{
    public const float DefaultStep = 0.001f;

    public static Equation GetDerivative(Equation equ, float step = DefaultStep) =>
        x => GetDerivativeAtPoint(equ, x, step);
    public static float GetDerivativeAtPoint(Equation equ, float x, float step = DefaultStep) =>
        (equ(x + DefaultStep) - equ(x)) / step;

    public static float GetIntegral(Equation equ, float lowerBound, float upperBound, float step = DefaultStep)
    {
        float val = 0;
        for (float x = lowerBound; x <= upperBound; x += step) val += equ(x) * step;
        return val;
    }

    public static Equation GetDynamicIntegral(Equation equ, Equation lowerBound, Equation upperBound,
        float step = DefaultStep) => x => GetIntegral(equ, lowerBound(x), upperBound(x), step);

    // Unfortunately, I cannot test this function, as I have literally no idea how it works and
    // I can't find any tools online (and couldn't make my own) to compare my results.
    // Something to know, though I didn't feel like it deserved its own [Obsolete] attribute.
    public static float GradientDescent(Equation equ, float initial, float rate, float stepCount = 1000,
        float step = DefaultStep)
    {
        float val = initial;
        for (int i = 0; i < stepCount; i++) val -= GetDerivativeAtPoint(equ, val, step) * rate;
        return val;
    }
}
