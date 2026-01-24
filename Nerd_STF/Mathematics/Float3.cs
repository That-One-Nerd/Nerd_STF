using Nerd_STF.Exceptions;
using Nerd_STF.Graphics;
using Nerd_STF.Mathematics.Algebra;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Nerd_STF.Mathematics
{
    public struct Float3 : INumberGroup<Float3, double>
#if CS11_OR_GREATER
                          ,IFromTuple<Float3, (double, double, double)>,
                           IPresets2d<Float3>,
                           IRoundable<Float3, Int3>,
                           IRefRoundable<Float3>,
                           IVector<Float3>
#endif
    {
        public static Float3 Backward => new Float3(0, 0, -1);
        public static Float3 Down => new Float3(0, -1, 0);
        public static Float3 Forward => new Float3(0, 0, 1);
        public static Float3 Left => new Float3(-1, 0, 0);
        public static Float3 Right => new Float3(1, 0, 0);
        public static Float3 Up => new Float3(0, 1, 0);

        public static Float3 One => new Float3(1, 1, 1);
        public static Float3 Zero => new Float3(0, 0, 0);

        public double InverseMagnitude => MathE.InverseSqrt(x * x + y * y + z * z);
        public double Magnitude => MathE.Sqrt(x * x + y * y + z * z);
        public Float3 Normalized => this * InverseMagnitude;

        public double x, y, z;

        public Float3(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
        public Float3(IEnumerable<double> nums)
        {
            x = 0;
            y = 0;
            z = 0;

            int index = 0;
            foreach (double item in nums)
            {
                this[index] = item;
                index++;
                if (index == 3) break;
            }
        }
        public Float3(Fill<double> fill)
        {
            x = fill(0);
            y = fill(1);
            z = fill(2);
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
                        default: throw new ArgumentException("Invalid key.", nameof(key));
                    }
                }
            }
        }

        public static Float3 Average(IEnumerable<Float3> values)
        {
            Float3 total = Zero;
            int count = 0;
            foreach (Float3 val in values)
            {
                total += val;
                count++;
            }
            return total / count;
        }
        public static Int3 Ceiling(Float3 val) =>
            new Int3(MathE.Ceiling(val.x),
                     MathE.Ceiling(val.y),
                     MathE.Ceiling(val.z));
        public static void Ceiling(ref Float3 val)
        {
            MathE.Ceiling(ref val.x);
            MathE.Ceiling(ref val.y);
            MathE.Ceiling(ref val.z);
        }
        public static Float3 Clamp(Float3 value, Float3 min, Float3 max) =>
            new Float3(MathE.Clamp(value.x, min.x, max.x),
                       MathE.Clamp(value.y, min.y, max.y),
                       MathE.Clamp(value.z, min.z, max.z));
        public static void Clamp(ref Float3 value, Float3 min, Float3 max)
        {
            MathE.Clamp(ref value.x, min.x, max.x);
            MathE.Clamp(ref value.y, min.y, max.y);
            MathE.Clamp(ref value.z, min.z, max.z);
        }
        public static Float3 ClampMagnitude(Float3 value, double minMag, double maxMag)
        {
            Float3 copy = value;
            ClampMagnitude(ref copy, minMag, maxMag);
            return copy;
        }
        public static void ClampMagnitude(ref Float3 value, double minMag, double maxMag)
        {
            if (minMag > maxMag) throw new ClampOrderMismatchException(nameof(minMag), nameof(maxMag));
            double mag = value.Magnitude;

            if (mag < minMag)
            {
                double factor = minMag / mag;
                value.x *= factor;
                value.y *= factor;
                value.z *= factor;
            }
            else if (mag > maxMag)
            {
                double factor = maxMag / mag;
                value.x *= factor;
                value.y *= factor;
                value.z *= factor;
            }
        }
        public static Float3 Cross(Float3 a, Float3 b) =>
            new Float3(a.y * b.z - a.z * b.y,
                       a.z * b.x - a.x * b.z,
                       a.x * b.y - a.y * b.x);
        public static double Dot(Float3 a, Float3 b) => a.x * b.x + a.y * b.y + a.z * b.z;
        public static double Dot(IEnumerable<Float3> values)
        {
            double x = 1, y = 1, z = 1;
            foreach (Float3 val in values)
            {
                x *= val.x;
                y *= val.y;
                z *= val.z;
            }
            return x + y + z;
        }
        public static Int3 Floor(Float3 value) =>
            new Int3(MathE.Floor(value.x),
                     MathE.Floor(value.y),
                     MathE.Floor(value.z));
        public static void Floor(ref Float3 value)
        {
            MathE.Floor(ref value.x);
            MathE.Floor(ref value.y);
            MathE.Floor(ref value.z);
        }
        public static Float3 Lerp(Float3 a, Float3 b, double t, bool clamp = true) =>
            new Float3(MathE.Lerp(a.x, b.x, t, clamp),
                       MathE.Lerp(a.y, b.y, t, clamp),
                       MathE.Lerp(a.z, b.z, t, clamp));
        public static Float3 Product(IEnumerable<Float3> values)
        {
            bool any = false;
            Float3 result = One;
            foreach (Float3 val in values)
            {
                any = true;
                result *= val;
            }
            return any ? result : Zero;
        }
        public static Int3 Round(Float3 value) =>
            new Int3(MathE.Round(value.x),
                     MathE.Round(value.y),
                     MathE.Round(value.z));
        public static void Round(ref Float3 value)
        {
            MathE.Round(ref value.x);
            MathE.Round(ref value.y);
            MathE.Round(ref value.z);
        }
        public static Float3 Sum(IEnumerable<Float3> values)
        {
            Float3 result = Zero;
            foreach (Float3 val in values) result += val;
            return result;
        }

        public void Normalize()
        {
            double invMag = InverseMagnitude;
            x *= invMag;
            y *= invMag;
            z *= invMag;
        }

        public IEnumerator<double> GetEnumerator()
        {
            yield return x;
            yield return y;
            yield return z;
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public void Deconstruct(out double x, out double y, out double z)
        {
            x = this.x;
            y = this.y;
            z = this.z;
        }

        public bool Equals(Float3 other) => x == other.x && y == other.y && z == other.z;
#if CS8_OR_GREATER
        public override bool Equals(object? obj)
#else
        public override bool Equals(object obj)
#endif
        {
            if (obj is null) return false;
            else if (obj is Float3 objFloat3) return Equals(objFloat3);
            else if (obj is Int3 objInt3) return Equals(objInt3);
            else return false;
        }
        public override int GetHashCode() => x.GetHashCode() ^ y.GetHashCode() ^ z.GetHashCode();
        public override string ToString() => $"({x}, {y}, {z})";
        public string ToString(string format) => $"({x.ToString(format)}, {y.ToString(format)}, {z.ToString(format)})";

        public double[] ToArray() => new double[] { x, y, z };
        public Fill<double> ToFill()
        {
            Float3 copy = this;
            return delegate (int i)
            {
                switch (i)
                {
                    case 0: return copy.x;
                    case 1: return copy.y;
                    case 2: return copy.z;
                    default: throw new ArgumentOutOfRangeException(nameof(i));
                }
            };
        }
        public List<double> ToList() => new List<double> { x, y, z };

        public static Float3 operator +(Float3 a, Float3 b) => new Float3(a.x + b.x, a.y + b.y, a.z + b.z);
        public static Float3 operator -(Float3 a) => new Float3(-a.x, -a.y, -a.z);
        public static Float3 operator -(Float3 a, Float3 b) => new Float3(a.x - b.x, a.y - b.y, a.z - b.z);
        public static Float3 operator *(Float3 a, double b) => new Float3(a.x * b, a.y * b, a.z * b);
        public static Float3 operator *(Float3 a, Float3 b) => new Float3(a.x * b.x, a.y * b.y, a.z * b.z);
        public static Float3 operator /(Float3 a, double b) => new Float3(a.x / b, a.y / b, a.z / b);
        public static Float3 operator /(Float3 a, Float3 b) => new Float3(a.x / b.x, a.y / b.y, a.z / b.z);
        public static bool operator ==(Float3 a, Float3 b) => a.Equals(b);
        public static bool operator !=(Float3 a, Float3 b) => !a.Equals(b);

        public static explicit operator Float3(ColorRGB color) => new Float3(color.r, color.g, color.b);
        public static implicit operator Float3(Float2 floats) => new Float3(floats.x, floats.y, 0);
        public static explicit operator Float3(Float4 floats) => new Float3(floats.x, floats.y, floats.z);
        public static implicit operator Float3(Int2 ints) => new Float3(ints.x, ints.y, 0);
        public static implicit operator Float3(Int3 ints) => new Float3(ints.x, ints.y, ints.z);
        public static explicit operator Float3(Int4 ints) => new Float3(ints.x, ints.y, ints.z);
        public static explicit operator Float3(Matrix mat) => new Float3(mat.TryGet(0, 0), mat.TryGet(1, 0), mat.TryGet(2, 0));
        public static implicit operator Float3(Vector2 vec) => new Float3(vec.X, vec.Y, 0);
        public static implicit operator Float3(Vector3 vec) => new Float3(vec.X, vec.Y, vec.Z);
        public static explicit operator Float3(Vector4 vec) => new Float3(vec.X, vec.Y, vec.Z);
        public static implicit operator Float3(ListTuple<double> tuple) => new Float3(tuple[0], tuple[1], tuple[2]);
        public static implicit operator Float3(ListTuple<int> tuple) => new Float3(tuple[0], tuple[1], tuple[2]);
        public static implicit operator Float3((double, double, double) tuple) => new Float3(tuple.Item1, tuple.Item2, tuple.Item3);

        public static explicit operator Vector2(Float3 group) => new Vector2((float)group.x, (float)group.y);
        public static implicit operator Vector3(Float3 group) => new Vector3((float)group.x, (float)group.y, (float)group.z);
        public static implicit operator Vector4(Float3 group) => new Vector4((float)group.x, (float)group.y, (float)group.z, 0);
        public static implicit operator ListTuple<double>(Float3 group) => new ListTuple<double>(group.x, group.y, group.z);
        public static explicit operator ListTuple<int>(Float3 group) => new ListTuple<int>((int)group.x, (int)group.y, (int)group.z);
        public static implicit operator ValueTuple<double, double, double>(Float3 group) => (group.x, group.y, group.z);
    }
}
