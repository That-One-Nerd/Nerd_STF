namespace Nerd_STF.Mathematics;

public struct Float4 : ICloneable, IComparable<Float4>, IEquatable<Float4>, IGroup<float>
{
    public static Float4 Back => new(0, 0, -1, 0);
    public static Float4 Down => new(0, -1, 0, 0);
    public static Float4 Far => new(0, 0, 0, 1);
    public static Float4 Forward => new(0, 0, 1, 0);
    public static Float4 Left => new(-1, 0, 0, 0);
    public static Float4 Near => new(0, 0, 0, -1);
    public static Float4 Right => new(1, 0, 0, 0);
    public static Float4 Up => new(0, 1, 0, 0);

    public static Float4 One => new(1, 1, 1, 1);
    public static Float4 Zero => new(0, 0, 0, 0);

    public float Magnitude => Mathf.Sqrt(x * x + y * y + z * z + w * w);
    public Float4 Normalized => this * Mathf.InverseSqrt(x * x + y * y + z * z + w * w);

    public Float2 XY => new(x, y);
    public Float2 XZ => new(x, z);
    public Float2 XW => new(x, w);
    public Float2 YW => new(y, w);
    public Float2 YZ => new(y, z);
    public Float2 ZW => new(z, w);

    public Float3 XYW => new(x, y, w);
    public Float3 XYZ => new(x, y, z);
    public Float3 YZW => new(y, z, w);
    public Float3 XZW => new(x, z, w);

    public float x, y, z, w;

    public Float4(float all) : this(all, all, all, all) { }
    public Float4(float x, float y) : this(x, y, 0, 0) { }
    public Float4(float x, float y, float z) : this(x, y, z, 0) { }
    public Float4(float x, float y, float z, float w)
    {
        this.x = x;
        this.y = y;
        this.z = z;
        this.w = w;
    }
    public Float4(Fill<float> fill) : this(fill(0), fill(1), fill(2), fill(3)) { }
    public Float4(Fill<int> fill) : this(fill(0), fill(1), fill(2), fill(3)) { }

    public float this[int index]
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

    public static Float4 Absolute(Float4 val) =>
        new(Mathf.Absolute(val.x), Mathf.Absolute(val.y), Mathf.Absolute(val.z), Mathf.Absolute(val.w));
    public static Float4 Average(params Float4[] vals) => Sum(vals) / vals.Length;
    public static Float4 Ceiling(Float4 val) =>
        new(Mathf.Ceiling(val.x), Mathf.Ceiling(val.y), Mathf.Ceiling(val.z), Mathf.Ceiling(val.w));
    public static Float4 Clamp(Float4 val, Float4 min, Float4 max) =>
        new(Mathf.Clamp(val.x, min.x, max.x),
            Mathf.Clamp(val.y, min.y, max.y),
            Mathf.Clamp(val.z, min.z, max.z),
            Mathf.Clamp(val.w, min.w, max.w));
    public static Float4 ClampMagnitude(Float4 val, float minMag, float maxMag)
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
    public static Float4 Divide(Float4 num, params Float4[] vals)
    {
        foreach (Float4 d in vals) num /= d;
        return num;
    }
    public static float Dot(Float4 a, Float4 b) => a.x * b.x + a.y * b.y + a.z * b.z + a.w * b.w;
    public static float Dot(params Float4[] vals)
    {
        if (vals.Length < 1) return 0;
        float x = 1, y = 1, z = 1, w = 1;
        foreach (Float4 d in vals)
        {
            x *= d.x;
            y *= d.y;
            z *= d.z;
            w *= d.w;
        }
        return x + y + z;
    }
    public static Float4 Floor(Float4 val) =>
        new(Mathf.Floor(val.x), Mathf.Floor(val.y), Mathf.Floor(val.z), Mathf.Floor(val.w));
    public static Float4 Lerp(Float4 a, Float4 b, float t, bool clamp = true) =>
        new(Mathf.Lerp(a.x, b.x, t, clamp), Mathf.Lerp(a.y, b.y, t, clamp), Mathf.Lerp(a.z, b.z, t, clamp),
            Mathf.Lerp(a.w, b.w, t, clamp));
    public static Float4 Median(params Float4[] vals)
    {
        float index = Mathf.Average(0, vals.Length - 1);
        Float4 valA = vals[Mathf.Floor(index)], valB = vals[Mathf.Ceiling(index)];
        return Average(valA, valB);
    }
    public static Float4 Max(params Float4[] vals)
    {
        if (vals.Length < 1) return Zero;
        Float4 val = vals[0];
        foreach (Float4 d in vals) val = d > val ? d : val;
        return val;
    }
    public static Float4 Min(params Float4[] vals)
    {
        if (vals.Length < 1) return Zero;
        Float4 val = vals[0];
        foreach (Float4 d in vals) val = d < val ? d : val;
        return val;
    }
    public static Float4 Round(Float4 val) =>
        new(Mathf.Round(val.x), Mathf.Round(val.y), Mathf.Round(val.z), Mathf.Round(val.w));
    public static Float4 Product(params Float4[] vals)
    {
        if (vals.Length < 1) return Zero;
        Float4 val = One;
        foreach (Float4 d in vals) val *= d;
        return val;
    }
    public static Float4 Subtract(Float4 num, params Float4[] vals)
    {
        foreach (Float4 d in vals) num -= d;
        return num;
    }
    public static Float4 Sum(params Float4[] vals)
    {
        Float4 val = Zero;
        foreach (Float4 d in vals) val += d;
        return val;
    }

    public static (float[] Xs, float[] Ys, float[] Zs, float[] Ws) SplitArray(params Float4[] vals)
    {
        float[] Xs = new float[vals.Length], Ys = new float[vals.Length], Zs = new float[vals.Length],
                Ws = new float[vals.Length];
        for (int i = 0; i < vals.Length; i++)
        {
            Xs[i] = vals[i].x;
            Ys[i] = vals[i].y;
            Zs[i] = vals[i].z;
            Ws[i] = vals[i].w;
        }
        return (Xs, Ys, Zs, Ws);
    }

    public int CompareTo(Float4 other) => Magnitude.CompareTo(other.Magnitude);
    public override bool Equals([NotNullWhen(true)] object? obj)
    {
        if (obj == null || obj.GetType() != typeof(Float4)) return base.Equals(obj);
        return Equals((Float4)obj);
    }
    public bool Equals(Float4 other) => x == other.x && y == other.y && z == other.z && w == other.w;
    public override int GetHashCode() => x.GetHashCode() ^ y.GetHashCode() ^ z.GetHashCode() ^ w.GetHashCode();
    public override string ToString() => ToString((string?)null);
    public string ToString(string? provider) =>
        "X: " + x.ToString(provider) + " Y: " + y.ToString(provider) + " Z: " + z.ToString(provider)
        + " W: " + w.ToString(provider);
    public string ToString(IFormatProvider provider) =>
        "X: " + x.ToString(provider) + " Y: " + y.ToString(provider) + " Z: " + z.ToString(provider)
        + " W: " + w.ToString(provider);

    public object Clone() => new Float4(x, y, z, w);

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    public IEnumerator<float> GetEnumerator()
    {
        yield return x;
        yield return y;
        yield return z;
        yield return w;
    }

    public float[] ToArray() => new[] { x, y, z, w };
    public Fill<float> ToFill()
    {
        Float4 @this = this;
        return i => @this[i];
    }
    public List<float> ToList() => new() { x, y, z, w };

    public static Float4 operator +(Float4 a, Float4 b) => new(a.x + b.x, a.y + b.y, a.z + b.z, a.w + b.w);
    public static Float4 operator -(Float4 d) => new(-d.x, -d.y, -d.z, -d.w);
    public static Float4 operator -(Float4 a, Float4 b) => new(a.x - b.x, a.y - b.y, a.z - b.z, a.w - b.w);
    public static Float4 operator *(Float4 a, Float4 b) => new(a.x * b.x, a.y * b.y, a.z * b.z, a.w * b.w);
    public static Float4 operator *(Float4 a, float b) => new(a.x * b, a.y * b, a.z * b, a.w * b);
    public static Float4 operator *(Float4 a, Matrix b) => (Float4)((Matrix)a * b);
    public static Float4 operator /(Float4 a, Float4 b) => new(a.x / b.x, a.y / b.y, a.z / b.z, a.w / b.w);
    public static Float4 operator /(Float4 a, float b) => new(a.x / b, a.y / b, a.z / b, a.w / b);
    public static Float4 operator /(Float4 a, Matrix b) => (Float4)((Matrix)a / b);
    public static bool operator ==(Float4 a, Float4 b) => a.Equals(b);
    public static bool operator !=(Float4 a, Float4 b) => !a.Equals(b);
    public static bool operator >(Float4 a, Float4 b) => a.CompareTo(b) > 0;
    public static bool operator <(Float4 a, Float4 b) => a.CompareTo(b) < 0;
    public static bool operator >=(Float4 a, Float4 b) => a == b || a > b;
    public static bool operator <=(Float4 a, Float4 b) => a == b || a < b;

    public static implicit operator Float4(Complex val) => new(val.u, val.i, 0, 0);
    public static implicit operator Float4(Quaternion val) => new(val.u, val.i, val.j, val.k);
    public static implicit operator Float4(Float2 val) => new(val.x, val.y, 0, 0);
    public static implicit operator Float4(Float3 val) => new(val.x, val.y, val.z, 0);
    public static implicit operator Float4(Int2 val) => new(val.x, val.y, 0, 0);
    public static implicit operator Float4(Int3 val) => new(val.x, val.y, val.z, 0);
    public static implicit operator Float4(Int4 val) => new(val.x, val.y, val.z, val.w);
    public static explicit operator Float4(Matrix m) => new(m[0, 0], m[1, 0], m[2, 0], m[3, 0]);
    public static explicit operator Float4(Vector2d val) => val.ToXYZ();
    public static implicit operator Float4(Vert val) => new(val.position.x, val.position.y, val.position.z, 0);
    public static implicit operator Float4(RGBA val) => new(val.R, val.G, val.B, val.A);
    public static explicit operator Float4(CMYKA val) => new(val.C, val.M, val.Y, val.K);
    public static explicit operator Float4(HSVA val) => new(val.H.Normalized, val.S, val.V, val.A);
    public static implicit operator Float4(RGBAByte val) => val.ToRGBA();
    public static explicit operator Float4(CMYKAByte val) => (Float4)val.ToCMYKA();
    public static implicit operator Float4(HSVAByte val) => (Float4)val.ToHSVA();
    public static implicit operator Float4(Fill<float> fill) => new(fill);
    public static implicit operator Float4(Fill<int> fill) => new(fill);
}
