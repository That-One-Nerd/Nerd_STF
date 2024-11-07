using Nerd_STF.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nerd_STF.Mathematics
{
    public struct Angle : IComparable<Angle>,
                          IEquatable<Angle>
#if CS11_OR_GREATER
                         ,IPresets2d<Angle>,
                          IFromTuple<Angle, (double, Angle.Unit)>
#endif
    {
        public static Angle Down => new Angle(0.75);
        public static Angle Left => new Angle(0.5);
        public static Angle Right => new Angle(0);
        public static Angle Up => new Angle(0.25);

        public static Angle Full => new Angle(1);
        public static Angle Half => new Angle(0.5);
        public static Angle Quarter => new Angle(0.25);
        public static Angle Zero => new Angle(0);

#if CS11_OR_GREATER
        static Angle IPresets1d<Angle>.One => new Angle(1, Unit.Degrees);
#endif

        public double Degrees
        {
            get => revTheta * 360;
            set => revTheta = value / 360;
        }
        public double Gradians
        {
            get => revTheta * 400;
            set => revTheta = value / 400;
        }
        public double Radians
        {
            get => revTheta * Constants.Tau;
            set => revTheta = value / Constants.Tau;
        }
        public double Revolutions
        {
            get => revTheta;
            set => revTheta = value;
        }

        public Angle Complimentary => new Angle(0.25 - MathE.ModAbs(revTheta, 1));
        public Angle Supplimentary => new Angle(0.5 - MathE.ModAbs(revTheta, 1));
        public Angle Normalized => new Angle(MathE.ModAbs(revTheta, 1));
        public Angle Reflected => new Angle(MathE.ModAbs(-revTheta, 1));

        private double revTheta;

        public Angle(double theta, Unit unit)
        {
            switch (unit)
            {
                case Unit.Revolutions: revTheta = theta; break;
                case Unit.Degrees: revTheta = theta / 360; break;
                case Unit.Radians: revTheta = theta / Constants.Tau; break;
                case Unit.Gradians: revTheta = theta / 400; break;
                default: throw new ArgumentException($"Unknown angle unit \"{unit}.\"", nameof(unit));
            }
        }
        private Angle(double revTheta)
        {
            this.revTheta = revTheta;
        }

        public double this[Unit unit]
        {
            get
            {
                switch (unit)
                {
                    case Unit.Revolutions: return revTheta;
                    case Unit.Degrees: return revTheta * 360;
                    case Unit.Radians: return revTheta * Constants.Tau;
                    case Unit.Gradians: return revTheta * 400;
                    default: throw new ArgumentException($"Unknown angle unit \"{unit}.\"", nameof(unit));
                }
            }
            set
            {
                switch (unit)
                {
                    case Unit.Revolutions: revTheta = value; break;
                    case Unit.Degrees: revTheta = value / 360; break;
                    case Unit.Radians: revTheta = value / Constants.Tau; break;
                    case Unit.Gradians: revTheta = value / 400; break;
                    default: throw new ArgumentException($"Unknown angle unit \"{unit}.\"", nameof(unit));
                }
            }
        }

        public static Angle Average(IEnumerable<Angle> angles)
        {
            Angle sum = Zero;
            int count = 0;
            foreach (Angle ang in angles)
            {
                sum += ang;
                count++;
            }
            return sum;
        }
        public static Angle Clamp(Angle value, Angle min, Angle max) =>
            new Angle(MathE.Clamp(value.revTheta, min.revTheta, max.revTheta));
        public static void Clamp(ref Angle value, Angle min, Angle max) =>
            MathE.Clamp(ref value.revTheta, min.revTheta, max.revTheta);
        public static Angle Max(IEnumerable<Angle> values) => Max(false, values);
        public static Angle Max(bool normalize, IEnumerable<Angle> values)
        {
            bool any = false;
            Angle best = Zero;
            double bestNormalized = 0;
            foreach (Angle ang in values)
            {
                if (!any)
                {
                    best = ang;
                    if (normalize) bestNormalized = MathE.ModAbs(ang.revTheta, 1);
                    any = true;
                }
                else if (normalize)
                {
                    double angNormalized = MathE.ModAbs(ang.revTheta, 1);
                    if (angNormalized > bestNormalized)
                    {
                        best = ang;
                        bestNormalized = angNormalized;
                    }
                }
                else if (ang.revTheta > best.revTheta) best = ang;
            }
            return best;
        }
        public static Angle Min(IEnumerable<Angle> values) => Min(false, values);
        public static Angle Min(bool normalize, IEnumerable<Angle> values)
        {
            bool any = false;
            Angle best = Zero;
            double bestNormalized = 0;
            foreach (Angle ang in values)
            {
                if (!any)
                {
                    best = ang;
                    if (normalize) bestNormalized = MathE.ModAbs(ang.revTheta, 1);
                    any = true;
                }
                else if (normalize)
                {
                    double angNormalized = MathE.ModAbs(ang.revTheta, 1);
                    if (angNormalized < bestNormalized)
                    {
                        best = ang;
                        bestNormalized = angNormalized;
                    }
                }
                else if (ang.revTheta < best.revTheta) best = ang;
            }
            return best;
        }
        public static Angle Sum(IEnumerable<Angle> angles)
        {
            Angle sum = Zero;
            foreach (Angle ang in angles) sum += ang;
            return sum;
        }

        public static double[] SplitArray(Unit unit, IEnumerable<Angle> values)
        {
            int count = values.Count();
            double[] angles = new double[count];
            int index = 0;
            foreach (Angle val in values)
            {
                angles[index] = val[unit];
                index++;
            }
            return angles;
        }

        public int CompareTo(Angle other) => revTheta.CompareTo(other.revTheta);
        public bool Equals(Angle other) => revTheta == other.revTheta;
#if CS8_OR_GREATER
        public override bool Equals(object? other)
#else
        public override bool Equals(object other)
#endif
        {
            if (other is null) return false;
            else if (other is Angle otherAng) return Equals(otherAng);
            else return false;
        }
        public override int GetHashCode() => revTheta.GetHashCode();
        public override string ToString() => ToString(Unit.Degrees, null);
        public string ToString(Unit unit) => ToString(unit, null);
#if CS8_OR_GREATER
        public string ToString(string? format) =>
#else
        public string ToString(string format) =>
#endif
            ToString(Unit.Degrees, format);
#if CS8_OR_GREATER
        public string ToString(Unit unit, string? format)
#else
        public string ToString(Unit unit, string format)
#endif
        {
            switch (unit)
            {
                case Unit.Revolutions: return $"{revTheta.ToString(format)} rev";
                case Unit.Degrees: return $"{(revTheta * 360).ToString(format)} deg";
                case Unit.Radians: return $"{(revTheta * Constants.Tau).ToString(format)} rad";
                case Unit.Gradians: return $"{(revTheta * 400).ToString(format)} grad";
                default: throw new ArgumentException($"Unknown angle unit \"{unit}.\"", nameof(unit));
            }
        }

        public static Angle operator +(Angle a, Angle b) => new Angle(a.revTheta + b.revTheta);
        public static Angle operator -(Angle a) => new Angle(-a.revTheta);
        public static Angle operator -(Angle a, Angle b) => new Angle(a.revTheta - b.revTheta);
        public static Angle operator *(Angle a, double b) => new Angle(a.revTheta * b);
        public static Angle operator /(Angle a, double b) => new Angle(a.revTheta / b);
        public static bool operator ==(Angle a, Angle b) => a.Equals(b);
        public static bool operator !=(Angle a, Angle b) => !a.Equals(b);
        public static bool operator >(Angle a, Angle b) => a.CompareTo(b) > 0;
        public static bool operator <(Angle a, Angle b) => a.CompareTo(b) < 0;
        public static bool operator >=(Angle a, Angle b) => a.CompareTo(b) >= 0;
        public static bool operator <=(Angle a, Angle b) => a.CompareTo(b) <= 0;

        public static implicit operator Angle((double, Unit) tuple) => new Angle(tuple.Item1, tuple.Item2);
#if CS11_OR_GREATER
        static implicit IFromTuple<Angle, (double, Unit)>.operator ValueTuple<double, Unit>(Angle angle) => (angle.revTheta, Unit.Revolutions);
#endif

        public enum Unit
        {
            Revolutions,
            Degrees,
            Radians,
            Gradians
        }
    }
}
