using Nerd_STF.Abstract;
using Nerd_STF.Helpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Numerics;

namespace Nerd_STF.Mathematics.Numbers
{
    public readonly struct Fraction : IComparable<Fraction>,
                                      IEquatable<Fraction>,
                                      IFormattable
#if CS11_OR_GREATER
                                     ,INumber<Fraction>,
                                      IInterpolable<Fraction>,
                                      IPresets1d<Fraction>,
                                      IRoundable<Fraction>,
                                      ISimpleMathOperations<Fraction>,
                                      ISplittable<Fraction, (int[] nums, int[] dens)>
#endif
    {
        public static Fraction NaN => new Fraction(0, 0);
        public static Fraction NegativeInfinity => new Fraction(-1, 0);
        public static Fraction One => new Fraction(1, 1);
        public static Fraction PositiveInfinity => new Fraction(1, 0);
        public static Fraction Zero => new Fraction(0, 1);

        public int Numerator => num;
        public int Denominator => den;

        public int Whole => num / den;
        public Fraction Partial => new Fraction(num % den, den);

        public Fraction Simplified
        {
            get
            {
                int newNum = num, newDen = den;
                List<int> numFactors = new List<int>(MathE.PrimeFactors(MathE.Abs(num))),
                          denFactors = new List<int>(MathE.PrimeFactors(den));
                foreach (int fac in numFactors)
                {
                    if (!denFactors.Contains(fac)) continue;
                    newNum /= fac;
                    newDen /= fac;
                    denFactors.Remove(fac);
                }
                return new Fraction(newNum, newDen);
            }
        }
        public Fraction Reciprocal => new Fraction(den, num);

        private readonly int num, den;

        public Fraction(int numerator, int denominator)
        {
            num = numerator;
            den = denominator;

            if (den < 0)
            {
                num = -num;
                den = -den;
            }
        }

        public static Fraction Approximate(double number, int iterations = 32)
        {
            int num, den;
            if (number == 0) return Zero;
            else if (number == 1) return One;
            else if (number % 1 == 0) return new Fraction((int)number, 1);
            else if (number < 0)
            {
                Approximate(-number, iterations, out num, out den, out _);
                num = -num;
            }
            else if (number > 1)
            {
                Approximate(number % 1, iterations, out num, out den, out _);
                int whole = (int)number;
                num += whole * den;
            }
            else Approximate(number, iterations, out num, out den, out _);
            return new Fraction(num, den);
        }
        private static void Approximate(double number, int iterations, out int num, out int den, out double error)
        {
            // Forget what this algorithm is called. When I remember, I'll put its
            // Wikipedia page here.

            int minNum = 0, maxNum = 1, newNum = minNum + maxNum,
                minDen = 1, maxDen = 1, newDen = minDen + maxDen;
            double newVal = (double)newNum / newDen;
            for (int i = 0; i < iterations; i++)
            {
                if (number == newVal) break;
                else if (number > newVal)
                {
                    minNum = newNum;
                    minDen = newDen;
                }
                else // if (number < newVal)
                {
                    maxNum = newNum;
                    maxDen = newDen;
                }
                newNum = minNum + maxNum;
                newDen = minDen + maxDen;
                newVal = (double)newNum / newDen;
            }
            num = newNum;
            den = newDen;
            error = MathE.Abs(newVal - number);
        }

        public static IEnumerable<Fraction> Egyptian(double number, int maxTerms) =>
            Egyptian(Approximate(number, 256), maxTerms);
        public static Fraction[] Egyptian(Fraction number, int maxTerms)
        {
            List<Fraction> parts = new List<Fraction>();
            int terms = 0;
            foreach (Fraction part in EgyptianE(number))
            {
                parts.Add(part);
                terms++;
                if (terms >= maxTerms) break;
            }
            return parts.ToArray();
        }
        public static IEnumerable<Fraction> EgyptianE(double number) =>
            EgyptianE(Approximate(number, 256));
        public static IEnumerable<Fraction> EgyptianE(Fraction number)
        {
            int wholes = number.Whole;
            if (wholes > 0) yield return new Fraction(wholes, 1);

            number = number.Partial;
            int newDen = 2, curNum = number.num, curDen = number.den;
            while (curNum > 0 && newDen <= curDen)
            {
                if (curNum * newDen >= curDen)
                {
                    yield return new Fraction(1, newDen);
                    curNum = curNum * newDen - curDen;
                    curDen *= newDen;
                }
                else newDen++;
            }
        }

#if CS8_OR_GREATER
        public static Fraction Parse(string? str) =>
#else
        public static Fraction Parse(string str) =>
#endif
            str is null ? NaN : Parse(str.AsSpan());
        public static Fraction Parse(ReadOnlySpan<char> str)
        {
            if (str.Length == 0) return NaN;

            ReadOnlySpan<char> numSpan, denSpan;
            str = str.Trim();
            char first = str[0];
            if (first == '\\')
            {
                // TeX format.
                if (str.Length >= 5 && str.StartsWith("\\frac".AsSpan())) str = str.Slice(5);
                else if (str.Length >= 6 && str.Slice(2, 4).Equals("frac".AsSpan(), StringComparison.Ordinal)) str = str.Slice(6); // Allows for \sfrac or things like that.
                else goto _fail;

                if (!str.StartsWith("{".AsSpan()) || !str.EndsWith("}".AsSpan())) goto _fail;
                int separator = str.IndexOf(',');
                if (separator == -1 || separator == 1 || separator == str.Length - 2) goto _fail;

                numSpan = str.Slice(1, separator - 1);
                denSpan = str.Slice(separator + 1, str.Length - separator - 2);
            }
            else
            {
                // Standard fraction format.
                char[] allowedSeparators = new char[] { '/', ':' };
                int separator = -1;
                foreach (char c in allowedSeparators)
                {
                    int newSep = str.IndexOf(c);
                    if (newSep == -1) continue;
                    else
                    {
                        if (separator != -1) goto _fail; // More than one separator.
                        else separator = newSep;
                    }
                }
                if (separator == 0 || separator == str.Length - 1) goto _fail;

                numSpan = str.Slice(0, separator);
                denSpan = str.Slice(separator + 1, str.Length - separator - 1);
            }

            int top = ParseHelper.ParseDoubleWholeDecimals(numSpan, out int topPlaces),
                bot = ParseHelper.ParseDoubleWholeDecimals(denSpan, out int botPlaces);
            int topDen = 1, botDen = 1;
            for (int i = 0; i < topPlaces; i++) topDen *= 10;
            for (int i = 0; i < botPlaces; i++) botDen *= 10;

            Fraction topF = new Fraction(top, topDen), botF = new Fraction(bot, botDen);
            return topF / botF;

        _fail:
            throw new FormatException("Cannot parse fraction from span.");
        }
#if CS8_OR_GREATER
        public static bool TryParse(string? str, out Fraction frac) =>
#else
        public static bool TryParse(string str, out Fraction frac) =>
#endif
            TryParse(str.AsSpan(), out frac);
        public static bool TryParse(ReadOnlySpan<char> str, out Fraction frac)
        {
            try
            {
                frac = Parse(str);
                return true;
            }
            catch
            {
                frac = NaN;
                return false;
            }
        }

        public static Fraction Abs(Fraction val) => new Fraction(MathE.Abs(val.num), val.den);
        public static Fraction Ceiling(Fraction val)
        {
            int newNum = val.num;
            if (val.num % val.den != 0)
            {
                if (val.num > 0) newNum += val.den - (val.num % val.den);
                else newNum -= val.num % val.den;
            }
            return new Fraction(newNum, val.den);
        }
        public static Fraction Clamp(Fraction val, Fraction min, Fraction max)
        {
            int lcm = MathE.Lcm(val.den, min.den, max.den);
            int valFac = val.den / lcm, minFac = min.den / lcm, maxFac = max.den / lcm;
            int trueVal = val.num * valFac, trueMin = min.num * minFac, trueMax = max.num * maxFac;
            if (trueVal > trueMax) return max;
            else if (trueVal < trueMin) return min;
            else return val;
        }
        public static Fraction Floor(Fraction val)
        {
            int newNum = val.num;
            if (val.num % val.den != 0)
            {
                if (val.num > 0) newNum -= val.num % val.den;
                else newNum -= val.den + (val.num % val.den);
            }
            return new Fraction(newNum, val.den);
        }
        public static Fraction Lerp(Fraction a, Fraction b, double t, bool clamp = true, bool fast = false)
        {
            if (fast)
            {
                int aNum = a.num * b.den, bNum = b.num * a.den, cDen = a.den * b.den;
                int cNum = (int)(aNum + t * (bNum - aNum));
                return new Fraction(cNum, cDen);
            }
            else return Approximate(MathE.Lerp(a, b, t, clamp), 128);
        }
#if CS11_OR_GREATER
        static Fraction IInterpolable<Fraction>.Lerp(Fraction a, Fraction b, double t, bool clamp) =>
            Lerp(a, b, t, clamp, false);
#endif
        public static Fraction Product(IEnumerable<Fraction> vals)
        {
            bool any = false;
            int resultNum = 1, resultDen = 1;
            foreach (Fraction frac in vals)
            {
                any = true;
                resultNum *= frac.num;
                resultDen *= frac.den;
            }
            return any ? new Fraction(resultNum, resultDen) : Zero;
        }
        public static Fraction Round(Fraction val)
        {
            int half = val.den / 2;
            int newNum = val.num;
            if (val.num > 0)
            {
                if (val.num % val.den > half) newNum += val.den - (val.num % val.den);
                else newNum -= val.num % val.den;
            }
            else
            {
                if (-val.num % val.den > half) newNum -= val.den + (val.num % val.den);
                else newNum -= val.num % val.den;
            }
            return new Fraction(newNum, val.den);
        }
        public static Fraction Sum(IEnumerable<Fraction> vals)
        {
            bool any = false;
            Fraction result = Zero;
            foreach (Fraction frac in vals)
            {
                any = true;
                result += frac;
            }
            return any ? result : NaN;
        }

#if CS8_OR_GREATER
        private static bool TryConvertFrom(object? value, out Fraction result)
#else
        private static bool TryConvertFrom(object value, out Fraction result)
#endif
        {
            if (value is null)
            {
                result = NaN;
                return false;
            }
            else if (value is Fraction valueFrac) result = valueFrac;
            else if (value is double valueDouble) result = Approximate(valueDouble);
            else if (value is float valueSingle) result = Approximate(valueSingle);
#if NET5_0_OR_GREATER
            else if (value is Half valueHalf) result = Approximate((double)valueHalf);
#endif
#if NET7_0_OR_GREATER
            else if (value is UInt128 valueUInt128) result = new Fraction((int)valueUInt128, 1);
            else if (value is Int128 valueInt128) result = new Fraction((int)valueInt128, 1);
#endif
            else if (value is ulong valueUInt64) result = new Fraction((int)valueUInt64, 1);
            else if (value is long valueInt64) result = new Fraction((int)valueInt64, 1);
            else if (value is uint valueUInt32) result = new Fraction((int)valueUInt32, 1);
            else if (value is int valueInt32) result = new Fraction(valueInt32, 1);
            else if (value is ushort valueUInt16) result = new Fraction(valueUInt16, 1);
            else if (value is short valueInt16) result = new Fraction(valueInt16, 1);
            else if (value is byte valueUInt8) result = new Fraction(valueUInt8, 1);
            else if (value is sbyte valueInt8) result = new Fraction(valueInt8, 1);
            else if (value is IntPtr valueInt) result = new Fraction((int)valueInt, 1);
            else if (value is UIntPtr valueUInt) result = new Fraction((int)valueUInt, 1);
            else
            {
                result = NaN;
                return false;
            }
            return true;
        }

        public static bool IsCanonical(Fraction val)
        {
            IEnumerable<int> factorsNum = MathE.PrimeFactorsE(MathE.Abs(val.num)),
                             factorsDen = MathE.PrimeFactorsE(val.den),
                             shared = factorsNum.Where(x => factorsDen.Contains(x));
            return shared.Any();
        }
        public static bool IsEvenInteger(Fraction val) =>
            val.num % val.den == 0 && val.num / val.den % 2 == 0;
        public static bool IsFinite(Fraction val) => val.den != 0 || val.num != 0;
        public static bool IsInfinity(Fraction val) => val.den == 0 && val.num != 0;
        public static bool IsInteger(Fraction val) => val.num % val.den == 0;
        public static bool IsNaN(Fraction val) => val.num == 0 && val.den == 0;
        public static bool IsNegative(Fraction val) => val.num < 0;
        public static bool IsNegativeInfinity(Fraction val) => val.den == 0 && val.num < 0;
        public static bool IsNormal(Fraction val) => val.den != 0 && val.num != 0;
        public static bool IsOddInteger(Fraction val) =>
            val.num % val.den == 0 && val.num / val.den % 2 == 1;
        public static bool IsPositive(Fraction val) => val.num > 0;
        public static bool IsPositiveInfinity(Fraction val) => val.den == 0 && val.num > 0;
        public static bool IsRealNumber(Fraction val) => val.den != 0;
        public static bool IsZero(Fraction val) => val.num == 0 && val.den != 0;
        public static Fraction MaxMagnitude(Fraction a, Fraction b) => a > b ? a : b;
        public static Fraction MaxMagnitudeNumber(Fraction a, Fraction b) => a > b ? a : b;
        public static Fraction MinMagnitude(Fraction a, Fraction b) => a < b ? a : b;
        public static Fraction MinMagnitudeNumber(Fraction a, Fraction b) => a < b ? a : b;
#if CS11_OR_GREATER
        static bool INumberBase<Fraction>.IsComplexNumber(Fraction val) => false;
        static bool INumberBase<Fraction>.IsImaginaryNumber(Fraction val) => false;
        static bool INumberBase<Fraction>.IsSubnormal(Fraction val) => false; // What does this mean???
        static Fraction INumberBase<Fraction>.Parse(string? str, NumberStyles style, IFormatProvider? provider) => Parse(str);
        static Fraction INumberBase<Fraction>.Parse(ReadOnlySpan<char> str, NumberStyles style, IFormatProvider? provider) => Parse(str);
        static bool INumberBase<Fraction>.TryParse(string? str, NumberStyles style, IFormatProvider? provider, out Fraction frac) => TryParse(str, out frac);
        static bool INumberBase<Fraction>.TryParse(ReadOnlySpan<char> str, NumberStyles style, IFormatProvider? provider, out Fraction frac) => TryParse(str, out frac);
        static Fraction IParsable<Fraction>.Parse(string? str, IFormatProvider? provider) => Parse(str);
        static bool IParsable<Fraction>.TryParse(string? str, IFormatProvider? provider, out Fraction frac) => TryParse(str, out frac);
        static Fraction ISpanParsable<Fraction>.Parse(ReadOnlySpan<char> str, IFormatProvider? provider) => Parse(str);
        static bool ISpanParsable<Fraction>.TryParse(ReadOnlySpan<char> str, IFormatProvider? provider, out Fraction frac) => TryParse(str, out frac);
        static Fraction IAdditiveIdentity<Fraction, Fraction>.AdditiveIdentity => Zero;
        static Fraction IMultiplicativeIdentity<Fraction, Fraction>.MultiplicativeIdentity => One;
        static int INumberBase<Fraction>.Radix => 2; // Not super sure what to put here.

        private static bool TryConvertTo<T>(Fraction frac, out T value)
        {
            object? tempValue;

            if (typeof(T) == typeof(Fraction)) tempValue = frac;
            else if (typeof(T) == typeof(double)) tempValue = frac.GetValue();
            else if (typeof(T) == typeof(float)) tempValue = (float)frac.GetValue();
#if NET5_0_OR_GREATER
            else if (typeof(T) == typeof(Half)) tempValue = (Half)frac.GetValue();
#endif
#if NET7_0_OR_GREATER
            else if (typeof(T) == typeof(UInt128)) tempValue = (UInt128)frac.GetValue();
            else if (typeof(T) == typeof(Int128)) tempValue = (Int128)frac.GetValue();
#endif
            else if (typeof(T) == typeof(ulong)) tempValue = (ulong)frac.GetValue();
            else if (typeof(T) == typeof(long)) tempValue = (long)frac.GetValue();
            else if (typeof(T) == typeof(uint)) tempValue = (uint)frac.GetValue();
            else if (typeof(T) == typeof(int)) tempValue = (int)frac.GetValue();
            else if (typeof(T) == typeof(ushort)) tempValue = (ushort)frac.GetValue();
            else if (typeof(T) == typeof(short)) tempValue = (short)frac.GetValue();
            else if (typeof(T) == typeof(byte)) tempValue = (byte)frac.GetValue();
            else if (typeof(T) == typeof(sbyte)) tempValue = (sbyte)frac.GetValue();
            else
            {
                value = default!;
                return false;
            }

            value = (T)tempValue;
            return true;
        }
        static bool INumberBase<Fraction>.TryConvertFromChecked<TOther>(TOther value, out Fraction result) => TryConvertFrom(value, out result);
        static bool INumberBase<Fraction>.TryConvertFromSaturating<TOther>(TOther value, out Fraction result) => TryConvertFrom(value, out result);
        static bool INumberBase<Fraction>.TryConvertFromTruncating<TOther>(TOther value, out Fraction result) => TryConvertFrom(value, out result);
        static bool INumberBase<Fraction>.TryConvertToChecked<TOther>(Fraction value, out TOther result) => TryConvertTo(value, out result);
        static bool INumberBase<Fraction>.TryConvertToSaturating<TOther>(Fraction value, out TOther result) => TryConvertTo(value, out result);
        static bool INumberBase<Fraction>.TryConvertToTruncating<TOther>(Fraction value, out TOther result) => TryConvertTo(value, out result);
#endif

        public static (int[] nums, int[] dens) SplitArray(IEnumerable<Fraction> vals)
        {
            int count = vals.Count();
            int[] nums = new int[count], dens = new int[count];
            int index = 0;
            foreach (Fraction val in vals)
            {
                nums[index] = val.num;
                dens[index] = val.den;
            }
            return (nums, dens);
        }

        public double GetValue() => (double)num / den;

#if CS8_OR_GREATER
        public int CompareTo(object? other)
#else
        public int CompareTo(object other)
#endif
        {
            if (other is null) return 1;
            else if (other is Fraction otherFrac) return CompareTo(otherFrac);
            else if (TryConvertFrom(other, out Fraction otherConvert)) return CompareTo(otherConvert);
            else return -1;
        }
        public int CompareTo(Fraction other) => (num * other.den).CompareTo(other.num * den);
        public bool Equals(Fraction other) => num * other.den == other.num * den;
#if CS8_OR_GREATER
        public override bool Equals(object? other)
#else
        public override bool Equals(object other)
#endif
        {
            if (other is null) return false;
            else if (other is Fraction otherFrac) return Equals(otherFrac);
            else if (TryConvertFrom(other, out Fraction otherConvert)) return Equals(otherConvert);
            else return false;
        }
        public override int GetHashCode() =>
            (int)(((uint)num.GetHashCode() & 0xFFFF0000u) | ((uint)den.GetHashCode() & 0x0000FFFFu));
#if CS8_OR_GREATER
        public override string ToString() => ToString(null, null);
        public string ToString(string? format) => ToString(format, null);
        public string ToString(IFormatProvider? provider) => ToString(null, provider);
        public string ToString(string? format, IFormatProvider? provider) => $"{num.ToString(format)}/{den.ToString(format)}";
#else
        public override string ToString() => ToString(null, null);
        public string ToString(string format) => ToString(format, null);
        public string ToString(IFormatProvider provider) => ToString(null, provider);
        public string ToString(string format, IFormatProvider provider) => $"{num.ToString(format)}/{den.ToString(format)}";
#endif

#if CS11_OR_GREATER
        public bool TryFormat(Span<char> dest, out int charsWritten, ReadOnlySpan<char> format, IFormatProvider? provider)
        {
            // Not really great, but I don't want to do this right now.
            string result = ToString(format.ToString(), provider);
            result.CopyTo(dest);
            charsWritten = result.Length;
            return true;
        }
#endif

        public static Fraction operator +(Fraction a) => a;
        public static Fraction operator +(Fraction a, Fraction b)
        {
            int lcm = MathE.Lcm(a.den, b.den);
            int facA = lcm / a.den, facB = lcm / b.den;
            return new Fraction(a.num * facA + b.num * facB, lcm);
        }
        public static Fraction operator +(Fraction a, int b) => new Fraction(a.num + b * a.den, a.den);
        public static Fraction operator +(Fraction a, double b) => a * Approximate(b);
        public static Fraction operator ++(Fraction a) => new Fraction(a.num + a.den, a.den);
        public static Fraction operator -(Fraction a) => new Fraction(-a.num, a.den);
        public static Fraction operator -(Fraction a, Fraction b)
        {
            int lcm = MathE.Lcm(a.den, b.den);
            int facA = lcm / a.den, facB = lcm / b.den;
            return new Fraction(a.num * facA - b.num * facB, lcm);
        }
        public static Fraction operator -(Fraction a, int b) => new Fraction(a.num - b * a.den, a.den);
        public static Fraction operator -(Fraction a, double b) => a * Approximate(b);
        public static Fraction operator --(Fraction a) => new Fraction(a.num - a.den, a.den);
        public static Fraction operator *(Fraction a, Fraction b) => new Fraction(a.num * b.num, a.den * b.den);
        public static Fraction operator *(Fraction a, int b) => new Fraction(a.num * b, a.den);
        public static Fraction operator *(Fraction a, double b) => a * Approximate(b);
        public static Fraction operator /(Fraction a, Fraction b) => new Fraction(a.num * b.den, a.den * b.num);
        public static Fraction operator /(Fraction a, int b) => new Fraction(a.num, a.den * b);
        public static Fraction operator /(Fraction a, double b) => a / Approximate(b);
        public static Fraction operator %(Fraction a, Fraction b)
        {
            // c = a / b
            // f = b * mod(c, 1)
            int cNum = a.num * b.den, cDen = a.den * b.num;
            if (cDen < 0)
            {
                cNum = -cNum;
                cDen = -cDen;
            }
            cNum = MathE.ModAbs(cNum, cDen); // Fractional portion.
            return new Fraction(b.num * cNum, b.den * cDen);
        }
        public static Fraction operator %(Fraction a, int b)
        {
            // c = a / b
            // f = b * mod(c, 1)
            int cNum = a.num, cDen = a.den * b;
            if (cDen < 0)
            {
                cNum = -cNum;
                cDen = -cDen;
            }
            cNum = MathE.ModAbs(cNum, cDen); // Fractional portion.
            return new Fraction(b * cNum, cDen);
        }
        public static Fraction operator %(Fraction a, double b) => a % Approximate(b);
        public static Fraction operator ^(Fraction a, Fraction b) => new Fraction(a.num + b.num, a.den + b.den);
        public static Fraction operator ~(Fraction a) => a.Reciprocal;
        public static bool operator ==(Fraction a, Fraction b) => a.Equals(b);
        public static bool operator !=(Fraction a, Fraction b) => !a.Equals(b);
        public static bool operator >(Fraction a, Fraction b) => a.CompareTo(b) > 0;
        public static bool operator <(Fraction a, Fraction b) => a.CompareTo(b) < 0;
        public static bool operator >=(Fraction a, Fraction b) => a.CompareTo(b) >= 0;
        public static bool operator <=(Fraction a, Fraction b) => a.CompareTo(b) <= 0;
        // TODO: Comparisons with a double on the right (maybe).

        public static implicit operator double(Fraction frac) => frac.GetValue();
        public static explicit operator Fraction(double num) => Approximate(num);
    }
}
