namespace Nerd_STF.Mathematics;

public struct Angle : ICloneable, IComparable<Angle>, IEquatable<Angle>
{
    public static Angle Full => new(360);
    public static Angle Half => new(180);
    public static Angle One => new(1);
    public static Angle Quarter => new(90);
    public static Angle Zero => new(0);

    public float Degrees
    {
        get => p_deg;
        set => p_deg = value;
    }
    public float Gradians
    {
        get => p_deg * 1.11111111111f; // Reciprocal of 9/10 as a constant (10/9)
        set => p_deg = value * 0.9f;
    }
    public float Normalized
    {
        get => p_deg / 360;
        set => p_deg = value * 360;
    }
    public float Radians
    {
        get => p_deg * Constants.DegToRad;
        set => p_deg = value * Constants.RadToDeg;
    }

    public Angle Bounded => new(p_deg % 360);

    private float p_deg;

    public Angle(float value, Type valueType = Type.Degrees)
    {
        p_deg = valueType switch
        {
            Type.Degrees => value,
            Type.Gradians => value * 0.9f,
            Type.Normalized => value * 360,
            Type.Radians => value * Constants.RadToDeg,
            _ => throw new ArgumentException("Unknown type.", nameof(valueType)),
        };
    }

    public static Angle Absolute(Angle val) => new(Mathf.Absolute(val.p_deg));
    public static Angle Average(params Angle[] vals) => new(Mathf.Average(SplitArray(Type.Degrees, vals)));
    public static Angle Ceiling(Angle val, Type type = Type.Degrees) => new(Mathf.Ceiling(val.ValueFromType(type)));
    public static Angle Clamp(Angle val, Angle min, Angle max) => new(Mathf.Clamp(val.p_deg, min.p_deg, max.p_deg));
    public static Angle Floor(Angle val, Type type = Type.Degrees) => new(Mathf.Floor(val.ValueFromType(type)));
    public static Angle Lerp(Angle a, Angle b, float t, bool clamp = true) =>
        new(Mathf.Lerp(a.p_deg, b.p_deg, t, clamp));
    public static Angle Max(params Angle[] vals) => new(Mathf.Max(SplitArray(Type.Degrees, vals)));
    public static Angle Median(params Angle[] vals) => new(Mathf.Median(SplitArray(Type.Degrees, vals)));
    public static Angle Min(params Angle[] vals) => new(Mathf.Min(SplitArray(Type.Degrees, vals)));

    public static float[] SplitArray(Type outputType, params Angle[] vals)
    {
        float[] res = new float[vals.Length];
        for (int i = 0; i < vals.Length; i++)
        {
            res[i] = outputType switch
            {
                Type.Degrees => vals[i].Degrees,
                Type.Gradians => vals[i].Gradians,
                Type.Normalized => vals[i].Normalized,
                Type.Radians => vals[i].Radians,
                _ => throw new ArgumentException("Unknown type.", nameof(outputType)),
            };
        }
        return res;
    }

    public int CompareTo(Angle other) => p_deg.CompareTo(other.p_deg);
    public override bool Equals([NotNullWhen(true)] object? obj)
    {
        if (obj == null || obj.GetType() != typeof(Angle)) return false;
        return Equals((Angle)obj);
    }
    public bool Equals(Angle other) => p_deg == other.p_deg;
    public override int GetHashCode() => Degrees.GetHashCode() ^ Gradians.GetHashCode() ^ Radians.GetHashCode();
    public override string ToString() => ToString((string?)null);
    public string ToString(Type outputType) => ToString((string?)null, outputType);
    public string ToString(string? provider, Type outputType = Type.Degrees) => outputType switch
    {
        Type.Degrees => p_deg.ToString(provider) + "°",
        Type.Gradians => Gradians.ToString(provider) + "grad",
        Type.Normalized => Normalized.ToString(provider) + "%",
        Type.Radians => Radians.ToString(provider) + "rad",
        _ => throw new ArgumentException("Unknown type.", nameof(outputType)),
    };
    public string ToString(IFormatProvider provider, Type outputType = Type.Degrees) => outputType switch
    {
        Type.Degrees => p_deg.ToString(provider) + "°",
        Type.Gradians => Gradians.ToString(provider) + "grad",
        Type.Normalized => Normalized.ToString(provider) + "%",
        Type.Radians => Radians.ToString(provider) + "rad",
        _ => throw new ArgumentException("Unknown type.", nameof(outputType)),
    };

    public object Clone() => new Angle(p_deg);

    public float ValueFromType(Type type) => type switch
    {
        Type.Degrees => Degrees,
        Type.Gradians => Gradians,
        Type.Normalized => Normalized,
        Type.Radians => Radians,
        _ => throw new ArgumentException("Unknown type.", nameof(type)),
    };

    public static Angle operator +(Angle a, Angle b) => new(a.p_deg + b.p_deg);
    public static Angle operator -(Angle a) => new(-a.p_deg);
    public static Angle operator -(Angle a, Angle b) => new(a.p_deg - b.p_deg);
    public static Angle operator *(Angle a, Angle b) => new(a.p_deg * b.p_deg);
    public static Angle operator *(Angle a, float b) => new(a.p_deg * b);
    public static Angle operator /(Angle a, Angle b) => new(a.p_deg / b.p_deg);
    public static Angle operator /(Angle a, float b) => new(a.p_deg / b);
    public static bool operator ==(Angle a, Angle b) => a.Equals(b);
    public static bool operator !=(Angle a, Angle b) => !a.Equals(b);
    public static bool operator >(Angle a, Angle b) => a.CompareTo(b) > 0;
    public static bool operator <(Angle a, Angle b) => a.CompareTo(b) < 0;
    public static bool operator >=(Angle a, Angle b) => a == b || a > b;
    public static bool operator <=(Angle a, Angle b) => a == b || a < b;

    public enum Type
    {
        Degrees,
        Gradians,
        Normalized,
        Radians,
    }
}
