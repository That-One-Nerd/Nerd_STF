namespace Nerd_STF.Mathematics;

public static class Mathf
{
    public const float RadToDeg = 0.0174532925199f; // Pi / 180
    public const float E = 2.71828182846f;
    public const float GoldenRatio = 1.61803398875f; // (1 + Sqrt(5)) / 2
    public const float HalfPi = 1.57079632679f; // Pi / 2
    public const float Pi = 3.14159265359f;
    public const float DegToRad = 57.2957795131f; // 180 / Pi
    public const float Tau = 6.28318530718f; // 2 * Pi

    public static float Absolute(float val) => val < 0 ? -val : val;
    public static int Absolute(int val) => val < 0 ? -val : val;

    public static float ArcCos(float value) => -ArcSin(value) + HalfPi;

    public static float ArcCot(float value) => ArcCos(value / Sqrt(1 + value * value));

    public static float ArcCsc(float value) => ArcSin(1 / value);

    public static float ArcSec(float value) => ArcCos(1 / value);

    // Maybe one day I'll have a polynomial for this, but the RMSE for an order 10 polynomial is only 0.00876.
    public static float ArcSin(float value) => (float)Math.Asin(value);

    public static float ArcTan(float value) => ArcSin(value / Sqrt(1 + value * value));

    public static float Average(Equation equ, float min, float max, float step = Calculus.DefaultStep)
    {
        List<float> vals = new();
        for (float x = min; x <= max; x += step) vals.Add(equ(x));
        return Average(vals.ToArray());
    }
    public static float Average(params float[] vals) => Sum(vals) / vals.Length;
    public static int Average(params int[] vals) => Sum(vals) / vals.Length;

    public static int Ceiling(float val) => (int)(val + (1 - (val % 1)));

    public static float Clamp(float val, float min, float max)
    {
        if (max < min) throw new ArgumentOutOfRangeException(nameof(max),
            nameof(max) + " must be greater than or equal to " + nameof(min));
        val = val < min ? min : val;
        val = val > max ? max : val;
        return val;
    }
    public static int Clamp(int val, int min, int max)
    {
        if (max < min) throw new ArgumentOutOfRangeException(nameof(max),
            nameof(max) + " must be greater than or equal to " + nameof(min));
        val = val < min ? min : val;
        val = val > max ? max : val;
        return val;
    }

    public static float Cos(float radians) => Sin(radians + HalfPi);

    public static float Cot(float radians) => Cos(radians) / Sin(radians);

    public static float Csc(float radians) => 1 / Sin(radians);

    public static float Divide(float val, params float[] dividends)
    {
        foreach (float d in dividends) val /= d;
        return val;
    }
    public static int Divide(int val, params int[] dividends)
    {
        foreach (int i in dividends) val /= i;
        return val;
    }

    public static int Factorial(int amount)
    {
        if (amount < 0) return 0;
        int val = 1;
        for (int i = 2; i <= amount; i++) val *= i;
        return val;
    }

    public static int Floor(float val) => (int)(val - (val % 1));

    public static Dictionary<float, float> GetValues(Equation equ, float min, float max,
        float step = Calculus.DefaultStep)
    {
        Dictionary<float, float> vals = new();
        for (float x = min; x <= max; x += step) vals.Add(x, equ(x));
        return vals;
    }

    public static float Lerp(float a, float b, float t, bool clamp = true)
    {
        float v = a + t * (b - a);
        if (clamp) v = Clamp(v, a, b);
        return v;
    }
    public static int Lerp(int a, int b, float value, bool clamp = true) => Floor(Lerp(a, b, value, clamp));

    public static Equation MakeEquation(Dictionary<float, float> vals) => (x) =>
    {
        float min = -1, max = -1;
        foreach (KeyValuePair<float, float> val in vals)
        {
            if (val.Key <= x) min = val.Key;
            if (val.Key >= x) max = val.Key;

            if (min != -1 && max != -1) break;
        }
        float per = x % (max - min);
        return Lerp(min, max, per);
    };

    public static float Max(Equation equ, float min, float max, float step = Calculus.DefaultStep)
    {
        float Y = equ(min);
        for (float x = min; x <= max; x += step)
        {
            float val = equ(x);
            Y = val > Y ? val : Y;
        }
        return Y;
    }
    public static float Max(params float[] vals)
    {
        if (vals.Length < 1) return 0;
        float val = vals[0];
        foreach (float d in vals) val = d > val ? d : val;
        return val;
    }
    public static int Max(params int[] vals)
    {
        if (vals.Length < 1) return 0;
        int val = vals[0];
        foreach (int i in vals) val = i > val ? i : val;
        return val;
    }

    public static float Median(params float[] vals)
    {
        float index = Average(0, vals.Length - 1);
        float valA = vals[Floor(index)], valB = vals[Ceiling(index)];
        return Average(valA, valB);
    }
    public static int Median(params int[] vals) => vals[Floor(Average(0, vals.Length - 1))];

    public static float Min(Equation equ, float min, float max, float step = Calculus.DefaultStep)
    {
        float Y = equ(min);
        for (float x = min; x <= max; x += step)
        {
            float val = equ(x);
            Y = val < Y ? val : Y;
        }
        return Y;
    }
    public static float Min(params float[] vals)
    {
        if (vals.Length < 1) return 0;
        float val = vals[0];
        foreach (float d in vals) val = d < val ? d : val;
        return val;
    }
    public static int Min(params int[] vals)
    {
        if (vals.Length < 1) return 0;
        int val = vals[0];
        foreach (int i in vals) val = i < val ? i : val;
        return val;
    }

    public static float Multiply(params float[] vals)
    {
        if (vals.Length < 1) return 0;
        float val = 1;
        foreach (float d in vals) val *= d;
        return val;
    }
    public static int Multiply(params int[] vals)
    {
        if (vals.Length < 1) return 0;
        int val = 1;
        foreach (int i in vals) val *= i;
        return val;
    }

    public static float Power(float num, float pow) => (float)Math.Pow(num, pow);
    public static int Power(int num, int pow)
    {
        if (pow < 0) return 0;
        int val = 1;
        for (int i = 0; i < Absolute(pow); i++) val *= num;
        return val;
    }

    public static float Root(float value, float index) => (float)Math.Exp(index * Math.Log(value));

    public static float Round(float num) => num % 1 >= 0.5 ? Ceiling(num) : Floor(num);
    public static float Round(float num, float nearest) => nearest * Round(num / nearest);
    public static int RoundInt(float num) => (int)Round(num);

    public static float Sec(float radians) => 1 / Cos(radians);

    public static float Sin(float radians)
    {
        // Really close polynomial to sin(x) (when modded by 2pi). RMSE of 0.000003833
        const float a =  0.000013028f,
                     b =  0.999677f,
                     c =  0.00174164f,
                     d = -0.170587f,
                     e =  0.0046494f,
                     f =  0.00508955f,
                     g =  0.00140205f,
                     h = -0.000577413f,
                     i =  0.0000613134f,
                     j = -0.00000216852f;
        float x = radians % Tau;

        return
            a + (b * x) + (c * x * x) + (d * x * x * x) + (e * x * x * x * x) + (f * x * x * x * x * x)
            + (g * x * x * x * x * x * x) + (h * x * x * x * x * x * x * x) + (i * x * x * x * x * x * x * x * x)
            + (j * x * x * x * x * x * x * x * x * x);
    }

    public static float Sqrt(float value) => Root(value, 2);

    public static float Subtract(float num, params float[] vals)
    {
        foreach (float d in vals) num -= d;
        return num;
    }
    public static int Subtract(int num, params int[] vals)
    {
        foreach (int i in vals) num -= i;
        return num;
    }

    public static float Sum(params float[] vals)
    {
        float val = 0;
        foreach (float d in vals) val += d;
        return val;
    }
    public static int Sum(params int[] vals)
    {
        int val = 0;
        foreach (int i in vals) val += i;
        return val;
    }

    public static float Tan(float radians) => Sin(radians) / Cos(radians);
}
