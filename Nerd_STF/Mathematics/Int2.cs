namespace Nerd_STF.Mathematics;

public record struct Int2 : IAbsolute<Int2>, IAverage<Int2>, IClamp<Int2>, IClampMagnitude<Int2, int>,
    IComparable<Int2>, ICross<Int2, Int3>, IDivide<Int2>, IDot<Int2, int>, IEquatable<Int2>,
    IFromTuple<Int2, (int x, int y)>, IGroup<int>, IIndexAll<int>, IIndexRangeAll<int>, ILerp<Int2, float>,
    IMathOperators<Int2>, IMax<Int2>, IMedian<Int2>, IMin<Int2>, IPresets2d<Int2>, IProduct<Int2>,
    ISplittable<Int2, (int[] Xs, int[] Ys)>, ISubtract<Int2>, ISum<Int2>
{
    public static Int2 Down => new(0, -1);
    public static Int2 Left => new(-1, 0);
    public static Int2 Right => new(1, 0);
    public static Int2 Up => new(0, 1);

    public static Int2 One => new(1, 1);
    public static Int2 Zero => new(0, 0);

    public float Magnitude => Mathf.Sqrt(x * x + y * y);
    public Int2 Normalized => (Int2)((Float2)this * Mathf.InverseSqrt(x * x + y * y));

    public int x, y;

    public Int2(int all) : this(all, all) { }
    public Int2(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
    public Int2(Fill<int> fill) : this(fill(0), fill(1)) { }

    public int this[int index]
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

    public static Int2 Absolute(Int2 val) =>
        new(Mathf.Absolute(val.x), Mathf.Absolute(val.y));
    public static Int2 Average(params Int2[] vals) => Sum(vals) / vals.Length;
    public static Int2 Clamp(Int2 val, Int2 min, Int2 max) =>
        new(Mathf.Clamp(val.x, min.x, max.x),
            Mathf.Clamp(val.y, min.y, max.y));
    public static Int2 ClampMagnitude(Int2 val, int minMag, int maxMag)
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
    public static Int3 Cross(Int2 a, Int2 b, bool normalized = false) =>
        Int3.Cross(a, b, normalized);
    public static Int2 Divide(Int2 num, params Int2[] vals) => num / Product(vals);
    public static int Dot(Int2 a, Int2 b) => a.x * b.x + a.y * b.y;
    public static int Dot(params Int2[] vals)
    {
        if (vals.Length < 1) return 0;
        int x = 1, y = 1;
        foreach (Int2 d in vals)
        {
            x *= d.x;
            y *= d.y;
        }
        return x + y;
    }
    public static Int2 Lerp(Int2 a, Int2 b, float t, bool clamp = true) =>
        new(Mathf.Lerp(a.x, b.x, t, clamp), Mathf.Lerp(a.y, b.y, t, clamp));
    public static Int2 Median(params Int2[] vals)
    {
        float index = (vals.Length - 1) * 0.5f;
        Int2 valA = vals[Mathf.Floor(index)], valB = vals[Mathf.Ceiling(index)];
        return (valA + valB) / 2;
    }
    public static Int2 Max(params Int2[] vals)
    {
        if (vals.Length < 1) return Zero;
        Int2 val = vals[0];
        foreach (Int2 d in vals) val = d.Magnitude > val.Magnitude ? d : val;
        return val;
    }
    public static Int2 Min(params Int2[] vals)
    {
        if (vals.Length < 1) return Zero;
        Int2 val = vals[0];
        foreach (Int2 d in vals) val = d.Magnitude < val.Magnitude ? d : val;
        return val;
    }
    public static Int2 Product(params Int2[] vals)
    {
        if (vals.Length < 1) return Zero;
        Int2 val = One;
        foreach (Int2 d in vals) val *= d;
        return val;
    }
    public static Int2 Subtract(Int2 num, params Int2[] vals) => num - Sum(vals);
    public static Int2 Sum(params Int2[] vals)
    {
        Int2 val = Zero;
        foreach (Int2 d in vals) val += d;
        return val;
    }

    public static (int[] Xs, int[] Ys) SplitArray(params Int2[] vals)
    {
        int[] Xs = new int[vals.Length], Ys = new int[vals.Length];
        for (int i = 0; i < vals.Length; i++)
        {
            Xs[i] = vals[i].x;
            Ys[i] = vals[i].y;
        }
        return (Xs, Ys);
    }

    public int CompareTo(Int2 other) => Magnitude.CompareTo(other.Magnitude);
    public bool Equals(Int2 other) => x == other.x && y == other.y;
    public override int GetHashCode() => base.GetHashCode();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    public IEnumerator<int> GetEnumerator()
    {
        yield return x;
        yield return y;
    }

    public int[] ToArray() => new[] { x, y };
    public Fill<int> ToFill()
    {
        Int2 @this = this;
        return i => @this[i];
    }
    public List<int> ToList() => new() { x, y };

    public Vector2d ToVector() => ((Float2)this).ToVector();

    private bool PrintMembers(StringBuilder builder)
    {
        builder.Append("x = ");
        builder.Append(x);
        builder.Append(", y = ");
        builder.Append(y);
        return true;
    }

    public static Int2 operator +(Int2 a, Int2 b) => new(a.x + b.x, a.y + b.y);
    public static Int2 operator -(Int2 i) => new(-i.x, -i.y);
    public static Int2 operator -(Int2 a, Int2 b) => new(a.x - b.x, a.y - b.y);
    public static Int2 operator *(Int2 a, Int2 b) => new(a.x * b.x, a.y * b.y);
    public static Int2 operator *(Int2 a, int b) => new(a.x * b, a.y * b);
    public static Int2 operator *(Int2 a, Matrix b) => (Int2)((Matrix)(Float2)a * b);
    public static Int2 operator /(Int2 a, Int2 b) => new(a.x / b.x, a.y / b.y);
    public static Int2 operator /(Int2 a, int b) => new(a.x / b, a.y / b);
    public static Int2 operator /(Int2 a, Matrix b) => (Int2)((Matrix)(Float2)a / b);
    public static Int2 operator &(Int2 a, Int2 b) => new(a.x & b.x, a.y & b.y);
    public static Int2 operator |(Int2 a, Int2 b) => new(a.x | b.x, a.y | b.y);
    public static Int2 operator ^(Int2 a, Int2 b) => new(a.x ^ b.x, a.y ^ b.y);

    public static explicit operator Int2(Complex val) => new((int)val.u, (int)val.i);
    public static explicit operator Int2(Quaternion val) => new((int)val.u, (int)val.i);
    public static explicit operator Int2(Float2 val) => new((int)val.x, (int)val.y);
    public static explicit operator Int2(Float3 val) => new((int)val.x, (int)val.y);
    public static explicit operator Int2(Float4 val) => new((int)val.x, (int)val.y);
    public static explicit operator Int2(Matrix m) => new((int)m[0, 0], (int)m[1, 0]);
    public static explicit operator Int2(Vector2d val) => (Int2)val.ToXYZ();
    public static explicit operator Int2(Int3 val) => new(val.x, val.y);
    public static explicit operator Int2(Int4 val) => new(val.x, val.y);
    public static implicit operator Int2(Fill<int> fill) => new(fill);
    public static implicit operator Int2((int x, int y) val) => new(val.x, val.y);
}
