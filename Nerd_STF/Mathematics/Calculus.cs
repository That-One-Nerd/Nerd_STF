namespace Nerd_STF.Mathematics;

public static class Calculus
{
    public const float DefaultStep = 0.001f;

    public static Equation GetDerivative(Equation equ, float step = DefaultStep) =>
        x => GetDerivativeAtPoint(equ, x, step);
    public static float GetDerivativeAtPoint(Equation equ, float x, float step = DefaultStep) =>
        (equ(x + step) - equ(x)) / step;

    public static float GetIntegral(Equation equ, float lowerBound, float upperBound, float step = DefaultStep)
    {
        float val = 0;
        for (float x = lowerBound; x <= upperBound; x += step) val += equ(x) * step;
        return val;
    }

    public static Equation GetDynamicIntegral(Equation equ, Equation lowerBound, Equation upperBound,
        float step = DefaultStep) => x => GetIntegral(equ, lowerBound(x), upperBound(x), step);

    public static Equation GetTaylorSeries(Equation equ, float referenceX, int iterations = 4, float step = 0.01f)
    {
        Equation activeDerivative = equ;
        float[] coefficients = new float[iterations];
        int fact = 1;
        for (int i = 0; i < iterations; i++)
        {
            coefficients[i] = activeDerivative(referenceX) / fact;
            activeDerivative = GetDerivative(activeDerivative, step);
            fact *= i + 1;
        }

        return delegate (float x)
        {
            float xVal = 1, result = 0;
            for (int i = 0; i < coefficients.Length; i++)
            {
                result += coefficients[i] * xVal;
                xVal *= x;
            }
            return result;
        };
    }

    // Unfortunately, I cannot test this function, as I have literally no idea how it works and
    // I can't find any tools online (and couldn't make my own) to compare my results.
    // Something to know, though I didn't feel like it deserved its own [Obsolete] attribute.
    public static float GradientDescent(Equation equ, float initial, float rate, int iterations = 1000,
        float step = DefaultStep)
    {
        float val = initial;
        for (int i = 0; i < iterations; i++) val -= GetDerivativeAtPoint(equ, val, step) * rate;
        return val;
    }
}
