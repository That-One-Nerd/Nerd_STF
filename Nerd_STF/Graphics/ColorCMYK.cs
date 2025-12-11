using Nerd_STF.Mathematics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Nerd_STF.Graphics
{
    public struct ColorCMYK : IColor<ColorCMYK>,
                              IEnumerable<double>
#if CS11_OR_GREATER
                             ,IFromTuple<ColorCMYK, (double, double, double, double)>,
                              IFromTuple<ColorCMYK, (double, double, double, double, double)>,
                              ISplittable<ColorCMYK, (double[] Cs, double[] Ms, double[] Ys, double[] Ks, double[] As)>
#endif
    {
        public static int ChannelCount => 5;

        public static ColorCMYK Black =>   new ColorCMYK(0  , 0  , 0  , 1  , 1);
        public static ColorCMYK Blue =>    new ColorCMYK(1  , 1  , 0  , 0  , 1);
        public static ColorCMYK Clear =>   new ColorCMYK(0  , 0  , 0  , 0  , 0);
        public static ColorCMYK Cyan =>    new ColorCMYK(1  , 0  , 0  , 0  , 1);
        public static ColorCMYK Gray =>    new ColorCMYK(0  , 0  , 0  , 0.5, 1);
        public static ColorCMYK Green =>   new ColorCMYK(1  , 0  , 1  , 0  , 1);
        public static ColorCMYK Magenta => new ColorCMYK(0  , 1  , 0  , 0  , 1);
        public static ColorCMYK Orange =>  new ColorCMYK(0  , 0.5, 1  , 0  , 1);
        public static ColorCMYK Purple =>  new ColorCMYK(0.5, 1  , 0  , 0  , 1);
        public static ColorCMYK Red =>     new ColorCMYK(0  , 1  , 1  , 0  , 1);
        public static ColorCMYK White =>   new ColorCMYK(0  , 0  , 0  , 0  , 1);
        public static ColorCMYK Yellow =>  new ColorCMYK(0  , 0  , 1  , 0  , 1);

        public double c, m, y, k, a;

        public ColorCMYK(double cyan, double magenta, double yellow, double black)
        {
            c = cyan;
            m = magenta;
            y = yellow;
            k = black;
            a = 1;
        }
        public ColorCMYK(double cyan, double magenta, double yellow, double black, double alpha)
        {
            c = cyan;
            m = magenta;
            y = yellow;
            k = black;
            a = alpha;
        }
        public ColorCMYK(IEnumerable<double> nums)
        {
            c = 0;
            m = 0;
            y = 0;
            k = 0;
            a = 1;

            int index = 0;
            foreach (double item in nums)
            {
                this[index] = item;
                index++;
                if (index == 5) break;
            }
        }
        public ColorCMYK(Fill<double> fill)
        {
            c = fill(0);
            m = fill(1);
            y = fill(2);
            k = fill(3);
            a = fill(4);
        }

        public double this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0: return c;
                    case 1: return m;
                    case 2: return y;
                    case 3: return k;
                    case 4: return a;
                    default: throw new ArgumentOutOfRangeException(nameof(index));
                }
            }
            set
            {
                switch (index)
                {
                    case 0: c = value; break;
                    case 1: m = value; break;
                    case 2: y = value; break;
                    case 3: k = value; break;
                    case 4: a = value; break;
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
                        case 'c': items[i] = c; break;
                        case 'm': items[i] = m; break;
                        case 'y': items[i] = y; break;
                        case 'k': items[i] = k; break;
                        case 'a': items[i] = a; break;
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
                        case 'c': this.c = stepper.Current; break;
                        case 'm': m = stepper.Current; break;
                        case 'y': y = stepper.Current; break;
                        case 'k': k = stepper.Current; break;
                        case 'a': a = stepper.Current; break;
                        default: throw new ArgumentException("Invalid key.", nameof(key));
                    }
                }
            }
        }
    
        public static ColorCMYK Average(IEnumerable<ColorCMYK> colors, double gamma = 1.0)
        {
            double avgC = 0, avgM = 0, avgY = 0, avgK = 0, avgA = 0;
            int count = 0;
            foreach (ColorCMYK color in colors)
            {
                double correctC = Math.Pow(color.c, gamma),
                       correctM = Math.Pow(color.m, gamma),
                       correctY = Math.Pow(color.y, gamma),
                       correctK = Math.Pow(color.k, gamma);
                // Gamma doesn't apply to the alpha channel.

                avgC += correctC;
                avgM += correctM;
                avgY += correctY;
                avgK += correctK;
                avgA += color.a;
                count++;
            }
            avgC /= count;
            avgM /= count;
            avgY /= count;
            avgK /= count;
            avgA /= count;
            double invGamma = 1 / gamma;
            return new ColorCMYK(Math.Pow(avgC, invGamma),
                                 Math.Pow(avgM, invGamma),
                                 Math.Pow(avgY, invGamma),
                                 Math.Pow(avgK, invGamma),
                                 avgA);
        }
#if CS11_OR_GREATER
        static ColorCMYK IColor<ColorCMYK>.Average(IEnumerable<ColorCMYK> colors) => Average(colors);
#endif
        public static ColorCMYK Clamp(ColorCMYK color, ColorCMYK min, ColorCMYK max) =>
            new ColorCMYK(MathE.Clamp(color.c, min.c, max.c),
                          MathE.Clamp(color.m, min.m, max.m),
                          MathE.Clamp(color.y, min.y, max.y),
                          MathE.Clamp(color.k, min.k, max.k),
                          MathE.Clamp(color.a, min.a, max.a));
        public static ColorCMYK Lerp(ColorCMYK a, ColorCMYK b, double t, double gamma = 1.0, bool clamp = true)
        {
            double aCorrectedC = Math.Pow(a.c, gamma), bCorrectedC = Math.Pow(b.c, gamma),
                   aCorrectedM = Math.Pow(a.m, gamma), bCorrectedM = Math.Pow(b.m, gamma),
                   aCorrectedY = Math.Pow(a.y, gamma), bCorrectedY = Math.Pow(b.y, gamma),
                   aCorrectedK = Math.Pow(a.k, gamma), bCorrectedK = Math.Pow(b.k, gamma);

            double newC = MathE.Lerp(aCorrectedC, bCorrectedC, t, clamp),
                   newM = MathE.Lerp(aCorrectedM, bCorrectedM, t, clamp),
                   newY = MathE.Lerp(aCorrectedY, bCorrectedY, t, clamp),
                   newK = MathE.Lerp(aCorrectedK, bCorrectedK, t, clamp),
                   newA = MathE.Lerp(a.a, b.a, t, clamp);

            double invGamma = 1 / gamma;
            return new ColorCMYK(Math.Pow(newC, invGamma),
                                 Math.Pow(newM, invGamma),
                                 Math.Pow(newY, invGamma),
                                 Math.Pow(newK, invGamma),
                                 newA);
        }
#if CS11_OR_GREATER
        static ColorCMYK IInterpolable<ColorCMYK>.Lerp(ColorCMYK a, ColorCMYK b, double t, bool clamp) => Lerp(a, b, t, 1.0, clamp);
#endif
        public static ColorCMYK Product(IEnumerable<ColorCMYK> colors)
        {
            bool any = false;
            ColorCMYK result = new ColorCMYK(1, 1, 1, 1, 1);
            foreach (ColorCMYK color in colors)
            {
                any = true;
                result *= color;
            }
            return any ? result : Black;
        }
        public static ColorCMYK Sum(IEnumerable<ColorCMYK> colors)
        {
            bool any = false;
            ColorCMYK result = new ColorCMYK(0, 0, 0, 0);
            foreach (ColorCMYK color in colors)
            {
                any = true;
                result += color;
            }
            return any ? result : Black;
        }
        public static (double[] Cs, double[] Ms, double[] Ys, double[] Ks, double[] As) SplitArray(IEnumerable<ColorCMYK> colors)
        {
            int count = colors.Count();
            double[] Cs = new double[count], Ms = new double[count], Ys = new double[count], Ks = new double[count], As = new double[count];
            int index = 0;
            foreach (ColorCMYK c in colors)
            {
                Cs[index] = c.c;
                Ms[index] = c.m;
                Ys[index] = c.y;
                Ks[index] = c.k;
                As[index] = c.a;
                index++;
            }
            return (Cs, Ms, Ys, Ks, As);
        }

        public Dictionary<ColorChannel, double> GetChannels() => new Dictionary<ColorChannel, double>()
        {
            { ColorChannel.Cyan, c },
            { ColorChannel.Magenta, m },
            { ColorChannel.Yellow, y },
            { ColorChannel.Key, k },
            { ColorChannel.Alpha, a }
        };

        public TColor AsColor<TColor>() where TColor : struct, IColor<TColor>
        {
            Type type = typeof(TColor);
            if (type == typeof(ColorRGB)) return (TColor)(object)AsRgb();
            else if (type == typeof(ColorHSV)) return (TColor)(object)AsHsv();
            else if (type == typeof(ColorCMYK)) return (TColor)(object)this;
            else throw new InvalidCastException();
        }
        public ColorRGB AsRgb()
        {
            // Thanks https://www.rapidtables.com/convert/color/cmyk-to-rgb.html
            double diffK = 1 - k;
            return new ColorRGB((1 - c) * diffK,
                                (1 - m) * diffK,
                                (1 - y) * diffK);
        }
        public ColorHSV AsHsv()
        {
            // Inlined version of AsRgb().AsHsv()
            double diffK = 1 - k;
            double r = (1 - c) * diffK, g = (1 - m) * diffK, b = (1 - y) * diffK;
            double[] group = new double[] { r, g, b };
            double cMax = MathE.Max(group), cMin = MathE.Min(group), delta = cMax - cMin;
            Angle h;

            if (delta == 0) h = Angle.Zero;
            else if (cMax == r) h = Angle.FromDegrees(60 * MathE.ModAbs((g - b) / delta, 6));
            else if (cMax == g) h = Angle.FromDegrees(60 * ((b - r) / delta + 2));
            else if (cMax == b) h = Angle.FromDegrees(60 * ((r - g) / delta + 4));
            else h = Angle.Zero;

            double s = cMax == 0 ? 0 : delta / cMax;
            return new ColorHSV(h, s, cMax);
        }
        public ColorCMYK AsCmyk() => this;
        public ColorYCC AsYcc() => AsRgb().AsYcc();

        public string HexCode() => AsRgb().HexCode();

        public IEnumerator<double> GetEnumerator()
        {
            yield return c;
            yield return m;
            yield return y;
            yield return k;
            yield return a;
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public void Deconstruct(out double c, out double m, out double y, out double k)
        {
            c = this.c;
            m = this.m;
            y = this.y;
            k = this.k;
        }
        public void Deconstruct(out double c, out double m, out double y, out double k, out double a)
        {
            c = this.c;
            m = this.m;
            y = this.y;
            k = this.k;
            a = this.a;
        }

        public bool Equals(ColorCMYK other)
        {
            if (a <= 0) return other.a <= 0;
            else if (k >= 1) return other.k >= 1;
            else return c == other.c && m == other.m && y == other.y && k == other.k && a == other.a;
        }
        public bool Equals(IColor other) => Equals(other.AsCmyk());
#if CS8_OR_GREATER
        public override bool Equals(object? other) =>
#else
        public override bool Equals(object other) =>
#endif
            other is IColor color && Equals(color.AsCmyk());
        public override int GetHashCode() => base.GetHashCode();
        public override string ToString() => $"{{ c={c:0.00}, m={m:0.00}, y={y:0.00}, k={k:0.00}, a={a:0.00} }}";

        public double[] ToArray() => new double[] { c, m, y, k, a };
        public Fill<double> ToFill()
        {
            ColorCMYK copy = this;
            return i => copy[i];
        }
        public List<double> ToList() => new List<double> { c, m, y, k, a };

        public static ColorCMYK operator +(ColorCMYK a, ColorCMYK b) => new ColorCMYK(a.c + b.c, a.m + b.m, a.y + b.y, a.k + b.k, 1 - (1 - a.a) * (1 - b.a));
        public static ColorCMYK operator *(ColorCMYK a, ColorCMYK b) => new ColorCMYK(a.c * b.c, a.m * b.m, a.y * b.y, a.k * b.k, a.a * b.a);
        public static ColorCMYK operator *(ColorCMYK a, double b) => new ColorCMYK(a.c, a.m, a.y, b == 0 ? 1 : MathE.Clamp(a.k / b, 0, 1), a.a);
        public static bool operator ==(ColorCMYK a, IColor b) => a.Equals(b.AsCmyk());
        public static bool operator !=(ColorCMYK a, IColor b) => !a.Equals(b.AsCmyk());
        public static bool operator ==(ColorCMYK a, ColorCMYK b) => a.Equals(b);
        public static bool operator !=(ColorCMYK a, ColorCMYK b) => !a.Equals(b);

        public static implicit operator ColorCMYK(ListTuple<double> tuple)
        {
            if (tuple.Length == 4) return new ColorCMYK(tuple[0], tuple[1], tuple[2], tuple[3]);
            else if (tuple.Length == 5) return new ColorCMYK(tuple[0], tuple[1], tuple[2], tuple[3], tuple[4]);
            else throw new InvalidCastException();
        }
        public static implicit operator ColorCMYK((double, double, double, double) tuple) => new ColorCMYK(tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4);
        public static implicit operator ColorCMYK((double, double, double, double, double) tuple) => new ColorCMYK(tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4, tuple.Item5);

        public static implicit operator ListTuple<double>(ColorCMYK color) => new ListTuple<double>(color.c, color.m, color.y, color.k, color.a);
        public static implicit operator ValueTuple<double, double, double, double>(ColorCMYK color) => (color.c, color.m, color.y, color.k);
        public static implicit operator ValueTuple<double, double, double, double, double>(ColorCMYK color) => (color.c, color.m, color.y, color.k, color.a);
    }
}
