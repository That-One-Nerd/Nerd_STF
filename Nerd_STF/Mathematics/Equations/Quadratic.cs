using Nerd_STF.Exceptions;
using Nerd_STF.Helpers;
using System;

namespace Nerd_STF.Mathematics.Equations
{
    public class Quadratic : IEquatable<Polynomial>, IEquatable<Quadratic>, IPolynomial
    {
        public double Discriminant => B * B - 4 * A * C;
        public double Vertex => -0.5 * B / A;

        public double A { get; set; }
        public double B { get; set; }
        public double C
        {
            get => _C;
            set
            {
                _C = value;
                showC = false;
            }
        }
        private double _C;

        internal bool showC;

        public Quadratic(double a, double b, double c)
        {
            A = a;
            B = b;
            C = c;
        }
        public Quadratic(double b, double c)
        {
            A = 0;
            B = b;
            C = c;
        }
        public Quadratic(double c)
        {
            A = 0;
            B = 0;
            C = c;
        }
        
        public double this[double x] => A * x * x + B * x + C;
        public double Get(double x) => A * x * x + B * x + C;

        public double[] GetTerms() => new double[] { C, B, A };
        public double[] GetRealRoots()
        {
            double disc = Discriminant;
            if (disc > 0)
            {
                double sqrtDisc = MathE.Sqrt(disc);
                return new double[]
                {
                    -0.5 * (B + sqrtDisc) / A,
                    -0.5 * (B - sqrtDisc) / A
                };
            }
            else if (disc == 0) return new double[] { -0.5 * B / A };
            else return TargetHelper.EmptyArray<double>();
        }

        public Quadratic Add(Quadratic other) =>
            new Quadratic(A + other.A, B + other.B, C + other.C);
        public Polynomial Add(Polynomial other) => other.Add(this);
        public IEquation Add(IEquation other)
        {
            if (other is Quadratic otherQuad) return Add(otherQuad);
            else if (other is Polynomial otherPoly) return Add(otherPoly);
            else return new Equation((double x) => Get(x) + other.Get(x));
        }
        public Quadratic Add(double constant) =>
            new Quadratic(A, B, C + constant);
        IEquation IEquation.Add(double constant) => Add(constant);
        public Quadratic Negate() => new Quadratic(-A, -B, -C);
        IEquation IEquation.Negate() => Negate();
        public Quadratic Subtract(Quadratic other) =>
            new Quadratic(A - other.A, B - other.B, C - other.C);
        public Polynomial Subtract(Polynomial other) => other.Subtract(this).Negate();
        public IEquation Subtract(IEquation other)
        {
            if (other is Quadratic otherQuad) return Subtract(otherQuad);
            else if (other is Polynomial otherPoly) return Subtract(otherPoly);
            else return new Equation((double x) => Get(x) - other.Get(x));
        }
        public Quadratic Subtract(double constant) =>
            new Quadratic(A, B, C - constant);
        IEquation IEquation.Subtract(double constant) => Subtract(constant);
        public Polynomial Multiply(Quadratic other) => // Expanded by hand because it's faster.
            new Polynomial(false,
                C * other.C,
                B * other.C + C * other.B,
                A * other.C + B * other.B + C * other.A,
                A * other.B + B * other.A,
                A * other.A);
        public Polynomial Multiply(Polynomial other) => other.Multiply(this);
        public IEquation Multiply(IEquation other)
        {
            if (other is Quadratic otherQuad) return Multiply(otherQuad);
            else if (other is Polynomial otherPoly) return Multiply(otherPoly);
            else return new Equation((double x) => Get(x) * other.Get(x));
        }
        public Quadratic Multiply(double factor) =>
            new Quadratic(A * factor, B * factor, C * factor);
        IEquation IEquation.Multiply(double factor) => Multiply(factor);
        public IEquation Divide(IEquation other) =>
            new Equation((double x) => Get(x) / other.Get(x));
        public Quadratic Divide(double factor) =>
            new Quadratic(A / factor, B / factor, C / factor);
        IEquation IEquation.Divide(double factor) => Divide(factor);

        public Linear Derive() => new Linear(2 * A, B);
        IEquation IEquation.Derive() => Derive();
        public Polynomial Integrate() => // Also expanded by hand because it's faster.
            new Polynomial(false, 0, C, B / 2, A / 3) { showC = true };
        IEquation IEquation.Integrate() => Integrate();
        public double Integrate(double lower, double upper)
        {
            // Integrate and compute by hand, since it'll always integrate
            // to a cubic.
            double intA = A / 3, intB = B / 2;
            double lowerVal = intA * lower * lower * lower +
                              intB * lower * lower +
                              C * lower;
            double upperVal = intA * upper * upper * upper +
                              intB * upper * upper +
                              C * upper;
            return upperVal - lowerVal;
        }

#if CS8_OR_GREATER
        public bool Equals(Quadratic? other) =>
#else
        public bool Equals(Quadratic other) =>
#endif
            !(other is null) && A == other.A && B == other.B && C == other.C;
#if CS8_OR_GREATER
        public bool Equals(Polynomial? other)
#else
        public bool Equals(Polynomial other)
#endif
        {
            if (other is null) return false;
            else if (other.Order <= 2) return Equals((Quadratic)other);
            else return false;
        }
#if CS8_OR_GREATER
        public override bool Equals(object? other)
#else
        public override bool Equals(object other)
#endif
        {
            if (other is null) return false;
            else if (other is Quadratic otherQuad) return Equals(otherQuad);
            else if (other is Polynomial otherPoly && otherPoly.Order <= 2) return Equals(otherPoly);
            return false;
        }
        public override int GetHashCode() => A.GetHashCode() ^ B.GetHashCode() ^ C.GetHashCode();
        public override string ToString() =>
            ToStringHelper.PolynomialToString(GetTerms(), showC, null);
#if CS8_OR_GREATER
        public string ToString(string? format) =>
#else
        public string ToString(string format) =>
#endif
            ToStringHelper.PolynomialToString(GetTerms(), showC, format);

        public static Quadratic operator +(Quadratic a, Quadratic b) => a.Add(b);
        public static Polynomial operator +(Quadratic a, Polynomial b) => a.Add(b);
        public static IEquation operator +(Quadratic a, IEquation b) => a.Add(b);
        public static Quadratic operator +(Quadratic a, double b) => a.Add(b);
        public static Quadratic operator -(Quadratic a) => a.Negate();
        public static Quadratic operator -(Quadratic a, Quadratic b) => a.Subtract(b);
        public static Polynomial operator -(Quadratic a, Polynomial b) => a.Subtract(b);
        public static IEquation operator -(Quadratic a, IEquation b) => a.Subtract(b);
        public static Quadratic operator -(Quadratic a, double b) => a.Subtract(b);
        public static Polynomial operator *(Quadratic a, Quadratic b) => a.Multiply(b);
        public static Polynomial operator *(Quadratic a, Polynomial b) => a.Multiply(b);
        public static IEquation operator *(Quadratic a, IEquation b) => a.Multiply(b);
        public static Polynomial operator *(Quadratic a, double b) => a.Multiply(b);
        public static IEquation operator /(Quadratic a, IEquation b) => a.Divide(b);
        public static Quadratic operator /(Quadratic a, double b) => a.Divide(b);
        public static bool operator ==(Quadratic a, Quadratic b) => a.Equals(b);
        public static bool operator ==(Quadratic a, Polynomial b) => a.Equals(b);
        public static bool operator !=(Quadratic a, Quadratic b) => !a.Equals(b);
        public static bool operator !=(Quadratic a, Polynomial b) => !a.Equals(b);

        public static implicit operator Quadratic(Linear linear) => new Quadratic(0, linear.M, linear.B);
        public static explicit operator Quadratic(Polynomial poly)
        {
            if (poly.Order > 2) throw new InvalidOrderException($"A quadratic is of order 2. Cannot convert a polynomial of order {poly.Order} into a quadratic.");
            else
            {
                double[] terms = poly.Terms;
                return new Quadratic(terms[2], terms[1], terms[0]);
            }
        }
    }
}
