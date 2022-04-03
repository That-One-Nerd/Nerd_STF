using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nerd_STF.Mathematics.Geometry
{
    public struct Line : ICloneable, IEquatable<Line>, IGroup<Vert>
    {
        public static Line Back => new(Vert.Zero, Vert.Back);
        public static Line Down => new(Vert.Zero, Vert.Down);
        public static Line Forward => new(Vert.Zero, Vert.Forward);
        public static Line Left => new(Vert.Zero, Vert.Left);
        public static Line Right => new(Vert.Zero, Vert.Right);
        public static Line Up => new(Vert.Zero, Vert.Up);

        public static Line One => new(Vert.Zero, Vert.One);
        public static Line Zero => new(Vert.Zero, Vert.Zero);

        public double Length => (end - start).Magnitude;

        public Vert start, end;

        public Line(Vert start, Vert end)
        {
            this.start = start;
            this.end = end;
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
                0 => start,
                1 => end,
                _ => throw new IndexOutOfRangeException(nameof(index)),
            };
            set
            {
                switch (index)
                {
                    case 0:
                        start = value;
                        break;

                    case 1:
                        end = value;
                        break;

                    default: throw new IndexOutOfRangeException(nameof(index));
                }
            }
        }

        public static Line Absolute(Line val) => new(Vert.Absolute(val.start), Vert.Absolute(val.end));
        public static Line Average(params Line[] vals)
        {
            (Vert[] starts, Vert[] ends) = SplitArray(vals);
            return new(Vert.Average(starts), Vert.Average(ends));
        }
        public static Line Ceiling(Line val) => new(Vert.Ceiling(val.start), Vert.Ceiling(val.end));
        public static Line Clamp(Line val, Line min, Line max) =>
            new(Vert.Clamp(val.start, min.start, max.start), Vert.Clamp(val.end, min.end, max.end));
        public static Line Floor(Line val) => new(Vert.Floor(val.start), Vert.Floor(val.end));
        public static Line Lerp(Line a, Line b, double t, bool clamp = true) =>
            new(Vert.Lerp(a.start, b.start, t, clamp), Vert.Lerp(a.end, b.end, t, clamp));
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
                starts[i] = lines[i].start;
                ends[i] = lines[i].end;
            }
            return (starts, ends);
        }

        public override bool Equals([NotNullWhen(true)] object? obj)
        {
            if (obj == null || obj.GetType() != typeof(Line)) return false;
            return Equals((Line)obj);
        }
        public bool Equals(Line other) => start == other.start && end == other.end;
        public override int GetHashCode() => start.GetHashCode() ^ end.GetHashCode();
        public override string ToString() => ToString((string?)null);
        public string ToString(string? provider) =>
            "A: " + start.ToString(provider) + " B: " + end.ToString(provider);
        public string ToString(IFormatProvider provider) =>
            "A: " + start.ToString(provider) + " B: " + end.ToString(provider);

        public object Clone() => new Line(start, end);

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<Vert> GetEnumerator()
        {
            yield return start;
            yield return end;
        }

        public Vert[] ToArray() => new Vert[] { start, end };
        public List<Vert> ToList() => new() { start, end };

        public double[] ToDoubleArray() => new double[] { start.position.x, start.position.y, start.position.z,
                                                          end.position.x, end.position.y, end.position.z };
        public List<double> ToDoubleList() => new() { start.position.x, start.position.y, start.position.z,
                                                      end.position.x, end.position.y, end.position.z };

        public static Line operator +(Line a, Line b) => new(a.start + b.start, a.end + b.end);
        public static Line operator +(Line a, Vert b) => new(a.start + b, a.end + b);
        public static Line operator -(Line a, Line b) => new(a.start - b.start, a.end - b.end);
        public static Line operator -(Line a, Vert b) => new(a.start - b, a.end - b);
        public static Line operator *(Line a, Line b) => new(a.start * b.start, a.end * b.end);
        public static Line operator *(Line a, Vert b) => new(a.start * b, a.end * b);
        public static Line operator *(Line a, double b) => new(a.start * b, a.end * b);
        public static Line operator /(Line a, Line b) => new(a.start / b.start, a.end / b.end);
        public static Line operator /(Line a, Vert b) => new(a.start / b, a.end / b);
        public static Line operator /(Line a, double b) => new(a.start / b, a.end / b);
        public static bool operator ==(Line a, Line b) => a.Equals(b);
        public static bool operator !=(Line a, Line b) => !a.Equals(b);

        public static implicit operator Line(Fill<Vert> fill) => new(fill);
        public static implicit operator Line(Fill<Double3> fill) => new(fill);
        public static implicit operator Line(Fill<Int3> fill) => new(fill);
        public static implicit operator Line(Fill<double> fill) => new(fill);
        public static implicit operator Line(Fill<int> fill) => new(fill);
    }
}
