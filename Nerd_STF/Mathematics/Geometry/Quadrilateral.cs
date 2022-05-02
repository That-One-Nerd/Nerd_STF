namespace Nerd_STF.Mathematics.Geometry;

public struct Quadrilateral : ICloneable, IEquatable<Quadrilateral>, IGroup<Vert>, ITriangulatable
{
    public Vert A
        {
            get => p_a;
            set
            {
                p_a = value;
                p_ab.a = value;
                p_da.b = value;
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
                p_cd.a = value;
            }
        }
    public Vert D
        {
            get => p_d;
            set
            {
                p_d = value;
                p_cd.b = value;
                p_da.a = value;
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
                p_da.b = value.a;
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
                p_cd.a = value.b;
                p_ab.b = value.a;
            }
        }
    public Line CD
        {
            get => p_cd;
            set
            {
                p_cd = value;
                p_c = value.a;
                p_d = value.b;
                p_da.a = value.b;
                p_bc.b = value.a;
            }
        }
    public Line DA
        {
            get => p_da;
            set
            {
                p_da = value;
                p_d = value.a;
                p_a = value.b;
                p_ab.a = value.b;
                p_cd.b = value.a;
            }
        }

    private Vert p_a, p_b, p_c, p_d;
    private Line p_ab, p_bc, p_cd, p_da;

    public float Area
    {
        get
        {
            float val = 0;
            foreach (Triangle t in Triangulate()) val += t.Area;
            return val;
        }
    }
    public float Perimeter => AB.Length + BC.Length + CD.Length + DA.Length;

    public Quadrilateral(Vert a, Vert b, Vert c, Vert d)
        {
            p_a = a;
            p_b = b;
            p_c = c;
            p_d = d;
            p_ab = new(a, b);
            p_bc = new(b, c);
            p_cd = new(c, d);
            p_da = new(d, a);
        }
    public Quadrilateral(Line ab, Line bc, Line cd, Line da)
        {
            if (ab.a != da.b || ab.b != bc.a || bc.b != cd.a || cd.b != da.a)
                throw new DisconnectedLinesException(ab, bc, cd, da);

            p_a = ab.a;
            p_b = bc.a;
            p_c = cd.a;
            p_d = da.a;
            p_ab = ab;
            p_bc = bc;
            p_cd = cd;
            p_da = da;
        }
    public Quadrilateral(float x1, float y1, float x2, float y2, float x3, float y3, float x4, float y4)
        : this(new Vert(x1, y1), new(x2, y2), new(x3, y3), new(x4, y4)) { }
    public Quadrilateral(float x1, float y1, float z1, float x2, float y2, float z2, float x3, float y3,
        float z3, float x4, float y4, float z4)
        : this(new Vert(x1, y1, z1), new(x2, y2, z2), new(x3, y3, z3), new(x4, y4, z4)) { }
    public Quadrilateral(Fill<Float3> fill) : this(fill(0), fill(1), fill(2), fill(3)) { }
    public Quadrilateral(Fill<Int3> fill) : this(fill(0), fill(1), fill(2), fill(3)) { }
    public Quadrilateral(Fill<Vert> fill) : this(fill(0), fill(1), fill(2), fill(3)) { }
    public Quadrilateral(Fill<Line> fill) : this(fill(0), fill(1), fill(2), fill(3)) { }
    public Quadrilateral(Fill<float> fill) : this(fill(0), fill(1), fill(2), fill(3), fill(4), fill(5), fill(6),
        fill(7), fill(8), fill(9), fill(10), fill(11)) { }
    public Quadrilateral(Fill<int> fill) : this(fill(0), fill(1), fill(2), fill(3), fill(4), fill(5), fill(6),
        fill(7), fill(8), fill(9), fill(10), fill(11)) { }

    public Vert this[int index]
        {
            get => index switch
            {
                0 => A,
                1 => B,
                2 => C,
                3 => D,
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

                    case 3:
                        D = value;
                        break;

                    default: throw new IndexOutOfRangeException(nameof(index));
                }
            }
        }

    public static Quadrilateral Absolute(Quadrilateral val) =>
        new(Vert.Absolute(val.A), Vert.Absolute(val.B), Vert.Absolute(val.C), Vert.Absolute(val.D));
    public static Quadrilateral Average(params Quadrilateral[] vals)
    {
        (Vert[] As, Vert[] Bs, Vert[] Cs, Vert[] Ds) = SplitVertArray(vals);
        return new(Vert.Average(As), Vert.Average(Bs), Vert.Average(Cs), Vert.Average(Ds));
    }
    public static Quadrilateral Ceiling(Quadrilateral val) =>
        new(Vert.Ceiling(val.A), Vert.Ceiling(val.B), Vert.Ceiling(val.C), Vert.Ceiling(val.D));
    public static Quadrilateral Clamp(Quadrilateral val, Quadrilateral min, Quadrilateral max) =>
        new(Vert.Clamp(val.A, min.A, max.A), Vert.Clamp(val.B, min.B, max.B), Vert.Clamp(val.C, min.C, max.C),
            Vert.Clamp(val.D, min.D, max.D));
    public static Quadrilateral Floor(Quadrilateral val) =>
        new(Vert.Floor(val.A), Vert.Floor(val.B), Vert.Floor(val.C), Vert.Floor(val.D));
    public static Quadrilateral Lerp(Quadrilateral a, Quadrilateral b, float t, bool clamp = true) =>
        new(Vert.Lerp(a.A, b.A, t, clamp), Vert.Lerp(a.B, b.B, t, clamp), Vert.Lerp(a.C, b.C, t, clamp),
            Vert.Lerp(a.D, b.D, t, clamp));
    public static Quadrilateral Max(params Quadrilateral[] vals)
    {
        (Vert[] As, Vert[] Bs, Vert[] Cs, Vert[] Ds) = SplitVertArray(vals);
        return new(Vert.Max(As), Vert.Max(Bs), Vert.Max(Cs), Vert.Max(Ds));
    }
    public static Quadrilateral Median(params Quadrilateral[] vals)
    {
        (Vert[] As, Vert[] Bs, Vert[] Cs, Vert[] Ds) = SplitVertArray(vals);
        return new(Vert.Median(As), Vert.Median(Bs), Vert.Median(Cs), Vert.Median(Ds));
    }
    public static Quadrilateral Min(params Quadrilateral[] vals)
    {
        (Vert[] As, Vert[] Bs, Vert[] Cs, Vert[] Ds) = SplitVertArray(vals);
        return new(Vert.Min(As), Vert.Min(Bs), Vert.Min(Cs), Vert.Min(Ds));
    }

    public static (Vert[] As, Vert[] Bs, Vert[] Cs, Vert[] Ds) SplitVertArray(params Quadrilateral[] quads)
        {
            Vert[] a = new Vert[quads.Length], b = new Vert[quads.Length],
                   c = new Vert[quads.Length], d = new Vert[quads.Length];
            for (int i = 0; i < quads.Length; i++)
            {
                a[i] = quads[i].A;
                b[i] = quads[i].B;
                c[i] = quads[i].C;
                d[i] = quads[i].D;
            }

            return (a, b, c, d);
        }
    public static (Line[] ABs, Line[] BCs, Line[] CDs, Line[] DAs) SplitLineArray(params Quadrilateral[] quads)
        {
            Line[] ab = new Line[quads.Length], bc = new Line[quads.Length],
                   cd = new Line[quads.Length], da = new Line[quads.Length];
            for (int i = 0; i < quads.Length; i++)
            {
                ab[i] = quads[i].AB;
                bc[i] = quads[i].BC;
                cd[i] = quads[i].CD;
                da[i] = quads[i].DA;
            }

            return (ab, bc, cd, da);
        }

    public static float[] ToDoubleArrayAll(params Quadrilateral[] quads)
        {
            float[] vals = new float[quads.Length * 12];
            for (int i = 0; i < quads.Length; i++)
            {
                int pos = i * 12;
                vals[pos + 0] = quads[i].A.position.x;
                vals[pos + 1] = quads[i].A.position.y;
                vals[pos + 2] = quads[i].A.position.z;
                vals[pos + 3] = quads[i].B.position.x;
                vals[pos + 4] = quads[i].B.position.y;
                vals[pos + 5] = quads[i].B.position.z;
                vals[pos + 6] = quads[i].C.position.x;
                vals[pos + 7] = quads[i].C.position.y;
                vals[pos + 8] = quads[i].C.position.z;
                vals[pos + 9] = quads[i].D.position.x;
                vals[pos + 10] = quads[i].D.position.y;
                vals[pos + 11] = quads[i].D.position.z;
            }
            return vals;
        }
    public static List<float> ToDoubleListAll(params Quadrilateral[] quads) => new(ToDoubleArrayAll(quads));

    public override bool Equals([NotNullWhen(true)] object? obj)
    {
        if (obj == null || obj.GetType() != typeof(Quadrilateral)) return false;
        return Equals((Quadrilateral)obj);
    }
    public bool Equals(Quadrilateral other) => A == other.A && B == other.B && C == other.C && D == other.D;
    public override int GetHashCode() => A.GetHashCode() ^ B.GetHashCode() ^ C.GetHashCode() ^ D.GetHashCode();
    public override string ToString() => ToString((string?)null);
    public string ToString(string? provider) => "A: " + A.ToString(provider) + " B: " + B.ToString(provider)
        + " C: " + C.ToString(provider) + " D: " + D.ToString(provider);
    public string ToString(IFormatProvider provider) => "A: " + A.ToString(provider) + " B: "
        + B.ToString(provider) + " C: " + C.ToString(provider) + " D: " + D.ToString(provider);

    public object Clone() => new Quadrilateral(A, B, C, D);

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    public IEnumerator<Vert> GetEnumerator()
    {
        yield return A;
        yield return B;
        yield return C;
        yield return D;
    }

    public Vert[] ToArray() => new Vert[] { A, B, C, D };
    public List<Vert> ToList() => new() { A, B, C, D };

    public float[] ToDoubleArray() => new float[] { A.position.x, A.position.y, A.position.z,
                                                      B.position.x, B.position.y, B.position.z,
                                                      C.position.x, C.position.y, C.position.z,
                                                      D.position.x, D.position.y, D.position.z };
    public List<float> ToDoubleList() => new() { A.position.x, A.position.y, A.position.z,
                                                  B.position.x, B.position.y, B.position.z,
                                                  C.position.x, C.position.y, C.position.z,
                                                  D.position.x, D.position.y, D.position.z };

    public Triangle[] Triangulate() => new Line(A, C).Length > new Line(B, D).Length ?
        new Triangle[] { new(A, B, C), new(C, D, A) } : new Triangle[] { new(B, C, D), new(D, A, B) };

    public static Quadrilateral operator +(Quadrilateral a, Quadrilateral b) => new(a.A + b.A, a.B + b.B,
                                                                                    a.C + b.C, a.D + b.D);
    public static Quadrilateral operator +(Quadrilateral a, Vert b) => new(a.A + b, a.B + b, a.C + b, a.D + b);
    public static Quadrilateral operator -(Quadrilateral q) => new(-q.A, -q.B, -q.C, -q.D);
    public static Quadrilateral operator -(Quadrilateral a, Quadrilateral b) => new(a.A - b.A, a.B - b.B,
                                                                                    a.C - b.C, a.D - b.D);
    public static Quadrilateral operator -(Quadrilateral a, Vert b) => new(a.A - b, a.B - b, a.C - b, a.D - b);
    public static Quadrilateral operator *(Quadrilateral a, Quadrilateral b) => new(a.A * b.A, a.B * b.B,
                                                                                    a.C * b.C, a.D * b.D);
    public static Quadrilateral operator *(Quadrilateral a, Vert b) => new(a.A * b, a.B * b, a.C * b, a.D * b);
    public static Quadrilateral operator *(Quadrilateral a, float b) => new(a.A * b, a.B * b, a.C * b, a.D * b);
    public static Quadrilateral operator /(Quadrilateral a, Quadrilateral b) => new(a.A / b.A, a.B / b.B,
                                                                                    a.C / b.C, a.D / b.D);
    public static Quadrilateral operator /(Quadrilateral a, Vert b) => new(a.A / b, a.B / b, a.C / b, a.D / b);
    public static Quadrilateral operator /(Quadrilateral a, float b) => new(a.A / b, a.B / b, a.C / b, a.D / b);
    public static bool operator ==(Quadrilateral a, Quadrilateral b) => a.Equals(b);
    public static bool operator !=(Quadrilateral a, Quadrilateral b) => !a.Equals(b);

    public static implicit operator Quadrilateral(Fill<Vert> fill) => new(fill);
    public static implicit operator Quadrilateral(Fill<Float3> fill) => new(fill);
    public static implicit operator Quadrilateral(Fill<Int3> fill) => new(fill);
    public static implicit operator Quadrilateral(Fill<Line> fill) => new(fill);
    public static implicit operator Quadrilateral(Fill<float> fill) => new(fill);
    public static implicit operator Quadrilateral(Fill<int> fill) => new(fill);
    public static explicit operator Quadrilateral(Polygon poly) => new(poly.Lines[0], poly.Lines[1],
                                                                       poly.Lines[2], poly.Lines[3]);
}
