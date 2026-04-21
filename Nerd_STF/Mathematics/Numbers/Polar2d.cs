using Nerd_STF.Mathematics.Algebra;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;

namespace Nerd_STF.Mathematics.Numbers
{
    public struct Polar2d : IVector<Polar2d>, ICombinationIndexer<double>, IEquatable<Polar2d>
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

        public double this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0: return a.Radians;
                    case 1: return m;
                    default: throw new ArgumentOutOfRangeException(nameof(index));
                }
            }
            set
            {
                switch (index)
                {
                    case 0: a = Angle.FromRadians(value); break;
                    case 1: m = value; break;
                    default: throw new ArgumentOutOfRangeException(nameof(index));
                }
            }
        }
        public ListTuple<double> this[string key]
        {
            get
            {
                double[] items = new double[key.Length];
                for (int i = 0; i < key.Length; i++)
                {
                    char c = char.ToLower(key[i]);
                    switch (c)
                    {
                        case 'a': items[i] = a.Radians; break;
                        case 'm': items[i] = m; break;
                        default: throw new ArgumentException("Invalid key.", nameof(key));
                    }
                }
                return new ListTuple<double>(items);
            }
            set
            {
                IEnumerator<double> stepper = value.GetEnumerator();
                for (int i = 0; i < key.Length; i++)
                {
                    char c = char.ToLower(key[i]);
                    stepper.MoveNext();
                    switch (c)
                    {
                        case 'a': a = Angle.FromRadians(stepper.Current); break;
                        case 'm': m = stepper.Current; break;
                        default: throw new ArgumentException("Invalid key.", nameof(key));
                    }
                }
            }
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
        public static Polar2d Lerp(Polar2d a, Polar2d b, double t, bool clamp = true) =>
            new Polar2d(Angle.Lerp(a.a, b.a, t, clamp),
                        MathE.Lerp(a.m, b.m, t, clamp));
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

        public bool Equals(Polar2d other)
        {
            if (Magnitude == 0) return other.Magnitude == 0;
            else if (!a.Normalized.Equals(other.a.Normalized)) return false; // Different angles.
            else if (!m.Equals(other.m)) return false;                       // Different magnitude.
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

        public static implicit operator Float2(Polar2d polar) => polar.ToXyz();
        public static implicit operator Float3(Polar2d polar) => polar.ToXyz();
        public static implicit operator Float4(Polar2d polar) => polar.ToXyz();
        public static explicit operator Int2(Polar2d polar) => (Int2)polar.ToXyz();
        public static explicit operator Int3(Polar2d polar) => (Int3)polar.ToXyz();
        public static explicit operator Int4(Polar2d polar) => (Int4)polar.ToXyz();
        // TODO
    }
}
