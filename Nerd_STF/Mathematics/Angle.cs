using System;
using System.Collections.Generic;
using System.Linq;

namespace Nerd_STF.Mathematics
{
    // Maybe move to .Numbers and add inheritance to INumber? Does this make sense?
    public readonly struct Angle : IComparable<Angle>,
                                   IEquatable<Angle>
#if CS11_OR_GREATER
                                  ,IFromTuple<Angle, (double, Angle.Units)>,
                                   IInterpolable<Angle>,
                                   IPresets2d<Angle>
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
        static Angle IPresets1d<Angle>.One => new Angle(1, Units.Degrees);
#endif

        public double Degrees => revTheta * 360;
        public double Gradians => revTheta * 400;
        public double Radians => revTheta * Constants.Tau;
        public double Revolutions => revTheta;

        public Angle Complimentary => new Angle(0.25 - MathE.ModAbs(revTheta, 1));
        public Angle Supplimentary => new Angle(0.5 - MathE.ModAbs(revTheta, 1));
        public Angle Normalized => new Angle(MathE.ModAbs(revTheta, 1));
        public Angle Reflected => new Angle(MathE.ModAbs(-revTheta, 1));

        private readonly double revTheta;

        public static Angle FromDegrees(double deg) => new Angle(deg, Units.Degrees);
        public static Angle FromRadians(double rad) => new Angle(rad, Units.Radians);
        public static Angle FromGradians(double grade) => new Angle(grade, Units.Gradians);
        public static Angle FromRevolutions(double turns) => new Angle(turns, Units.Revolutions);

        public Angle(double theta, Units unit)
        {
            switch (unit)
            {
                case Units.Revolutions: revTheta = theta; break;
                case Units.Degrees: revTheta = theta / 360; break;
                case Units.Radians: revTheta = theta / Constants.Tau; break;
                case Units.Gradians: revTheta = theta / 400; break;
                default: throw new ArgumentException($"Unknown angle unit \"{unit}.\"", nameof(unit));
            }
        }
        private Angle(double revTheta)
        {
            this.revTheta = revTheta;
        }

        public double this[Units unit]
        {
            get
            {
                switch (unit)
                {
                    case Units.Revolutions: return revTheta;
                    case Units.Degrees: return revTheta * 360;
                    case Units.Radians: return revTheta * Constants.Tau;
                    case Units.Gradians: return revTheta * 400;
                    default: throw new ArgumentException($"Unknown angle unit \"{unit}.\"", nameof(unit));
                }
            }
        }

        public static double Convert(double value, Units from, Units to)
        {
            switch (from)
            {
                case Units.Revolutions:
                    switch (to)
                    {
                        case Units.Revolutions: return value;
                        case Units.Degrees:     return value * 360;
                        case Units.Radians:     return value * 6.28318530718;
                        case Units.Gradians:    return value * 400;
                        default: goto _fail;
                    }
                case Units.Degrees:
                    switch (to)
                    {
                        case Units.Revolutions: return value * 0.00277777777778;
                        case Units.Degrees:     return value;
                        case Units.Radians:     return value * 0.0174532925199;
                        case Units.Gradians:    return value * 1.11111111111;
                        default: goto _fail;
                    }
                case Units.Radians:
                    switch (to)
                    {
                        case Units.Revolutions: return value * 0.159154943092;
                        case Units.Degrees:     return value * 57.2957795131;
                        case Units.Radians:     return value;
                        case Units.Gradians:    return value * 63.6619772368;
                        default: goto _fail;
                    }
                case Units.Gradians:
                    switch (to)
                    {
                        case Units.Revolutions: return value * 0.0025;
                        case Units.Degrees:     return value * 0.9;
                        case Units.Radians:     return value * 0.0157079632679;
                        case Units.Gradians:    return value;
                        default: goto _fail;
                    }
                default: goto _fail;
            }
        _fail: throw new ArgumentException($"Invalid conversion: {from} -> {to}.");
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
        public static Angle Lerp(Angle a, Angle b, double t, bool clamp = true) =>
            new Angle(MathE.Lerp(a.revTheta, b.revTheta, t, clamp));
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

        public static double[] SplitArray(Units unit, IEnumerable<Angle> values)
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

        public Angle Coterminal(int turns) => new Angle(revTheta + turns);

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
        public override string ToString() => ToString(Units.Degrees, null);
        public string ToString(Units unit) => ToString(unit, null);
#if CS8_OR_GREATER
        public string ToString(string? format) =>
#else
        public string ToString(string format) =>
#endif
            ToString(Units.Degrees, format);
#if CS8_OR_GREATER
        public string ToString(Units unit, string? format)
#else
        public string ToString(Units unit, string format)
#endif
        {
            switch (unit)
            {
                case Units.Revolutions: return $"{revTheta.ToString(format)} rev";
                case Units.Degrees: return $"{(revTheta * 360).ToString(format)} deg";
                case Units.Radians: return $"{(revTheta * Constants.Tau).ToString(format)} rad";
                case Units.Gradians: return $"{(revTheta * 400).ToString(format)} grad";
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

        public static implicit operator Angle((double, Units) tuple) => new Angle(tuple.Item1, tuple.Item2);
#if CS11_OR_GREATER
        static implicit IFromTuple<Angle, (double, Units)>.operator ValueTuple<double, Units>(Angle angle) => (angle.revTheta, Units.Revolutions);
#endif

        public enum Units
        {
            Revolutions,
            Degrees,
            Radians,
            Gradians
        }
    }
}
