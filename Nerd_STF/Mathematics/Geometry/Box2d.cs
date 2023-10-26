namespace Nerd_STF.Mathematics.Geometry;

public class Box2d : IAverage<Box2d>, IClosestTo<Float2>, IContains<Box2d>, IContains<Float2>,
    IContains<Line>, IContains<Triangle>, IIntersect<Box2d>, IIntersect<Line>,
    IIntersect<Triangle>, IEquatable<Box2d>, ILerp<Box2d, float>, IMedian<Box2d>,
    ISubdivide<Box2d>, ITriangulate, IWithinRange<Float2, float>
{
    public float Area => Size.x * Size.y;
    public float Perimeter => 2 * Size.x + 2 * Size.y;

    public float Height
    {
        get => extents.y * 2;
        set => extents.y = value / 2;
    }
    public Float2 Max
    {
        get => center + extents;
        set => extents = value - center;
    }
    public Float2 Min
    {
        get => center - extents;
        set => extents = center - value;
    }
    public Float2 Size
    {
        get => extents * 2;
        set => extents = value / 2;
    }
    public float Width
    {
        get => extents.x * 2;
        set => extents.x = value / 2;
    }

    public Float2 center, extents;

    public Box2d()
    {
        center = Float2.Zero;
        extents = Float2.Zero;
    }
    public Box2d(Float2 center, Float2 extents)
    {
        this.center = center;
        this.extents = extents;
    }
    public Box2d(Float2 center, float width, float height)
    {
        this.center = center;
        extents = (width, height);
    }
    public Box2d(float centerX, float centerY, float width, float height)
    {
        center = (centerX, centerY);
        extents = (width, height);
    }
    public Box2d(Fill<Float2> fill) : this(fill(0), fill(1)) { }
    public Box2d(Fill<Int2> fill) : this(fill(0), fill(1)) { }
    public Box2d(Fill<float> fill) : this(fill(0), fill(1), fill(2), fill(3)) { }
    public Box2d(Fill<int> fill) : this(fill(0), fill(1), fill(2), fill(3)) { }

    public static Box2d FromRange(Float2 min, Float2 max)
    {
        if (min.x > max.x) (max.x, min.x) = (min.x, max.x);
        if (min.y > max.y) (max.y, min.y) = (min.y, max.y);
        return new((max + min) / 2, (max - min) / 2);
    }

    public static Box2d Average(params Box2d[] vals)
    {
        (Float2[] centers, Float2[] extents) = SplitArray(vals);
        return new(Float2.Average(centers), Float2.Average(extents));
    }
    public static Box2d Lerp(Box2d a, Box2d b, float t, bool clamp = true) =>
        FromRange(Float2.Lerp(a.Min, b.Min, t, clamp), Float2.Lerp(a.Max, b.Max, t, clamp));
    public static (Float2[] centers, Float2[] extents) SplitArray(params Box2d[] vals)
    {
        Float2[] centers = new Float2[vals.Length],
                 extents = new Float2[vals.Length];
        for (int i = 0; i < vals.Length; i++)
        {
            centers[i] = vals[i].center;
            extents[i] = vals[i].extents;
        }
        return (centers, extents);
    }

    public Float2 AlongRay(Float2 p) => GeometryHelper.Box2dAlongRay(this, p);
    public Float2 ClosestTo(Float2 p) => Float2.Clamp(p, Min, Max);

    public bool Contains(Float2 point)
    {
        Float2 diff = Float2.Absolute(point - center);
        return diff.x <= extents.x && diff.y <= extents.y;
    }
    public bool Contains(Float3 point) => Contains((Float2)point);

    public bool Contains(Box2d box) => Contains(box.Min) && Contains(box.Max);
    public bool Contains(Line line) => Contains(line.a) && Contains(line.b);
    public bool Contains(Triangle tri) =>
        Contains(tri.a) && Contains(tri.b) & Contains(tri.c);
    public bool Contains(IEnumerable<Float2> points)
    {
        foreach (Float3 p in points) if (!Contains(p)) return false;
        return true;
    }
    public bool Contains(IEnumerable<Float3> points)
    {
        foreach (Float3 p in points) if (!Contains(p)) return false;
        return true;
    }
    public bool Contains(Fill<Float2> points, int count)
    {
        for (int i = 0; i < count; i++) if (!Contains(points(i))) return false;
        return true;
    }
    public bool Contains(Fill<Float3> points, int count)
    {
        for (int i = 0; i < count; i++) if (!Contains(points(i))) return false;
        return true;
    }

    public bool Intersects(Box2d box)
    {
        if (Contains(box) || box.Contains(this)) return true;

        // A bunch of brute force work but it's still decently fast.
        (Line top, Line right, Line bottom, Line left) = box.GetOutlines();
        return Intersects(top) || Intersects(right) || Intersects(bottom) || Intersects(left);
    }
    public bool Intersects(Line line)
    {
        if (Contains(line)) return true;

        (Line top, Line right, Line bottom, Line left) = GetOutlines();
        return GeometryHelper.LineIntersects2d(line, top, CrossSection2d.XY) ||
               GeometryHelper.LineIntersects2d(line, right, CrossSection2d.XY) ||
               GeometryHelper.LineIntersects2d(line, bottom, CrossSection2d.XY) ||
               GeometryHelper.LineIntersects2d(line, left, CrossSection2d.XY);
    }
    public bool Intersects(Triangle tri)
    {
        if (Contains(tri) || tri.Contains(this)) return true;
        return Intersects(tri.AB) || Intersects(tri.BC) || Intersects(tri.CA);
    }
    public bool Intersects(IEnumerable<Line> lines)
    {
        foreach (Line l in lines) if (Intersects(l)) return true;
        return false;
    }
    public bool Intersects(Fill<Line> lines, int count)
    {
        for (int i = 0; i < count; i++) if (Intersects(lines(i))) return true;
        return false;
    }

    public Float2 LerpAcrossOutline(float t, bool clamp = true)
    {
        if (!clamp) return LerpAcrossOutline(Mathf.AbsoluteMod(t, 1), true);

        (Line top, Line right, Line bottom, Line left) = GetOutlines();
        float weightTB = top.Length / Perimeter, weightLR = left.Length / Perimeter;

        if (t < weightTB) return (Float2)top.LerpAcross(t / weightTB);
        else if (t < 0.5f) return (Float2)right.LerpAcross((t - weightTB) / weightLR);
        else if (t < 0.5f + weightTB) return (Float2)bottom.LerpAcross((t - 0.5f) / weightTB);
        else return (Float2)left.LerpAcross((t - 0.5f - weightTB) / weightLR);
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        else if (obj is Box2d box2d) return Equals(box2d);
        return false;
    }
    public bool Equals(Box2d? other) => other is not null &&
        center == other.center && extents == other.extents;
    public override int GetHashCode() => base.GetHashCode();
    public override string ToString() => $"{nameof(Box2d)} {{ " +
        $"Min = {Min}, Max = {Max} }}";

    // todo: lerp across the rectangle bounds.
    // also todo: add contains and contains partially for the polygon interface.
    // also also todo: add the encapsulate method

    public (Float2 topLeft, Float2 topRight, Float2 bottomRight, Float2 bottomLeft) GetCorners()
    {
        float top = center.y + extents.y, bottom = center.y - extents.y,
              left = center.x - extents.x, right = center.x + extents.x;
        return ((top, left), (top, right), (bottom, right), (bottom, left));
    }
    public (Line top, Line right, Line bottom, Line left) GetOutlines()
    {
        (Float2 topLeft, Float2 topRight, Float2 bottomRight, Float2 bottomLeft) = GetCorners();
        return ((topLeft, topRight), (topRight, bottomRight),
                (bottomRight, bottomLeft), (bottomLeft, topLeft));
    }

    public Box2d[] Subdivide()
    {
        Float2 halfExtents = extents / 2;
        (Float2 topLeft, Float2 topRight, Float2 bottomRight, Float2 bottomLeft) = GetCorners();
        return new Box2d[]
        {
            new(Float2.Lerp(center, topLeft, 0.5f), halfExtents),
            new(Float2.Lerp(center, topRight, 0.5f), halfExtents),
            new(Float2.Lerp(center, bottomRight, 0.5f), halfExtents),
            new(Float2.Lerp(center, bottomLeft, 0.5f), halfExtents)
        };
    }
    public Box2d[] Subdivide(int iterations)
    {
        Box2d[] active = new[] { this };
        for (int i = 0; i < iterations; i++)
        {
            List<Box2d> newBoxes = new();
            foreach (Box2d box in active) newBoxes.AddRange(box.Subdivide());
            active = newBoxes.ToArray();
        }
        return active;
    }

    public Triangle[] Triangulate()
    {
        (Float2 topLeft, Float2 topRight, Float2 bottomRight, Float2 bottomLeft) = GetCorners();
        return new Triangle[]
        {
            (bottomLeft, topLeft, topRight),
            (topRight, bottomRight, bottomLeft)
        };
    }

    public bool WithinRange(Float2 point, float range)
    {
        // First, get the distance to each edge.
        float top = center.y + extents.y, bottom = center.y - extents.y,
              left = center.x - extents.x, right = center.x + extents.x;
        (Float2 topLeft, Float2 topRight, Float2 bottomRight, Float2 bottomLeft) = GetCorners();

        // Positive if inside the box, but that doesn't matter.
        float toTop = Mathf.Absolute(top - point.y),
              toBottom = Mathf.Absolute(point.y - bottom),
              toLeft = Mathf.Absolute(left - point.x),
              toRight = Mathf.Absolute(point.x - right);

        // Then get the distance to each corner.
        float toTL = (topLeft - point).Magnitude,
              toTR = (topRight - point).Magnitude,
              toBR = (bottomRight - point).Magnitude,
              toBL = (bottomLeft - point).Magnitude;

        // Get the minimum of them all and compare.
        return Mathf.Min(toTop, toBottom, toRight, toLeft,
                         toTL, toTR, toBL, toBR) <= range;
    }

    public enum ClosestToMethod
    {
        Iterative,
        RayCalculations
    }
}
