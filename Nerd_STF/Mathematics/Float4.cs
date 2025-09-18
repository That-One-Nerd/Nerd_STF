using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Nerd_STF.Exceptions;
using Nerd_STF.Graphics;

namespace Nerd_STF.Mathematics
{
    public struct Float4 : INumberGroup<Float4, double>
#if CS11_OR_GREATER
                          ,IFromTuple<Float4, (double, double, double, double)>,
                           IPresets4d<Float4>,
                           IRoundable<Float4, Int4>,
                           IRefRoundable<Float4>,
                           ISplittable<Float4, (double[] Ws, double[] Xs, double[] Ys, double[] Zs)>
#endif
    {
        public static Float4 Backward => new Float4(0, 0, 0, -1);
        public static Float4 Down => new Float4(0, 0, -1, 0);
        public static Float4 Forward => new Float4(0, 0, 0, 1);
        public static Float4 HighW => new Float4(1, 0, 0, 0);
        public static Float4 Left => new Float4(0, -1, 0, 0);
        public static Float4 LowW => new Float4(-1, 0, 0, 0);
        public static Float4 Right => new Float4(0, 1, 0, 0);
        public static Float4 Up => new Float4(0, 0, 1, 0);

        public static Float4 One => new Float4(1, 1, 1, 1);
        public static Float4 Zero => new Float4(0, 0, 0, 0);

        public double InverseMagnitude => MathE.InverseSqrt(w * w + x * x + y * y + z * z);
        public double Magnitude => MathE.Sqrt(w * w + x * x + y * y + z * z);
        public Float4 Normalized => this * InverseMagnitude;

        public double w, x, y, z;

        public Float4(double w, double x, double y, double z)
        {
            this.w = w;
            this.x = x;
            this.y = y;
            this.z = z;
        }
        public Float4(IEnumerable<double> nums)
        {
            w = 0;
            x = 0;
            y = 0;
            z = 0;

            int index = 0;
            foreach (double item in nums)
            {
                this[index] = item;
                index++;
                if (index == 4) break;
            }
        }
        public Float4(Fill<double> fill)
        {
            w = fill(0);
            x = fill(1);
            y = fill(2);
            z = fill(3);
        }

        public double this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0: return w;
                    case 1: return x;
                    case 2: return y;
                    case 3: return z;
                    default: throw new ArgumentOutOfRangeException(nameof(index));
                }
            }
            set
            {
                switch (index)
                {
                    case 0: w = value; break;
                    case 1: x = value; break;
                    case 2: y = value; break;
                    case 3: z = value; break;
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
                    char c = key[i];
                    switch (c)
                    {
                        case 'w': items[i] = w; break;
                        case 'x': items[i] = x; break;
                        case 'y': items[i] = y; break;
                        case 'z': items[i] = z; break;
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
                    char c = key[i];
                    stepper.MoveNext();
                    switch (c)
                    {
                        case 'w': w = stepper.Current; break;
                        case 'x': x = stepper.Current; break;
                        case 'y': y = stepper.Current; break;
                        case 'z': z = stepper.Current; break;
                        default: throw new ArgumentException("Invalid key.", nameof(key));
                    }
                }
            }
        }

        public static Float4 Average(IEnumerable<Float4> values)
        {
            Float4 total = Zero;
            int count = 0;
            foreach (Float4 val in values)
            {
                total += val;
                count++;
            }
            return total / count;
        }
        public static Int4 Ceiling(Float4 val) =>
            new Int4(MathE.Ceiling(val.w),
                     MathE.Ceiling(val.x),
                     MathE.Ceiling(val.y),
                     MathE.Ceiling(val.z));
        public static void Ceiling(ref Float4 val)
        {
            MathE.Ceiling(ref val.w);
            MathE.Ceiling(ref val.x);
            MathE.Ceiling(ref val.y);
            MathE.Ceiling(ref val.z);
        }
        public static Float4 Clamp(Float4 value, Float4 min, Float4 max) =>
            new Float4(MathE.Clamp(value.w, min.w, max.w),
                       MathE.Clamp(value.x, min.x, max.x),
                       MathE.Clamp(value.y, min.y, max.y),
                       MathE.Clamp(value.z, min.z, max.z));
        public static void Clamp(ref Float4 value, Float4 min, Float4 max)
        {
            MathE.Clamp(ref value.w, min.w, max.w);
            MathE.Clamp(ref value.x, min.x, max.x);
            MathE.Clamp(ref value.y, min.y, max.y);
            MathE.Clamp(ref value.z, min.z, max.z);
        }
        public static Float4 ClampMagnitude(Float4 value, double minMag, double maxMag)
        {
            Float4 copy = value;
            ClampMagnitude(ref copy, minMag, maxMag);
            return copy;
        }
        public static void ClampMagnitude(ref Float4 value, double minMag, double maxMag)
        {
            if (minMag > maxMag) throw new ClampOrderMismatchException(nameof(minMag), nameof(maxMag));
            double mag = value.Magnitude;

            if (mag < minMag)
            {
                double factor = minMag / mag;
                value.w *= factor;
                value.x *= factor;
                value.y *= factor;
                value.z *= factor;
            }
            else if (mag > maxMag)
            {
                double factor = maxMag / mag;
                value.w *= factor;
                value.x *= factor;
                value.y *= factor;
                value.z *= factor;
            }
        }
        public static double Dot(Float4 a, Float4 b) => a.w * b.w + a.x * b.x + a.y * b.y + a.z * b.z;
        public static double Dot(IEnumerable<Float4> values)
        {
            double w = 1, x = 1, y = 1, z = 1;
            foreach (Float4 val in values)
            {
                w *= val.w;
                x *= val.x;
                y *= val.y;
                z *= val.z;
            }
            return w + x + y + z;
        }
        public static Int4 Floor(Float4 value) =>
            new Int4(MathE.Floor(value.w),
                     MathE.Floor(value.x),
                     MathE.Floor(value.y),
                     MathE.Floor(value.z));
        public static void Floor(ref Float4 value)
        {
            MathE.Floor(ref value.w);
            MathE.Floor(ref value.x);
            MathE.Floor(ref value.y);
            MathE.Floor(ref value.z);
        }
        public static Float4 Lerp(Float4 a, Float4 b, double t, bool clamp = true) =>
            new Float4(MathE.Lerp(a.w, b.w, t, clamp),
                       MathE.Lerp(a.x, b.x, t, clamp),
                       MathE.Lerp(a.y, b.y, t, clamp),
                       MathE.Lerp(a.z, b.z, t, clamp));
        public static Float4 Product(IEnumerable<Float4> values)
        {
            bool any = false;
            Float4 result = One;
            foreach (Float4 val in values)
            {
                any = true;
                result *= val;
            }
            return any ? result : Zero;
        }
        public static Int4 Round(Float4 value) =>
            new Int4(MathE.Round(value.w),
                     MathE.Round(value.x),
                     MathE.Round(value.y),
                     MathE.Round(value.z));
        public static void Round(ref Float4 value)
        {
            MathE.Round(ref value.w);
            MathE.Round(ref value.x);
            MathE.Round(ref value.y);
            MathE.Round(ref value.z);
        }
        public static Float4 Sum(IEnumerable<Float4> values)
        {
            Float4 result = Zero;
            foreach (Float4 val in values) result += val;
            return result;
        }
        
        public static (double[] Ws, double[] Xs, double[] Ys, double[] Zs) SplitArray(IEnumerable<Float4> values)
        {
            int count = values.Count();
            double[] Ws = new double[count], Xs = new double[count], Ys = new double[count], Zs = new double[count];
            int index = 0;
            foreach (Float4 val in values)
            {
                Ws[index] = val.w;
                Xs[index] = val.x;
                Ys[index] = val.y;
                Zs[index] = val.z;
                index++;
            }
            return (Ws, Xs, Ys, Zs);
        }

        public void Normalize()
        {
            double invMag = InverseMagnitude;
            w *= invMag;
            x *= invMag;
            y *= invMag;
            z *= invMag;
        }
        
        public IEnumerator<double> GetEnumerator()
        {
            yield return w;
            yield return x;
            yield return y;
            yield return z;
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public void Deconstruct(out double w, out double x, out double y, out double z)
        {
            w = this.w;
            x = this.x;
            y = this.y;
            z = this.z;
        }

        public bool Equals(Float4 other) => w == other.w && x == other.x && y == other.y && z == other.z;
#if CS8_OR_GREATER
        public override bool Equals(object? obj)
#else
        public override bool Equals(object obj)
#endif
        {
            if (obj is null) return false;
            else if (obj is Float4 objFloat4) return Equals(objFloat4);
            else if (obj is Int4 objInt4) return Equals(objInt4);
            else return false;
        }
        public override int GetHashCode() => w.GetHashCode() ^ x.GetHashCode() ^ y.GetHashCode() ^ z.GetHashCode();
        public override string ToString() => $"({w}, {x}, {y}, {z})";
        public string ToString(string format) => $"({w.ToString(format)}, {x.ToString(format)}, {y.ToString(format)}, {z.ToString(format)})";

        public double[] ToArray() => new double[] { w, x, y, z };
        public Fill<double> ToFill()
        {
            Float4 copy = this;
            return delegate (int i)
            {
                switch (i)
                {
                    case 0: return copy.w;
                    case 1: return copy.x;
                    case 2: return copy.y;
                    case 3: return copy.z;
                    default: throw new ArgumentOutOfRangeException(nameof(i));
                }
            };
        }
        public List<double> ToList() => new List<double> { w, x, y, z };

        public static Float4 operator +(Float4 a, Float4 b) => new Float4(a.w + b.w, a.x + b.x, a.y + b.y, a.z + b.z);
        public static Float4 operator -(Float4 a) => new Float4(-a.w, -a.x, -a.y, -a.z);
        public static Float4 operator -(Float4 a, Float4 b) => new Float4(a.w - b.w, a.x - b.x, a.y - b.y, a.z - b.z);
        public static Float4 operator *(Float4 a, double b) => new Float4(a.w * b, a.x * b, a.y * b, a.z * b);
        public static Float4 operator *(Float4 a, Float4 b) => new Float4(a.w * b.w, a.x * b.x, a.y * b.y, a.z * b.z);
        public static Float4 operator /(Float4 a, double b) => new Float4(a.w / b, a.x / b, a.y / b, a.z / b);
        public static Float4 operator /(Float4 a, Float4 b) => new Float4(a.w / b.w, a.x / b.x, a.y / b.y, a.z / b.z);
        public static bool operator ==(Float4 a, Float4 b) => a.Equals(b);
        public static bool operator !=(Float4 a, Float4 b) => !a.Equals(b);

        public static explicit operator Float4(ColorRGB color) => new Float4(color.a, color.r, color.g, color.b);
        public static implicit operator Float4(Int2 ints) => new Float4(0, ints.x, ints.y, 0);
        public static implicit operator Float4(Int3 ints) => new Float4(0, ints.x, ints.y, ints.z);
        public static implicit operator Float4(Int4 ints) => new Float4(ints.w, ints.x, ints.y, ints.z);
        public static implicit operator Float4(Float2 floats) => new Float4(0, floats.x, floats.y, 0);
        public static implicit operator Float4(Float3 floats) => new Float4(0, floats.x, floats.y, floats.z);
        public static implicit operator Float4(Numbers.Quaternion quat) => new Float4(quat.w, quat.x, quat.y, quat.z);
        public static implicit operator Float4(Vector2 vec) => new Float4(0, vec.X, vec.Y, 0);
        public static implicit operator Float4(Vector3 vec) => new Float4(0, vec.X, vec.Y, vec.Z);
        public static implicit operator Float4(Vector4 vec) => new Float4(vec.W, vec.X, vec.Y, vec.Z);
        public static implicit operator Float4(ListTuple<double> tuple) => new Float4(tuple[0], tuple[1], tuple[2], tuple[3]);
        public static implicit operator Float4(ListTuple<int> tuple) => new Float4(tuple[0], tuple[1], tuple[2], tuple[3]);
        public static implicit operator Float4((double, double, double, double) tuple) => new Float4(tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4);

        public static explicit operator Vector2(Float4 group) => new Vector2((float)group.x, (float)group.y);
        public static explicit operator Vector3(Float4 group) => new Vector3((float)group.x, (float)group.y, (float)group.z);
        public static implicit operator Vector4(Float4 group) => new Vector4((float)group.x, (float)group.y, (float)group.z, (float)group.w);
        public static implicit operator ListTuple<double>(Float4 group) => new ListTuple<double>(group.w, group.x, group.y, group.z);
        public static explicit operator ListTuple<int>(Float4 group) => new ListTuple<int>((int)group.w, (int)group.x, (int)group.y, (int)group.z);
        public static implicit operator ValueTuple<double, double, double, double>(Float4 group) => (group.w, group.x, group.y, group.z);
    }
}
