namespace Nerd_STF.Mathematics.Geometry;

public record class Sphere : IAverage<Sphere>, ICeiling<Sphere>, IClamp<Sphere>, IClosestTo<Vert>,
    IComparable<Sphere>, IComparable<float>, IContains<Vert>, IEquatable<Sphere>, IEquatable<float>, IFloor<Sphere>,
    IFromTuple<Sphere, (Vert center, float radius)>, ILerp<Sphere, float>, IMax<Sphere>, IMedian<Sphere>,
    IMin<Sphere>, IRound<Sphere>, ISplittable<Sphere, (Vert[] centers, float[] radii)>
{
    public static Sphere Unit => new(Vert.Zero, 1);

    public Vert center;
    public float radius;

    public float SurfaceArea => 4 * Constants.Pi * radius * radius;
    public float Volume => 4 / 3 * (Constants.Pi * radius * radius * radius);

    public static Sphere FromDiameter(Vert a, Vert b) => new(Vert.Average(a, b), (a - b).Magnitude / 2);
    public static Sphere FromRadius(Vert center, Vert radius) => new(center, (center - radius).Magnitude);

    public Sphere(Vert center, float radius)
    {
        this.center = center;
        this.radius = radius;
    }
    public Sphere(float cX, float cY, float radius) : this(new Vert(cX, cY), radius) { }
    public Sphere(float cX, float cY, float cZ, float radius) : this(new Vert(cX, cY, cZ), radius) { }
    public Sphere(Fill<float> fill, float radius) : this(new Vert(fill), radius) { }
    public Sphere(Fill<float> fill) : this(new Vert(fill), fill(3)) { }
    public Sphere(Fill<int> fill, float radius) : this(new Vert(fill), radius) { }
    public Sphere(Fill<int> fill) : this(new Vert(fill), fill(3)) { }
    public Sphere(Fill<Vert> fill, float radius) : this(fill(0), radius) { }
    public Sphere(Fill<Vert> fillA, Fill<float> fillB) : this(fillA(0), fillB(0)) { }

    public static Sphere Average(params Sphere[] vals)
    {
        (Vert[] centers, float[] radii) = SplitArray(vals);
        return new(Vert.Average(centers), Mathf.Average(radii));
    }
    public static Sphere Ceiling(Sphere val) => new(Vert.Ceiling(val.center), Mathf.Ceiling(val.radius));
    public static Sphere Clamp(Sphere val, Sphere min, Sphere max) =>
        new(Vert.Clamp(val.center, min.center, max.center), Mathf.Clamp(val.radius, min.radius, max.radius));
    public static Sphere Floor(Sphere val) => new(Vert.Floor(val.center), Mathf.Floor(val.radius));
    public static Sphere Lerp(Sphere a, Sphere b, float t, bool clamp = true) =>
        new(Vert.Lerp(a.center, b.center, t, clamp), Mathf.Lerp(a.radius, b.radius, t, clamp));
    public static Sphere Max(params Sphere[] vals)
    {
        (Vert[] centers, float[] radii) = SplitArray(vals);
        return new(Vert.Max(centers), Mathf.Max(radii));
    }
    public static Sphere Median(params Sphere[] vals)
    {
        (Vert[] centers, float[] radii) = SplitArray(vals);
        return new(Vert.Median(centers), Mathf.Median(radii));
    }
    public static Sphere Min(params Sphere[] vals)
    {
        (Vert[] centers, float[] radii) = SplitArray(vals);
        return new(Vert.Min(centers), Mathf.Min(radii));
    }
    public static Sphere Round(Sphere val) => new(Vert.Round(val.center), Mathf.Round(val.radius));

    public static (Vert[] centers, float[] radii) SplitArray(params Sphere[] spheres)
    {
        Vert[] centers = new Vert[spheres.Length];
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

    public bool Contains(Vert vert) => (center - vert).Magnitude <= radius;

    public Vert ClosestTo(Vert vert) => Contains(vert) ? vert : ((vert - center).Normalized * radius) + center;

    protected virtual bool PrintMembers(StringBuilder builder)
    {
        builder.Append("Center = ");
        builder.Append(builder);
        builder.Append(", Radius = ");
        builder.Append(radius);
        return true;
    }

    public static Sphere operator +(Sphere a, Sphere b) => new(a.center + b.center, a.radius + b.radius);
    public static Sphere operator +(Sphere a, Vert b) => new(a.center + b, a.radius);
    public static Sphere operator +(Sphere a, float b) => new(a.center, a.radius + b);
    public static Sphere operator -(Sphere a, Sphere b) => new(a.center + b.center, a.radius + b.radius);
    public static Sphere operator -(Sphere a, Vert b) => new(a.center + b, a.radius);
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

    public static implicit operator Sphere((Vert center, float radius) val) =>
        new(val.center, val.radius);
}
