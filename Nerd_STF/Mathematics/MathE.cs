using Nerd_STF.Exceptions;
using Nerd_STF.Helpers;
using Nerd_STF.Mathematics.Equations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace Nerd_STF.Mathematics
{
    public static class MathE
    {
        public static int Abs(int value) => value < 0 ? -value : value;
        public static double Abs(double value) => value < 0 ? -value : value;
#if CS11_OR_GREATER
        public static T Abs<T>(T num) where T : INumber<T>
        {
            return num < T.Zero ? -num : num;
        }
#endif

        public static int Average(IEnumerable<int> values)
        {
            int sum = 0;
            int count = 0;
            foreach (int val in values)
            {
                sum += val;
                count++;
            }
            return sum / count;
        }
        public static double Average(IEnumerable<double> values)
        {
            double sum = 0;
            int count = 0;
            foreach (double val in values)
            {
                sum += val;
                count++;
            }
            return sum / count;
        }
#if CS11_OR_GREATER
        public static T Average<T>(IEnumerable<T> values) where T : INumber<T>
        {
            T sum = T.Zero;
            int count = 0;
            foreach (T val in values)
            {
                sum += val;
                count++;
            }
            return sum / T.CreateChecked(count);
        }
#endif
        public static double Average(IEquation equ, double lowerBound, double upperBound, double epsilon = 1e-3)
        {
            double sum = 0;
            double steps = Abs(upperBound - lowerBound) / epsilon;
            for (double x = lowerBound; x <= upperBound; x += epsilon) sum += equ[x];
            return sum / steps;
        }

        public static int Ceiling(double value)
        {
            if (value % 1 == 0 || value < 0) return (int)value;
            else return (int)value + 1;
        }
        public static void Ceiling(ref double value)
        {
            if (value % 1 != 0)
            {
                if (value > 0) value += 1 - value % 1;
                else value -= value % 1;
            }
        }
        public static IEquation Ceiling(IEquation equ) =>
            new Equation((double x) => Ceiling(equ.Get(x)));

        public static double Clamp(double value, double min, double max)
        {
            if (min > max) throw new ClampOrderMismatchException(nameof(min), nameof(max));
            if (value < min) value = min;
            if (value > max) value = max;
            return value;
        }
        public static int Clamp(int value, int min, int max)
        {
            if (min > max) throw new ClampOrderMismatchException(nameof(min), nameof(max));
            if (value < min) value = min;
            if (value > max) value = max;
            return value;
        }
        public static void Clamp(ref double value, double min, double max)
        {
            if (min > max) throw new ClampOrderMismatchException(nameof(min), nameof(max));
            if (value < min) value = min;
            if (value > max) value = max;
        }
        public static void Clamp(ref int value, int min, int max)
        {
            if (min > max) throw new ClampOrderMismatchException(nameof(min), nameof(max));
            if (value < min) value = min;
            if (value > max) value = max;
        }
        public static IEquation Clamp(IEquation equ, double min, double max) =>
            new Equation((double x) => Clamp(equ.Get(x), min, max));
        public static IEquation Clamp(IEquation value, IEquation min, IEquation max) =>
            new Equation((double x) => Clamp(value.Get(x), min.Get(x), max.Get(x)));
#if CS11_OR_GREATER
        public static T Clamp<T>(T value, T min, T max)
            where T : INumber<T>
        {
            if (min > max) throw new ClampOrderMismatchException(nameof(min), nameof(max));
            if (value < min) value = min;
            if (value > max) value = max;
            return value;
        }
        public static void Clamp<T>(ref T value, T min, T max)
            where T : INumber<T>
        {
            if (min > max) throw new ClampOrderMismatchException(nameof(min), nameof(max));
            if (value < min) value = min;
            if (value > max) value = max;
        }
#endif

        public static int Dot(IEnumerable<int> a, IEnumerable<int> b)
        {
            List<int> parts = new List<int>(a);
            int index = 0;
            foreach (int bPart in b)
            {
                if (index < parts.Count) parts[index] *= bPart;
                else parts.Add(bPart);
                index++;
            }
            return Sum(parts);
        }
        public static double Dot(IEnumerable<double> a, IEnumerable<double> b)
        {
            List<double> parts = new List<double>(a);
            int index = 0;
            foreach (double bPart in b)
            {
                if (index < parts.Count) parts[index] *= bPart;
                else parts.Add(bPart);
                index++;
            }
            return Sum(parts);
        }
        public static int Dot(IEnumerable<IEnumerable<int>> groups)
        {
            List<int> parts = new List<int>();
            foreach (IEnumerable<int> group in groups)
            {
                int index = 0;
                foreach (int gPart in group)
                {
                    if (index < parts.Count) parts[index] *= gPart;
                    else parts.Add(gPart);
                    index++;
                }
            }
            return Sum(parts);
        }
        public static double Dot(IEnumerable<IEnumerable<double>> groups)
        {
            List<double> parts = new List<double>();
            foreach (IEnumerable<double> group in groups)
            {
                int index = 0;
                foreach (double gPart in group)
                {
                    if (index < parts.Count) parts[index] *= gPart;
                    else parts.Add(gPart);
                    index++;
                }
            }
            return Sum(parts);
        }
#if CS11_OR_GREATER
        public static T Dot<T>(IEnumerable<T> a, IEnumerable<T> b) where T : INumber<T>
        {
            List<T> parts = new List<T>(a);
            int index = 0;
            foreach (T bPart in b)
            {
                if (index < parts.Count) parts[index] *= bPart;
                else parts.Add(bPart);
                index++;
            }
            return Sum(parts);
        }
        public static T Dot<T>(IEnumerable<IEnumerable<T>> groups)
            where T : INumber<T>
        {
            List<T> parts = new List<T>();
            foreach (IEnumerable<T> group in groups)
            {
                int index = 0;
                foreach (T gPart in group)
                {
                    if (index < parts.Count) parts[index] *= gPart;
                    else parts.Add(gPart);
                    index++;
                }
            }
            return Sum(parts);
        }
#endif

        public static IEquation DynamicIntegral(IEquation equ, IEquation lower, IEquation upper) =>
            new Equation((double x) => equ.Integrate(lower[x], upper[x]));

        public static double EulersMethod(IEquation equ, double refX, double deltaX) =>
            equ.Derive()[refX] * deltaX + equ[refX];

        // TODO: Gamma function at some point.
        public static BigInteger FactorialBig(int num)
        {
            if (num < 0) return 0;
            BigInteger result = 1;
            for (int i = 2; i <= num; i++) result *= i;
            return result;
        }
        public static BigInteger FactorialBig(int fromInclusive, int toInclusive)
        {
            if (fromInclusive > toInclusive) throw new ClampOrderMismatchException();
            BigInteger result = 1;
            for (int i =  fromInclusive; i <= toInclusive; i++) result *= i;
            return result;
        }
        public static ulong Factorial(int num)
        {
            if (num < 0) return 0;
            else if (num > 20) throw new ArgumentOutOfRangeException(nameof(num), "Resulting number is too big to fit in a ulong.");

            ulong result = 1, ulNum = (ulong)num;
            for (ulong i = 2; i <= ulNum; i++) result *= i;
            return result;
        }
        public static ulong Factorial(int fromInclusive, int toInclusive)
        {
            if (fromInclusive > toInclusive) throw new ClampOrderMismatchException();

            ulong result = 1, ulMin = (ulong)fromInclusive, ulMax = (ulong)toInclusive;
            for (ulong i = ulMin; i <= ulMax; i++) result *= i;
            return result;
        }
#if CS11_OR_GREATER
        public static T Factorial<T>(T num) where T : INumber<T>
        {
            if (num < T.Zero) return T.Zero;
            T result = T.One;
            for (T i = T.One; i <= num; i++) result *= i;
            return result;
        }
        public static T Factorial<T>(T fromInclusive, T toInclusive) where T : INumber<T>
        {
            if (fromInclusive > toInclusive) throw new ClampOrderMismatchException();
            T result = T.One;
            for (T i = fromInclusive; i <= toInclusive; i++) result *= i;
            return result;
        }
#endif

        public static int[] Factors(int num) => FactorsE(num).ToArray();
        public static IEnumerable<int> FactorsE(int num)
        {
            for (int i = 1; i <= num; i++) if (num % i == 0) yield return i;
        }

        public static unsafe double FastExp2(int pow)
        {
            // 11-bit exponent, 52-bit mantissa.
            ulong bits = (ulong)pow;
            if (pow > 0) bits = ((bits - 1) & 0b01111111111 | 0b10000000000) << 52;
            else bits = ((bits - 1) & 0b01111111111) << 52;
            return *(double*)&bits;
        }
        public static unsafe float FastExp2f(int pow)
        {
            // 8-bit exponent, 23-bit mantissa
            uint bits = (uint)pow;
            if (pow > 0) bits = ((bits - 1) & 0b01111111 | 0b10000000) << 23;
            else bits = ((bits - 1) & 0b01111111) << 23;
            return *(float*)&bits;
        }

        public static int Floor(double value)
        {
            if (value % 1 == 0 || value > 0) return (int)value;
            else return (int)value - 1;
        }
        public static void Floor(ref double value)
        {
            if (value % 1 != 0)
            {
                if (value > 0) value -= value % 1;
                else value -= 1 + value % 1;
            }
        }
        public static IEquation Floor(IEquation equ) =>
            new Equation((double x) => Floor(equ.Get(x)));

        public static int Gcf(int a, int b)
        {
            for (int i = Min(a, b); i >= 1; i--)
            {
                if (a % i == 0 &&
                    b % i == 0) return i;
            }
            return -1;
        }
        public static int Gcf(int a, int b, int c)
        {
            for (int i = Min(a, b); i >= 1; i--)
            {
                if (a % i == 0 &&
                    b % i == 0 &&
                    c % i == 0) return i;
            }
            return -1;
        }
        public static int Gcf(IEnumerable<int> nums)
        {
            for (int i = Min(nums); i >= 1; i--)
            {
                bool valid = true;
                foreach (int check in nums)
                {
                    if (check % i != 0)
                    {
                        valid = false;
                        break;
                    }
                }
                if (valid) return i;
            }
            return -1; // Will only get here if there are negative numbers in the collection.
        }

        public static double InverseSqrt(double num) => 1 / Sqrt(num);
        public static unsafe float InverseSqrtFast(float num)
        {
            // I think we all know this function. Code structure
            // has changed (ported), but the idea is exactly the
            // same.

            int raw = *(int*)&num;
            float half = num * 0.5f;
            raw = 0x5F3759DF - (raw >> 1);
            num = *(float*)&raw;

            num *= 1.5f - (half * num * num); // Newton's method.
            return num;
        }

        public static int Lcm(int a, int b) => a * b / Gcf(a, b);
        public static int Lcm(int a, int b, int c) => a * b * c / Gcf(a, b, c);
        public static int Lcm(IEnumerable<int> nums) => Product(nums) / Gcf(nums);

        public static double Lerp(double a, double b, double t, bool clamp = true)
        {
            if (clamp) Clamp(ref t, 0, 1);
            return a + t * (b - a);
        }
        public static int Lerp(int a, int b, double t, bool clamp = true)
        {
            if (clamp) Clamp(ref t, 0, 1);
            return (int)(a + t * (b - a));
        }
        public static IEquation Lerp(IEquation a, IEquation b, double t, bool clamp = true) =>
            new Equation((double x) => Lerp(a.Get(x), b.Get(x), t, clamp));
        public static Fill<double> Lerp(Fill<double> a, Fill<double> b, double t, bool clamp = true)
        {
            if (clamp) Clamp(ref t, 0, 1);
            return delegate (int index)
            {
                double aVal = a(index);
                return aVal + t * (b(index) - aVal);
            };
        }
        public static Fill<int> Lerp(Fill<int> a, Fill<int> b, double t, bool clamp = true)
        {
            if (clamp) Clamp(ref t, 0, 1);
            return delegate (int index)
            {
                int aVal = a(index);
                return (int)(aVal + t * (b(index) - aVal));
            };
        }
#if CS11_OR_GREATER
        public static T Lerp<T>(T a, T b, T t, bool clamp = true)
            where T : INumber<T>
        {
            if (clamp) Clamp(ref t, T.Zero, T.One);
            return a + t * (b - a);
        }
        public static T Lerp<T>(T a, T b, double t, bool clamp = true)
            where T : IInterpolable<T> => T.Lerp(a, b, t, clamp);
        public static Fill<T> Lerp<T>(Fill<T> a, Fill<T> b, T t, bool clamp = true)
            where T : INumber<T>
        {
            if (clamp) Clamp(ref t, T.Zero, T.One);
            return delegate (int index)
            {
                T aVal = a(index);
                return aVal + t * (b(index) - aVal);
            };
        }
        public static Fill<T> Lerp<T>(Fill<T> a, Fill<T> b, double t, bool clamp = true)
            where T : IInterpolable<T> => (int index) => T.Lerp(a(index), b(index), t, clamp);
#endif

        public static int Max(int a, int b) => a > b ? a : b;
        public static double Max(double a, double b) => a > b ? a : b;
        public static T Max<T>(T a, T b) where T : IComparable<T>
        {
            if (a.CompareTo(b) > 0) return a;
            else return b;
        }
        public static int Max(IEnumerable<int> values)
        {
            bool any = false;
            int best = 0;
            foreach (int val in values)
            {
                if (!any)
                {
                    any = true;
                    best = val;
                }
                else if (val > best) best = val;
            }
            return best;
        }
        public static double Max(IEnumerable<double> values)
        {
            bool any = false;
            double best = 0;
            foreach (double val in values)
            {
                if (!any)
                {
                    any = true;
                    best = val;
                }
                else if (val > best) best = val;
            }
            return best;
        }
        public static T Max<T>(IEnumerable<T> values) where T : IComparable<T>
        {
            bool any = false;
            T best = values.First();
            foreach (T val in values)
            {
                if (!any)
                {
                    any = true;
                    best = val;
                }
                else if (val.CompareTo(best) > 0) best = val;
            }
            return best;
        }

        public static int Min(int a, int b) => a < b ? a : b;
        public static double Min(double a, double b) => a < b ? a : b;
        public static T Min<T>(T a, T b) where T : IComparable<T>
        {
            if (a.CompareTo(b) < 0) return a;
            else return b;
        }
        public static int Min(IEnumerable<int> values)
        {
            bool any = false;
            int best = 0;
            foreach (int val in values)
            {
                if (!any)
                {
                    any = true;
                    best = val;
                }
                else if (val < best) best = val;
            }
            return best;
        }
        public static double Min(IEnumerable<double> values)
        {
            bool any = false;
            double best = 0;
            foreach (double val in values)
            {
                if (!any)
                {
                    any = true;
                    best = val;
                }
                else if (val < best) best = val;
            }
            return best;
        }
        public static T Min<T>(IEnumerable<T> values) where T : IComparable<T>
        {
            bool any = false;
            T best = values.First();
            foreach (T val in values)
            {
                if (!any)
                {
                    any = true;
                    best = val;
                }
                else if (val.CompareTo(best) < 0) best = val;
            }
            return best;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ModAbs(int value, int mod) => (value % mod + mod) % mod;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double ModAbs(double value, double mod) => (value % mod + mod) % mod;
#if CS11_OR_GREATER
        public static T ModAbs<T>(T value, T mod) where T : INumber<T> => (value % mod + mod) % mod;
#endif

        public static BigInteger NprBig(int n, int r) => FactorialBig(n - r + 1, n);
        public static int Npr(int n, int r) => (int)Factorial(n - r + 1, n);

        public static int Pow(int @base, int pow)
        {
            if (pow < 0) return 1 / Pow(@base, -pow);
            int result = 1;
            for (int i = 1; i <= pow; i++) result *= @base;
            return result;
        }
        public static double Pow(double @base, int pow)
        {
            if (pow < 0) return 1 / Pow(@base, -pow);
            double result = 1;
            for (int i = 1; i <= pow; i++) result *= @base;
            return result;
        }
        public static double Pow(double @base, double pow) =>
            CordicHelper.PowAnyBase(@base, pow, 8);

        public static int[] PrimeFactors(int num) => PrimeFactorsE(num).ToArray();
        public static IEnumerable<int> PrimeFactorsE(int num)
        {
            int i = 2;
            while (num > 1)
            {
                while (num % i == 0)
                {
                    yield return i;
                    num /= i;
                }
                if (i > 2) i += 2;
                else i++;
            }
        }

        public static int Product(IEnumerable<int> values)
        {
            bool any = false;
            int prod = 1;
            foreach (int val in values)
            {
                any = true;
                prod *= val;
            }
            return any ? prod : 0;
        }
        public static double Product(IEnumerable<double> values)
        {
            bool any = false;
            double prod = 1;
            foreach (double val in values)
            {
                any = true;
                prod *= val;
            }
            return any ? prod : 0;
        }
#if CS11_OR_GREATER
        public static T Product<T>(IEnumerable<T> values)
            where T : INumber<T>
        {
            bool any = false;
            T prod = T.One;
            foreach (T val in values)
            {
                any = true;
                prod *= val;
            }
            return any ? prod : T.Zero;
        }
#endif

        public static int Round(double value)
        {
            if (value > 0)
            {
                if (value % 1 >= 0.5) return (int)value + 1;
                else return (int)value;
            }
            else
            {
                if (-value % 1 >= 0.5) return (int)value - 1;
                else return (int)value;
            }
        }
        public static void Round(ref double value)
        {
            if (value > 0)
            {
                if (value % 1 >= 0.5) value += 1 - value % 1;
                else value -= value % 1;
            }
            else
            {
                if (-value % 1 >= 0.5) value -= 1 + value % 1;
                else value -= value % 1;
            }
        }
        public static IEquation Round(IEquation equ) =>
            new Equation((double x) => Round(equ.Get(x)));

#if CS11_OR_GREATER
        public static int Sign<T>(T num) where T : INumber<T> =>
            num > T.Zero ? 1 : num < T.Zero ? -1 : 0;
#endif
        public static int Sign(double num) => num > 0 ? 1 : num < 0 ? -1 : 0;
        public static int Sign(int num) => num > 0 ? 1 : num < 0 ? -1 : 0;

        public static double Sin(double rad)
        {
            // My previous implementation of the taylor series was flawed,
            // but it doesn't matter because this implementation is more
            // accurate with less terms.

            // First, constrain the input to [0, 2pi)
            rad = ModAbs(rad, Constants.Tau);

            // Then constrain to [0, pi)
            double x;
            if (rad > Constants.Pi)
            {
                x = -1;
                rad = Constants.Tau - rad;
            }
            else x = 1;

            // Then constrain to [0, pi/2)
            if (rad > Constants.HalfPi) rad = Constants.Pi - rad;

            // Then split into two conditions, x > pi/4 or x <= pi/4
            // We have a different polynomial for each.
            if (rad <= Constants.HalfPi * 0.5)
            {
                const double c1 =  0.999996294518,
                             c2 =  0.0000604571732224,
                             c3 = -0.1669919062,
                             c4 =  0.000737798693456,
                             c5 =  0.00768534676739;

                // Cumulative multiply, a little scuffed but it works.
                return c1 * (x *= rad) +
                       c2 * (x *= rad) +
                       c3 * (x *= rad) +
                       c4 * (x *= rad) +
                       c5 * (x *= rad);
            }
            else
            {
                const double c1 = 0.997825841172,
                             c2 = 0.00929372002469,
                             c3 = -0.182014083754,
                             c4 = 0.0119041921981,
                             c5 = 0.0044612026723;

                // Cumulative multiply, a little scuffed but it works.
                return c1 * (x *= rad) +
                       c2 * (x *= rad) +
                       c3 * (x *= rad) +
                       c4 * (x *= rad) +
                       c5 * (x *= rad);
            }
        }
        public static double Sin(Angle angle) => Sin(angle.Radians);
        public static IEquation Sin(IEquation inputRad) => new Equation(x => Sin(inputRad[x]));
        public static double Cos(double rad) => Sin(rad + Constants.HalfPi);
        public static double Cos(Angle angle) => Sin(angle.Radians + Constants.HalfPi);
        public static IEquation Cos(IEquation inputRad) => new Equation(x => Sin(inputRad[x] + Constants.HalfPi));
        public static double Tan(double rad) => Sin(rad) / Sin(rad + Constants.HalfPi);
        public static double Tan(Angle angle) => Sin(angle.Radians) / Sin(angle.Radians + Constants.HalfPi);
        public static IEquation Tan(IEquation inputRad) => new Equation(x => Sin(inputRad[x]) / Sin(inputRad[x] + Constants.HalfPi));
        public static double Csc(double rad) => 1 / Sin(rad);
        public static double Csc(Angle angle) => 1 / Sin(angle.Radians);
        public static IEquation Csc(IEquation inputRad) => new Equation(x => 1 / Sin(inputRad[x]));
        public static double Sec(double rad) => 1 / Sin(rad + Constants.HalfPi);
        public static double Sec(Angle angle) => 1 / Sin(angle.Radians + Constants.HalfPi);
        public static IEquation Sec(IEquation inputRad) => new Equation(x => 1 / Sin(inputRad[x] + Constants.HalfPi));
        public static double Cot(double rad) => Sin(rad + Constants.HalfPi) / Sin(rad);
        public static double Cot(Angle angle) => Sin(angle.Radians + Constants.HalfPi) / Sin(angle.Radians);
        public static IEquation Cot(IEquation inputRad) => new Equation(x => Sin(inputRad[x] + Constants.HalfPi) / Sin(inputRad[x]));

        // YOU CANNOT USE POW HERE!!!
        // The CordicHelper uses the Sqrt function for the Pow method.
        // It'll cause a stack overflow.
        // !!TODO!! - Bring back Newton's
        public static double Sqrt(double num) => 1 / InverseSqrtFast((float)num);
        public static IEquation Sqrt(IEquation equ) =>
            new Equation((double x) => Sqrt(equ.Get(x)));

        public static double Stdev(IEnumerable<double> nums) => Sqrt(Variance(nums));

        public static int Sum(IEnumerable<int> nums)
        {
            int sum = 0;
            foreach (int num in nums) sum += num;
            return sum;
        }
        public static double Sum(IEnumerable<double> nums)
        {
            double sum = 0;
            foreach (double num in nums) sum += num;
            return sum;
        }
#if CS11_OR_GREATER
        public static T Sum<T>(IEnumerable<T> nums) where T : INumber<T>
        {
            T sum = T.Zero;
            foreach (T num in nums) sum += num;
            return sum;
        }
#endif

        public static Linear TangentLine(IEquation equ, double x)
        {
            double slope = equ.Derive()[x];
            return new Linear(slope, equ[x] - slope * x);
        }

        public static Polynomial TaylorSeries(IEquation equ, double x, int terms)
        {
            IEquation current = equ;
            double[] coeffs = new double[terms];
            int fact = 1;
            for (int i = 0; i < terms; i++)
            {
                coeffs[i] = current.Get(x) / fact;
                current = current.Derive();
                fact *= i + 1;
            }
            return new Polynomial(false, coeffs);
        }

        public static double Variance(IEnumerable<double> nums)
        {
            double mean = Average(nums), sum = 0;
            int count = 0;
            foreach (double num in nums)
            {
                double dist = num - mean;
                sum += dist * dist;
                count++;
            }
            return sum / (count - 1);
        }

        public static double ZScore(double val, IEnumerable<double> nums)
        {
            double mean = Average(nums), sum = 0;
            int count = 0;
            foreach (double num in nums)
            {
                double dist = num - mean;
                sum += dist * dist;
                count++;
            }
            double stdev = Sqrt(sum / count - 1);
            return (val - mean) / stdev;
        }
        public static double ZScore(double val, double mean, double stdev) =>
            (val - mean) / stdev;
    }
}
