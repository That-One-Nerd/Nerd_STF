namespace Nerd_STF.Mathematics.Geometry;

public class Triangle : IClosestTo<Float3>, IContains<Float3>,
    IFromTuple<Triangle, (Float3 a, Float3 b, Float3 c)>, IPolygon<Triangle>,
    ISubdivide<Triangle>, IWithinRange<Triangle>
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
