namespace Nerd_STF.Mathematics.Geometry;

public class Quadrilateral : IClosestTo<Float3>,
    IFromTuple<Quadrilateral, (Float3 a, Float3 b, Float3 c, Float3 d)>, IPolygon<Quadrilateral>,
    ISplittable<Quadrilateral, (Float3[] As, Float3[] Bs, Float3[] Cs, Float3[] Ds)>,
    ISubdivide<Quadrilateral>, ITriangulate, IWithinRange<Float3, float>
{
    public float Area
    {
        get // Modification of Heron's Formula to work with quadrilaterals.
        {
            float a = AB.Length, b = BC.Length, c = CD.Length, d = DA.Length,
                  s = (a + b + c + d) / 2;

            Angle theta1 = Angle.FromPoints(this.a, this.b, this.c),
                  theta2 = Angle.FromPoints(this.c, this.d, this.a);

            float cos = Mathf.Cos((theta1 + theta2) / 2);

            return Mathf.Sqrt((s - a) * (s - b) * (s - c) * (s - d) - (a * b * c * d * cos * cos));
        }
    }
    public Float3 Midpoint => Float3.Average(a, b, c, d);
    public float Perimeter => AB.Length + BC.Length + CD.Length + DA.Length;

    public Line AB
    {
        get => (a, b);
        set
        {
            a = value.a;
            b = value.b;
        }
    }
    public Line AC
    {
        get => (a, c);
        set
        {
            a = value.a;
            c = value.b;
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
    public Line BD
    {
        get => (b, d);
        set
        {
            b = value.a;
            d = value.b;
        }
    }
    public Line CD
    {
        get => (c, d);
        set
        {
            c = value.a;
            d = value.b;
        }
    }
    public Line DA
    {
        get => (d, a);
        set
        {
            d = value.a;
            a = value.b;
        }
    }

    public Triangle ABC
    {
        get => (a, b, c);
        set
        {
            a = value.a;
            b = value.b;
            c = value.c;
        }
    }
    public Triangle BCD
    {
        get => (b, c, d);
        set
        {
            b = value.a;
            c = value.b;
            d = value.c;
        }
    }
    public Triangle CDA
    {
        get => (c, d, a);
        set
        {
            c = value.a;
            d = value.b;
            a = value.c;
        }
    }
    public Triangle DAB
    {
        get => (d, a, b);
        set
        {
            d = value.a;
            a = value.b;
            b = value.c;
        }
    }

    public Angle AngleABC => Angle.FromPoints(a, b, c);
    public Angle AngleBCD => Angle.FromPoints(b, c, d);
    public Angle AngleCDA => Angle.FromPoints(c, d, a);
    public Angle AngleDAB => Angle.FromPoints(d, a, b);

    public Float3 a, b, c, d;

    public Quadrilateral() : this(Float3.Zero, Float3.Zero, Float3.Zero, Float3.Zero) { }
    public Quadrilateral(Float3 a, Float3 b, Float3 c, Float3 d)
    {
        this.a = a;
        this.b = b;
        this.c = c;
        this.d = d;
    }
    public Quadrilateral(Line ab, Line bc, Line cd, Line da)
    {
        if (ab.b != bc.a || bc.b != cd.a || cd.b != da.a || da.b != ab.a)
            throw new DisconnectedLinesException(ab, bc, cd, da);

        a = ab.a;
        b = bc.a;
        c = cd.a;
        d = da.a;
    }
    public Quadrilateral(float x1, float y1, float x2, float y2, float x3, float y3,
        float x4, float y4)
    {
        a = (x1, y1, 0);
        b = (x2, y2, 0);
        c = (x3, y3, 0);
        d = (x4, y4, 0);
    }
    public Quadrilateral(float x1, float y1, float z1, float x2, float y2, float z2,
        float x3, float y3, float z3, float x4, float y4, float z4)
    {
        a = (x1, y1, z1);
        b = (x2, y2, z2);
        c = (x3, y3, z3);
        d = (x4, y4, z4);
    }
    public Quadrilateral(Fill<Float3> fill) : this(fill(0), fill(1), fill(2), fill(3)) { }
    public Quadrilateral(Fill<Int3> fill) : this(fill(0), fill(1), fill(2), fill(3)) { }
    public Quadrilateral(Fill<Line> fill) : this(fill(0), fill(1), fill(2), fill(3)) { }
    public Quadrilateral(Fill<float> fill) : this(fill(0), fill(1), fill(2), fill(3),
        fill(4), fill(5), fill(6), fill(7), fill(8), fill(9), fill(10), fill(11)) { }
    public Quadrilateral(Fill<int> fill) : this(fill(0), fill(1), fill(2), fill(3),
        fill(4), fill(5), fill(6), fill(7), fill(8), fill(9), fill(10), fill(11)) { }

    public Float3 this[int index]
    {
        get => index switch
        {
            0 => a,
            1 => b,
            2 => c,
            3 => d,
            _ => throw new IndexOutOfRangeException(nameof(index))
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

                case 3:
                    d = value;
                    break;

                default: throw new IndexOutOfRangeException(nameof(index));
            }
        }
    }

    public Float3 this[Index index]
    {
        get => this[index.IsFromEnd ? 4 - index.Value : index.Value];
        set => this[index.IsFromEnd ? 4 - index.Value : index.Value] = value;
    }
    public Float3[] this[Range range]
    {
        get
        {
            int start = range.Start.IsFromEnd ? 4 - range.Start.Value : range.Start.Value;
            int end = range.End.IsFromEnd ? 4 - range.End.Value : range.End.Value;
            List<Float3> res = new();
            for (int i = start; i < end; i++) res.Add(this[i]);
            return res.ToArray();
        }
        set
        {
            int start = range.Start.IsFromEnd ? 4 - range.Start.Value : range.Start.Value;
            int end = range.End.IsFromEnd ? 4 - range.End.Value : range.End.Value;
            for (int i = start; i < end; i++) this[i] = value[i];
        }
    }

    public static Quadrilateral Average(params Quadrilateral[] vals)
    {
        (Float3[] As, Float3[] Bs, Float3[] Cs, Float3[] Ds) = SplitArray(vals);
        return (Float3.Average(As), Float3.Average(Bs), Float3.Average(Cs), Float3.Average(Ds));
    }
    public static Quadrilateral Lerp(Quadrilateral a, Quadrilateral b, float t,
        bool clamp = true) => (Float3.Lerp(a.a, b.a, t, clamp), Float3.Lerp(a.b, b.b, t, clamp),
                               Float3.Lerp(a.c, b.c, t, clamp), Float3.Lerp(a.d, b.d, t, clamp));
    public static (Float3[] As, Float3[] Bs, Float3[] Cs, Float3[] Ds) SplitArray(
        params Quadrilateral[] vals)
    {
        Float3[] As = new Float3[vals.Length],
                 Bs = new Float3[vals.Length],
                 Cs = new Float3[vals.Length],
                 Ds = new Float3[vals.Length];
        for (int i = 0; i < vals.Length; i++)
        {
            As[i] = vals[i].a;
            Bs[i] = vals[i].b;
            Cs[i] = vals[i].c;
            Ds[i] = vals[i].d;
        }
        return (As, Bs, Cs, Ds);
    }

    public static float[] ToFloatArrayAll(params Quadrilateral[] vals)
    {
        float[] result = new float[12 * vals.Length];
        for (int i = 0; i < vals.Length; i++)
        {
            int p = i * 12;
            result[p +  0] = vals[i].a.x;
            result[p +  1] = vals[i].a.y;
            result[p +  2] = vals[i].a.z;
            result[p +  3] = vals[i].b.x;
            result[p +  4] = vals[i].b.y;
            result[p +  5] = vals[i].b.z;
            result[p +  6] = vals[i].c.x;
            result[p +  7] = vals[i].c.y;
            result[p +  8] = vals[i].c.z;
            result[p +  9] = vals[i].d.x;
            result[p + 10] = vals[i].d.y;
            result[p + 11] = vals[i].d.y;
        }
        return result;
    }

    public Float3 ClosestTo(Float3 point)
    {
        Float3 abClosest = AB.ClosestTo(point),
               bcClosest = BC.ClosestTo(point),
               cdClosest = CD.ClosestTo(point),
               daClosest = DA.ClosestTo(point);

        // Very inefficient way to select the closest point.

        float abDist = (abClosest - point).Magnitude,
              bcDist = (bcClosest - point).Magnitude,
              cdDist = (cdClosest - point).Magnitude,
              daDist = (daClosest - point).Magnitude;

        float min = Mathf.Min(abDist, bcDist, cdDist, daDist);
        if (min == abDist) return abClosest;
        else if (min == bcDist) return bcClosest;
        else if (min == cdDist) return cdClosest;
        else return daClosest;
    }

    public bool Equals(Quadrilateral? other) => other is not null &&
        a == other.a && b == other.b && c == other.c && d == other.d;
    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        else if (obj is Quadrilateral quad) return Equals(quad);
        return false;
    }
    public override int GetHashCode() => base.GetHashCode();
    public override string ToString() => $"{nameof(Quadrilateral)} {{ a: {a}, b: {b}, c: {c}, d: {d} }}";

    public bool Contains(Float3 point) => Contains(point, 0.05f);
    public bool Contains(Float3 point, float tolerance)
    {
        Triangle pab = (point, a, b),
                 pbc = (point, b, c),
                 pcd = (point, c, d),
                 pda = (point, d, a);
        return Mathf.Absolute(Area - (pab.Area + pbc.Area + pcd.Area + pda.Area)) < tolerance;
    }
    public bool Contains(Line line) => Contains(line, 0.05f);
    public bool Contains(Line line, float tolerance, float step = Calculus.DefaultStep)
    {
        for (float t = 0; t <= 1; t += step)
            if (!Contains(Float3.Lerp(line.a, line.b, t), tolerance)) return false;
        return true;
    }
    public bool Contains(Triangle tri) => Contains(tri.a) && Contains(tri.b) && Contains(tri.c);
    public bool Contains(Quadrilateral quad) => Contains(quad.a) && Contains(quad.b) &&
        Contains(quad.c) && Contains(quad.d);
    public bool Contains(Box2d box)
    {
        (Float2 topLeft, Float2 topRight, Float2 bottomRight, Float2 bottomLeft) = box.GetCorners();
        return Contains(topLeft) || Contains(topRight) ||
               Contains(bottomRight) || Contains(bottomLeft);
    }
    public bool Contains(IEnumerable<Float3> points)
    {
        foreach (Float3 point in points) if (!Contains(point)) return false;
        return true;
    }
    public bool Contains(Fill<Float3> points, int count)
    {
        for (int i = 0; i < count; i++) if (!Contains(points(i))) return false;
        return true;
    }
    
    public bool Intersects(Line line) => Intersects(line, 0.05f);
    public bool Intersects(Line line, float tolerance, float step = Calculus.DefaultStep)
    {
        for (float t = 0; t <= 1; t += step)
            if (Contains(Float3.Lerp(line.a, line.b, t), tolerance))
                return true;
        return false;
    }
    public bool Intersects(Quadrilateral quad)
    {
        if (Contains(quad) || quad.Contains(this)) return true;
        return Intersects(quad.AB) || Intersects(quad.BC) ||
               Intersects(quad.CD) || Intersects(quad.DA);
    }
    public bool Intersects(Box2d box)
    {
        if (Contains(box) || box.Contains(this)) return true;

        (Line top, Line right, Line bottom, Line left) = box.GetOutlines();
        return Intersects(top) || Intersects(right) ||
               Intersects(bottom) || Intersects(left);
    }
    public bool Intersects(Triangle tri)
    {
        if (Contains(tri) || tri.Contains(this)) return true;
        return Intersects(tri.AB) || Intersects(tri.BC) || Intersects(tri.CA);
    }
    public bool Intersects(IEnumerable<Line> lines)
    {
        foreach (Line l in lines) if (Contains(l)) return true;
        return false;
    }
    public bool Intersects(Fill<Line> lines, int count)
    {
        for (int i = 0; i < count; i++) if (Contains(lines(i))) return true;
        return false;
    }

    public Float3[] GetAllVerts() => new[] { a, b, c, d };
    public Line[] GetOutlines() => new[] { AB, BC, CD, DA };

    public Quadrilateral[] Subdivide()
    {
        Float3 abMid = AB.Midpoint,
               bcMid = BC.Midpoint,
               cdMid = CD.Midpoint,
               daMid = DA.Midpoint,
               abcdMid = Midpoint;

        return new Quadrilateral[4]
        {
            (a, abMid, abcdMid, daMid),
            (b, bcMid, abcdMid, abMid),
            (c, cdMid, abcdMid, bcMid),
            (d, daMid, abcdMid, cdMid)
        };
    }
    public Quadrilateral[] Subdivide(int iterations)
    {
        Quadrilateral[] active = new[] { this };
        for (int i = 0; i < iterations; i++)
        {
            List<Quadrilateral> newQuads = new();
            foreach (Quadrilateral quad in active) newQuads.AddRange(quad.Subdivide());
            active = newQuads.ToArray();
        }
        return active;
    }

    public Triangle[] Triangulate()
    {
        if (AC.Length > BD.Length) return new Triangle[]
        {
            (a, b, c),
            (c, d, a)
        };
        else return new Triangle[]
        {
            (b, c, d),
            (d, a, b)
        };
    }

    public bool WithinRange(Float3 point, float range) =>
        WithinRange(point, range, Calculus.DefaultStep);
    public bool WithinRange(Float3 point, float range, float step)
    {
        // Just like line, this is probably optimizable but who cares?
        return AB.WithinRange(point, range, step) ||
               BC.WithinRange(point, range, step) ||
               CD.WithinRange(point, range, step) ||
               DA.WithinRange(point, range, step);
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    public IEnumerator<Float3> GetEnumerator()
    {
        yield return a;
        yield return b;
        yield return c;
        yield return d;
    }

    public Float3[] ToArray() => new[] { a, b, c, d };
    public Fill<Float3> ToFill()
    {
        Quadrilateral @this = this;
        return i => @this[i];
    }
    public List<Float3> ToList() => new() { a, b, c, d };

    public float[] ToFloatArray() => new[] { a.x, a.y, a.z,
                                             b.x, b.y, b.z,
                                             c.x, c.y, c.z,
                                             d.x, d.y, d.z };

    public static Quadrilateral operator +(Quadrilateral q, Float3 offset) =>
        new(q.a + offset, q.b + offset, q.c + offset, q.d + offset);
    public static Quadrilateral operator -(Quadrilateral q, Float3 offset) =>
        new(q.a - offset, q.b - offset, q.c - offset, q.d - offset);
    public static Quadrilateral operator *(Quadrilateral q, float factor) =>
        new(q.a * factor, q.b * factor, q.c * factor, q.d * factor);
    public static Quadrilateral operator *(Quadrilateral q, Float3 factor) =>
        new(q.a * factor, q.b * factor, q.c * factor, q.d * factor);
    public static Quadrilateral operator /(Quadrilateral q, float factor) =>
        new(q.a / factor, q.b / factor, q.c / factor, q.d / factor);
    public static Quadrilateral operator /(Quadrilateral q, Float3 factor) =>
        new(q.a / factor, q.b / factor, q.c / factor, q.d / factor);

    public static bool operator ==(Quadrilateral a, Quadrilateral b) => a.Equals(b);
    public static bool operator !=(Quadrilateral a, Quadrilateral b) => !a.Equals(b);

    // TODO: explicit conversion from polygon, check if exactly four verts, then cast.
    public static implicit operator Quadrilateral(Fill<Float3> fill) => new(fill);
    public static implicit operator Quadrilateral(Fill<Int3> fill) => new(fill);
    public static implicit operator Quadrilateral(Fill<Line> fill) => new(fill);
    public static implicit operator Quadrilateral(Fill<float> fill) => new(fill);
    public static implicit operator Quadrilateral(Fill<int> fill) => new(fill);
    public static implicit operator Quadrilateral((Float3 a, Float3 b, Float3 c, Float3 d) val) =>
        new(val.a, val.b, val.c, val.d);
}
