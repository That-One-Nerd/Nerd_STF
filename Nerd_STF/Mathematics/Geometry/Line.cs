namespace Nerd_STF.Mathematics.Geometry;

public struct Line : ICloneable, IClosest<Vert>, IComparable<Line>, IContainer<Vert>, IEquatable<Line>,
    IGroup<Vert>, ISubdividable<Line[]>
{
    public static Line Back => new(Vert.Zero, Vert.Back);
    public static Line Down => new(Vert.Zero, Vert.Down);
    public static Line Forward => new(Vert.Zero, Vert.Forward);
    public static Line Left => new(Vert.Zero, Vert.Left);
    public static Line Right => new(Vert.Zero, Vert.Right);
    public static Line Up => new(Vert.Zero, Vert.Up);

    public static Line One => new(Vert.Zero, Vert.One);
    public static Line Zero => new(Vert.Zero, Vert.Zero);

    public double Length => (b - a).Magnitude;

    public Vert a, b;

    public Line(Vert a, Vert b)
    {
        this.a = a;
        this.b = b;
    }
    public Line(double x1, double y1, double x2, double y2) : this(new(x1, y1), new(x2, y2)) { }
    public Line(double x1, double y1, double z1, double x2, double y2, double z2)
        : this(new(x1, y1, z1), new(x2, y2, z2)) { }
    public Line(Fill<Vert> fill) : this(fill(0), fill(1)) { }
    public Line(Fill<Double3> fill) : this(new(fill(0)), new(fill(1))) { }
    public Line(Fill<Int3> fill) : this(new(fill(0)), new(fill(1))) { }
    public Line(Fill<double> fill) : this(new(fill(0), fill(1), fill(2)), new(fill(3), fill(4), fill(5))) { }
    public Line(Fill<int> fill) : this(new(fill(0), fill(1), fill(2)), new(fill(3), fill(4), fill(5))) { }

    public Vert this[int index]
    {
        get => index switch
        {
            0 => a,
            1 => b,
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

                default: throw new IndexOutOfRangeException(nameof(index));
            }
        }
    }

    public static Line Absolute(Line val) => new(Vert.Absolute(val.a), Vert.Absolute(val.b));
    public static Line Average(params Line[] vals)
    {
        (Vert[] starts, Vert[] ends) = SplitArray(vals);
        return new(Vert.Average(starts), Vert.Average(ends));
    }
    public static Line Ceiling(Line val) => new(Vert.Ceiling(val.a), Vert.Ceiling(val.b));
    public static Line Clamp(Line val, Line min, Line max) =>
        new(Vert.Clamp(val.a, min.a, max.a), Vert.Clamp(val.b, min.b, max.b));
    public static Line Floor(Line val) => new(Vert.Floor(val.a), Vert.Floor(val.b));
    public static Line Lerp(Line a, Line b, double t, bool clamp = true) =>
        new(Vert.Lerp(a.a, b.a, t, clamp), Vert.Lerp(a.b, b.b, t, clamp));
    public static Line Median(params Line[] vals)
    {
        (Vert[] starts, Vert[] ends) = SplitArray(vals);
        return new(Vert.Median(starts), Vert.Median(ends));
    }
    public static Line Max(params Line[] vals)
    {
        (Vert[] starts, Vert[] ends) = SplitArray(vals);
        return new(Vert.Max(starts), Vert.Max(ends));
    }
    public static Line Min(params Line[] vals)
    {
        (Vert[] starts, Vert[] ends) = SplitArray(vals);
        return new(Vert.Min(starts), Vert.Min(ends));
    }

    public static (Vert[] starts, Vert[] ends) SplitArray(params Line[] lines)
    {
        Vert[] starts = new Vert[lines.Length], ends = new Vert[lines.Length];
        for (int i = 0; i < lines.Length; i++)
        {
            starts[i] = lines[i].a;
            ends[i] = lines[i].b;
        }
        return (starts, ends);
    }

    public override bool Equals([NotNullWhen(true)] object? obj)
    {
        if (obj == null || obj.GetType() != typeof(Line)) return false;
        return Equals((Line)obj);
    }
    public bool Equals(Line other) => a == other.a && b == other.b;
    public override int GetHashCode() => a.GetHashCode() ^ b.GetHashCode();
    public override string ToString() => ToString((string?)null);
    public string ToString(string? provider) =>
        "A: " + a.ToString(provider) + " B: " + b.ToString(provider);
    public string ToString(IFormatProvider provider) =>
        "A: " + a.ToString(provider) + " B: " + b.ToString(provider);

    public object Clone() => new Line(a, b);

    public int CompareTo(Line line) => Length.CompareTo(line.Length);

    public bool Contains(Vert vert)
    {
        Double3 diffA = a - vert, diffB = a - b;
        double lerpVal = diffA.Magnitude / diffB.Magnitude;
        return Vert.Lerp(a, b, lerpVal) == vert;
    }

    public Vert ClosestTo(Vert vert) => ClosestTo(vert, Calculus.DefaultStep);
    public Vert ClosestTo(Vert vert, double step)
    {
        Vert closestA = a, closestB = b;
        for (double t = 0; t <= 1; t += step)
        {
            Vert valA = Vert.Lerp(a, b, t);
            Vert valB = Vert.Lerp(b, a, t);
            closestA = (valA - vert).Magnitude < (closestA - vert).Magnitude ? valA : closestA;
            closestB = (valB - vert).Magnitude < (closestB - vert).Magnitude ? valB : closestB;
        }

        return (closestA - vert).Magnitude >= (closestB - vert).Magnitude ? closestA : closestB;
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    public IEnumerator<Vert> GetEnumerator()
    {
        yield return a;
        yield return b;
    }

    public Line[] Subdivide()
    {
        Vert middle = Vert.Lerp(a, b, 0.5);
        return new Line[] { new(a, middle), new(middle, b) };
    }
    public Line[] Subdivide(int iterations)
    {
        if (iterations < 1) return Array.Empty<Line>();
        List<Line> lines = new(Subdivide());
        for (int i = 1; i < iterations; i++)
        {
            List<Line> add = new();
            for (int j = 0; j < lines.Count; j++) add.AddRange(lines[j].Subdivide());
            lines = add;
        }
        return lines.ToArray();
    }

    public Vert[] ToArray() => new Vert[] { a, b };
    public List<Vert> ToList() => new() { a, b };

    public double[] ToDoubleArray() => new double[] { a.position.x, a.position.y, a.position.z,
                                                      b.position.x, b.position.y, b.position.z };
    public List<double> ToDoubleList() => new() { a.position.x, a.position.y, a.position.z,
                                                  b.position.x, b.position.y, b.position.z };

    public static Line operator +(Line a, Line b) => new(a.a + b.a, a.b + b.b);
    public static Line operator +(Line a, Vert b) => new(a.a + b, a.b + b);
    public static Line operator -(Line l) => new(-l.a, -l.b);
    public static Line operator -(Line a, Line b) => new(a.a - b.a, a.b - b.b);
    public static Line operator -(Line a, Vert b) => new(a.a - b, a.b - b);
    public static Line operator *(Line a, Line b) => new(a.a * b.a, a.b * b.b);
    public static Line operator *(Line a, Vert b) => new(a.a * b, a.b * b);
    public static Line operator *(Line a, double b) => new(a.a * b, a.b * b);
    public static Line operator /(Line a, Line b) => new(a.a / b.a, a.b / b.b);
    public static Line operator /(Line a, Vert b) => new(a.a / b, a.b / b);
    public static Line operator /(Line a, double b) => new(a.a / b, a.b / b);
    public static bool operator ==(Line a, Line b) => a.Equals(b);
    public static bool operator !=(Line a, Line b) => !a.Equals(b);
    public static bool operator >(Line a, Line b) => a.CompareTo(b) > 0;
    public static bool operator <(Line a, Line b) => a.CompareTo(b) < 0;
    public static bool operator >=(Line a, Line b) => a > b || a == b;
    public static bool operator <=(Line a, Line b) => a < b || a == b;

    public static implicit operator Line(Fill<Vert> fill) => new(fill);
    public static implicit operator Line(Fill<Double3> fill) => new(fill);
    public static implicit operator Line(Fill<Int3> fill) => new(fill);
    public static implicit operator Line(Fill<double> fill) => new(fill);
    public static implicit operator Line(Fill<int> fill) => new(fill);
}
