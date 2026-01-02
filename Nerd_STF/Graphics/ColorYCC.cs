using Nerd_STF.Mathematics;
using Nerd_STF.Mathematics.Algebra;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Nerd_STF.Graphics
{
    public struct ColorYCC : IColor<ColorYCC>,
                             IEnumerable<double>
#if CS11_OR_GREATER
                            ,IFromTuple<ColorYCC, (double, double, double)>,
                             IFromTuple<ColorYCC, (double, double, double, double)>,
                             ISplittable<ColorYCC, (double[] Ys, double[] Cbs, double[] Crs, double[] As)>
#endif
    {
        // Constants that you can change, depending on what specification you're trying to meet.
        // Maybe at some point I could make a better setup?
        public static double Kr
        {
            get => kr;
            set
            {
                kr = value;
                matrix.Invalidate();
                invMatrix.Invalidate();
            }
        }
        public static double Kg
        {
            get => kg;
            set
            {
                kg = value;
                matrix.Invalidate();
                invMatrix.Invalidate();
            }
        }
        public static double Kb
        {
            get => kb;
            set
            {
                kb = value;
                matrix.Invalidate();
                invMatrix.Invalidate();
            }
        }
        private static double kr = 0.299, kg = 0.587, kb = 0.114;

        // Cache the matrices and re-validate them when the constants are changed.
        // In most circumstances, this will only validate once, maybe twice.
        internal static Validatable<Matrix3x3> matrix = new Validatable<Matrix3x3>(RecalculateColorMatrix);
        internal static Validatable<Matrix3x3> invMatrix = new Validatable<Matrix3x3>(RecalculateInverseColorMatrix);

        public static int ChannelCount => 5;

        // Since the Kr, Kg, and Kb values aren't guaranteed to be constant, we
        // have to do a conversion every time one of these are invoked, with a
        // few trivial exceptions. Use wisely!
        public static ColorYCC Black => new ColorYCC(0, 0, 0, 1);
        public static ColorYCC Blue => ColorRGB.Blue.AsYcc();
        public static ColorYCC Clear => new ColorYCC(0, 0, 0, 0);
        public static ColorYCC Cyan => ColorRGB.Cyan.AsYcc();
        public static ColorYCC Gray => ColorRGB.Gray.AsYcc();
        public static ColorYCC Green => ColorRGB.Green.AsYcc();
        public static ColorYCC Magenta => ColorRGB.Magenta.AsYcc();
        public static ColorYCC Orange => ColorRGB.Orange.AsYcc();
        public static ColorYCC Purple => ColorRGB.Purple.AsYcc();
        public static ColorYCC Red => new ColorYCC(1, 1, 1, 1);
        public static ColorYCC White => ColorRGB.White.AsYcc();
        public static ColorYCC Yellow => ColorRGB.Yellow.AsYcc();

        public double y, cb, cr, a;

        public ColorYCC(double y, double cb, double cr)
        {
            this.y = y;
            this.cb = cb;
            this.cr = cr;
            a = 1;
        }
        public ColorYCC(double y, double cb, double cr, double a)
        {
            this.y = y;
            this.cb = cb;
            this.cr = cr;
            this.a = a;
        }
        public ColorYCC(IEnumerable<double> nums)
        {
            y = 0;
            cb = 0;
            cr = 0;
            a = 1;

            int index = 0;
            foreach (double item in nums)
            {
                this[index] = item;
                index++;
                if (index == 5) break;
            }
        }
        public ColorYCC(Fill<double> fill)
        {
            y = fill(0);
            cb = fill(1);
            cr = fill(2);
            a = fill(3);
        }

        public double this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0: return y;
                    case 1: return cb;
                    case 2: return cr;
                    case 3: return a;
                    default: throw new ArgumentOutOfRangeException(nameof(index));
                }
            }
            set
            {
                switch (index)
                {
                    case 0: y = value; break;
                    case 1: cb = value; break;
                    case 2: cr = value; break;
                    case 3: a = value; break;
                    default: throw new ArgumentOutOfRangeException(nameof(index));
                }
            }
        }
        public ListTuple<double> this[string key]
        {
            get
            {
                // Because we don't know the exact size (who knows how many 'c's there are),
                // we've gotta use a list here or add another for loop. Spans would be really
                // nice, maybe I'll switch to that sometime.
                List<double> items = new List<double>();
                for (int i = 0; i < key.Length; i++)
                {
                    char c = char.ToLower(key[i]);
                    switch (c)
                    {
                        case 'y': items.Add(y); break;
                        case 'c':
                            // Gotta check the next character to see if it's 'cb' or 'cr'.
                            if (i == key.Length - 1) goto default;
                            c = key[++i];
                            bool valid = true;
                            switch (c)
                            {
                                case 'b': items.Add(cb); break;
                                case 'r': items.Add(cr); break;
                                default: valid = false; break;
                            }
                            if (!valid) goto default;
                            break;
                        case 'a': items.Add(a); break;
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
                        case 'y': y = stepper.Current; break;
                        case 'c':
                            // Gotta check the next character to see if it's 'cb' or 'cr'.
                            if (i == key.Length - 1) goto default;
                            c = key[++i];
                            bool valid = true;
                            switch (c)
                            {
                                case 'b': cb = stepper.Current; break;
                                case 'r': cr = stepper.Current; break;
                                default: valid = false; break;
                            }
                            if (!valid) goto default;
                            break;
                        case 'a': a = stepper.Current; break;
                        default: throw new ArgumentException("Invalid key.", nameof(key));
                    }
                }
            }
        }

        public static ColorYCC Average(IEnumerable<ColorYCC> colors, double gamma = 1.0)
        {
            // I have no idea if this is how gamma works in YCC.
            double avgY = 0, avgCb = 0, avgCr = 0, avgA = 0;
            int count = 0;
            foreach (ColorYCC color in colors)
            {
                double correctY = Math.Pow(color.y, gamma);
                // Gamma doesn't apply the chrominance or alpha channels.

                avgY += correctY;
                avgCb += color.cb;
                avgCr += color.cr;
                avgA += color.a;
                count++;
            }
            avgY /= count;
            avgCb /= count;
            avgCr /= count;
            avgA /= count;
            double invGamma = 1 / gamma;
            return new ColorYCC(Math.Pow(avgY, invGamma),
                                avgCb,
                                avgCr,
                                avgA);
        }
#if CS11_OR_GREATER
        static ColorYCC IColor<ColorYCC>.Average(IEnumerable<ColorYCC> colors) => Average(colors);
#endif
        public static ColorYCC Clamp(ColorYCC color, ColorYCC min, ColorYCC max) =>
            new ColorYCC(MathE.Clamp(color.y, min.y, max.y),
                         MathE.Clamp(color.cb, min.cb, max.cb),
                         MathE.Clamp(color.cr, min.cr, max.cr),
                         MathE.Clamp(color.a, min.a, max.a));
        public static ColorYCC Lerp(ColorYCC a, ColorYCC b, double t, double gamma = 1.0, bool clamp = true)
        {
            double aCorrectedY = Math.Pow(a.y, gamma), bCorrectedY = Math.Pow(b.y, gamma);

            double newY = MathE.Lerp(aCorrectedY, bCorrectedY, t, clamp),
                   newCb = MathE.Lerp(a.cb, b.cb, t, clamp),
                   newCr = MathE.Lerp(a.cr, b.cr, t, clamp),
                   newA = MathE.Lerp(a.a, b.a, t, clamp);

            double invGamma = 1 / gamma;
            return new ColorYCC(Math.Pow(newY, invGamma),
                                newCb,
                                newCr,
                                newA);
        }
#if CS11_OR_GREATER
        static ColorYCC IInterpolable<ColorYCC>.Lerp(ColorYCC a, ColorYCC b, double t, bool clamp) => Lerp(a, b, t, 1.0, clamp);
#endif
        public static ColorYCC Product(IEnumerable<ColorYCC> colors)
        {
            bool any = false;
            ColorYCC result = new ColorYCC(1, 1, 1, 1);
            foreach (ColorYCC color in colors)
            {
                any = true;
                result *= color;
            }
            return any ? result : Black;
        }
        public static ColorYCC Sum(IEnumerable<ColorYCC> colors)
        {
            bool any = false;
            ColorYCC result = new ColorYCC(0, 0, 0, 0);
            foreach (ColorYCC color in colors)
            {
                any = true;
                result += color;
            }
            return any ? result : Black;
        }
        public static (double[] Ys, double[] Cbs, double[] Crs, double[] As) SplitArray(IEnumerable<ColorYCC> colors)
        {
            int count = colors.Count();
            double[] Ys = new double[count], Cbs = new double[count], Crs = new double[count], As = new double[count];
            int index = 0;
            foreach (ColorYCC c in colors)
            {
                Ys[index] = c.y;
                Cbs[index] = c.cb;
                Crs[index] = c.cr;
                As[index] = c.a;
                index++;
            }
            return (Ys, Cbs, Crs, As);
        }

        public Dictionary<ColorChannel, double> GetChannels() => new Dictionary<ColorChannel, double>()
        {
            { ColorChannel.Luminance, y },
            { ColorChannel.ChrominanceBlue, cb },
            { ColorChannel.ChrominanceRed, cr },
            { ColorChannel.Alpha, a }
        };
        public TColor AsColor<TColor>() where TColor : struct, IColor<TColor>
        {
            Type type = typeof(TColor);
            if (type == typeof(ColorRGB)) return (TColor)(object)AsRgb();
            else if (type == typeof(ColorCMYK)) return (TColor)(object)AsCmyk();
            else if (type == typeof(ColorHSV)) return (TColor)(object)AsHsv();
            else if (type == typeof(ColorYCC)) return (TColor)(object)this;
            else throw new InvalidCastException();
        }
        public ColorRGB AsRgb()
        {
            // I wouldn't necessarily have guessed that matrix multiplication is
            // used here, but I'm also not too surprised.
            Float3 result = matrix.Value * (y, cb, cr);
            return new ColorRGB(result.x, result.y, result.z, a);
        }
        public ColorCMYK AsCmyk() => AsRgb().AsCmyk();
        public ColorHSV AsHsv() => AsRgb().AsHsv();
        public ColorYCC AsYcc() => this;

        public string HexCode() => AsRgb().HexCode();

        public IEnumerator<double> GetEnumerator()
        {
            yield return y;
            yield return cb;
            yield return cr;
            yield return a;
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public void Deconstruct(out double y, out double cb, out double cr)
        {
            y = this.y;
            cb = this.cb;
            cr = this.cr;
        }
        public void Deconstruct(out double y, out double cb, out double cr, out double a)
        {
            y = this.y;
            cb = this.cb;
            cr = this.cr;
            a = this.a;
        }

        public bool Equals(ColorYCC other)
        {
            if (a <= 0) return other.a <= 0;
            else if (y <= 0) return other.y <= 0;
            else return y == other.y && cb == other.cb && cr == other.cr && a == other.a;
        }
        public bool Equals(IColor other) => Equals(other.AsYcc());
#if CS8_OR_GREATER
        public override bool Equals(object? other) =>
#else
        public override bool Equals(object other) =>
#endif
            other is IColor color && Equals(color.AsYcc());
        public override int GetHashCode() => base.GetHashCode();
        public override string ToString() => $"{{ y={y:0.00}, cb={cb:0.00}, cr={cr:0.00}, a={a:0.00} }}";

        public double[] ToArray() => new double[] { y, cb, cr, a };
        public Fill<double> ToFill()
        {
            ColorYCC copy = this;
            return i => copy[i];
        }
        public List<double> ToList() => new List<double>() { y, cb, cr, a };

        public static ColorYCC operator +(ColorYCC a, ColorYCC b) => new ColorYCC(a.y + b.y, a.cb + b.cb, a.cr + b.cr, 1 - (1 - a.a) * (1 - b.a));
        public static ColorYCC operator *(ColorYCC a, ColorYCC b) => new ColorYCC(a.y * b.y, a.cb * b.cb, a.cr * b.cr, a.a * b.a);
        public static ColorYCC operator *(ColorYCC a, double b) => new ColorYCC(a.y * b, a.cb * b, a.cr * b, a.a);
        public static bool operator ==(ColorYCC a, IColor b) => a.Equals(b);
        public static bool operator !=(ColorYCC a, IColor b) => a.Equals(b);
        public static bool operator ==(ColorYCC a, ColorYCC b) => a.Equals(b);
        public static bool operator !=(ColorYCC a, ColorYCC b) => a.Equals(b);

        public static implicit operator ColorYCC(ListTuple<double> tuple)
        {
            if (tuple.Length == 3) return new ColorYCC(tuple[0], tuple[1], tuple[2]);
            else if (tuple.Length == 4) return new ColorYCC(tuple[0], tuple[1], tuple[2], tuple[3]);
            else throw new InvalidCastException();
        }
        public static implicit operator ColorYCC((double, double, double) tuple) => new ColorYCC(tuple.Item1, tuple.Item2, tuple.Item3);
        public static implicit operator ColorYCC((double, double, double, double) tuple) => new ColorYCC(tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4);
        public static explicit operator ColorYCC(Float3 group) => new ColorYCC(group.x, group.y, group.z);
        public static explicit operator ColorYCC(Float4 group) => new ColorYCC(group.x, group.y, group.z, group.w);

        public static implicit operator ListTuple<double>(ColorYCC color) => new ListTuple<double>(color.y, color.cb, color.cr, color.a);
        public static implicit operator ValueTuple<double, double, double>(ColorYCC color) => (color.y, color.cb, color.cr);
        public static implicit operator ValueTuple<double, double, double, double>(ColorYCC color) => (color.y, color.cb, color.cr, color.a);

        // We could technically grab the inverse for cheap, but I'm not convinced
        // it's meaningfully faster than recalculating again later, and it spares
        // us the case that we don't need it yet.
        private static Matrix3x3 RecalculateColorMatrix() => new Matrix3x3(new double[,]
        {
            { kr, kg, kb },
            { -0.5 * kr / (1 - kb), -0.5 * kg / (1 - kb), 0.5 },
            { 0.5, -0.5 * kg / (1 - kr), -0.5 * kb / (1 - kr) }
        });
        private static Matrix3x3 RecalculateInverseColorMatrix() => new Matrix3x3(new double[,]
        {
            { 1, 0, 2 - 2 * kr },
            { 1, -kb * (2 - 2 * kb) / kg, -kr * (2 - 2 * kr) / kg },
            { 1, 2 - 2 * kb, 0 }
        });
    }
}
