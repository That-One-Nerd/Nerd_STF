namespace Nerd_STF.Mathematics.Geometry;

public record class Quadrilateral : IAbsolute<Quadrilateral>, IAverage<Quadrilateral>, ICeiling<Quadrilateral>,
    IClamp<Quadrilateral>, IEquatable<Quadrilateral>, IFloor<Quadrilateral>,
    IFromTuple<Quadrilateral, (Float3 a, Float3 b, Float3 c, Float3 d)>, IGroup<Float3>, IIndexAll<Float3>, IIndexRangeAll<Float3>,
    ILerp<Quadrilateral, float>, IRound<Quadrilateral>, IShape2d<float>, ITriangulate
{
    public Float3 A
    {
        get => p_a;
        set
        {
            p_a = value;
            p_ab.a = value;
            p_da.b = value;
        }
    }
    public Float3 B
    {
        get => p_b;
        set
        {
            p_b = value;
            p_ab.b = value;
            p_bc.a = value;
        }
    }
    public Float3 C
    {
        get => p_c;
        set
        {
            p_c = value;
            p_bc.b = value;
            p_cd.a = value;
        }
    }
    public Float3 D
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

    private Float3 p_a, p_b, p_c, p_d;
    private Line p_ab, p_bc, p_cd, p_da;

    [Obsolete("This field doesn't account for the Z-axis. This will be fixed in v2.4.0")]
    public float Area
    {
        get
        {
            float val = 0;
            foreach (Triangle t in Triangulate()) val += t.Area;
            return val;
        }
    }
    public Float3 Midpoint => Float3.Average(A, B, C, D);
    public float Perimeter => AB.Length + BC.Length + CD.Length + DA.Length;

    public Quadrilateral(Float3 a, Float3 b, Float3 c, Float3 d)
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
        : this(new Float3(x1, y1), new(x2, y2), new(x3, y3), new(x4, y4)) { }
    public Quadrilateral(float x1, float y1, float z1, float x2, float y2, float z2, float x3, float y3,
        float z3, float x4, float y4, float z4)
        : this(new Float3(x1, y1, z1), new(x2, y2, z2), new(x3, y3, z3), new(x4, y4, z4)) { }
    public Quadrilateral(Fill<Float3> fill) : this(fill(0), fill(1), fill(2), fill(3)) { }
    public Quadrilateral(Fill<Int3> fill) : this(fill(0), fill(1), fill(2), fill(3)) { }
    public Quadrilateral(Fill<Line> fill) : this(fill(0), fill(1), fill(2), fill(3)) { }
    public Quadrilateral(Fill<float> fill) : this(fill(0), fill(1), fill(2), fill(3), fill(4), fill(5), fill(6),
        fill(7), fill(8), fill(9), fill(10), fill(11)) { }
    public Quadrilateral(Fill<int> fill) : this(fill(0), fill(1), fill(2), fill(3), fill(4), fill(5), fill(6),
        fill(7), fill(8), fill(9), fill(10), fill(11)) { }

    public Float3 this[int index]
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

    public static Quadrilateral Absolute(Quadrilateral val) =>
        new(Float3.Absolute(val.A), Float3.Absolute(val.B), Float3.Absolute(val.C), Float3.Absolute(val.D));
    public static Quadrilateral Average(params Quadrilateral[] vals)
    {
        (Float3[] As, Float3[] Bs, Float3[] Cs, Float3[] Ds) = SplitVertArray(vals);
        return new(Float3.Average(As), Float3.Average(Bs), Float3.Average(Cs), Float3.Average(Ds));
    }
    public static Quadrilateral Ceiling(Quadrilateral val) =>
        new(Float3.Ceiling(val.A), Float3.Ceiling(val.B), Float3.Ceiling(val.C), Float3.Ceiling(val.D));
    public static Quadrilateral Clamp(Quadrilateral val, Quadrilateral min, Quadrilateral max) =>
        new(Float3.Clamp(val.A, min.A, max.A), Float3.Clamp(val.B, min.B, max.B), Float3.Clamp(val.C, min.C, max.C),
            Float3.Clamp(val.D, min.D, max.D));
    public static Quadrilateral Floor(Quadrilateral val) =>
        new(Float3.Floor(val.A), Float3.Floor(val.B), Float3.Floor(val.C), Float3.Floor(val.D));
    public static Quadrilateral Lerp(Quadrilateral a, Quadrilateral b, float t, bool clamp = true) =>
        new(Float3.Lerp(a.A, b.A, t, clamp), Float3.Lerp(a.B, b.B, t, clamp), Float3.Lerp(a.C, b.C, t, clamp),
            Float3.Lerp(a.D, b.D, t, clamp));
    public static Quadrilateral Max(params Quadrilateral[] vals)
    {
        (Float3[] As, Float3[] Bs, Float3[] Cs, Float3[] Ds) = SplitVertArray(vals);
        return new(Float3.Max(As), Float3.Max(Bs), Float3.Max(Cs), Float3.Max(Ds));
    }
    public static Quadrilateral Median(params Quadrilateral[] vals)
    {
        (Float3[] As, Float3[] Bs, Float3[] Cs, Float3[] Ds) = SplitVertArray(vals);
        return new(Float3.Median(As), Float3.Median(Bs), Float3.Median(Cs), Float3.Median(Ds));
    }
    public static Quadrilateral Min(params Quadrilateral[] vals)
    {
        (Float3[] As, Float3[] Bs, Float3[] Cs, Float3[] Ds) = SplitVertArray(vals);
        return new(Float3.Min(As), Float3.Min(Bs), Float3.Min(Cs), Float3.Min(Ds));
    }
    public static Quadrilateral Round(Quadrilateral val) =>
        new(Float3.Round(val.A), Float3.Round(val.B), Float3.Round(val.C), Float3.Round(val.D));

    public static (Float3[] As, Float3[] Bs, Float3[] Cs, Float3[] Ds) SplitVertArray(params Quadrilateral[] quads)
    {
        Float3[] a = new Float3[quads.Length], b = new Float3[quads.Length],
               c = new Float3[quads.Length], d = new Float3[quads.Length];
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

    public static float[] ToFloatArrayAll(params Quadrilateral[] quads)
    {
        float[] vals = new float[quads.Length * 12];
        for (int i = 0; i < quads.Length; i++)
        {
            int pos = i * 12;
            vals[pos + 0] = quads[i].A.x;
            vals[pos + 1] = quads[i].A.y;
            vals[pos + 2] = quads[i].A.z;
            vals[pos + 3] = quads[i].B.x;
            vals[pos + 4] = quads[i].B.y;
            vals[pos + 5] = quads[i].B.z;
            vals[pos + 6] = quads[i].C.x;
            vals[pos + 7] = quads[i].C.y;
            vals[pos + 8] = quads[i].C.z;
            vals[pos + 9] = quads[i].D.x;
            vals[pos + 10] = quads[i].D.y;
            vals[pos + 11] = quads[i].D.z;
        }
        return vals;
    }
    public static List<float> ToFloatListAll(params Quadrilateral[] quads) => new(ToFloatArrayAll(quads));

    public virtual bool Equals(Quadrilateral? other)
    {
        if (other is null) return false;
        return A == other.A && B == other.B && C == other.C && D == other.D;
    }
    public override int GetHashCode() => base.GetHashCode();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    public IEnumerator<Float3> GetEnumerator()
    {
        yield return A;
        yield return B;
        yield return C;
        yield return D;
    }

    public Float3[] ToArray() => new Float3[] { A, B, C, D };
    public Fill<Float3> ToFill()
    {
        Quadrilateral @this = this;
        return i => @this[i];
    }
    public List<Float3> ToList() => new() { A, B, C, D };

    public float[] ToFloatArray() => new float[] { A.x, A.y, A.z,
                                                   B.x, B.y, B.z,
                                                   C.x, C.y, C.z,
                                                   D.x, D.y, D.z };
    public List<float> ToFloatList() => new() { A.x, A.y, A.z,
                                                B.x, B.y, B.z,
                                                C.x, C.y, C.z,
                                                D.x, D.y, D.z };

    public Triangle[] Triangulate() => new Line(A, C).Length > new Line(B, D).Length ?
        new Triangle[] { new(A, B, C), new(C, D, A) } : new Triangle[] { new(B, C, D), new(D, A, B) };

    protected virtual bool PrintMembers(StringBuilder builder)
    {
        builder.Append("A = ");
        builder.Append(A);
        builder.Append(", B = ");
        builder.Append(B);
        builder.Append(", C = ");
        builder.Append(C);
        builder.Append(", D = ");
        builder.Append(D);
        return true;
    }

    public static Quadrilateral operator +(Quadrilateral a, Quadrilateral b) => new(a.A + b.A, a.B + b.B,
                                                                                    a.C + b.C, a.D + b.D);
    public static Quadrilateral operator +(Quadrilateral a, Float3 b) => new(a.A + b, a.B + b, a.C + b, a.D + b);
    public static Quadrilateral operator -(Quadrilateral q) => new(-q.A, -q.B, -q.C, -q.D);
    public static Quadrilateral operator -(Quadrilateral a, Quadrilateral b) => new(a.A - b.A, a.B - b.B,
                                                                                    a.C - b.C, a.D - b.D);
    public static Quadrilateral operator -(Quadrilateral a, Float3 b) => new(a.A - b, a.B - b, a.C - b, a.D - b);
    public static Quadrilateral operator *(Quadrilateral a, Quadrilateral b) => new(a.A * b.A, a.B * b.B,
                                                                                    a.C * b.C, a.D * b.D);
    public static Quadrilateral operator *(Quadrilateral a, Float3 b) => new(a.A * b, a.B * b, a.C * b, a.D * b);
    public static Quadrilateral operator *(Quadrilateral a, float b) => new(a.A * b, a.B * b, a.C * b, a.D * b);
    public static Quadrilateral operator /(Quadrilateral a, Quadrilateral b) => new(a.A / b.A, a.B / b.B,
                                                                                    a.C / b.C, a.D / b.D);
    public static Quadrilateral operator /(Quadrilateral a, Float3 b) => new(a.A / b, a.B / b, a.C / b, a.D / b);
    public static Quadrilateral operator /(Quadrilateral a, float b) => new(a.A / b, a.B / b, a.C / b, a.D / b);

    public static implicit operator Quadrilateral(Fill<Float3> fill) => new(fill);
    public static implicit operator Quadrilateral(Fill<Int3> fill) => new(fill);
    public static implicit operator Quadrilateral(Fill<Line> fill) => new(fill);
    public static implicit operator Quadrilateral(Fill<float> fill) => new(fill);
    public static implicit operator Quadrilateral(Fill<int> fill) => new(fill);
    public static implicit operator Quadrilateral((Float3 a, Float3 b, Float3 c, Float3 d) val) =>
        new(val.a, val.b, val.c, val.d);
}
