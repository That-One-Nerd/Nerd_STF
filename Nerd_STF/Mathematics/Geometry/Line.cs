namespace Nerd_STF.Mathematics.Geometry;

public class Line : IAverage<Line>, IClosestTo<Float3>, IContains<Float3>, IEquatable<Line>,
    IFloatArray<Line>, IFromTuple<Line, (Float3 a, Float3 b)>, IGroup<Float3>,
    IIndexAll<Float3>, IIndexRangeAll<Float3>, ILerp<Line, float>, IMedian<Line>,
    IPresets3d<Line>, ISplittable<Line, (Float3[] As, Float3[] Bs)>, ISubdivide<Line[]>,
    IWithinRange<Float3, float>
{
    public static Line Back => (Float3.Zero, Float3.Back);
    public static Line Down => (Float3.Zero, Float3.Down);
    public static Line Forward => (Float3.Zero, Float3.Forward);
    public static Line Left => (Float3.Zero, Float3.Left);
    public static Line Right => (Float3.Zero, Float3.Right);
    public static Line Up => (Float3.Zero, Float3.Up);

    public static Line One => (Float3.Zero, Float3.One);
    public static Line Zero => (Float3.Zero, Float3.Zero);

    public Angle Angle => Mathf.ArcTan(Slope);
    public float Length => (b - a).Magnitude;
    public Float3 Midpoint => (a + b) / 2;
    public float Slope => (b.y - a.y) / (b.x - a.x);

    public Float3 a, b;

    public Line() : this(Float3.Zero, Float3.Zero) { }
    public Line(Float3 a, Float3 b)
    {
        this.a = a;
        this.b = b;
    }
    public Line(float x1, float y1, float x2, float y2)
    {
        a = (x1, y1, 0);
        b = (x2, y2, 0);
    }
    public Line(float x1, float y1, float z1, float x2, float y2, float z2)
    {
        a = (x1, y1, z1);
        b = (x2, y2, z2);
    }
    public Line(Fill<Float3> fill) : this(fill(0), fill(1)) { }
    public Line(Fill<Int3> fill) : this(fill(0), fill(1)) { }
    public Line(Fill<float> fill) : this(fill(0), fill(1), fill(2),
        fill(3), fill(4), fill(5)) { }
    public Line(Fill<int> fill) : this(fill(0), fill(1), fill(2),
        fill(3), fill(4), fill(5)) { }

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

    public static Line Average(params Line[] lines)
    {
        (Float3[] As, Float3[] Bs) = SplitArray(lines);
        return (Float3.Average(As), Float3.Average(Bs));
    }
    public static Line Lerp(Line a, Line b, float t, bool clamp = true) =>
        (Float3.Lerp(a.a, b.a, t, clamp), Float3.Lerp(a.b, b.b, t, clamp));
    public static Line Median(params Line[] lines)
    {
        (Float3[] As, Float3[] Bs) = SplitArray(lines);
        return (Float3.Median(As), Float3.Median(Bs));
    }
    public static (Float3[] As, Float3[] Bs) SplitArray(params Line[] lines)
    {
        Float3[] As = new Float3[lines.Length],
                 Bs = new Float3[lines.Length];

        for (int i = 0; i < lines.Length; i++)
        {
            As[i] = lines[i].a;
            Bs[i] = lines[i].b;
        }

        return (As, Bs);
    }

    public static float[] ToFloatArrayAll(params Line[] vals)
    {
        float[] result = new float[6 * vals.Length];
        for (int i = 0; i < vals.Length; i++)
        {
            int p = i * 6;
            result[p + 0] = vals[i].a.x;
            result[p + 1] = vals[i].a.y;
            result[p + 2] = vals[i].a.z;
            result[p + 3] = vals[i].b.x;
            result[p + 4] = vals[i].b.y;
            result[p + 5] = vals[i].b.z;
        }
        return result;
    }

    public Float3[] ToArray() => new[] { a, b };
    public Fill<Float3> ToFill()
    {
        Line @this = this;
        return i => @this[i];
    }
    public List<Float3> ToList() => new() { a, b };

    public float[] ToFloatArray() => new[] { a.x, a.y, a.z, b.x, b.y, b.z };

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    public IEnumerator<Float3> GetEnumerator()
    {
        yield return a;
        yield return b;
    }

    public bool Equals(Line? other) => other is not null && a == other.a && b == other.b;
    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        else if (obj is Line line) return Equals(line);
        else return false;
    }
    public override int GetHashCode() => base.GetHashCode();

    public override string ToString()
    {
        StringBuilder builder = new();
        builder.Append(nameof(Line));
        builder.Append(" { ");
        builder.Append(a);
        builder.Append(", ");
        builder.Append(b);
        builder.Append(" }");
        return builder.ToString();
    }

    public Float3 ClosestTo(Float3 point) => ClosestTo(point, Calculus.DefaultStep);
    public Float3 ClosestTo(Float3 point, float step)
    {
        // Probably could optimize this with some weird formula but whatever.
        // This isn't the optimization update.

        (Float3 point, float dist) min = (a, float.MaxValue);
        for (float f = 0; f < 1; f += step)
        {
            Float3 check = Float3.Lerp(a, b, f);
            float dist = (check - point).Magnitude;

            if (dist < min.dist) min = (check, dist);
        }
        return min.point;
    }

    public bool Contains(Float3 point)
    {
        float left = (point.y - a.y) / (b.y - a.y),
              right = (point.x - a.x) / (b.x - a.x);

        return left == right && point.x >= float.Min(a.x, b.x)
                             && point.x <= float.Max(a.x, b.x);
    }

    public Line[] Subdivide()
    {
        Float3 midPoint = Float3.Lerp(a, b, 0.5f);
        return new Line[] { (a, midPoint), (midPoint, b) };
    }
    public Line[] Subdivide(int iterations)
    {
        Line[] result = new Line[iterations + 4];
        float step = 1f / (iterations + 1);

        int i = 0;
        float prev = 0;
        for (float f = step; f <= 1; f += step)
        {
            result[i] = (Float3.Lerp(a, b, prev), Float3.Lerp(a, b, f));
            prev = f;
            i++;
        }

        return result;
    }

    public bool WithinRange(Float3 point, float range) =>
        WithinRange(point, range, Calculus.DefaultStep);
    public bool WithinRange(Float3 point, float range, float step)
    {
        // I could probably replace this with a more optimized solution (such as a
        // modified version of `Contains(Float3)`), but hey, this isn't the optimization
        // update, is it?

        for (float f = 0; f <= 1; f += step)
        {
            Float3 check = Float3.Lerp(a, b, f);

            // I could make a new line but that seems wasteful.
            float dist = (check - point).Magnitude;
            if (dist <= range) return true;
        }

        return false;
    }

    public static Line operator +(Line l, Float3 offset) => (l.a + offset, l.b + offset);
    public static Line operator -(Line l, Float3 offset) => (l.a + offset, l.b + offset);
    public static Line operator *(Line l, float factor) => (l.a * factor, l.b * factor);
    public static Line operator *(Line l, Float3 factor) => (l.a * factor, l.b * factor);
    public static Line operator /(Line l, float factor) => (l.a / factor, l.b / factor);
    public static Line operator /(Line l, Float3 factor) => (l.a / factor, l.b / factor);
    public static bool operator ==(Line a, Line b) => a.Equals(b);
    public static bool operator !=(Line a, Line b) => !a.Equals(b);

    public static implicit operator Line(Fill<Float3> fill) => new(fill);
    public static implicit operator Line(Fill<Int3> fill) => new(fill);
    public static implicit operator Line(Fill<float> fill) => new(fill);
    public static implicit operator Line(Fill<int> fill) => new(fill);
    public static implicit operator Line((Float3 a, Float3 b) tuple) => new(tuple.a, tuple.b);
}
