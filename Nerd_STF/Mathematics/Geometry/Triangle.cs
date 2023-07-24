using System.Net.Security;

namespace Nerd_STF.Mathematics.Geometry;

public record class Triangle : IAbsolute<Triangle>, IAverage<Triangle>, ICeiling<Triangle>, IClamp<Triangle>,
    IEquatable<Triangle>, IFloor<Triangle>, IFromTuple<Triangle, (Float3 a, Float3 b, Float3 c)>, IGroup<Float3>,
    IIndexAll<Float3>, IIndexRangeAll<Float3>, ILerp<Triangle, float>, IRound<Triangle>, IShape2d<float>
{
    public Float3 A
    {
        get => p_a;
        set
        {
            p_a = value;
            p_ab.a = value;
            p_ca.b = value;
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

    private Float3 p_a, p_b, p_c;
    private Line p_ab, p_bc, p_ca;

    [Obsolete("This field doesn't account for the Z-axis. This will be fixed in v2.4.0")]
    public float Area => (float)Mathf.Absolute((A.x * B.y) + (B.x * C.y) +
        (C.x * A.y) - ((B.x * A.y) + (C.x * B.y) +
        (A.x * C.y))) * 0.5f;
    public Float3 Midpoint => Float3.Average(A, B, C);
    public float Perimeter => AB.Length + BC.Length + CA.Length;

    public Triangle(Float3 a, Float3 b, Float3 c)
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
    public Triangle(float x1, float y1, float x2, float y2, float x3, float y3)
        : this(new Float3(x1, y1), new Float3(x2, y2), new Float3(x3, y3)) { }
    public Triangle(float x1, float y1, float z1, float x2, float y2, float z2, float x3, float y3,
        float z3) : this(new Float3(x1, y1, z1), new Float3(x2, y2, z2), new Float3(x3, y3, z3)) { }
    public Triangle(Fill<Float3> fill) : this(fill(0), fill(1), fill(2)) { }
    public Triangle(Fill<Int3> fill) : this(fill(0), fill(1), fill(2)) { }
    public Triangle(Fill<Line> fill) : this(fill(0), fill(1), fill(2)) { }
    public Triangle(Fill<float> fill) : this(fill(0), fill(1), fill(2), fill(3), fill(4), fill(5), fill(6),
        fill(7), fill(8)) { }
    public Triangle(Fill<int> fill) : this(fill(0), fill(1), fill(2), fill(3), fill(4), fill(5), fill(6),
        fill(7), fill(8)) { }

    public Float3 this[int index]
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

    public static Triangle Absolute(Triangle val) =>
        new(Float3.Absolute(val.A), Float3.Absolute(val.B), Float3.Absolute(val.C));
    public static Triangle Average(params Triangle[] vals)
    {
        (Float3[] As, Float3[] Bs, Float3[] Cs) = SplitFloat3Array(vals);
        return new(Float3.Average(As), Float3.Average(Bs), Float3.Average(Cs));
    }
    public static Triangle Ceiling(Triangle val) =>
        new(Float3.Ceiling(val.A), Float3.Ceiling(val.B), Float3.Ceiling(val.C));
    public static Triangle Clamp(Triangle val, Triangle min, Triangle max) =>
        new(Float3.Clamp(val.A, min.A, max.A), Float3.Clamp(val.B, min.B, max.B), Float3.Clamp(val.C, min.C, max.C));
    public static Triangle Floor(Triangle val) =>
        new(Float3.Floor(val.A), Float3.Floor(val.B), Float3.Floor(val.C));
    public static Triangle Lerp(Triangle a, Triangle b, float t, bool clamp = true) =>
        new(Float3.Lerp(a.A, b.A, t, clamp), Float3.Lerp(a.B, b.B, t, clamp), Float3.Lerp(a.C, b.C, t, clamp));
    public static Triangle Max(params Triangle[] vals)
    {
        (Float3[] As, Float3[] Bs, Float3[] Cs) = SplitFloat3Array(vals);
        return new(Float3.Max(As), Float3.Max(Bs), Float3.Max(Cs));
    }
    public static Triangle Median(params Triangle[] vals)
    {
        (Float3[] As, Float3[] Bs, Float3[] Cs) = SplitFloat3Array(vals);
        return new(Float3.Median(As), Float3.Median(Bs), Float3.Median(Cs));
    }
    public static Triangle Min(params Triangle[] vals)
    {
        (Float3[] As, Float3[] Bs, Float3[] Cs) = SplitFloat3Array(vals);
        return new(Float3.Min(As), Float3.Min(Bs), Float3.Min(Cs));
    }
    public static Triangle Round(Triangle val) =>
        new(Float3.Round(val.A), Float3.Round(val.B), Float3.Round(val.C));

    public static (Float3[] As, Float3[] Bs, Float3[] Cs) SplitFloat3Array(params Triangle[] tris)
    {
        Float3[] a = new Float3[tris.Length], b = new Float3[tris.Length], c = new Float3[tris.Length];
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

    public static float[] ToFloatArrayAll(params Triangle[] tris)
    {
        float[] vals = new float[tris.Length * 9];
        for (int i = 0; i < tris.Length; i++)
        {
            int pos = i * 9;
            vals[pos + 0] = tris[i].A.x;
            vals[pos + 1] = tris[i].A.y;
            vals[pos + 2] = tris[i].A.z;
            vals[pos + 3] = tris[i].B.x;
            vals[pos + 4] = tris[i].B.y;
            vals[pos + 5] = tris[i].B.z;
            vals[pos + 6] = tris[i].C.x;
            vals[pos + 7] = tris[i].C.y;
            vals[pos + 8] = tris[i].C.z;
        }
        return vals;
    }
    public static List<float> ToFloatListAll(params Triangle[] tris) => new(ToFloatArrayAll(tris));

    public virtual bool Equals(Triangle? other)
    {
        if (other is null) return false;
        return A == other.A && B == other.B && C == other.C;
    }
    public override int GetHashCode() => base.GetHashCode();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    public IEnumerator<Float3> GetEnumerator()
    {
        yield return A;
        yield return B;
        yield return C;
    }

    public Float3[] ToArray() => new Float3[] { A, B, C };
    public Fill<Float3> ToFill()
    {
        Triangle @this = this;
        return i => @this[i];
    }
    public List<Float3> ToList() => new() { A, B, C };

    public float[] ToFloatArray() => new float[] { A.x, A.y, A.z,
                                                   B.x, B.y, B.z,
                                                   C.x, C.y, C.z };
    public List<float> ToFloatList() => new() { A.x, A.y, A.z,
                                                B.x, B.y, B.z,
                                                C.x, C.y, C.z };

    protected virtual bool PrintMembers(StringBuilder builder)
    {
        builder.Append("A = ");
        builder.Append(A);
        builder.Append(", B = ");
        builder.Append(B);
        builder.Append(", C = ");
        builder.Append(C);
        return true;
    }

    public static Triangle operator +(Triangle a, Triangle b) => new(a.A + b.A, a.B + b.B, a.C + b.C);
    public static Triangle operator +(Triangle a, Float3 b) => new(a.A + b, a.B + b, a.C + b);
    public static Triangle operator -(Triangle t) => new(-t.A, -t.B, -t.C);
    public static Triangle operator -(Triangle a, Triangle b) => new(a.A - b.A, a.B - b.B, a.C - b.C);
    public static Triangle operator -(Triangle a, Float3 b) => new(a.A - b, a.B - b, a.C - b);
    public static Triangle operator *(Triangle a, Triangle b) => new(a.A * b.A, a.B * b.B, a.C * b.C);
    public static Triangle operator *(Triangle a, Float3 b) => new(a.A * b, a.B * b, a.C * b);
    public static Triangle operator *(Triangle a, float b) => new(a.A * b, a.B * b, a.C * b);
    public static Triangle operator /(Triangle a, Triangle b) => new(a.A / b.A, a.B / b.B, a.C / b.C);
    public static Triangle operator /(Triangle a, Float3 b) => new(a.A / b, a.B / b, a.C / b);
    public static Triangle operator /(Triangle a, float b) => new(a.A / b, a.B / b, a.C / b);

    public static implicit operator Triangle(Fill<Float3> fill) => new(fill);
    public static implicit operator Triangle(Fill<Int3> fill) => new(fill);
    public static implicit operator Triangle(Fill<Line> fill) => new(fill);
    public static implicit operator Triangle(Fill<float> fill) => new(fill);
    public static implicit operator Triangle(Fill<int> fill) => new(fill);
    public static implicit operator Triangle((Float3 a, Float3 b, Float3 c) val) =>
        new(val.a, val.b, val.c);
}
