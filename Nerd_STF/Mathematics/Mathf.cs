namespace Nerd_STF.Mathematics;

public static class Mathf
{
    public static float Absolute(float val) => val < 0 ? -val : val;
    public static int Absolute(int val) => val < 0 ? -val : val;

    public static float AbsoluteMod(float val, float mod)
    {
        while (val >= mod) val -= mod;
        while (val < 0) val += mod;
        return val;
    }
    public static int AbsoluteMod(int val, int mod)
    {
        while (val >= mod) val -= mod;
        while (val < 0) val += mod;
        return val;
    }

    public static Angle ArcCos(float value) => ArcSin(-value) + Angle.Quarter;
    public static Angle ArcCot(float value) => ArcCos(value / Sqrt(1 + value * value));
    public static Angle ArcCsc(float value) => ArcSin(1 / value);
    public static Angle ArcSec(float value) => ArcCos(1 / value);
    public static Angle ArcSin(float value)
    {
        if (value > 1 || value < -1) throw new ArgumentOutOfRangeException(nameof(value));
        return (SolveNewton(x => Sin(x) - value, 0), Angle.Type.Radians);
    }    
    public static Angle ArcTan(float value) => ArcSin(value / Sqrt(1 + value * value));
    public static Angle ArcTan2(float a, float b) => ArcTan(a / b);

    // I would've much rather used CORDIC for these inverses,
    // but I can't think of an intuitive way to do it, so I'll
    // hold off for now.
    public static float ArcCosh(float value) => Log(Constants.E, value + Sqrt(value * value - 1));
    public static float ArcCoth(float value) => Log(Constants.E, (1 + value) / (value - 1)) / 2;
    public static float ArcCsch(float value) => Log(Constants.E, (1 + Sqrt(1 + value * value)) / value);
    public static float ArcSech(float value) => Log(Constants.E, (1 + Sqrt(1 - value * value)) / value);
    public static float ArcSinh(float value) => Log(Constants.E, value + Sqrt(value * value + 1));
    public static float ArcTanh(float value) => Log(Constants.E, (1 + value) / (1 - value)) / 2;
    public static float ArcTanh2(float a, float b) => ArcTanh(a / b);

    public static float Average(Equation equ, float min, float max, float step = Calculus.DefaultStep)
    {
        List<float> vals = new();
        for (float x = min; x <= max; x += step) vals.Add(equ(x));
        return Average(vals.ToArray());
    }
    public static float Average(params float[] vals) => Sum(vals) / vals.Length;
    public static int Average(params int[] vals) => Sum(vals) / vals.Length;

    public static float Binomial(int n, int total, float successRate) =>
        Combinations(total, n) * Power(successRate, n) * Power(1 - successRate, total - n);

    public static float Cbrt(float value) => SolveNewton(x => x * x * x - value, 1);

    public static int Ceiling(float val)
    {
        float mod = val % 1;
        return (int)(mod == 0 ? val : (val + (1 - mod)));
    }

    public static float Clamp(float val, float min, float max)
    {
        if (max < min) throw new ArgumentOutOfRangeException(nameof(max),
            nameof(max) + " must be greater than or equal to " + nameof(min) + ".");
        val = val < min ? min : val;
        val = val > max ? max : val;
        return val;
    }
    public static int Clamp(int val, int min, int max) => (int)Clamp((float)val, min, max);

    // nCr (n = total, r = size)
    public static int Combinations(int total, int size) => Factorial(total) /
        (Factorial(size) * Factorial(total - size));

    public static float Cos(Angle angle) => Cos(angle.Radians);
    public static float Cos(float radians) => Sin(radians + Constants.HalfPi);

    public static float Cosh(float value)
    {
        if (value == 0) return 1;
        else if (value < 0) return Cosh(-value);
        else return CordicHelper.CalculateHyperTrig(value, 16).cosh;
    }

    public static float Cot(Angle angle) => Cot(angle.Radians);
    public static float Cot(float radians) => Cos(radians) / Sin(radians);

    public static float Coth(float value) => 1 / Tanh(value);

    public static float Csc(Angle angle) => Csc(angle.Radians);
    public static float Csc(float radians) => 1 / Sin(radians);

    public static float Csch(float value) => 1 / Sinh(value);

    public static float Divide(float val, params float[] dividends) => val / Product(dividends);
    public static int Divide(int val, params int[] dividends) => val / Product(dividends);

    public static float Dot(float[] a, float[] b)
    {
        if (a.Length != b.Length) throw new InvalidSizeException("Both arrays must have the same length");
        float[] vals = new float[a.Length];
        for (int i = 0; i < a.Length; i++) vals[i] = a[i] * b[i];
        return Sum(vals);
    }
    public static float Dot(params float[][] vals)
    {
        float[] res = new float[vals[0].Length];
        for (int i = 0; i < res.Length; i++)
        {
            float m = 1;
            for (int j = 0; j < vals.Length; j++) m *= vals[j][i];
            res[i] = m;
        }
        return Sum(res);
    }

    public static int Factorial(int amount)
    {
        if (amount < 0) return 0;
        int val = 1;
        for (int i = 2; i <= amount; i++) val *= i;
        return val;
    }

    public static int[] Factors(int val)
    {
        List<int> factors = new() { 1 };
        for (int i = 2; i < val; i++) if (val % i == 0) factors.Add(i);
        factors.Add(val);
        return factors.ToArray();
    }

    public static int Floor(float val) => (int)(val - (val % 1));

    public static Dictionary<float, float> GetValues(Equation equ, float min, float max,
        float step = Calculus.DefaultStep)
    {
        Dictionary<float, float> vals = new();
        for (float x = min; x <= max; x += step) vals.Add(x, equ(x));
        return vals;
    }

    public static int GreatestCommonFactor(params int[] vals)
    {
        int loops = Min(vals);
        for (int i = loops; i > 0; i--)
        {
            bool fit = true;
            foreach (int v in vals) fit &= v % i == 0;
            if (fit) return i;
        }
        return -1;
    }

    public static float InverseSqrt(float val) => 1 / Sqrt(val);

    public static bool IsPrime(int num, PrimeCheckMethod method = PrimeCheckMethod.Classic) =>
        method switch
        {
            PrimeCheckMethod.Classic => MathfHelper.IsPrimeClassic(num),
            PrimeCheckMethod.MillerRabin => MathfHelper.IsPrimeMillerRabin(num),
            _ => throw new ArgumentException("Unknown prime check method.", nameof(method))
        };

    public static int LeastCommonMultiple(params int[] vals) => Product(vals) / GreatestCommonFactor(vals);

    public static float Lerp(float a, float b, float t, bool clamp = true)
    {
        float v = a + t * (b - a);
        if (clamp) v = Clamp(v, Min(a, b), Max(a, b));
        return v;
    }
    public static int Lerp(int a, int b, float t, bool clamp = true) => (int)Lerp((float)a, b, t, clamp);
    public static Equation Lerp(float a, float b, Equation t, bool clamp = true) =>
        x => Lerp(a, b, t(x), clamp);
    public static Equation Lerp(Equation a, Equation b, float t, bool clamp = true) =>
        x => Lerp(a(x), b(x), t, clamp);
    public static Equation Lerp(Equation a, Equation b, Equation t, bool clamp = true) =>
        x => Lerp(a(x), b(x), t(x), clamp);

    public static float Log(float @base, float val)
    {
        if (val <= 0) throw new ArgumentOutOfRangeException(nameof(val));
        else if (val < 1) return -Log(@base, 1 / val);
        else return CordicHelper.LogAnyBase(@base, val, 16, 16);
    }

    public static Equation MakeEquation(Dictionary<float, float> vals) => delegate (float x)
    {
        if (vals.Count < 1) throw new UndefinedException();
        if (vals.Count == 1) return vals.Values.First();

        if (vals.TryGetValue(x, out float value)) return value;
        float? min, max;

        if (x < (min = vals.Keys.Min()))
        {
            max = vals.Keys.Where(x => x != min).Min();
            float distX = x - min.Value, distAB = max.Value - min.Value;
            return Lerp(vals[min.Value], vals[max.Value], distX / distAB, false);
        }
        else if (x > (max = vals.Keys.Max()))
        {
            min = vals.Keys.Where(x => x != max).Max();
            float distX = x - min.Value, distAB = max.Value - min.Value;
            return Lerp(vals[min.Value], vals[max.Value], distX / distAB, false);
        }

        float curDistMax = float.MaxValue, curDistMin = float.MaxValue;
        foreach (float keyX in vals.Keys)
        {
            float dist = Absolute(keyX - x);
            if (keyX < x && dist <= curDistMin)
            {
                min = keyX;
                curDistMin = dist;
            }
            if (keyX > x && dist <= curDistMax)
            {
                max = keyX;
                curDistMax = dist;
            }
        }

        if (!min.HasValue || !max.HasValue || min == max) throw new UndefinedException();
        float all = max.Value - min.Value, diff = x - min.Value;
        return Lerp(vals[min.Value], vals[max.Value], diff / all);
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
    public static T Max<T>(params T[] vals) where T : IComparable<T>
    {
        if (vals.Length < 1) return default!;
        T val = vals[0];
        foreach (T t in vals) val = t.CompareTo(val) > 0 ? t : val;
        return val;
    }

    public static float Median(params float[] vals)
    {
        float index = (vals.Length - 1) * 0.5f;
        if (index % 1 == 0) return vals[(int)index];
        float valA = vals[(int)index], valB = vals[(int)index + 1];
        return (valA + valB) * 0.5f;
    }
    public static int Median(params int[] vals) => Median<int>(vals);
    public static T Median<T>(params T[] vals) => vals[(vals.Length - 1) / 2];

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
    public static T Min<T>(params T[] vals) where T : IComparable<T>
    {
        if (vals.Length < 1) return default!;
        T val = vals[0];
        foreach (T t in vals) val = t.CompareTo(val) < 0 ? t : val;
        return val;
    }

    public static (T value, int occurences) Mode<T>(params T[] vals) where T : IEquatable<T>
    {
        if (vals.Length < 1) throw new ArgumentException("List must contain at least 1 element.", nameof(vals));
        (T value, int occurences) = (vals[0], -1);
        for (int i = 0; i < vals.Length; i++)
        {
            int count = vals.Count(x => x.Equals(vals[i]));
            if (count > occurences)
            {
                value = vals[i];
                occurences = count;
            }
        }
        return (value, occurences);
    }

    // nPr (n = total, r = size)
    public static int Permutations(int total, int size) => Factorial(total) / Factorial(total - size);

    public static int[] PrimeFactors(int num)
    {
        List<int> factors = new();
        for (int i = 2; i <= num; i++)
        {
            while (num % i == 0)
            {
                factors.Add(i);
                num /= i;
            }
        }
        return factors.ToArray();
    }

    public static float Product(params float[] vals)
    {
        if (vals.Length < 1) return 0;
        float val = 1;
        foreach (float d in vals) val *= d;
        return val;
    }
    public static int Product(params int[] vals)
    {
        if (vals.Length < 1) return 0;
        int val = 1;
        foreach (int i in vals) val *= i;
        return val;
    }
    public static float Product(Equation equ, float min, float max, float step = 1)
    {
        float total = 1;
        for (float f = min; f <= max; f += step) total *= equ(f);
        return total;
    }

    public static float Power(float num, float pow)
    {
        if (pow == 0) return 1;
        else if (pow < 0) return 1 / Power(num, -pow);
        else return CordicHelper.ExpAnyBase(num, pow, 16, 16);
    }
    public static float Power(float num, int pow)
    {
        if (pow <= 0) return 0;
        if (pow == 1) return num;
        float val = 1;
        float abs = Absolute(pow);
        for (int i = 0; i < abs; i++) val *= num;
        if (pow < 1) val = 1 / val;
        return val;
    }
    public static int Power(int num, int pow)
    {
        if (pow == 1) return num;
        if (pow < 1) return 0;
        int val = 1;
        for (int i = 0; i < pow; i++) val *= num;
        return val;
    }

    public static int PowerMod(int num, int pow, int mod)
    {
        if (pow == 1) return num;
        if (pow < 1) return 0;
        int val = 1;
        for (int i = 0; i < pow; i++) val = val * num % mod;
        return val;
    }
    public static long PowerMod(long num, long pow, long mod)
    {
        if (pow == 1) return num;
        if (pow < 1) return 0;
        long val = 1;
        for (long i = 0; i < pow; i++) val = val * num % mod;
        return val;
    }

    public static float Root(float value, float index) => (float)Math.Exp(Math.Log(value) / index);

    public static float Round(float num) => num % 1 >= 0.5 ? Ceiling(num) : Floor(num);
    public static float Round(float num, float nearest) => nearest * Round(num / nearest);
    public static int RoundInt(float num) => (int)Round(num);

    public static float Sec(Angle angle) => Sec(angle.Radians);
    public static float Sec(float radians) => 1 / Cos(radians);

    public static float Sech(float value) => 1 / Cosh(value);

    public static T[] SharedItems<T>(params T[][] arrays) where T : IEquatable<T>
    {
        if (arrays.Length < 1) return Array.Empty<T>();

        IEnumerable<T> results = arrays[0];
        foreach (T[] array in arrays) results = results.Where(x => array.Any(y => y.Equals(x)));

        return UniqueItems(results.ToArray());
    }

    public static float Sin(Angle angle) => Sin(angle.Radians);
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
        float x = AbsoluteMod(radians, Constants.Tau);

        return
            a + (b * x) + (c * x * x) + (d * x * x * x) + (e * x * x * x * x) + (f * x * x * x * x * x)
            + (g * x * x * x * x * x * x) + (h * x * x * x * x * x * x * x) + (i * x * x * x * x * x * x * x * x)
            + (j * x * x * x * x * x * x * x * x * x);
    }

    public static float Sinh(float value)
    {
        if (value == 0) return 0;
        else if (value < 0) return -Sinh(-value);
        else return CordicHelper.CalculateHyperTrig(value, 16).sinh;
    }

    public static float SolveBisection(Equation equ, float initialA, float initialB, float tolerance = 1e-5f,
        int maxIterations = 1000)
    {
        if (equ(initialA) == 0) return initialA;
        else if (equ(initialB) == 0) return initialB;

        float guessA = initialA, guessB = initialB, guessMid;

        if (Math.Sign(equ(guessA)) == Math.Sign(equ(guessB)))
        {
            // Guess doesn't contain a zero (or isn't continuous). Return NaN.
            return float.NaN;
        }

        int iterations = 0;
        do
        {
            guessMid = (guessA + guessB) / 2;
            float valMid = equ(guessMid);

            if (valMid == 0) return guessMid;

            if (Math.Sign(equ(guessA)) != Math.Sign(valMid)) guessB = guessMid;
            else guessA = guessMid;

            iterations++;
            if (iterations > maxIterations)
            {
                // Result isn't good enough. Return NaN.
                return float.NaN;
            }
        }
        while ((guessB - guessA) > tolerance);

        return guessMid;
    }
    public static float SolveEquation(Equation equ, float initial, float tolerance = 1e-5f,
        float step = Calculus.DefaultStep, int maxIterations = 1000) =>
        SolveNewton(equ, initial, tolerance, step, maxIterations);
    public static float SolveNewton(Equation equ, float initial, float tolerance = 1e-5f,
        float step = Calculus.DefaultStep, int maxIterations = 1000)
    {
        if (equ(initial) == 0) return initial;

        float lastResult = initial, result;
        int iterations = 0;
        do
        {
            result = lastResult - (equ(lastResult) / Calculus.GetDerivativeAtPoint(equ, lastResult, step));
            lastResult = result;

            iterations++;
            if (iterations > maxIterations)
            {
                // Result isn't good enough. Return NaN.
                return float.NaN;
            }
        }
        while (Absolute(equ(result)) > tolerance);

        return result;
    }

    public static float Sqrt(float value) => SolveNewton(x => x * x - value, 1);

    public static float Subtract(float num, params float[] vals) => num - Sum(vals);
    public static int Subtract(int num, params int[] vals) => num - Sum(vals);

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
    public static float Sum(Equation equ, float min, float max, float step = 1)
    {
        float total = 0;
        for (float f = min; f <= max; f += step) total += equ(f);
        return total;
    }

    // Known as stdev
    public static float StandardDeviation(params float[] vals) => Sqrt(Variance(vals));

    public static float Tan(Angle angle) => Tan(angle.Radians);
    public static float Tan(float radians) => Sin(radians) / Cos(radians);

    public static float Tanh(float value)
    {
        float cosh, sinh;
        if (value < 0)
        {
            (cosh, sinh) = CordicHelper.CalculateHyperTrig(-value, 16);
            sinh = -sinh;
        }
        else (cosh, sinh) = CordicHelper.CalculateHyperTrig(value, 16);

        return cosh / sinh;
    }

    public static T[] UniqueItems<T>(params T[] vals) where T : IEquatable<T>
    {
        List<T> unique = new();
        foreach (T item in vals) if (!unique.Any(x => x.Equals(item))) unique.Add(item);
        return unique.ToArray();
    }

    public static float Variance(params float[] vals)
    {
        float mean = Average(vals), sum = 0;
        for (int i = 0; i < vals.Length; i++)
        {
            float val = vals[i] - mean;
            sum += val * val;
        }
        return sum / (vals.Length - 1);
    }

    public static float ZScore(float val, params float[] vals) => ZScore(val, Average(vals), StandardDeviation(vals));
    public static float ZScore(float val, float mean, float stdev) => (val - mean) / stdev;
}
