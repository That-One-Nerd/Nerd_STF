using Nerd_STF.Helpers;
using Nerd_STF.Mathematics.Algebra;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;

namespace Nerd_STF.Mathematics.Numbers
{
    public struct Polar2d : IVector<Polar2d>, IEquatable<Polar2d>
#if CS11_OR_GREATER
                           ,IFromTuple<Polar2d, (Angle, double)>,
                            IPresets2d<Polar2d>
#endif
    {
        public static Polar2d Down => new Polar2d(Angle.Down, 1);
        public static Polar2d Left => new Polar2d(Angle.Left, 1);
        public static Polar2d Right => new Polar2d(Angle.Right, 1);
        public static Polar2d Up => new Polar2d(Angle.Up, 1);

#if CS11_OR_GREATER
        // Bit of an odd definition, that's why it's hidden.
        static Polar2d IPresets1d<Polar2d>.One => new Polar2d(Angle.FromRevolutions(1), 1);
#endif
        public static Polar2d Zero => new Polar2d(Angle.Zero, 0);

        public Angle Theta
        {
            get => a;
            set => a = value;
        }
        public double Magnitude
        {
            get => m;
            set => m = value;
        }
        double IMagnitudeOperators<Polar2d>.MagnitudeSqr => m * m;

        public Polar2d Normalized
        {
            get
            {
                // Kind of a little scuffed. But it works, doesn't it?
                Polar2d copy = this;
                copy.Normalize();
                return copy;
            }
        }

        public Angle a;
        public double m;

        public Polar2d(Angle angle, double magnitude)
        {
            a = angle;
            m = magnitude;
        }
        public Polar2d(double angle, double magnitude)
        {
            a = Angle.FromRadians(angle);
            m = magnitude;
        }
        public Polar2d(Fill<double> fill)
        {
            a = Angle.FromRadians(fill(0));
            m = fill(1);
        }

        public static Polar2d Average(IEnumerable<Polar2d> values)
        {
            Angle totalAng = Angle.Zero;
            double totalMag = 0;
            int count = 0;

            foreach (Polar2d val in values)
            {
                totalAng += val.a;
                totalMag += val.m;
            }

            if (count == 0) return Zero;
            else return new Polar2d(totalAng / count, totalMag / count);
        }
        public static Polar2d Clamp(Polar2d value, Polar2d min, Polar2d max) =>
            new Polar2d(Angle.Clamp(value.a, min.a, max.a),
                        MathE.Clamp(value.m, min.m, max.m));
        public static void Clamp(ref Polar2d value, Polar2d min, Polar2d max)
        {
            // Can't clamp angle in-place, because it's a readonly struct.
            value.a = Angle.Clamp(value.a, min.a, max.a);
            MathE.Clamp(ref value.m, min.m, max.m);
        }
        public static Polar2d ClampMagnitude(Polar2d value, double minMag, double maxMag) =>
            new Polar2d(value.a, MathE.Clamp(value.m, minMag, maxMag));
        public static void ClampMagnitude(ref Polar2d value, double minMag, double maxMag) =>
            MathE.Clamp(ref value.m, minMag, maxMag);
#if CS11_OR_GREATER
        static double IDotOperation<Polar2d, double>.Dot(Polar2d a, Polar2d b) => Float2.Dot(a, b);
        static double IDotOperation<Polar2d, double>.Dot(IEnumerable<Polar2d> vals) => Float2.Dot(vals.Cast<Float2>());
#endif
        public static Polar2d Lerp(Polar2d a, Polar2d b, double t, bool clamp = true) =>
            new Polar2d(Angle.Lerp(a.a, b.a, t, clamp),
                        MathE.Lerp(a.m, b.m, t, clamp));
        public static Polar2d Sum(IEnumerable<Polar2d> vals)
        {
            Polar2d result = Zero;
            foreach (Polar2d val in vals) result += val;
            return result;
        }

        public void Normalize()
        {
            if (m < 0)
            {
                // Inverted magnitude, so we
                // flip the angle first.
                a += Angle.Half;
            }

            a = a.Normalized;
            m = 1;
        }

        public void Deconstruct(out Angle angle, out double magnitude)
        {
            angle = a;
            magnitude = m;
        }

        public Float2 ToXyz() => new Float2(m * MathE.Cos(a), m * MathE.Sin(a));

        public bool Equals(Polar2d other) => Equals(other, 1e-3);
        public bool Equals(Polar2d other, double delta)
        {
            Angle  a1 = a, a2 = other.a;
            double m1 = m, m2 = other.m;

            // Correct for negative magnitudes.
            if (m1 < 0)
            {
                a1 += Angle.Half;
                m1 *= -1;
            }
            if (m2 < 0)
            {
                a2 += Angle.Half;
                m2 *= -1;
            }

            if (Math.Abs(m1) <= delta && Math.Abs(m2) <= delta) return true; // Edge case, close to zero.
            if (Math.Abs(m1 - m2) > delta) return false;                     // Different magnitude.
            else if (!a1.Normalized.Equals(a2.Normalized)) return false;     // Different angles.
            else return true;                                                // LGTM! 👍
        }
#if CS8_OR_GREATER
        public override bool Equals(object? obj)
#else
        public override bool Equals(object obj)
#endif
        {
            if (obj is Polar2d objPolar) return Equals(objPolar);
            else return false;
        }
        public override int GetHashCode() => a.GetHashCode() ^ m.GetHashCode();
        public override string ToString() => $"({m}, {a})";
        public string ToString(string format) => $"({m.ToString(format)}, {a.ToString(format)})";

        public static Polar2d operator +(Polar2d a, Polar2d b) => (a.ToXyz() + b.ToXyz()).ToPolar();
        public static Polar2d operator -(Polar2d a) => new Polar2d(a.a, -a.m);
        public static Polar2d operator -(Polar2d a, Polar2d b) => (a.ToXyz() - b.ToXyz()).ToPolar();
        public static Polar2d operator *(Polar2d a, double b) => new Polar2d(a.a, a.m * b);
        public static Polar2d operator *(double a, Polar2d b) => new Polar2d(b.a, b.m * a);
        public static Polar2d operator /(Polar2d a, double b) => new Polar2d(a.a, a.m / b);
        public static Polar2d operator /(double a, Polar2d b) => new Polar2d(b.a, b.m / a);
        public static bool operator ==(Polar2d a, Polar2d b) => a.Equals(b);
        public static bool operator !=(Polar2d a, Polar2d b) => !a.Equals(b);

        public static implicit operator Polar2d(Float2 floats) => floats.ToPolar();
        public static explicit operator Polar2d(Float3 floats) => ((Float2)floats).ToPolar();
        public static explicit operator Polar2d(Float4 floats) => ((Float2)floats).ToPolar();
        public static implicit operator Polar2d(Int2 ints) => ints.ToPolar();
        public static explicit operator Polar2d(Int3 ints) => ((Float2)ints).ToPolar();
        public static explicit operator Polar2d(Int4 ints) => ((Float2)ints).ToPolar();
        public static implicit operator Polar2d(Point point) => ((Float2)point).ToPolar();
        public static implicit operator Polar2d(PointF point) => ((Float2)point).ToPolar();
        public static implicit operator Polar2d(Vector2 vec) => ((Float2)vec).ToPolar();
        public static implicit operator Polar2d(Vector3 vec) => ((Float2)vec).ToPolar();
        public static implicit operator Polar2d(Vector4 vec) => ((Float2)vec).ToPolar();
        public static implicit operator Polar2d((Angle, double) tuple) => new Polar2d(tuple.Item1, tuple.Item2);

        public static explicit operator Point(Polar2d polar) => (Point)polar.ToXyz();
        public static explicit operator PointF(Polar2d polar) => polar.ToXyz();
        public static implicit operator Vector2(Polar2d polar) => polar.ToXyz();
        public static implicit operator Vector3(Polar2d polar) => polar.ToXyz();
        public static implicit operator Vector4(Polar2d polar) => polar.ToXyz();
        public static implicit operator ValueTuple<Angle, double>(Polar2d polar) => (polar.a, polar.m);
    }
}
