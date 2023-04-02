namespace Nerd_STF.Mathematics.Geometry;

public record class Box3D : IAbsolute<Box3D>, IAverage<Box3D>, ICeiling<Box3D>, IClamp<Box3D>,
    IContains<Vert>, IEquatable<Box3D>, IFloor<Box3D>, ILerp<Box3D, float>, IMedian<Box3D>,
    IRound<Box3D>, IShape3d<float>, ISplittable<Box3D, (Vert[] centers, Float3[] sizes)>
{
    public static Box3D Unit => new(Vert.Zero, Float3.One);

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

    public float Perimeter => 2 * (size.x + size.y + size.z);
    public float SurfaceArea => 2 * (size.x * size.y + size.y * size.z + size.x * size.z);
    public float Volume => size.x * size.y * size.z;

    public Vert center;
    public Float3 size;

    public Box3D(Box2D box) : this(box.center, (Float3)box.size) { }
    public Box3D(Vert min, Vert max) : this(Vert.Average(min, max), (Float3)(min - max)) { }
    public Box3D(Vert center, Float3 size)
    {
        this.center = center;
        this.size = size;
    }
    public Box3D(Fill<float> fill) : this(fill, new Float3(fill(3), fill(4), fill(5))) { }

    public float this[int index]
    {
        get => size[index];
        set => size[index] = value;
    }

    public static Box3D Absolute(Box3D val) => new(Vert.Absolute(val.MinVert), Vert.Absolute(val.MaxVert));
    public static Box3D Average(params Box3D[] vals)
    {
        (Vert[] centers, Float3[] sizes) = SplitArray(vals);
        return new(Vert.Average(centers), Float3.Average(sizes));
    }
    public static Box3D Ceiling(Box3D val) =>
        new(Vert.Ceiling(val.center), (Float3)Float3.Ceiling(val.size));
    public static Box3D Clamp(Box3D val, Box3D min, Box3D max) =>
        new(Vert.Clamp(val.center, min.center, max.center), Float3.Clamp(val.size, min.size, max.size));
    public static Box3D Floor(Box3D val) =>
        new(Vert.Floor(val.center), (Float3)Float3.Floor(val.size));
    public static Box3D Lerp(Box3D a, Box3D b, float t, bool clamp = true) =>
        new(Vert.Lerp(a.center, b.center, t, clamp), Float3.Lerp(a.size, b.size, t, clamp));
    public static Box3D Median(params Box3D[] vals)
    {
        (Vert[] verts, Float3[] sizes) = SplitArray(vals);
        return new(Vert.Median(verts), Float3.Median(sizes));
    }
    public static Box3D Round(Box3D val) => new(Vert.Ceiling(val.center), (Float3)Float3.Ceiling(val.size));

    public static (Vert[] centers, Float3[] sizes) SplitArray(params Box3D[] vals)
    {
        Vert[] centers = new Vert[vals.Length];
        Float3[] sizes = new Float3[vals.Length];

        for (int i = 0; i < vals.Length; i++)
        {
            centers[i] = vals[i].center;
            sizes[i] = vals[i].size;
        }

        return (centers, sizes);
    }

    public virtual bool Equals(Box3D? other)
    {
        if (other is null) return false;
        return center == other.center && size == other.size;
    }
    public override int GetHashCode() => base.GetHashCode();

    public bool Contains(Vert vert)
    {
        Float3 diff = Float3.Absolute(center - vert);
        return diff.x <= size.x && diff.y <= size.y && diff.z <= size.z;
    }

    protected virtual bool PrintMembers(StringBuilder builder)
    {
        builder.Append("Min = ");
        builder.Append(MinVert);
        builder.Append(", Max = ");
        builder.Append(MaxVert);
        return true;
    }

    public static Box3D operator +(Box3D a, Vert b) => new(a.center + b, a.size);
    public static Box3D operator +(Box3D a, Float3 b) => new(a.center, a.size + b);
    public static Box3D operator -(Box3D b) => new(-b.MaxVert, -b.MinVert);
    public static Box3D operator -(Box3D a, Vert b) => new(a.center - b, a.size);
    public static Box3D operator -(Box3D a, Float3 b) => new(a.center, a.size - b);
    public static Box3D operator *(Box3D a, float b) => new(a.center * b, a.size * b);
    public static Box3D operator *(Box3D a, Float3 b) => new(a.center, a.size * b);
    public static Box3D operator /(Box3D a, float b) => new(a.center / b, a.size / b);
    public static Box3D operator /(Box3D a, Float3 b) => new(a.center, a.size / b);

    public static implicit operator Box3D(Fill<float> fill) => new(fill);
    public static implicit operator Box3D(Box2D box) => new(box);
}
