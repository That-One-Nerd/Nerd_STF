using Nerd_STF.Exceptions;
using Nerd_STF.Mathematics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Nerd_STF.Graphics
{
    public struct ColorRGB : IColor<ColorRGB>,
                             INumberGroup<ColorRGB, double>
#if CS11_OR_GREATER
                            ,IFromTuple<ColorRGB, (double, double, double)>,
                             IFromTuple<ColorRGB, (double, double, double, double)>
#endif
    {
        public static int ChannelCount => 4;

        public static ColorRGB Black =>   new ColorRGB(0  , 0  , 0  , 1);
        public static ColorRGB Blue =>    new ColorRGB(0  , 0  , 1  , 1);
        public static ColorRGB Clear =>   new ColorRGB(0  , 0  , 0  , 0);
        public static ColorRGB Cyan =>    new ColorRGB(0  , 1  , 1  , 1);
        public static ColorRGB Gray =>    new ColorRGB(0.5, 0.5, 0.5, 1);
        public static ColorRGB Green =>   new ColorRGB(0  , 1  , 0  , 1);
        public static ColorRGB Magenta => new ColorRGB(1  , 0  , 1  , 1);
        public static ColorRGB Orange =>  new ColorRGB(1  , 0.5, 0  , 1);
        public static ColorRGB Purple =>  new ColorRGB(0.5, 0  , 1  , 1);
        public static ColorRGB Red =>     new ColorRGB(1  , 0  , 0  , 1);
        public static ColorRGB White =>   new ColorRGB(1  , 1  , 1  , 1);
        public static ColorRGB Yellow =>  new ColorRGB(1  , 1  , 0  , 1);

        public double Magnitude => MathE.Sqrt(r * r + g * g + b * b);

        public double r, g, b, a;

        public ColorRGB(double red, double green, double blue)
        {
            r = red;
            g = green;
            b = blue;
            a = 1;
        }
        public ColorRGB(double red, double green, double blue, double alpha)
        {
            r = red;
            g = green;
            b = blue;
            a = alpha;
        }
        public ColorRGB(IEnumerable<double> nums)
        {
            r = 0;
            g = 0;
            b = 0;
            a = 1;

            int index = 0;
            foreach (double item in nums)
            {
                this[index] = item;
                index++;
                if (index == 4) break;
            }
        }
        public ColorRGB(Fill<double> fill)
        {
            r = fill(0);
            g = fill(1);
            b = fill(2);
            a = fill(3);
        }

        public double this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0: return r;
                    case 1: return g;
                    case 2: return b;
                    case 3: return a;
                    default: throw new ArgumentOutOfRangeException(nameof(index));
                }
            }
            set
            {
                switch (index)
                {
                    case 0: r = value; break;
                    case 1: g = value; break;
                    case 2: b = value; break;
                    case 3: a = value; break;
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
                        case 'r': items[i] = r; break;
                        case 'g': items[i] = g; break;
                        case 'b': items[i] = b; break;
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
                        case 'r': r = stepper.Current; break;
                        case 'g': g = stepper.Current; break;
                        case 'b': b = stepper.Current; break;
                        case 'a': a = stepper.Current; break;
                        default: throw new ArgumentException("Invalid key.", nameof(key));
                    }
                }
            }
        }

        public static ColorRGB Average(IEnumerable<ColorRGB> colors, double gamma = 1.0)
        {
            double avgR = 0, avgG = 0, avgB = 0, avgA = 0;
            int count = 0;
            foreach (ColorRGB color in colors)
            {
                double correctR = Math.Pow(color.r, gamma),
                       correctG = Math.Pow(color.g, gamma),
                       correctB = Math.Pow(color.b, gamma);
                // Gamma doesn't apply to the alpha channel.

                avgR += correctR;
                avgG += correctG;
                avgB += correctB;
                avgA += color.a;
                count++;
            }
            avgR /= count;
            avgG /= count;
            avgB /= count;
            avgA /= count;
            double invGamma = 1 / gamma;
            return new ColorRGB(Math.Pow(avgR, invGamma),
                                Math.Pow(avgG, invGamma),
                                Math.Pow(avgB, invGamma),
                                avgA);
        }
#if CS11_OR_GREATER
        static ColorRGB IColor<ColorRGB>.Average(IEnumerable<ColorRGB> colors) => Average(colors);
#endif
        public static ColorRGB Clamp(ColorRGB color, ColorRGB min, ColorRGB max) =>
            new ColorRGB(MathE.Clamp(color.r, min.r, max.r),
                         MathE.Clamp(color.g, min.g, max.g),
                         MathE.Clamp(color.b, min.b, max.b),
                         MathE.Clamp(color.a, min.a, max.a));
        public static ColorRGB ClampMagnitude(ColorRGB color, double minMag, double maxMag)
        {
            ColorRGB copy = color;
            ClampMagnitude(ref copy, minMag, maxMag);
            return copy;
        }
        public static void ClampMagnitude(ref ColorRGB color, double minMag, double maxMag)
        {
            if (minMag > maxMag) throw new ClampOrderMismatchException(nameof(minMag), nameof(maxMag));
            double mag = color.Magnitude;

            if (mag < minMag)
            {
                double factor = minMag / mag;
                color.r *= factor;
                color.g *= factor;
                color.b *= factor;
            }
            else if (mag > maxMag)
            {
                double factor = maxMag / mag;
                color.r *= factor;
                color.g *= factor;
                color.b *= factor;
            }
        }
        public static double Dot(ColorRGB a, ColorRGB b) => a.r * b.r + a.g * b.g + a.b * b.b;
        public static double Dot(IEnumerable<ColorRGB> colors)
        {
            bool any = false;
            double r = 1, g = 1, b = 1;
            foreach (ColorRGB c in colors)
            {
                r *= c.r;
                g *= c.g;
                b *= c.b;
            }
            return any ? (r + g + b) : 0;
        }
        public static ColorRGB Lerp(ColorRGB a, ColorRGB b, double t, double gamma = 1.0, bool clamp = true)
        {
            double aCorrectedR = Math.Pow(a.r, gamma), bCorrectedR = Math.Pow(b.r, gamma),
                   aCorrectedG = Math.Pow(a.g, gamma), bCorrectedG = Math.Pow(b.g, gamma),
                   aCorrectedB = Math.Pow(a.b, gamma), bCorrectedB = Math.Pow(b.b, gamma);
            // Gamma doesn't apply to the alpha channel.

            double newR = MathE.Lerp(aCorrectedR, bCorrectedR, t, clamp),
                   newG = MathE.Lerp(aCorrectedG, bCorrectedG, t, clamp),
                   newB = MathE.Lerp(aCorrectedB, bCorrectedB, t, clamp),
                   newA = MathE.Lerp(a.a, b.a, t, clamp);

            double invGamma = 1 / gamma;
            return new ColorRGB(Math.Pow(newR, invGamma),
                                Math.Pow(newG, invGamma),
                                Math.Pow(newB, invGamma),
                                newA);
        }
#if CS11_OR_GREATER
        static ColorRGB IInterpolable<ColorRGB>.Lerp(ColorRGB a, ColorRGB b, double t, bool clamp) => Lerp(a, b, t, 1.0, clamp);
#endif
        public static ColorRGB Product(IEnumerable<ColorRGB> colors)
        {
            bool any = false;
            ColorRGB result = new ColorRGB(1, 1, 1, 1);
            foreach (ColorRGB color in colors)
            {
                any = true;
                result *= color;
            }
            return any ? result : Black;
        }
        public static ColorRGB Sum(IEnumerable<ColorRGB> colors)
        {
            bool any = false;
            ColorRGB result = new ColorRGB(0, 0, 0, 0);
            foreach (ColorRGB color in colors)
            {
                any = true;
                result += color;
            }
            return any ? result : Black;
        }

        public Dictionary<ColorChannel, double> GetChannels() => new Dictionary<ColorChannel, double>()
        {
            { ColorChannel.Red, r },
            { ColorChannel.Green, g },
            { ColorChannel.Blue, b },
            { ColorChannel.Alpha, a },
        };

        public TColor AsColor<TColor>() where TColor : struct, IColor<TColor>
        {
            Type type = typeof(TColor);
            if (type == typeof(ColorRGB)) return (TColor)(object)this;
            else if (type == typeof(ColorHSV)) return (TColor)(object)AsHsv();
            else if (type == typeof(ColorCMYK)) return (TColor)(object)AsCmyk();
            else if (type == typeof(ColorYCC)) return (TColor)(object)AsYcc();
            else throw new InvalidCastException();
        }
        public ColorRGB AsRgb() => this;
        public ColorHSV AsHsv()
        {
            // Thanks https://www.rapidtables.com/convert/color/rgb-to-hsv.html
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
        public ColorCMYK AsCmyk()
        {
            // Thanks https://www.rapidtables.com/convert/color/rgb-to-cmyk.html
            double diffK = MathE.Max(new double[] { r, g, b }), invDiffK = 1 / diffK;
            return new ColorCMYK((diffK - r) * invDiffK,
                                 (diffK - g) * invDiffK,
                                 (diffK - b) * invDiffK,
                                 1 - diffK);
        }
        public ColorYCC AsYcc()
        {
            // Most of this work is done in the ColorYCC implementation.
            Float3 result = ColorYCC.invMatrix.Value * (r, g, b);
            return new ColorYCC(result.x, result.y, result.z, a);
        }

        public string HexCode() => $"#{(int)(r * 255):X2}{(int)(g * 255):X2}{(int)(b * 255):X2}";

        public IEnumerator<double> GetEnumerator()
        {
            yield return r;
            yield return g;
            yield return b;
            yield return a;
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public void Deconstruct(out double r, out double g, out double b)
        {
            r = this.r;
            g = this.g;
            b = this.b;
        }
        public void Deconstruct(out double r, out double g, out double b, out double a)
        {
            r = this.r;
            g = this.g;
            b = this.b;
            a = this.a;
        }

        public bool Equals(ColorRGB other)
        {
            if (a <= 0) return other.a <= 0;
            else return r == other.r && g == other.g && b == other.b && a == other.a;
        }
        public bool Equals(IColor other) => Equals(other.AsRgb());
#if CS8_OR_GREATER
        public override bool Equals(object? other) =>
#else
        public override bool Equals(object other) =>
#endif
            other is IColor color && Equals(color.AsRgb());
        public override int GetHashCode() => base.GetHashCode();
        public override string ToString() => $"{{ r={r:0.00}, g={g:0.00}, b={b:0.00}, a={a:0.00} }}";

        public double[] ToArray() => new double[] { r, g, b, a };
        public Fill<double> ToFill()
        {
            ColorRGB copy = this;
            return i => copy[i];
        }
        public List<double> ToList() => new List<double> { r, g, b, a };

        public static ColorRGB operator +(ColorRGB a, ColorRGB b) => new ColorRGB(a.r + b.r, a.g + b.g, a.b + b.b, 1 - (1 - a.a) * (1 - b.a));
        public static ColorRGB operator *(ColorRGB a, ColorRGB b) => new ColorRGB(a.r * b.r, a.g * b.g, a.b * b.b, a.a * b.a);
        public static ColorRGB operator *(ColorRGB a, double b) => new ColorRGB(a.r * b, a.g * b, a.b * b, a.a);
        public static ColorRGB operator /(ColorRGB a, double b) => new ColorRGB(a.r / b, a.g / b, a.b / b, a.a);
        public static bool operator ==(ColorRGB a, IColor b) => a.Equals(b.AsRgb());
        public static bool operator !=(ColorRGB a, IColor b) => !a.Equals(b.AsRgb());
        public static bool operator ==(ColorRGB a, ColorRGB b) => a.Equals(b);
        public static bool operator !=(ColorRGB a, ColorRGB b) => !a.Equals(b);

        public static implicit operator ColorRGB(ListTuple<double> tuple)
        {
            if (tuple.Length == 3) return new ColorRGB(tuple[0], tuple[1], tuple[2]);
            else if (tuple.Length == 4) return new ColorRGB(tuple[0], tuple[1], tuple[2], tuple[3]);
            else throw new InvalidCastException();
        }
        public static implicit operator ColorRGB((double, double, double) tuple) => new ColorRGB(tuple.Item1, tuple.Item2, tuple.Item3);
        public static implicit operator ColorRGB((double, double, double, double) tuple) => new ColorRGB(tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4);
        public static explicit operator ColorRGB(Float3 group) => new ColorRGB(group.x, group.y, group.z);
        public static explicit operator ColorRGB(Float4 group) => new ColorRGB(group.x, group.y, group.z, group.w);

        public static implicit operator ListTuple<double>(ColorRGB color) => new ListTuple<double>(color.r, color.g, color.b, color.a);
        public static implicit operator ValueTuple<double, double, double>(ColorRGB color) => (color.r, color.g, color.b);
        public static implicit operator ValueTuple<double, double, double, double>(ColorRGB color) => (color.r, color.g, color.b, color.a);
    }
}
