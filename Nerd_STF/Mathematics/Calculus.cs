namespace Nerd_STF.Mathematics;

public static class Calculus
{
    public const float DefaultStep = 0.001f;

    public static Equation GetDerivative(Equation equ, float min, float max, float step = DefaultStep)
    {
        Dictionary<float, float> vals = new();
        for (float x = min; x <= max; x += step)
        {
            float val1 = equ(x), val2 = equ(x + step), change = (val2 - val1) / step;
            vals.Add(x, change);
        }
        return Mathf.MakeEquation(vals);
    }
    public static float GetDerivativeAtPoint(Equation equ, float x, float step = DefaultStep) =>
        (equ(x + DefaultStep) - equ(x)) / step;

    public static float GetIntegral(Equation equ, float lowerBound, float upperBound, float step = DefaultStep)
    {
        float val = 0;
        for (float x = lowerBound; x <= upperBound; x += step) val += equ(x) * step;
        return val;
    }

    public static float GradientDescent(Equation equ, float initial, float rate, float stepCount = 1000,
        float step = DefaultStep)
    {
        float val = initial;
        for (int i = 0; i < stepCount; i++) val -= GetDerivativeAtPoint(equ, val, step) * rate;
        return val;
    }
}
