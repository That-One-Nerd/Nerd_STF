using Nerd_STF.Exceptions;
using Nerd_STF.Mathematics.Algebra;
using Nerd_STF.Mathematics.Numbers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Nerd_STF.Mathematics
{
    public struct Int2 : INumberGroup<Int2, int>
#if CS11_OR_GREATER
                        ,IFromTuple<Int2, (int, int)>,
                         IPresets2d<Int2>,
                         ISplittable<Int2, (int[] Xs, int[] Ys)>
#endif
    {
        public static Int2 Down => new Int2(0, -1);
        public static Int2 Left => new Int2(-1, 0);
        public static Int2 Right => new Int2(1, 0);
        public static Int2 Up => new Int2(0, 1);

        public static Int2 One => new Int2(1, 1);
        public static Int2 Zero => new Int2(0, 0);

        public double InverseMagnitude => MathE.InverseSqrt(x * x + y * y);
        public double Magnitude => MathE.Sqrt(x * x + y * y);
        public Float2 Normalized => (Float2)this * InverseMagnitude;

        public int x, y;

        public Int2(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public Int2(IEnumerable<int> nums)
        {
            x = 0;
            y = 0;

            int index = 0;
            foreach (int item in nums)
            {
                this[index] = item;
                index++;
                if (index == 2) break;
            }
        }
        public Int2(Fill<int> fill)
        {
            x = fill(0);
            y = fill(1);
        }

        public int this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0: return x;
                    case 1: return y;
                    default: throw new ArgumentOutOfRangeException(nameof(index));
                }
            }
            set
            {
                switch (index)
                {
                    case 0: x = value; break;
                    case 1: y = value; break;
                    default: throw new ArgumentOutOfRangeException(nameof(index));
                }
            }
        }
        public ListTuple<int> this[string key]
        {
            get
            {
                int[] items = new int[key.Length];
                for (int i = 0; i < key.Length; i++)
                {
                    char c = char.ToLower(key[i]);
                    switch (c)
                    {
                        case 'x': items[i] = x; break;
                        case 'y': items[i] = y; break;
                        default: throw new ArgumentException("Invalid key.", nameof(key));
                    }
                }
                return new ListTuple<int>(items);
            }
            set
            {
                IEnumerator<int> stepper = value.GetEnumerator();
                for (int i = 0; i < key.Length; i++)
                {
                    char c = char.ToLower(key[i]);
                    stepper.MoveNext();
                    switch (c)
                    {
                        case 'x': x = stepper.Current; break;
                        case 'y': y = stepper.Current; break;
                        default: throw new ArgumentException("Invalid key.", nameof(key));
                    }
                }
            }
        }

        public static Int2 Average(IEnumerable<Int2> values)
        {
            Int2 total = Zero;
            int count = 0;
            foreach (Int2 val in values)
            {
                total += val;
                count++;
            }
            return total / count;
        }
        public static Int2 Clamp(Int2 value, Int2 min, Int2 max) =>
            new Int2(MathE.Clamp(value.x, min.x, max.x),
                     MathE.Clamp(value.y, min.y, max.y));
        public static void Clamp(ref Int2 value, Int2 min, Int2 max)
        {
            MathE.Clamp(ref value.x, min.x, max.x);
            MathE.Clamp(ref value.y, min.y, max.y);
        }
        public static Int2 ClampMagnitude(Int2 value, double minMag, double maxMag)
        {
            Int2 copy = value;
            ClampMagnitude(ref copy, minMag, maxMag);
            return copy;
        }
        public static void ClampMagnitude(ref Int2 value, double minMag, double maxMag)
        {
            if (minMag > maxMag) throw new ClampOrderMismatchException(nameof(minMag), nameof(maxMag));
            double mag = value.Magnitude;
            if (mag < minMag)
            {
                double factor = minMag / mag;
                value.x = MathE.Ceiling(value.x * factor);
                value.y = MathE.Ceiling(value.y * factor);
            }
            else if (mag > maxMag)
            {
                double factor = maxMag / mag;
                value.x = MathE.Floor(value.x * factor);
                value.y = MathE.Floor(value.y * factor);
            }
        }
        public static Int3 Cross(Int2 a, Int2 b) => Int3.Cross(a, b);
        public static int Dot(Int2 a, Int2 b) => a.x * b.x + a.y * b.y;
        public static int Dot(IEnumerable<Int2> values)
        {
            int x = 1, y = 1;
            foreach (Int2 val in values)
            {
                x *= val.x;
                y *= val.y;
            }
            return x + y;
        }
#if CS11_OR_GREATER
        static double IVectorOperations<Int2>.Dot(Int2 a, Int2 b) => Dot(a, b);
        static double IVectorOperations<Int2>.Dot(IEnumerable<Int2> vals) => Dot(vals);
#endif
        public static Int2 Lerp(Int2 a, Int2 b, double t, bool clamp = true) =>
            new Int2(MathE.Lerp(a.x, b.x, t, clamp),
                     MathE.Lerp(a.y, b.y, t, clamp));
        public static Int2 Product(IEnumerable<Int2> values)
        {
            bool any = false;
            Int2 total = One;
            foreach (Int2 val in values)
            {
                any = true;
                total *= val;
            }
            return any ? total : Zero;
        }
        public static Int2 Sum(IEnumerable<Int2> values)
        {
            Int2 total = Zero;
            foreach (Int2 val in values) total += val;
            return total;
        }

        public static (int[] Xs, int[] Ys) SplitArray(IEnumerable<Int2> values)
        {
            int count = values.Count();
            int[] Xs = new int[count], Ys = new int[count];
            int index = 0;
            foreach (Int2 val in values)
            {
                Xs[index] = val.x;
                Ys[index] = val.y;
                index++;
            }
            return (Xs, Ys);
        }

        public IEnumerator<int> GetEnumerator()
        {
            yield return x;
            yield return y;
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public void Deconstruct(out int x, out int y)
        {
            x = this.x;
            y = this.y;
        }

        public bool Equals(Int2 other) => x == other.x && y == other.y;
#if CS8_OR_GREATER
        public override bool Equals(object? obj)
#else
        public override bool Equals(object obj)
#endif
        {
            if (obj is null) return false;
            else if (obj is Int2 objInt2) return Equals(objInt2);
            else if (obj is Float2 objFloat2) return objFloat2.Equals(this);
            else return false;
        }
        public override int GetHashCode() => x.GetHashCode() ^ y.GetHashCode();
        public override string ToString() => $"({x}, {y})";
        public string ToString(string format) => $"({x.ToString(format)}, {y.ToString(format)})";

        public int[] ToArray() => new int[] { x, y };
        public Fill<int> ToFill()
        {
            Int2 copy = this;
            return delegate (int i)
            {
                switch (i)
                {
                    case 0: return copy.x;
                    case 1: return copy.y;
                    default: throw new ArgumentOutOfRangeException();
                }
            };
        }
        public List<int> ToList() => new List<int> { x, y };

        public static Int2 operator +(Int2 a, Int2 b) => new Int2(a.x + b.x, a.y + b.y);
        public static Int2 operator -(Int2 a) => new Int2(-a.x, -a.y);
        public static Int2 operator -(Int2 a, Int2 b) => new Int2(a.x - b.x, a.y - b.y);
        public static Int2 operator *(Int2 a, int b) => new Int2(a.x * b, a.y * b);
        public static Int2 operator *(Int2 a, Int2 b) => new Int2(a.x * b.x, a.y * b.y);
        public static Int2 operator /(Int2 a, int b) => new Int2(a.x / b, a.y / b);
        public static Int2 operator /(Int2 a, Int2 b) => new Int2(a.x / b.x, a.y / b.y);
        public static Int2 operator &(Int2 a, Int2 b) => new Int2(a.x & b.x, a.y & b.y);
        public static Int2 operator |(Int2 a, Int2 b) => new Int2(a.x | b.x, a.y | b.y);
        public static Int2 operator ^(Int2 a, Int2 b) => new Int2(a.x ^ b.x, a.y ^ b.y);
        public static bool operator ==(Int2 a, Int2 b) => a.Equals(b);
        public static bool operator !=(Int2 a, Int2 b) => !a.Equals(b);

        public static explicit operator Int2(Complex complex) => new Int2((int)complex.r, (int)complex.i);
        public static explicit operator Int2(Float2 floats) => new Int2((int)floats.x, (int)floats.y);
        public static explicit operator Int2(Float3 floats) => new Int2((int)floats.x, (int)floats.y);
        public static explicit operator Int2(Float4 floats) => new Int2((int)floats.x, (int)floats.y);
        public static explicit operator Int2(Int3 ints) => new Int2(ints.x, ints.y);
        public static explicit operator Int2(Int4 ints) => new Int2(ints.x, ints.y);
        public static implicit operator Int2(Point point) => new Int2(point.X, point.Y);
        public static explicit operator Int2(PointF point) => new Int2((int)point.X, (int)point.Y);
        public static implicit operator Int2(Size size) => new Int2(size.Width, size.Height);
        public static explicit operator Int2(SizeF size) => new Int2((int)size.Width, (int)size.Height);
        public static explicit operator Int2(ListTuple<double> tuple) => new Int2((int)tuple[0], (int)tuple[1]);
        public static implicit operator Int2(ListTuple<int> tuple) => new Int2(tuple[0], tuple[1]);
        public static implicit operator Int2((int, int) tuple) => new Int2(tuple.Item1, tuple.Item2);

        public static implicit operator Point(Int2 group) => new Point(group.x, group.y);
        public static explicit operator PointF(Int2 group) => new PointF(group.x, group.y);
        public static implicit operator Size(Int2 group) => new Size(group.x, group.y);
        public static explicit operator SizeF(Int2 group) => new SizeF(group.x, group.y);
        public static implicit operator ListTuple<double>(Int2 group) => new ListTuple<double>(group.x, group.y);
        public static implicit operator ListTuple<int>(Int2 group) => new ListTuple<int>(group.x, group.y);
        public static implicit operator ValueTuple<int, int>(Int2 group) => (group.x, group.y);
    }
}
