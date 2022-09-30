namespace Nerd_STF.Mathematics.NumberSystems;

public struct Complex : ICloneable, IComparable<Complex>, IEquatable<Complex>, IGroup<float>
{
    public static Complex Down => new(0, -1);
    public static Complex Left => new(-1, 0);
    public static Complex Right => new(1, 0);
    public static Complex Up => new(0, 1);

    public static Complex One => new(1, 1);
    public static Complex Zero => new(0, 0);

    public Complex Conjugate => new(u, -i);
    public Complex Inverse => Conjugate / (u * u + i * i);
    public float Magnitude => Mathf.Sqrt(u * u + i * i);
    public Complex Normalized => this * Mathf.InverseSqrt(u * u + i * i);

    public float u, i;

    public Complex(float all) : this(all, all) { }
    public Complex(float u, float i)
    {
        this.u = u;
        this.i = i;
    }
    public Complex(Fill<float> fill) : this(fill(0), fill(1)) { }
    public Complex(Fill<int> fill) : this(fill(0), fill(1)) { }

    public float this[int index]
    {
        get => index switch
        {
            0 => u,
            1 => i,
            _ => throw new IndexOutOfRangeException(nameof(index)),
        };
        set
        {
            switch (index)
            {
                case 0:
                    u = value;
                    break;

                case 1:
                    i = value;
                    break;

                default: throw new IndexOutOfRangeException(nameof(index));
            }
        }
    }

    public static Complex Absolute(Complex val) => Float2.Absolute(val);
    public static Complex Average(params Complex[] vals)
    {
        List<Float2> floats = new();
        foreach (Complex c in vals) floats.Add(c);
        return Float2.Average(floats.ToArray());
    }
    public static Complex Ceiling(Complex val) => Float2.Ceiling(val);
    public static Complex Clamp(Complex val, Complex min, Complex max) => Float2.Clamp(val, min, max);
    public static Complex ClampMagnitude(Complex val, float minMag, float maxMag) =>
        Float2.ClampMagnitude(val, minMag, maxMag);
    public static Complex Divide(Complex num, params Complex[] vals)
    {
        List<Float2> floats = new();
        foreach (Complex c in vals) floats.Add(c);
        return Float2.Divide(num, floats.ToArray());
    }
    public static float Dot(Complex a, Complex b) => Float2.Dot(a, b);
    public static float Dot(params Complex[] vals)
    {
        List<Float2> floats = new();
        foreach (Complex c in vals) floats.Add(c);
        return Float2.Dot(floats.ToArray());
    }
    public static Complex Floor(Complex val) => Float2.Floor(val);
    public static Complex Lerp(Complex a, Complex b, float t, bool clamp = true) => Float2.Lerp(a, b, t, clamp);
    public static Complex Median(params Complex[] vals)
    {
        List<Float2> floats = new();
        foreach (Complex c in vals) floats.Add(c);
        return Float2.Median(floats.ToArray());
    }
    public static Complex Max(params Complex[] vals)
    {
        List<Float2> floats = new();
        foreach (Complex c in vals) floats.Add(c);
        return Float2.Max(floats.ToArray());
    }
    public static Complex Min(params Complex[] vals)
    {
        List<Float2> floats = new();
        foreach (Complex c in vals) floats.Add(c);
        return Float2.Min(floats.ToArray());
    }
    public static Complex Product(params Complex[] vals)
    {
        List<Float2> floats = new();
        foreach (Complex c in vals) floats.Add(c);
        return Float2.Product(floats.ToArray());
    }
    public static Complex Round(Complex val) => Float2.Round(val);
    public static Complex Subtract(Complex num, params Complex[] vals)
    {
        List<Float2> floats = new();
        foreach (Complex c in vals) floats.Add(c);
        return Float2.Subtract(num, floats.ToArray());
    }
    public static Complex Sum(params Complex[] vals)
    {
        List<Float2> floats = new();
        foreach (Complex c in vals) floats.Add(c);
        return Float2.Sum(floats.ToArray());
    }

    public static (float[] Us, float[] Is) SplitArray(params Complex[] vals)
    {
        float[] Us = new float[vals.Length], Is = new float[vals.Length];
        for (int i = 0; i < vals.Length; i++)
        {
            Us[i] = vals[i].u;
            Is[i] = vals[i].i;
        }
        return (Us, Is);
    }

    public Angle GetAngle() => Mathf.ArcTan(i / u);

    public int CompareTo(Complex other) => Magnitude.CompareTo(other.Magnitude);
    public override bool Equals([NotNullWhen(true)] object? obj)
    {
        if (obj == null || obj.GetType() != typeof(Complex)) return base.Equals(obj);
        return Equals((Complex)obj);
    }
    public bool Equals(Complex other) => u == other.u && i == other.i;
    public override int GetHashCode() => u.GetHashCode() ^ i.GetHashCode();
    public override string ToString() => ToString((string?)null);
    public string ToString(string? provider) =>
        u.ToString(provider) + (i >= 0 ? " + " : " - ") + Mathf.Absolute(i).ToString(provider) + "i";
    public string ToString(IFormatProvider provider) =>
        u.ToString(provider) + (i >= 0 ? " + " : " - ") + Mathf.Absolute(i).ToString(provider) + "i";

    public object Clone() => new Complex(u, i);

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    public IEnumerator<float> GetEnumerator()
    {
        yield return u;
        yield return i;
    }

    public float[] ToArray() => new[] { u, i };
    public Fill<float> ToFill()
    {
        Complex @this = this;
        return i => @this[i];
    }
    public List<float> ToList() => new() { u, i };

    public Vector2d ToVector() => ((Float2)this).ToVector();

    public static Complex operator +(Complex a, Complex b) => new(a.u + b.u, a.i + b.i);
    public static Complex operator -(Complex c) => new(-c.u, -c.i);
    public static Complex operator -(Complex a, Complex b) => new(a.u - b.u, a.i - b.i);
    public static Complex operator *(Complex a, Complex b) => new(a.u * b.u - a.i * b.i, a.u * b.i + a.i * b.u);
    public static Complex operator *(Complex a, float b) => new(a.u * b, a.i * b);
    public static Complex operator *(Complex a, Matrix b) => (Complex)((Matrix)a * b);
    public static Complex operator /(Complex a, Complex b) => a * b.Inverse;
    public static Complex operator /(Complex a, float b) => new(a.u / b, a.i / b);
    public static Complex operator /(Complex a, Matrix b) => (Complex)((Matrix)a / b);
    public static bool operator ==(Complex a, Complex b) => a.Equals(b);
    public static bool operator !=(Complex a, Complex b) => !a.Equals(b);
    public static bool operator >(Complex a, Complex b) => a.CompareTo(b) > 0;
    public static bool operator <(Complex a, Complex b) => a.CompareTo(b) < 0;
    public static bool operator >=(Complex a, Complex b) => a == b || a > b;
    public static bool operator <=(Complex a, Complex b) => a == b || a < b;

    public static explicit operator Complex(Quaternion val) => new(val.u, val.i);
    public static implicit operator Complex(Float2 val) => new(val.x, val.y);
    public static explicit operator Complex(Float3 val) => new(val.x, val.y);
    public static explicit operator Complex(Float4 val) => new(val.x, val.y);
    public static implicit operator Complex(Int2 val) => new(val.x, val.y);
    public static explicit operator Complex(Int3 val) => new(val.x, val.y);
    public static explicit operator Complex(Int4 val) => new(val.x, val.y);
    public static explicit operator Complex(Matrix m) => new(m[0, 0], m[1, 0]);
    public static explicit operator Complex(Vector2d val) => val.ToXYZ();
    public static explicit operator Complex(Vert val) => new(val.position.x, val.position.y);
    public static implicit operator Complex(Fill<float> fill) => new(fill);
    public static implicit operator Complex(Fill<int> fill) => new(fill);
}
