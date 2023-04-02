namespace Nerd_STF.Helpers;

internal static class RationalHelper
{
    public static Rational SimplifyAuto(float value)
    {
        string valueStr = value.ToString();
        int pointIndex = valueStr.IndexOf(".");
        if (pointIndex < 0) return new((int)value, 1);

        int raise = valueStr.Substring(pointIndex + 1).Length;
        int den = Mathf.Power(10, raise);

        return new((int)(value * den), den);
    }
    public static Rational SimplifyFarey(float value, float tolerance, int maxIters)
    {
        float remainder = value % 1;
        if (remainder == 0) return new((int)value, 1);

        int additional = (int)(value - remainder);

        Rational min = Rational.Zero, max = Rational.One;
        Rational result;
        float resultValue;

        int iters = 0;

        do
        {
            result = new(min.Numerator + max.Numerator, min.Denominator + max.Denominator, false);
            resultValue = result.GetValue();

            if (remainder == resultValue) break;
            else if (remainder > resultValue) min = result;
            else if (remainder < resultValue) max = result;

            iters++;
            if (maxIters != -1 && iters > maxIters) break;
        }
        while (Mathf.Absolute(resultValue - value) > tolerance);

        result.Numerator += additional * result.Denominator;
        return result;
    }
}
