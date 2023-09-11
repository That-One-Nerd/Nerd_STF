namespace Nerd_STF.Mathematics;

public readonly record struct Rational : IAbsolute<Rational>, IAverage<Rational>, ICeiling<Rational, int>, IClamp<Rational>,
    IComparable<Rational>, IComparable<float>, IEquatable<Rational>, IEquatable<float>,
    IFloor<Rational, int>, IIndexGet<int>, IIndexRangeGet<int>, ILerp<Rational, float>, IMathOperators<Rational>,
    IMax<Rational>, IMedian<Rational>, IMin<Rational>, IPresets1d<Rational>,
    IRound<Rational, int>, ISplittable<Rational, (int[] nums, int[] dens)>
{
    public static Rational One => new(1, 1);
    public static Rational Zero => new(0, 1);

    public readonly int numerator;
    public readonly int denominator;

    public Rational Reciprocal => new(denominator, numerator);
    public Rational Simplified
    {
        get
        {
            int[] denFactors = Mathf.PrimeFactors(denominator);

            int newNum = numerator,
                newDen = denominator;

            foreach (int factor in denFactors)
            {
                if (newNum % factor != 0) continue;

                newNum /= factor;
                newDen /= factor;
            }

            return new(newNum, newDen, false);
        }
    }

    public Rational() : this(0, 1) { }
    public Rational(int numerator, int denominator, bool simplified = true)
    {
        this.numerator = numerator * Math.Sign(denominator);
        this.denominator = Mathf.Absolute(denominator);
        if (simplified) this = Simplified;
    }
    public Rational(Fill<int> fill, bool simplified = true) : this(fill(0), fill(1), simplified) { }

    public int this[int index]
    {
        get => index switch
        {
            0 => numerator,
            1 => denominator,
            _ => throw new IndexOutOfRangeException(nameof(index))
        };
    }
    public int this[Index index]
    {
        get => this[index.IsFromEnd ? 2 - index.Value : index.Value];
    }
    public int[] this[Range range]
    {
        get
        {
            int start = range.Start.IsFromEnd ? 2 - range.Start.Value : range.Start.Value;
            int end = range.End.IsFromEnd ? 2 - range.End.Value : range.End.Value;
            List<int> res = new();
            for (int i = start; i < end; i++) res.Add(this[i]);
            return res.ToArray();
        }
    }

    public static Rational FromFloat(float value, float tolerance = 1e-5f,
        SimplificationMethod method = SimplificationMethod.FareySequence, int maxIterations = 100) =>
        method switch
        {
            SimplificationMethod.AutoSimplify => RationalHelper.SimplifyAuto(value),
            SimplificationMethod.FareySequence => RationalHelper.SimplifyFarey(value, tolerance, maxIterations),
            _ => throw new ArgumentException("Unknown simplification method.", nameof(method))
        };

    public static Rational Absolute(Rational value) =>
        new(Mathf.Absolute(value.numerator), value.denominator);
    public static Rational Average(params Rational[] vals) => Sum(vals) / (float)vals.Length;
    public static int Ceiling(Rational r)
    {
        int mod = r.numerator % r.denominator;

        if (mod == 0) return r.numerator / r.denominator;
        return r.numerator + (r.denominator - mod);
    }
    public static Rational Clamp(Rational val, Rational min, Rational max)
        => FromFloat(Mathf.Clamp(val.GetValue(), min.GetValue(), max.GetValue()));
    public static int Floor(Rational val) => val.numerator / val.denominator;
    public static Rational Lerp(Rational a, Rational b, float t, bool clamp = true) =>
        FromFloat(Mathf.Lerp(a.GetValue(), b.GetValue(), t, clamp));
    public static int Round(Rational r) => (int)Mathf.Round(r.numerator, r.denominator) / r.denominator;

    public static (int[] nums, int[] dens) SplitArray(params Rational[] vals)
    {
        int[] nums = new int[vals.Length], dens = new int[vals.Length];
        for (int i = 0; i < vals.Length; i++)
        {
            nums[i] = vals[i].numerator;
            dens[i] = vals[i].denominator;
        }
        return (nums, dens);
    }

    public float GetValue() => numerator / (float)denominator;

    public int CompareTo(Rational other) => GetValue().CompareTo(other.GetValue());
    public int CompareTo(float other) => GetValue().CompareTo(other);
    public bool Equals(Rational other)
    {
        Rational thisSim = Simplified,
                 otherSim = other.Simplified;
        return thisSim.numerator == otherSim.numerator &&
               thisSim.denominator == otherSim.denominator;
    }
    public bool Equals(float other) => GetValue() == other;
    public override int GetHashCode() => base.GetHashCode();

    private bool PrintMembers(StringBuilder builder)
    {
        builder.Append(numerator);
        builder.Append(" / ");
        builder.Append(denominator);

        return true;
    }
    
    public static Rational operator +(Rational a, Rational b)
    {
        int sharedDen = a.denominator * b.denominator,
            newNumA = a.numerator * b.denominator,
            newNumB = b.numerator * a.denominator;
        return new Rational(newNumA + newNumB, sharedDen).Simplified;
    }
    public static Rational operator +(Rational a, float b) => a + FromFloat(b);
    public static Rational operator +(float a, Rational b) => FromFloat(a) + b;
    public static Rational operator -(Rational r) => new(-r.numerator, r.denominator);
    public static Rational operator -(Rational a, Rational b)
    {
        int sharedDen = a.denominator * b.denominator,
            newNumA = a.numerator * b.denominator,
            newNumB = b.numerator * a.denominator;
        return new Rational(newNumA - newNumB, sharedDen).Simplified;
    }
    public static Rational operator -(Rational a, float b) => a - FromFloat(b);
    public static Rational operator -(float a, Rational b) => FromFloat(a) - b;
    public static Rational operator *(Rational a, Rational b) =>
        new Rational(a.numerator * b.numerator, a.denominator * b.denominator).Simplified;
    public static Rational operator *(Rational a, float b) => a * FromFloat(b);
    public static Rational operator *(float a, Rational b) => FromFloat(a) * b;
    public static Rational operator /(Rational a, Rational b) => a * b.Reciprocal;
    public static Rational operator /(Rational a, float b) => a * FromFloat(b).Reciprocal;
    public static Rational operator /(float a, Rational b) => FromFloat(a) * b.Reciprocal;

    public static implicit operator float(Rational r) => r.GetValue();
    public static implicit operator Rational(float f) => FromFloat(f);
}
