using Nerd_STF.Mathematics.Geometry.Abstract;

namespace Nerd_STF.Mathematics.Geometry;

public class Ellipse : IAverage<Ellipse>, IEquatable<Ellipse>,
    IGeometricScale2d<Ellipse>, IGeometricTranslate2d<Ellipse>, ILerp<Ellipse, float>,
    IMedian<Ellipse>, IPresets0d<Ellipse>,
    ISplittable<Ellipse, (Float2[] positions, Float2[] radii)>, ITriangulate
{
    public static Ellipse Unit => new(Float2.Zero, Float2.One, true);

    public float Area => Constants.Pi * Radius.x * Radius.y;
    public float Perimeter => GeometryHelper.EllipsePerimeterRamanujan2(this);

    public float Eccentricity =>
        Mathf.Sqrt(Radius.x * Radius.x - Radius.y * Radius.y) / Radius.x;

    public float H =>
        ((Radius.x - Radius.y) * (Radius.x - Radius.y))
      / ((Radius.x + Radius.y) * (Radius.x + Radius.y));

    public Float2 Bottom
    {
        get => Position + Float2.Down * Radius;
        set
        {
            Float2 pos = Position;
            Float2 rad = Radius;

            pos.x = value.x;
            float offset = (pos + Float2.Down * rad).y - value.y;
            rad.y *= offset / 2;
            pos.y += offset / 2;

            Position = pos;
            Radius = rad;
        }
    }
    public Float2 Left
    {
        get => Position + Float2.Left * Radius;
        set
        {
            Float2 pos = Position;
            Float2 rad = Radius;

            pos.x = value.x;
            float offset = (pos + Float2.Left * rad).y - value.y;
            rad.y *= offset / 2;
            pos.y += offset / 2;

            Position = pos;
            Radius = rad;
        }
    }
    public Float2 Right
    {
        get => Position + Float2.Right * Radius;
        set
        {
            Float2 pos = Position;
            Float2 rad = Radius;

            pos.x = value.x;
            float offset = (pos + Float2.Right * rad).y - value.y;
            rad.y *= offset / 2;
            pos.y += offset / 2;

            Position = pos;
            Radius = rad;
        }
    }
    public Float2 Top
    {
        get => Position + Float2.Up * Radius;
        set
        {
            Float2 pos = Position;
            Float2 rad = Radius;

            pos.x = value.x;
            float offset = (pos + Float2.Up * rad).y - value.y;
            rad.y *= offset / 2;
            pos.y += offset / 2;

            Position = pos;
            Radius = rad;
        }
    }

    public Float2 Position
    {
        get => p_position;
        set => p_position = value;
    }
    public Float2 Radius
    {
        get => p_radius;
        set
        {
            float ogRadiusAspect = p_radius.y / p_radius.x,
                  newRadiusAspect = value.y / value.x;
            if (LockAspect && ogRadiusAspect != newRadiusAspect) ThrowLockedAspect();
            p_radius = Float2.Absolute(value);
        }
    }

    private Float2 p_position;
    private Float2 p_radius;

    public bool LockAspect { get; init; }

    public Ellipse(Float2 position, Float2 radius, bool lockAspect = false)
    {
        p_position = position;
        p_radius = Float2.Absolute(radius);
        this.LockAspect = lockAspect;
    }
    public Ellipse(Float2 position, float radius, bool lockAspect = false)
    {
        p_position = position;
        p_radius = Float2.Absolute((radius, radius));
        this.LockAspect = lockAspect;
    }
    public Ellipse(Float2 position, float radiusX, float radiusY, bool lockAspect = false)
    {
        p_position = position;
        p_radius = Float2.Absolute((radiusX, radiusY));
        this.LockAspect = lockAspect;
    }
    public Ellipse(float x, float y, Float2 radius, bool lockAspect = false)
    {
        p_position = (x, y);
        p_radius = Float2.Absolute(radius);
        this.LockAspect = lockAspect;
    }
    public Ellipse(float x, float y, float radius, bool lockAspect = false)
    {
        p_position = (x, y);
        p_radius = Float2.Absolute((radius, radius));
        this.LockAspect = lockAspect;
    }
    public Ellipse(float x, float y, float radiusX, float radiusY, bool lockAspect = false)
    {
        p_position = (x, y);
        p_radius = Float2.Absolute((radiusX, radiusY));
        this.LockAspect = lockAspect;
    }
    public Ellipse(Fill<Float2> fill, bool lockAspect = false)
        : this(fill(0), fill(1), lockAspect) { }
    public Ellipse(Fill<Int2> fill, bool lockAspect = false)
        : this(fill(0), fill(1), lockAspect) { }
    public Ellipse(Fill<float> fill, bool lockAspect = false)
        : this(fill(0), fill(1), fill(2), fill(3), lockAspect) { }
    public Ellipse(Fill<int> fill, bool lockAspect = false)
        : this(fill(0), fill(1), fill(2), fill(3), lockAspect) { }

    // TODO
    public static Ellipse FromFocalPoints(Float2 left, Float2 right, float length, bool lockAspect = false)
    {
        if (left.y != right.y)
            throw new NotAlignedException("Focal points must be aligned on the Y-axis.");
        if (left.x < right.x)
            throw new ArgumentException("The left focal point must be on the left.");
        
        // TODO: E = c / a
        //       c = |focal - center|
        //       c = sqrt(a^2 - b^2) / a

        Float2 center = Float2.Average(left, right);
        float c = right.x - center.x;

        return null!; // TODO
    }
    public static Ellipse FromBounds(Box2d box, bool lockAspect) =>
        new(box.center, box.extents, lockAspect);
    public static Ellipse FromBounds(Float2 min, Float2 max, bool lockAspect = false)
    {
        float a = (max.x - min.x) / 2,
              b = (max.y - min.y) / 2;
        Float2 center = (min + max) / 2;
        return new(center, (a, b), lockAspect);
    }

    public static Ellipse Average(params Ellipse[] vals)
    {
        (Float2[] positions, Float2[] radii) = SplitArray(vals);
        return new(Float2.Average(positions), Float2.Average(radii));
    }
    public static Ellipse Lerp(Ellipse a, Ellipse b, float t, bool clamp = true) =>
        new(Float2.Lerp(a.Position, b.Position, t, clamp),
            Float2.Lerp(a.Radius, b.Radius, t, clamp));
    public static (Float2[] positions, Float2[] radii) SplitArray(params Ellipse[] vals)
    {
        Float2[] positions = new Float2[vals.Length],
                 radii = new Float2[vals.Length];
        for (int i = 0; i < vals.Length; i++)
        {
            positions[i] = vals[i].Position;
            radii[i] = vals[i].Radius;
        }
        return (positions, radii);
    }

    public Float2 ClosestTo(Float2 point)
    {
        point = (point - Position) / Radius;
        point = point.Normalized;
        point = (point * Radius) + Position;
        return point;
    }

    public bool Contains(Float2 point) =>
        ((point.x - Position.x) / Radius.x) * ((point.x - Position.x) / Radius.x) +
        ((point.y - Position.y) / Radius.y) * ((point.y - Position.y) / Radius.y) <= 1;

    public bool Contains<T>(T poly) where T : IPolygon<T>
    {
        Float3[] verts = poly.GetAllVerts();
        if (verts.Length < 1) return false;

        foreach (Float3 v in verts) if (!Contains((Float2)v)) return false;
        return true;
    }
    public bool Contains(Box2d box) => Contains(box.Min) && Contains(box.Max);
    public bool Contains(Line line) => Contains((Float2)line.a) && Contains((Float2)line.b);
    public bool Contains(Triangle tri) =>
        Contains((Float2)tri.a) && Contains((Float2)tri.b) & Contains((Float2)tri.c);
    public bool Contains(IEnumerable<Float2> points)
    {
        foreach (Float3 p in points) if (!Contains((Float2)p)) return false;
        return true;
    }
    public bool Contains(Fill<Float2> points, int count)
    {
        for (int i = 0; i < count; i++) if (!Contains(points(i))) return false;
        return true;
    }

    public (Float2 left, Float2 right) GetFocalPoints()
    {
        float c = Eccentricity * Radius.x;
        return (Position - (c, 0), Position + (c, 0));
    }

    public Float2 LerpAcrossOutline(float t, bool clamp = true)
    {
        if (clamp) t = Mathf.Clamp(t, 0, 1);
        else t = Mathf.AbsoluteMod(t, 1);

        float rot = 2 * Constants.Pi * t;
        Float2 point = (Mathf.Cos(rot), Mathf.Sin(rot));
        point.x *= Radius.x;
        point.y *= Radius.y;

        point += Position;
        return point;
    }

    public void Scale(float factor)
    {
        Radius *= factor;
    }
    public void Scale(Float2 factor)
    {
        Radius *= factor;
    }

    public Ellipse ScaleImmutable(float factor)
    {
        Ellipse clone = new(Position, Radius, LockAspect);
        clone.Scale(factor);
        return clone;
    }
    public Ellipse ScaleImmutable(Float2 factor)
    {
        Ellipse clone = new(Position, Radius, LockAspect);
        clone.Scale(factor);
        return clone;
    }

    public void Translate(Float2 offset)
    {
        Position += offset;
    }
    public Ellipse TranslateImmutable(Float2 offset)
    {
        Ellipse clone = new(Position, Radius, LockAspect);
        clone.Translate(offset);
        return clone;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        else if (obj is Ellipse ell) return Equals(ell);
        return false;
    }
    public bool Equals(Ellipse? other) => other is not null && Position == other.Position &&
        Radius == other.Radius;
    public override int GetHashCode() => base.GetHashCode();
    public override string ToString() => $"{nameof(Ellipse)} {{ Position: {Position}, Radius: {Radius} }}";

    // TODO: public Polygon ToPolygon()
    public Triangle[] Triangulate() => Triangulate(TriangulationMode.TriangleFan, 32);
    public Triangle[] Triangulate(int detail) => Triangulate(TriangulationMode.TriangleFan, detail);
    public Triangle[] Triangulate(TriangulationMode mode) => Triangulate(mode, 32);
    public Triangle[] Triangulate(TriangulationMode mode, int detail) => mode switch
    {
        TriangulationMode.TriangleFan => GeometryHelper.EllipseTriangulateFan(this, 1f / detail),
        _ => throw new ArgumentException("Unknown triangulation mode \"" + mode + "\"")
    };

    private void ThrowLockedAspect() => throw new AspectLockedException(
            "Ellipse has a locked aspect ratio which cannot be changed.", this);

    public enum TriangulationMode
    {
        TriangleFan
    }
}
