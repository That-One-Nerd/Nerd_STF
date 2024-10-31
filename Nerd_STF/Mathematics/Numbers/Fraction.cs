using System.Numerics;

namespace Nerd_STF.Mathematics.Numbers
{
    public struct Fraction
    {
        public static Fraction One => new Fraction(1, 1);
        public static Fraction Zero => new Fraction(0, 1);

        public int numerator;
        public int denominator;

        public Fraction(int numerator, int denominator)
        {
            this.numerator = numerator;
            this.denominator = denominator;
        }

        public static Fraction Approximate(double number, int iterations = 32)
        {
            // Forget what this algorithm is called. When I remember, I'll put its
            // Wikipedia page here.

            if (number == 0) return Zero;
            else if (number == 1) return One;
            else if (number < 0)
            {
                Fraction result = Approximate(-number, iterations);
                result.numerator = -result.numerator;
                return result;
            }
            else if (number > 1)
            {
                int whole = (int)number;
                Fraction result = Approximate(number % 1, iterations);
                result.numerator += whole * result.denominator;
                return result;
            }

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
            return new Fraction(newNum, newDen);
        }

        public double GetValue() => (double)numerator / denominator;

        public override string ToString() => $"{numerator} / {denominator}";

        public static implicit operator double(Fraction frac) => frac.GetValue();
        public static explicit operator Fraction(double num) => Approximate(num);
    }
}
