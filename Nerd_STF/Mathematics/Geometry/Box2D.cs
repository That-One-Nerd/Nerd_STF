namespace Nerd_STF.Mathematics.Geometry;

public struct Box2D : ICloneable, IContainer<Vert>, IEquatable<Box2D>
{
    public static Box2D Unit => new(Vert.Zero, Float2.One);

    public Vert MaxVert
    {
        get => center + (size / 2);
        set
        {
            Vert diff = center - value;
            size = (Float2)diff.position * 2;
        }
    }
    public Vert MinVert
    {
        get => center - (size / 2);
        set
        {
            Vert diff = center + value;
            size = (Float2)diff.position * 2;
        }
    }

    public float Area => size.x * size.y;
    public float Perimeter => size.x * 2 + size.y * 2;

    public Vert center;
    public Float2 size;

    public Box2D(Vert min, Vert max) : this(Vert.Average(min, max), (Float2)(min - max)) { }
    public Box2D(Vert center, Float2 size)
    {
        this.center = center;
        this.size = size;
    }
    public Box2D(Fill<float> fill) : this(fill, new Float2(fill(3), fill(4))) { }

    public float this[int index]
    {
        get => size[index];
        set => size[index] = value;
    }

    public static Box2D Absolute(Box2D val) => new(Vert.Absolute(val.MinVert), Vert.Absolute(val.MaxVert));
    public static Box2D Average(params Box2D[] vals)
    {
        (Vert[] centers, Float2[] sizes) = SplitArray(vals);
        return new(Vert.Average(centers), Float2.Average(sizes));
    }
    public static Box2D Ceiling(Box2D val) => new(Vert.Ceiling(val.center), Float2.Ceiling(val.size));
    public static Box2D Clamp(Box2D val, Box2D min, Box2D max) =>
        new(Vert.Clamp(val.center, min.center, max.center), Float2.Clamp(val.size, min.size, max.size));
    public static Box2D Floor(Box2D val) => new(Vert.Floor(val.center), Float2.Floor(val.size));
    public static Box2D Lerp(Box2D a, Box2D b, float t, bool clamp = true) =>
        new(Vert.Lerp(a.center, b.center, t, clamp), Float2.Lerp(a.size, b.size, t, clamp));
    public static Box2D Median(params Box2D[] vals)
    {
        (Vert[] verts, Float2[] sizes) = SplitArray(vals);
        return new(Vert.Median(verts), Float2.Median(sizes));
    }
    public static Box2D Max(params Box2D[] vals)
    {
        (Vert[] verts, Float2[] sizes) = SplitArray(vals);
        return new(Vert.Max(verts), Float2.Max(sizes));
    }
    public static Box2D Min(params Box2D[] vals)
    {
        (Vert[] verts, Float2[] sizes) = SplitArray(vals);
        return new(Vert.Min(verts), Float2.Min(sizes));
    }
    public static (Vert[] centers, Float2[] sizes) SplitArray(params Box2D[] vals)
    {
        Vert[] centers = new Vert[vals.Length];
        Float2[] sizes = new Float2[vals.Length];

        for (int i = 0; i < vals.Length; i++)
        {
            centers[i] = vals[i].center;
            sizes[i] = vals[i].size;
        }

        return (centers, sizes);
    }

    public override bool Equals([NotNullWhen(true)] object? obj)
    {
        if (obj == null || obj.GetType() != typeof(Box2D)) return base.Equals(obj);
        return Equals((Box2D)obj);
    }
    public bool Equals(Box2D other) => center == other.center && size == other.size;
    public override int GetHashCode() => center.GetHashCode() ^ size.GetHashCode();
    public override string ToString() => ToString((string?)null);
    public string ToString(string? provider) =>
        "Min: " + MinVert.ToString(provider) + " Max: " + MaxVert.ToString(provider);
    public string ToString(IFormatProvider provider) =>
        "Min: " + MinVert.ToString(provider) + " Max: " + MaxVert.ToString(provider);

    public bool Contains(Vert vert)
    {
        Float2 diff = Float2.Absolute((Float2)(center - vert));
        return diff.x <= size.x && diff.y <= size.y;
    }

    public object Clone() => new Box2D(center, size);

    public static Box2D operator +(Box2D a, Vert b) => new(a.center + b, a.size);
    public static Box2D operator +(Box2D a, Float2 b) => new(a.center, a.size + b);
    public static Box2D operator -(Box2D b) => new(-b.MaxVert, -b.MinVert);
    public static Box2D operator -(Box2D a, Vert b) => new(a.center - b, a.size);
    public static Box2D operator -(Box2D a, Float2 b) => new(a.center, a.size - b);
    public static Box2D operator *(Box2D a, float b) => new(a.center * b, a.size * b);
    public static Box2D operator *(Box2D a, Float2 b) => new(a.center, a.size * b);
    public static Box2D operator /(Box2D a, float b) => new(a.center / b, a.size / b);
    public static Box2D operator /(Box2D a, Float2 b) => new(a.center, a.size / b);
    public static bool operator ==(Box2D a, Box2D b) => a.Equals(b);
    public static bool operator !=(Box2D a, Box2D b) => !a.Equals(b);

    public static implicit operator Box2D(Fill<float> fill) => new(fill);
    public static explicit operator Box2D(Box3D box) => new(box.center, (Float2)box.size);
}
