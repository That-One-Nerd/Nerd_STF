namespace Nerd_STF.Mathematics.Geometry;

public struct Triangle : ICloneable, IEquatable<Triangle>, IGroup<Vert>
{
    public Vert A
        {
            get => p_a;
            set
            {
                p_a = value;
                p_ab.a = value;
                p_ca.b = value;
            }
        }
    public Vert B
        {
            get => p_b;
            set
            {
                p_b = value;
                p_ab.b = value;
                p_bc.a = value;
            }
        }
    public Vert C
        {
            get => p_c;
            set
            {
                p_c = value;
                p_bc.b = value;
                p_ca.a = value;
            }
        }
    public Line AB
        {
            get => p_ab;
            set
            {
                p_ab = value;
                p_a = value.a;
                p_b = value.b;
                p_bc.a = value.b;
                p_ca.b = value.a;
            }
        }
    public Line BC
        {
            get => p_bc;
            set
            {
                p_bc = value;
                p_b = value.a;
                p_c = value.b;
                p_ca.a = value.b;
                p_ab.b = value.a;
            }
        }
    public Line CA
        {
            get => p_ca;
            set
            {
                p_ca = value;
                p_a = value.b;
                p_c = value.a;
                p_ab.a = value.b;
                p_bc.b = value.a;
            }
        }

    private Vert p_a, p_b, p_c;
    private Line p_ab, p_bc, p_ca;

    public double Area => Mathf.Absolute((A.position.x * B.position.y) + (B.position.x * C.position.y) + (C.position.x * A.position.y) -
        ((B.position.x * A.position.y) + (C.position.x * B.position.y) + (A.position.x * C.position.y))) * 0.5;
    public double Perimeter => AB.Length + BC.Length + CA.Length;

    public Triangle(Vert a, Vert b, Vert c)
    {
        p_a = a;
        p_b = b;
        p_c = c;
        p_ab = new(a, b);
        p_bc = new(b, c);
        p_ca = new(c, a);
    }
    public Triangle(Line ab, Line bc, Line ca)
    {
        if (ab.a != ca.b || ab.b != bc.a || bc.b != ca.a)
            throw new DisconnectedLinesException(ab, bc, ca);

        p_a = ab.a;
        p_b = bc.a;
        p_c = ca.a;
        p_ab = ab;
        p_bc = bc;
        p_ca = ca;
    }
    public Triangle(double x1, double y1, double x2, double y2, double x3, double y3)
        : this(new Vert(x1, y1), new Vert(x2, y2), new Vert(x3, y3)) { }
    public Triangle(double x1, double y1, double z1, double x2, double y2, double z2, double x3, double y3,
        double z3) : this(new Vert(x1, y1, z1), new Vert(x2, y2, z2), new Vert(x3, y3, z3)) { }
    public Triangle(Fill<Double3> fill) : this(fill(0), fill(1), fill(2)) { }
    public Triangle(Fill<Int3> fill) : this(fill(0), fill(1), fill(2)) { }
    public Triangle(Fill<Vert> fill) : this(fill(0), fill(1), fill(2)) { }
    public Triangle(Fill<Line> fill) : this(fill(0), fill(1), fill(2)) { }
    public Triangle(Fill<double> fill) : this(fill(0), fill(1), fill(2), fill(3), fill(4), fill(5), fill(6),
        fill(7), fill(8)) { }
    public Triangle(Fill<int> fill) : this(fill(0), fill(1), fill(2), fill(3), fill(4), fill(5), fill(6),
        fill(7), fill(8)) { }

    public Vert this[int index]
    {
        get => index switch
        {
            0 => A,
            1 => B,
            2 => C,
            _ => throw new IndexOutOfRangeException(nameof(index)),
        };
        set
        {
            switch (index)
            {
                case 0:
                    A = value;
                    break;

                case 1:
                    B = value;
                    break;

                case 2:
                    C = value;
                    break;

                default: throw new IndexOutOfRangeException(nameof(index));
            }
        }
    }

    public static Triangle Absolute(Triangle val) =>
        new(Vert.Absolute(val.A), Vert.Absolute(val.B), Vert.Absolute(val.C));
    public static Triangle Average(params Triangle[] vals)
    {
        (Vert[] As, Vert[] Bs, Vert[] Cs) = SplitVertArray(vals);
        return new(Vert.Average(As), Vert.Average(Bs), Vert.Average(Cs));
    }
    public static Triangle Ceiling(Triangle val) =>
        new(Vert.Ceiling(val.A), Vert.Ceiling(val.B), Vert.Ceiling(val.C));
    public static Triangle Clamp(Triangle val, Triangle min, Triangle max) =>
        new(Vert.Clamp(val.A, min.A, max.A), Vert.Clamp(val.B, min.B, max.B), Vert.Clamp(val.C, min.C, max.C));
    public static Triangle Floor(Triangle val) =>
        new(Vert.Floor(val.A), Vert.Floor(val.B), Vert.Floor(val.C));
    public static Triangle Lerp(Triangle a, Triangle b, double t, bool clamp = true) =>
        new(Vert.Lerp(a.A, b.A, t, clamp), Vert.Lerp(a.B, b.B, t, clamp), Vert.Lerp(a.C, b.C, t, clamp));
    public static Triangle Max(params Triangle[] vals)
    {
        (Vert[] As, Vert[] Bs, Vert[] Cs) = SplitVertArray(vals);
        return new(Vert.Max(As), Vert.Max(Bs), Vert.Max(Cs));
    }
    public static Triangle Median(params Triangle[] vals)
    {
        (Vert[] As, Vert[] Bs, Vert[] Cs) = SplitVertArray(vals);
        return new(Vert.Median(As), Vert.Median(Bs), Vert.Median(Cs));
    }
    public static Triangle Min(params Triangle[] vals)
    {
        (Vert[] As, Vert[] Bs, Vert[] Cs) = SplitVertArray(vals);
        return new(Vert.Min(As), Vert.Min(Bs), Vert.Min(Cs));
    }

    public static (Vert[] As, Vert[] Bs, Vert[] Cs) SplitVertArray(params Triangle[] tris)
    {
        Vert[] a = new Vert[tris.Length], b = new Vert[tris.Length], c = new Vert[tris.Length];
        for (int i = 0; i < tris.Length; i++)
        {
            a[i] = tris[i].A;
            b[i] = tris[i].B;
            c[i] = tris[i].C;
        }
         return (a, b, c);
    }
    public static (Line[] ABs, Line[] BCs, Line[] CAs) SplitLineArray(params Triangle[] tris)
    {
        Line[] ab = new Line[tris.Length], bc = new Line[tris.Length], ca = new Line[tris.Length];
        for (int i = 0; i < tris.Length; i++)
        {
            ab[i] = tris[i].AB;
            bc[i] = tris[i].BC;
            ca[i] = tris[i].CA;
        }
         return (ab, bc, ca);
    }

    public static double[] ToDoubleArrayAll(params Triangle[] tris)
    {
        double[] vals = new double[tris.Length * 9];
        for (int i = 0; i < tris.Length; i++)
        {
            int pos = i * 9;
            vals[pos + 0] = tris[i].A.position.x;
            vals[pos + 1] = tris[i].A.position.y;
            vals[pos + 2] = tris[i].A.position.z;
            vals[pos + 3] = tris[i].B.position.x;
            vals[pos + 4] = tris[i].B.position.y;
            vals[pos + 5] = tris[i].B.position.z;
            vals[pos + 6] = tris[i].C.position.x;
            vals[pos + 7] = tris[i].C.position.y;
            vals[pos + 8] = tris[i].C.position.z;
        }
        return vals;
    }
    public static List<double> ToDoubleListAll(params Triangle[] tris) => new(ToDoubleArrayAll(tris));

    public override bool Equals([NotNullWhen(true)] object? obj)
    {
        if (obj == null || obj.GetType() != typeof(Triangle)) return false;
        return Equals((Triangle)obj);
    }
    public bool Equals(Triangle other) => A == other.A && B == other.B && C == other.C;
    public override int GetHashCode() => A.GetHashCode() ^ B.GetHashCode() ^ C.GetHashCode();
    public override string ToString() => ToString((string?)null);
    public string ToString(string? provider) =>
        "A: " + A.ToString(provider) + " B: " + B.ToString(provider) + " C: " + C.ToString(provider);
    public string ToString(IFormatProvider provider) =>
        "A: " + A.ToString(provider) + " B: " + B.ToString(provider) + " C: " + C.ToString(provider);

    public object Clone() => new Triangle(A, B, C);

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    public IEnumerator<Vert> GetEnumerator()
    {
        yield return A;
        yield return B;
        yield return C;
    }

    public Vert[] ToArray() => new Vert[] { A, B, C };
    public List<Vert> ToList() => new() { A, B, C };

    public double[] ToDoubleArray() => new double[] { A.position.x, A.position.y, A.position.z,
                                                      B.position.x, B.position.y, B.position.z,
                                                      C.position.x, C.position.y, C.position.z };
    public List<double> ToDoubleList() => new() { A.position.x, A.position.y, A.position.z,
                                                  B.position.x, B.position.y, B.position.z,
                                                  C.position.x, C.position.y, C.position.z };
    public static Triangle operator +(Triangle a, Triangle b) => new(a.A + b.A, a.B + b.B, a.C + b.C);
    public static Triangle operator +(Triangle a, Vert b) => new(a.A + b, a.B + b, a.C + b);
    public static Triangle operator -(Triangle t) => new(-t.A, -t.B, -t.C);
    public static Triangle operator -(Triangle a, Triangle b) => new(a.A - b.A, a.B - b.B, a.C - b.C);
    public static Triangle operator -(Triangle a, Vert b) => new(a.A - b, a.B - b, a.C - b);
    public static Triangle operator *(Triangle a, Triangle b) => new(a.A * b.A, a.B * b.B, a.C * b.C);
    public static Triangle operator *(Triangle a, Vert b) => new(a.A * b, a.B * b, a.C * b);
    public static Triangle operator *(Triangle a, double b) => new(a.A * b, a.B * b, a.C * b);
    public static Triangle operator /(Triangle a, Triangle b) => new(a.A / b.A, a.B / b.B, a.C / b.C);
    public static Triangle operator /(Triangle a, Vert b) => new(a.A / b, a.B / b, a.C / b);
    public static Triangle operator /(Triangle a, double b) => new(a.A / b, a.B / b, a.C / b);
    public static bool operator ==(Triangle a, Triangle b) => a.Equals(b);
    public static bool operator !=(Triangle a, Triangle b) => !a.Equals(b);

    public static implicit operator Triangle(Fill<Vert> fill) => new(fill);
    public static implicit operator Triangle(Fill<Double3> fill) => new(fill);
    public static implicit operator Triangle(Fill<Int3> fill) => new(fill);
    public static implicit operator Triangle(Fill<Line> fill) => new(fill);
    public static implicit operator Triangle(Fill<double> fill) => new(fill);
    public static implicit operator Triangle(Fill<int> fill) => new(fill);
    public static explicit operator Triangle(Polygon poly) => new(poly.Lines[0], poly.Lines[1], poly.Lines[2]);
}
