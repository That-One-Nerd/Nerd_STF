using Nerd_STF.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nerd_STF.Mathematics.Equations
{
    public class Polynomial : IEquatable<Polynomial>, IPolynomial
    {
        public double[] Terms => GetTerms(true);
        public int Order => terms.Length;

        private readonly double[] terms;
        internal bool showC;

        public Polynomial(params double[] terms) : this(true, terms) { }
        public Polynomial(bool reverse, params double[] terms)
        {
            if (reverse)
            {
                this.terms = new double[terms.Length];
                for (int i = 0; i < terms.Length; i++) this.terms[i] = terms[terms.Length - 1 - i];
                this.terms = TrimExcessTerms(this.terms);
            }
            else this.terms = TrimExcessTerms(terms);
        }
        public Polynomial(IEnumerable<double> terms) : this(true, terms) { }
        public Polynomial(bool reverse, IEnumerable<double> terms)
        {
            if (reverse) this.terms = TrimExcessTerms(terms.Reverse().ToArray());
            else this.terms = TrimExcessTerms(terms.ToArray());
        }
        private static double[] TrimExcessTerms(double[] terms)
        {
            int newLength = terms.Length;
            while (newLength > 0 && terms[newLength - 1] == 0) newLength--;
            if (newLength == 0) return TargetHelper.EmptyArray<double>();
            double[] newTerms = new double[newLength];
            Array.Copy(terms, newTerms, newLength);
            return newTerms;
        }

        public double this[double x] => Get(x);
        public double Get(double x)
        {
            double xPow = 1;
            double sum = 0;
            for (int i = 0; i < terms.Length; i++)
            {
                sum += terms[i] * xPow;
                xPow *= x;
            }
            return sum;
        }

        private double[] GetTerms(bool copy)
        {
            if (copy)
            {
                double[] newTerms = new double[terms.Length];
                Array.Copy(terms, newTerms, terms.Length);
                return newTerms;
            }
            else return terms;
        }
        double[] IPolynomial.GetTerms() => GetTerms(true);

        public Polynomial Add(Polynomial other)
        {
            int thisTerms = terms.Length, otherTerms = other.terms.Length;
            double[] newTerms = new double[Math.Max(thisTerms, otherTerms)];
            for (int i = 0; i < newTerms.Length; i++)
            {
                if (i < thisTerms) newTerms[i] += terms[i];
                if (i < otherTerms) newTerms[i] += other.terms[i];
            }
            return new Polynomial(false, newTerms);
        }
        public IEquation Add(IEquation other)
        {
            if (other is Polynomial otherPoly) return Add(otherPoly);
            else return new Equation((double x) => Get(x) + other.Get(x));
        }
        public Polynomial Add(double constant)
        {
            if (terms.Length == 0) return new Polynomial(false, new double[] { constant });
            else
            {
                double[] newTerms = GetTerms(true);
                newTerms[0] += constant;
                return new Polynomial(false, newTerms);
            }
        }
        IEquation IEquation.Add(double constant) => Add(constant);
        public Polynomial Negate()
        {
            double[] newTerms = GetTerms(true);
            for (int i = 0; i < newTerms.Length; i++) newTerms[i] = -newTerms[i];
            return new Polynomial(newTerms);
        }
        IEquation IEquation.Negate() => Negate();
        public Polynomial Subtract(Polynomial other)
        {
            int thisTerms = terms.Length, otherTerms = other.terms.Length;
            double[] newTerms = new double[Math.Max(thisTerms, otherTerms)];
            for (int i = 0; i < newTerms.Length; i++)
            {
                if (i < thisTerms) newTerms[i] += terms[i];
                if (i < otherTerms) newTerms[i] -= other.terms[i];
            }
            return new Polynomial(false, newTerms);
        }
        public IEquation Subtract(IEquation other)
        {
            if (other is Polynomial otherPoly) return Subtract(otherPoly);
            else return new Equation((double x) => Get(x) - other.Get(x));
        }
        public Polynomial Subtract(double constant)
        {
            if (terms.Length == 0) return new Polynomial(false, new double[] { -constant });
            else
            {
                double[] newTerms = GetTerms(true);
                newTerms[0] -= constant;
                return new Polynomial(false, newTerms);
            }
        }
        IEquation IEquation.Subtract(double constant) => Subtract(constant);
        public Polynomial Multiply(Polynomial other)
        {
            double[] newTerms = new double[terms.Length + other.terms.Length - 1];
            for (int i = 0; i < terms.Length; i++)
            {
                for (int j = 0; j < other.terms.Length; j++)
                {
                    int index = i + j;
                    newTerms[index] += terms[i] * other.terms[j];
                }
            }
            return new Polynomial(false, newTerms);
        }
        public IEquation Multiply(IEquation other)
        {
            if (other is Polynomial otherPoly) return Multiply(otherPoly);
            else return new Equation((double x) => Get(x) + other.Get(x));
        }
        public Polynomial Multiply(double factor)
        {
            double[] newTerms = GetTerms(true);
            for (int i = 0; i < terms.Length; i++) newTerms[i] *= factor;
            return new Polynomial(false, newTerms);
        }
        IEquation IEquation.Multiply(double factor) => Multiply(factor);
        public IEquation Divide(IEquation other) =>
            new Equation((double x) => Get(x) / other.Get(x));
        public Polynomial Divide(double factor)
        {
            double[] newTerms = GetTerms(true);
            for (int i = 0; i < terms.Length; i++) newTerms[i] /= factor;
            return new Polynomial(false, newTerms);
        }
        IEquation IEquation.Divide(double factor) => Divide(factor);

        public Polynomial Derive()
        {
            double[] newTerms = new double[terms.Length - 1];
            for (int i = 1; i < terms.Length; i++) newTerms[i - 1] = terms[i] * i;
            return new Polynomial(false, newTerms);
        }
        IEquation IEquation.Derive() => Derive();
        public Polynomial Integrate()
        {
            double[] newTerms = new double[terms.Length + 1];
            for (int i = 0; i < terms.Length; i++) newTerms[i + 1] = terms[i] / (i + 1);
            return new Polynomial(false, newTerms) { showC = true };
        }
        IEquation IEquation.Integrate() => Integrate();
        public double Integrate(double lower, double upper)
        {
            double lowPow = lower, highPow = upper;
            double lowSum = 0, highSum = 0;
            for (int i = 0; i < terms.Length; i++)
            {
                double trueCoeff = terms[i] / (i + 1);
                lowSum += trueCoeff * lowPow;
                highSum += trueCoeff * highPow;
                lowPow *= lower;
                highPow *= upper;
            }
            return highSum - lowSum;
        }

#if CS8_OR_GREATER
        public bool Equals(Polynomial? other)
#else
        public bool Equals(Polynomial other)
#endif
        {
            if (other is null || terms.Length != other.terms.Length) return false;
            for (int i = 0; i < terms.Length; i++)
            {
                if (terms[i] != other.terms[i]) return false;
            }
            return true;
        }
#if CS8_OR_GREATER
        public override bool Equals(object? obj)
#else
        public override bool Equals(object obj)
#endif
        {
            if (obj is null) return false;
            else if (obj is Polynomial objPoly) return Equals(objPoly);
            else return false;
        }
        public override int GetHashCode()
        {
            int hashCode = 0;
            for (int i = 0; i < terms.Length; i++) hashCode ^= (terms[i] * (i + 1)).GetHashCode();
            return hashCode;
        }
        public override string ToString() =>
            ToStringHelper.PolynomialToString(terms, showC, null);
#if CS8_OR_GREATER
        public string ToString(string format) =>
#else
        public string ToString(string format) =>
#endif
            ToStringHelper.PolynomialToString(terms, showC, format);

        public static Polynomial operator +(Polynomial a, Polynomial b) => a.Add(b);
        public static IEquation operator +(Polynomial a, IEquation b) => a.Add(b);
        public static Polynomial operator +(Polynomial a, double b) => a.Add(b);
        public static Polynomial operator -(Polynomial a) => a.Negate();
        public static Polynomial operator -(Polynomial a, Polynomial b) => a.Subtract(b);
        public static IEquation operator -(Polynomial a, IEquation b) => a.Subtract(b);
        public static Polynomial operator -(Polynomial a, double b) => a.Subtract(b);
        public static Polynomial operator *(Polynomial a, Polynomial b) => a.Multiply(b);
        public static IEquation operator *(Polynomial a, IEquation b) => a.Multiply(b);
        public static Polynomial operator *(Polynomial a, double b) => a.Multiply(b);
        public static IEquation operator /(Polynomial a, IEquation b) => a.Divide(b);
        public static Polynomial operator /(Polynomial a, double b) => a.Divide(b);
        public static bool operator ==(Polynomial a, Polynomial b) => a.Equals(b);
        public static bool operator !=(Polynomial a, Polynomial b) => !a.Equals(b);

        public static implicit operator Polynomial(Quadratic quad) => new Polynomial(false, quad.C, quad.B, quad.A);
        public static implicit operator Polynomial(Linear linear) => new Polynomial(false, linear.B, linear.M);
    }
}
