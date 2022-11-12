namespace Nerd_STF.Mathematics.Geometry;

public struct Sphere : ICloneable, IClosest<Vert>, IComparable<Sphere>, IComparable<float>, IContainer<Vert>,
    IEquatable<Sphere>, IEquatable<float>
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

    public override bool Equals([NotNullWhen(true)] object? obj)
    {
        if (obj == null) return base.Equals(obj);
        Type type = obj.GetType();
        if (type == typeof(Sphere)) return Equals((Sphere)obj);
        if (type == typeof(float)) return Equals((float)obj);
        return base.Equals(obj);
    }
    public bool Equals(float other) => Volume == other;
    public bool Equals(Sphere other) => center == other.center && radius == other.radius;
    public override int GetHashCode() => center.GetHashCode() ^ radius.GetHashCode();
    public override string ToString() => ToString((string?)null);
    public string ToString(string? provider) => "Center: " + center.ToString(provider)
        + " Radius: " + radius.ToString(provider);
    public string ToString(IFormatProvider provider) => "Center: " + center.ToString(provider)
        + " Radius: " + radius.ToString(provider);

    public object Clone() => new Sphere(center, radius);

    public int CompareTo(Sphere sphere) => Volume.CompareTo(sphere.Volume);
    public int CompareTo(float volume) => Volume.CompareTo(volume);

    public bool Contains(Vert vert) => (center - vert).Magnitude <= radius;

    public Vert ClosestTo(Vert vert) => Contains(vert) ? vert : ((vert - center).Normalized * radius) + center;

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
    public static bool operator ==(Sphere a, Sphere b) => a.Equals(b);
    public static bool operator !=(Sphere a, Sphere b) => !a.Equals(b);
    public static bool operator ==(Sphere a, float b) => a.Equals(b);
    public static bool operator !=(Sphere a, float b) => !a.Equals(b);
    public static bool operator >(Sphere a, Sphere b) => a.CompareTo(b) > 0;
    public static bool operator <(Sphere a, Sphere b) => a.CompareTo(b) < 0;
    public static bool operator >(Sphere a, float b) => a.CompareTo(b) > 0;
    public static bool operator <(Sphere a, float b) => a.CompareTo(b) < 0;
    public static bool operator >=(Sphere a, Sphere b) => a > b || a == b;
    public static bool operator <=(Sphere a, Sphere b) => a < b || a == b;
    public static bool operator >=(Sphere a, float b) => a > b || a == b;
    public static bool operator <=(Sphere a, float b) => a < b || a == b;
}
