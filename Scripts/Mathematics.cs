using Nerd_STF.Mathematics.Interfaces;
using System;

namespace Nerd_STF.Mathematics
{
    [Serializable]
    public struct Angle : IMathFunctions<Angle>, INegatives<Angle>
    {
        public float value;

        public Angle Clamped
        {
            get
            {
                Angle returned = this;
                while (returned.value >= 360) returned.value -= 360;
                while (returned.value < 0) returned.value += 360;
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
        public bool IsAcute => Clamped.value > 0 && Clamped.value < 90; 
        public bool IsClamped => value < 360 && value >= 0; 
        public bool IsNegative => value < 0; 
        public bool IsObtuse => Clamped.value > 90 && Clamped.value < 180; 
        public bool IsReflex => Clamped.value > 180 && Clamped.value < 360; 
        public bool IsRight => Clamped.value == 90; 
        public bool IsStraight => Clamped.value == 180; 
        public bool IsZero => Clamped.value == 0; 
        public Angle Negative
        {
            get
            {
                Angle returned = new(value);
                if (value > 0) returned *= -1;
                return returned;
            }
        }
        public Angle Positive => Absolute; 
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

        public Angle(float degree) => value = degree;

        public static Angle Acute => new(45);
        public static Angle Obtuse => new(135);
        public static Angle Reflex => new(270);
        public static Angle Right => new(90);
        public static Angle Straight => new(180);
        public static Angle Zero => new(0);

        public override bool Equals(object obj) => base.Equals(obj);
        public bool Equals(Angle other) => value == other.value;
        public bool Equals(float other) => value == other;
        public override int GetHashCode() => base.GetHashCode();
        public override string ToString() => value.ToString() + "°";
        public string ToString(string format) => value.ToString(format) + "°";

        public Angle Average(params Angle[] objs)
        {
            float[] average = new float[objs.Length + 1];
            for (int i = 0; i < objs.Length; i++) average[i] = objs[i].value;
            average[objs.Length] = value;
            return new(Math.Average(average));
        }
        public Angle Max(params Angle[] objs)
        {
            float[] max = new float[objs.Length + 1];
            for (int i = 0; i < objs.Length; i++) max[i] = objs[i].value;
            max[objs.Length] = value;
            return new(Math.Max(max));
        }
        public Angle Min(params Angle[] objs)
        {
            float[] min = new float[objs.Length + 1];
            for (int i = 0; i < objs.Length; i++) min[i] = objs[i].value;
            min[objs.Length] = value;
            return new(Math.Min(min));
        }

        public static Angle operator +(Angle a, Angle b) => new(a.value + b.value);
        public static Angle operator +(Angle a, float b) => new(a.value + b);
        public static Angle operator +(float a, Angle b) => new(a + b.value);
        public static Angle operator -(Angle a, Angle b) => new(a.value - b.value);
        public static Angle operator -(Angle a, float b) => new(a.value - b);
        public static Angle operator -(float a, Angle b) => new(a - b.value);
        public static Angle operator *(Angle a, Angle b) => new(a.value * b.value);
        public static Angle operator *(Angle a, float b) => new(a.value * b);
        public static Angle operator *(float a, Angle b) => new(a * b.value);
        public static Angle operator /(Angle a, Angle b) => new(a.value / b.value);
        public static Angle operator /(Angle a, float b) => new(a.value / b);
        public static Angle operator /(float a, Angle b) => new(a / b.value);
        public static bool operator ==(Angle a, Angle b) => a.Equals(b);
        public static bool operator ==(Angle a, float b) => a.Equals(b);
        public static bool operator ==(float a, Angle b) => b.Equals(a);
        public static bool operator !=(Angle a, Angle b) => !a.Equals(b);
        public static bool operator !=(Angle a, float b) => !a.Equals(b);
        public static bool operator !=(float a, Angle b) => !b.Equals(a);
        public static bool operator >(Angle a, Angle b) => a.value > b.value;
        public static bool operator >(Angle a, float b) => a.value > b;
        public static bool operator >(float a, Angle b) => a > b.value;
        public static bool operator <(Angle a, Angle b) => a.value < b.value;
        public static bool operator <(Angle a, float b) => a.value < b;
        public static bool operator <(float a, Angle b) => a < b.value;
        public static bool operator >=(Angle a, Angle b) => a.value > b.value || a.Equals(b);
        public static bool operator >=(Angle a, float b) => a.value > b || a.Equals(b);
        public static bool operator >=(float a, Angle b) => a > b.value || b.Equals(a);
        public static bool operator <=(Angle a, Angle b) => a.value < b.value || a.Equals(b);
        public static bool operator <=(Angle a, float b) => a.value < b || a.Equals(b);
        public static bool operator <=(float a, Angle b) => a < b.value || b.Equals(a);

        public static explicit operator float(Angle input) => input.value;
        public static explicit operator Angle(float input) => new(input);

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

    [Serializable]
    public struct Color : IMathFunctions<Color>
    {
        public float r, g, b, a;

        public bool IsBlue => b != 0;
        public bool IsClear => a == 0;
        public bool IsGreen => g != 0;
        public bool IsRed => a != 0;
        public bool IsTransparent => a > 0 && a < 1;
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

        public static Color Black => new(0, 0, 0);
        public static Color Blue => new(0, 0, 1);
        public static Color Clear => new(0, 0, 0, 0);
        public static Color Cyan => new(0, 1, 1);
        public static Color Gray => new(0.5f, 0.5f, 0.5f);
        public static Color Green => new(0, 1, 0);
        public static Color Magenta => new(1, 0, 1);
        public static Color Orange => new(1, 0.5f, 0);
        public static Color Purple => new(0.5f, 0, 1);
        public static Color Red => new(1, 0, 0);
        public static Color White => new(1, 1, 1);
        public static Color Yellow => new(1, 1, 0);

        public Color(float r, float g, float b)
        {
            this = new(r, g, b, 1);
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

        public Color Average(params Color[] objs)
        {
            float[] averageR = new float[objs.Length + 1];
            float[] averageG = new float[objs.Length + 1];
            float[] averageB = new float[objs.Length + 1];
            float[] averageA = new float[objs.Length + 1];
            for (int i = 0; i < objs.Length; i++)
            {
                averageR[i] = objs[i].r;
                averageG[i] = objs[i].g;
                averageB[i] = objs[i].b;
                averageA[i] = objs[i].a;
            }
            averageR[objs.Length] = r;
            averageG[objs.Length] = g;
            averageB[objs.Length] = b;
            averageA[objs.Length] = a;
            return new(Math.Average(averageR), Math.Average(averageG), Math.Average(averageB), Math.Average(averageA));
        }
        public Color Max(params Color[] objs)
        {
            float[] maxR = new float[objs.Length + 1];
            float[] maxG = new float[objs.Length + 1];
            float[] maxB = new float[objs.Length + 1];
            float[] maxA = new float[objs.Length + 1];
            for (int i = 0; i < objs.Length; i++)
            {
                maxR[i] = objs[i].r;
                maxG[i] = objs[i].g;
                maxB[i] = objs[i].b;
                maxA[i] = objs[i].a;
            }
            maxR[objs.Length] = r;
            maxG[objs.Length] = g;
            maxB[objs.Length] = b;
            maxA[objs.Length] = a;
            return new(Math.Max(maxR), Math.Max(maxG), Math.Max(maxB), Math.Max(maxA));
        }
        public Color Min(params Color[] objs)
        {
            float[] minR = new float[objs.Length + 1];
            float[] minG = new float[objs.Length + 1];
            float[] minB = new float[objs.Length + 1];
            float[] minA = new float[objs.Length + 1];
            for (int i = 0; i < objs.Length; i++)
            {
                minR[i] = objs[i].r;
                minG[i] = objs[i].g;
                minB[i] = objs[i].b;
                minA[i] = objs[i].a;
            }
            minR[objs.Length] = r;
            minG[objs.Length] = g;
            minB[objs.Length] = b;
            minA[objs.Length] = a;
            return new(Math.Min(minR), Math.Min(minG), Math.Min(minB), Math.Min(minA));
        }

        public override bool Equals(object obj) => base.Equals(obj);
        public bool Equals(Color other) => r == other.r && g == other.g && b == other.b && a == other.a;
        public override int GetHashCode() => base.GetHashCode();
        public override string ToString() => "R: " + r.ToString() + " | G: " + g.ToString() + " | B: " + b.ToString() + " | A: " + a.ToString();
        public string ToString(string format) => "R: " + r.ToString(format) + " | G: " + g.ToString(format) + " | B: " + b.ToString(format) + " | A: " + a.ToString(format);

        public static Color operator +(Color a, Color b) => new(a.r + b.r, a.g + b.g, a.b + b.b, a.a + b.a);
        public static Color operator +(Color a, float b) => new(a.r + b, a.g + b, a.b + b, a.a + b);
        public static Color operator +(float a, Color b) => new(a + b.r, a + b.g, a + b.b, a + b.a);
        public static Color operator -(Color a, Color b) => new(a.r - b.r, a.g - b.g, a.b - b.b, a.a - b.a);
        public static Color operator -(Color a, float b) => new(a.r - b, a.g - b, a.b - b, a.a - b);
        public static Color operator -(float a, Color b) => new(a - b.r, a - b.g, a - b.b, a - b.a);
        public static Color operator *(Color a, Color b) => new(a.r * b.r, a.g * b.g, a.b * b.b, a.a * b.a);
        public static Color operator *(Color a, float b) => new(a.r * b, a.g * b, a.b * b, a.a * b);
        public static Color operator *(float a, Color b) => new(a * b.r, a * b.g, a * b.b, a * b.a);
        public static Color operator /(Color a, Color b) => new(a.r / b.r, a.g / b.g, a.b / b.b, a.a / b.a);
        public static Color operator /(Color a, float b) => new(a.r / b, a.g / b, a.b / b, a.a / b);
        public static Color operator /(float a, Color b) => new(a / b.r, a / b.g, a / b.b, a / b.a);
        public static bool operator ==(Color a, Color b) => a.Equals(b);
        public static bool operator !=(Color a, Color b) => !a.Equals(b);

        public static implicit operator Color(ColorByte input) => new(input.r / 255, input.g / 255, input.b / 255, input.a / 255);
        public static explicit operator Color(Vector3 input) => new(input.x, input.y, input.z, 1);
        public static explicit operator Color(Vector4 input) => new(input.x, input.y, input.z, input.w);
    }
    [Serializable]
    public struct ColorByte : IMathFunctions<ColorByte>
    {
        public byte r, g, b, a;

        public bool IsBlue => b != byte.MinValue;
        public bool IsClear => a == byte.MinValue;
        public bool IsGreen => g != byte.MinValue;
        public bool IsRed => a != byte.MinValue;
        public bool IsTransparent => a > byte.MinValue && a < byte.MaxValue;

        public static ColorByte Black => new(0, 0, 0);
        public static ColorByte Blue => new(0, 0, 255);
        public static ColorByte Clear => new(0, 0, 0, 0);
        public static ColorByte Cyan => new(0, 255, 255);
        public static ColorByte Gray => new(128, 128, 128);
        public static ColorByte Green => new(0, 255, 0);
        public static ColorByte Magenta => new(255, 0, 255);
        public static ColorByte Orange => new(1, 128, 0);
        public static ColorByte Purple => new(128, 0, 1);
        public static ColorByte Red => new(255, 0, 0);
        public static ColorByte White => new(255, 255, 255);
        public static ColorByte Yellow => new(255, 255, 0);

        public ColorByte(byte r, byte g, byte b) => this = new(r, g, b, byte.MaxValue);
        public ColorByte(byte r, byte g, byte b, byte a)
        {
            this.r = r;
            this.g = g;
            this.b = b;
            this.a = a;
        }
        public ColorByte(int r, int g, int b) => this = new(r, g, b, byte.MaxValue);
        public ColorByte(int r, int g, int b, int a)
        {
            this.r = (byte)r;
            this.g = (byte)g;
            this.b = (byte)b;
            this.a = (byte)a;
        }

        public ColorByte Average(params ColorByte[] objs)
        {
            float[] averageR = new float[objs.Length + 1];
            float[] averageG = new float[objs.Length + 1];
            float[] averageB = new float[objs.Length + 1];
            float[] averageA = new float[objs.Length + 1];
            for (int i = 0; i < objs.Length; i++)
            {
                averageR[i] = objs[i].r;
                averageG[i] = objs[i].g;
                averageB[i] = objs[i].b;
                averageA[i] = objs[i].a;
            }
            averageR[objs.Length] = r;
            averageG[objs.Length] = g;
            averageB[objs.Length] = b;
            averageA[objs.Length] = a;
            return new(Math.RoundToInt(Math.Average(averageR)), Math.RoundToInt(Math.Average(averageG)), Math.RoundToInt(Math.Average(averageB)), Math.RoundToInt(Math.Average(averageA)));
        }
        public ColorByte Max(params ColorByte[] objs)
        {
            float[] maxR = new float[objs.Length + 1];
            float[] maxG = new float[objs.Length + 1];
            float[] maxB = new float[objs.Length + 1];
            float[] maxA = new float[objs.Length + 1];
            for (int i = 0; i < objs.Length; i++)
            {
                maxR[i] = objs[i].r;
                maxG[i] = objs[i].g;
                maxB[i] = objs[i].b;
                maxA[i] = objs[i].a;
            }
            maxR[objs.Length] = r;
            maxG[objs.Length] = g;
            maxB[objs.Length] = b;
            maxA[objs.Length] = a;
            return new(Math.RoundToInt(Math.Max(maxR)), Math.RoundToInt(Math.Max(maxG)), Math.RoundToInt(Math.Max(maxB)), Math.RoundToInt(Math.Max(maxA)));
        }
        public ColorByte Min(params ColorByte[] objs)
        {
            float[] minR = new float[objs.Length + 1];
            float[] minG = new float[objs.Length + 1];
            float[] minB = new float[objs.Length + 1];
            float[] minA = new float[objs.Length + 1];
            for (int i = 0; i < objs.Length; i++)
            {
                minR[i] = objs[i].r;
                minG[i] = objs[i].g;
                minB[i] = objs[i].b;
                minA[i] = objs[i].a;
            }
            minR[objs.Length] = r;
            minG[objs.Length] = g;
            minB[objs.Length] = b;
            minA[objs.Length] = a;
            return new(Math.RoundToInt(Math.Min(minR)), Math.RoundToInt(Math.Min(minG)), Math.RoundToInt(Math.Min(minB)), Math.RoundToInt(Math.Min(minA)));
        }

        public override bool Equals(object obj) => base.Equals(obj);
        public bool Equals(ColorByte other) => r == other.r && g == other.g && b == other.b && a == other.a;
        public override int GetHashCode() => base.GetHashCode();
        public override string ToString() => "R: " + r.ToString() + " | G: " + g.ToString() + " | B: " + b.ToString() + " | A: " + a.ToString();
        public string ToString(string format) => "R: " + r.ToString(format) + " | G: " + g.ToString(format) + " | B: " + b.ToString(format) + " | A: " + a.ToString(format);

        public static ColorByte operator +(ColorByte a, ColorByte b) => new(a.r + b.r, a.g + b.g, a.b + b.b, a.a + b.a);
        public static ColorByte operator +(ColorByte a, byte b) => new(a.r + b, a.g + b, a.b + b, a.a + b);
        public static ColorByte operator +(byte a, ColorByte b) => new(a + b.r, a + b.g, a + b.b, a + b.a);
        public static ColorByte operator -(ColorByte a, ColorByte b) => new(a.r - b.r, a.g - b.g, a.b - b.b, a.a - b.a);
        public static ColorByte operator -(ColorByte a, byte b) => new(a.r - b, a.g - b, a.b - b, a.a - b);
        public static ColorByte operator -(byte a, ColorByte b) => new(a - b.r, a - b.g, a - b.b, a - b.a);
        public static ColorByte operator *(ColorByte a, ColorByte b) => new(a.r * b.r, a.g * b.g, a.b * b.b, a.a * b.a);
        public static ColorByte operator *(ColorByte a, byte b) => new(a.r * b, a.g * b, a.b * b, a.a * b);
        public static ColorByte operator *(byte a, ColorByte b) => new(a * b.r, a * b.g, a * b.b, a * b.a);
        public static ColorByte operator /(ColorByte a, ColorByte b) => new(a.r / b.r, a.g / b.g, a.b / b.b, a.a / b.a);
        public static ColorByte operator /(ColorByte a, byte b) => new(a.r / b, a.g / b, a.b / b, a.a / b);
        public static ColorByte operator /(byte a, ColorByte b) => new(a / b.r, a / b.g, a / b.b, a / b.a);
        public static bool operator ==(ColorByte a, ColorByte b) => a.Equals(b);
        public static bool operator !=(ColorByte a, ColorByte b) => !a.Equals(b);

        public static explicit operator ColorByte(Color input) => new((byte)(input.r * 255), (byte)(input.g * 255), (byte)(input.b * 255), (byte)(input.a * 255));
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
            foreach (float f in input) returned += f;
            return returned;
        }

        public static float Average(params float[] input)
        {
            float returned = 0;
            foreach (float f in input) returned += f;
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
            for (uint i = 1; i < input.Length; i++) returned /= input[i];
            return returned;
        }

        public static float Max(params float[] input)
        {
            float returned = input[0];
            for (uint i = 0; i < input.Length; i++) if (input[i] > returned) returned = input[i];
            return returned;
        }

        public static float Min(params float[] input)
        {
            float returned = input[0];
            for (uint i = 0; i < input.Length; i++) if (input[i] < returned) returned = input[0];
            return returned;
        }

        public static float Multiply(params float[] input)
        {
            float returned = 1;
            foreach (float f in input) returned *= f;
            return returned;
        }

        public static float Power(float input, int power)
        {
            float returned = 1;
            for (uint i = 0; i < Absolute(power); i++) returned *= input;
            if (power < 0) returned = 1 / returned;
            return returned;
        }

        public static float Round(float value)
        {
            if (value % 1 >= 0.5f) value += 1 - (value % 1);
            else value -= value % 1;

            return value;
        }
        public static float Round(float value, float dividend) => Round(value / dividend) * dividend;
        public static int RoundToInt(float value) => (int)Round(value);

        public static float Subtract(params float[] input)
        {
            float returned = input[0];
            for (uint i = 1; i < input.Length; i++) returned -= input[i];
            return returned;
        }

        public static class Formulas
        {
            public static float CircleArea(float radius) => Pi * radius * radius;
            public static float CircleCircum(float radius) => 2 * radius * Pi;
            public static float CircleDiam(float radius) => radius * 2;
            public static float CircleRadius(float circumference) => circumference / Pi / 2;

            public static float Perimeter(params float[] sideLengths) => Add(sideLengths);

            public static float RectangleArea(float length, float width) => length * width;

            public static float SquareArea(float length) => RectangleArea(length, length);
        }
    }

    [Serializable]
    public struct Percent : IMathFunctions<Percent>, INegatives<Percent>
    {
        public float value;

        public Percent Absolute
        {
            get
            {
                Percent returned = new(value);
                if (returned < 0) returned *= -1;
                return returned;
            }
        }
        public bool IsFull { get => value == 100; }
        public bool IsNegative { get => value < 0; }
        public bool IsOverflow { get => value > 100; }
        public bool IsZero { get => value == 0; }
        public Percent Negative
        {
            get
            {
                Percent returned = new(value);
                if (returned > 0) returned *= -1;
                return returned;
            }
        }
        public Percent Positive { get => Absolute; }

        public static Percent Full { get => new(100); }
        public static Percent One { get => new(1); }
        public static Percent Zero { get => new(0); }

        public Percent(float value) => this = new(value, 0, 100);
        public Percent(float value, float maxValue) => this = new(value, 0, maxValue);
        public Percent(float value, float minValue, float maxValue) => this.value = value / (maxValue - minValue);

        public Percent Average(params Percent[] objs)
        {
            float[] average = new float[objs.Length + 1];
            for (int i = 0; i < objs.Length; i++) average[i] = objs[i].value;
            average[objs.Length] = value;
            return new(Math.Average(average));
        }
        public Percent Max(params Percent[] objs)
        {
            float[] max = new float[objs.Length];
            for (int i = 0; i < objs.Length; i++) max[i] = objs[i].value;
            max[objs.Length] = value;
            return new(Math.Max(max));
        }
        public Percent Min(params Percent[] objs)
        {
            float[] min = new float[objs.Length];
            for (int i = 0; i < objs.Length; i++) min[i] = objs[i].value;
            min[objs.Length] = value;
            return new(Math.Min(min));
        }

        public override bool Equals(object obj) => base.Equals(obj);
        public bool Equals(float other) => value == other || value == (other / 100);
        public bool Equals(Percent other) => value == other.value;
        public override int GetHashCode() => base.GetHashCode();
        public override string ToString() => value.ToString() + "%";
        public string ToString(string format) => value.ToString(format) + "%";

        public static Percent operator +(Percent a, Percent b) => new() { value = a.value + b.value };
        public static Percent operator +(Percent a, float b) => new() { value = a.value + (b / 100) };
        public static Percent operator +(float a, Percent b) => new() { value = (a / 100) + b.value };
        public static Percent operator -(Percent a, Percent b) => new() { value = a.value - b.value };
        public static Percent operator -(Percent a, float b) => new() { value = a.value - (b / 100) };
        public static Percent operator -(float a, Percent b) => new() { value = (a / 100) + b.value };
        public static Percent operator *(Percent a, Percent b) => new() { value = a.value * b.value };
        public static Percent operator *(Percent a, float b) => new() { value = a.value * (b / 100) };
        public static Percent operator *(float a, Percent b) => new() { value = (a / 100) + b.value };
        public static Percent operator /(Percent a, Percent b) => new() { value = a.value / b.value };
        public static Percent operator /(Percent a, float b) => new() { value = a.value / b / 100 };
        public static Percent operator /(float a, Percent b) => new() { value = (a / 100) + b.value };
        public static bool operator ==(Percent a, Percent b) => a.Equals(b);
        public static bool operator ==(Percent a, float b) => a.Equals(b);
        public static bool operator ==(float a, Percent b) => b.Equals(a);
        public static bool operator !=(Percent a, Percent b) => !a.Equals(b);
        public static bool operator !=(Percent a, float b) => !a.Equals(b);
        public static bool operator !=(float a, Percent b) => !b.Equals(a);
        public static bool operator >(Percent a, Percent b) => a.value > b.value;
        public static bool operator >(Percent a, float b) => a.value > b;
        public static bool operator >(float a, Percent b) => a > b.value;
        public static bool operator <(Percent a, Percent b) => a.value < b.value;
        public static bool operator <(Percent a, float b) => a.value < b;
        public static bool operator <(float a, Percent b) => a < b.value;
        public static bool operator >=(Percent a, Percent b) => a.value > b.value || a.Equals(b);
        public static bool operator >=(Percent a, float b) => a.value > b || a.Equals(b);
        public static bool operator >=(float a, Percent b) => a > b.value || b.Equals(a);
        public static bool operator <=(Percent a, Percent b) => a.value < b.value || a.Equals(b);
        public static bool operator <=(Percent a, float b) => a.value < b || a.Equals(b);
        public static bool operator <=(float a, Percent b) => a < b.value || b.Equals(a);

        public static explicit operator float(Percent input) => input.value;
        public static explicit operator Percent(float input) => new(input);
    }

    [Serializable]
    public struct Vector : IMathFunctions<Vector>
    {
        public Angle direction;
        public float strength;

        public Vector Inverse { get => new(direction.value - 180, -strength); }
        public Vector Reflected { get => new(360 - direction, strength); }

        public static Vector Zero { get => new(0, 0); }

        public Vector(Angle direction, float strength, bool clampDir = true)
        {
            if (clampDir) direction = direction.Clamped;
            this.direction = direction;
            this.strength = strength;
        }
        public Vector(float direction, float strength, bool clampDir = true) => this = new Vector(new Angle(direction), strength, clampDir);

        public Vector Average(params Vector[] objs)
        {
            float[] averageD = new float[objs.Length + 1];
            float[] averageS = new float[objs.Length + 1];
            for (int i = 0; i < objs.Length; i++)
            {
                averageD[i] = objs[i].direction.Clamped.value;
                averageS[i] = objs[i].strength;
            }
            averageD[objs.Length] = direction.Clamped.value;
            averageS[objs.Length] = strength;
            return new(Math.Average(averageD), Math.Average(averageS));
        }
        public Vector Max(params Vector[] objs)
        {
            float[] maxD = new float[objs.Length + 1];
            float[] maxS = new float[objs.Length + 1];
            for (int i = 0; i < objs.Length; i++)
            {
                maxD[i] = objs[i].direction.Clamped.value;
                maxS[i] = objs[i].strength;
            }
            maxD[objs.Length] = direction.Clamped.value;
            maxS[objs.Length] = strength;
            return new(Math.Max(maxD), Math.Max(maxS));
        }
        public Vector Min(params Vector[] objs)
        {
            float[] minD = new float[objs.Length + 1];
            float[] minS = new float[objs.Length + 1];
            for (int i = 0; i < objs.Length; i++)
            {
                minD[i] = objs[i].direction.Clamped.value;
                minS[i] = objs[i].strength;
            }
            minD[objs.Length] = direction.Clamped.value;
            minS[objs.Length] = strength;
            return new(Math.Min(minD), Math.Min(minS));
        }

        public override bool Equals(object obj) => base.Equals(obj);
        public bool Equals(Vector other) => direction == other.direction && strength == other.strength;
        public override int GetHashCode() => base.GetHashCode();
        public override string ToString() => "D: " + direction.ToString() + " | S: " + strength.ToString();
        public string ToString(string format) => "D: " + direction.ToString(format) + " | S: " + strength.ToString(format);

        public static Vector operator +(Vector a, Vector b) => new(a.direction + b.direction, a.strength + b.strength, false);
        public static Vector operator -(Vector a, Vector b) => new(a.direction - b.direction, a.strength - b.strength, false);
        public static Vector operator *(Vector a, Vector b) => new(a.direction * b.direction, a.strength * b.strength, false);
        public static Vector operator /(Vector a, Vector b) => new(a.direction / b.direction, a.strength / b.strength, false);
        public static bool operator ==(Vector a, Vector b) => a.Equals(b);
        public static bool operator !=(Vector a, Vector b) => !a.Equals(b);
    }

    [Serializable]
    public struct Vector2 : IMathFunctions<Vector2>
    {
        public float x, y;

        public static Vector2 NegativeInfinity { get => new(float.NegativeInfinity, float.NegativeInfinity); }
        public static Vector2 One { get => new(1, 1); }
        public static Vector2 PositiveInfinity { get => new(float.PositiveInfinity, float.PositiveInfinity); }
        public static Vector2 Zero { get => new(0, 0); }

        public Vector2(float x) => this = new Vector2(x, 0);
        public Vector2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public Vector2 Average(params Vector2[] objs)
        {
            float[] averageX = new float[objs.Length + 1];
            float[] averageY = new float[objs.Length + 1];
            for (int i = 0; i < objs.Length; i++)
            {
                averageX[i] = objs[i].x;
                averageY[i] = objs[i].y;
            }
            averageX[objs.Length] = x;
            averageY[objs.Length] = y;
            return new(Math.Average(averageX), Math.Average(averageY));
        }
        public Vector2 Max(params Vector2[] objs)
        {
            float[] maxX = new float[objs.Length + 1];
            float[] maxY = new float[objs.Length + 1];
            for (int i = 0; i < objs.Length; i++)
            {
                maxX[i] = objs[i].x;
                maxY[i] = objs[i].y;
            }
            maxX[objs.Length] = x;
            maxY[objs.Length] = y;
            return new(Math.Max(maxX), Math.Max(maxY));
        }
        public Vector2 Min(params Vector2[] objs)
        {
            float[] minX = new float[objs.Length + 1];
            float[] minY = new float[objs.Length + 1];
            for (int i = 0; i < objs.Length; i++)
            {
                minX[i] = objs[i].x;
                minY[i] = objs[i].y;
            }
            minX[objs.Length] = x;
            minY[objs.Length] = y;
            return new(Math.Min(minX), Math.Min(minY));
        }

        public override bool Equals(object obj) => base.Equals(obj);
        public bool Equals(Vector2 other) => x == other.x && y == other.y;
        public override int GetHashCode() => base.GetHashCode();
        public override string ToString() => "X: " + x.ToString() + " | Y: " + y.ToString();
        public string ToString(string format) => "X: " + x.ToString(format) + " | Y: " + y.ToString(format);

        public static Vector2 operator +(Vector2 a, Vector2 b) => new(a.x + b.x, a.y + b.y);
        public static Vector2 operator +(Vector2 a, float b) => new(a.x + b, a.y + b);
        public static Vector2 operator +(float a, Vector2 b) => new(a + b.x, a + b.y);
        public static Vector2 operator -(Vector2 a, Vector2 b) => new(a.x - b.x, a.y - b.y);
        public static Vector2 operator -(Vector2 a, float b) => new(a.x - b, a.y - b);
        public static Vector2 operator -(float a, Vector2 b) => new(a - b.x, a - b.y);
        public static Vector2 operator *(Vector2 a, Vector2 b) => new(a.x * b.x, a.y * b.y);
        public static Vector2 operator *(Vector2 a, float b) => new(a.x * b, a.y * b);
        public static Vector2 operator *(float a, Vector2 b) => new(a * b.x, a * b.y);
        public static Vector2 operator /(Vector2 a, Vector2 b) => new(a.x / b.x, a.y / b.y);
        public static Vector2 operator /(Vector2 a, float b) => new(a.x / b, a.y / b);
        public static Vector2 operator /(float a, Vector2 b) => new(a / b.x, a / b.y);
        public static bool operator ==(Vector2 a, Vector2 b) => a.Equals(b);
        public static bool operator !=(Vector2 a, Vector2 b) => !a.Equals(b);
        public static bool operator >(Vector2 a, Vector2 b) => a.x > b.x && a.y > b.y;
        public static bool operator <(Vector2 a, Vector2 b) => a.x < b.x && a.y < b.y;
        public static bool operator >=(Vector2 a, Vector2 b) => (a.x > b.x && a.y > b.y) || a.Equals(b);
        public static bool operator <=(Vector2 a, Vector2 b) => (a.x < b.x && a.y < b.y) || a.Equals(b);

        public static explicit operator Vector2(Vector3 input) => new(input.x, input.y);
        public static explicit operator Vector2(Vector4 input) => new(input.x, input.y);
    }
    [Serializable]
    public struct Vector3 : IMathFunctions<Vector3>
    {
        public float x, y, z;

        public static Vector3 NegativeInfinity { get => new(float.NegativeInfinity, float.NegativeInfinity, float.NegativeInfinity); }
        public static Vector3 One { get => new(1, 1, 1); }
        public static Vector3 PositiveInfinity { get => new(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity); }
        public static Vector3 Zero { get => new(0, 0, 0); }

        public Vector3(float x) => this = new(x, 0, 0);
        public Vector3(float x, float y) => this = new(x, y, 0);
        public Vector3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public Vector3 Average(params Vector3[] objs)
        {
            float[] averageX = new float[objs.Length + 1];
            float[] averageY = new float[objs.Length + 1];
            float[] averageZ = new float[objs.Length + 1];
            for (int i = 0; i < objs.Length; i++)
            {
                averageX[i] = objs[i].x;
                averageY[i] = objs[i].y;
                averageZ[i] = objs[i].z;
            }
            averageX[objs.Length] = x;
            averageY[objs.Length] = y;
            averageZ[objs.Length] = z;
            return new(Math.Average(averageX), Math.Average(averageY), Math.Average(averageZ));
        }
        public Vector3 Max(params Vector3[] objs)
        {
            float[] maxX = new float[objs.Length + 1];
            float[] maxY = new float[objs.Length + 1];
            float[] maxZ = new float[objs.Length + 1];
            for (int i = 0; i < objs.Length; i++)
            {
                maxX[i] = objs[i].x;
                maxY[i] = objs[i].y;
                maxZ[i] = objs[i].z;
            }
            maxX[objs.Length] = x;
            maxY[objs.Length] = y;
            maxZ[objs.Length] = z;
            return new(Math.Max(maxX), Math.Max(maxY), Math.Max(maxZ));
        }
        public Vector3 Min(params Vector3[] objs)
        {
            float[] minX = new float[objs.Length + 1];
            float[] minY = new float[objs.Length + 1];
            float[] minZ = new float[objs.Length + 1];
            for (int i = 0; i < objs.Length; i++)
            {
                minX[i] = objs[i].x;
                minY[i] = objs[i].y;
                minZ[i] = objs[i].z;
            }
            minX[objs.Length] = x;
            minY[objs.Length] = y;
            minZ[objs.Length] = z;
            return new(Math.Min(minX), Math.Min(minY), Math.Min(minZ));
        }

        public override bool Equals(object obj) => base.Equals(obj);
        public bool Equals(Vector3 other) => x == other.x && y == other.y && z == other.z;
        public override int GetHashCode() => base.GetHashCode();
        public override string ToString() => "X: " + x.ToString() + " | Y: " + y.ToString() + " | Z:" + z.ToString();
        public string ToString(string format) => "X: " + x.ToString(format) + " | Y: " + y.ToString(format) + " | Z:" + z.ToString(format);

        public static Vector3 operator +(Vector3 a, Vector3 b) => new(a.x + b.x, a.y + b.y, a.z + b.z);
        public static Vector3 operator +(Vector3 a, float b) => new(a.x + b, a.y + b, a.z + b);
        public static Vector3 operator +(float a, Vector3 b) => new(a + b.x, a + b.y, a + b.z);
        public static Vector3 operator -(Vector3 a, Vector3 b) => new(a.x - b.x, a.y - b.y, a.z - b.z);
        public static Vector3 operator -(Vector3 a, float b) => new(a.x - b, a.y - b, a.z - b);
        public static Vector3 operator -(float a, Vector3 b) => new(a - b.x, a - b.y, a - b.z);
        public static Vector3 operator *(Vector3 a, Vector3 b) => new(a.x * b.x, a.y * b.y, a.z * b.z);
        public static Vector3 operator *(Vector3 a, float b) => new(a.x * b, a.y * b, a.z * b);
        public static Vector3 operator *(float a, Vector3 b) => new(a * b.x, a * b.y, a * b.z);
        public static Vector3 operator /(Vector3 a, Vector3 b) => new(a.x / b.x, a.y / b.y, a.z / b.z);
        public static Vector3 operator /(Vector3 a, float b) => new(a.x / b, a.y / b, a.z / b);
        public static Vector3 operator /(float a, Vector3 b) => new(a / b.x, a / b.y, a / b.z);
        public static bool operator ==(Vector3 a, Vector3 b) => a.Equals(b);
        public static bool operator !=(Vector3 a, Vector3 b) => !a.Equals(b);
        public static bool operator >(Vector3 a, Vector3 b) => a.x > b.x && a.y > b.y && a.z > b.z;
        public static bool operator <(Vector3 a, Vector3 b) => a.x < b.x && a.y < b.y && a.z < b.z;
        public static bool operator >=(Vector3 a, Vector3 b) => (a.x > b.x && a.y > b.y && a.z > b.z) || a.Equals(b);
        public static bool operator <=(Vector3 a, Vector3 b) => (a.x < b.x && a.y < b.y && a.z < b.z) || a.Equals(b);

        public static implicit operator Vector3(Color input) => new(input.r, input.g, input.b);
        public static implicit operator Vector3(Vector2 input) => new(input.x, input.y);
        public static explicit operator Vector3(Vector4 input) => new(input.x, input.y, input.z);
    }
    [Serializable]
    public struct Vector4 : IMathFunctions<Vector4>
    {
        public float x, y, z, w;

        public static Vector4 NegativeInfinity { get => new(float.NegativeInfinity, float.NegativeInfinity, float.NegativeInfinity, float.NegativeInfinity); }
        public static Vector4 One { get => new(1, 1, 1, 1); }
        public static Vector4 PositiveInfinity { get => new(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity); }
        public static Vector4 Zero { get => new(0, 0, 0, 0); }

        public Vector4(float x) => this = new(x, 0, 0, 0);
        public Vector4(float x, float y) => this = new(x, y, 0, 0);
        public Vector4(float x, float y, float z) => this = new(x, y, z, 0);
        public Vector4(float x, float y, float z, float w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        public Vector4 Average(params Vector4[] objs)
        {
            float[] averageX = new float[objs.Length + 1];
            float[] averageY = new float[objs.Length + 1];
            float[] averageZ = new float[objs.Length + 1];
            float[] averageW = new float[objs.Length + 1];
            for (int i = 0; i < objs.Length; i++)
            {
                averageX[i] = objs[i].x;
                averageY[i] = objs[i].y;
                averageZ[i] = objs[i].z;
                averageW[i] = objs[i].w;
            }
            averageX[objs.Length] = x;
            averageY[objs.Length] = y;
            averageZ[objs.Length] = z;
            averageW[objs.Length] = w;
            return new(Math.Average(averageX), Math.Average(averageY), Math.Average(averageZ), Math.Average(averageW));
        }
        public Vector4 Max(params Vector4[] objs)
        {
            float[] maxX = new float[objs.Length + 1];
            float[] maxY = new float[objs.Length + 1];
            float[] maxZ = new float[objs.Length + 1];
            float[] maxW = new float[objs.Length + 1];
            for (int i = 0; i < objs.Length; i++)
            {
                maxX[i] = objs[i].x;
                maxY[i] = objs[i].y;
                maxZ[i] = objs[i].z;
                maxW[i] = objs[i].w;
            }
            maxX[objs.Length] = x;
            maxY[objs.Length] = y;
            maxZ[objs.Length] = z;
            maxW[objs.Length] = w;
            return new(Math.Max(maxX), Math.Max(maxY), Math.Max(maxZ), Math.Max(maxW));
        }
        public Vector4 Min(params Vector4[] objs)
        {
            float[] minX = new float[objs.Length + 1];
            float[] minY = new float[objs.Length + 1];
            float[] minZ = new float[objs.Length + 1];
            float[] minW = new float[objs.Length + 1];
            for (int i = 0; i < objs.Length; i++)
            {
                minX[i] = objs[i].x;
                minY[i] = objs[i].y;
                minZ[i] = objs[i].z;
                minW[i] = objs[i].w;
            }
            minX[objs.Length] = x;
            minY[objs.Length] = y;
            minZ[objs.Length] = z;
            minW[objs.Length] = w;
            return new(Math.Min(minX), Math.Min(minY), Math.Min(minZ), Math.Min(minW));
        }

        public override bool Equals(object obj) => base.Equals(obj);
        public bool Equals(Vector4 other) => x == other.x && y == other.y && z == other.z && w == other.w;
        public override int GetHashCode() => base.GetHashCode();
        public override string ToString() => "X: " + x.ToString() + " | Y: " + y.ToString() + " | Z: " + z.ToString() + " | W: " + w.ToString();
        public string ToString(string format) => "X: " + x.ToString(format) + " | Y: " + y.ToString(format) + " | Z: " + z.ToString(format) + " | W: " + w.ToString(format);

        public static Vector4 operator +(Vector4 a, Vector4 b) => new(a.x + b.x, a.y + b.y, a.z + b.z, a.w + b.w);
        public static Vector4 operator +(Vector4 a, float b) => new(a.x + b, a.y + b, a.z + b, a.w + b);
        public static Vector4 operator +(float a, Vector4 b) => new(a + b.x, a + b.y, a + b.z, a + b.w);
        public static Vector4 operator -(Vector4 a, Vector4 b) => new(a.x - b.x, a.y - b.y, a.z - b.z, a.w - b.w);
        public static Vector4 operator -(Vector4 a, float b) => new(a.x - b, a.y - b, a.z - b, a.w - b);
        public static Vector4 operator -(float a, Vector4 b) => new(a - b.x, a - b.y, a - b.z, a - b.w);
        public static Vector4 operator *(Vector4 a, Vector4 b) => new(a.x * b.x, a.y * b.y, a.z * b.z, a.w * b.w);
        public static Vector4 operator *(Vector4 a, float b) => new(a.x * b, a.y * b, a.z * b, a.w * b);
        public static Vector4 operator *(float a, Vector4 b) => new(a * b.x, a * b.y, a * b.z, a * b.w);
        public static Vector4 operator /(Vector4 a, Vector4 b) => new(a.x / b.x, a.y / b.y, a.z / b.z, a.w / b.w);
        public static Vector4 operator /(Vector4 a, float b) => new(a.x / b, a.y / b, a.z / b, a.w / b);
        public static Vector4 operator /(float a, Vector4 b) => new(a / b.x, a / b.y, a / b.z, a / b.w);
        public static bool operator ==(Vector4 a, Vector4 b) => a.Equals(b);
        public static bool operator !=(Vector4 a, Vector4 b) => !a.Equals(b);
        public static bool operator >(Vector4 a, Vector4 b) => a.x > b.x && a.y > b.y && a.z > b.z && a.w > b.w;
        public static bool operator <(Vector4 a, Vector4 b) => a.x < b.x && a.y < b.y && a.z < b.z && a.w < b.w;
        public static bool operator >=(Vector4 a, Vector4 b) => (a.x > b.x && a.y > b.y && a.z > b.z && a.w > b.w) || a.Equals(b);
        public static bool operator <=(Vector4 a, Vector4 b) => (a.x < b.x && a.y < b.y && a.z < b.z && a.w < b.w) || a.Equals(b);

        public static implicit operator Vector4(Color input) => new(input.r, input.g, input.b, input.a);
        public static implicit operator Vector4(Vector2 input) => new(input.x, input.y);
        public static implicit operator Vector4(Vector3 input) => new(input.x, input.y, input.z);
    }

    namespace Interfaces
    {
        public interface IMathFunctions<T>
        {
            public T Average(params T[] objs);
            public T Max(params T[] objs);
            public T Min(params T[] objs);
        }

        public interface INegatives<T>
        {
            public T Absolute { get; }
            public bool IsNegative { get; }
            public T Negative { get; }
            public T Positive { get; }
        }
    }
}