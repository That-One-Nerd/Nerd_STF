namespace Nerd_STF.Mathematics.Geometry;

public struct Box3D : ICloneable, IContainer<Vert>, IEquatable<Box3D>
{
    public static Box3D Unit => new(Vert.Zero, Double3.One);

    public Vert MaxVert
    {
        get => center + (Vert)(size / 2);
        set
        {
            Vert diff = center - value;
            size = diff.position * 2;
        }
    }
    public Vert MinVert
    {
        get => center - (Vert)(size / 2);
        set
        {
            Vert diff = center + value;
            size = diff.position * 2;
        }
    }

    public double Area => size.x * size.y * size.z;
    public double Perimeter => size.x * 2 + size.y * 2 + size.z * 2;

    public Vert center;
    public Double3 size;

    public Box3D(Box2D box) : this(box.center, (Double3)box.size) { }
    public Box3D(Vert min, Vert max) : this(Vert.Average(min, max), (Double3)(min - max)) { }
    public Box3D(Vert center, Double3 size)
    {
        this.center = center;
        this.size = size;
    }
    public Box3D(Fill<double> fill) : this(fill, new Double3(fill(3), fill(4), fill(5))) { }

    public double this[int index]
    {
        get => size[index];
        set => size[index] = value;
    }

    public static Box3D Absolute(Box3D val) => new(Vert.Absolute(val.MinVert), Vert.Absolute(val.MaxVert));
    public static Box3D Average(params Box3D[] vals)
    {
        (Vert[] centers, Double3[] sizes) = SplitArray(vals);
        return new(Vert.Average(centers), Double3.Average(sizes));
    }
    public static Box3D Ceiling(Box3D val) => new(Vert.Ceiling(val.center), Double3.Ceiling(val.size));
    public static Box3D Clamp(Box3D val, Box3D min, Box3D max) =>
        new(Vert.Clamp(val.center, min.center, max.center), Double3.Clamp(val.size, min.size, max.size));
    public static Box3D Floor(Box3D val) => new(Vert.Floor(val.center), Double3.Floor(val.size));
    public static Box3D Lerp(Box3D a, Box3D b, float t, bool clamp = true) =>
        new(Vert.Lerp(a.center, b.center, t, clamp), Double3.Lerp(a.size, b.size, t, clamp));
    public static Box3D Median(params Box3D[] vals)
    {
        (Vert[] verts, Double3[] sizes) = SplitArray(vals);
        return new(Vert.Median(verts), Double3.Median(sizes));
    }
    public static Box3D Max(params Box3D[] vals)
    {
        (Vert[] verts, Double3[] sizes) = SplitArray(vals);
        return new(Vert.Max(verts), Double3.Max(sizes));
    }
    public static Box3D Min(params Box3D[] vals)
    {
        (Vert[] verts, Double3[] sizes) = SplitArray(vals);
        return new(Vert.Min(verts), Double3.Min(sizes));
    }
    public static (Vert[] centers, Double3[] sizes) SplitArray(params Box3D[] vals)
    {
        Vert[] centers = new Vert[vals.Length];
        Double3[] sizes = new Double3[vals.Length];

        for (int i = 0; i < vals.Length; i++)
        {
            centers[i] = vals[i].center;
            sizes[i] = vals[i].size;
        }

        return (centers, sizes);
    }

    public override bool Equals([NotNullWhen(true)] object? obj)
    {
        if (obj == null || obj.GetType() != typeof(Box3D)) return false;
        return Equals((Box3D)obj);
    }
    public bool Equals(Box3D other) => center == other.center && size == other.size;
    public override int GetHashCode() => center.GetHashCode() ^ size.GetHashCode();
    public override string ToString() => ToString((string?)null);
    public string ToString(string? provider) =>
        "Min: " + MinVert.ToString(provider) + " Max: " + MaxVert.ToString(provider);
    public string ToString(IFormatProvider provider) =>
        "Min: " + MinVert.ToString(provider) + " Max: " + MaxVert.ToString(provider);

    public bool Contains(Vert vert)
    {
        Double3 diff = Double3.Absolute(center - vert);
        return diff.x <= size.x && diff.y <= size.y && diff.z <= size.z;
    }
    public object Clone() => new Box3D(center, size);

    public static Box3D operator +(Box3D a, Vert b) => new(a.center + b, a.size);
    public static Box3D operator +(Box3D a, Double3 b) => new(a.center, a.size + b);
    public static Box3D operator -(Box3D b) => new(-b.MaxVert, -b.MinVert);
    public static Box3D operator -(Box3D a, Vert b) => new(a.center - b, a.size);
    public static Box3D operator -(Box3D a, Double3 b) => new(a.center, a.size - b);
    public static Box3D operator *(Box3D a, double b) => new(a.center * b, a.size * b);
    public static Box3D operator *(Box3D a, Double3 b) => new(a.center, a.size * b);
    public static Box3D operator /(Box3D a, double b) => new(a.center / b, a.size / b);
    public static Box3D operator /(Box3D a, Double3 b) => new(a.center, a.size / b);
    public static bool operator ==(Box3D a, Box3D b) => a.Equals(b);
    public static bool operator !=(Box3D a, Box3D b) => !a.Equals(b);

    public static implicit operator Box3D(Fill<double> fill) => new(fill);
    public static implicit operator Box3D(Box2D box) => new(box);
}
