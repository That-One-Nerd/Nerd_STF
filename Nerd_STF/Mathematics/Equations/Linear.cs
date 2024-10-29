using Nerd_STF.Exceptions;
using Nerd_STF.Helpers;
using System;

namespace Nerd_STF.Mathematics.Equations
{
    public class Linear : IEquatable<Linear>, IEquatable<Quadratic>, IEquatable<Polynomial>, IPolynomial
    {
        public double M { get; set; }
        public double B { get; set; }
        public double Root => -B / M;
        public double InverseM => -1 / M;

        public Linear(double b)
        {
            M = 0;
            B = b;
        }
        public Linear(double m, double b)
        {
            M = m;
            B = b;
        }

        public double this[double x] => M * x + B;
        public double Get(double x) => M * x + B;

        public double[] GetTerms() => new double[2] { B, M };

        public Linear Add(Linear other) =>
            new Linear(M + other.M, B + other.B);
        public Quadratic Add(Quadratic other) =>
            new Quadratic(other.A, M + other.B, B + other.C);
        public Polynomial Add(Polynomial other) => other.Add(this);
        public IEquation Add(IEquation other)
        {
            if (other is Linear otherLinear) return Add(otherLinear);
            else if (other is Quadratic otherQuad) return Add(otherQuad);
            else if (other is Polynomial otherPoly) return Add(otherPoly);
            else return new Equation((double x) => Get(x) + other.Get(x));
        }
        public Linear Add(double constant) =>
            new Linear(M, B + constant);
        IEquation IEquation.Add(double constant) => Add(constant);
        public Linear Negate() => new Linear(-M, -B);
        IEquation IEquation.Negate() => Negate();
        public Linear Subtract(Linear other) =>
            new Linear(M - other.M, B - other.B);
        public Quadratic Subtract(Quadratic other) =>
            new Quadratic(-other.A, M - other.B, B - other.C);
        public Polynomial Subtract(Polynomial other) => other.Subtract(this).Negate();
        public IEquation Subtract(IEquation other)
        {
            if (other is Linear otherLinear) return Subtract(otherLinear);
            else if (other is Quadratic otherQuad) return Subtract(otherQuad);
            else if (other is Polynomial otherPoly) return Subtract(otherPoly);
            else return new Equation((double x) => Get(x) - other.Get(x));
        }
        public Linear Subtract(double constant) =>
            new Linear(M, B - constant);
        IEquation IEquation.Subtract(double constant) => Subtract(constant);
        public Quadratic Multiply(Linear other) =>
            new Quadratic(M * other.M, M * other.B + other.M * B, B * other.B);
        public Polynomial Multiply(Quadratic other) =>
            new Polynomial(false,
                B * other.C,
                B * other.B + M * other.C,
                B * other.A + M * other.B,
                M * other.A);
        public Polynomial Multiply(Polynomial other) => other.Multiply(this);
        public IEquation Multiply(IEquation other)
        {
            if (other is Linear otherLinear) return Multiply(otherLinear);
            else if (other is Quadratic otherQuad) return Multiply(otherQuad);
            else if (other is Polynomial otherPoly) return Multiply(otherPoly);
            else return new Equation((double x) => Get(x) * other.Get(x));
        }
        public Linear Multiply(double factor) =>
            new Linear(M * factor, B * factor);
        IEquation IEquation.Multiply(double factor) => Multiply(factor);
        public IEquation Divide(IEquation other) =>
            new Equation((double x) => Get(x) / other.Get(x));
        public Linear Divide(double factor) =>
            new Linear(M / factor, B / factor);
        IEquation IEquation.Divide(double factor) => Divide(factor);

        public IEquation Derive() => new Equation((double x) => M); // Constant
        public Quadratic Integrate() =>
            new Quadratic(M / 2, B, 0) { showC = true };
        IEquation IEquation.Integrate() => Integrate();
        public double Integrate(double lower, double upper)
        {
            double intM = M / 2;
            double lowerVal = intM * lower * lower + B * lower;
            double upperVal = intM * upper * upper + B * upper;
            return upperVal - lowerVal;
        }

#if CS8_OR_GREATER
        public bool Equals(Linear? other) =>
#else
        public bool Equals(Linear other) =>
#endif
            !(other is null) && M == other.M && B == other.B;
#if CS8_OR_GREATER
        public bool Equals(Quadratic? other) =>
#else
        public bool Equals(Quadratic other) =>
#endif
            !(other is null) && other.A == 0 && M == other.B && B == other.C;
#if CS8_OR_GREATER
        public bool Equals(Polynomial? other)
#else
        public bool Equals(Polynomial other)
#endif
        {
            if (other is null) return false;
            else if (other.Order <= 1) return Equals((Linear)other);
            else return false;
        }
#if CS8_OR_GREATER
        public override bool Equals(object? other)
#else
        public override bool Equals(object other)
#endif
        {
            if (other is null) return false;
            else if (other is Linear otherLinear) return Equals(otherLinear);
            else if (other is Quadratic otherQuad) return Equals(otherQuad);
            else if (other is Polynomial otherPoly && otherPoly.Order <= 2) return Equals(otherPoly);
            return false;
        }
        public override int GetHashCode() => M.GetHashCode() ^ B.GetHashCode();
        public override string ToString() => ToStringHelper.PolynomialToString(GetTerms(), false, null);
#if CS8_OR_GREATER
        public string ToString(string? format) =>
#else
        public string ToString(string format) =>
#endif
            ToStringHelper.PolynomialToString(GetTerms(), false, format);

        public static Linear operator +(Linear a, Linear b) => a.Add(b);
        public static Quadratic operator +(Linear a, Quadratic b) => a.Add(b);
        public static Polynomial operator +(Linear a, Polynomial b) => a.Add(b);
        public static IEquation operator +(Linear a, IEquation b) => a.Add(b);
        public static Linear operator +(Linear a, double b) => a.Add(b);
        public static Linear operator -(Linear a) => a.Negate();
        public static Linear operator -(Linear a, Linear b) => a.Subtract(b);
        public static Quadratic operator -(Linear a, Quadratic b) => a.Subtract(b);
        public static Polynomial operator -(Linear a, Polynomial b) => a.Subtract(b);
        public static IEquation operator -(Linear a, IEquation b) => a.Subtract(b);
        public static Linear operator -(Linear a, double b) => a.Subtract(b);
        public static Quadratic operator *(Linear a, Linear b) => a.Multiply(b);
        public static Polynomial operator *(Linear a, Quadratic b) => a.Multiply(b);
        public static Polynomial operator *(Linear a, Polynomial b) => a.Multiply(b);
        public static IEquation operator *(Linear a, IEquation b) => a.Multiply(b);
        public static Linear operator *(Linear a, double b) => a.Multiply(b);
        public static IEquation operator /(Linear a, IEquation b) => a.Divide(b);
        public static IEquation operator /(Linear a, double b) => a.Divide(b);
        public static bool operator ==(Linear a, Linear b) => a.Equals(b);
        public static bool operator ==(Linear a, Quadratic b) => a.Equals(b);
        public static bool operator ==(Linear a, Polynomial b) => a.Equals(b);
        public static bool operator !=(Linear a, Linear b) => !a.Equals(b);
        public static bool operator !=(Linear a, Quadratic b) => !a.Equals(b);
        public static bool operator !=(Linear a, Polynomial b) => !a.Equals(b);

        public static explicit operator Linear(Quadratic quad)
        {
            if (quad.A != 0) throw new InvalidOrderException("Cannot convert a quadratic into a linear equation.");
            else return new Linear(quad.B, quad.C);
        }
        public static explicit operator Linear(Polynomial poly)
        {
            if (poly.Order > 2) throw new InvalidOrderException($"A linear equation is of order 1. Cannot convert a polynomial of order {poly.Order} into a linear equation.");
            else
            {
                double[] terms = poly.Terms;
                return new Linear(terms[1], terms[0]);
            }
        }
    }
}
