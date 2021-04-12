using System;
using Nerd_STF.Interfaces;

namespace Nerd_STF.Mathematics
{
    public struct Angle : INegatives<Angle>
    {
        public float value;

        public Angle Clamped
        {
            get
            {
                Angle returned = this;
                while (returned.value >= 360)
                {
                    returned.value -= 360;
                }
                while (returned.value < 0)
                {
                    returned.value += 360;
                }
                return returned;
            }
        }
        public Angle Absolute
        {
            get
            {
                Angle returned = new(value);
                if (value < 0) returned *= -1;
                return returned;
            }
        }
        public bool IsAcute
        {
            get
            {
                Angle returned = Clamped;

                return returned.value > 0 && returned.value < 90;
            }
        }
        public bool IsClamped
        {
            get
            {
                return value < 360 && value >= 0;
            }
        }
        public bool IsNegative
        {
            get
            {
                return value < 0;
            }
        }
        public bool IsObtuse
        {
            get
            {
                Angle returned = Clamped;

                return returned.value > 90 && returned.value < 180;
            }
        }
        public bool IsReflex
        {
            get
            {
                Angle returned = Clamped;

                return returned.value > 180 && returned.value < 360;
            }
        }
        public bool IsRight
        {
            get
            {
                Angle returned = Clamped;

                return returned.value == 90;
            }
        }
        public bool IsStraight
        {
            get
            {
                Angle returned = Clamped;

                return returned.value == 180;
            }
        }
        public bool IsZero
        {
            get
            {
                Angle returned = Clamped;

                return returned.value == 0;
            }
        }
        public Angle Negative
        {
            get
            {
                Angle returned = new(value);
                if (value > 0) returned *= -1;
                return returned;
            }
        }
        public Angle Positive
        {
            get
            {
                return Absolute;
            }
        }
        public AngleType Type
        {
            get
            {
                if (IsAcute) return AngleType.Acute;
                else if (IsObtuse) return AngleType.Obtuse;
                else if (IsReflex) return AngleType.Reflex;
                else if (IsRight) return AngleType.Right;
                else if (IsStraight) return AngleType.Straight;
                else if (IsZero) return AngleType.Zero;
                else throw new ArithmeticException();
            }
        }

        public Angle(float degree)
        {
            value = degree;
        }

        public static Angle Acute
        {
            get
            {
                return new Angle(45);
            }
        }
        public static Angle Obtuse
        {
            get
            {
                return new Angle(135);
            }
        }
        public static Angle Reflex
        {
            get
            {
                return new Angle(270);
            }
        }
        public static Angle Right
        {
            get
            {
                return new Angle(90);
            }
        }
        public static Angle Straight
        {
            get
            {
                return new Angle(180);
            }
        }
        public static Angle Zero
        {
            get
            {
                return new Angle(0);
            }
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
        public bool Equals(Angle other)
        {
            return value == other.value;
        }
        public bool Equals(float other)
        {
            return value == other;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public override string ToString()
        {
            return value.ToString() + "°";
        }
        public string ToString(string format)
        {
            return value.ToString(format) + "°";
        }

        public static Angle Average(params Angle[] input)
        {
            float[] average = new float[input.Length];
            for (int i = 0; i < input.Length; i++)
            {
                average[i] = input[i].value;
            }
            return new(Math.Average(average));
        }
        public static Angle Max(params Angle[] input)
        {
            float[] max = new float[input.Length];
            for (int i = 0; i < input.Length; i++)
            {
                max[i] = input[i].value;
            }
            return new(Math.Max(max));
        }
        public static Angle Min(params Angle[] input)
        {
            float[] min = new float[input.Length];
            for (int i = 0; i < input.Length; i++)
            {
                min[i] = input[i].value;
            }
            return new(Math.Min(min));
        }

        public static Angle operator +(Angle a, Angle b)
        {
            return new(a.value + b.value);
        }
        public static Angle operator +(Angle a, float b)
        {
            return new(a.value + b);
        }
        public static Angle operator +(float a, Angle b)
        {
            return new(a + b.value);
        }
        public static Angle operator -(Angle a, Angle b)
        {
            return new(a.value - b.value);
        }
        public static Angle operator -(Angle a, float b)
        {
            return new(a.value - b);
        }
        public static Angle operator -(float a, Angle b)
        {
            return new(a - b.value);
        }
        public static Angle operator *(Angle a, Angle b)
        {
            return new(a.value * b.value);
        }
        public static Angle operator *(Angle a, float b)
        {
            return new(a.value * b);
        }
        public static Angle operator *(float a, Angle b)
        {
            return new(a * b.value);
        }
        public static Angle operator /(Angle a, Angle b)
        {
            return new(a.value / b.value);
        }
        public static Angle operator /(Angle a, float b)
        {
            return new(a.value / b);
        }
        public static Angle operator /(float a, Angle b)
        {
            return new Angle(a / b.value);
        }
        public static bool operator ==(Angle a, Angle b)
        {
            return a.Equals(b);
        }
        public static bool operator ==(Angle a, float b)
        {
            return a.Equals(b);
        }
        public static bool operator ==(float a, Angle b)
        {
            return b.Equals(a);
        }
        public static bool operator !=(Angle a, Angle b)
        {
            return !a.Equals(b);
        }
        public static bool operator !=(Angle a, float b)
        {
            return !a.Equals(b);
        }
        public static bool operator !=(float a, Angle b)
        {
            return !b.Equals(a);
        }
        public static bool operator >(Angle a, Angle b)
        {
            return a.value > b.value;
        }
        public static bool operator >(Angle a, float b)
        {
            return a.value > b;
        }
        public static bool operator >(float a, Angle b)
        {
            return a > b.value;
        }
        public static bool operator <(Angle a, Angle b)
        {
            return a.value < b.value;
        }
        public static bool operator <(Angle a, float b)
        {
            return a.value < b;
        }
        public static bool operator <(float a, Angle b)
        {
            return a < b.value;
        }
        public static bool operator >=(Angle a, Angle b)
        {
            return a.value > b.value || a.Equals(b);
        }
        public static bool operator >=(Angle a, float b)
        {
            return a.value > b || a.Equals(b);
        }
        public static bool operator >=(float a, Angle b)
        {
            return a > b.value || b.Equals(a);
        }
        public static bool operator <=(Angle a, Angle b)
        {
            return a.value < b.value || a.Equals(b);
        }
        public static bool operator <=(Angle a, float b)
        {
            return a.value < b || a.Equals(b);
        }
        public static bool operator <=(float a, Angle b)
        {
            return a < b.value || b.Equals(a);
        }

        public static explicit operator float(Angle input)
        {
            return input.value;
        }
        public static explicit operator Angle(float input)
        {
            return new Angle(input);
        }

        public enum AngleType
        {
            Acute,
            Obtuse,
            Reflex,
            Right,
            Straight,
            Zero,
        }
    }

    public struct Color
    {
        public float r, g, b, a;

        public bool IsBlue
        {
            get
            {
                return b != 0;
            }
        }
        public bool IsClear
        {
            get
            {
                return a == 0;
            }
        }
        public bool IsGreen
        {
            get
            {
                return g != 0;
            }
        }
        public bool IsRed
        {
            get
            {
                return a != 0;
            }
        }
        public bool IsTransparent
        {
            get
            {
                return a > 0 && a < 1;
            }
        }
        public bool IsBroken
        {
            get
            {
                bool returned = false;
                returned |= r < 0 || r > 1;
                returned |= g < 0 || g > 1;
                returned |= b < 0 || b > 1;
                returned |= a < 0 || a > 1;
                return returned;
            }
        }

        public static Color Black
        {
            get
            {
                return new Color(0, 0, 0);
            }
        }
        public static Color Blue
        {
            get
            {
                return new Color(0, 0, 1);
            }
        }
        public static Color Clear
        {
            get
            {
                return new Color(0, 0, 0, 0);
            }
        }
        public static Color Cyan
        {
            get
            {
                return new Color(0, 1, 1);
            }
        }
        public static Color Gray
        {
            get
            {
                return new Color(0.5f, 0.5f, 0.5f);
            }
        }
        public static Color Green
        {
            get
            {
                return new Color(0, 1, 0);
            }
        }
        public static Color Magenta
        {
            get
            {
                return new Color(1, 0, 1);
            }
        }
        public static Color Orange
        {
            get
            {
                return new Color(1, 0.5f, 0);
            }
        }
        public static Color Purple
        {
            get
            {
                return new Color(0.5f, 0, 1);
            }
        }
        public static Color Red
        {
            get
            {
                return new Color(1, 0, 0);
            }
        }
        public static Color White
        {
            get
            {
                return new Color(1, 1, 1);
            }
        }
        public static Color Yellow
        {
            get
            {
                return new Color(1, 1, 0);
            }
        }

        public Color(float r, float g, float b)
        {
            this = new Color(r, g, b, 1);
        }
        public Color(float r, float g, float b, float a)
        {
            r = Math.Clamp(r, 0, 1);
            g = Math.Clamp(g, 0, 1);
            b = Math.Clamp(b, 0, 1);
            a = Math.Clamp(a, 0, 1);

            this.r = r;
            this.g = g;
            this.b = b;
            this.a = a;
        }

        public void Check()
        {
            r = Math.Clamp(r, 0, 1);
            g = Math.Clamp(g, 0, 1);
            b = Math.Clamp(b, 0, 1);
            a = Math.Clamp(a, 0, 1);
        }

        public static Color Average(params Color[] input)
        {
            float[] averageR = new float[input.Length];
            float[] averageG = new float[input.Length];
            float[] averageB = new float[input.Length];
            float[] averageA = new float[input.Length];
            for (int i = 0; i < input.Length; i++)
            {
                averageR[i] = input[i].r;
                averageG[i] = input[i].g;
                averageB[i] = input[i].b;
                averageA[i] = input[i].a;
            }
            return new(Math.Average(averageR), Math.Average(averageG), Math.Average(averageB), Math.Average(averageA));
        }
        public static Color Max(params Color[] input)
        {
            float[] maxR = new float[input.Length];
            float[] maxG = new float[input.Length];
            float[] maxB = new float[input.Length];
            float[] maxA = new float[input.Length];
            for (int i = 0; i < input.Length; i++)
            {
                maxR[i] = input[i].r;
                maxG[i] = input[i].g;
                maxB[i] = input[i].b;
                maxA[i] = input[i].a;
            }
            return new(Math.Max(maxR), Math.Max(maxG), Math.Max(maxB), Math.Max(maxA));
        }
        public static Color Min(params Color[] input)
        {
            float[] minR = new float[input.Length];
            float[] minG = new float[input.Length];
            float[] minB = new float[input.Length];
            float[] minA = new float[input.Length];
            for (int i = 0; i < input.Length; i++)
            {
                minR[i] = input[i].r;
                minG[i] = input[i].g;
                minB[i] = input[i].b;
                minA[i] = input[i].a;
            }
            return new(Math.Min(minR), Math.Min(minG), Math.Min(minB), Math.Min(minA));
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
        public bool Equals(Color other)
        {
            return r == other.r && g == other.g && b == other.b && a == other.a;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public override string ToString()
        {
            return "R: " + r.ToString() + " | G: " + g.ToString() + " | B: " + b.ToString() + " | A: " + a.ToString();
        }
        public string ToString(string format)
        {
            return "R: " + r.ToString(format) + " | G: " + g.ToString(format) + " | B: " + b.ToString(format) + " | A: " + a.ToString(format);
        }

        public static Color operator +(Color a, Color b)
        {
            return new Color(a.r + b.r, a.g + b.g, a.b + b.b, a.a + b.a);
        }
        public static Color operator +(Color a, float b)
        {
            return new Color(a.r + b, a.g + b, a.b + b, a.a + b);
        }
        public static Color operator +(float a, Color b)
        {
            return new Color(a + b.r, a + b.g, a + b.b, a + b.a);
        }
        public static Color operator -(Color a, Color b)
        {
            return new Color(a.r - b.r, a.g - b.g, a.b - b.b, a.a - b.a);
        }
        public static Color operator -(Color a, float b)
        {
            return new Color(a.r - b, a.g - b, a.b - b, a.a - b);
        }
        public static Color operator -(float a, Color b)
        {
            return new Color(a - b.r, a - b.g, a - b.b, a - b.a);
        }
        public static Color operator *(Color a, Color b)
        {
            return new Color(a.r * b.r, a.g * b.g, a.b * b.b, a.a * b.a);
        }
        public static Color operator *(Color a, float b)
        {
            return new Color(a.r * b, a.g * b, a.b * b, a.a * b);
        }
        public static Color operator *(float a, Color b)
        {
            return new Color(a * b.r, a * b.g, a * b.b, a * b.a);
        }
        public static Color operator /(Color a, Color b)
        {
            return new Color(a.r / b.r, a.g / b.g, a.b / b.b, a.a / b.a);
        }
        public static Color operator /(Color a, float b)
        {
            return new Color(a.r / b, a.g / b, a.b / b, a.a / b);
        }
        public static Color operator /(float a, Color b)
        {
            return new Color(a / b.r, a / b.g, a / b.b, a / b.a);
        }
        public static bool operator ==(Color a, Color b)
        {
            return a.Equals(b);
        }
        public static bool operator !=(Color a, Color b)
        {
            return !a.Equals(b);
        }

        public static implicit operator Color(ColorByte input)
        {
            return new Color(input.r / 255, input.g / 255, input.b / 255, input.a / 255);
        }
        public static explicit operator Color(Vector3 input)
        {
            return new Color(input.x, input.y, input.z, 1);
        }
        public static explicit operator Color(Vector4 input)
        {
            return new Color(input.x, input.y, input.z, input.w);
        }
    }
    public struct ColorByte
    {
        public byte r, g, b, a;

        public bool IsBlue
        {
            get
            {
                return b != byte.MinValue;
            }
        }
        public bool IsClear
        {
            get
            {
                return a == byte.MinValue;
            }
        }
        public bool IsGreen
        {
            get
            {
                return g != byte.MinValue;
            }
        }
        public bool IsRed
        {
            get
            {
                return a != byte.MinValue;
            }
        }
        public bool IsTransparent
        {
            get
            {
                return a > byte.MinValue && a < byte.MaxValue;
            }
        }

        public static ColorByte Black
        {
            get
            {
                return new ColorByte(0, 0, 0);
            }
        }
        public static ColorByte Blue
        {
            get
            {
                return new ColorByte(0, 0, 255);
            }
        }
        public static ColorByte Clear
        {
            get
            {
                return new ColorByte(0, 0, 0, 0);
            }
        }
        public static ColorByte Cyan
        {
            get
            {
                return new ColorByte(0, 255, 255);
            }
        }
        public static ColorByte Gray
        {
            get
            {
                return new ColorByte(128, 128, 128);
            }
        }
        public static ColorByte Green
        {
            get
            {
                return new ColorByte(0, 255, 0);
            }
        }
        public static ColorByte Magenta
        {
            get
            {
                return new ColorByte(255, 0, 255);
            }
        }
        public static ColorByte Orange
        {
            get
            {
                return new ColorByte(1, 128, 0);
            }
        }
        public static ColorByte Purple
        {
            get
            {
                return new ColorByte(128, 0, 1);
            }
        }
        public static ColorByte Red
        {
            get
            {
                return new ColorByte(255, 0, 0);
            }
        }
        public static ColorByte White
        {
            get
            {
                return new ColorByte(255, 255, 255);
            }
        }
        public static ColorByte Yellow
        {
            get
            {
                return new ColorByte(255, 255, 0);
            }
        }

        public ColorByte(byte r, byte g, byte b)
        {
            this = new ColorByte(r, g, b, byte.MaxValue);
        }
        public ColorByte(byte r, byte g, byte b, byte a)
        {
            this.r = r;
            this.g = g;
            this.b = b;
            this.a = a;
        }
        public ColorByte(int r, int g, int b)
        {
            this = new ColorByte(r, g, b, byte.MaxValue);
        }
        public ColorByte(int r, int g, int b, int a)
        {
            this.r = (byte)r;
            this.g = (byte)g;
            this.b = (byte)b;
            this.a = (byte)a;
        }

        public static ColorByte Average(params ColorByte[] input)
        {
            float[] averageR = new float[input.Length];
            float[] averageG = new float[input.Length];
            float[] averageB = new float[input.Length];
            float[] averageA = new float[input.Length];
            for (int i = 0; i < input.Length; i++)
            {
                averageR[i] = input[i].r;
                averageG[i] = input[i].g;
                averageB[i] = input[i].b;
                averageA[i] = input[i].a;
            }
            return new(Math.RoundToInt(Math.Average(averageR)), Math.RoundToInt(Math.Average(averageG)), Math.RoundToInt(Math.Average(averageB)), Math.RoundToInt(Math.Average(averageA)));
        }
        public static ColorByte Max(params ColorByte[] input)
        {
            float[] maxR = new float[input.Length];
            float[] maxG = new float[input.Length];
            float[] maxB = new float[input.Length];
            float[] maxA = new float[input.Length];
            for (int i = 0; i < input.Length; i++)
            {
                maxR[i] = input[i].r;
                maxG[i] = input[i].g;
                maxB[i] = input[i].b;
                maxA[i] = input[i].a;
            }
            return new(Math.RoundToInt(Math.Max(maxR)), Math.RoundToInt(Math.Max(maxG)), Math.RoundToInt(Math.Max(maxB)), Math.RoundToInt(Math.Max(maxA)));
        }
        public static ColorByte Min(params ColorByte[] input)
        {
            float[] minR = new float[input.Length];
            float[] minG = new float[input.Length];
            float[] minB = new float[input.Length];
            float[] minA = new float[input.Length];
            for (int i = 0; i < input.Length; i++)
            {
                minR[i] = input[i].r;
                minG[i] = input[i].g;
                minB[i] = input[i].b;
                minA[i] = input[i].a;
            }
            return new(Math.RoundToInt(Math.Min(minR)), Math.RoundToInt(Math.Min(minG)), Math.RoundToInt(Math.Min(minB)), Math.RoundToInt(Math.Min(minA)));
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
        public bool Equals(ColorByte other)
        {
            return r == other.r && g == other.g && b == other.b && a == other.a;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public override string ToString()
        {
            return "R: " + r.ToString() + " | G: " + g.ToString() + " | B: " + b.ToString() + " | A: " + a.ToString();
        }
        public string ToString(string format)
        {
            return "R: " + r.ToString(format) + " | G: " + g.ToString(format) + " | B: " + b.ToString(format) + " | A: " + a.ToString(format);
        }

        public static ColorByte operator +(ColorByte a, ColorByte b)
        {
            return new ColorByte(a.r + b.r, a.g + b.g, a.b + b.b, a.a + b.a);
        }
        public static ColorByte operator +(ColorByte a, byte b)
        {
            return new ColorByte(a.r + b, a.g + b, a.b + b, a.a + b);
        }
        public static ColorByte operator +(byte a, ColorByte b)
        {
            return new ColorByte(a + b.r, a + b.g, a + b.b, a + b.a);
        }
        public static ColorByte operator -(ColorByte a, ColorByte b)
        {
            return new ColorByte(a.r - b.r, a.g - b.g, a.b - b.b, a.a - b.a);
        }
        public static ColorByte operator -(ColorByte a, byte b)
        {
            return new ColorByte(a.r - b, a.g - b, a.b - b, a.a - b);
        }
        public static ColorByte operator -(byte a, ColorByte b)
        {
            return new ColorByte(a - b.r, a - b.g, a - b.b, a - b.a);
        }
        public static ColorByte operator *(ColorByte a, ColorByte b)
        {
            return new ColorByte(a.r * b.r, a.g * b.g, a.b * b.b, a.a * b.a);
        }
        public static ColorByte operator *(ColorByte a, byte b)
        {
            return new ColorByte(a.r * b, a.g * b, a.b * b, a.a * b);
        }
        public static ColorByte operator *(byte a, ColorByte b)
        {
            return new ColorByte(a * b.r, a * b.g, a * b.b, a * b.a);
        }
        public static ColorByte operator /(ColorByte a, ColorByte b)
        {
            return new ColorByte(a.r / b.r, a.g / b.g, a.b / b.b, a.a / b.a);
        }
        public static ColorByte operator /(ColorByte a, byte b)
        {
            return new ColorByte(a.r / b, a.g / b, a.b / b, a.a / b);
        }
        public static ColorByte operator /(byte a, ColorByte b)
        {
            return new ColorByte(a / b.r, a / b.g, a / b.b, a / b.a);
        }
        public static bool operator ==(ColorByte a, ColorByte b)
        {
            return a.Equals(b);
        }
        public static bool operator !=(ColorByte a, ColorByte b)
        {
            return !a.Equals(b);
        }

        public static explicit operator ColorByte(Color input)
        {
            return new ColorByte((byte)(input.r * 255), (byte)(input.g * 255), (byte)(input.b * 255), (byte)(input.a * 255));
        }
    }

    public static class Math
    {
        public const float E = 2.7182818284590451f;
        public const float Pi = 3.1415926535897931f;
        public const float Tau = 6.2831853071795862f;

        public static float Absolute(float input)
        {
            if (input < 0) input *= -1;
            return input;
        }

        public static float Add(params float[] input)
        {
            float returned = 0;
            foreach (float f in input)
            {
                returned += f;
            }
            return returned;
        }

        public static float Average(params float[] input)
        {
            float returned = 0;
            foreach (float f in input)
            {
                returned += f;
            }
            returned /= input.Length;
            return returned;
        }

        public static float Clamp(float input, float min, float max)
        {
            if (min > max) throw new ArgumentException("Minimun cannot be greater than maximum.");

            if (input > max) input = max;
            else if (input < min) input = min;

            return input;
        }

        public static int Clamp(float input, int min, int max)
        {
            if (min > max) throw new ArgumentException("Minimun cannot be greater than maximum.");

            if (input > max) input = max;
            else if (input < min) input = min;

            return (int)input;
        }

        public static float Divide(params float[] input)
        {
            float returned = input[0];
            for (uint i = 1; i < input.Length; i++)
            {
                returned /= input[i];
            }
            return returned;
        }

        public static float Max(params float[] input)
        {
            float returned = input[1];
            for (uint i = 0; i < input.Length; i++)
            {
                if (input[i] > returned) returned = input[1];
            }
            return returned;
        }

        public static float Min(params float[] input)
        {
            float returned = input[1];
            for (uint i = 0; i < input.Length; i++)
            {
                if (input[i] < returned) returned = input[1];
            }
            return returned;
        }

        public static float Multiply(params float[] input)
        {
            float returned = 1;
            foreach (float f in input)
            {
                returned *= f;
            }
            return returned;
        }

        public static float Power(float input, int power)
        {
            float returned = 1;
            for (uint i = 0; i < Absolute(power); i++)
            {
                returned *= input;
            }
            if (power < 0) returned = 1 / returned;
            return returned;
        }

        public static float Round(float value)
        {
            if (value % 1 >= 0.5f) value += 1 - (value % 1);
            else value -= value % 1;

            return value;
        }
        public static float Round(float value, float dividend)
        {
            return Round(value / dividend) * dividend;
        }
        public static int RoundToInt(float value)
        {
            return (int)Round(value);
        }

        public static float Subtract(params float[] input)
        {
            float returned = input[0];
            for (uint i = 1; i < input.Length; i++)
            {
                returned -= input[i];
            }
            return returned;
        }

        public static class Formulas
        {
            public static float CircleArea(float radius)
            {
                return Pi * radius * radius;
            }
            public static float CircleCircum(float radius)
            {
                return 2 * radius * Pi;
            }
            public static float CircleDiam(float radius)
            {
                return radius * 2;
            }
            public static float CircleRadius(float circumference)
            {
                return circumference / Pi / 2;
            }

            public static float Perimeter(params float[] sideLengths)
            {
                return Add(sideLengths);
            }

            public static float RectangleArea(float length, float width)
            {
                return length * width;
            }

            public static float SquareArea(float length)
            {
                return RectangleArea(length, length);
            }
        }
    }

    public struct Percent : INegatives<Percent>
    {
        public float value;

        public Percent Absolute
        {
            get
            {
                Percent returned = new (value);
                if (returned < 0) returned *= -1;
                return returned;
            }
        }
        public bool IsFull
        {
            get
            {
                return value == 100;
            }
        }
        public bool IsNegative
        {
            get
            {
                return value < 0;
            }
        }
        public bool IsOverflow
        {
            get
            {
                return value > 100;
            }
        }
        public bool IsZero
        {
            get
            {
                return value == 0;
            }
        }
        public Percent Negative
        {
            get
            {
                Percent returned = new(value);
                if (returned > 0) returned *= -1;
                return returned;
            }
        }
        public Percent Positive
        {
            get
            {
                return Absolute;
            }
        }

        public static Percent Full
        {
            get
            {
                return new Percent(100);
            }
        }
        public static Percent One
        {
            get
            {
                return new Percent(1);
            }
        }
        public static Percent Zero
        {
            get
            {
                return new Percent(0);
            }
        }

        public Percent(float value)
        {
            this = new Percent(value, 0, 100);
        }
        public Percent(float value, float maxValue)
        {
            this = new Percent(value, 0, maxValue);
        }
        public Percent(float value, float minValue, float maxValue)
        {
            this.value = value / (maxValue - minValue);
        }

        public static Percent Average(params Percent[] input)
        {
            float[] average = new float[input.Length];
            for (int i = 0; i < input.Length; i++)
            {
                average[i] = input[i].value;
            }
            return new(Math.Average(average));
        }
        public static Percent Max(params Percent[] input)
        {
            float[] max = new float[input.Length];
            for (int i = 0; i < input.Length; i++)
            {
                max[i] = input[i].value;
            }
            return new(Math.Max(max));
        }
        public static Percent Min(params Percent[] input)
        {
            float[] min = new float[input.Length];
            for (int i = 0; i < input.Length; i++)
            {
                min[i] = input[i].value;
            }
            return new(Math.Min(min));
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
        public bool Equals(float other)
        {
            return value == other || value == (other / 100);
        }
        public bool Equals(Percent other)
        {
            return value == other.value;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public override string ToString()
        {
            return value.ToString() + "%";
        }
        public string ToString(string format)
        {
            return value.ToString(format) + "%";
        }

        public static Percent operator +(Percent a, Percent b)
        {
            return new Percent { value = a.value + b.value };
        }
        public static Percent operator +(Percent a, float b)
        {
            return new Percent { value = a.value + (b / 100) };
        }
        public static Percent operator +(float a, Percent b)
        {
            return new Percent { value = (a / 100) + b.value };
        }
        public static Percent operator -(Percent a, Percent b)
        {
            return new Percent { value = a.value - b.value };
        }
        public static Percent operator -(Percent a, float b)
        {
            return new Percent { value = a.value - (b / 100) };
        }
        public static Percent operator -(float a, Percent b)
        {
            return new Percent { value = (a / 100) + b.value };
        }
        public static Percent operator *(Percent a, Percent b)
        {
            return new Percent { value = a.value * b.value };
        }
        public static Percent operator *(Percent a, float b)
        {
            return new Percent { value = a.value * (b / 100) };
        }
        public static Percent operator *(float a, Percent b)
        {
            return new Percent { value = (a / 100) + b.value };
        }
        public static Percent operator /(Percent a, Percent b)
        {
            return new Percent { value = a.value / b.value };
        }
        public static Percent operator /(Percent a, float b)
        {
            return new Percent { value = a.value / b / 100 };
        }
        public static Percent operator /(float a, Percent b)
        {
            return new Percent { value = (a / 100) + b.value };
        }
        public static bool operator ==(Percent a, Percent b)
        {
            return a.Equals(b);
        }
        public static bool operator ==(Percent a, float b)
        {
            return a.Equals(b);
        }
        public static bool operator ==(float a, Percent b)
        {
            return b.Equals(a);
        }
        public static bool operator !=(Percent a, Percent b)
        {
            return !a.Equals(b);
        }
        public static bool operator !=(Percent a, float b)
        {
            return !a.Equals(b);
        }
        public static bool operator !=(float a, Percent b)
        {
            return !b.Equals(a);
        }
        public static bool operator >(Percent a, Percent b)
        {
            return a.value > b.value;
        }
        public static bool operator >(Percent a, float b)
        {
            return a.value > b;
        }
        public static bool operator >(float a, Percent b)
        {
            return a > b.value;
        }
        public static bool operator <(Percent a, Percent b)
        {
            return a.value < b.value;
        }
        public static bool operator <(Percent a, float b)
        {
            return a.value < b;
        }
        public static bool operator <(float a, Percent b)
        {
            return a < b.value;
        }
        public static bool operator >=(Percent a, Percent b)
        {
            return a.value > b.value || a.Equals(b);
        }
        public static bool operator >=(Percent a, float b)
        {
            return a.value > b || a.Equals(b);
        }
        public static bool operator >=(float a, Percent b)
        {
            return a > b.value || b.Equals(a);
        }
        public static bool operator <=(Percent a, Percent b)
        {
            return a.value < b.value || a.Equals(b);
        }
        public static bool operator <=(Percent a, float b)
        {
            return a.value < b || a.Equals(b);
        }
        public static bool operator <=(float a, Percent b)
        {
            return a < b.value || b.Equals(a);
        }

        public static explicit operator float(Percent input)
        {
            return input.value;
        }
        public static explicit operator Percent(float input)
        {
            return new Percent(input);
        }
    }

    public struct Vector
    {
        public Angle direction;
        public float strength;

        public Vector Inverse
        {
            get
            {
                return new(direction.value - 180, -strength);
            }
        }
        public Vector Reflected
        {
            get
            {
                return new(360 - direction, strength);
            }
        }

        public static Vector Zero
        {
            get
            {
                return new Vector(0, 0);
            }
        }

        public Vector(Angle direction, float strength, bool clampDir = true)
        {
            if (clampDir) direction = direction.Clamped;
            this.direction = direction;
            this.strength = strength;
        }
        public Vector(float direction, float strength, bool clampDir = true)
        {
            this = new Vector(new Angle(direction), strength, clampDir);
        }

        public static Vector Average(params Vector[] input)
        {
            float[] averageD = new float[input.Length];
            float[] averageS = new float[input.Length];
            for (int i = 0; i < input.Length; i++)
            {
                averageD[i] = input[i].direction.Clamped.value;
                averageS[i] = input[i].strength;
            }
            return new(Math.Average(averageD), Math.Average(averageS));
        }
        public static Vector Max(params Vector[] input)
        {
            float[] maxD = new float[input.Length];
            float[] maxS = new float[input.Length];
            for (int i = 0; i < input.Length; i++)
            {
                maxD[i] = input[i].direction.Clamped.value;
                maxS[i] = input[i].strength;
            }
            return new(Math.Max(maxD), Math.Max(maxS));
        }
        public static Vector Min(params Vector[] input)
        {
            float[] minD = new float[input.Length];
            float[] minS = new float[input.Length];
            for (int i = 0; i < input.Length; i++)
            {
                minD[i] = input[i].direction.Clamped.value;
                minS[i] = input[i].strength;
            }
            return new(Math.Min(minD), Math.Min(minS));
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
        public bool Equals(Vector other)
        {
            return direction == other.direction && strength == other.strength;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public override string ToString()
        {
            return "D: " + direction.ToString() + " | S: " + strength.ToString();
        }
        public string ToString(string format)
        {
            return "D: " + direction.ToString(format) + " | S: " + strength.ToString(format);
        }

        public static Vector operator +(Vector a, Vector b)
        {
            return new Vector(a.direction + b.direction, a.strength + b.strength, false);
        }
        public static Vector operator -(Vector a, Vector b)
        {
            return new Vector(a.direction - b.direction, a.strength - b.strength, false);
        }
        public static Vector operator *(Vector a, Vector b)
        {
            return new Vector(a.direction * b.direction, a.strength * b.strength, false);
        }
        public static Vector operator /(Vector a, Vector b)
        {
            return new Vector(a.direction / b.direction, a.strength / b.strength, false);
        }
        public static bool operator ==(Vector a, Vector b)
        {
            return a.Equals(b);
        }
        public static bool operator !=(Vector a, Vector b)
        {
            return !a.Equals(b);
        }
    }

    public struct Vector2
    {
        public float x, y;

        public static Vector2 NegativeInfinity
        {
            get
            {
                return new Vector2(float.NegativeInfinity, float.NegativeInfinity);
            }
        }
        public static Vector2 One
        {
            get
            {
                return new Vector2(1, 1);
            }
        }
        public static Vector2 PositiveInfinity
        {
            get
            {
                return new Vector2(float.PositiveInfinity, float.PositiveInfinity);
            }
        }
        public static Vector2 Zero
        {
            get
            {
                return new Vector2(0, 0);
            }
        }

        public Vector2(float x)
        {
            this = new Vector2(x, 0);
        }
        public Vector2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public static Vector2 Average(params Vector2[] input)
        {
            float[] averageX = new float[input.Length];
            float[] averageY = new float[input.Length];
            for (int i = 0; i < input.Length; i++)
            {
                averageX[i] = input[i].x;
                averageY[i] = input[i].y;
            }
            return new(Math.Average(averageX), Math.Average(averageY));
        }
        public static Vector2 Max(params Vector2[] input)
        {
            float[] maxX = new float[input.Length];
            float[] maxY = new float[input.Length];
            for (int i = 0; i < input.Length; i++)
            {
                maxX[i] = input[i].x;
                maxY[i] = input[i].y;
            }
            return new(Math.Max(maxX), Math.Max(maxY));
        }
        public static Vector2 Min(params Vector2[] input)
        {
            float[] minX = new float[input.Length];
            float[] minY = new float[input.Length];
            for (int i = 0; i < input.Length; i++)
            {
                minX[i] = input[i].x;
                minY[i] = input[i].y;
            }
            return new(Math.Min(minX), Math.Min(minY));
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
        public bool Equals(Vector2 other)
        {
            return x == other.x && y == other.y;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public override string ToString()
        {
            return "X: " + x.ToString() + " | Y: " + y.ToString();
        }
        public string ToString(string format)
        {
            return "X: " + x.ToString(format) + " | Y: " + y.ToString(format);
        }

        public static Vector2 operator +(Vector2 a, Vector2 b)
        {
            return new Vector2(a.x + b.x, a.y + b.y);
        }
        public static Vector2 operator +(Vector2 a, float b)
        {
            return new Vector2(a.x + b, a.y + b);
        }
        public static Vector2 operator +(float a, Vector2 b)
        {
            return new Vector2(a + b.x, a + b.y);
        }
        public static Vector2 operator -(Vector2 a, Vector2 b)
        {
            return new Vector2(a.x - b.x, a.y - b.y);
        }
        public static Vector2 operator -(Vector2 a, float b)
        {
            return new Vector2(a.x - b, a.y - b);
        }
        public static Vector2 operator -(float a, Vector2 b)
        {
            return new Vector2(a - b.x, a - b.y);
        }
        public static Vector2 operator *(Vector2 a, Vector2 b)
        {
            return new Vector2(a.x * b.x, a.y * b.y);
        }
        public static Vector2 operator *(Vector2 a, float b)
        {
            return new Vector2(a.x * b, a.y * b);
        }
        public static Vector2 operator *(float a, Vector2 b)
        {
            return new Vector2(a * b.x, a * b.y);
        }
        public static Vector2 operator /(Vector2 a, Vector2 b)
        {
            return new Vector2(a.x / b.x, a.y / b.y);
        }
        public static Vector2 operator /(Vector2 a, float b)
        {
            return new Vector2(a.x / b, a.y / b);
        }
        public static Vector2 operator /(float a, Vector2 b)
        {
            return new Vector2(a / b.x, a / b.y);
        }
        public static bool operator ==(Vector2 a, Vector2 b)
        {
            return a.Equals(b);
        }
        public static bool operator !=(Vector2 a, Vector2 b)
        {
            return !a.Equals(b);
        }
        public static bool operator >(Vector2 a, Vector2 b)
        {
            return a.x > b.x && a.y > b.y;
        }
        public static bool operator <(Vector2 a, Vector2 b)
        {
            return a.x < b.x && a.y < b.y;
        }
        public static bool operator >=(Vector2 a, Vector2 b)
        {
            return (a.x > b.x && a.y > b.y) || a.Equals(b);
        }
        public static bool operator <=(Vector2 a, Vector2 b)
        {
            return (a.x < b.x && a.y < b.y) || a.Equals(b);
        }

        public static explicit operator Vector2(Vector3 input)
        {
            return new Vector2(input.x, input.y);
        }
        public static explicit operator Vector2(Vector4 input)
        {
            return new Vector2(input.x, input.y);
        }
    }
    public struct Vector3
    {
        public float x, y, z;

        public static Vector3 NegativeInfinity
        {
            get
            {
                return new Vector3(float.NegativeInfinity, float.NegativeInfinity, float.NegativeInfinity);
            }
        }
        public static Vector3 One
        {
            get
            {
                return new Vector3(1, 1, 1);
            }
        }
        public static Vector3 PositiveInfinity
        {
            get
            {
                return new Vector3(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity);
            }
        }
        public static Vector3 Zero
        {
            get
            {
                return new Vector3(0, 0, 0);
            }
        }

        public Vector3(float x)
        {
            this = new Vector3(x, 0, 0);
        }
        public Vector3(float x, float y)
        {
            this = new Vector3(x, y, 0);
        }
        public Vector3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public static Vector3 Average(params Vector3[] input)
        {
            float[] averageX = new float[input.Length];
            float[] averageY = new float[input.Length];
            float[] averageZ = new float[input.Length];
            for (int i = 0; i < input.Length; i++)
            {
                averageX[i] = input[i].x;
                averageY[i] = input[i].y;
                averageZ[i] = input[i].z;
            }
            return new(Math.Average(averageX), Math.Average(averageY), Math.Average(averageZ));
        }
        public static Vector3 Max(params Vector3[] input)
        {
            float[] maxX = new float[input.Length];
            float[] maxY = new float[input.Length];
            float[] maxZ = new float[input.Length];
            for (int i = 0; i < input.Length; i++)
            {
                maxX[i] = input[i].x;
                maxY[i] = input[i].y;
                maxZ[i] = input[i].z;
            }
            return new(Math.Max(maxX), Math.Max(maxY), Math.Max(maxZ));
        }
        public static Vector3 Min(params Vector3[] input)
        {
            float[] minX = new float[input.Length];
            float[] minY = new float[input.Length];
            float[] minZ = new float[input.Length];
            for (int i = 0; i < input.Length; i++)
            {
                minX[i] = input[i].x;
                minY[i] = input[i].y;
                minZ[i] = input[i].z;
            }
            return new(Math.Min(minX), Math.Min(minY), Math.Min(minZ));
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
        public bool Equals(Vector3 other)
        {
            return x == other.x && y == other.y && z == other.z;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public override string ToString()
        {
            return "X: " + x.ToString() + " | Y: " + y.ToString() + " | Z:" + z.ToString();
        }
        public string ToString(string format)
        {
            return "X: " + x.ToString(format) + " | Y: " + y.ToString(format) + " | Z:" + z.ToString(format);
        }

        public static Vector3 operator +(Vector3 a, Vector3 b)
        {
            return new Vector3(a.x + b.x, a.y + b.y, a.z + b.z);
        }
        public static Vector3 operator +(Vector3 a, float b)
        {
            return new Vector3(a.x + b, a.y + b, a.z + b);
        }
        public static Vector3 operator +(float a, Vector3 b)
        {
            return new Vector3(a + b.x, a + b.y, a + b.z);
        }
        public static Vector3 operator -(Vector3 a, Vector3 b)
        {
            return new Vector3(a.x - b.x, a.y - b.y, a.z - b.z);
        }
        public static Vector3 operator -(Vector3 a, float b)
        {
            return new Vector3(a.x - b, a.y - b, a.z - b);
        }
        public static Vector3 operator -(float a, Vector3 b)
        {
            return new Vector3(a - b.x, a - b.y, a - b.z);
        }
        public static Vector3 operator *(Vector3 a, Vector3 b)
        {
            return new Vector3(a.x * b.x, a.y * b.y, a.z * b.z);
        }
        public static Vector3 operator *(Vector3 a, float b)
        {
            return new Vector3(a.x * b, a.y * b, a.z * b);
        }
        public static Vector3 operator *(float a, Vector3 b)
        {
            return new Vector3(a * b.x, a * b.y, a * b.z);
        }
        public static Vector3 operator /(Vector3 a, Vector3 b)
        {
            return new Vector3(a.x / b.x, a.y / b.y, a.z / b.z);
        }
        public static Vector3 operator /(Vector3 a, float b)
        {
            return new Vector3(a.x / b, a.y / b, a.z / b);
        }
        public static Vector3 operator /(float a, Vector3 b)
        {
            return new Vector3(a / b.x, a / b.y, a / b.z);
        }
        public static bool operator ==(Vector3 a, Vector3 b)
        {
            return a.Equals(b);
        }
        public static bool operator !=(Vector3 a, Vector3 b)
        {
            return !a.Equals(b);
        }
        public static bool operator >(Vector3 a, Vector3 b)
        {
            return a.x > b.x && a.y > b.y && a.z > b.z;
        }
        public static bool operator <(Vector3 a, Vector3 b)
        {
            return a.x < b.x && a.y < b.y && a.z < b.z;
        }
        public static bool operator >=(Vector3 a, Vector3 b)
        {
            return (a.x > b.x && a.y > b.y && a.z > b.z) || a.Equals(b);
        }
        public static bool operator <=(Vector3 a, Vector3 b)
        {
            return (a.x < b.x && a.y < b.y && a.z < b.z) || a.Equals(b);
        }

        public static implicit operator Vector3(Color input)
        {
            return new Vector3(input.r, input.g, input.b);
        }
        public static implicit operator Vector3(Vector2 input)
        {
            return new Vector3(input.x, input.y);
        }
        public static explicit operator Vector3(Vector4 input)
        {
            return new Vector3(input.x, input.y, input.z);
        }
    }
    public struct Vector4
    {
        public float x, y, z, w;

        public static Vector4 NegativeInfinity
        {
            get
            {
                return new Vector4(float.NegativeInfinity, float.NegativeInfinity, float.NegativeInfinity, float.NegativeInfinity);
            }
        }
        public static Vector4 One
        {
            get
            {
                return new Vector4(1, 1, 1, 1);
            }
        }
        public static Vector4 PositiveInfinity
        {
            get
            {
                return new Vector4(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity);
            }
        }
        public static Vector4 Zero
        {
            get
            {
                return new Vector4(0, 0, 0, 0);
            }
        }

        public Vector4(float x)
        {
            this = new Vector4(x, 0, 0, 0);
        }
        public Vector4(float x, float y)
        {
            this = new Vector4(x, y, 0, 0);
        }
        public Vector4(float x, float y, float z)
        {
            this = new Vector4(x, y, z, 0);
        }
        public Vector4(float x, float y, float z, float w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        public static Vector4 Average(params Vector4[] input)
        {
            float[] averageX = new float[input.Length];
            float[] averageY = new float[input.Length];
            float[] averageZ = new float[input.Length];
            float[] averageW = new float[input.Length];
            for (int i = 0; i < input.Length; i++)
            {
                averageX[i] = input[i].x;
                averageY[i] = input[i].y;
                averageZ[i] = input[i].z;
                averageW[i] = input[i].w;
            }
            return new(Math.Average(averageX), Math.Average(averageY), Math.Average(averageZ), Math.Average(averageW));
        }
        public static Vector4 Max(params Vector4[] input)
        {
            float[] maxX = new float[input.Length];
            float[] maxY = new float[input.Length];
            float[] maxZ = new float[input.Length];
            float[] maxW = new float[input.Length];
            for (int i = 0; i < input.Length; i++)
            {
                maxX[i] = input[i].x;
                maxY[i] = input[i].y;
                maxZ[i] = input[i].z;
                maxW[i] = input[i].w;
            }
            return new(Math.Max(maxX), Math.Max(maxY), Math.Max(maxZ), Math.Max(maxW));
        }
        public static Vector4 Min(params Vector4[] input)
        {
            float[] minX = new float[input.Length];
            float[] minY = new float[input.Length];
            float[] minZ = new float[input.Length];
            float[] minW = new float[input.Length];
            for (int i = 0; i < input.Length; i++)
            {
                minX[i] = input[i].x;
                minY[i] = input[i].y;
                minZ[i] = input[i].z;
                minW[i] = input[i].w;
            }
            return new(Math.Min(minX), Math.Min(minY), Math.Min(minZ), Math.Min(minW));
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
        public bool Equals(Vector4 other)
        {
            return x == other.x && y == other.y && z == other.z && w == other.w;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public override string ToString()
        {
            return "X: " + x.ToString() + " | Y: " + y.ToString() + " | Z: " + z.ToString() + " | W: " + w.ToString();
        }
        public string ToString(string format)
        {
            return "X: " + x.ToString(format) + " | Y: " + y.ToString(format) + " | Z: " + z.ToString(format) + " | W: " + w.ToString(format);
        }

        public static Vector4 operator +(Vector4 a, Vector4 b)
        {
            return new Vector4(a.x + b.x, a.y + b.y, a.z + b.z, a.w + b.w);
        }
        public static Vector4 operator +(Vector4 a, float b)
        {
            return new Vector4(a.x + b, a.y + b, a.z + b, a.w + b);
        }
        public static Vector4 operator +(float a, Vector4 b)
        {
            return new Vector4(a + b.x, a + b.y, a + b.z, a + b.w);
        }
        public static Vector4 operator -(Vector4 a, Vector4 b)
        {
            return new Vector4(a.x - b.x, a.y - b.y, a.z - b.z, a.w - b.w);
        }
        public static Vector4 operator -(Vector4 a, float b)
        {
            return new Vector4(a.x - b, a.y - b, a.z - b, a.w - b);
        }
        public static Vector4 operator -(float a, Vector4 b)
        {
            return new Vector4(a - b.x, a - b.y, a - b.z, a - b.w);
        }
        public static Vector4 operator *(Vector4 a, Vector4 b)
        {
            return new Vector4(a.x * b.x, a.y * b.y, a.z * b.z, a.w * b.w);
        }
        public static Vector4 operator *(Vector4 a, float b)
        {
            return new Vector4(a.x * b, a.y * b, a.z * b, a.w * b);
        }
        public static Vector4 operator *(float a, Vector4 b)
        {
            return new Vector4(a * b.x, a * b.y, a * b.z, a * b.w);
        }
        public static Vector4 operator /(Vector4 a, Vector4 b)
        {
            return new Vector4(a.x / b.x, a.y / b.y, a.z / b.z, a.w / b.w);
        }
        public static Vector4 operator /(Vector4 a, float b)
        {
            return new Vector4(a.x / b, a.y / b, a.z / b, a.w / b);
        }
        public static Vector4 operator /(float a, Vector4 b)
        {
            return new Vector4(a / b.x, a / b.y, a / b.z, a / b.w);
        }
        public static bool operator ==(Vector4 a, Vector4 b)
        {
            return a.Equals(b);
        }
        public static bool operator !=(Vector4 a, Vector4 b)
        {
            return !a.Equals(b);
        }
        public static bool operator >(Vector4 a, Vector4 b)
        {
            return a.x > b.x && a.y > b.y && a.z > b.z && a.w > b.w;
        }
        public static bool operator <(Vector4 a, Vector4 b)
        {
            return a.x < b.x && a.y < b.y && a.z < b.z && a.w < b.w;
        }
        public static bool operator >=(Vector4 a, Vector4 b)
        {
            return (a.x > b.x && a.y > b.y && a.z > b.z && a.w > b.w) || a.Equals(b);
        }
        public static bool operator <=(Vector4 a, Vector4 b)
        {
            return (a.x < b.x && a.y < b.y && a.z < b.z && a.w < b.w) || a.Equals(b);
        }

        public static implicit operator Vector4(Color input)
        {
            return new Vector4(input.r, input.g, input.b, input.a);
        }
        public static implicit operator Vector4(Vector2 input)
        {
            return new Vector4(input.x, input.y);
        }
        public static implicit operator Vector4(Vector3 input)
        {
            return new Vector4(input.x, input.y, input.z);
        }
    }
}