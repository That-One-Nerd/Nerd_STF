namespace Nerd_STF.Mathematics;

public record struct Int4 : IAbsolute<Int4>, IAverage<Int4>, IClamp<Int4>, IClampMagnitude<Int4, int>,
    IComparable<Int4>, IDivide<Int4>, IDot<Int4, int>, IEquatable<Int4>,
    IFromTuple<Int4, (int x, int y, int z, int w)>, IGroup<int>, IIndexAll<int>, IIndexRangeAll<int>,
    ILerp<Int4, float>, IMathOperators<Int4>, IMax<Int4>, IMedian<Int4>, IMin<Int4>, IPresets4d<Int4>,
    IProduct<Int4>, ISplittable<Int4, (int[] Xs, int[] Ys, int[] Zs, int[] Ws)>, ISubtract<Int4>, ISum<Int4>
{
    public static Int4 Back => new(0, 0, -1, 0);
    public static Int4 Down => new(0, -1, 0, 0);
    public static Int4 Forward => new(0, 0, 1, 0);
    public static Int4 HighW => new(0, 0, 0, 1);
    public static Int4 Left => new(-1, 0, 0, 0);
    public static Int4 LowW => new(0, 0, 0, -1);
    public static Int4 Right => new(1, 0, 0, 0);
    public static Int4 Up => new(0, 1, 0, 0);

    public static Int4 One => new(1, 1, 1, 1);
    public static Int4 Zero => new(0, 0, 0, 0);

    public float InverseMagnitude => Mathf.InverseSqrt(x * x + y * y + z * z + w * w);
    public float Magnitude => Mathf.Sqrt(x * x + y * y + z * z + w * w);
    public Int4 Normalized => (Int4)((Float4)this * Mathf.InverseSqrt(x * x + y * y + z * z + w * w));

    public Int2 XW
    {
        get => (x, w);
        set
        {
            x = value.x;
            w = value.y;
        }
    }
    public Int2 XY
    {
        get => (x, y);
        set
        {
            x = value.x;
            y = value.y;
        }
    }
    public Int2 XZ
    {
        get => (x, z);
        set
        {
            x = value.x;
            z = value.y;
        }
    }
    public Int2 YW
    {
        get => (y, w);
        set
        {
            y = value.x;
            w = value.y;
        }
    }
    public Int2 YZ
    {
        get => (y, z);
        set
        {
            y = value.x;
            z = value.y;
        }
    }
    public Int2 ZW
    {
        get => (z, w);
        set
        {
            z = value.x;
            w = value.y;
        }
    }

    public Int3 XYW
    {
        get => (x, y, w);
        set
        {
            x = value.x;
            y = value.y;
            w = value.z;
        }
    }
    public Int3 XYZ
    {
        get => (x, y, z);
        set
        {
            x = value.x;
            y = value.y;
            z = value.z;
        }
    }
    public Int3 XZW
    {
        get => (x, z, w);
        set
        {
            x = value.x;
            z = value.y;
            w = value.z;
        }
    }
    public Int3 YZW
    {
        get => (y, z, w);
        set
        {
            y = value.x;
            z = value.y;
            w = value.z;
        }
    }

    public int x, y, z, w;

    public Int4(int all) : this(all, all, all, all) { }
    public Int4(int x, int y) : this(x, y, 0, 0) { }
    public Int4(int x, int y, int z) : this(x, y, z, 0) { }
    public Int4(int x, int y, int z, int w)
    {
        this.x = x;
        this.y = y;
        this.z = z;
        this.w = w;
    }
    public Int4(Fill<int> fill) : this(fill(0), fill(1), fill(2), fill(3)) { }

    public int this[int index]
    {
        get => index switch
        {
            0 => x,
            1 => y,
            2 => z,
            3 => w,
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

                case 2:
                    z = value;
                    break;

                case 3:
                    w = value;
                    break;

                default: throw new IndexOutOfRangeException(nameof(index));
            }
        }
    }
    public int this[Index index]
    {
        get => this[index.IsFromEnd ? 4 - index.Value : index.Value];
        set => this[index.IsFromEnd ? 4 - index.Value : index.Value] = value;
    }
    public int[] this[Range range]
    {
        get
        {
            int start = range.Start.IsFromEnd ? 4 - range.Start.Value : range.Start.Value;
            int end = range.End.IsFromEnd ? 4 - range.End.Value : range.End.Value;
            List<int> res = new();
            for (int i = start; i < end; i++) res.Add(this[i]);
            return res.ToArray();
        }
        set
        {
            int start = range.Start.IsFromEnd ? 4 - range.Start.Value : range.Start.Value;
            int end = range.End.IsFromEnd ? 4 - range.End.Value : range.End.Value;
            for (int i = start; i < end; i++) this[i] = value[i];
        }
    }

    public static Int4 Absolute(Int4 val) =>
        new(Mathf.Absolute(val.x), Mathf.Absolute(val.y), Mathf.Absolute(val.z), Mathf.Absolute(val.w));
    public static Int4 Average(params Int4[] vals) => Sum(vals) / vals.Length;
    public static Int4 Clamp(Int4 val, Int4 min, Int4 max) =>
        new(Mathf.Clamp(val.x, min.x, max.x),
            Mathf.Clamp(val.y, min.y, max.y),
            Mathf.Clamp(val.z, min.z, max.z),
            Mathf.Clamp(val.w, min.w, max.w));
    public static Int4 ClampMagnitude(Int4 val, int minMag, int maxMag)
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
    public static Int4 Divide(Int4 num, params Int4[] vals) => num / Product(vals);
    public static int Dot(Int4 a, Int4 b) => a.x * b.x + a.y * b.y + a.z * b.z + a.w * b.w;
    public static int Dot(params Int4[] vals)
    {
        if (vals.Length < 1) return 0;
        int x = 1, y = 1, z = 1, w = 1;
        foreach (Int4 d in vals)
        {
            x *= d.x;
            y *= d.y;
            z *= d.z;
            w *= d.w;
        }
        return x + y + z;
    }
    public static Int4 Lerp(Int4 a, Int4 b, float t, bool clamp = true) =>
        new(Mathf.Lerp(a.x, b.x, t, clamp), Mathf.Lerp(a.y, b.y, t, clamp), Mathf.Lerp(a.z, b.z, t, clamp),
            Mathf.Lerp(a.w, b.w, t, clamp));
    public static Int4 Median(params Int4[] vals)
    {
        float index = (vals.Length - 1) * 0.5f;
        Int4 valA = vals[Mathf.Floor(index)], valB = vals[Mathf.Ceiling(index)];
        return (valA + valB) / 2;
    }
    public static Int4 Max(params Int4[] vals)
    {
        if (vals.Length < 1) return Zero;
        Int4 val = vals[0];
        foreach (Int4 d in vals) val = d.Magnitude > val.Magnitude ? d : val;
        return val;
    }
    public static Int4 Min(params Int4[] vals)
    {
        if (vals.Length < 1) return Zero;
        Int4 val = vals[0];
        foreach (Int4 d in vals) val = d.Magnitude < val.Magnitude ? d : val;
        return val;
    }
    public static Int4 Product(params Int4[] vals)
    {
        if (vals.Length < 1) return Zero;
        Int4 val = One;
        foreach (Int4 d in vals) val *= d;
        return val;
    }
    public static Int4 Subtract(Int4 num, params Int4[] vals) => num - Sum(vals);
    public static Int4 Sum(params Int4[] vals)
    {
        Int4 val = Zero;
        foreach (Int4 d in vals) val += d;
        return val;
    }

    public static (int[] Xs, int[] Ys, int[] Zs, int[] Ws) SplitArray(params Int4[] vals)
    {
        int[] Xs = new int[vals.Length], Ys = new int[vals.Length], Zs = new int[vals.Length],
              Ws = new int[vals.Length];
        for (int i = 0; i < vals.Length; i++)
        {
            Xs[i] = vals[i].x;
            Ys[i] = vals[i].y;
            Zs[i] = vals[i].z;
            Ws[i] = vals[i].w;
        }
        return (Xs, Ys, Zs, Ws);
    }

    public int CompareTo(Int4 other) => Magnitude.CompareTo(other.Magnitude);
    public bool Equals(Int4 other) => x == other.x && y == other.y && z == other.z && w == other.w;
    public override int GetHashCode() => base.GetHashCode();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    public IEnumerator<int> GetEnumerator()
    {
        yield return x;
        yield return y;
        yield return z;
        yield return w;
    }

    public int[] ToArray() => new[] { x, y, z, w };
    public Fill<int> ToFill()
    {
        Int4 @this = this;
        return i => @this[i];
    }
    public List<int> ToList() => new() { x, y, z, w };

    private bool PrintMembers(StringBuilder builder)
    {
        builder.Append("x = ");
        builder.Append(x);
        builder.Append(", y = ");
        builder.Append(y);
        builder.Append(", z = ");
        builder.Append(z);
        builder.Append(", w = ");
        builder.Append(w);
        return true;
    }

    public static Int4 operator +(Int4 a, Int4 b) => new(a.x + b.x, a.y + b.y, a.z + b.z, a.w + b.w);
    public static Int4 operator -(Int4 d) => new(-d.x, -d.y, -d.z, -d.w);
    public static Int4 operator -(Int4 a, Int4 b) => new(a.x - b.x, a.y - b.y, a.z - b.z, a.w - b.w);
    public static Int4 operator *(Int4 a, Int4 b) => new(a.x * b.x, a.y * b.y, a.z * b.z, a.w * b.w);
    public static Int4 operator *(Int4 a, int b) => new(a.x * b, a.y * b, a.z * b, a.w * b);
    public static Int4 operator *(Int4 a, Matrix b) => (Int4)((Matrix)(Float4)a * b);
    public static Int4 operator /(Int4 a, Int4 b) => new(a.x / b.x, a.y / b.y, a.z / b.z, a.w / b.w);
    public static Int4 operator /(Int4 a, int b) => new(a.x / b, a.y / b, a.z / b, a.w / b);
    public static Int4 operator /(Int4 a, Matrix b) => (Int4)((Matrix)(Float4)a / b);
    public static Int4 operator &(Int4 a, Int4 b) => new(a.x & b.x, a.y & b.y, a.z & b.z, a.w & b.w);
    public static Int4 operator |(Int4 a, Int4 b) => new(a.x | b.x, a.y | b.y, a.z | b.z, a.w | b.w);
    public static Int4 operator ^(Int4 a, Int4 b) => new(a.x ^ b.x, a.y ^ b.y, a.z ^ b.z, a.w ^ b.w);

    public static explicit operator Int4(Complex val) => new((int)val.u, (int)val.i, 0, 0);
    public static explicit operator Int4(Quaternion val) => new((int)val.u, (int)val.i, (int)val.j, (int)val.k);
    public static explicit operator Int4(Float2 val) => new((int)val.x, (int)val.y, 0, 0);
    public static explicit operator Int4(Float3 val) => new((int)val.x, (int)val.y, (int)val.z, 0);
    public static explicit operator Int4(Float4 val) => new((int)val.x, (int)val.y, (int)val.z, (int)val.w);
    public static implicit operator Int4(Int2 val) => new(val.x, val.y, 0, 0);
    public static implicit operator Int4(Int3 val) => new(val.x, val.y, val.z, 0);
    public static explicit operator Int4(Matrix m) => new((int)m[0, 0], (int)m[1, 0], (int)m[2, 0], (int)m[3, 0]);
    public static explicit operator Int4(Vector2d val) => (Int4)val.ToXYZ();
    public static explicit operator Int4(RGBA val) => val.ToRGBAByte();
    public static explicit operator Int4(CMYKA val) => (Int4)val.ToCMYKAByte();
    public static explicit operator Int4(HSVA val) => val.ToHSVAByte();
    public static implicit operator Int4(RGBAByte val) => new(val.R, val.G, val.B, val.A);
    public static explicit operator Int4(CMYKAByte val) => new(val.C, val.M, val.Y, val.K);
    public static implicit operator Int4(HSVAByte val) => new(val.H, val.S, val.V, val.A);
    public static implicit operator Int4(Fill<int> fill) => new(fill);
    public static implicit operator Int4((int x, int y, int z, int w) vals) =>
        new(vals.x, vals.y, vals.z, vals.w);
}
