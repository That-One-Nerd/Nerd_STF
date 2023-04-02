namespace Nerd_STF.Mathematics;

public record struct Rational : IAbsolute<Rational>, IAverage<Rational>, ICeiling<Rational, int>, IClamp<Rational>,
    IComparable<Rational>, IComparable<float>, IDivide<Rational>, IEquatable<Rational>, IEquatable<float>,
    IFloor<Rational, int>, IIndexAll<int>, IIndexRangeAll<int>, ILerp<Rational, float>, IMathOperators<Rational>,
    IMax<Rational>, IMedian<Rational>, IMin<Rational>, IPresets1d<Rational>, IProduct<Rational>,
    IRound<Rational, int>, ISplittable<Rational, (int[] nums, int[] dens)>, ISubtract<Rational>,
    ISum<Rational>
{
    public static Rational One => new(1, 1);
    public static Rational Zero => new(0, 1);

    public int Numerator
    {
        get => p_num;
        set => p_num = value;
    }
    public int Denominator
    {
        get => p_den;
        set
        {
            if (Math.Sign(value) == -1)
            {
                p_num *= -1;
                p_den = -value;
            }
            else p_den = value;
        }
    }

    private int p_num;
    private int p_den;

    public Rational Reciprocal => new(p_den, p_num);
    public Rational Simplified
    {
        get
        {
            int[] denFactors = Mathf.PrimeFactors(p_den);

            int newNum = p_num,
                newDen = p_den;

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
        Numerator = numerator;
        Denominator = denominator;
        if (simplified) this = Simplified;
    }
    public Rational(Fill<int> fill, bool simplified = true) : this(fill(0), fill(1), simplified) { }

    public int this[int index]
    {
        get => index switch
        {
            0 => p_num,
            1 => p_den,
            _ => throw new IndexOutOfRangeException(nameof(index))
        };
        set
        {
            switch (index)
            {
                case 0:
                    p_num = value;
                    break;

                case 1:
                    p_den = value;
                    break;

                default: throw new IndexOutOfRangeException(nameof(index));
            }
        }
    }
    public int this[Index index]
    {
        get => this[index.IsFromEnd ? 2 - index.Value : index.Value];
        set => this[index.IsFromEnd ? 2 - index.Value : index.Value] = value;
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
        set
        {
            int start = range.Start.IsFromEnd ? 2 - range.Start.Value : range.Start.Value;
            int end = range.End.IsFromEnd ? 2 - range.End.Value : range.End.Value;
            for (int i = start; i < end; i++) this[i] = value[i];
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
        new(Mathf.Absolute(value.p_num), value.p_den);
    public static Rational Average(params Rational[] vals) => Sum(vals) / (float)vals.Length;
    public static int Ceiling(Rational r)
    {
        int mod = r.p_num % r.p_den;

        if (mod == 0) return r.p_num / r.p_den;
        return r.p_num + (r.p_den - mod);
    }
    public static Rational Clamp(Rational val, Rational min, Rational max)
        => FromFloat(Mathf.Clamp(val.GetValue(), min.GetValue(), max.GetValue()));
    public static Rational Divide(Rational val, params Rational[] vals) =>
        val / Product(vals);
    public static int Floor(Rational val) => val.p_num / val.p_den;
    public static Rational Lerp(Rational a, Rational b, float t, bool clamp = true) =>
        FromFloat(Mathf.Lerp(a.GetValue(), b.GetValue(), t, clamp));
    public static Rational Product(params Rational[] vals)
    {
        Rational res = One;
        foreach (Rational r in vals) res *= r;
        return res;
    }
    public static int Round(Rational r) => (int)Mathf.Round(r.p_num, r.p_den) / r.p_den;
    public static Rational Subtract(Rational val, params Rational[] vals) =>
        val - Sum(vals);
    public static Rational Sum(params Rational[] vals)
    {
        Rational sum = Zero;
        foreach (Rational r in vals) sum += r;
        return sum;
    }

    public static (int[] nums, int[] dens) SplitArray(params Rational[] vals)
    {
        int[] nums = new int[vals.Length], dens = new int[vals.Length];
        for (int i = 0; i < vals.Length; i++)
        {
            nums[i] = vals[i].p_num;
            dens[i] = vals[i].p_den;
        }
        return (nums, dens);
    }

    public float GetValue() => p_num / (float)p_den;

    public int CompareTo(Rational other) => GetValue().CompareTo(other.GetValue());
    public int CompareTo(float other) => GetValue().CompareTo(other);
    public bool Equals(Rational other)
    {
        Rational thisSim = Simplified,
                 otherSim = other.Simplified;
        return thisSim.p_num == otherSim.p_num &&
               thisSim.p_den == otherSim.p_den;
    }
    public bool Equals(float other) => GetValue() == other;
    public override int GetHashCode() => base.GetHashCode();

    private bool PrintMembers(StringBuilder builder)
    {
        builder.Append(p_num);
        builder.Append(" / ");
        builder.Append(p_den);

        return true;
    }
    
    public static Rational operator +(Rational a, Rational b)
    {
        int sharedDen = a.p_den * b.p_den,
            newNumA = a.p_num * b.p_den,
            newNumB = b.p_num * a.p_den;
        return new Rational(newNumA + newNumB, sharedDen).Simplified;
    }
    public static Rational operator +(Rational a, float b) => a + FromFloat(b);
    public static Rational operator +(float a, Rational b) => FromFloat(a) + b;
    public static Rational operator -(Rational r) => new(-r.p_num, r.p_den);
    public static Rational operator -(Rational a, Rational b)
    {
        int sharedDen = a.p_den * b.p_den,
            newNumA = a.p_num * b.p_den,
            newNumB = b.p_num * a.p_den;
        return new Rational(newNumA - newNumB, sharedDen).Simplified;
    }
    public static Rational operator -(Rational a, float b) => a - FromFloat(b);
    public static Rational operator -(float a, Rational b) => FromFloat(a) - b;
    public static Rational operator *(Rational a, Rational b) =>
        new Rational(a.p_num * b.p_num, a.p_den * b.p_den);
    public static Rational operator *(Rational a, float b) => a * FromFloat(b);
    public static Rational operator *(float a, Rational b) => FromFloat(a) * b;
    public static Rational operator /(Rational a, Rational b) => a * b.Reciprocal;
    public static Rational operator /(Rational a, float b) => a * FromFloat(b).Reciprocal;
    public static Rational operator /(float a, Rational b) => FromFloat(a) * b.Reciprocal;

    public static implicit operator float(Rational r) => r.GetValue();
    public static implicit operator Rational(float f) => FromFloat(f);
}
