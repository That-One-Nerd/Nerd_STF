namespace Nerd_STF.Mathematics.Geometry;

public record class Box2d : IAbsolute<Box2d>, IAverage<Box2d>, ICeiling<Box2d>, IClamp<Box2d>, IContains<Float3>,
    IEquatable<Box2d>, IFloor<Box2d>, ILerp<Box2d, float>, IMedian<Box2d>, IRound<Box2d>, IShape2d<float>,
    ISplittable<Box2d, (Float3[] centers, Float2[] sizes)>
{
    public static Box2d Unit => new(Float3.Zero, Float2.One);

    public Float3 MaxVert
    {
        get => center + (size / 2);
        set
        {
            Float3 diff = center - value;
            size = (Float2)diff * 2f;
        }
    }
    public Float3 MinVert
    {
        get => center - (size / 2);
        set
        {
            Float3 diff = center + value;
            size = (Float2)diff * 2f;
        }
    }

    public float Area => size.x * size.y;
    public float Perimeter => 2 * (size.x + size.y);

    public Float3 center;
    public Float2 size;

    public Box2d(Float3 min, Float3 max) : this(Float3.Average(min, max), (Float2)(min - max)) { }
    public Box2d(Float3 center, Float2 size)
    {
        this.center = center;
        this.size = size;
    }
    public Box2d(Fill<float> fill) : this(fill, new Float2(fill(3), fill(4))) { }

    public float this[int index]
    {
        get => size[index];
        set => size[index] = value;
    }

    public static Box2d Absolute(Box2d val) => new(Float3.Absolute(val.MinVert), Float3.Absolute(val.MaxVert));
    public static Box2d Average(params Box2d[] vals)
    {
        (Float3[] centers, Float2[] sizes) = SplitArray(vals);
        return new(Float3.Average(centers), Float2.Average(sizes));
    }
    public static Box2d Ceiling(Box2d val) => new(Float3.Ceiling(val.center), Float2.Ceiling(val.size));
    public static Box2d Clamp(Box2d val, Box2d min, Box2d max) =>
        new(Float3.Clamp(val.center, min.center, max.center), Float2.Clamp(val.size, min.size, max.size));
    public static Box2d Floor(Box2d val) => new(Float3.Floor(val.center), Float2.Floor(val.size));
    public static Box2d Lerp(Box2d a, Box2d b, float t, bool clamp = true) =>
        new(Float3.Lerp(a.center, b.center, t, clamp), Float2.Lerp(a.size, b.size, t, clamp));
    public static Box2d Median(params Box2d[] vals)
    {
        (Float3[] verts, Float2[] sizes) = SplitArray(vals);
        return new(Float3.Median(verts), Float2.Median(sizes));
    }
    public static Box2d Round(Box2d val) => new(Float3.Round(val.center), Float2.Round(val.size));

    public static (Float3[] centers, Float2[] sizes) SplitArray(params Box2d[] vals)
    {
        Float3[] centers = new Float3[vals.Length];
        Float2[] sizes = new Float2[vals.Length];

        for (int i = 0; i < vals.Length; i++)
        {
            centers[i] = vals[i].center;
            sizes[i] = vals[i].size;
        }

        return (centers, sizes);
    }

    public virtual bool Equals(Box2d? other)
    {
        if (other is null) return false;
        return center == other.center && size == other.size;
    }
    public override int GetHashCode() => base.GetHashCode();

    public bool Contains(Float3 vert)
    {
        Float2 diff = Float2.Absolute((Float2)(center - vert));
        return diff.x <= size.x && diff.y <= size.y;
    }

    protected virtual bool PrintMembers(StringBuilder builder)
    {
        builder.Append("Min = ");
        builder.Append(MinVert);
        builder.Append(", Max = ");
        builder.Append(MaxVert);
        return true;
    }

    public static Box2d operator +(Box2d a, Float3 b) => new(a.center + b, a.size);
    public static Box2d operator +(Box2d a, Float2 b) => new(a.center, a.size + b);
    public static Box2d operator -(Box2d b) => new(-b.MaxVert, -b.MinVert);
    public static Box2d operator -(Box2d a, Float3 b) => new(a.center - b, a.size);
    public static Box2d operator -(Box2d a, Float2 b) => new(a.center, a.size - b);
    public static Box2d operator *(Box2d a, float b) => new(a.center * b, a.size * b);
    public static Box2d operator *(Box2d a, Float2 b) => new(a.center, a.size * b);
    public static Box2d operator /(Box2d a, float b) => new(a.center / b, a.size / b);
    public static Box2d operator /(Box2d a, Float2 b) => new(a.center, a.size / b);

    public static implicit operator Box2d(Fill<float> fill) => new(fill);
    public static explicit operator Box2d(Box3d box) => new(box.center, (Float2)box.size);
}
