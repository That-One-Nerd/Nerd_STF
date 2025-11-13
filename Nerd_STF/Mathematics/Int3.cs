using Nerd_STF.Exceptions;
using Nerd_STF.Mathematics.Algebra;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Nerd_STF.Mathematics
{
    public struct Int3 : INumberGroup<Int3, int>
#if CS11_OR_GREATER
                        ,IFromTuple<Int3, (int, int, int)>,
                         IPresets3d<Int3>,
                         ISplittable<Int3, (int[] Xs, int[] Ys, int[] Zs)>
#endif
    {
        public static Int3 Backward => new Int3(0, 0, -1);
        public static Int3 Down => new Int3(0, -1, 0);
        public static Int3 Forward => new Int3(0, 0, 1);
        public static Int3 Left => new Int3(-1, 0, 0);
        public static Int3 Right => new Int3(1, 0, 0);
        public static Int3 Up => new Int3(0, 1, 0);

        public static Int3 One => new Int3(1, 1, 1);
        public static Int3 Zero => new Int3(0, 0, 0);

        public double InverseMagnitude => MathE.InverseSqrt(x * x + y * y + z * z);
        public double Magnitude => MathE.Sqrt(x * x + y * y + z * z);
        public Float3 Normalized => (Float3)this * InverseMagnitude;

        public int x, y, z;

        public Int3(int x, int y, int z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
        public Int3(IEnumerable<int> nums)
        {
            x = 0;
            y = 0;
            z = 0;

            int index = 0;
            foreach (int item in nums)
            {
                this[index] = item;
                index++;
                if (index == 3) break;
            }
        }
        public Int3(Fill<int> fill)
        {
            x = fill(0);
            y = fill(1);
            z = fill(2);
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
        public ListTuple<int> this[string key]
        {
            get
            {
                int[] items = new int[key.Length];
                for (int i = 0; i < key.Length; i++)
                {
                    char c = key[i];
                    switch (c)
                    {
                        case 'x': items[i] = x; break;
                        case 'y': items[i] = y; break;
                        case 'z': items[i] = z; break;
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
                    char c = key[i];
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

        public static Int3 Average(IEnumerable<Int3> values)
        {
            Int3 total = Zero;
            int count = 0;
            foreach (Int3 val in values)
            {
                total += val;
                count++;
            }
            return total / count;
        }
        public static Int3 Clamp(Int3 value, Int3 min, Int3 max) =>
            new Int3(MathE.Clamp(value.x, min.x, max.x),
                     MathE.Clamp(value.y, min.y, max.y),
                     MathE.Clamp(value.z, min.z, max.z));
        public static void Clamp(ref Int3 value, Int3 min, Int3 max)
        {
            MathE.Clamp(ref value.x, min.x, max.x);
            MathE.Clamp(ref value.y, min.y, max.y);
            MathE.Clamp(ref value.z, min.z, max.z);
        }
        public static Int3 ClampMagnitude(Int3 value, double minMag, double maxMag)
        {
            Int3 copy = value;
            ClampMagnitude(ref copy, minMag, maxMag);
            return copy;
        }
        public static void ClampMagnitude(ref Int3 value, double minMag, double maxMag)
        {
            if (minMag > maxMag) throw new ClampOrderMismatchException(nameof(minMag), nameof(maxMag));
            double mag = value.Magnitude;

            if (mag < minMag)
            {
                double factor = minMag / mag;
                value.x = MathE.Ceiling(value.x * factor);
                value.y = MathE.Ceiling(value.y * factor);
                value.z = MathE.Ceiling(value.z * factor);
            }
            else if (mag > maxMag)
            {
                double factor = maxMag / mag;
                value.x = MathE.Floor(value.x * factor);
                value.y = MathE.Floor(value.y * factor);
                value.z = MathE.Floor(value.z * factor);
            }
        }
        public static Int3 Cross(Int3 a, Int3 b) =>
            new Int3(a.y * b.z - a.z * b.y,
                     a.z * b.x - a.x * b.z,
                     a.x * b.y - a.y * b.x);
        public static int Dot(Int3 a, Int3 b) => a.x * b.x + a.y * b.y + a.z * b.z;
        public static int Dot(IEnumerable<Int3> values)
        {
            int x = 1, y = 1, z = 1;
            foreach (Int3 val in values)
            {
                x *= val.x;
                y *= val.y;
                z *= val.z;
            }
            return x + y + z;
        }
#if CS11_OR_GREATER
        static double IVectorOperations<Int3>.Dot(Int3 a, Int3 b) => Dot(a, b);
        static double IVectorOperations<Int3>.Dot(IEnumerable<Int3> vals) => Dot(vals);
#endif
        public static Int3 Lerp(Int3 a, Int3 b, double t, bool clamp = true) =>
            new Int3(MathE.Lerp(a.x, b.x, t, clamp),
                     MathE.Lerp(a.y, b.y, t, clamp),
                     MathE.Lerp(a.z, b.z, t, clamp));
        public static Int3 Product(IEnumerable<Int3> values)
        {
            bool any = false;
            Int3 total = One;
            foreach (Int3 val in values)
            {
                any = true;
                total *= val;
            }
            return any ? total : Zero;
        }
        public static Int3 Sum(IEnumerable<Int3> values)
        {
            Int3 total = Zero;
            foreach (Int3 val in values) total += val;
            return total;
        }

        public static (int[] Xs, int[] Ys, int[] Zs) SplitArray(IEnumerable<Int3> values)
        {
            int count = values.Count();
            int[] Xs = new int[count], Ys = new int[count], Zs = new int[count];
            int index = 0;
            foreach (Int3 val in values)
            {
                Xs[index] = val.x;
                Ys[index] = val.y;
                Zs[index] = val.z;
                index++;
            }
            return (Xs, Ys, Zs);
        }

        public IEnumerator<int> GetEnumerator()
        {
            yield return x;
            yield return y;
            yield return z;
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public void Dceconstruct(out int x, out int y, out int z)
        {
            x = this.x;
            y = this.y;
            z = this.z;
        }

        public bool Equals(Int3 other) => x == other.x && y == other.y && z == other.z;
#if CS8_OR_GREATER
        public override bool Equals(object? obj)
#else
        public override bool Equals(object obj)
#endif
        {
            if (obj is null) return false;
            else if (obj is Int3 objInt3) return Equals(objInt3);
            else if (obj is Float2 objFloat3) return objFloat3.Equals(this);
            else return false;
        }
        public override int GetHashCode() => x.GetHashCode() ^ y.GetHashCode() ^ z.GetHashCode();
        public override string ToString() => $"({x}, {y}, {z})";
        public string ToString(string format) => $"({x.ToString(format)}, {y.ToString(format)}, {z.ToString(format)})";

        public int[] ToArray() => new int[] { x, y, z };
        public Fill<int> ToFill()
        {
            Int3 copy = this;
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
        public List<int> ToList() => new List<int> { x, y, z };

        public static Int3 operator +(Int3 a, Int3 b) => new Int3(a.x + b.x, a.y + b.y, a.z + b.z);
        public static Int3 operator -(Int3 a) => new Int3(-a.x, -a.y, -a.z);
        public static Int3 operator -(Int3 a, Int3 b) => new Int3(a.x - b.x, a.y - b.y, a.z - b.z);
        public static Int3 operator *(Int3 a, int b) => new Int3(a.x * b, a.y * b, a.z * b);
        public static Int3 operator *(Int3 a, Int3 b) => new Int3(a.x * b.x, a.y * b.y, a.z * b.z);
        public static Int3 operator /(Int3 a, int b) => new Int3(a.x / b, a.y / b, a.z / b);
        public static Int3 operator /(Int3 a, Int3 b) => new Int3(a.x / b.x, a.y / b.y, a.z / b.z);
        public static Int3 operator &(Int3 a, Int3 b) => new Int3(a.x & b.x, a.y & b.y, a.z & b.z);
        public static Int3 operator |(Int3 a, Int3 b) => new Int3(a.x | b.x, a.y | b.y, a.z | b.z);
        public static Int3 operator ^(Int3 a, Int3 b) => new Int3(a.x ^ b.x, a.y ^ b.y, a.z ^ b.z);
        public static bool operator ==(Int3 a, Int3 b) => a.Equals(b);
        public static bool operator !=(Int3 a, Int3 b) => !a.Equals(b);

        public static implicit operator Int3(Int2 ints) => new Int3(ints.x, ints.y, 0);
        public static explicit operator Int3(Int4 ints) => new Int3(ints.x, ints.y, ints.z);
        public static explicit operator Int3(Float2 floats) => new Int3((int)floats.x, (int)floats.y, 0);
        public static explicit operator Int3(Float3 floats) => new Int3((int)floats.x, (int)floats.y, (int)floats.z);
        public static explicit operator Int3(Float4 floats) => new Int3((int)floats.x, (int)floats.y, (int)floats.z);
        public static explicit operator Int3(ListTuple<double> tuple) => new Int3((int)tuple[0], (int)tuple[1], (int)tuple[2]);
        public static implicit operator Int3(ListTuple<int> tuple) => new Int3(tuple[0], tuple[1], tuple[2]);
        public static implicit operator Int3((int, int, int) tuple) => new Int3(tuple.Item1, tuple.Item2, tuple.Item3);

        public static implicit operator ListTuple<double>(Int3 group) => new ListTuple<double>(group.x, group.y, group.z);
        public static implicit operator ListTuple<int>(Int3 group) => new ListTuple<int>(group.x, group.y, group.z);
        public static implicit operator ValueTuple<int, int, int>(Int3 group) => (group.x, group.y, group.z);
    }
}
