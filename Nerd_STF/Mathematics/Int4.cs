using Nerd_STF.Exceptions;
using Nerd_STF.Mathematics.Algebra;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Nerd_STF.Mathematics
{
    public struct Int4 : INumberGroup<Int4, int>
#if CS11_OR_GREATER
                        ,IFromTuple<Int4, (int, int, int, int)>,
                         IPresets4d<Int4>
#endif
    {
        public static Int4 Backward => new Int4( 0,  0, -1,  0);
        public static Int4 Down =>     new Int4( 0, -1,  0,  0);
        public static Int4 Forward =>  new Int4( 0,  0,  1,  0);
        public static Int4 HighW =>    new Int4( 0,  0,  0,  1);
        public static Int4 Left =>     new Int4(-1,  0,  0,  0);
        public static Int4 LowW =>     new Int4( 0,  0,  0, -1);
        public static Int4 Right =>    new Int4( 1,  0,  0,  0);
        public static Int4 Up =>       new Int4( 0,  1,  0,  0);

        public static Int4 One => new Int4(1, 1, 1, 1);
        public static Int4 Zero => new Int4(0, 0, 0, 0);

        public double InverseMagnitude => MathE.InverseSqrt(x * x + y * y + z * z + w * w);
        public double Magnitude => MathE.Sqrt(x * x + y * y + z * z + w * w);
        public int MagnitudeSqr => x * x + y * y + z * z + w * w;
        double IMagnitudeOperators<Int4>.MagnitudeSqr => MagnitudeSqr;
        public Float4 Normalized => (Float4)this * InverseMagnitude;

        public int x, y, z, w;

        public Int4(int x, int y, int z, int w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }
        public Int4(IEnumerable<int> nums)
        {
            x = 0;
            y = 0;
            z = 0;
            w = 0;

            int index = 0;
            foreach (int item in nums)
            {
                this[index] = item;
                index++;
                if (index == 4) break;
            }
        }
        public Int4(Fill<int> fill)
        {
            x = fill(0);
            y = fill(1);
            z = fill(2);
            w = fill(3);
        }

        public int this[int index]
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
                        case 'z': items[i] = z; break;
                        case 'w': items[i] = w; break;
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
                        case 'z': z = stepper.Current; break;
                        case 'w': w = stepper.Current; break;
                        default: throw new ArgumentException("Invalid key.", nameof(key));
                    }
                }
            }
        }

        public static Int4 Average(IEnumerable<Int4> values)
        {
            Int4 total = Zero;
            int count = 0;
            foreach (Int4 val in values)
            {
                total += val;
                count++;
            }
            return total / count;
        }
        public static Int4 Clamp(Int4 value, Int4 min, Int4 max) =>
            new Int4(MathE.Clamp(value.x, min.x, max.x),
                     MathE.Clamp(value.y, min.y, max.y),
                     MathE.Clamp(value.z, min.z, max.z),
                     MathE.Clamp(value.w, min.w, max.w));
        public static void Clamp(ref Int4 value, Int4 min, Int4 max)
        {
            MathE.Clamp(ref value.x, min.x, max.x);
            MathE.Clamp(ref value.y, min.y, max.y);
            MathE.Clamp(ref value.z, min.z, max.z);
            MathE.Clamp(ref value.w, min.w, max.w);
        }
        public static Int4 ClampMagnitude(Int4 value, double minMag, double maxMag)
        {
            Int4 copy = value;
            ClampMagnitude(ref copy, minMag, maxMag);
            return copy;
        }
        public static void ClampMagnitude(ref Int4 value, double minMag, double maxMag)
        {
            if (minMag > maxMag) throw new ClampOrderMismatchException(nameof(minMag), nameof(maxMag));
            double mag = value.Magnitude;
            if (mag < minMag)
            {
                double factor = minMag / mag;
                value.x = MathE.Ceiling(value.x * factor);
                value.y = MathE.Ceiling(value.y * factor);
                value.z = MathE.Ceiling(value.z * factor);
                value.w = MathE.Ceiling(value.w * factor);
            }
            else if (mag > maxMag)
            {
                double factor = maxMag / mag;
                value.x = MathE.Floor(value.x * factor);
                value.y = MathE.Floor(value.y * factor);
                value.z = MathE.Floor(value.z * factor);
                value.w = MathE.Floor(value.w * factor);
            }
        }
        public static int Dot(Int4 a, Int4 b) => a.x * b.x + a.y * b.y + a.z * b.z + a.w * b.w;
        public static int Dot(IEnumerable<Int4> values)
        {
            int x = 1, y = 1, z = 1, w = 1;
            foreach (Int4 val in values)
            {
                x *= val.x;
                y *= val.y;
                z *= val.z;
                w *= val.w;
            }
            return x + y + z + w;
        }
        public static Int4 Lerp(Int4 a, Int4 b, double t, bool clamp = true) =>
            new Int4(MathE.Lerp(a.x, b.x, t, clamp),
                     MathE.Lerp(a.y, b.y, t, clamp),
                     MathE.Lerp(a.z, b.z, t, clamp),
                     MathE.Lerp(a.w, b.w, t, clamp));
        public static Int4 Product(IEnumerable<Int4> values)
        {
            bool any = false;
            Int4 total = One;
            foreach (Int4 val in values)
            {
                any = true;
                total *= val;
            }
            return any ? total : Zero;
        }
        public static Int4 Sum(IEnumerable<Int4> values)
        {
            Int4 total = Zero;
            foreach (Int4 val in values) total += val;
            return total;
        }

        public IEnumerator<int> GetEnumerator()
        {
            yield return x;
            yield return y;
            yield return z;
            yield return w;
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public void Deconstruct(out int x, out int y, out int z, out int w)
        {
            x = this.x;
            y = this.y;
            z = this.z;
            w = this.w;
        }

        public bool Equals(Int4 other) => x == other.x && y == other.y && z == other.z && w == other.w;
#if CS8_OR_GREATER
        public override bool Equals(object? obj)
#else
        public override bool Equals(object obj)
#endif
        {
            if (obj is null) return false;
            else if (obj is Int4 objInt4) return Equals(objInt4);
            else if (obj is Float4 objFloat4) return objFloat4.Equals(this);
            else return false;
        }
        public override int GetHashCode() => x.GetHashCode() ^ y.GetHashCode() ^ z.GetHashCode() ^ w.GetHashCode();
        public override string ToString() => $"({x}, {y}, {z}, {w})";
        public string ToString(string format) => $"({x.ToString(format)}, {y.ToString(format)}, {z.ToString(format)}, {w.ToString(format)})";

        public int[] ToArray() => new int[] { x, y, z, w };
        public Fill<int> ToFill()
        {
            Int4 copy = this;
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
        public List<int> ToList() => new List<int> { x, y, z, w };

        public static Int4 operator +(Int4 a, Int4 b) => new Int4(a.x + b.x, a.y + b.y, a.z + b.z, a.w + b.w);
        public static Int4 operator -(Int4 a) => new Int4(-a.x, -a.y, -a.z, -a.w);
        public static Int4 operator -(Int4 a, Int4 b) => new Int4(a.x - b.x, a.y - b.y, a.z - b.z, a.w - b.w);
        public static Int4 operator *(Int4 a, int b) => new Int4(a.x * b, a.y * b, a.z * b, a.w * b);
        public static Int4 operator *(Int4 a, Int4 b) => new Int4(a.x * b.x, a.y * b.y, a.z * b.z, a.w * b.w);
        public static Int4 operator /(Int4 a, int b) => new Int4(a.x / b, a.y / b, a.z / b, a.w / b);
        public static Int4 operator /(Int4 a, Int4 b) => new Int4(a.x / b.x, a.y / b.y, a.z / b.z, a.w / b.w);
        public static Int4 operator &(Int4 a, Int4 b) => new Int4(a.x & b.x, a.y & b.y, a.z & b.z, a.w & b.w);
        public static Int4 operator |(Int4 a, Int4 b) => new Int4(a.x | b.x, a.y | b.y, a.z | b.z, a.w | b.w);
        public static Int4 operator ^(Int4 a, Int4 b) => new Int4(a.x ^ b.x, a.y ^ b.y, a.z ^ b.z, a.w ^ b.w);
        public static bool operator ==(Int4 a, Int4 b) => a.Equals(b);
        public static bool operator !=(Int4 a, Int4 b) => !a.Equals(b);

        public static implicit operator Int4(Int2 ints) => new Int4(ints.x, ints.y, 0, 0);
        public static implicit operator Int4(Int3 ints) => new Int4(ints.x, ints.y, ints.z, 0);
        public static explicit operator Int4(Float2 floats) => new Int4((int)floats.x, (int)floats.y, 0, 0);
        public static explicit operator Int4(Float3 floats) => new Int4((int)floats.x, (int)floats.y, (int)floats.z, 0);
        public static explicit operator Int4(Float4 floats) => new Int4((int)floats.x, (int)floats.y, (int)floats.z, (int)floats.w);
        public static explicit operator Int4(Numbers.Quaternion quat) => new Int4((int)quat.x, (int)quat.y, (int)quat.z, (int)quat.w);
        public static explicit operator Int4(ListTuple<double> tuple) => new Int4((int)tuple[0], (int)tuple[1], (int)tuple[2], (int)tuple[3]);
        public static implicit operator Int4(ListTuple<int> tuple) => new Int4(tuple[0], tuple[1], tuple[2], tuple[3]);
        public static implicit operator Int4((int, int, int, int) tuple) => new Int4(tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4);

        public static implicit operator ListTuple<double>(Int4 group) => new ListTuple<double>(group.x, group.y, group.z, group.w);
        public static implicit operator ListTuple<int>(Int4 group) => new ListTuple<int>(group.x, group.y, group.z, group.w);
        public static implicit operator ValueTuple<int, int, int, int>(Int4 group) => (group.x, group.y, group.z, group.w);
    }
}
