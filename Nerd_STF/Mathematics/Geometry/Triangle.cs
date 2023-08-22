namespace Nerd_STF.Mathematics.Geometry;

public class Triangle : IClosestTo<Float3>, IContains<Float3>,
    IFromTuple<Triangle, (Float3 a, Float3 b, Float3 c)>, IPolygon<Triangle>,
    ISplittable<Triangle, (Float3[] As, Float3[] Bs, Float3[] Cs)>,
    ISubdivide<Triangle[]>, IWithinRange<Float3, float>
{
    public float Area
    {
        get // Heron's Formula
        {
            float a = AB.Length, b = BC.Length, c = CA.Length,
                  s = (a + b + c) / 2;
            return Mathf.Sqrt(s * (s - a) * (s - b) * (s - c));
        }
    }
    public Float3 Midpoint => Float3.Average(a, b, c);
    public float Perimeter => AB.Length + BC.Length + CA.Length;

    public Line AB
    {
        get => (a, b);
        set
        {
            a = value.a;
            b = value.b;
        }
    }
    public Line BC
    {
        get => (b, c);
        set
        {
            b = value.a;
            c = value.b;
        }
    }
    public Line CA
    {
        get => (c, a);
        set
        {
            c = value.a;
            a = value.b;
        }
    }

    public Float3 a, b, c;

    public Triangle() : this(Float3.Zero, Float3.Zero, Float3.Zero) { }
    public Triangle(Float3 a, Float3 b, Float3 c)
    {
        this.a = a;
        this.b = b;
        this.c = c;
    }
    public Triangle(Line ab, Line bc, Line ca)
    {
        if (ab.b != bc.a || bc.b != ca.a || ca.b != ab.a)
            throw new DisconnectedLinesException(ab, bc, ca);

        a = ab.a;
        b = bc.a;
        c = ca.a;
    }
    public Triangle(float x1, float y1, float x2, float y2, float x3, float y3)
    {
        a = (x1, y1, 0);
        b = (x2, y2, 0);
        c = (x3, y3, 0);
    }
    public Triangle(float x1, float y1, float z1, float x2, float y2, float z2, float x3,
        float y3, float z3)
    {
        a = (x1, y1, z1);
        b = (x2, y2, z2);
        c = (x3, y3, z3);
    }
    public Triangle(Fill<Float3> fill) : this(fill(0), fill(1), fill(2)) { }
    public Triangle(Fill<Int3> fill) : this(fill(0), fill(1), fill(2)) { }
    public Triangle(Fill<Line> fill) : this(fill(0), fill(1), fill(2)) { }
    public Triangle(Fill<float> fill) : this(fill(0), fill(1), fill(2), fill(3), fill(4),
        fill(5), fill(6), fill(7), fill(8)) { }
    public Triangle(Fill<int> fill) : this(fill(0), fill(1), fill(2), fill(3), fill(4),
        fill(5), fill(6), fill(7), fill(8)) { }

    public Float3 this[int index]
    {
        get => index switch
        {
            0 => a,
            1 => b,
            2 => c,
            _ => throw new IndexOutOfRangeException(nameof(index)),
        };
        set
        {
            switch (index)
            {
                case 0:
                    a = value;
                    break;

                case 1:
                    b = value;
                    break;

                case 2:
                    c = value;
                    break;

                default: throw new IndexOutOfRangeException(nameof(index));
            }
        }
    }
    public Float3 this[Index index]
    {
        get => this[index.IsFromEnd ? 3 - index.Value : index.Value];
        set => this[index.IsFromEnd ? 3 - index.Value : index.Value] = value;
    }
    public Float3[] this[Range range]
    {
        get
        {
            int start = range.Start.IsFromEnd ? 3 - range.Start.Value : range.Start.Value;
            int end = range.End.IsFromEnd ? 3 - range.End.Value : range.End.Value;
            List<Float3> res = new();
            for (int i = start; i < end; i++) res.Add(this[i]);
            return res.ToArray();
        }
        set
        {
            int start = range.Start.IsFromEnd ? 3 - range.Start.Value : range.Start.Value;
            int end = range.End.IsFromEnd ? 3 - range.End.Value : range.End.Value;
            for (int i = start; i < end; i++) this[i] = value[i];
        }
    }

    public static Triangle Average(params Triangle[] vals)
    {
        (Float3[] As, Float3[] Bs, Float3[] Cs) = SplitArray(vals);
        return (Float3.Average(As), Float3.Average(Bs), Float3.Average(Cs));
    }
    public static Triangle Lerp(Triangle a, Triangle b, float t, bool clamp = true) =>
        (Float3.Lerp(a.a, b.a, t, clamp), Float3.Lerp(a.b, b.b, t, clamp), Float3.Lerp(a.c, a.c, t, clamp));
    public static (Float3[] As, Float3[] Bs, Float3[] Cs) SplitArray(params Triangle[] vals)
    {
        Float3[] As = new Float3[vals.Length],
                 Bs = new Float3[vals.Length],
                 Cs = new Float3[vals.Length];
        for (int i = 0; i < vals.Length; i++)
        {
            As[i] = vals[i].a;
            Bs[i] = vals[i].b;
            Cs[i] = vals[i].c;
        }
        return (As, Bs, Cs);
    }

    public static float[] ToFloatArrayAll(params Triangle[] vals)
    {
        float[] result = new float[9 * vals.Length];
        for (int i = 0; i < vals.Length; i++)
        {
            int p = i * 9;
            result[p + 0] = vals[i].a.x;
            result[p + 1] = vals[i].a.y;
            result[p + 2] = vals[i].a.z;
            result[p + 3] = vals[i].b.x;
            result[p + 4] = vals[i].b.y;
            result[p + 5] = vals[i].b.z;
            result[p + 6] = vals[i].c.x;
            result[p + 7] = vals[i].c.y;
            result[p + 8] = vals[i].c.z;
        }
        return result;
    }

    public bool Equals(Triangle? other) => other is not null && a == other.a
        && b == other.b && c == other.c;
    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        else if (obj is Triangle tri) return Equals(tri);
        return false;
    }
    public override int GetHashCode() => base.GetHashCode();

    public bool Contains(Float3 point)
    {
        Triangle pab = (point, a, b),
                 pbc = (point, b, c),
                 pca = (point, c, a);
        return Mathf.Absolute(Area - (pab.Area + pbc.Area + pca.Area)) < 0.025f;
    }

    public Float3[] GetAllVerts() => new[] { a, b, c };

    public Triangle[] Subdivide()
    {
        Float3 abMid = AB.Midpoint,
               bcMid = BC.Midpoint,
               caMid = CA.Midpoint;

        return new Triangle[4]
        {
            (a, abMid, caMid),
            (abMid, b, bcMid),
            (caMid, bcMid, c),
            (abMid, bcMid, caMid)
        };
    }
    public Triangle[] Subdivide(int iterations)
    {
        Triangle[] active = new[] { this };
        for (int i = 0; i < iterations; i++)
        {
            List<Triangle> newTris = new();
            foreach (Triangle tri in active) newTris.AddRange(tri.Subdivide());
            active = newTris.ToArray();
        }
        return active;
    }

    public Triangle[] Triangulate() => new[] { this };

    public bool WithinRange(Float3 point, float range) =>
        WithinRange(point, range, Calculus.DefaultStep);
    public bool WithinRange(Float3 point, float range, float step)
    {
        // Just like line, this is probably optimizable but who cares?
        return AB.WithinRange(point, range, step) ||
               BC.WithinRange(point, range, step) ||
               CA.WithinRange(point, range, step);
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    public IEnumerator<Float3> GetEnumerator()
    {
        yield return a;
        yield return b;
        yield return c;
    }

    public Float3[] ToArray() => new[] { a, b, c };
    public Fill<Float3> ToFill()
    {
        Triangle @this = this;
        return i => @this[i];
    }
    public List<Float3> ToList() => new() { a, b, c };

    public float[] ToFloatArray() => new[] { a.x, a.y, a.z,
                                             b.x, b.y, b.z,
                                             c.x, c.y, c.z};

    public static Triangle operator +(Triangle t, Float3 offset) =>
        new(t.a + offset, t.b + offset, t.c + offset);
    public static Triangle operator -(Triangle t, Float3 offset) =>
        new(t.a - offset, t.b - offset, t.c - offset);
    public static Triangle operator *(Triangle t, float factor) =>
        new(t.a * factor, t.b * factor, t.c * factor);
    public static Triangle operator *(Triangle t, Float3 factor) =>
        new(t.a * factor, t.b * factor, t.c * factor);
    public static Triangle operator /(Triangle t, float factor) =>
        new(t.a / factor, t.b / factor, t.c / factor);
    public static Triangle operator /(Triangle t, Float3 factor) =>
        new(t.a / factor, t.b / factor, t.c / factor);

    public static bool operator ==(Triangle a, Triangle b) => a.Equals(b);
    public static bool operator !=(Triangle a, Triangle b) => !a.Equals(b);

    public static implicit operator Triangle(Fill<Float3> fill) => new(fill);
    public static implicit operator Triangle(Fill<Int3> fill) => new(fill);
    public static implicit operator Triangle(Fill<Line> fill) => new(fill);
    public static implicit operator Triangle(Fill<float> fill) => new(fill);
    public static implicit operator Triangle(Fill<int> fill) => new(fill);
    public static implicit operator Triangle((Float3 a, Float3 b, Float3 c) val) =>
        new(val.a, val.b, val.c);
}
