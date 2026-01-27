using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Nerd_STF.Exceptions;
using Nerd_STF.Graphics;
using Nerd_STF.Mathematics.Algebra;

namespace Nerd_STF.Mathematics
{
    public struct Float4 : INumberGroup<Float4, double>
#if CS11_OR_GREATER
                          ,IFromTuple<Float4, (double, double, double, double)>,
                           IPresets4d<Float4>,
                           IRoundable<Float4, Int4>,
                           IRefRoundable<Float4>,
                           IVector<Float4>
#endif
    {
        public static Float4 Backward => new Float4( 0,  0, -1,  0);
        public static Float4 Down =>     new Float4( 0, -1,  0,  0);
        public static Float4 Forward =>  new Float4( 0,  0,  1,  0);
        public static Float4 HighW =>    new Float4( 0,  0,  0,  1);
        public static Float4 Left =>     new Float4(-1,  0,  0,  0);
        public static Float4 LowW =>     new Float4( 0,  0,  0, -1);
        public static Float4 Right =>    new Float4( 1,  0,  0,  0);
        public static Float4 Up =>       new Float4( 0,  1,  0,  0);

        public static Float4 One => new Float4(1, 1, 1, 1);
        public static Float4 Zero => new Float4(0, 0, 0, 0);

        public double InverseMagnitude => MathE.InverseSqrt(x * x + y * y + z * z + w * w);
        public double Magnitude => MathE.Sqrt(x * x + y * y + z * z + w * w);
        public double MagnitudeSqr => x * x + y * y + z * z + w * w;
        public Float4 Normalized => this * InverseMagnitude;

        public double x, y, z, w;

        public Float4(double x, double y, double z, double w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }
        public Float4(IEnumerable<double> nums)
        {
            x = 0;
            y = 0;
            z = 0;
            w = 0;

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
            x = fill(0);
            y = fill(1);
            z = fill(2);
            w = fill(3);
        }

        public double this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0: return x;
                    case 1: return y;
                    case 2: return z;
                    case 3: return w;
                    default: throw new ArgumentOutOfRangeException(nameof(index));
                }
            }
            set
            {
                switch (index)
                {
                    case 0: x = value; break;
                    case 1: y = value; break;
                    case 2: z = value; break;
                    case 3: w = value; break;
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
                        case 'z': items[i] = z; break;
                        case 'w': items[i] = w; break;
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
                        case 'z': z = stepper.Current; break;
                        case 'w': w = stepper.Current; break;
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
            new Int4(MathE.Ceiling(val.x),
                     MathE.Ceiling(val.y),
                     MathE.Ceiling(val.z),
                     MathE.Ceiling(val.w));
        public static void Ceiling(ref Float4 val)
        {
            MathE.Ceiling(ref val.x);
            MathE.Ceiling(ref val.y);
            MathE.Ceiling(ref val.z);
            MathE.Ceiling(ref val.w);
        }
        public static Float4 Clamp(Float4 value, Float4 min, Float4 max) =>
            new Float4(MathE.Clamp(value.x, min.x, max.x),
                       MathE.Clamp(value.y, min.y, max.y),
                       MathE.Clamp(value.z, min.z, max.z),
                       MathE.Clamp(value.w, min.w, max.w));
        public static void Clamp(ref Float4 value, Float4 min, Float4 max)
        {
            MathE.Clamp(ref value.x, min.x, max.x);
            MathE.Clamp(ref value.y, min.y, max.y);
            MathE.Clamp(ref value.z, min.z, max.z);
            MathE.Clamp(ref value.w, min.w, max.w);
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
                value.x *= factor;
                value.y *= factor;
                value.z *= factor;
                value.w *= factor;
            }
            else if (mag > maxMag)
            {
                double factor = maxMag / mag;
                value.x *= factor;
                value.y *= factor;
                value.z *= factor;
                value.w *= factor;
            }
        }
        public static double Dot(Float4 a, Float4 b) => a.x * b.x + a.y * b.y + a.z * b.z + a.w * b.w;
        public static double Dot(IEnumerable<Float4> values)
        {
            double x = 1, y = 1, z = 1, w = 1;
            foreach (Float4 val in values)
            {
                x *= val.x;
                y *= val.y;
                z *= val.z;
                w *= val.w;
            }
            return x + y + z + w;
        }
        public static Int4 Floor(Float4 value) =>
            new Int4(MathE.Floor(value.x),
                     MathE.Floor(value.y),
                     MathE.Floor(value.z),
                     MathE.Floor(value.w));
        public static void Floor(ref Float4 value)
        {
            MathE.Floor(ref value.x);
            MathE.Floor(ref value.y);
            MathE.Floor(ref value.z);
            MathE.Floor(ref value.w);
        }
        public static Float4 Lerp(Float4 a, Float4 b, double t, bool clamp = true) =>
            new Float4(MathE.Lerp(a.x, b.x, t, clamp),
                       MathE.Lerp(a.y, b.y, t, clamp),
                       MathE.Lerp(a.z, b.z, t, clamp),
                       MathE.Lerp(a.w, b.w, t, clamp));
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
            new Int4(MathE.Round(value.x),
                     MathE.Round(value.y),
                     MathE.Round(value.z),
                     MathE.Round(value.w));
        public static void Round(ref Float4 value)
        {
            MathE.Round(ref value.x);
            MathE.Round(ref value.y);
            MathE.Round(ref value.z);
            MathE.Round(ref value.w);
        }
        public static Float4 Sum(IEnumerable<Float4> values)
        {
            Float4 result = Zero;
            foreach (Float4 val in values) result += val;
            return result;
        }

        public void Normalize()
        {
            double invMag = InverseMagnitude;
            x *= invMag;
            y *= invMag;
            z *= invMag;
            w *= invMag;
        }
        
        public IEnumerator<double> GetEnumerator()
        {
            yield return x;
            yield return y;
            yield return z;
            yield return w;
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public void Deconstruct(out double x, out double y, out double z, out double w)
        {
            x = this.x;
            y = this.y;
            z = this.z;
            w = this.w;
        }

        public bool Equals(Float4 other) => x == other.x && y == other.y && z == other.z && w == other.w;
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
        public override int GetHashCode() => x.GetHashCode() ^ y.GetHashCode() ^ z.GetHashCode() ^ w.GetHashCode();
        public override string ToString() => $"({x}, {y}, {z}, {w})";
        public string ToString(string format) => $"({x.ToString(format)}, {y.ToString(format)}, {z.ToString(format)}, {w.ToString(format)})";

        public double[] ToArray() => new double[] { x, y, z, w };
        public Fill<double> ToFill()
        {
            Float4 copy = this;
            return delegate (int i)
            {
                switch (i)
                {
                    case 0: return copy.x;
                    case 1: return copy.y;
                    case 2: return copy.z;
                    case 3: return copy.w;
                    default: throw new ArgumentOutOfRangeException(nameof(i));
                }
            };
        }
        public List<double> ToList() => new List<double> { x, y, z, w };

        public static Float4 operator +(Float4 a, Float4 b) => new Float4(a.x + b.x, a.y + b.y, a.z + b.z, a.w + b.w);
        public static Float4 operator -(Float4 a) => new Float4(-a.x, -a.y, -a.z, -a.w);
        public static Float4 operator -(Float4 a, Float4 b) => new Float4(a.x - b.x, a.y - b.y, a.z - b.z, a.w - b.w);
        public static Float4 operator *(Float4 a, double b) => new Float4(a.x * b, a.y * b, a.z * b, a.w * b);
        public static Float4 operator *(Float4 a, Float4 b) => new Float4(a.x * b.x, a.y * b.y, a.z * b.z, a.w * b.w);
        public static Float4 operator /(Float4 a, double b) => new Float4(a.x / b, a.y / b, a.z / b, a.w / b);
        public static Float4 operator /(Float4 a, Float4 b) => new Float4(a.x / b.x, a.y / b.y, a.z / b.z, a.w / b.w);
        public static bool operator ==(Float4 a, Float4 b) => a.Equals(b);
        public static bool operator !=(Float4 a, Float4 b) => !a.Equals(b);

        public static explicit operator Float4(ColorRGB color) => new Float4(color.r, color.g, color.b, color.a);
        public static implicit operator Float4(Int2 ints) => new Float4(ints.x, ints.y, 0, 0);
        public static implicit operator Float4(Int3 ints) => new Float4(ints.x, ints.y, ints.z, 0);
        public static implicit operator Float4(Int4 ints) => new Float4(ints.x, ints.y, ints.z, ints.w);
        public static implicit operator Float4(Float2 floats) => new Float4(floats.x, floats.y, 0, 0);
        public static implicit operator Float4(Float3 floats) => new Float4(floats.x, floats.y, floats.z, 0);
        public static explicit operator Float4(Matrix mat) => new Float4(mat.TryGet(0, 0), mat.TryGet(1, 0), mat.TryGet(2, 0), mat.TryGet(3, 0));
        public static implicit operator Float4(Numbers.Quaternion quat) => new Float4(quat.x, quat.y, quat.z, quat.w);
        public static implicit operator Float4(Vector2 vec) => new Float4(vec.X, vec.Y, 0, 0);
        public static implicit operator Float4(Vector3 vec) => new Float4(vec.X, vec.Y, vec.Z, 0);
        public static implicit operator Float4(Vector4 vec) => new Float4(vec.X, vec.Y, vec.Z, vec.W);
        public static implicit operator Float4(ListTuple<double> tuple) => new Float4(tuple[0], tuple[1], tuple[2], tuple[3]);
        public static implicit operator Float4(ListTuple<int> tuple) => new Float4(tuple[0], tuple[1], tuple[2], tuple[3]);
        public static implicit operator Float4((double, double, double, double) tuple) => new Float4(tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4);

        public static explicit operator Vector2(Float4 group) => new Vector2((float)group.x, (float)group.y);
        public static explicit operator Vector3(Float4 group) => new Vector3((float)group.x, (float)group.y, (float)group.z);
        public static implicit operator Vector4(Float4 group) => new Vector4((float)group.x, (float)group.y, (float)group.z, (float)group.w);
        public static implicit operator ListTuple<double>(Float4 group) => new ListTuple<double>(group.x, group.y, group.z, group.w);
        public static explicit operator ListTuple<int>(Float4 group) => new ListTuple<int>((int)group.x, (int)group.y, (int)group.z, (int)group.w);
        public static implicit operator ValueTuple<double, double, double, double>(Float4 group) => (group.x, group.y, group.z, group.w);
    }
}
