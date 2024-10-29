using System;

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
    }
}
