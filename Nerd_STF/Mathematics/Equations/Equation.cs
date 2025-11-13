using System;

namespace Nerd_STF.Mathematics.Equations
{
    public class Equation : IEquation
    {
        private readonly Func<double, double> func;

        public Equation(Func<double, double> equ)
        {
            func = equ;
        }

        public static Float2[] GetPoints(IEquation equ, double lowerBound, double upperBound, double step = 1)
        {
            int count = (int)((upperBound - lowerBound) / step);
            Float2[] result = new Float2[count];
            int index = 0;
            for (double x = lowerBound; x <= upperBound && index < count; x += step)
                result[index] = new Float2(x, equ[x]);
            return result;
        }

        public double this[double x] => func(x);
        public double Get(double x) => func(x);

        public Equation Add(IEquation other) => new Equation((double x) => func(x) + other.Get(x));
        IEquation IEquation.Add(IEquation other) => Add(other);
        public Equation Add(double constant) => new Equation((double x) => func(x) + constant);
        IEquation IEquation.Add(double constant) => Add(constant);
        public Equation Negate() => new Equation((double x) => -func(x));
        IEquation IEquation.Negate() => Negate();
        public Equation Subtract(IEquation other) => new Equation((double x) => func(x) - other.Get(x));
        IEquation IEquation.Subtract(IEquation other) => Subtract(other);
        public Equation Subtract(double constant) => new Equation((double x) => func(x) - constant);
        IEquation IEquation.Subtract(double constant)=> Subtract(constant);
        public Equation Multiply(IEquation other) => new Equation((double x) => func(x) * other.Get(x));
        IEquation IEquation.Multiply(IEquation other) => Multiply(other);
        public Equation Multiply(double factor) => new Equation((double x) => func(x) * factor);
        IEquation IEquation.Multiply(double factor) => Multiply(factor);
        public Equation Divide(IEquation other) => new Equation((double x) => func(x) / other.Get(x));
        IEquation IEquation.Divide(IEquation other) => Divide(other);
        public Equation Divide(double factor) => new Equation((double x) => func(x) / factor);
        IEquation IEquation.Divide(double factor) => Divide(factor);

        public Equation Derive(double epsilon = 1e-3f) => new Equation((double x) =>
            (func(x + epsilon) - func(x)) / epsilon);
        IEquation IEquation.Derive() => Derive();

        public Equation Integrate(double epsilon = 1e-3f,
            IntegrationMethod method = IntegrationMethod.LeftRect) =>
            new Equation((double x) => Integrate(0, x, epsilon, method));
        IEquation IEquation.Integrate() => Integrate();

        public double Integrate(double lower, double upper, double epsilon = 1e-3f,
            IntegrationMethod method = IntegrationMethod.LeftRect)
        {
            switch (method)
            {
                case IntegrationMethod.LeftRect: return IntegrateLRAM(lower, upper, epsilon);
                case IntegrationMethod.MiddleRect: return IntegrateMRAM(lower, upper, epsilon);
                case IntegrationMethod.RightRect: return IntegrateRRAM(lower, upper, epsilon);
                case IntegrationMethod.Trapezoid: return IntegrateTrap(lower, upper, epsilon);
                default: throw new ArgumentException("Unknown integration method.", nameof(method));
            }
        }
        private double IntegrateLRAM(double lower, double upper, double epsilon)
        {
            bool reversed = lower > upper;
            if (reversed) (lower, upper) = (upper, lower);

            double sum = 0;
            for (double x = lower; x < upper; x += epsilon) sum += func(x) * epsilon;
            return reversed ? -sum : sum;
        }
        private double IntegrateMRAM(double lower, double upper, double epsilon)
        {
            bool reversed = lower > upper;
            if (reversed) (lower, upper) = (upper, lower);
            double halfEpsilon = epsilon * 0.5f;

            double sum = 0;
            for (double x = lower; x < upper; x += epsilon) sum += func(x + halfEpsilon) * epsilon;
            return reversed ? -sum : sum;
        }
        private double IntegrateRRAM(double lower, double upper, double epsilon)
        {
            bool reversed = lower > upper;
            if (reversed) (lower, upper) = (upper, lower);

            double sum = 0;
            for (double x = lower; x < upper; x += epsilon) sum += func(x + epsilon) * epsilon;
            return reversed ? -sum : sum;
        }
        private double IntegrateTrap(double lower, double upper, double epsilon)
        {
            bool reversed = lower > upper;
            if (reversed) (lower, upper) = (upper, lower);
            double halfEpsilon = epsilon * 0.5f;

            double sum = 0;
            double curEqu = func(lower);
            for (double x = lower + epsilon; x < upper; x += epsilon)
            {
                double nextEqu = func(x);
                sum += halfEpsilon * (curEqu + nextEqu);
                curEqu = nextEqu;
            }
            return reversed ? -sum : sum;
        }
        double IEquation.Integrate(double lower, double upper) => Integrate(lower, upper);

        public static Equation operator +(Equation a, IEquation b) => a.Add(b);
        public static Equation operator +(Equation a, double b) => a.Add(b);
        public static Equation operator -(Equation a) => a.Negate();
        public static Equation operator -(Equation a, IEquation b) => a.Subtract(b);
        public static Equation operator -(Equation a, double b) => a.Subtract(b);
        public static Equation operator *(Equation a, IEquation b) => a.Multiply(b);
        public static Equation operator *(Equation a, double b) => a.Multiply(b);
        public static Equation operator /(Equation a, IEquation b) => a.Divide(b);
        public static Equation operator /(Equation a, double b) => a.Divide(b);

        public static implicit operator Equation(Func<double, double> equ) => new Equation(equ);
        public static implicit operator Func<double, double>(Equation equ) => equ.func;
    }
}
