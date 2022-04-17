namespace Nerd_STF.Mathematics;

public struct Double4 : ICloneable, IComparable<Double4>, IEquatable<Double4>, IGroup<double>
{
    public static Double4 Back => new(0, 0, -1, 0);
    public static Double4 Deep => new(0, 0, 0, -1);
    public static Double4 Down => new(0, -1, 0, 0);
    public static Double4 Far => new(0, 0, 0, 1);
    public static Double4 Forward => new(0, 0, 1, 0);
    public static Double4 Left => new(-1, 0, 0, 0);
    public static Double4 Right => new(1, 0, 0, 0);
    public static Double4 Up => new(0, 1, 0, 0);

    public static Double4 One => new(1, 1, 1, 1);
    public static Double4 Zero => new(0, 0, 0, 0);

    public double Magnitude => Mathf.Sqrt(x * x + y * y + z * z + w * w);
    public Double4 Normalized => this / Magnitude;

    public Double2 XY => new(x, y);
    public Double2 XZ => new(x, z);
    public Double2 XW => new(x, w);
    public Double2 YW => new(y, w);
    public Double2 YZ => new(y, z);
    public Double2 ZW => new(z, w);

    public Double3 XYW => new(x, y, w);
    public Double3 XYZ => new(x, y, z);
    public Double3 YZW => new(y, z, w);
    public Double3 XZW => new(x, z, w);

    public double x, y, z, w;

    public Double4(double all) : this(all, all, all, all) { }
    public Double4(double x, double y) : this(x, y, 0, 0) { }
    public Double4(double x, double y, double z) : this(x, y, z, 0) { }
    public Double4(double x, double y, double z, double w)
    {
        this.x = x;
        this.y = y;
        this.z = z;
        this.w = w;
    }
    public Double4(Fill<double> fill) : this(fill(0), fill(1), fill(2), fill(3)) { }
    public Double4(Fill<int> fill) : this(fill(0), fill(1), fill(2), fill(3)) { }

    public double this[int index]
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

    public static Double4 Absolute(Double4 val) =>
        new(Mathf.Absolute(val.x), Mathf.Absolute(val.y), Mathf.Absolute(val.z), Mathf.Absolute(val.w));
    public static Double4 Average(params Double4[] vals) => Sum(vals) / vals.Length;
    public static Double4 Ceiling(Double4 val) =>
        new(Mathf.Ceiling(val.x), Mathf.Ceiling(val.y), Mathf.Ceiling(val.z), Mathf.Ceiling(val.w));
    public static Double4 Clamp(Double4 val, Double4 min, Double4 max) =>
        new(Mathf.Clamp(val.x, min.x, max.x),
            Mathf.Clamp(val.y, min.y, max.y),
            Mathf.Clamp(val.z, min.z, max.z),
            Mathf.Clamp(val.w, min.w, max.w));
    public static Double4 ClampMagnitude(Double4 val, double minMag, double maxMag)
    {
        if (maxMag < minMag) throw new ArgumentOutOfRangeException(nameof(maxMag),
            nameof(maxMag) + " must be greater than or equal to " + nameof(minMag));
        double mag = val.Magnitude;
        if (mag >= minMag && mag <= maxMag) return val;
        val = val.Normalized;
        if (mag < minMag) val *= minMag;
        else if (mag > maxMag) val *= maxMag;
        return val;
    }
    public static Double4 Divide(Double4 num, params Double4[] vals)
    {
        foreach (Double4 d in vals) num /= d;
        return num;
    }
    public static double Dot(Double4 a, Double4 b) => a.x * b.x + a.y * b.y + a.z * b.z + a.w * b.w;
    public static double Dot(params Double4[] vals)
    {
        if (vals.Length < 1) return 0;
        double x = 1, y = 1, z = 1, w = 1;
        foreach (Double4 d in vals)
        {
            x *= d.x;
            y *= d.y;
            z *= d.z;
            w *= d.w;
        }
        return x + y + z;
    }
    public static Double4 Floor(Double4 val) =>
        new(Mathf.Floor(val.x), Mathf.Floor(val.y), Mathf.Floor(val.z), Mathf.Floor(val.w));
    public static Double4 Lerp(Double4 a, Double4 b, double t, bool clamp = true) =>
        new(Mathf.Lerp(a.x, b.x, t, clamp), Mathf.Lerp(a.y, b.y, t, clamp), Mathf.Lerp(a.z, b.z, t, clamp),
            Mathf.Lerp(a.w, b.w, t, clamp));
    public static Double4 Median(params Double4[] vals)
    {
        double index = Mathf.Average(0, vals.Length - 1);
        Double4 valA = vals[Mathf.Floor(index)], valB = vals[Mathf.Ceiling(index)];
        return Average(valA, valB);
    }
    public static Double4 Max(params Double4[] vals)
    {
        if (vals.Length < 1) return Zero;
        Double4 val = vals[0];
        foreach (Double4 d in vals) val = d > val ? d : val;
        return val;
    }
    public static Double4 Min(params Double4[] vals)
    {
        if (vals.Length < 1) return Zero;
        Double4 val = vals[0];
        foreach (Double4 d in vals) val = d < val ? d : val;
        return val;
    }
    public static Double4 Multiply(params Double4[] vals)
    {
        if (vals.Length < 1) return Zero;
        Double4 val = One;
        foreach (Double4 d in vals) val *= d;
        return val;
    }
    public static Double4 Subtract(Double4 num, params Double4[] vals)
    {
        foreach (Double4 d in vals) num -= d;
        return num;
    }
    public static Double4 Sum(params Double4[] vals)
    {
        Double4 val = Zero;
        foreach (Double4 d in vals) val += d;
        return val;
    }

    public int CompareTo(Double4 other) => Magnitude.CompareTo(other.Magnitude);
    public override bool Equals([NotNullWhen(true)] object? obj)
    {
        if (obj == null || obj.GetType() != typeof(Double4)) return false;
        return Equals((Double4)obj);
    }
    public bool Equals(Double4 other) => x == other.x && y == other.y && z == other.z && w == other.w;
    public override int GetHashCode() => x.GetHashCode() ^ y.GetHashCode() ^ z.GetHashCode() ^ w.GetHashCode();
    public override string ToString() => ToString((string?)null);
    public string ToString(string? provider) =>
        "X: " + x.ToString(provider) + " Y: " + y.ToString(provider) + " Z: " + z.ToString(provider)
        + " W: " + w.ToString(provider);
    public string ToString(IFormatProvider provider) =>
        "X: " + x.ToString(provider) + " Y: " + y.ToString(provider) + " Z: " + z.ToString(provider)
        + " W: " + w.ToString(provider);

    public object Clone() => new Double4(x, y, z, w);

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    public IEnumerator<double> GetEnumerator()
    {
        yield return x;
        yield return y;
        yield return z;
        yield return w;
    }

    public double[] ToArray() => new[] { x, y, z, w };
    public List<double> ToList() => new() { x, y, z, w };

    public static Double4 operator +(Double4 a, Double4 b) => new(a.x + b.x, a.y + b.y, a.z + b.z, a.w + b.w);
    public static Double4 operator -(Double4 d) => new(-d.x, -d.y, -d.z, -d.w);
    public static Double4 operator -(Double4 a, Double4 b) => new(a.x - b.x, a.y - b.y, a.z - b.z, a.w - b.w);
    public static Double4 operator *(Double4 a, Double4 b) => new(a.x * b.x, a.y * b.y, a.z * b.z, a.w * b.w);
    public static Double4 operator *(Double4 a, double b) => new(a.x * b, a.y * b, a.z * b, a.w * b);
    public static Double4 operator /(Double4 a, Double4 b) => new(a.x / b.x, a.y / b.y, a.z / b.z, a.w / b.w);
    public static Double4 operator /(Double4 a, double b) => new(a.x / b, a.y / b, a.z / b, a.w / b);
    public static bool operator ==(Double4 a, Double4 b) => a.Equals(b);
    public static bool operator !=(Double4 a, Double4 b) => !a.Equals(b);
    public static bool operator >(Double4 a, Double4 b) => a.CompareTo(b) > 0;
    public static bool operator <(Double4 a, Double4 b) => a.CompareTo(b) < 0;
    public static bool operator >=(Double4 a, Double4 b) => a == b || a > b;
    public static bool operator <=(Double4 a, Double4 b) => a == b || a < b;

    public static implicit operator Double4(Double2 val) => new(val.x, val.y, 0, 0);
    public static implicit operator Double4(Double3 val) => new(val.x, val.y, val.z, 0);
    public static implicit operator Double4(Int2 val) => new(val.x, val.y, 0, 0);
    public static implicit operator Double4(Int3 val) => new(val.x, val.y, val.z, 0);
    public static implicit operator Double4(Int4 val) => new(val.x, val.y, val.z, val.w);
    public static implicit operator Double4(Vert val) => new(val.position.x, val.position.y, val.position.z, 0);
    public static implicit operator Double4(Fill<double> fill) => new(fill);
    public static implicit operator Double4(Fill<int> fill) => new(fill);
}
