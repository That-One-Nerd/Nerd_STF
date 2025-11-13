using System;
using System.Numerics;

namespace Nerd_STF.Mathematics.Equations
{
    public interface IEquation
    {
        double this[double x] { get; }

        double Get(double x);

        IEquation Add(IEquation other);
        IEquation Add(double constant);
        IEquation Negate();
        IEquation Subtract(IEquation other);
        IEquation Subtract(double constant);
        IEquation Multiply(IEquation other);
        IEquation Multiply(double factor);
        IEquation Divide(IEquation other);
        IEquation Divide(double factor);

        IEquation Derive();
        IEquation Integrate();
        double Integrate(double lower, double upper);

        // TODO: Solve

#if CS8_OR_GREATER
        static IEquation operator +(IEquation a, IEquation b) => a.Add(b);
        static IEquation operator +(IEquation a, double b) => a.Add(b);
        static IEquation operator -(IEquation a) => a.Negate();
        static IEquation operator -(IEquation a, IEquation b) => a.Subtract(b);
        static IEquation operator -(IEquation a, double b) => a.Subtract(b);
        static IEquation operator *(IEquation a, IEquation b) => a.Multiply(b);
        static IEquation operator *(IEquation a, double b) => a.Multiply(b);
        static IEquation operator /(IEquation a, IEquation b) => a.Divide(b);
        static IEquation operator /(IEquation a, double b) => a.Divide(b);
#endif
    }
}
