namespace Nerd_STF.Mathematics;

public struct Double2 : ICloneable, IComparable<Double2>, IEquatable<Double2>, IGroup<double>
{
    public static Double2 Down => new(0, -1);
    public static Double2 Left => new(-1, 0);
    public static Double2 Right => new(1, 0);
    public static Double2 Up => new(0, 1);

    public static Double2 One => new(1, 1);
    public static Double2 Zero => new(0, 0);

    public double Magnitude => Mathf.Sqrt(x * x + y * y);
    public Double2 Normalized => this / Magnitude;

    public double x, y;

    public Double2(double all) : this(all, all) { }
    public Double2(double x, double y)
    {
        this.x = x;
        this.y = y;
    }
    public Double2(Fill<double> fill) : this(fill(0), fill(1)) { }
    public Double2(Fill<int> fill) : this(fill(0), fill(1)) { }

    public double this[int index]
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

    public static Double2 Absolute(Double2 val) =>
        new(Mathf.Absolute(val.x), Mathf.Absolute(val.y));
    public static Double2 Average(params Double2[] vals) => Sum(vals) / vals.Length;
    public static Double2 Ceiling(Double2 val) =>
        new(Mathf.Ceiling(val.x), Mathf.Ceiling(val.y));
    public static Double2 Clamp(Double2 val, Double2 min, Double2 max) =>
        new(Mathf.Clamp(val.x, min.x, max.x),
            Mathf.Clamp(val.y, min.y, max.y));
    public static Double2 ClampMagnitude(Double2 val, double minMag, double maxMag)
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
    public static Double3 Cross(Double2 a, Double2 b, bool normalized = false) =>
        Double3.Cross(a, b, normalized);
    public static Double2 Divide(Double2 num, params Double2[] vals)
    {
        foreach (Double2 d in vals) num /= d;
        return num;
    }
    public static double Dot(Double2 a, Double2 b) => a.x * b.x + a.y * b.y;
    public static double Dot(params Double2[] vals)
    {
        if (vals.Length < 1) return 0;
        double x = 1, y = 1;
        foreach (Double2 d in vals)
        {
            x *= d.x;
            y *= d.y;
        }
        return x + y;
    }
    public static Double2 Floor(Double2 val) =>
        new(Mathf.Floor(val.x), Mathf.Floor(val.y));
    public static Double2 Lerp(Double2 a, Double2 b, double t, bool clamp = true) =>
        new(Mathf.Lerp(a.x, b.x, t, clamp), Mathf.Lerp(a.y, b.y, t, clamp));
    public static Double2 Median(params Double2[] vals)
    {
        double index = Mathf.Average(0, vals.Length - 1);
        Double2 valA = vals[Mathf.Floor(index)], valB = vals[Mathf.Ceiling(index)];
        return Average(valA, valB);
    }
    public static Double2 Max(params Double2[] vals)
    {
        if (vals.Length < 1) return Zero;
        Double2 val = vals[0];
        foreach (Double2 d in vals) val = d > val ? d : val;
        return val;
    }
    public static Double2 Min(params Double2[] vals)
    {
        if (vals.Length < 1) return Zero;
        Double2 val = vals[0];
        foreach (Double2 d in vals) val = d < val ? d : val;
        return val;
    }
    public static Double2 Multiply(params Double2[] vals)
    {
        if (vals.Length < 1) return Zero;
        Double2 val = One;
        foreach (Double2 d in vals) val *= d;
        return val;
    }
    public static Double2 Subtract(Double2 num, params Double2[] vals)
    {
        foreach (Double2 d in vals) num -= d;
        return num;
    }
    public static Double2 Sum(params Double2[] vals)
    {
        Double2 val = Zero;
        foreach (Double2 d in vals) val += d;
        return val;
    }

    public int CompareTo(Double2 other) => Magnitude.CompareTo(other.Magnitude);
    public override bool Equals([NotNullWhen(true)] object? obj)
    {
        if (obj == null || obj.GetType() != typeof(Double2)) return false;
        return Equals((Double2)obj);
    }
    public bool Equals(Double2 other) => x == other.x && y == other.y;
    public override int GetHashCode() => x.GetHashCode() ^ y.GetHashCode();
    public override string ToString() => ToString((string?)null);
    public string ToString(string? provider) =>
        "X: " + x.ToString(provider) + " Y: " + y.ToString(provider);
    public string ToString(IFormatProvider provider) =>
        "X: " + x.ToString(provider) + " Y: " + y.ToString(provider);

    public object Clone() => new Double2(x, y);

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    public IEnumerator<double> GetEnumerator()
    {
        yield return x;
        yield return y;
    }

    public double[] ToArray() => new[] { x, y };
    public List<double> ToList() => new() { x, y };

    public static Double2 operator +(Double2 a, Double2 b) => new(a.x + b.x, a.y + b.y);
    public static Double2 operator -(Double2 d) => new(-d.x, -d.y);
    public static Double2 operator -(Double2 a, Double2 b) => new(a.x - b.x, a.y - b.y);
    public static Double2 operator *(Double2 a, Double2 b) => new(a.x * b.x, a.y * b.y);
    public static Double2 operator *(Double2 a, double b) => new(a.x * b, a.y * b);
    public static Double2 operator /(Double2 a, Double2 b) => new(a.x / b.x, a.y / b.y);
    public static Double2 operator /(Double2 a, double b) => new(a.x / b, a.y / b);
    public static bool operator ==(Double2 a, Double2 b) => a.Equals(b);
    public static bool operator !=(Double2 a, Double2 b) => !a.Equals(b);
    public static bool operator >(Double2 a, Double2 b) => a.CompareTo(b) > 0;
    public static bool operator <(Double2 a, Double2 b) => a.CompareTo(b) < 0;
    public static bool operator >=(Double2 a, Double2 b) => a == b || a > b;
    public static bool operator <=(Double2 a, Double2 b) => a == b || a < b;

    public static explicit operator Double2(Double3 val) => new(val.x, val.y);
    public static explicit operator Double2(Double4 val) => new(val.x, val.y);
    public static implicit operator Double2(Int2 val) => new(val.x, val.y);
    public static explicit operator Double2(Int3 val) => new(val.x, val.y);
    public static explicit operator Double2(Int4 val) => new(val.x, val.y);
    public static explicit operator Double2(Vert val) => new(val.position.x, val.position.y);
    public static implicit operator Double2(Fill<double> fill) => new(fill);
    public static implicit operator Double2(Fill<int> fill) => new(fill);
}
