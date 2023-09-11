namespace Nerd_STF.Extensions;

public static class EquationExtension
{
    private static readonly List<Type> ValidNumberTypes = new()
    {
        typeof(byte),
        typeof(sbyte),
        typeof(short),
        typeof(ushort),
        typeof(int),
        typeof(uint),
        typeof(long),
        typeof(ulong),
        typeof(float),
        typeof(double),
        typeof(decimal)
    };

    public static Equation Absolute(this Equation equ) => x => Mathf.Absolute(equ(x));
    public static Equation AbsoluteMod(this Equation equ, float mod) => x => Mathf.AbsoluteMod(equ(x), mod);

    public static Equation Add(this Equation equ, float offset) => x => equ(x) + offset;
    public static Equation Add(this Equation equ, Equation offset) => x => equ(x) + offset(x);

    public static Equation ArcCos(this Equation equ) => x => Mathf.ArcCos(equ(x)).Radians;
    public static Equation ArcCot(this Equation equ) => x => Mathf.ArcCot(equ(x)).Radians;
    public static Equation ArcCsc(this Equation equ) => x => Mathf.ArcCsc(equ(x)).Radians;
    public static Equation ArcSec(this Equation equ) => x => Mathf.ArcSec(equ(x)).Radians;
    public static Equation ArcSin(this Equation equ) => x => Mathf.ArcSin(equ(x)).Radians;
    public static Equation ArcTan(this Equation equ) => x => Mathf.ArcTan(equ(x)).Radians;

    public static Equation ArcCosh(this Equation equ) => x => Mathf.ArcCosh(equ(x));
    public static Equation ArcCoth(this Equation equ) => x => Mathf.ArcCoth(equ(x));
    public static Equation ArcCsch(this Equation equ) => x => Mathf.ArcCsch(equ(x));
    public static Equation ArcSech(this Equation equ) => x => Mathf.ArcSech(equ(x));
    public static Equation ArcSinh(this Equation equ) => x => Mathf.ArcSinh(equ(x));
    public static Equation ArcTanh(this Equation equ) => x => Mathf.ArcTanh(equ(x));

    public static float Average(this Equation equ, float min, float max, float step = Calculus.DefaultStep) =>
        Mathf.Average(equ, min, max, step);
    public static Equation Average(this Equation equ, Equation min, Equation max, float step = Calculus.DefaultStep) =>
        x => Mathf.Average(equ, min(x), max(x), step);

    public static Equation Binomial(this Equation equ, int total, float successRate) =>
        x => Mathf.Binomial((int)equ(x), total, successRate);
    public static Equation Binomial(this Equation equ, Equation total, Equation successRate) =>
        x => Mathf.Binomial((int)equ(x), (int)total(x), successRate(x));

    public static Equation Cbrt(this Equation equ) => x => Mathf.Cbrt(equ(x));

    public static Equation Ceiling(this Equation equ) => x => Mathf.Ceiling(equ(x));

    public static Equation Clamp(this Equation equ, float min, float max) => x => Mathf.Clamp(equ(x), min, max);
    public static Equation Clamp(this Equation equ, Equation min, Equation max) =>
        x => Mathf.Clamp(equ(x), min(x), max(x));

    public static Equation Combinations(this Equation equ, int size) =>
        x => Mathf.Combinations(size, (int)equ(x));
    public static Equation Combinations(this Equation equ, Equation size) =>
        x => Mathf.Combinations((int)size(x), (int)equ(x));

    public static Equation Cos(this Equation equ) => x => Mathf.Cos(equ(x));
    public static Equation Cot(this Equation equ) => x => Mathf.Cot(equ(x));
    public static Equation Csc(this Equation equ) => x => Mathf.Csc(equ(x));

    public static Equation Cosh(this Equation equ) => x => Mathf.Cosh(equ(x));
    public static Equation Coth(this Equation equ) => x => Mathf.Coth(equ(x));
    public static Equation Csch(this Equation equ) => x => Mathf.Csch(equ(x));

    public static Equation Divide(this Equation equ, float factor) => x => equ(x) / factor;
    public static Equation Divide(this Equation equ, Equation factor) => x => equ(x) / factor(x);

    public static Equation Factorial(this Equation equ) => x => Mathf.Factorial((int)equ(x));

    public static Equation Floor(this Equation equ) => x => Mathf.Floor(equ(x));

    public static Equation GetDerivative(this Equation equ, float step = Calculus.DefaultStep) =>
        Calculus.GetDerivative(equ, step);
    public static float GetDerivativeAtPoint(this Equation equ, float x, float step = Calculus.DefaultStep) =>
        Calculus.GetDerivativeAtPoint(equ, x, step);

    public static float GetIntegral(this Equation equ, float lowerBound, float upperBound,
        float step = Calculus.DefaultStep) => Calculus.GetIntegral(equ, lowerBound, upperBound, step);

    public static Equation GetDynamicIntegral(this Equation equ, Equation lowerBound,
        Equation upperBound, float step = Calculus.DefaultStep) => Calculus.GetDynamicIntegral(equ, lowerBound, upperBound, step);

    public static Equation GetTaylorSeries(this Equation equ, float referenceX, int iterations = 4, float step = 0.01f) =>
        Calculus.GetTaylorSeries(equ, referenceX, iterations, step);

    public static Dictionary<float, float> GetValues(this Equation equ, float min, float max,
        float step = Calculus.DefaultStep) => Mathf.GetValues(equ, min, max, step);

    public static float GradientDescent(this Equation equ, float initial, float rate, int iterations = 1000,
        float step = Calculus.DefaultStep) => Calculus.GradientDescent(equ, initial, rate, iterations, step);

    public static Equation InverseSqrt(this Equation equ) => x => Mathf.InverseSqrt(equ(x));

    public static Equation Log(this Equation equ, float @base) => x => Mathf.Log(@base, equ(x));

    public static float Max(this Equation equ, float min, float max, float step = Calculus.DefaultStep) =>
        Mathf.Max(equ, min, max, step);
    public static float Min(this Equation equ, float min, float max, float step = Calculus.DefaultStep) =>
        Mathf.Min(equ, min, max, step);

    public static Equation Multiply(this Equation equ, float factor) => x => equ(x) * factor;
    public static Equation Multiply(this Equation equ, Equation factor) => x => equ(x) * factor(x);

    public static Equation Permutations(this Equation equ, int size) =>
        x => Mathf.Permutations(size, (int)equ(x));
    public static Equation Permutations(this Equation equ, Equation size) =>
        x => Mathf.Permutations((int)size(x), (int)equ(x));

    public static Equation Power(this Equation equ, float pow) => x => Mathf.Power(equ(x), pow);
    public static Equation Power(this Equation equ, Equation pow) => x => Mathf.Power(equ(x), pow(x));

    public static float Product(this Equation equ, float lower, float upper, float step = 1) =>
        Mathf.Product(equ, lower, upper, step);

    public static Equation Root(this Equation equ, float index) => x => Mathf.Root(equ(x), index);
    public static Equation Root(this Equation equ, Equation index) => x => Mathf.Root(equ(x), index(x));

    public static Equation Round(this Equation equ) => x => Mathf.Round(equ(x));

    public static Equation Sec(this Equation equ) => x => Mathf.Sec(equ(x));
    public static Equation Sin(this Equation equ) => x => Mathf.Sin(equ(x));

    public static Equation Sech(this Equation equ) => x => Mathf.Sech(equ(x));
    public static Equation Sinh(this Equation equ) => x => Mathf.Sinh(equ(x));

    public static float SolveBisection(this Equation equ, float initialA, float initialB, float tolerance = 1e-5f,
        int maxIterations = 1000) =>
        Mathf.SolveBisection(equ, initialA, initialB, tolerance, maxIterations);
    public static float SolveEquation(this Equation equ, float initial, float tolerance = 1e-5f,
        float step = Calculus.DefaultStep, int maxIterations = 1000) =>
        Mathf.SolveEquation(equ, initial, tolerance, step, maxIterations);
    public static float SolveNewton(this Equation equ, float initial, float tolerance = 1e-5f,
        float step = Calculus.DefaultStep, int maxIterations = 1000) =>
        Mathf.SolveNewton(equ, initial, tolerance, step, maxIterations);

    public static Equation Sqrt(this Equation equ) => x => Mathf.Sqrt(equ(x));

    public static Equation Subtract(this Equation equ, float offset) => x => equ(x) - offset;
    public static Equation Subtract(this Equation equ, Equation offset) => x => equ(x) - offset(x);
    
    public static float Sum(this Equation equ, float lower, float upper, float step = 1) =>
        Mathf.Sum(equ, lower, upper, step);

    public static Equation Tan(this Equation equ) => x => Mathf.Tan(equ(x));

    public static Equation Tanh(this Equation equ) => x => Mathf.Tanh(equ(x));

    public static Equation ZScore(this Equation equ, params float[] vals) => x => Mathf.ZScore(equ(x), vals);
    public static Equation ZScore(this Equation equ, params Equation[] vals) => delegate (float x)
    {
        float[] valsAtValue = new float[vals.Length];
        for (int i = 0; i < vals.Length; i++) valsAtValue[i] = vals[i](x);
        return Mathf.ZScore(equ(x), valsAtValue);
    };

    public static Equation ZScore(this Equation equ, float mean, float stdev) =>
        x => Mathf.ZScore(equ(x), mean, stdev);
    public static Equation ZScore(this Equation equ, Equation mean, Equation stdev) =>
        x => Mathf.ZScore(equ(x), mean(x), stdev(x));

    public static Equation InvokeMethod(this Equation equ, MethodInfo method, params object?[]? args)
    {
        // Determine if this method is a valid method. This exception will be thrown if this method
        // shouldn't be invoked this way. Might be able to be handled a bit better, but it works.
        Exception throwIfBad = new BadMethodException("This method cannot be invoked in the context of an " +
            nameof(Equation), method);

        // Basic method property check.
        if (method.IsAbstract || method.IsConstructor || method.IsGenericMethod || !method.IsPublic)
            throw throwIfBad;

        // Check if a valid number of arguments is provided and the first one takes a number.
        ParameterInfo[] paramTypes = method.GetParameters();
        int requiredParams = 0;
        while (requiredParams < paramTypes.Length && !paramTypes[requiredParams].IsOptional) requiredParams++;

        args ??= Array.Empty<object>();
        if (args.Length + 1 < requiredParams || args.Length > paramTypes.Length) throw throwIfBad;

        if (paramTypes.Length < 1) throw throwIfBad;

        if (!ValidNumberTypes.Contains(paramTypes[0].ParameterType)) throw throwIfBad;

        // Check if the return type is also a number.
        if (!ValidNumberTypes.Contains(method.ReturnType)) throw throwIfBad;

        // This is a good method. Generate the arguments required using the equation and invoke it.
        // The first item in this list will be the float value of the equation.
        List<object?> invokeArgs = new() { 0 };
        invokeArgs.AddRange(args);

        return delegate (float x)
        {
            // Invoke the method (with some casting of course).
            invokeArgs[0] = Convert.ChangeType(equ(x), method.ReturnType);
            object? result = method.Invoke(null, invokeArgs.ToArray());

            if (result is null) throw new UndefinedException($"Invoked method \"{method.Name}\" returned null " +
                "for this input.");

            return (float)Convert.ChangeType(result, typeof(float));
        };
    }
    public static Equation InvokeMathMethod(this Equation equ, string name, params object?[]? args)
    {
        // Check a couple math classes to see if the method is found. If at least one is found,
        // compare the parameters and return type to what is expected. If more than one perfect
        // match exists, the first one will be selected.

        args ??= Array.Empty<object>();
        Type[] toCheck = { typeof(Mathf), typeof(Math) }; // This is the order methods should be searched in.

        foreach (Type t in toCheck)
        {
            // Basic property and return checks.
            List<MethodInfo> possibleMethods = (from m in t.GetMethods()
                                                let basicCheck = !m.IsAbstract && !m.IsConstructor &&
                                                                 !m.IsGenericMethod && m.IsPublic
                                                let nameCheck = m.Name == name
                                                let returnCheck = ValidNumberTypes.Contains(m.ReturnType)
                                                where basicCheck && nameCheck && returnCheck
                                                select m).ToList();

            if (possibleMethods.Count < 1) continue;

            foreach (MethodInfo m in possibleMethods)
            {
                // Check if a valid number of arguments is provided and the first one takes a number.
                ParameterInfo[] paramTypes = m.GetParameters();
                int requiredParams = 0;
                while (requiredParams < paramTypes.Length && !paramTypes[requiredParams].IsOptional) requiredParams++;

                args ??= Array.Empty<object>();
                if (args.Length + 1 < requiredParams || args.Length > paramTypes.Length) continue;

                if (paramTypes.Length < 1) continue;

                if (!ValidNumberTypes.Contains(paramTypes[0].ParameterType)) continue;

                // This is a good method. Generate the arguments required using the equation and invoke it.
                // The first item in this list will be the float value of the equation.
                List<object?> invokeArgs = new() { 0 };
                invokeArgs.AddRange(args);

                return delegate (float x)
                {
                    // Invoke the method (with some casting of course).
                    invokeArgs[0] = Convert.ChangeType(equ(x), m.ReturnType);
                    object? result = m.Invoke(null, invokeArgs.ToArray());

                    if (result is null) throw new UndefinedException($"Invoked method \"{m.Name}\" returned " +
                        "null for this input.");

                    return (float)Convert.ChangeType(result, typeof(float));
                };
            }
        }

        throw new BadMethodException("No method that fits this criteria found in the math types.");
    }

    public static Equation Scale(this Equation equ, float value, ScaleType type = ScaleType.Both) => type switch
    {
        ScaleType.X => x => equ(x / value),
        ScaleType.Y => x => value * equ(x),
        ScaleType.Both => x => value * equ(x / value),
        _ => throw new ArgumentException("Unknown scale type " + type)
    };
}
