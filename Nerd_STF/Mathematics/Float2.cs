namespace Nerd_STF.Mathematics;

public record struct Float2 : IAbsolute<Float2>, IAverage<Float2>, ICeiling<Float2, Int2>,
    IClamp<Float2>, IClampMagnitude<Float2, float>, IComparable<Float2>,
    ICross<Float2, float>, IDivide<Float2>, IDot<Float2, float>, IEquatable<Float2>,
    IFloor<Float2, Int2>, IFromTuple<Float2, (float x, float y)>, IGroup<float>,
    ILerp<Float2, float>, IMathOperators<Float2>, IMax<Float2>, IMedian<Float2>, IMin<Float2>,
    IIndexAll<float>, IIndexRangeAll<float>, IPresets2d<Float2>, IProduct<Float2>, IRound<Float2, Int2>,
    ISplittable<Float2, (float[] Xs, float[] Ys)>, ISubtract<Float2>, ISum<Float2>
{
    public static Float2 Down => new(0, -1);
    public static Float2 Left => new(-1, 0);
    public static Float2 Right => new(1, 0);
    public static Float2 Up => new(0, 1);

    public static Float2 One => new(1, 1);
    public static Float2 Zero => new(0, 0);

    public float InverseMagnitude => Mathf.InverseSqrt(x * x + y * y);
    public float Magnitude => Mathf.Sqrt(x * x + y * y);
    public Float2 Normalized => this * Mathf.InverseSqrt(x * x + y * y);

    public float x, y;

    public Float2(float all) : this(all, all) { }
    public Float2(Fill<float> fill) : this(fill(0), fill(1)) { }
    public Float2(Fill<int> fill) : this(fill(0), fill(1)) { }
    public Float2(float x, float y)
    {
        this.x = x;
        this.y = y;
    }
    public float this[int index]
    {
        get => index switch
        {
            0 => x,
            1 => y,
            _ => throw new IndexOutOfRangeException(nameof(index)),
        };
        set
        {
            switch (index)
            {
                case 0:
                    x = value;
                    break;

                case 1:
                    y = value;
                    break;

                default: throw new IndexOutOfRangeException(nameof(index));
            }
        }
    }
    public float this[Index index]
    {
        get => this[index.IsFromEnd ? 2 - index.Value : index.Value];
        set => this[index.IsFromEnd ? 2 - index.Value : index.Value] = value;
    }
    public float[] this[Range range]
    {
        get
        {
            int start = range.Start.IsFromEnd ? 2 - range.Start.Value : range.Start.Value;
            int end = range.End.IsFromEnd ? 2 - range.End.Value : range.End.Value;
            List<float> res = new();
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

    public static Float2 Absolute(Float2 val) =>
        new(Mathf.Absolute(val.x), Mathf.Absolute(val.y));
    public static Float2 Average(params Float2[] vals) => Sum(vals) / vals.Length;
    public static Int2 Ceiling(Float2 val) =>
        new(Mathf.Ceiling(val.x), Mathf.Ceiling(val.y));
    public static Float2 Clamp(Float2 val, Float2 min, Float2 max) =>
        new(Mathf.Clamp(val.x, min.x, max.x),
            Mathf.Clamp(val.y, min.y, max.y));
    public static Float2 ClampMagnitude(Float2 val, float minMag, float maxMag)
    {
        if (maxMag < minMag) throw new ArgumentOutOfRangeException(nameof(maxMag),
            nameof(maxMag) + " must be greater than or equal to " + nameof(minMag));
        float mag = val.Magnitude;
        if (mag >= minMag && mag <= maxMag) return val;
        val = val.Normalized;
        if (mag < minMag) val *= minMag;
        else if (mag > maxMag) val *= maxMag;
        return val;
    }
    public static float Cross(Float2 a, Float2 b, bool normalized = false) =>
        Float3.Cross(a, b, normalized).z;
    public static Float2 Divide(Float2 num, params Float2[] vals) => num / Product(vals);
    public static float Dot(Float2 a, Float2 b) => a.x * b.x + a.y * b.y;
    public static float Dot(params Float2[] vals)
    {
        if (vals.Length < 1) return 0;
        float x = 1, y = 1;
        foreach (Float2 f in vals)
        {
            x *= f.x;
            y *= f.y;
        }
        return x + y;
    }
    public static Int2 Floor(Float2 val) =>
        new(Mathf.Floor(val.x), Mathf.Floor(val.y));
    public static Float2 Lerp(Float2 a, Float2 b, float t, bool clamp = true) =>
        new(Mathf.Lerp(a.x, b.x, t, clamp), Mathf.Lerp(a.y, b.y, t, clamp));
    public static Float2 Median(params Float2[] vals)
    {
        float index = (vals.Length - 1) * 0.5f;
        Float2 valA = vals[Mathf.Floor(index)], valB = vals[Mathf.Ceiling(index)];
        return (valA + valB) * 0.5f;
    }
    public static Float2 Max(params Float2[] vals)
    {
        if (vals.Length < 1) return Zero;
        Float2 val = vals[0];
        foreach (Float2 f in vals) val = f.Magnitude > val.Magnitude ? f : val;
        return val;
    }
    public static Float2 Min(params Float2[] vals)
    {
        if (vals.Length < 1) return Zero;
        Float2 val = vals[0];
        foreach (Float2 f in vals) val = f.Magnitude < val.Magnitude ? f : val;
        return val;
    }
    public static Float2 Product(params Float2[] vals)
    {
        if (vals.Length < 1) return Zero;
        Float2 val = One;
        foreach (Float2 f in vals) val *= f;
        return val;
    }
    public static Int2 Round(Float2 val) =>
        new(Mathf.RoundInt(val.x), Mathf.RoundInt(val.y));
    public static Float2 Subtract(Float2 num, params Float2[] vals) => num - Sum(vals);
    public static Float2 Sum(params Float2[] vals)
    {
        Float2 val = Zero;
        foreach (Float2 f in vals) val += f;
        return val;
    }

    public static (float[] Xs, float[] Ys) SplitArray(params Float2[] vals)
    {
        float[] Xs = new float[vals.Length], Ys = new float[vals.Length];
        for (int i = 0; i < vals.Length; i++)
        {
            Xs[i] = vals[i].x;
            Ys[i] = vals[i].y;
        }
        return (Xs, Ys);
    }

    public int CompareTo(Float2 other) => Magnitude.CompareTo(other.Magnitude);
    public bool Equals(Float2 other) => x == other.x && y == other.y;
    public override int GetHashCode() => base.GetHashCode();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    public IEnumerator<float> GetEnumerator()
    {
        yield return x;
        yield return y;
    }

    public float[] ToArray() => new[] { x, y };
    public Fill<float> ToFill()
    {
        Float2 @this = this;
        return i => @this[i];
    }
    public List<float> ToList() => new() { x, y };

    public Vector2d ToVector() => new(Mathf.ArcTan(y / x), Magnitude);

    private bool PrintMembers(StringBuilder builder)
    {
        builder.Append("x = ");
        builder.Append(x);
        builder.Append(", y = ");
        builder.Append(y);
        return true;
    }

    public static Float2 operator +(Float2 a, Float2 b) => new(a.x + b.x, a.y + b.y);
    public static Float2 operator -(Float2 d) => new(-d.x, -d.y);
    public static Float2 operator -(Float2 a, Float2 b) => new(a.x - b.x, a.y - b.y);
    public static Float2 operator *(Float2 a, Float2 b) => new(a.x * b.x, a.y * b.y);
    public static Float2 operator *(Float2 a, float b) => new(a.x * b, a.y * b);
    public static Float2 operator *(Float2 a, Matrix b) => (Float2)((Matrix)a * b);
    public static Quaternion operator *(Float2 a, Quaternion b) => (Quaternion)a * b;
    public static Float2 operator /(Float2 a, Float2 b) => new(a.x / b.x, a.y / b.y);
    public static Float2 operator /(Float2 a, float b) => new(a.x / b, a.y / b);
    public static Float2 operator /(Float2 a, Matrix b) => (Float2)((Matrix)a / b);

    public static implicit operator Float2(Complex val) => new(val.u, val.i);
    public static explicit operator Float2(Quaternion val) => new(val.u, val.i);
    public static explicit operator Float2(Float3 val) => new(val.x, val.y);
    public static explicit operator Float2(Float4 val) => new(val.x, val.y);
    public static implicit operator Float2(Int2 val) => new(val.x, val.y);
    public static explicit operator Float2(Int3 val) => new(val.x, val.y);
    public static explicit operator Float2(Int4 val) => new(val.x, val.y);
    public static explicit operator Float2(Matrix m) => new(m[0, 0], m[1, 0]);
    public static explicit operator Float2(Vector2d val) => val.ToXYZ();
    public static implicit operator Float2(Fill<float> fill) => new(fill);
    public static implicit operator Float2(Fill<int> fill) => new(fill);
    public static implicit operator Float2((float x, float y) val) => new(val.x, val.y);
}
