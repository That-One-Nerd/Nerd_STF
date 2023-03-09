using Nerd_STF.Mathematics.Abstract;

namespace Nerd_STF.Mathematics.Geometry;

public record class Line : IAbsolute<Line>, IAverage<Line>, ICeiling<Line>, IClamp<Line>, IClosestTo<Vert>,
    IComparable<Line>, IContains<Vert>, IEquatable<Line>, IFloor<Line>, IFromTuple<Line, (Vert start, Vert end)>,
    IGroup<Vert>, IIndexAll<Vert>, IIndexRangeAll<Vert>, ILerp<Line, float>, IMedian<Line>, IPresets3D<Line>,
    IRound<Line>, ISplittable<Line, (Vert[] starts, Vert[] ends)>, ISubdivide<Line[]>
{
    public static Line Back => new(Vert.Zero, Vert.Back);
    public static Line Down => new(Vert.Zero, Vert.Down);
    public static Line Forward => new(Vert.Zero, Vert.Forward);
    public static Line Left => new(Vert.Zero, Vert.Left);
    public static Line Right => new(Vert.Zero, Vert.Right);
    public static Line Up => new(Vert.Zero, Vert.Up);

    public static Line One => new(Vert.Zero, Vert.One);
    public static Line Zero => new(Vert.Zero, Vert.Zero);

    public float Length => (b - a).Magnitude;
    public Vert Midpoint => Vert.Average(a, b);

    public Vert a, b;

    public Line(Vert a, Vert b)
    {
        this.a = a;
        this.b = b;
    }
    public Line(float x1, float y1, float x2, float y2) : this(new(x1, y1), new(x2, y2)) { }
    public Line(float x1, float y1, float z1, float x2, float y2, float z2)
        : this(new(x1, y1, z1), new(x2, y2, z2)) { }
    public Line(Fill<Vert> fill) : this(fill(0), fill(1)) { }
    public Line(Fill<Float3> fill) : this(new(fill(0)), new(fill(1))) { }
    public Line(Fill<Int3> fill) : this(new(fill(0)), new(fill(1))) { }
    public Line(Fill<float> fill) : this(new(fill(0), fill(1), fill(2)), new(fill(3), fill(4), fill(5))) { }
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
    public Vert this[Index index]
    {
        get => this[index.IsFromEnd ? 2 - index.Value : index.Value];
        set => this[index.IsFromEnd ? 2 - index.Value : index.Value] = value;
    }
    public Vert[] this[Range range]
    {
        get
        {
            int start = range.Start.IsFromEnd ? 2 - range.Start.Value : range.Start.Value;
            int end = range.End.IsFromEnd ? 2 - range.End.Value : range.End.Value;
            List<Vert> res = new();
            for (int i = start; i < end; i++) res.Add(this[i]);
            return res.ToArray();
        }
        set
        {
            int start = range.Start.IsFromEnd ? 2 - range.Start.Value : range.Start.Value;
            int end = range.End.IsFromEnd ? 2 - range.End.Value : range.End.Value;
            for (int i = start; i < end; i++) this[i] = value[i];
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
    public static Line Lerp(Line a, Line b, float t, bool clamp = true) =>
        new(Vert.Lerp(a.a, b.a, t, clamp), Vert.Lerp(a.b, b.b, t, clamp));
    public static Line Median(params Line[] vals)
    {
        (Vert[] starts, Vert[] ends) = SplitArray(vals);
        return new(Vert.Median(starts), Vert.Median(ends));
    }
    public static Line Round(Line val) => new(Vert.Round(val.a), Vert.Round(val.b));

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

    public virtual bool Equals(Line? other)
    {
        if (other is null) return false;
        return a == other.a && b == other.b;
    }
    public override int GetHashCode() => base.GetHashCode();

    [Obsolete("This method is a bit ambiguous. You should instead compare " +
        nameof(Length) + "s directly.")]
    public int CompareTo(Line? line)
    {
        if (line is null) return -1;
        return Length.CompareTo(line.Length);
    }

    public bool Contains(Vert vert)
    {
        Float3 diffA = a - vert, diffB = a - b;
        float lerpVal = diffA.Magnitude / diffB.Magnitude;
        return Vert.Lerp(a, b, lerpVal) == vert;
    }

    public Vert ClosestTo(Vert vert) => ClosestTo(vert, Calculus.DefaultStep);
    public Vert ClosestTo(Vert vert, float step)
    {
        Vert closestA = a, closestB = b;
        for (float t = 0; t <= 1; t += step)
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
        Vert middle = Vert.Lerp(a, b, 0.5f);
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
    public Fill<Vert> ToFill()
    {
        Line @this = this;
        return i => @this[i];
    }
    public List<Vert> ToList() => new() { a, b };

    public float[] ToFloatArray() => new float[] { a.position.x, a.position.y, a.position.z,
                                                   b.position.x, b.position.y, b.position.z };
    public List<float> ToFloatList() => new() { a.position.x, a.position.y, a.position.z,
                                                b.position.x, b.position.y, b.position.z };

    protected virtual bool PrintMembers(StringBuilder builder)
    {
        builder.Append("A = ");
        builder.Append(a);
        builder.Append(", B = ");
        builder.Append(b);
        return true;
    }

    public static Line operator +(Line a, Line b) => new(a.a + b.a, a.b + b.b);
    public static Line operator +(Line a, Vert b) => new(a.a + b, a.b + b);
    public static Line operator -(Line l) => new(-l.a, -l.b);
    public static Line operator -(Line a, Line b) => new(a.a - b.a, a.b - b.b);
    public static Line operator -(Line a, Vert b) => new(a.a - b, a.b - b);
    public static Line operator *(Line a, Line b) => new(a.a * b.a, a.b * b.b);
    public static Line operator *(Line a, Vert b) => new(a.a * b, a.b * b);
    public static Line operator *(Line a, float b) => new(a.a * b, a.b * b);
    public static Line operator /(Line a, Line b) => new(a.a / b.a, a.b / b.b);
    public static Line operator /(Line a, Vert b) => new(a.a / b, a.b / b);
    public static Line operator /(Line a, float b) => new(a.a / b, a.b / b);
    [Obsolete("This operator is a bit ambiguous. You should instead compare " +
        nameof(Length) + "s directly.")]
    public static bool operator >(Line a, Line b) => a.CompareTo(b) > 0;
    [Obsolete("This operator is a bit ambiguous. You should instead compare " +
        nameof(Length) + "s directly.")]
    public static bool operator <(Line a, Line b) => a.CompareTo(b) < 0;
    [Obsolete("This operator is a bit ambiguous (and misleading at times). " +
        "You should instead compare " + nameof(Length) + "s directly.")]
    public static bool operator >=(Line a, Line b) => a > b || a == b;
    [Obsolete("This operator is a bit ambiguous (and misleading at times). " +
        "You should instead compare " + nameof(Length) + "s directly.")]
    public static bool operator <=(Line a, Line b) => a < b || a == b;

    public static implicit operator Line(Fill<Vert> fill) => new(fill);
    public static implicit operator Line(Fill<Float3> fill) => new(fill);
    public static implicit operator Line(Fill<Int3> fill) => new(fill);
    public static implicit operator Line(Fill<float> fill) => new(fill);
    public static implicit operator Line(Fill<int> fill) => new(fill);
    public static implicit operator Line((Vert start, Vert end) val) => new(val.start, val.end);
}
