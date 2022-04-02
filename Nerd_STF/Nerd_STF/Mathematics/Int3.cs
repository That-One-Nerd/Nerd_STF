using Nerd_STF.Mathematics.Geometry;
using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace Nerd_STF.Mathematics
{
    public struct Int3 : ICloneable, IComparable<Int3>, IEquatable<Int3>, IGroup<int>
    {
        public static Int3 Back => new(0, 0, -1);
        public static Int3 Down => new(0, -1, 0);
        public static Int3 Forward => new(0, 0, 1);
        public static Int3 Left => new(-1, 0, 0);
        public static Int3 Right => new(1, 0, 0);
        public static Int3 Up => new(0, 1, 0);

        public static Int3 One => new(1, 1, 1);
        public static Int3 Zero => new(0, 0, 0);

        public double Magnitude => Mathf.Sqrt(x * x + y * y + z * z);
        public Int3 Normalized => (Int3)((Double3)this / Magnitude);

        public Int2 XY => new(x, y);
        public Int2 XZ => new(x, z);
        public Int2 YZ => new(y, z);

        public int x, y, z;

        public Int3(int all) : this(all, all, all) { }
        public Int3(int x, int y) : this(x, y, 0) { }
        public Int3(int x, int y, int z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
        public Int3(Fill<int> fill) : this(fill(0), fill(1), fill(2)) { }

        public int this[int index]
        {
            get => index switch
            {
                0 => x,
                1 => y,
                2 => z,
                _ => throw new IndexOutOfRangeException(nameof(index)),
            };
            set
            {
                switch (index)
                {
                    case 0:
                        x = value;
                        break;

                    case 1:
                        y = value;
                        break;

                    case 2:
                        z = value;
                        break;

                    default: throw new IndexOutOfRangeException(nameof(index));
                }
            }
        }

        public static Int3 Absolute(Int3 val) =>
            new(Mathf.Absolute(val.x), Mathf.Absolute(val.y), Mathf.Absolute(val.z));
        public static Int3 Average(params Int3[] vals) => Sum(vals) / vals.Length;
        public static Int3 Clamp(Int3 val, Int3 min, Int3 max) =>
            new(Mathf.Clamp(val.x, min.x, max.x),
                Mathf.Clamp(val.y, min.y, max.y),
                Mathf.Clamp(val.z, min.z, max.z));
        public static Int3 ClampMagnitude(Int3 val, int minMag, int maxMag)
        {
            if (maxMag < minMag) throw new ArgumentOutOfRangeException(nameof(maxMag),
                nameof(maxMag) + " must be greater than or equal to " + nameof(minMag));
            double mag = val.Magnitude;
            if (mag >= minMag && mag <= maxMag) return val;
            val = val.Normalized;
            if (mag < minMag) val *= minMag;
            else if (mag > maxMag) val *= maxMag;
            return val;
        }
        public static Int3 Cross(Int3 a, Int3 b, bool normalized = false)
        {
            Int3 val = new(a.y * b.z - b.y * a.z,
                           b.x * a.z - a.x * b.z,
                           a.x * b.y - b.x * a.y);
            return normalized ? val.Normalized : val;
        }
        public static Int3 Divide(Int3 num, params Int3[] vals)
        {
            foreach (Int3 d in vals) num /= d;
            return num;
        }
        public static int Dot(Int3 a, Int3 b) => a.x * b.x + a.y * b.y + a.z * b.z;
        public static int Dot(params Int3[] vals)
        {
            if (vals.Length < 1) return 0;
            int x = 1, y = 1, z = 1;
            foreach (Int3 d in vals)
            {
                x *= d.x;
                y *= d.y;
                z *= d.z;
            }
            return x + y + z;
        }
        public static Int3 Lerp(Int3 a, Int3 b, double t, bool clamp = true) =>
            new(Mathf.Lerp(a.x, b.x, t, clamp), Mathf.Lerp(a.y, b.y, t, clamp), Mathf.Lerp(a.z, b.z, t, clamp));
        public static Int3 Median(params Int3[] vals)
        {
            int index = Mathf.Average(0, vals.Length - 1);
            Int3 valA = vals[Mathf.Floor(index)], valB = vals[Mathf.Ceiling(index)];
            return Average(valA, valB);
        }
        public static Int3 Max(params Int3[] vals)
        {
            if (vals.Length < 1) return Zero;
            Int3 val = vals[0];
            foreach (Int3 d in vals) val = d > val ? d : val;
            return val;
        }
        public static Int3 Min(params Int3[] vals)
        {
            if (vals.Length < 1) return Zero;
            Int3 val = vals[0];
            foreach (Int3 d in vals) val = d < val ? d : val;
            return val;
        }
        public static Int3 Multiply(params Int3[] vals)
        {
            if (vals.Length < 1) return Zero;
            Int3 val = One;
            foreach (Int3 d in vals) val *= d;
            return val;
        }
        public static Int3 Subtract(Int3 num, params Int3[] vals)
        {
            foreach (Int3 d in vals) num -= d;
            return num;
        }
        public static Int3 Sum(params Int3[] vals)
        {
            Int3 val = Zero;
            foreach (Int3 d in vals) val += d;
            return val;
        }

        public int CompareTo(Int3 other)
        {
            double magA = Magnitude, magB = other.Magnitude;
            return magA == magB ? 0 : magA > magB ? 1 : -1;
        }
        public override bool Equals([NotNullWhen(true)] object? obj)
        {
            if (obj == null || obj.GetType() != typeof(Int3)) return false;
            return Equals((Int3)obj);
        }
        public bool Equals(Int3 other) => x == other.x && y == other.y && z == other.z;
        public override int GetHashCode() => x.GetHashCode() ^ y.GetHashCode() ^ z.GetHashCode();
        public override string ToString() => ToString((string?)null);
        public string ToString(string? provider) =>
            "X: " + x.ToString(provider) + " Y: " + y.ToString(provider) + " Z: " + z.ToString(provider);
        public string ToString(IFormatProvider provider) =>
            "X: " + x.ToString(provider) + " Y: " + y.ToString(provider) + " Z: " + z.ToString(provider);

        public object Clone() => new Int3(x, y, z);

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<int> GetEnumerator()
        {
            yield return x;
            yield return y;
            yield return z;
        }

        public int[] ToArray() => new[] { x, y, z };
        public List<int> ToList() => new() { x, y, z };

        public static Int3 operator +(Int3 a, Int3 b) => new(a.x + b.x, a.y + b.y, a.z + b.z);
        public static Int3 operator -(Int3 i) => new(-i.x, -i.y, -i.z);
        public static Int3 operator -(Int3 a, Int3 b) => new(a.x - b.x, a.y - b.y, a.z - b.z);
        public static Int3 operator *(Int3 a, Int3 b) => new(a.x * b.x, a.y * b.y, a.z * b.z);
        public static Int3 operator *(Int3 a, int b) => new(a.x * b, a.y * b, a.z * b);
        public static Int3 operator /(Int3 a, Int3 b) => new(a.x / b.x, a.y / b.y, a.z / b.z);
        public static Int3 operator /(Int3 a, int b) => new(a.x / b, a.y / b, a.z / b);
        public static bool operator ==(Int3 a, Int3 b) => a.Equals(b);
        public static bool operator !=(Int3 a, Int3 b) => !a.Equals(b);
        public static bool operator >(Int3 a, Int3 b) => a.CompareTo(b) > 0;
        public static bool operator <(Int3 a, Int3 b) => a.CompareTo(b) < 0;
        public static bool operator >=(Int3 a, Int3 b) => a == b || a > b;
        public static bool operator <=(Int3 a, Int3 b) => a == b || a < b;

        public static explicit operator Int3(Double2 val) => new((int)val.x, (int)val.y, 0);
        public static explicit operator Int3(Double3 val) => new((int)val.x, (int)val.y, (int)val.z);
        public static explicit operator Int3(Double4 val) => new((int)val.x, (int)val.y, (int)val.z);
        public static implicit operator Int3(Int2 val) => new(val.x, val.y, 0);
        public static explicit operator Int3(Int4 val) => new(val.x, val.y, val.z);
        public static explicit operator Int3(Vert val) => new((int)val.position.x, (int)val.position.y,
                                                              (int)val.position.z);
        public static implicit operator Int3(Fill<int> fill) => new(fill);
    }
}
