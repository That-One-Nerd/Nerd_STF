namespace Nerd_STF.Mathematics;

public struct Angle : IAbsolute<Angle>, IAverage<Angle>, IClamp<Angle>, ICloneable,
    IComparable<Angle>, IEquatable<Angle>, ILerp<Angle, float>, IMax<Angle>, IMedian<Angle>,
    IMin<Angle>, IPresets2d<Angle>
{
    public static Angle Down => new(270);
    public static Angle Left => new(180);
    public static Angle Right => new(0);
    public static Angle Up => new(90);

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
        get => p_deg * 1.11111111111f;
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

    public Angle Bounded => new(Mathf.AbsoluteMod(p_deg, 360));
    public Angle Complimentary => Quarter - this;
    public Angle Supplementary => Half - this;
    public Angle Reflected => new Angle(-p_deg).Bounded;

    private float p_deg;

    public Angle(float value, Type valueType = Type.Degrees) => p_deg = valueType switch
    {
        Type.Degrees => value,
        Type.Gradians => value * 0.9f,
        Type.Normalized => value * 360,
        Type.Radians => value * Constants.RadToDeg,
        _ => throw new ArgumentException("Unknown type.", nameof(valueType)),
    };

    public static Angle FromVerts(Float3 endA, Float3 middleB, Float3 endC)
    {
        endA -= middleB;
        endC -= middleB;

        float dot = Float3.Dot(endA, endC);
        return Mathf.ArcCos(dot * endA.InverseMagnitude * endC.InverseMagnitude);
    }

    public static Angle Absolute(Angle val) => new(Mathf.Absolute(val.p_deg));
    public static Angle Average(params Angle[] vals) => new(Mathf.Average(SplitArray(Type.Degrees, vals)));
    public static Angle Ceiling(Angle val, Type type = Type.Degrees) =>
        new(Mathf.Ceiling(val.ValueFromType(type)), type);
    public static Angle Clamp(Angle val, Angle min, Angle max) => new(Mathf.Clamp(val.p_deg, min.p_deg, max.p_deg));
    public static Angle Floor(Angle val, Type type = Type.Degrees) =>
        new(Mathf.Floor(val.ValueFromType(type)), type);
    public static Angle Lerp(Angle a, Angle b, float t, bool clamp = true) =>
        new(Mathf.Lerp(a.p_deg, b.p_deg, t, clamp));
    public static Angle Max(params Angle[] vals) => Max(true, vals);
    public static Angle Max(bool useBounded, params Angle[] vals)
    {
        if (!useBounded) return new(Mathf.Max(SplitArray(Type.Degrees, vals)));

        Angle[] boundeds = new Angle[vals.Length];
        for (int i = 0; i < vals.Length; i++) boundeds[i] = vals[i].Bounded;
        return new(Mathf.Max(SplitArray(Type.Degrees, boundeds)));
    }
    public static Angle Median(params Angle[] vals) => new(Mathf.Median(SplitArray(Type.Degrees, vals)));
    public static Angle Min(params Angle[] vals) => Min(true, vals);
    public static Angle Min(bool useBounded, params Angle[] vals)
    {
        if (!useBounded) return new(Mathf.Min(SplitArray(Type.Degrees, vals)));

        Angle[] boundeds = new Angle[vals.Length];
        for (int i = 0; i < vals.Length; i++) boundeds[i] = vals[i].Bounded;
        return new(Mathf.Min(SplitArray(Type.Degrees, boundeds)));
    }
    public static Angle Round(Angle val, Type type = Type.Degrees) =>
        new(Mathf.Round(val.ValueFromType(type)), type);

    public static float[] SplitArray(Type outputType, params Angle[] vals)
    {
        float[] res = new float[vals.Length];
        for (int i = 0; i < vals.Length; i++) res[i] = vals[i].ValueFromType(outputType);
        return res;
    }

    public int CompareTo(Angle other) => p_deg.CompareTo(other.p_deg);
    public override bool Equals([NotNullWhen(true)] object? obj)
    {
        if (obj == null || obj.GetType() != typeof(Angle)) return base.Equals(obj);
        return Equals((Angle)obj);
    }
    public bool Equals(Angle other) => p_deg == other.p_deg;
    public override int GetHashCode() => Degrees.GetHashCode() ^ Gradians.GetHashCode() ^ Radians.GetHashCode();
    public override string ToString() => ToString(Type.Degrees);
    public string ToString(Type outputType) => outputType switch
    {
        Type.Degrees => p_deg + "°",
        Type.Gradians => Gradians + "grad",
        Type.Normalized => Normalized + "%",
        Type.Radians => Radians + "rad",
        _ => throw new ArgumentException("Unknown type.", nameof(outputType)),
    };

    public object Clone() => new Angle(p_deg);

    public Angle[] GetCoterminalAngles() => GetCoterminalAngles(Zero, Full);
    public Angle[] GetCoterminalAngles(Angle lowerBound, Angle upperBound)
    {
        List<Angle> values = new();

        Angle active = this;

        // A little bit redundant but it's a fairly easy way to guarentee
        // all coterminal angles are between the lower and upper bounds.
        // Definitely not the most efficient approach.
        while (active < upperBound) active += Full;
        active -= Full;

        while (active > lowerBound) active -= Full;
        active += Full;

        while (active < upperBound)
        {
            values.Add(active);
            active += Full;
        }

        return values.ToArray();
    }

    public float ValueFromType(Type type) => type switch
    {
        Type.Degrees => Degrees,
        Type.Gradians => Gradians,
        Type.Normalized => Normalized,
        Type.Radians => Radians,
        _ => throw new ArgumentException("Unknown type.", nameof(type))
    };

    public static Angle operator +(Angle a, Angle b) => new(a.p_deg + b.p_deg);
    public static Angle operator -(Angle a) => new(a.p_deg + 180);
    public static Angle operator -(Angle a, Angle b) => new(a.p_deg - b.p_deg);
    public static Angle operator *(Angle a, float b) => new(a.p_deg * b);
    public static Angle operator /(Angle a, float b) => new(a.p_deg / b);
    public static bool operator ==(Angle a, Angle b) => a.Equals(b);
    public static bool operator !=(Angle a, Angle b) => !a.Equals(b);
    public static bool operator >(Angle a, Angle b) => a.CompareTo(b) > 0;
    public static bool operator <(Angle a, Angle b) => a.CompareTo(b) < 0;
    public static bool operator >=(Angle a, Angle b) => a == b || a > b;
    public static bool operator <=(Angle a, Angle b) => a == b || a < b;

    public static implicit operator Angle((float val, Type type) obj) => new(obj.val, obj.type);

    public enum Type
    {
        Degrees,
        Gradians,
        Normalized,
        Radians,
    }
}
