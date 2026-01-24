using Nerd_STF.Exceptions;
using Nerd_STF.Mathematics.Algebra;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;

namespace Nerd_STF.Mathematics
{
    public struct Float2 : INumberGroup<Float2, double>
#if CS11_OR_GREATER
                          ,IFromTuple<Float2, (double, double)>,
                           IPresets2d<Float2>,
                           IRoundable<Float2, Int2>,
                           IRefRoundable<Float2>,
                           ISplittable<Float2, (double[] Xs, double[] Ys)>,
                           IVector<Float2>
#endif
    {
        public static Float2 Down => new Float2(0, -1);
        public static Float2 Left => new Float2(-1, 0);
        public static Float2 Right => new Float2(1, 0);
        public static Float2 Up => new Float2(0, 1);

        public static Float2 One => new Float2(1, 1);
        public static Float2 Zero => new Float2(0, 0);

        public double InverseMagnitude => MathE.InverseSqrt(x * x + y * y);
        public double Magnitude => MathE.Sqrt(x * x + y * y);
        public Float2 Normalized => this * InverseMagnitude;

        public double x, y;

        public Float2(double x, double y)
        {
            this.x = x;
            this.y = y;
        }
        public Float2(IEnumerable<double> nums)
        {
            x = 0;
            y = 0;

            int index = 0;
            foreach (double item in nums)
            {
                this[index] = item;
                index++;
                if (index == 2) break;
            }
        }
        public Float2(Fill<double> fill)
        {
            x = fill(0);
            y = fill(1);
        }

        public double this[int index]
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
        public ListTuple<double> this[string key]
        {
            get
            {
                double[] items = new double[key.Length];
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
                return new ListTuple<double>(items);
            }
            set
            {
                IEnumerator<double> stepper = value.GetEnumerator();
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

        public static Float2 Average(IEnumerable<Float2> values)
        {
            Float2 total = Zero;
            int count = 0;
            foreach (Float2 val in values)
            {
                total += val;
                count++;
            }
            return total / count;
        }
        public static Int2 Ceiling(Float2 val) =>
            new Int2(MathE.Ceiling(val.x),
                     MathE.Ceiling(val.y));
        public static void Ceiling(ref Float2 val)
        {
            MathE.Ceiling(ref val.x);
            MathE.Ceiling(ref val.y);
        }
        public static Float2 Clamp(Float2 value, Float2 min, Float2 max) =>
            new Float2(MathE.Clamp(value.x, min.x, max.x),
                       MathE.Clamp(value.y, min.y, max.y));
        public static void Clamp(ref Float2 value, Float2 min, Float2 max)
        {
            MathE.Clamp(ref value.x, min.x, max.x);
            MathE.Clamp(ref value.y, min.y, max.y);
        }
        public static Float2 ClampMagnitude(Float2 value, double minMag, double maxMag)
        {
            Float2 copy = value;
            ClampMagnitude(ref copy, minMag, maxMag);
            return copy;
        }
        public static void ClampMagnitude(ref Float2 value, double minMag, double maxMag)
        {
            if (minMag > maxMag) throw new ClampOrderMismatchException(nameof(minMag), nameof(maxMag));
            double mag = value.Magnitude;

            if (mag < minMag)
            {
                double factor = minMag / mag;
                value.x *= factor;
                value.y *= factor;
            }
            else if (mag > maxMag)
            {
                double factor = maxMag / mag;
                value.x *= factor;
                value.y *= factor;
            }
        }
        public static Float3 Cross(Float2 a, Float2 b) => Float3.Cross(a, b);
        public static double Dot(Float2 a, Float2 b) => a.x * b.x + a.y * b.y;
        public static double Dot(IEnumerable<Float2> values)
        {
            double x = 1, y = 1;
            foreach (Float2 val in values)
            {
                x *= val.x;
                y *= val.y;
            }
            return x + y;
        }
        public static Int2 Floor(Float2 value) =>
            new Int2(MathE.Floor(value.x),
                     MathE.Floor(value.y));
        public static void Floor(ref Float2 value)
        {
            MathE.Floor(ref value.x);
            MathE.Floor(ref value.y);
        }
        public static Float2 Lerp(Float2 a, Float2 b, double t, bool clamp = true) =>
            new Float2(MathE.Lerp(a.x, b.x, t, clamp),
                       MathE.Lerp(a.y, b.y, t, clamp));
        public static Float2 Product(IEnumerable<Float2> values)
        {
            bool any = false;
            Float2 result = One;
            foreach (Float2 val in values)
            {
                any = true;
                result *= val;
            }
            return any ? result : Zero;
        }
        public static Int2 Round(Float2 value) =>
            new Int2(MathE.Round(value.x),
                     MathE.Round(value.y));
        public static void Round(ref Float2 value)
        {
            MathE.Round(ref value.x);
            MathE.Round(ref value.y);
        }
        public static Float2 Sum(IEnumerable<Float2> values)
        {
            Float2 result = Zero;
            foreach (Float2 val in values) result += val;
            return result;
        }
        
        public static (double[] Xs, double[] Ys) SplitArray(IEnumerable<Float2> values)
        {
            int count = values.Count();
            double[] Xs = new double[count], Ys = new double[count];
            int index = 0;
            foreach (Float2 val in values)
            {
                Xs[index] = val.x;
                Ys[index] = val.y;
                index++;
            }
            return (Xs, Ys);
        }

        public void Normalize()
        {
            double invMag = InverseMagnitude;
            x *= invMag;
            y *= invMag;
        }

        public IEnumerator<double> GetEnumerator()
        {
            yield return x;
            yield return y;
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public void Deconstruct(out double x, out double y)
        {
            x = this.x;
            y = this.y;
        }

        public bool Equals(Float2 other) => x == other.x && y == other.y;
#if CS8_OR_GREATER
        public override bool Equals(object? obj)
#else
        public override bool Equals(object obj)
#endif
        {
            if (obj is null) return false;
            else if (obj is Float2 objFloat2) return Equals(objFloat2);
            else if (obj is Int2 objInt2) return Equals(objInt2);
            else return false;
        }
        public override int GetHashCode() => x.GetHashCode() ^ y.GetHashCode();
        public override string ToString() => $"({x}, {y})";
        public string ToString(string format) => $"({x.ToString(format)}, {y.ToString(format)})";

        public double[] ToArray() => new double[] { x, y };
        public Fill<double> ToFill()
        {
            Float2 copy = this;
            return delegate (int i)
            {
                switch (i)
                {
                    case 0: return copy.x;
                    case 1: return copy.y;
                    default: throw new ArgumentOutOfRangeException(nameof(i));
                }
            };
        }
        public List<double> ToList() => new List<double> { x, y };

        public static Float2 operator +(Float2 a, Float2 b) => new Float2(a.x + b.x, a.y + b.y);
        public static Float2 operator -(Float2 a) => new Float2(-a.x, -a.y);
        public static Float2 operator -(Float2 a, Float2 b) => new Float2(a.x - b.x, a.y - b.y);
        public static Float2 operator *(Float2 a, double b) => new Float2(a.x * b, a.y * b);
        public static Float2 operator *(Float2 a, Float2 b) => new Float2(a.x * b.x, a.y * b.y);
        public static Float2 operator /(Float2 a, double b) => new Float2(a.x / b, a.y / b);
        public static Float2 operator /(Float2 a, Float2 b) => new Float2(a.x / b.x, a.y / b.y);
        public static bool operator ==(Float2 a, Float2 b) => a.Equals(b);
        public static bool operator !=(Float2 a, Float2 b) => !a.Equals(b);

        public static explicit operator Float2(Complex complex) => new Float2(complex.Real, complex.Imaginary);
        public static explicit operator Float2(Float3 floats) => new Float2(floats.x, floats.y);
        public static explicit operator Float2(Float4 floats) => new Float2(floats.x, floats.y);
        public static implicit operator Float2(Int2 ints) => new Float2(ints.x, ints.y);
        public static explicit operator Float2(Int3 ints) => new Float2(ints.x, ints.y);
        public static explicit operator Float2(Int4 ints) => new Float2(ints.x, ints.y);
        public static implicit operator Float2(Point point) => new Float2(point.X, point.Y);
        public static implicit operator Float2(PointF point) => new Float2(point.X, point.Y);
        public static implicit operator Float2(Size point) => new Float2(point.Width, point.Height);
        public static implicit operator Float2(SizeF size) => new Float2(size.Width, size.Height);
        public static implicit operator Float2(Vector2 vec) => new Float2(vec.X, vec.Y);
        public static explicit operator Float2(Vector3 vec) => new Float2(vec.X, vec.Y);
        public static explicit operator Float2(Vector4 vec) => new Float2(vec.X, vec.Y);
        public static implicit operator Float2(ListTuple<double> tuple) => new Float2(tuple[0], tuple[1]);
        public static implicit operator Float2(ListTuple<int> tuple) => new Float2(tuple[0], tuple[1]);
        public static implicit operator Float2((double, double) tuple) => new Float2(tuple.Item1, tuple.Item2);

        public static explicit operator Point(Float2 group) => new Point((int)group.x, (int)group.y);
        public static implicit operator PointF(Float2 group) => new PointF((float)group.x, (float)group.y);
        public static explicit operator Size(Float2 group) => new Size((int)group.x, (int)group.y);
        public static implicit operator SizeF(Float2 group) => new SizeF((float)group.x, (float)group.y);
        public static implicit operator Vector2(Float2 group) => new Vector2((float)group.x, (float)group.y);
        public static implicit operator Vector3(Float2 group) => new Vector3((float)group.x, (float)group.y, 0);
        public static implicit operator Vector4(Float2 group) => new Vector4((float)group.x, (float)group.y, 0, 0);
        public static implicit operator ListTuple<double>(Float2 group) => new ListTuple<double>(group.x, group.y);
        public static explicit operator ListTuple<int>(Float2 group) => new ListTuple<int>((int)group.x, (int)group.y);
        public static implicit operator ValueTuple<double, double>(Float2 group) => (group.x, group.y);
    }
}
