using Nerd_STF.Exceptions;
using Nerd_STF.Helpers;
using Nerd_STF.Mathematics.Algebra;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Nerd_STF.Mathematics.Numbers
{
    public struct Quaternion : IComparable<double>,
                               IEnumerable<double>,
                               IFormattable,
                               INumberGroup<Quaternion, double>
#if CS11_OR_GREATER
                              ,//INumber<Quaternion>, Maybe some day.
                               IFromTuple<Quaternion, (double, double, double, double)>,
                               IInterpolable<Quaternion>,
                               IPresets4d<Quaternion>,
                               IProductOperation<Quaternion>,
                               IRoundable<Quaternion>,
                               ISumOperation<Quaternion>,
                               IVector<Quaternion>
#endif
    {
        public static Quaternion Backward => new Quaternion( 0,  0, -1,  0);
        public static Quaternion Down =>     new Quaternion( 0, -1,  0,  0);
        public static Quaternion Forward =>  new Quaternion( 0,  0,  1,  0);
        public static Quaternion HighW =>    new Quaternion( 0,  0,  0,  1);
        public static Quaternion Left =>     new Quaternion(-1,  0,  0,  0);
        public static Quaternion LowW =>     new Quaternion( 0,  0,  0, -1);
        public static Quaternion Right =>    new Quaternion( 1,  0,  0,  0);
        public static Quaternion Up =>       new Quaternion( 0,  1,  0,  0);

        public static Quaternion One => new Quaternion(1, 1, 1, 1);
        public static Quaternion Zero => new Quaternion(0, 0, 0, 0);

        public Quaternion Conjugate => new Quaternion(-x, -y, -z, w);
        public double Magnitude => MathE.Sqrt(x * x + y * y + z * z + w * w);
        public double MagnitudeSqr => x * x + y * y + z * z + w * w;

        public double x, y, z, w;

        public Quaternion(double x, double y, double z, double w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }
        public Quaternion(IEnumerable<double> nums)
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
        public Quaternion(Fill<double> fill)
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

        public static Quaternion Abs(Quaternion num) => new Quaternion(num.Magnitude, 0, 0, 0);
        public static Quaternion Ceiling(Quaternion num) =>
            new Quaternion(MathE.Ceiling(num.x),
                           MathE.Ceiling(num.y),
                           MathE.Ceiling(num.z),
                           MathE.Ceiling(num.w));
        public static Quaternion Ceiling(Quaternion num, Quaternion min, Quaternion max) =>
            new Quaternion(MathE.Clamp(num.x, min.x, max.x),
                           MathE.Clamp(num.y, min.y, max.y),
                           MathE.Clamp(num.z, min.z, max.z),
                           MathE.Clamp(num.w, min.w, max.w));
        public static Quaternion ClampMagnitude(Quaternion num, double minMag, double maxMag)
        {
            Quaternion copy = num;
            ClampMagnitude(ref copy, minMag, maxMag);
            return copy;
        }
        public static void ClampMagnitude(ref Quaternion num, double minMag, double maxMag)
        {
            if (minMag > maxMag) throw new ClampOrderMismatchException(nameof(minMag), nameof(maxMag));
            double mag = num.Magnitude;

            double factor;
            if (mag < minMag) factor = minMag / mag;
            else if (mag > maxMag) factor = maxMag / mag;
            else factor = 1;

            num.x *= factor;
            num.y *= factor;
            num.z *= factor;
            num.w *= factor;
        }
        public static double Dot(Quaternion a, Quaternion b) => a.x * b.x + a.y * b.y + a.z * b.z + a.w * b.w;
        public static double Dot(IEnumerable<Quaternion> nums)
        {
            double x = 1, y = 1, z = 1, w = 1;
            foreach (Quaternion q in nums)
            {
                x *= q.x;
                y *= q.y;
                z *= q.z;
                w *= q.w;
            }
            return x + y + z + w;
        }
        public static Quaternion Floor(Quaternion num) =>
            new Quaternion(MathE.Floor(num.x),
                           MathE.Floor(num.y),
                           MathE.Floor(num.z),
                           MathE.Floor(num.w));
        public static Quaternion Lerp(Quaternion a, Quaternion b, double t, bool clamp = true) =>
            new Quaternion(MathE.Lerp(a.x, b.x, t, clamp),
                           MathE.Lerp(a.y, b.y, t, clamp),
                           MathE.Lerp(a.z, b.z, t, clamp),
                           MathE.Lerp(a.w, b.w, t, clamp));
        public static Quaternion Product(IEnumerable<Quaternion> nums)
        {
            bool any = false;
            Quaternion result = One;
            foreach (Quaternion q in nums)
            {
                any = true;
                result *= q;
            }
            return any ? result : Zero;
        }
        public static Quaternion Round(Quaternion num) =>
            new Quaternion(MathE.Round(num.x),
                           MathE.Round(num.y),
                           MathE.Round(num.z),
                           MathE.Round(num.w));
        public static Quaternion Sum(IEnumerable<Quaternion> nums)
        {
            bool any = false;
            Quaternion result = One;
            foreach (Quaternion q in nums)
            {
                any = true;
                result += q;
            }
            return any ? result : Zero;
        }

        public IEnumerator<double> GetEnumerator()
        {
            yield return x;
            yield return y;
            yield return z;
            yield return w;
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public int CompareTo(double other) => MagnitudeSqr.CompareTo(other * other);
        public int CompareTo(Quaternion other) => MagnitudeSqr.CompareTo(other.MagnitudeSqr);
#if CS8_OR_GREATER
        public int CompareTo(object? other)
#else
        public int CompareTo(object other)
#endif
        {
            if (other is null) return 1;
            else if (other is Quaternion otherQuat) return CompareTo(otherQuat);
            else if (other is double otherNum) return CompareTo(otherNum);
            //else if (TryConvertFrom(other, out Quaternion otherConvert)) return CompareTo(otherConvert);
            else return 1;
        }
        public bool Equals(double other) => x == other && y == 0 && z == 0 && w == 0;
        public bool Equals(Complex other) => x == other.r && y == other.i && z == 0 && w == 0;
        public bool Equals(Quaternion other) => x == other.x && y == other.y && z == other.z && w == other.w;
#if CS8_OR_GREATER
        public override bool Equals(object? other)
#else
        public override bool Equals(object other)
#endif
        {
            if (other is null) return false;
            else if (other is Quaternion otherQuat) return Equals(otherQuat);
            else if (other is Complex otherComp) return Equals(otherComp);
            else if (other is double otherNum) return Equals(otherNum);
            //else if (TryConvertFrom(other, out Quaternion otherConvert)) return Equals(otherConvert);
            else return false;
        }
        public override int GetHashCode() => base.GetHashCode();
        public override string ToString() => ToStringHelper.HighDimNumberToString(this, null, null);
#if CS8_OR_GREATER
        public string ToString(string? format) => ToStringHelper.HighDimNumberToString(this, format, null);
        public string ToString(IFormatProvider? provider) => ToStringHelper.HighDimNumberToString(this, null, provider);
        public string ToString(string? format, IFormatProvider? provider) => ToStringHelper.HighDimNumberToString(this, format, provider);
#else
        public string ToString(string format) => ToStringHelper.HighDimNumberToString(this, format, null);
        public string ToString(IFormatProvider provider) => ToStringHelper.HighDimNumberToString(this, null, provider);
        public string ToString(string format, IFormatProvider provider) => ToStringHelper.HighDimNumberToString(this, format, provider);
#endif

        public double[] ToArray() => new double[] { x, y, z, w };
        public Fill<double> ToFill()
        {
            Quaternion @this = this;
            return i => @this[i];
        }
        public List<double> ToList() => new List<double>() { x, y, z, w };

        public static Quaternion operator +(Quaternion a) => a;
        public static Quaternion operator +(Quaternion a, Quaternion b) => new Quaternion(a.x + b.x, a.y + b.y, a.z + b.z, a.w + b.w);
        public static Quaternion operator +(Quaternion a, Complex b) => new Quaternion(a.x + b.r, a.y + b.i, a.z, a.w);
        public static Quaternion operator +(Quaternion a, double b) => new Quaternion(a.x + b, a.y, a.z, a.w);
        public static Quaternion operator -(Quaternion a) => new Quaternion(-a.x, -a.y, -a.z, -a.w);
        public static Quaternion operator -(Quaternion a, Quaternion b) => new Quaternion(a.x - b.x, a.y - b.y, a.z - b.z, a.w - b.w);
        public static Quaternion operator -(Quaternion a, Complex b) => new Quaternion(a.x - b.r, a.y - b.i, a.z, a.w);
        public static Quaternion operator -(Quaternion a, double b) => new Quaternion(a.x - b, a.y, a.z, a.w);
        public static Quaternion operator *(Quaternion a, Quaternion b) =>
            new Quaternion(a.x * b.x - a.y * b.y - a.z * b.z - a.w * b.w,
                           a.x * b.y + a.y * b.x + a.z * b.w - a.w * b.z,
                           a.x * b.z - a.y * b.w + a.z * b.x + a.w * b.y,
                           a.x * b.w + a.y * b.z - a.z * b.y + a.w * b.x);
        public static Quaternion operator *(Quaternion a, Complex b) =>
            new Quaternion(a.x * b.r - a.y * b.i,
                           a.y * b.r + a.x * b.i,
                           a.z * b.r + a.w * b.i,
                           a.w * b.r - a.z * b.i);
        public static Quaternion operator *(Quaternion a, double b) => new Quaternion(a.x * b, a.y * b, a.z * b, a.w * b);
        public static Quaternion operator /(Quaternion a, double b) => new Quaternion(a.x / b, a.y / b, a.z / b, a.w / b);
        public static bool operator ==(Quaternion a, Quaternion b) => a.Equals(b);
        public static bool operator !=(Quaternion a, Quaternion b) => !a.Equals(b);
        public static bool operator >(Quaternion a, Quaternion b) => a.CompareTo(b) > 0;
        public static bool operator <(Quaternion a, Quaternion b) => a.CompareTo(b) < 0;
        public static bool operator >=(Quaternion a, Quaternion b) => a.CompareTo(b) >= 0;
        public static bool operator <=(Quaternion a, Quaternion b) => a.CompareTo(b) <= 0;

        public static implicit operator Quaternion(Complex complex) => new Quaternion(complex.r, complex.i, 0, 0);
        public static implicit operator Quaternion(Float4 group) => new Quaternion(group.x, group.y, group.z, group.w);
        public static explicit operator Quaternion(Int4 group) => new Quaternion(group.x, group.y, group.z, group.w);
        public static implicit operator Quaternion(ListTuple<double> tuple) => new Quaternion(tuple[0], tuple[1], tuple[2], tuple[3]);
        public static explicit operator Quaternion(System.Numerics.Quaternion quat) => new Quaternion(quat.X, quat.Y, quat.Z, quat.W);
        public static implicit operator Quaternion(ValueTuple<double, double, double, double> tuple) => new Quaternion(tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4);

        public static implicit operator ListTuple<double>(Quaternion quat) => new ListTuple<double>(quat.x, quat.y, quat.z, quat.w);
        public static implicit operator System.Numerics.Quaternion(Quaternion quat) => new System.Numerics.Quaternion((float)quat.x, (float)quat.y, (float)quat.z, (float)quat.w);
        public static implicit operator ValueTuple<double, double, double, double>(Quaternion quat) => (quat.x, quat.y, quat.z, quat.w);
    }
}
