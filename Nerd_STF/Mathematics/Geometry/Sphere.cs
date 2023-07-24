namespace Nerd_STF.Mathematics.Geometry;

public record class Sphere : IAverage<Sphere>, ICeiling<Sphere>, IClamp<Sphere>, IClosestTo<Float3>,
    IComparable<Sphere>, IComparable<float>, IContains<Float3>, IEquatable<Sphere>, IEquatable<float>, IFloor<Sphere>,
    IFromTuple<Sphere, (Float3 center, float radius)>, ILerp<Sphere, float>, IMax<Sphere>, IMedian<Sphere>,
    IMin<Sphere>, IRound<Sphere>, ISplittable<Sphere, (Float3[] centers, float[] radii)>
{
    public static Sphere Unit => new(Float3.Zero, 1);

    public Float3 center;
    public float radius;

    public float SurfaceArea => 4 * Constants.Pi * radius * radius;
    public float Volume => 4 / 3 * (Constants.Pi * radius * radius * radius);

    public static Sphere FromDiameter(Float3 a, Float3 b) => new(Float3.Average(a, b), (a - b).Magnitude / 2);
    public static Sphere FromRadius(Float3 center, Float3 radius) => new(center, (center - radius).Magnitude);

    public Sphere(Float3 center, float radius)
    {
        this.center = center;
        this.radius = radius;
    }
    public Sphere(float cX, float cY, float radius) : this(new Float3(cX, cY), radius) { }
    public Sphere(float cX, float cY, float cZ, float radius) : this(new Float3(cX, cY, cZ), radius) { }
    public Sphere(Fill<float> fill, float radius) : this(new Float3(fill), radius) { }
    public Sphere(Fill<float> fill) : this(new Float3(fill), fill(3)) { }
    public Sphere(Fill<int> fill, float radius) : this(new Float3(fill), radius) { }
    public Sphere(Fill<int> fill) : this(new Float3(fill), fill(3)) { }
    public Sphere(Fill<Float3> fill, float radius) : this(fill(0), radius) { }
    public Sphere(Fill<Float3> fillA, Fill<float> fillB) : this(fillA(0), fillB(0)) { }

    public static Sphere Average(params Sphere[] vals)
    {
        (Float3[] centers, float[] radii) = SplitArray(vals);
        return new(Float3.Average(centers), Mathf.Average(radii));
    }
    public static Sphere Ceiling(Sphere val) => new(Float3.Ceiling(val.center), Mathf.Ceiling(val.radius));
    public static Sphere Clamp(Sphere val, Sphere min, Sphere max) =>
        new(Float3.Clamp(val.center, min.center, max.center), Mathf.Clamp(val.radius, min.radius, max.radius));
    public static Sphere Floor(Sphere val) => new(Float3.Floor(val.center), Mathf.Floor(val.radius));
    public static Sphere Lerp(Sphere a, Sphere b, float t, bool clamp = true) =>
        new(Float3.Lerp(a.center, b.center, t, clamp), Mathf.Lerp(a.radius, b.radius, t, clamp));
    public static Sphere Max(params Sphere[] vals)
    {
        (Float3[] centers, float[] radii) = SplitArray(vals);
        return new(Float3.Max(centers), Mathf.Max(radii));
    }
    public static Sphere Median(params Sphere[] vals)
    {
        (Float3[] centers, float[] radii) = SplitArray(vals);
        return new(Float3.Median(centers), Mathf.Median(radii));
    }
    public static Sphere Min(params Sphere[] vals)
    {
        (Float3[] centers, float[] radii) = SplitArray(vals);
        return new(Float3.Min(centers), Mathf.Min(radii));
    }
    public static Sphere Round(Sphere val) => new(Float3.Round(val.center), Mathf.Round(val.radius));

    public static (Float3[] centers, float[] radii) SplitArray(params Sphere[] spheres)
    {
        Float3[] centers = new Float3[spheres.Length];
        float[] radii = new float[spheres.Length];
        for (int i = 0; i < spheres.Length; i++)
        {
            centers[i] = spheres[i].center;
            radii[i] = spheres[i].radius;
        }
        return (centers, radii);
    }

    [Obsolete("This method is a bit ambiguous. You should instead compare " + nameof(radius) + "es directly. " +
              "This method will be removed in Nerd_STF 2.5.0.")]
    public bool Equals(float other) => Volume == other;
    public virtual bool Equals(Sphere? other)
    {
        if (other is null) return false;
        return center == other.center && radius == other.radius;
    }
    public override int GetHashCode() => base.GetHashCode();

    public int CompareTo(Sphere? other)
    {
        if (other is null) return -1;
        return Volume.CompareTo(other.Volume);
    }
    [Obsolete("This method is a bit ambiguous. You should instead compare " + nameof(radius) + "es directly. " +
              "This method will be removed in Nerd_STF 2.5.0.")]
    public int CompareTo(float volume) => Volume.CompareTo(volume);

    public bool Contains(Float3 vert) => (center - vert).Magnitude <= radius;

    public Float3 ClosestTo(Float3 vert) => Contains(vert) ? vert : ((vert - center).Normalized * radius) + center;

    protected virtual bool PrintMembers(StringBuilder builder)
    {
        builder.Append("Center = ");
        builder.Append(builder);
        builder.Append(", Radius = ");
        builder.Append(radius);
        return true;
    }

    public static Sphere operator +(Sphere a, Sphere b) => new(a.center + b.center, a.radius + b.radius);
    public static Sphere operator +(Sphere a, Float3 b) => new(a.center + b, a.radius);
    public static Sphere operator +(Sphere a, float b) => new(a.center, a.radius + b);
    public static Sphere operator -(Sphere a, Sphere b) => new(a.center + b.center, a.radius + b.radius);
    public static Sphere operator -(Sphere a, Float3 b) => new(a.center + b, a.radius);
    public static Sphere operator -(Sphere a, float b) => new(a.center, a.radius + b);
    public static Sphere operator *(Sphere a, Sphere b) => new(a.center * b.center, a.radius * b.radius);
    public static Sphere operator *(Sphere a, float b) => new(a.center * b, a.radius * b);
    public static Sphere operator /(Sphere a, Sphere b) => new(a.center * b.center, a.radius * b.radius);
    public static Sphere operator /(Sphere a, float b) => new(a.center * b, a.radius * b);
    [Obsolete("This method is a bit ambiguous. You should instead compare " + nameof(radius) + "es directly. " +
              "This method will be removed in Nerd_STF 2.5.0.")]
    public static bool operator ==(Sphere a, float b) => a.Equals(b);
    [Obsolete("This method is a bit ambiguous. You should instead compare " + nameof(radius) + "es directly. " +
              "This method will be removed in Nerd_STF 2.5.0.")]
    public static bool operator !=(Sphere a, float b) => !a.Equals(b);
    [Obsolete("This method is a bit ambiguous. You should instead compare " + nameof(radius) + "es directly. " +
              "This method will be removed in Nerd_STF 2.5.0.")]
    public static bool operator >(Sphere a, Sphere b) => a.CompareTo(b) > 0;
    [Obsolete("This method is a bit ambiguous. You should instead compare " + nameof(radius) + "es directly. " +
              "This method will be removed in Nerd_STF 2.5.0.")]
    public static bool operator <(Sphere a, Sphere b) => a.CompareTo(b) < 0;
    [Obsolete("This method is a bit ambiguous. You should instead compare " + nameof(radius) + "es directly. " +
              "This method will be removed in Nerd_STF 2.5.0.")]
    public static bool operator >(Sphere a, float b) => a.CompareTo(b) > 0;
    [Obsolete("This method is a bit ambiguous. You should instead compare " + nameof(radius) + "es directly. " +
              "This method will be removed in Nerd_STF 2.5.0.")]
    public static bool operator <(Sphere a, float b) => a.CompareTo(b) < 0;
    [Obsolete("This method is a bit ambiguous. You should instead compare " + nameof(radius) + "es directly. " +
              "This method will be removed in Nerd_STF 2.5.0.")]
    public static bool operator >=(Sphere a, Sphere b) => a > b || a == b;
    [Obsolete("This method is a bit ambiguous. You should instead compare " + nameof(radius) + "es directly. " +
              "This method will be removed in Nerd_STF 2.5.0.")]
    public static bool operator <=(Sphere a, Sphere b) => a < b || a == b;
    [Obsolete("This method is a bit ambiguous. You should instead compare " + nameof(radius) + "es directly. " +
              "This method will be removed in Nerd_STF 2.5.0.")]
    public static bool operator >=(Sphere a, float b) => a > b || a == b;
    [Obsolete("This method is a bit ambiguous. You should instead compare " + nameof(radius) + "es directly. " +
              "This method will be removed in Nerd_STF 2.5.0.")]
    public static bool operator <=(Sphere a, float b) => a < b || a == b;

    public static implicit operator Sphere((Float3 center, float radius) val) =>
        new(val.center, val.radius);
}
