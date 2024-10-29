using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Nerd_STF.Exceptions;
using Nerd_STF.Mathematics.Abstract;

namespace Nerd_STF.Mathematics
{
    public struct Int4 : INumberGroup<Int4, int>
#if CS11_OR_GREATER
                        ,IFromTuple<Int4, (int, int, int, int)>,
                         IPresets4d<Int4>,
                         ISplittable<Int4, (int[] Ws, int[] Xs, int[] Ys, int[] Zs)>
#endif
    {
        public static Int4 Backward => new Int4(0, 0, 0, -1);
        public static Int4 Down => new Int4(0, 0, -1, 0);
        public static Int4 Forward => new Int4(0, 0, 0, 1);
        public static Int4 HighW => new Int4(1, 0, 0, 0);
        public static Int4 Left => new Int4(0, -1, 0, 0);
        public static Int4 LowW => new Int4(-1, 0, 0, 0);
        public static Int4 Right => new Int4(0, 1, 0, 0);
        public static Int4 Up => new Int4(0, 0, 1, 0);

        public static Int4 One => new Int4(1, 1, 1, 1);
        public static Int4 Zero => new Int4(0, 0, 0, 0);

        public double InverseMagnitude => MathE.InverseSqrt(w * w + x * x + y * y + z * z);
        public double Magnitude => MathE.Sqrt(w * w + x * x + y * y + z * z);
        public Float4 Normalized => (Float4)this * InverseMagnitude;

        public int w, x, y, z;

        public Int4(int w, int x, int y, int z)
        {
            this.w = w;
            this.x = x;
            this.y = y;
            this.z = z;
        }
        public Int4(IEnumerable<int> nums)
        {
            w = 0;
            x = 0;
            y = 0;
            z = 0;

            int index = 0;
            foreach (int item in nums)
            {
                this[index] = item;
                index++;
                if (index >= 2) break;
            }
        }

        public int this[int index]
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
        public IEnumerable<int> this[string key]
        {
            get
            {
                for (int i = 0; i < key.Length; i++)
                {
                    char c = key[i];
                    switch (c)
                    {
                        case 'w': yield return w; break;
                        case 'x': yield return x; break;
                        case 'y': yield return y; break;
                        case 'z': yield return z; break;
                        default: throw new ArgumentException("Invalid key.", nameof(key));
                    }
                }
            }
            set
            {
                IEnumerator<int> stepper = value.GetEnumerator();
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
            new Int4(MathE.Clamp(value.w, min.w, max.w),
                     MathE.Clamp(value.x, min.x, max.x),
                     MathE.Clamp(value.y, min.y, max.y),
                     MathE.Clamp(value.z, min.z, max.z));
        public static void Clamp(ref Int4 value, Int4 min, Int4 max)
        {
            MathE.Clamp(ref value.w, min.w, max.w);
            MathE.Clamp(ref value.x, min.x, max.x);
            MathE.Clamp(ref value.y, min.y, max.y);
            MathE.Clamp(ref value.z, min.z, max.z);
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
                value.w = (int)(value.w * factor);
                value.x = (int)(value.x * factor);
                value.y = (int)(value.y * factor);
                value.z = (int)(value.z * factor);
            }
            else if (mag > maxMag)
            {
                double factor = maxMag / mag;
                value.w = (int)(value.w * factor);
                value.x = (int)(value.x * factor);
                value.y = (int)(value.y * factor);
                value.z = (int)(value.z * factor);
            }
        }
        public static int Dot(Int4 a, Int4 b) => a.w * b.w + a.x * b.x + a.y * b.y + a.z * b.z;
        public static int Dot(IEnumerable<Int4> values)
        {
            int w = 1, x = 1, y = 1, z = 1;
            foreach (Int4 val in values)
            {
                w *= val.w;
                x *= val.x;
                y *= val.y;
                z *= val.z;
            }
            return w + x + y + z;
        }
        public static Int4 Lerp(Int4 a, Int4 b, double t, bool clamp = true) =>
            new Int4(MathE.Lerp(a.w, b.w, t, clamp),
                     MathE.Lerp(a.x, b.x, t, clamp),
                     MathE.Lerp(a.y, b.y, t, clamp),
                     MathE.Lerp(a.z, b.z, t, clamp));
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

        public static (int[] Ws, int[] Xs, int[] Ys, int[] Zs) SplitArray(IEnumerable<Int4> values)
        {
            int count = values.Count();
            int[] Ws = new int[count], Xs = new int[count], Ys = new int[count], Zs = new int[count];
            int index = 0;
            foreach (Int4 val in values)
            {
                Ws[index] = val.w;
                Xs[index] = val.x;
                Ys[index] = val.y;
                Zs[index] = val.z;
                index++;
            }
            return (Ws, Xs, Ys, Zs);
        }

        public IEnumerator<int> GetEnumerator()
        {
            yield return w;
            yield return x;
            yield return y;
            yield return z;
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public void Deconstruct(out int w, out int x, out int y, out int z)
        {
            w = this.w;
            x = this.x;
            y = this.y;
            z = this.z;
        }

        public bool Equals(Int4 other) => w == other.w && x == other.x && y == other.y && z == other.z;
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
        public override int GetHashCode() => w.GetHashCode() ^ x.GetHashCode() ^ y.GetHashCode() ^ z.GetHashCode();
        public override string ToString() => $"({w}, {x}, {y}, {z})";
        public string ToString(string format) => $"({w.ToString(format)}, {x.ToString(format)}, {y.ToString(format)}, {z.ToString(format)})";

        public int[] ToArray() => new int[] { w, x, y, z };
        public List<int> ToList() => new List<int> { w, x, y, z };

        public static Int4 operator +(Int4 a, Int4 b) => new Int4(a.w + b.w, a.x + b.x, a.y + b.y, a.z + b.z);
        public static Int4 operator -(Int4 a) => new Int4(-a.w, -a.x, -a.y, -a.z);
        public static Int4 operator -(Int4 a, Int4 b) => new Int4(a.w - b.w, a.x - b.x, a.y - b.y, a.z - b.z);
        public static Int4 operator *(Int4 a, int b) => new Int4(a.w * b, a.x * b, a.y * b, a.z * b);
        public static Int4 operator *(Int4 a, Int4 b) => new Int4(a.w * b.w, a.x * b.x, a.y * b.y, a.z * b.z);
        public static Int4 operator /(Int4 a, int b) => new Int4(a.w / b, a.x / b, a.y / b, a.z / b);
        public static Int4 operator /(Int4 a, Int4 b) => new Int4(a.w / b.w, a.x / b.x, a.y / b.y, a.z / b.z);
        public static Int4 operator &(Int4 a, Int4 b) => new Int4(a.w & b.w, a.x & b.x, a.y & b.y, a.z & b.z);
        public static Int4 operator |(Int4 a, Int4 b) => new Int4(a.w | b.w, a.x | b.x, a.y | b.y, a.z | b.z);
        public static Int4 operator ^(Int4 a, Int4 b) => new Int4(a.w ^ b.w, a.x ^ b.x, a.y ^ b.y, a.z ^ b.z);
        public static bool operator ==(Int4 a, Int4 b) => a.Equals(b);
        public static bool operator !=(Int4 a, Int4 b) => !a.Equals(b);

        public static implicit operator Int4(Int2 ints) => new Int4(0, ints.x, ints.y, 0);
        public static implicit operator Int4(Int3 ints) => new Int4(0, ints.x, ints.y, ints.z);
        public static explicit operator Int4(Float2 floats) => new Int4(0, (int)floats.x, (int)floats.y, 0);
        public static explicit operator Int4(Float3 floats) => new Int4(0, (int)floats.x, (int)floats.y, (int)floats.z);
        public static explicit operator Int4(Float4 floats) => new Int4((int)floats.w, (int)floats.x, (int)floats.y, (int)floats.z);
        public static implicit operator Int4((int, int, int, int) tuple) => new Int4(tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4);

        public static implicit operator ValueTuple<int, int, int, int>(Int4 group) => (group.w, group.x, group.y, group.z);
    }
}
