namespace Nerd_STF.Mathematics;

public struct Int4 : ICloneable, IComparable<Int4>, IEquatable<Int4>, IGroup<int>
{
    public static Int4 Back => new(0, 0, -1, 0);
    public static Int4 Deep => new(0, 0, 0, -1);
    public static Int4 Down => new(0, -1, 0, 0);
    public static Int4 Far => new(0, 0, 0, 1);
    public static Int4 Forward => new(0, 0, 1, 0);
    public static Int4 Left => new(-1, 0, 0, 0);
    public static Int4 Right => new(1, 0, 0, 0);
    public static Int4 Up => new(0, 1, 0, 0);

    public static Int4 One => new(1, 1, 1, 1);
    public static Int4 Zero => new(0, 0, 0, 0);

    public float Magnitude => Mathf.Sqrt(x * x + y * y + z * z + w * w);
    public Int4 Normalized => (Int4)((Float4)this / Magnitude);

    public Int2 XY => new(x, y);
    public Int2 XZ => new(x, z);
    public Int2 XW => new(x, w);
    public Int2 YW => new(y, w);
    public Int2 YZ => new(y, z);
    public Int2 ZW => new(z, w);

    public Int3 XYW => new(x, y, w);
    public Int3 XYZ => new(x, y, z);
    public Int3 YZW => new(y, z, w);
    public Int3 XZW => new(x, z, w);

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
    public static Int4 Divide(Int4 num, params Int4[] vals)
    {
        foreach (Int4 d in vals) num /= d;
        return num;
    }
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
        int index = Mathf.Average(0, vals.Length - 1);
        Int4 valA = vals[Mathf.Floor(index)], valB = vals[Mathf.Ceiling(index)];
        return Average(valA, valB);
    }
    public static Int4 Max(params Int4[] vals)
    {
        if (vals.Length < 1) return Zero;
        Int4 val = vals[0];
        foreach (Int4 d in vals) val = d > val ? d : val;
        return val;
    }
    public static Int4 Min(params Int4[] vals)
    {
        if (vals.Length < 1) return Zero;
        Int4 val = vals[0];
        foreach (Int4 d in vals) val = d < val ? d : val;
        return val;
    }
    public static Int4 Multiply(params Int4[] vals)
    {
        if (vals.Length < 1) return Zero;
        Int4 val = One;
        foreach (Int4 d in vals) val *= d;
        return val;
    }
    public static Int4 Subtract(Int4 num, params Int4[] vals)
    {
        foreach (Int4 d in vals) num -= d;
        return num;
    }
    public static Int4 Sum(params Int4[] vals)
    {
        Int4 val = Zero;
        foreach (Int4 d in vals) val += d;
        return val;
    }

    public int CompareTo(Int4 other) => Magnitude.CompareTo(other.Magnitude);
    public override bool Equals([NotNullWhen(true)] object? obj)
    {
        if (obj == null || obj.GetType() != typeof(Int4)) return false;
        return Equals((Int4)obj);
    }
    public bool Equals(Int4 other) => x == other.x && y == other.y && z == other.z && w == other.w;
    public override int GetHashCode() => x.GetHashCode() ^ y.GetHashCode() ^ z.GetHashCode() ^ w.GetHashCode();
    public override string ToString() => ToString((string?)null);
    public string ToString(string? provider) =>
        "X: " + x.ToString(provider) + " Y: " + y.ToString(provider) + " Z: " + z.ToString(provider)
        + " W: " + w.ToString(provider);
    public string ToString(IFormatProvider provider) =>
        "X: " + x.ToString(provider) + " Y: " + y.ToString(provider) + " Z: " + z.ToString(provider)
        + " W: " + w.ToString(provider);

    public object Clone() => new Int4(x, y, z, w);

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    public IEnumerator<int> GetEnumerator()
    {
        yield return x;
        yield return y;
        yield return z;
        yield return w;
    }

    public int[] ToArray() => new[] { x, y, z, w };
    public List<int> ToList() => new() { x, y, z, w };

    public static Int4 operator +(Int4 a, Int4 b) => new(a.x + b.x, a.y + b.y, a.z + b.z, a.w + b.w);
    public static Int4 operator -(Int4 d) => new(-d.x, -d.y, -d.z, -d.w);
    public static Int4 operator -(Int4 a, Int4 b) => new(a.x - b.x, a.y - b.y, a.z - b.z, a.w - b.w);
    public static Int4 operator *(Int4 a, Int4 b) => new(a.x * b.x, a.y * b.y, a.z * b.z, a.w * b.w);
    public static Int4 operator *(Int4 a, int b) => new(a.x * b, a.y * b, a.z * b, a.w * b);
    public static Int4 operator /(Int4 a, Int4 b) => new(a.x / b.x, a.y / b.y, a.z / b.z, a.w / b.w);
    public static Int4 operator /(Int4 a, int b) => new(a.x / b, a.y / b, a.z / b, a.w / b);
    public static Int4 operator &(Int4 a, Int4 b) => new(a.x & b.x, a.y & b.y, a.z & b.z, a.w & b.w);
    public static Int4 operator |(Int4 a, Int4 b) => new(a.x | b.x, a.y | b.y, a.z | b.z, a.w | b.w);
    public static Int4 operator ^(Int4 a, Int4 b) => new(a.x ^ b.x, a.y ^ b.y, a.z ^ b.z, a.w ^ b.w);
    public static bool operator ==(Int4 a, Int4 b) => a.Equals(b);
    public static bool operator !=(Int4 a, Int4 b) => !a.Equals(b);
    public static bool operator >(Int4 a, Int4 b) => a.CompareTo(b) > 0;
    public static bool operator <(Int4 a, Int4 b) => a.CompareTo(b) < 0;
    public static bool operator >=(Int4 a, Int4 b) => a == b || a > b;
    public static bool operator <=(Int4 a, Int4 b) => a == b || a < b;

    public static explicit operator Int4(Float2 val) => new((int)val.x, (int)val.y, 0, 0);
    public static explicit operator Int4(Float3 val) => new((int)val.x, (int)val.y, (int)val.z, 0);
    public static explicit operator Int4(Float4 val) => new((int)val.x, (int)val.y, (int)val.z, (int)val.w);
    public static implicit operator Int4(Int2 val) => new(val.x, val.y, 0, 0);
    public static implicit operator Int4(Int3 val) => new(val.x, val.y, val.z, 0);
    public static explicit operator Int4(Vert val) => new((int)val.position.x, (int)val.position.y,
                                                          (int)val.position.z, 0);
    public static implicit operator Int4(Fill<int> fill) => new(fill);
}
