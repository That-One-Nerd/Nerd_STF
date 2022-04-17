namespace Nerd_STF.Mathematics;

public struct Double3 : ICloneable, IComparable<Double3>, IEquatable<Double3>, IGroup<double>
{
    public static Double3 Back => new(0, 0, -1);
    public static Double3 Down => new(0, -1, 0);
    public static Double3 Forward => new(0, 0, 1);
    public static Double3 Left => new(-1, 0, 0);
    public static Double3 Right => new(1, 0, 0);
    public static Double3 Up => new(0, 1, 0);

    public static Double3 One => new(1, 1, 1);
    public static Double3 Zero => new(0, 0, 0);

    public double Magnitude => Mathf.Sqrt(x * x + y * y + z * z);
    public Double3 Normalized => this / Magnitude;

    public Double2 XY => new(x, y);
    public Double2 XZ => new(x, z);
    public Double2 YZ => new(y, z);

    public double x, y, z;

    public Double3(double all) : this(all, all, all) { }
    public Double3(double x, double y) : this(x, y, 0) { }
    public Double3(double x, double y, double z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }
    public Double3(Fill<double> fill) : this(fill(0), fill(1), fill(2)) { }
    public Double3(Fill<int> fill) : this(fill(0), fill(1), fill(2)) { }

    public double this[int index]
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

    public static Double3 Absolute(Double3 val) =>
        new(Mathf.Absolute(val.x), Mathf.Absolute(val.y), Mathf.Absolute(val.z));
    public static Double3 Average(params Double3[] vals) => Sum(vals) / vals.Length;
    public static Double3 Ceiling(Double3 val) =>
        new(Mathf.Ceiling(val.x), Mathf.Ceiling(val.y), Mathf.Ceiling(val.z));
    public static Double3 Clamp(Double3 val, Double3 min, Double3 max) =>
        new(Mathf.Clamp(val.x, min.x, max.x),
            Mathf.Clamp(val.y, min.y, max.y),
            Mathf.Clamp(val.z, min.z, max.z));
    public static Double3 ClampMagnitude(Double3 val, double minMag, double maxMag)
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
    public static Double3 Cross(Double3 a, Double3 b, bool normalized = false)
    {
        Double3 val = new(a.y * b.z - b.y * a.z,
                          b.x * a.z - a.x * b.z,
                          a.x * b.y - b.x * a.y);
        return normalized ? val.Normalized : val;
    }
    public static Double3 Divide(Double3 num, params Double3[] vals)
    {
        foreach (Double3 d in vals) num /= d;
        return num;
    }
    public static double Dot(Double3 a, Double3 b) => a.x * b.x + a.y * b.y + a.z * b.z;
    public static double Dot(params Double3[] vals)
    {
        if (vals.Length < 1) return 0;
        double x = 1, y = 1, z = 1;
        foreach (Double3 d in vals)
        {
            x *= d.x;
            y *= d.y;
            z *= d.z;
        }
        return x + y + z;
    }
    public static Double3 Floor(Double3 val) =>
        new(Mathf.Floor(val.x), Mathf.Floor(val.y), Mathf.Floor(val.z));
    public static Double3 Lerp(Double3 a, Double3 b, double t, bool clamp = true) =>
        new(Mathf.Lerp(a.x, b.x, t, clamp), Mathf.Lerp(a.y, b.y, t, clamp), Mathf.Lerp(a.z, b.z, t, clamp));
    public static Double3 Median(params Double3[] vals)
    {
        double index = Mathf.Average(0, vals.Length - 1);
        Double3 valA = vals[Mathf.Floor(index)], valB = vals[Mathf.Ceiling(index)];
        return Average(valA, valB);
    }
    public static Double3 Max(params Double3[] vals)
    {
        if (vals.Length < 1) return Zero;
        Double3 val = vals[0];
        foreach (Double3 d in vals) val = d > val ? d : val;
        return val;
    }
    public static Double3 Min(params Double3[] vals)
    {
        if (vals.Length < 1) return Zero;
        Double3 val = vals[0];
        foreach (Double3 d in vals) val = d < val ? d : val;
        return val;
    }
    public static Double3 Multiply(params Double3[] vals)
    {
        if (vals.Length < 1) return Zero;
        Double3 val = One;
        foreach (Double3 d in vals) val *= d;
        return val;
    }
    public static Double3 Subtract(Double3 num, params Double3[] vals)
    {
        foreach (Double3 d in vals) num -= d;
        return num;
    }
    public static Double3 Sum(params Double3[] vals)
    {
        Double3 val = Zero;
        foreach (Double3 d in vals) val += d;
        return val;
    }

    public int CompareTo(Double3 other) => Magnitude.CompareTo(other.Magnitude);
    public override bool Equals([NotNullWhen(true)] object? obj)
    {
        if (obj == null || obj.GetType() != typeof(Double3)) return false;
        return Equals((Double3)obj);
    }
    public bool Equals(Double3 other) => x == other.x && y == other.y && z == other.z;
    public override int GetHashCode() => x.GetHashCode() ^ y.GetHashCode() ^ z.GetHashCode();
    public override string ToString() => ToString((string?)null);
    public string ToString(string? provider) =>
        "X: " + x.ToString(provider) + " Y: " + y.ToString(provider) + " Z: " + z.ToString(provider);
    public string ToString(IFormatProvider provider) =>
        "X: " + x.ToString(provider) + " Y: " + y.ToString(provider) + " Z: " + z.ToString(provider);

    public object Clone() => new Double3(x, y, z);

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    public IEnumerator<double> GetEnumerator()
    {
        yield return x;
        yield return y;
        yield return z;
    }

    public double[] ToArray() => new[] { x, y, z };
    public List<double> ToList() => new() { x, y, z };

    public static Double3 operator +(Double3 a, Double3 b) => new(a.x + b.x, a.y + b.y, a.z + b.z);
    public static Double3 operator -(Double3 d) => new(-d.x, -d.y, -d.z);
    public static Double3 operator -(Double3 a, Double3 b) => new(a.x - b.x, a.y - b.y, a.z - b.z);
    public static Double3 operator *(Double3 a, Double3 b) => new(a.x * b.x, a.y * b.y, a.z * b.z);
    public static Double3 operator *(Double3 a, double b) => new(a.x * b, a.y * b, a.z * b);
    public static Double3 operator /(Double3 a, Double3 b) => new(a.x / b.x, a.y / b.y, a.z / b.z);
    public static Double3 operator /(Double3 a, double b) => new(a.x / b, a.y / b, a.z / b);
    public static bool operator ==(Double3 a, Double3 b) => a.Equals(b);
    public static bool operator !=(Double3 a, Double3 b) => !a.Equals(b);
    public static bool operator >(Double3 a, Double3 b) => a.CompareTo(b) > 0;
    public static bool operator <(Double3 a, Double3 b) => a.CompareTo(b) < 0;
    public static bool operator >=(Double3 a, Double3 b) => a == b || a > b;
    public static bool operator <=(Double3 a, Double3 b) => a == b || a < b;

    public static implicit operator Double3(Double2 val) => new(val.x, val.y, 0);
    public static explicit operator Double3(Double4 val) => new(val.x, val.y, val.z);
    public static implicit operator Double3(Int2 val) => new(val.x, val.y, 0);
    public static implicit operator Double3(Int3 val) => new(val.x, val.y, val.z);
    public static explicit operator Double3(Int4 val) => new(val.x, val.y, val.z);
    public static implicit operator Double3(Vert val) => new(val.position.x, val.position.y, val.position.z);
    public static implicit operator Double3(Fill<double> fill) => new(fill);
    public static implicit operator Double3(Fill<int> fill) => new(fill);
}
