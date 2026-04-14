using Nerd_STF.Mathematics.Algebra;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nerd_STF.Mathematics.Numbers
{
    public struct Polar2d : IVector<Polar2d>, ICombinationIndexer<double>
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

        public Polar2d Normalized => new Polar2d(a, 1);

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

        public void Normalize() => m = 1;

        public Float2 ToXyz() => new Float2(m * MathE.Cos(a), m * MathE.Sin(a));
    }
}
