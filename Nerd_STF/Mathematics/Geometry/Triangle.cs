using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace Nerd_STF.Mathematics.Geometry
{
    public struct Triangle : ICloneable, IEquatable<Triangle>, IGroup<Vert>
    {
        public Vert A
        {
            get => p_a;
            set
            {
                p_a = value;
                p_l1 = new(value, p_b);
                p_l3 = new(p_c, value);
            }
        }
        public Vert B
        {
            get => p_b;
            set
            {
                p_b = value;
                p_l1 = new(p_a, value);
                p_l2 = new(value, p_c);
            }
        }
        public Vert C
        {
            get => p_c;
            set
            {
                p_c = value;
                p_l2 = new(p_b, value);
                p_l3 = new(value, p_a);
            }
        }
        public Line L1
        {
            get => p_l1;
            set
            {
                p_a = value.start;
                p_b = value.end;
                p_l2 = new(value.end, p_c);
                p_l3 = new(p_c, value.start);
            }
        }
        public Line L2
        {
            get => p_l2;
            set
            {
                p_b = value.start;
                p_c = value.end;
                p_l1 = new(p_a, value.start);
                p_l3 = new(value.end, p_a);
            }
        }
        public Line L3
        {
            get => p_l3;
            set
            {
                p_a = value.end;
                p_c = value.start;
                p_l1 = new(value.end, p_b);
                p_l2 = new(p_b, value.start);
            }
        }

        private Vert p_a, p_b, p_c;
        private Line p_l1, p_l2, p_l3;

        public Triangle(Vert a, Vert b, Vert c)
        {
            p_a = a;
            p_b = b;
            p_c = c;
            p_l1 = new(a, b);
            p_l2 = new(b, c);
            p_l3 = new(c, a);
        }
        public Triangle(Line l1, Line l2, Line l3)
        {
            if (l1.start != l3.end && l1.end != l2.start && l2.end != l3.start)
                throw new ArgumentException("Lines are not connected.");

            p_a = l1.start;
            p_b = l2.start;
            p_c = l3.start;
            p_l1 = l1;
            p_l2 = l2;
            p_l3 = l3;
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
            fill(7), fill(8))
        { }
        public Triangle(Fill<int> fill) : this(fill(0), fill(1), fill(2), fill(3), fill(4), fill(5), fill(6),
            fill(7), fill(8))
        { }

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
        public static Triangle Lerp(Triangle a, Triangle b, double t, bool clamp = false) =>
            new(Vert.Lerp(a.A, b.A, t, clamp), Vert.Lerp(a.B, b.B, t, clamp), Vert.Lerp(a.C, b.C, t, clamp));
        public static Triangle Median(params Triangle[] vals)
        {
            (Vert[] As, Vert[] Bs, Vert[] Cs) = SplitVertArray(vals);
            return new(Vert.Median(As), Vert.Median(Bs), Vert.Median(Cs));
        }
        public static Triangle Max(params Triangle[] vals)
        {
            (Vert[] As, Vert[] Bs, Vert[] Cs) = SplitVertArray(vals);
            return new(Vert.Max(As), Vert.Max(Bs), Vert.Max(Cs));
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
        public static (Line[] L1s, Line[] L2s, Line[] L3s) SplitLineArray(params Triangle[] tris)
        {
            Line[] l1 = new Line[tris.Length], l2 = new Line[tris.Length], l3 = new Line[tris.Length];
            for (int i = 0; i < tris.Length; i++)
            {
                l1[i] = tris[i].L1;
                l2[i] = tris[i].L2;
                l3[i] = tris[i].L3;
            }

            return (l1, l2, l3);
        }

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

        public static Triangle operator +(Triangle a, Triangle b) => new(a.A + b.A, a.B + b.B, a.C + b.C);
        public static Triangle operator +(Triangle a, Vert b) => new(a.A + b, a.B + b, a.C + b);
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
    }
}
