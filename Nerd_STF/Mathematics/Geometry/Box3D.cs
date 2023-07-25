using Nerd_STF.Mathematics.Abstract;

namespace Nerd_STF.Mathematics.Geometry;

public record class Box3d : IAbsolute<Box3d>, IAverage<Box3d>, ICeiling<Box3d>, IClamp<Box3d>,
    IContains<Float3>, IEquatable<Box3d>, IFloor<Box3d>, ILerp<Box3d, float>, IMedian<Box3d>,
    IRound<Box3d>, IShape3d<float>, ISplittable<Box3d, (Float3[] centers, Float3[] sizes)>
{
    public static Box3d Unit => new(Float3.Zero, Float3.One);

    public Float3 MaxVert
    {
        get => center + (size / 2);
        set
        {
            Float3 diff = center - value;
            size = diff * 2;
        }
    }
    public Float3 MinVert
    {
        get => center - (size / 2);
        set
        {
            Float3 diff = center + value;
            size = diff * 2;
        }
    }

    public float Perimeter => 2 * (size.x + size.y + size.z);
    public float SurfaceArea => 2 * (size.x * size.y + size.y * size.z + size.x * size.z);
    public float Volume => size.x * size.y * size.z;

    public Float3 center;
    public Float3 size;

    public Box3d(Box2d box) : this(box.center, (Float3)box.size) { }
    public Box3d(Float3 min, Float3 max)
    {
        this.center = Float3.Average(min, max);
        this.size = max - min;
    }
    public Box3d(Fill<float> fill) : this(fill, new Float3(fill(3), fill(4), fill(5))) { }

    public float this[int index]
    {
        get => size[index];
        set => size[index] = value;
    }

    public static Box3d Absolute(Box3d val) => new(Float3.Absolute(val.MinFloat3), Float3.Absolute(val.MaxFloat3));
    public static Box3d Average(params Box3d[] vals)
    {
        (Float3[] centers, Float3[] sizes) = SplitArray(vals);
        return new(Float3.Average(centers), Float3.Average(sizes));
    }
    public static Box3d Ceiling(Box3d val) =>
        new(Float3.Ceiling(val.center), (Float3)Float3.Ceiling(val.size));
    public static Box3d Clamp(Box3d val, Box3d min, Box3d max) =>
        new(Float3.Clamp(val.center, min.center, max.center), Float3.Clamp(val.size, min.size, max.size));
    public static Box3d Floor(Box3d val) =>
        new(Float3.Floor(val.center), (Float3)Float3.Floor(val.size));
    public static Box3d Lerp(Box3d a, Box3d b, float t, bool clamp = true) =>
        new(Float3.Lerp(a.center, b.center, t, clamp), Float3.Lerp(a.size, b.size, t, clamp));
    public static Box3d Median(params Box3d[] vals)
    {
        (Float3[] verts, Float3[] sizes) = SplitArray(vals);
        return new(Float3.Median(verts), Float3.Median(sizes));
    }
    public static Box3d Round(Box3d val) => new(Float3.Ceiling(val.center), (Float3)Float3.Ceiling(val.size));

    public static (Float3[] centers, Float3[] sizes) SplitArray(params Box3d[] vals)
    {
        Float3[] centers = new Float3[vals.Length];
        Float3[] sizes = new Float3[vals.Length];

        for (int i = 0; i < vals.Length; i++)
        {
            centers[i] = vals[i].center;
            sizes[i] = vals[i].size;
        }

        return (centers, sizes);
    }

    public virtual bool Equals(Box3d? other)
    {
        if (other is null) return false;
        return center == other.center && size == other.size;
    }
    public override int GetHashCode() => base.GetHashCode();

    public bool Contains(Float3 vert)
    {
        Float3 diff = Float3.Absolute(center - vert);
        return diff.x <= size.x && diff.y <= size.y && diff.z <= size.z;
    }

    protected virtual bool PrintMembers(StringBuilder builder)
    {
        builder.Append("Min = ");
        builder.Append(MinFloat3);
        builder.Append(", Max = ");
        builder.Append(MaxFloat3);
        return true;
    }

    public static Box3d operator +(Box3d a, Float3 b) => new(a.center + b, a.size);
    public static Box3d operator -(Box3d b) => new(-b.MaxFloat3, -b.MinFloat3);
    public static Box3d operator -(Box3d a, Float3 b) => new(a.center - b, a.size);
    public static Box3d operator *(Box3d a, float b) => new(a.center * b, a.size * b);
    public static Box3d operator *(Box3d a, Float3 b) => new(a.center, a.size * b);
    public static Box3d operator /(Box3d a, float b) => new(a.center / b, a.size / b);
    public static Box3d operator /(Box3d a, Float3 b) => new(a.center, a.size / b);

    public static implicit operator Box3d(Fill<float> fill) => new(fill);
    public static implicit operator Box3d(Box2d box) => new(box);
}
