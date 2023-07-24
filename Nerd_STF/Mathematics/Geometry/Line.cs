using Nerd_STF.Mathematics.Abstract;

namespace Nerd_STF.Mathematics.Geometry;

public record class Line : IAbsolute<Line>, IAverage<Line>, ICeiling<Line>, IClamp<Line>, IClosestTo<Float3>,
    IComparable<Line>, IContains<Float3>, IEquatable<Line>, IFloor<Line>, IFromTuple<Line, (Float3 start, Float3 end)>,
    IGroup<Float3>, IIndexAll<Float3>, IIndexRangeAll<Float3>, ILerp<Line, float>, IMedian<Line>, IPresets3d<Line>,
    IRound<Line>, ISplittable<Line, (Float3[] starts, Float3[] ends)>, ISubdivide<Line[]>
{
    public static Line Back => new(Float3.Zero, Float3.Back);
    public static Line Down => new(Float3.Zero, Float3.Down);
    public static Line Forward => new(Float3.Zero, Float3.Forward);
    public static Line Left => new(Float3.Zero, Float3.Left);
    public static Line Right => new(Float3.Zero, Float3.Right);
    public static Line Up => new(Float3.Zero, Float3.Up);

    public static Line One => new(Float3.Zero, Float3.One);
    public static Line Zero => new(Float3.Zero, Float3.Zero);

    public float Length => (b - a).Magnitude;
    public Float3 Midpoint => Float3.Average(a, b);

    public Float3 a, b;

    public Line(Float3 a, Float3 b)
    {
        this.a = a;
        this.b = b;
    }
    public Line(float x1, float y1, float x2, float y2) : this(new(x1, y1), new(x2, y2)) { }
    public Line(float x1, float y1, float z1, float x2, float y2, float z2)
        : this(new(x1, y1, z1), new(x2, y2, z2)) { }
    public Line(Fill<Float3> fill) : this(fill(0), fill(1)) { }
    public Line(Fill<Int3> fill) : this(fill(0), fill(1)) { }
    public Line(Fill<float> fill) : this(new(fill(0), fill(1), fill(2)), new(fill(3), fill(4), fill(5))) { }
    public Line(Fill<int> fill) : this(new(fill(0), fill(1), fill(2)), new(fill(3), fill(4), fill(5))) { }

    public Float3 this[int index]
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
    public Float3 this[Index index]
    {
        get => this[index.IsFromEnd ? 2 - index.Value : index.Value];
        set => this[index.IsFromEnd ? 2 - index.Value : index.Value] = value;
    }
    public Float3[] this[Range range]
    {
        get
        {
            int start = range.Start.IsFromEnd ? 2 - range.Start.Value : range.Start.Value;
            int end = range.End.IsFromEnd ? 2 - range.End.Value : range.End.Value;
            List<Float3> res = new();
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

    public static Line Absolute(Line val) => new(Float3.Absolute(val.a), Float3.Absolute(val.b));
    public static Line Average(params Line[] vals)
    {
        (Float3[] starts, Float3[] ends) = SplitArray(vals);
        return new(Float3.Average(starts), Float3.Average(ends));
    }
    public static Line Ceiling(Line val) => new(Float3.Ceiling(val.a), Float3.Ceiling(val.b));
    public static Line Clamp(Line val, Line min, Line max) =>
        new(Float3.Clamp(val.a, min.a, max.a), Float3.Clamp(val.b, min.b, max.b));
    public static Line Floor(Line val) => new(Float3.Floor(val.a), Float3.Floor(val.b));
    public static Line Lerp(Line a, Line b, float t, bool clamp = true) =>
        new(Float3.Lerp(a.a, b.a, t, clamp), Float3.Lerp(a.b, b.b, t, clamp));
    public static Line Median(params Line[] vals)
    {
        (Float3[] starts, Float3[] ends) = SplitArray(vals);
        return new(Float3.Median(starts), Float3.Median(ends));
    }
    public static Line Round(Line val) => new(Float3.Round(val.a), Float3.Round(val.b));

    public static (Float3[] starts, Float3[] ends) SplitArray(params Line[] lines)
    {
        Float3[] starts = new Float3[lines.Length], ends = new Float3[lines.Length];
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

    public bool Contains(Float3 vert)
    {
        Float3 diffA = a - vert, diffB = a - b;
        float lerpVal = diffA.Magnitude / diffB.Magnitude;
        return Float3.Lerp(a, b, lerpVal) == vert;
    }

    public Float3 ClosestTo(Float3 vert) => ClosestTo(vert, Calculus.DefaultStep);
    public Float3 ClosestTo(Float3 vert, float step)
    {
        Float3 closestA = a, closestB = b;
        for (float t = 0; t <= 1; t += step)
        {
            Float3 valA = Float3.Lerp(a, b, t);
            Float3 valB = Float3.Lerp(b, a, t);
            closestA = (valA - vert).Magnitude < (closestA - vert).Magnitude ? valA : closestA;
            closestB = (valB - vert).Magnitude < (closestB - vert).Magnitude ? valB : closestB;
        }

        return (closestA - vert).Magnitude >= (closestB - vert).Magnitude ? closestA : closestB;
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    public IEnumerator<Float3> GetEnumerator()
    {
        yield return a;
        yield return b;
    }

    public Line[] Subdivide()
    {
        Float3 middle = Float3.Lerp(a, b, 0.5f);
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

    public Float3[] ToArray() => new Float3[] { a, b };
    public Fill<Float3> ToFill()
    {
        Line @this = this;
        return i => @this[i];
    }
    public List<Float3> ToList() => new() { a, b };

    public float[] ToFloatArray() => new float[] { a.x, a.y, a.z,
                                                   b.x, b.y, b.z };
    public List<float> ToFloatList() => new() { a.x, a.y, a.z,
                                                b.x, b.y, b.z };

    protected virtual bool PrintMembers(StringBuilder builder)
    {
        builder.Append("A = ");
        builder.Append(a);
        builder.Append(", B = ");
        builder.Append(b);
        return true;
    }

    public static Line operator +(Line a, Line b) => new(a.a + b.a, a.b + b.b);
    public static Line operator +(Line a, Float3 b) => new(a.a + b, a.b + b);
    public static Line operator -(Line l) => new(-l.a, -l.b);
    public static Line operator -(Line a, Line b) => new(a.a - b.a, a.b - b.b);
    public static Line operator -(Line a, Float3 b) => new(a.a - b, a.b - b);
    public static Line operator *(Line a, Line b) => new(a.a * b.a, a.b * b.b);
    public static Line operator *(Line a, Float3 b) => new(a.a * b, a.b * b);
    public static Line operator *(Line a, float b) => new(a.a * b, a.b * b);
    public static Line operator /(Line a, Line b) => new(a.a / b.a, a.b / b.b);
    public static Line operator /(Line a, Float3 b) => new(a.a / b, a.b / b);
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

    public static implicit operator Line(Fill<Float3> fill) => new(fill);
    public static implicit operator Line(Fill<Int3> fill) => new(fill);
    public static implicit operator Line(Fill<float> fill) => new(fill);
    public static implicit operator Line(Fill<int> fill) => new(fill);
    public static implicit operator Line((Float3 start, Float3 end) val) => new(val.start, val.end);
}
