using Nerd_STF.Helpers;
using Nerd_STF.Mathematics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Nerd_STF.Graphics
{
    public struct ColorHSV : IColor<ColorHSV>,
                             IEnumerable<double>
#if CS11_OR_GREATER
                            ,IFromTuple<ColorHSV, (Angle, double, double)>,
                             IFromTuple<ColorHSV, (double, double, double)>,
                             IFromTuple<ColorHSV, (Angle, double, double, double)>,
                             IFromTuple<ColorHSV, (double, double, double, double)>
#endif
    {
        public static int ChannelCount => 4;

        public static ColorHSV Black =>   new ColorHSV(Angle.FromDegrees(  0), 0, 0  , 1);
        public static ColorHSV Blue =>    new ColorHSV(Angle.FromDegrees(240), 1, 1  , 1);
        public static ColorHSV Clear =>   new ColorHSV(Angle.FromDegrees(  0), 0, 0  , 0);
        public static ColorHSV Cyan =>    new ColorHSV(Angle.FromDegrees(180), 1, 1  , 1);
        public static ColorHSV Gray =>    new ColorHSV(Angle.FromDegrees(  0), 0, 0.5, 1);
        public static ColorHSV Green =>   new ColorHSV(Angle.FromDegrees(120), 1, 1  , 1);
        public static ColorHSV Magenta => new ColorHSV(Angle.FromDegrees(300), 1, 1  , 1);
        public static ColorHSV Orange =>  new ColorHSV(Angle.FromDegrees( 30), 1, 1  , 1);
        public static ColorHSV Purple =>  new ColorHSV(Angle.FromDegrees(270), 1, 1  , 1);
        public static ColorHSV Red =>     new ColorHSV(Angle.FromDegrees(  0), 1, 1  , 1);
        public static ColorHSV White =>   new ColorHSV(Angle.FromDegrees(  0), 0, 1  , 1);
        public static ColorHSV Yellow =>  new ColorHSV(Angle.FromDegrees( 60), 0, 1  , 1);

        public Angle h;
        public double s, v, a;

        public ColorHSV(Angle hue)
        {
            h = hue.Normalized;
            s = 1;
            v = 1;
            a = 1;
        }
        public ColorHSV(double hue)
        {
            h = Angle.FromRevolutions(hue);
            s = 1;
            v = 1;
            a = 1;
        }
        public ColorHSV(Angle hue, double saturation, double value)
        {
            h = hue.Normalized;
            s = saturation;
            v = value;
            a = 1;
        }
        public ColorHSV(double hue, double saturation, double value)
        {
            h = Angle.FromRevolutions(hue);
            s = saturation;
            v = value;
            a = 1;
        }
        public ColorHSV(Angle hue, double saturation, double value, double alpha)
        {
            h = hue.Normalized;
            s = saturation;
            v = value;
            a = alpha;
        }
        public ColorHSV(double hue, double saturation, double value, double alpha)
        {
            h = Angle.FromRevolutions(hue);
            s = saturation;
            v = value;
            a = alpha;
        }
        public ColorHSV(IEnumerable<double> nums)
        {
            h = Angle.Zero;
            s = 0;
            v = 0;
            a = 1;

            int index = 0;
            foreach (double item in nums)
            {
                this[index] = item;
                index++;
                if (index == 4) break;
            }
        }
        public ColorHSV(Fill<double> fill)
        {
            h = Angle.FromRevolutions(fill(0) % 1);
            s = fill(1);
            v = fill(2);
            a = fill(3);
        }

        public double this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0: return h.Revolutions;
                    case 1: return s;
                    case 2: return v;
                    case 3: return a;
                    default: throw new ArgumentOutOfRangeException(nameof(index));
                }
            }
            set
            {
                switch (index)
                {
                    case 0: h = Angle.FromRevolutions(value); break;
                    case 1: s = value; break;
                    case 2: v = value; break;
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
                        case 'h': items[i] = h.Revolutions; break;
                        case 'H': items[i] = h.Degrees; break;
                        case 's': items[i] = s; break;
                        case 'v': items[i] = v; break;
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
                        case 'h': h = Angle.FromRevolutions(stepper.Current); break;
                        case 'H': h = Angle.FromDegrees(stepper.Current); break;
                        case 's': s = stepper.Current; break;
                        case 'v': v = stepper.Current; break;
                        case 'a': a = stepper.Current; break;
                        default: throw new ArgumentException("Invalid key.", nameof(key));
                    }
                }
            }
        }

        public static ColorHSV Average(IEnumerable<ColorHSV> colors)
        {
            Angle avgH = Angle.Zero;
            double avgS = 0, avgV = 0, avgA = 0;
            int count = 0;
            foreach (ColorHSV color in colors)
            {
                avgH += color.h;
                avgS += color.s;
                avgV += color.v;
                avgA += color.a;
            }
            return new ColorHSV(avgH / count, avgS / count, avgV / count, avgA / count);
        }
        public static ColorHSV Clamp(ColorHSV color, ColorHSV min, ColorHSV max) =>
            new ColorHSV(Angle.Clamp(color.h, min.h, max.h),
                         MathE.Clamp(color.s, min.s, max.s),
                         MathE.Clamp(color.v, min.v, max.v),
                         MathE.Clamp(color.a, min.a, max.a));
        public static ColorHSV Lerp(ColorHSV a, ColorHSV b, double t, bool clamp = true) =>
            new ColorHSV(Angle.Lerp(a.h, b.h, t, clamp),
                         MathE.Lerp(a.s, b.s, t, clamp),
                         MathE.Lerp(a.v, b.v, t, clamp),
                         MathE.Lerp(a.a, b.a, t, clamp));
        public static ColorHSV Product(IEnumerable<ColorHSV> colors)
        {
            bool any = false;
            ColorHSV result = new ColorHSV(Angle.Full, 1, 1, 1);
            foreach (ColorHSV color in colors)
            {
                any = true;
                result *= color;
            }
            return any ? result : Black;
        }
        public static ColorHSV Sum(IEnumerable<ColorHSV> colors)
        {
            bool any = false;
            ColorHSV result = new ColorHSV(Angle.Zero, 0, 0, 0);
            foreach (ColorHSV color in colors)
            {
                any = true;
                result += color;
            }
            return any ? result : Black;
        }

        public Dictionary<ColorChannel, double> GetChannels() => new Dictionary<ColorChannel, double>()
        {
            { ColorChannel.Hue, h.Revolutions },
            { ColorChannel.Saturation, s },
            { ColorChannel.Value, v },
            { ColorChannel.Alpha, a }
        };
        
        public TColor AsColor<TColor>() where TColor : struct, IColor<TColor>
        {
            Type type = typeof(TColor);
            if (type == typeof(ColorRGB)) return (TColor)(object)AsRgb();
            else if (type == typeof(ColorHSV)) return (TColor)(object)this;
            else if (type == typeof(ColorCMYK)) return (TColor)(object)AsCmyk();
            else if (type == typeof(ColorYCC)) return (TColor)(object)AsYcc();
            else throw new InvalidCastException();
        }
        public ColorRGB AsRgb()
        {
            // Thanks https://www.rapidtables.com/convert/color/hsv-to-rgb.html
            double H = h.Normalized.Degrees,
                   c = v * s,
                   x = c * (1 - MathE.Abs(MathE.ModAbs(H / 60, 2) - 1)),
                   m = v - c;

            ColorRGB color;
            if (H < 60) color = new ColorRGB(c, x, 0);
            else if (H < 120) color = new ColorRGB(x, c, 0);
            else if (H < 180) color = new ColorRGB(0, c, x);
            else if (H < 240) color = new ColorRGB(0, x, c);
            else if (H < 300) color = new ColorRGB(x, 0, c);
            else if (H < 360) color = new ColorRGB(c, 0, x);
            else throw new ArgumentOutOfRangeException();

            color.r += m;
            color.g += m;
            color.b += m;
            return color;
        }
        public ColorCMYK AsCmyk() => AsRgb().AsCmyk();
        public ColorHSV AsHsv() => this;
        public ColorYCC AsYcc() => AsRgb().AsYcc();

        public string HexCode() => AsRgb().HexCode();

        public IEnumerator<double> GetEnumerator()
        {
            yield return h.Revolutions;
            yield return s;
            yield return v;
            yield return a;
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public void Deconstruct(out Angle h, out double s, out double v)
        {
            h = this.h;
            s = this.s;
            v = this.v;
        }
        public void Deconstruct(out Angle h, out double s, out double v, out double a)
        {
            h = this.h;
            s = this.s;
            v = this.v;
            a = this.a;
        }

        public bool Equals(ColorHSV other)
        {
            if (a <= 0) return other.a <= 0;
            else if (v <= 0) return other.v <= 0;
            else if (s <= 0) return other.s <= 0;
            else return h == other.h && s == other.s && v == other.v && a == other.a;
        }
        public bool Equals(IColor other) => Equals(other.AsHsv());
#if CS8_OR_GREATER
        public override bool Equals(object? other) =>
#else
        public override bool Equals(object other) =>
#endif
            other is IColor color && Equals(color.AsHsv());
        public override int GetHashCode() => base.GetHashCode();
        public override string ToString() => $"{{ h={h.Degrees:0}°, s={s:0.00}, v={v:0.00}, a={a:0.00} }}";

        public double[] ToArray() => new double[] { h.Revolutions, s, v, a };
        public Fill<double> ToFill()
        {
            ColorHSV copy = this;
            return i => copy[i];
        }
        public List<double> ToList() => new List<double> { h.Revolutions, s, v, a };

        public static ColorHSV operator +(ColorHSV a, ColorHSV b) => new ColorHSV(a.h + b.h, a.s + b.s, a.v + b.v, 1 - (1 - a.a) * (1 - b.a));
        public static ColorHSV operator *(ColorHSV a, ColorHSV b) => new ColorHSV(Angle.FromRevolutions(a.h.Revolutions * b.h.Revolutions), a.s * b.s, a.v * b.v, a.a * b.a);
        public static ColorHSV operator *(ColorHSV a, double b) => new ColorHSV(a.h, a.s * b, a.v, a.a);
        public static bool operator ==(ColorHSV a, IColor b) => a.Equals(b.AsHsv());
        public static bool operator !=(ColorHSV a, IColor b) => !a.Equals(b.AsHsv());
        public static bool operator ==(ColorHSV a, ColorHSV b) => a.Equals(b);
        public static bool operator !=(ColorHSV a, ColorHSV b) => !a.Equals(b);

        public static implicit operator ColorHSV(ListTuple<double> tuple)
        {
            if (tuple.Length == 3) return new ColorHSV(tuple[0], tuple[1], tuple[2]);
            else if (tuple.Length == 4) return new ColorHSV(tuple[0], tuple[1], tuple[2], tuple[3]);
            else throw new InvalidCastException();
        }
        public static implicit operator ColorHSV((Angle, double, double) tuple) => new ColorHSV(tuple.Item1, tuple.Item2, tuple.Item3);
        public static implicit operator ColorHSV((double, double, double) tuple) => new ColorHSV(tuple.Item1, tuple.Item2, tuple.Item3);
        public static implicit operator ColorHSV((Angle, double, double, double) tuple) => new ColorHSV(tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4);
        public static implicit operator ColorHSV((double, double, double, double) tuple) => new ColorHSV(tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4);

        public static implicit operator ListTuple<double>(ColorHSV color) => new ListTuple<double>(color.h.Revolutions, color.s, color.v, color.a);
        public static implicit operator ValueTuple<Angle, double, double>(ColorHSV color) => (color.h, color.s, color.v);
        public static implicit operator ValueTuple<double, double, double>(ColorHSV color) => (color.h.Revolutions, color.s, color.v);
        public static implicit operator ValueTuple<Angle, double, double, double>(ColorHSV color) => (color.h, color.s, color.v, color.a);
        public static implicit operator ValueTuple<double, double, double, double>(ColorHSV color) => (color.h.Revolutions, color.s, color.v, color.a);
    }
}
