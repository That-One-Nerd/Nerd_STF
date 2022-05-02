namespace Nerd_STF.Mathematics;

public struct Int2 : ICloneable, IComparable<Int2>, IEquatable<Int2>, IGroup<int>
{
    public static Int2 Down => new(0, -1);
    public static Int2 Left => new(-1, 0);
    public static Int2 Right => new(1, 0);
    public static Int2 Up => new(0, 1);

    public static Int2 One => new(1, 1);
    public static Int2 Zero => new(0, 0);

    public float Magnitude => Mathf.Sqrt(x * x + y * y);
    public Int2 Normalized => (Int2)((Float2)this / Magnitude);

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
    public static Int2 Divide(Int2 num, params Int2[] vals)
    {
        foreach (Int2 d in vals) num /= d;
        return num;
    }
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
        int index = Mathf.Average(0, vals.Length - 1);
        Int2 valA = vals[Mathf.Floor(index)], valB = vals[Mathf.Ceiling(index)];
        return Average(valA, valB);
    }
    public static Int2 Max(params Int2[] vals)
    {
        if (vals.Length < 1) return Zero;
        Int2 val = vals[0];
        foreach (Int2 d in vals) val = d > val ? d : val;
        return val;
    }
    public static Int2 Min(params Int2[] vals)
    {
        if (vals.Length < 1) return Zero;
        Int2 val = vals[0];
        foreach (Int2 d in vals) val = d < val ? d : val;
        return val;
    }
    public static Int2 Multiply(params Int2[] vals)
    {
        if (vals.Length < 1) return Zero;
        Int2 val = One;
        foreach (Int2 d in vals) val *= d;
        return val;
    }
    public static Int2 Subtract(Int2 num, params Int2[] vals)
    {
        foreach (Int2 d in vals) num -= d;
        return num;
    }
    public static Int2 Sum(params Int2[] vals)
    {
        Int2 val = Zero;
        foreach (Int2 d in vals) val += d;
        return val;
    }

    public int CompareTo(Int2 other) => Magnitude.CompareTo(other.Magnitude);
    public override bool Equals([NotNullWhen(true)] object? obj)
    {
        if (obj == null || obj.GetType() != typeof(Int2)) return false;
        return Equals((Int2)obj);
    }
    public bool Equals(Int2 other) => x == other.x && y == other.y;
    public override int GetHashCode() => x.GetHashCode() ^ y.GetHashCode();
    public override string ToString() => ToString((string?)null);
    public string ToString(string? provider) =>
        "X: " + x.ToString(provider) + " Y: " + y.ToString(provider);
    public string ToString(IFormatProvider provider) =>
        "X: " + x.ToString(provider) + " Y: " + y.ToString(provider);

    public object Clone() => new Int2(x, y);

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    public IEnumerator<int> GetEnumerator()
    {
        yield return x;
        yield return y;
    }

    public int[] ToArray() => new[] { x, y };
    public List<int> ToList() => new() { x, y };

    public static Int2 operator +(Int2 a, Int2 b) => new(a.x + b.x, a.y + b.y);
    public static Int2 operator -(Int2 i) => new(-i.x, -i.y);
    public static Int2 operator -(Int2 a, Int2 b) => new(a.x - b.x, a.y - b.y);
    public static Int2 operator *(Int2 a, Int2 b) => new(a.x * b.x, a.y * b.y);
    public static Int2 operator *(Int2 a, int b) => new(a.x * b, a.y * b);
    public static Int2 operator /(Int2 a, Int2 b) => new(a.x / b.x, a.y / b.y);
    public static Int2 operator /(Int2 a, int b) => new(a.x / b, a.y / b);
    public static Int2 operator &(Int2 a, Int2 b) => new(a.x & b.x, a.y & b.y);
    public static Int2 operator |(Int2 a, Int2 b) => new(a.x | b.x, a.y | b.y);
    public static Int2 operator ^(Int2 a, Int2 b) => new(a.x ^ b.x, a.y ^ b.y);
    public static bool operator ==(Int2 a, Int2 b) => a.Equals(b);
    public static bool operator !=(Int2 a, Int2 b) => !a.Equals(b);
    public static bool operator >(Int2 a, Int2 b) => a.CompareTo(b) > 0;
    public static bool operator <(Int2 a, Int2 b) => a.CompareTo(b) < 0;
    public static bool operator >=(Int2 a, Int2 b) => a == b || a > b;
    public static bool operator <=(Int2 a, Int2 b) => a == b || a < b;

    public static explicit operator Int2(Float2 val) => new((int)val.x, (int)val.y);
    public static explicit operator Int2(Float3 val) => new((int)val.x, (int)val.y);
    public static explicit operator Int2(Float4 val) => new((int)val.x, (int)val.y);
    public static explicit operator Int2(Int3 val) => new(val.x, val.y);
    public static explicit operator Int2(Int4 val) => new(val.x, val.y);
    public static explicit operator Int2(Vert val) => new((int)val.position.x, (int)val.position.y);
    public static implicit operator Int2(Fill<int> fill) => new(fill);
}
