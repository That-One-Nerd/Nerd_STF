using System.Data;

namespace Nerd_STF.Mathematics;

public record struct Int3 : IAbsolute<Int3>, IAverage<Int3>, IClamp<Int3>, IClampMagnitude<Int3, int>,
    IComparable<Int3>, ICross<Int3>, IDivide<Int3>, IDot<Int3, int>, IEquatable<Int3>,
    IFromTuple<Int3, (int x, int y, int z)>, IGroup<int>, IIndexAll<int>, IIndexRangeAll<int>, ILerp<Int3, float>,
    IMathOperators<Int3>, IMax<Int3>, IMedian<Int3>, IMin<Int3>, IPresets3d<Int3>, IProduct<Int3>,
    ISplittable<Int3, (int[] Xs, int[] Ys, int[] Zs)>, ISubtract<Int3>, ISum<Int3>
{
    public static Int3 Back => new(0, 0, -1);
    public static Int3 Down => new(0, -1, 0);
    public static Int3 Forward => new(0, 0, 1);
    public static Int3 Left => new(-1, 0, 0);
    public static Int3 Right => new(1, 0, 0);
    public static Int3 Up => new(0, 1, 0);

    public static Int3 One => new(1, 1, 1);
    public static Int3 Zero => new(0, 0, 0);

    public float Magnitude => Mathf.Sqrt(x * x + y * y + z * z);
    public Int3 Normalized => (Int3)((Float3)this * Mathf.InverseSqrt(x * x + y * y + z * z));

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
    public Int2 YZ
    {
        get => (y, z);
        set
        {
            y = value.x;
            z = value.y;
        }
    }

    public int x, y, z;

    public Int3(int all) : this(all, all, all) { }
    public Int3(int x, int y) : this(x, y, 0) { }
    public Int3(int x, int y, int z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }
    public Int3(Fill<int> fill) : this(fill(0), fill(1), fill(2)) { }

    public int this[int index]
    {
        get => index switch
        {
            0 => x,
            1 => y,
            2 => z,
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

                default: throw new IndexOutOfRangeException(nameof(index));
            }
        }
    }
    public int this[Index index]
    {
        get => this[index.IsFromEnd ? 3 - index.Value : index.Value];
        set => this[index.IsFromEnd ? 3 - index.Value : index.Value] = value;
    }
    public int[] this[Range range]
    {
        get
        {
            int start = range.Start.IsFromEnd ? 3 - range.Start.Value : range.Start.Value;
            int end = range.End.IsFromEnd ? 3 - range.End.Value : range.End.Value;
            List<int> res = new();
            for (int i = start; i < end; i++) res.Add(this[i]);
            return res.ToArray();
        }
        set
        {
            int start = range.Start.IsFromEnd ? 3 - range.Start.Value : range.Start.Value;
            int end = range.End.IsFromEnd ? 3 - range.End.Value : range.End.Value;
            for (int i = start; i < end; i++) this[i] = value[i];
        }
    }

    public static Int3 Absolute(Int3 val) =>
        new(Mathf.Absolute(val.x), Mathf.Absolute(val.y), Mathf.Absolute(val.z));
    public static Int3 Average(params Int3[] vals) => Sum(vals) / vals.Length;
    public static Int3 Clamp(Int3 val, Int3 min, Int3 max) =>
        new(Mathf.Clamp(val.x, min.x, max.x),
            Mathf.Clamp(val.y, min.y, max.y),
            Mathf.Clamp(val.z, min.z, max.z));
    public static Int3 ClampMagnitude(Int3 val, int minMag, int maxMag)
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
    public static Int3 Cross(Int3 a, Int3 b, bool normalized = false)
    {
        Int3 val = new(a.y * b.z - b.y * a.z,
                       b.x * a.z - a.x * b.z,
                       a.x * b.y - b.x * a.y);
        return normalized ? val.Normalized : val;
    }
    public static Int3 Divide(Int3 num, params Int3[] vals) => num / Product(vals);
    public static int Dot(Int3 a, Int3 b) => a.x * b.x + a.y * b.y + a.z * b.z;
    public static int Dot(params Int3[] vals)
    {
        if (vals.Length < 1) return 0;
        int x = 1, y = 1, z = 1;
        foreach (Int3 d in vals)
        {
            x *= d.x;
            y *= d.y;
            z *= d.z;
        }
        return x + y + z;
    }
    public static Int3 Lerp(Int3 a, Int3 b, float t, bool clamp = true) =>
        new(Mathf.Lerp(a.x, b.x, t, clamp), Mathf.Lerp(a.y, b.y, t, clamp), Mathf.Lerp(a.z, b.z, t, clamp));
    public static Int3 Median(params Int3[] vals)
    {
        float index = (vals.Length - 1) * 0.5f;
        Int3 valA = vals[Mathf.Floor(index)], valB = vals[Mathf.Ceiling(index)];
        return (valA + valB) / 2;
    }
    public static Int3 Max(params Int3[] vals)
    {
        if (vals.Length < 1) return Zero;
        Int3 val = vals[0];
        foreach (Int3 d in vals) val = d.Magnitude > val.Magnitude ? d : val;
        return val;
    }
    public static Int3 Min(params Int3[] vals)
    {
        if (vals.Length < 1) return Zero;
        Int3 val = vals[0];
        foreach (Int3 d in vals) val = d.Magnitude < val.Magnitude ? d : val;
        return val;
    }
    public static Int3 Product(params Int3[] vals)
    {
        if (vals.Length < 1) return Zero;
        Int3 val = One;
        foreach (Int3 d in vals) val *= d;
        return val;
    }
    public static Int3 Subtract(Int3 num, params Int3[] vals) => num - Sum(vals);
    public static Int3 Sum(params Int3[] vals)
    {
        Int3 val = Zero;
        foreach (Int3 d in vals) val += d;
        return val;
    }

    public static (int[] Xs, int[] Ys, int[] Zs) SplitArray(params Int3[] vals)
    {
        int[] Xs = new int[vals.Length], Ys = new int[vals.Length], Zs = new int[vals.Length];
        for (int i = 0; i < vals.Length; i++)
        {
            Xs[i] = vals[i].x;
            Ys[i] = vals[i].y;
            Zs[i] = vals[i].z;
        }
        return (Xs, Ys, Zs);
    }

    public int CompareTo(Int3 other) => Magnitude.CompareTo(other.Magnitude);
    public bool Equals(Int3 other) => x == other.x && y == other.y && z == other.z;
    public override int GetHashCode() => base.GetHashCode();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    public IEnumerator<int> GetEnumerator()
    {
        yield return x;
        yield return y;
        yield return z;
    }

    public int[] ToArray() => new[] { x, y, z };
    public Fill<int> ToFill()
    {
        Int3 @this = this;
        return i => @this[i];
    }
    public List<int> ToList() => new() { x, y, z };

    public Vector3d ToVector() => ((Float3)this).ToVector();

    private bool PrintMembers(StringBuilder builder)
    {
        builder.Append("x = ");
        builder.Append(x);
        builder.Append(", y = ");
        builder.Append(y);
        builder.Append(", z = ");
        builder.Append(z);
        return true;
    }

    public static Int3 operator +(Int3 a, Int3 b) => new(a.x + b.x, a.y + b.y, a.z + b.z);
    public static Int3 operator -(Int3 i) => new(-i.x, -i.y, -i.z);
    public static Int3 operator -(Int3 a, Int3 b) => new(a.x - b.x, a.y - b.y, a.z - b.z);
    public static Int3 operator *(Int3 a, Int3 b) => new(a.x * b.x, a.y * b.y, a.z * b.z);
    public static Int3 operator *(Int3 a, int b) => new(a.x * b, a.y * b, a.z * b);
    public static Int3 operator *(Int3 a, Matrix b) => (Int3)((Matrix)(Float3)a * b);
    public static Int3 operator /(Int3 a, Int3 b) => new(a.x / b.x, a.y / b.y, a.z / b.z);
    public static Int3 operator /(Int3 a, int b) => new(a.x / b, a.y / b, a.z / b);
    public static Int3 operator /(Int3 a, Matrix b) => (Int3)((Matrix)(Float3)a / b);
    public static Int3 operator &(Int3 a, Int3 b) => new(a.x & b.x, a.y & b.y, a.z & b.z);
    public static Int3 operator |(Int3 a, Int3 b) => new(a.x | b.x, a.y | b.y, a.z | b.z);
    public static Int3 operator ^(Int3 a, Int3 b) => new(a.x ^ b.x, a.y ^ b.y, a.z ^ b.z);

    public static explicit operator Int3(Complex val) => new((int)val.u, (int)val.i, 0);
    public static explicit operator Int3(Quaternion val) => new((int)val.u, (int)val.i, (int)val.j);
    public static explicit operator Int3(Float2 val) => new((int)val.x, (int)val.y, 0);
    public static explicit operator Int3(Float3 val) => new((int)val.x, (int)val.y, (int)val.z);
    public static explicit operator Int3(Float4 val) => new((int)val.x, (int)val.y, (int)val.z);
    public static implicit operator Int3(Int2 val) => new(val.x, val.y, 0);
    public static explicit operator Int3(Int4 val) => new(val.x, val.y, val.z);
    public static explicit operator Int3(Matrix m) => new((int)m[0, 0], (int)m[1, 0], (int)m[2, 0]);
    public static explicit operator Int3(Vector2d val) => (Int3)val.ToXYZ();
    public static explicit operator Int3(Vert val) => new((int)val.position.x, (int)val.position.y,
                                                          (int)val.position.z);
    public static explicit operator Int3(RGBA val) => (Int3)val.ToRGBAByte();
    public static explicit operator Int3(HSVA val) => (Int3)val.ToHSVAByte();
    public static explicit operator Int3(RGBAByte val) => new(val.R, val.G, val.B);
    public static explicit operator Int3(HSVAByte val) => new(val.H, val.S, val.V);
    public static implicit operator Int3(Fill<int> fill) => new(fill);
    public static implicit operator Int3((int x, int y, int z) vals) =>
        new(vals.x, vals.y, vals.z);
}
