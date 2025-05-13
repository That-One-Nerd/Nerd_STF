using Nerd_STF.Exceptions;
using Nerd_STF.Helpers;
using Nerd_STF.Mathematics.Algebra;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Numerics;

namespace Nerd_STF.Mathematics.Numbers
{
    public struct Complex : IComparable<Complex>,
                            IEquatable<Complex>,
                            IFormattable,
                            INumberGroup<Complex, double>
#if CS11_OR_GREATER
                           ,INumber<Complex>,
                            IFromTuple<Complex, (double, double)>,
                            IInterpolable<Complex>,
                            IPresets2d<Complex>,
                            IRoundable<Complex>,
                            ISimpleMathOperations<Complex>,
                            ISplittable<Complex, (double[] reals, double[] imaginaries)>,
                            IVectorOperations<Complex>
#endif
    {
        public static Complex Down => new Complex(0, 1);
        public static Complex Left => new Complex(-1, 0);
        public static Complex Right => new Complex(1, 0);
        public static Complex Up => new Complex(0, -1);

        public static Complex One => new Complex(1, 0);
        public static Complex Zero => new Complex(0, 0);

        public static Complex NaN => new Complex(double.NaN, double.NaN);

        public Complex Conjugate => new Complex(r, -i);
        public double Magnitude => MathE.Sqrt(r * r + i * i);
        public double MagnitudeSqr => r * r + i * i;

        public double Real
        {
            get => r;
            set => r = value;
        }
        public double Imaginary
        {
            get => i;
            set => i = value;
        }

        public double r, i;

        public Complex(double real, double imaginary)
        {
            r = real;
            i = imaginary;
        }

        public double this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0: return r;
                    case 1: return i;
                    default: throw new ArgumentOutOfRangeException(nameof(index));
                }
            }
            set
            {
                switch (index)
                {
                    case 0: r = value; break;
                    case 1: i = value; break;
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
                        case 'r': items[i] = r; break;
                        case 'i': items[i] = this.i; break;
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
                        case 'r': r = stepper.Current; break;
                        case 'i': this.i = stepper.Current; break;
                        default: throw new ArgumentException("Invalid key.", nameof(key));
                    }
                }
            }
        }

#if CS8_OR_GREATER
        public static Complex Parse(string? str) =>
#else
        public static Complex Parse(string str) =>
#endif
            str is null ? NaN : Parse(str.AsSpan());
        public static Complex Parse(ReadOnlySpan<char> str)
        {
            if (TryParse(str, out Complex result)) return result;
            else throw new FormatException("Cannot parse complex number from input.");
        }
#if CS8_OR_GREATER
        public static bool TryParse(string? str, out Complex frac) =>
#else
        public static bool TryParse(string str, out Complex frac) =>
#endif
            TryParse(str.AsSpan(), out frac);
        public static bool TryParse(ReadOnlySpan<char> str, out Complex num)
        {
            if (str.Length == 0)
            {
                num = NaN;
                return false;
            }

            str = str.Trim();
            int signFirst, signSecond;

            if (str.StartsWith("+".AsSpan()))
            {
                signFirst = 1;
                str = str.Slice(1);
            }
            else if (str.StartsWith("-".AsSpan()))
            {
                signFirst = -1;
                str = str.Slice(1);
            }
            else signFirst = 1;

            ReadOnlySpan<char> first, second;
            int splitIndex = str.IndexOf('+');
            if (splitIndex == -1) splitIndex = str.IndexOf('-');

            if (splitIndex != -1)
            {
                first = str;
                second = ReadOnlySpan<char>.Empty;
            }
            else
            {
                first = str.Slice(0, splitIndex);
                second = str.Slice(splitIndex);
            }
            first = first.Trim();
            second = second.Trim();

            if (second.StartsWith("+".AsSpan()))
            {
                signSecond = 1;
                second = second.Slice(1).Trim();
            }
            else if (str.StartsWith("-".AsSpan()))
            {
                signSecond = -1;
                second = second.Slice(1).Trim();
            }
            else signSecond = 1;

            bool firstIsImag;
            if (first.EndsWith("i".AsSpan()))
            {
                firstIsImag = true;
                first = first.Slice(0, first.Length - 1).Trim();
            }
            else if (second.EndsWith("i".AsSpan()))
            {
                firstIsImag = false;
                second = first.Slice(0, second.Length - 1).Trim();
            }
            else
            {
                num = NaN;
                return false;
            }

            double firstNum = ParseHelper.ParseDouble(first) * signFirst,
                   secondNum = ParseHelper.ParseDouble(second) * signSecond;

            if (firstIsImag) num = new Complex(secondNum, firstNum);
            else num = new Complex(firstNum, secondNum);

            return true;
        }

        public static Complex Abs(Complex num) => new Complex(num.Magnitude, 0);
        public static Complex Ceiling(Complex num) =>
            new Complex(MathE.Ceiling(num.r),
                        MathE.Ceiling(num.i));
        public static Complex Clamp(Complex num, Complex min, Complex max) =>
            new Complex(MathE.Clamp(num.r, min.r, max.r),
                        MathE.Clamp(num.i, min.i, max.i));
        public static Complex ClampMagnitude(Complex num, double minMag, double maxMag)
        {
            Complex copy = num;
            ClampMagnitude(ref copy, minMag, maxMag);
            return copy;
        }
        public static void ClampMagnitude(ref Complex num, double minMag, double maxMag)
        {
            if (minMag > maxMag) throw new ClampOrderMismatchException(nameof(minMag), nameof(maxMag));
            double mag = num.Magnitude;

            if (mag < minMag)
            {
                double factor = minMag / mag;
                num.r *= factor;
                num.i *= factor;
            }
            else if (mag > maxMag)
            {
                double factor = maxMag / mag;
                num.r *= factor;
                num.i *= factor;
            }
        }
        public static double Dot(Complex a, Complex b) => a.r * b.r + a.i * b.i;
        public static double Dot(IEnumerable<Complex> values)
        {
            double r = 1, i = 1;
            foreach (Complex val in values)
            {
                r *= val.r;
                i *= val.i;
            }
            return r + i;
        }
        public static Complex Floor(Complex num) =>
            new Complex(MathE.Floor(num.r),
                        MathE.Floor(num.i));
        public static Complex Lerp(Complex a, Complex b, double t, bool clamp = true) =>
            new Complex(MathE.Lerp(a.r, b.r, t, clamp),
                        MathE.Lerp(a.i, b.i, t, clamp));
        public static Complex Product(IEnumerable<Complex> vals)
        {
            bool any = false;
            Complex result = One;
            foreach (Complex val in vals)
            {
                any = true;
                result *= val;
            }
            return any ? result : Zero;
        }
        public static Complex Round(Complex val) =>
            new Complex(MathE.Round(val.r),
                        MathE.Round(val.i));
        public static Complex Sum(IEnumerable<Complex> vals)
        {
            double resultR = 0;
            double resultI = 0;
            foreach (Complex val in vals)
            {
                resultR += val.r;
                resultI += val.i;
            }
            return new Complex(resultR, resultI);
        }

#if CS8_OR_GREATER
        private static bool TryConvertFrom(object? value, out Complex result)
#else
        private static bool TryConvertFrom(object value, out Complex result)
#endif
        {
            if (value is Complex vComplex) result = vComplex;
            else if (value is Fraction vFrac) result = new Complex(vFrac.GetValue(), 0);
            else if (value is Float2 vFloat2) result = new Complex(vFloat2.x, vFloat2.y);
            else if (value is Vector2 vVector2) result = new Complex(vVector2.X, vVector2.Y);
            else if (value is Int2 vInt2) result = new Complex(vInt2.x, vInt2.y);
            else if (value is double vDouble) result = new Complex(vDouble, 0);
            else if (value is float vSingle) result = new Complex(vSingle, 0);
#if NET5_0_OR_GREATER
            else if (value is Half vHalf) result = new Complex((double)vHalf, 0);
#endif
            else if (value is int vInt32) result = new Complex(vInt32, 0);
            else
            {
                result = new Complex(0, 0);
                return false;
            }
            return true;
        }

        public static bool IsEvenInteger(Complex value) => value.i == 0 && value.r % 2 == 0;
        public static bool IsOddInteger(Complex value) => value.i == 0 && value.r % 2 == 1;
        public static bool IsFinite(Complex value) => TargetHelper.IsFinite(value.r) &&
                                                      TargetHelper.IsFinite(value.i);
        public static bool IsInfinity(Complex value) => TargetHelper.IsInfinity(value.r) ||
                                                        TargetHelper.IsInfinity(value.i);
        public static bool IsInteger(Complex value) => value.i == 0 && value.r % 1 == 0;
        public static bool IsNaN(Complex value)
        {
            // NaN never equals itself.
#pragma warning disable CS1718
            return (value.r != value.r) ||
                   (value.i != value.i);
#pragma warning restore CS1718
        }
        public static bool IsNegative(Complex value) => value.r < 0;
        public static bool IsNegativeInfinity(Complex value) => value.r == double.NegativeInfinity ||
                                                                value.i == double.NegativeInfinity;
        public static bool IsNormal(Complex value) => false; // ??? uhh i think this is right
        public static bool IsPositive(Complex value) => value.r > 0;
        public static bool IsPositiveInfinity(Complex value) => value.r == double.PositiveInfinity ||
                                                                value.i == double.PositiveInfinity;
        public static bool IsRealNumber(Complex value) => value.i == 0;
        public static bool IsZero(Complex value) => value.r == 0 && value.i == 0;
        public static Complex MaxMagnitude(Complex a, Complex b) => a.MagnitudeSqr > b.MagnitudeSqr ? a : b;
        public static Complex MinMagnitude(Complex a, Complex b) => a.MagnitudeSqr < b.MagnitudeSqr ? a : b;
#if CS11_OR_GREATER
        static Complex INumberBase<Complex>.MaxMagnitudeNumber(Complex a, Complex b) => MaxMagnitude(a, b);
        static Complex INumberBase<Complex>.MinMagnitudeNumber(Complex a, Complex b) => MinMagnitude(a, b);
        static bool INumberBase<Complex>.IsCanonical(Complex value) => true;
        static bool INumberBase<Complex>.IsComplexNumber(Complex value) => value.i != 0;
        static bool INumberBase<Complex>.IsImaginaryNumber(Complex value) => value.i != 0;
        static bool INumberBase<Complex>.IsSubnormal(Complex value) => false; // What does this mean???

        static Complex INumberBase<Complex>.Parse(string str, NumberStyles numStyles, IFormatProvider? provider) => Parse(str);
        static Complex INumberBase<Complex>.Parse(ReadOnlySpan<char> str, NumberStyles numStyles, IFormatProvider? provider) => Parse(str);
        static Complex IParsable<Complex>.Parse(string str, IFormatProvider? provider) => Parse(str);
        static Complex ISpanParsable<Complex>.Parse(ReadOnlySpan<char> str, IFormatProvider? provider) => Parse(str);
        static bool INumberBase<Complex>.TryParse(string? str, NumberStyles numStyles, IFormatProvider? provider, out Complex num) => TryParse(str, out num);
        static bool INumberBase<Complex>.TryParse(ReadOnlySpan<char> str, NumberStyles numStyles, IFormatProvider? provider, out Complex num) => TryParse(str, out num);
        static bool IParsable<Complex>.TryParse(string? str, IFormatProvider? provider, out Complex num) => TryParse(str, out num);
        static bool ISpanParsable<Complex>.TryParse(ReadOnlySpan<char> str, IFormatProvider? provider, out Complex num) => TryParse(str, out num);

        static Complex IAdditiveIdentity<Complex, Complex>.AdditiveIdentity => Zero;
        static Complex IMultiplicativeIdentity<Complex, Complex>.MultiplicativeIdentity => One;
        static int INumberBase<Complex>.Radix => 2; // Not super sure what to put here.

        private static bool TryConvertTo<T>(Complex num, out T result)
        {
            object? tempValue;

            if (typeof(T) == typeof(Complex)) tempValue = num;
            else if (typeof(T) == typeof(Fraction)) tempValue = Fraction.Approximate(num.r);
            else if (typeof(T) == typeof(Float2)) tempValue = new Float2(num.r, num.i);
            else if (typeof(T) == typeof(Vector2)) tempValue = new Vector2((float)num.r, (float)num.i);
            else if (typeof(T) == typeof(Int2)) tempValue = new Int2((int)num.r, (int)num.i);
            else if (typeof(T) == typeof(double)) tempValue = num.r;
            else if (typeof(T) == typeof(float)) tempValue = (float)num.r;
#if NET5_0_OR_GREATER
            else if (typeof(T) == typeof(Half)) tempValue = (Half)num.r;
#endif
            else if (typeof(T) == typeof(int)) tempValue = (int)num.r;
            else
            {
                result = default!;
                return false;
            }

            result = (T)tempValue;
            return true;
        }
        static bool INumberBase<Complex>.TryConvertFromChecked<TOther>(TOther value, out Complex result) => TryConvertFrom(value, out result);
        static bool INumberBase<Complex>.TryConvertFromSaturating<TOther>(TOther value, out Complex result) => TryConvertFrom(value, out result);
        static bool INumberBase<Complex>.TryConvertFromTruncating<TOther>(TOther value, out Complex result) => TryConvertFrom(value, out result);
        static bool INumberBase<Complex>.TryConvertToChecked<TOther>(Complex value, out TOther result) => TryConvertTo(value, out result);
        static bool INumberBase<Complex>.TryConvertToSaturating<TOther>(Complex value, out TOther result) => TryConvertTo(value, out result);
        static bool INumberBase<Complex>.TryConvertToTruncating<TOther>(Complex value, out TOther result) => TryConvertTo(value, out result);
#endif

        public static (double[] reals, double[] imaginaries) SplitArray(IEnumerable<Complex> vals)
        {
            int count = vals.Count();
            double[] reals = new double[count], imaginaries = new double[count];
            int index = 0;
            foreach (Complex val in vals)
            {
                reals[index] = val.r;
                imaginaries[index] = val.i;
                index++;
            }
            return (reals, imaginaries);
        }

        public IEnumerator<double> GetEnumerator()
        {
            yield return r;
            yield return i;
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public int CompareTo(double other) => MagnitudeSqr.CompareTo(MathE.Abs(other * other));
        public int CompareTo(Complex other) => MagnitudeSqr.CompareTo(other.MagnitudeSqr);
#if CS8_OR_GREATER
        public int CompareTo(object? other)
#else
        public int CompareTo(object other)
#endif
        {
            if (other is null) return 1;
            else if (other is Complex otherComplex) return CompareTo(otherComplex);
            else if (other is double otherDouble) return CompareTo(otherDouble);
            else if (TryConvertFrom(other, out Complex otherConvert)) return CompareTo(otherConvert);
            else return 1;
        }
        public bool Equals(double other) => r == other && i == 0;
        public bool Equals(Complex other) => r == other.r && i == other.i;
#if CS8_OR_GREATER
        public override bool Equals(object? other)
#else
        public override bool Equals(object other)
#endif
        {
            if (other is null) return false;
            else if (other is Complex otherComplex) return Equals(otherComplex);
            else if (TryConvertFrom(other, out Complex otherConvert)) return Equals(otherConvert);
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

        public double[] ToArray() => new double[] { r, i };
        public Fill<double> ToFill()
        {
            Complex @this = this;
            return i => @this[i];
        }
        public List<double> ToList() => new List<double>() { r, i };

#if CS11_OR_GREATER
        public bool TryFormat(Span<char> dest, out int charsWritten, ReadOnlySpan<char> format, IFormatProvider? provider)
        {
            // Not really great, but I don't want to do this right now.
            string result = ToString(format.ToString(), provider);
            result.CopyTo(dest);
            charsWritten = result.Length;
            return true;
        }

        static Complex IIncrementOperators<Complex>.operator ++(Complex a) => new Complex(a.r + 1, a.i);
        static Complex IDecrementOperators<Complex>.operator --(Complex a) => new Complex(a.r - 1, a.i);
#endif

        public static Complex operator +(Complex a) => a;
        public static Complex operator +(Complex a, Complex b) => new Complex(a.r + b.r, a.i + b.i);
        public static Complex operator +(Complex a, double b) => new Complex(a.r + b, a.i);
        public static Complex operator -(Complex a) => new Complex(-a.r, -a.i);
        public static Complex operator -(Complex a, Complex b) => new Complex(a.r - b.r, a.i - b.i);
        public static Complex operator -(Complex a, double b) => new Complex(a.r - b, a.i);
        public static Complex operator *(Complex a, Complex b) => new Complex(a.r * b.r - a.i * b.i, a.r * b.i + a.i * b.r);
        public static Complex operator *(Complex a, double b) => new Complex(a.r * b, a.i * b);
        public static Complex operator /(Complex a, Complex b)
        {
            double scaleFactor = 1 / (b.r * b.r + b.i * b.i);
            return new Complex((a.r * b.r + a.i * b.i) * scaleFactor,
                               (a.i * b.r - a.r * b.i) * scaleFactor);
        }
        public static Complex operator /(Complex a, double b) => new Complex(a.r / b, a.i / b);
        public static Complex operator %(Complex a, Complex b)
        {
            // TODO: Maybe expand and inline this. Don't feel like it at the moment.
            return a + b * Ceiling(-a / b);
        }
        public static Complex operator %(Complex a, double b) => a % new Complex(b, 0);
        public static bool operator ==(Complex a, Complex b) => a.Equals(b);
        public static bool operator !=(Complex a, Complex b) => !a.Equals(b);
        public static bool operator >(Complex a, Complex b) => a.CompareTo(b) > 0;
        public static bool operator <(Complex a, Complex b) => a.CompareTo(b) < 0;
        public static bool operator >=(Complex a, Complex b) => a.CompareTo(b) >= 0;
        public static bool operator <=(Complex a, Complex b) => a.CompareTo(b) <= 0;

        public static implicit operator Complex(System.Numerics.Complex num) => new Complex(num.Real, num.Imaginary);
        public static implicit operator Complex(Float2 group) => new Complex(group.x, group.y);
        public static explicit operator Complex(Int2 group) => new Complex(group.x, group.y);
        public static implicit operator Complex(ListTuple<double> tuple) => new Complex(tuple[0], tuple[1]);
        public static implicit operator Complex(ValueTuple<double, double> tuple) => new Complex(tuple.Item1, tuple.Item2);
        public static explicit operator Complex(Vector2 group) => new Complex(group.X, group.Y);

        public static implicit operator System.Numerics.Complex(Complex num) => new Complex(num.r, num.i);
        public static implicit operator ListTuple<double>(Complex num) => new ListTuple<double>(num.r, num.i);
        public static implicit operator ValueTuple<double, double>(Complex num) => (num.r, num.i);
    }
}
