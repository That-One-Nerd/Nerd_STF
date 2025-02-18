using Nerd_STF.Mathematics;
using System;
using System.Collections.Generic;
using System.IO;

namespace Nerd_STF.Graphics.Formats
{
    public class R8G8B8A8 : IColorFormat<R8G8B8A8, ColorRGB>
    {
        public static int ChannelCount => 4;
        public static int BitLength => 32;
        public static Dictionary<ColorChannel, int> BitsPerChannel { get; } = new Dictionary<ColorChannel, int>()
        {
            { ColorChannel.Red, 8 },
            { ColorChannel.Green, 8 },
            { ColorChannel.Blue, 8 },
            { ColorChannel.Alpha, 8 }
        };

        int IColorFormat.ChannelCount => ChannelCount;
        int IColorFormat.BitLength => BitLength;
        Dictionary<ColorChannel, int> IColorFormat.BitsPerChannel => BitsPerChannel;

        public byte R
        {
            get => r;
            set => r = value;
        }
        public byte G
        {
            get => g;
            set => g = value;
        }
        public byte B
        {
            get => b;
            set => b = value;
        }
        public byte A
        {
            get => a;
            set => a = value;
        }

        private byte r, g, b, a;

        public R8G8B8A8(ColorRGB color)
        {
            r = (byte)MathE.Clamp(color.r * 255, 0, 255);
            g = (byte)MathE.Clamp(color.g * 255, 0, 255);
            b = (byte)MathE.Clamp(color.b * 255, 0, 255);
            a = (byte)MathE.Clamp(color.a * 255, 0, 255);
        }
        public R8G8B8A8(byte r, byte g, byte b, byte a)
        {
            this.r = r;
            this.g = g;
            this.b = b;
            this.a = a;
        }
        public static R8G8B8A8 FromColor(IColor color) => new R8G8B8A8(color.AsRgb());
        public static R8G8B8A8 FromColor(ColorRGB color) => new R8G8B8A8(color);

        public static byte[] GetBitfield(ColorChannel channel)
        {
            byte[] buf = new byte[4];
            switch (channel)
            {
                case ColorChannel.Red:   buf[0] = 0xFF; break;
                case ColorChannel.Green: buf[1] = 0xFF; break;
                case ColorChannel.Blue:  buf[2] = 0xFF; break;
                case ColorChannel.Alpha: buf[3] = 0xFF; break;
            }
            return buf;
        }
        byte[] IColorFormat.GetBitfield(ColorChannel channel) => GetBitfield(channel);

#if CS8_OR_GREATER
        public bool Equals(R8G8B8A8? other) =>
#else
        public bool Equals(R8G8B8A8 other) =>
#endif
            !(other is null) && r == other.r && g == other.g && b == other.b && a == other.a;
#if CS8_OR_GREATER
        public override bool Equals(object? obj)
#else
        public override bool Equals(object obj)
#endif
        {
            if (obj is null) return false;
            else if (obj is R8G8B8A8 formatObj) return Equals(formatObj);
            else return false;
        }
        public override int GetHashCode() => base.GetHashCode();
        public override string ToString() => $"{{ r={r}, g={g}, b={b}, a={a} }}";

        public byte[] GetBits() => new byte[] { r, g, b, a };

        public ColorRGB GetColor()
        {
            const double inv255 = 0.00392156862745; // Constant for 1/255
            return new ColorRGB(r * inv255,
                                g * inv255,
                                b * inv255,
                                a * inv255);
        }
        IColor IColorFormat.GetColor() => GetColor();
        public void SetColor(ColorRGB color)
        {
            r = (byte)MathE.Clamp(color.r * 255, 0, 255);
            g = (byte)MathE.Clamp(color.g * 255, 0, 255);
            b = (byte)MathE.Clamp(color.b * 255, 0, 255);
            a = (byte)MathE.Clamp(color.a * 255, 0, 255);
        }
        public void SetColor(byte r, byte g, byte b, byte a)
        {
            this.r = (byte)MathE.Clamp(r * 255, 0, 255);
            this.g = (byte)MathE.Clamp(g * 255, 0, 255);
            this.b = (byte)MathE.Clamp(b * 255, 0, 255);
            this.a = (byte)MathE.Clamp(a * 255, 0, 255);
        }

        public static R8G8B8A8 operator +(R8G8B8A8 a, R8G8B8A8 b)
        {
            return new R8G8B8A8((byte)MathE.Clamp(a.r + b.r, 0, 255),
                                (byte)MathE.Clamp(a.g + b.g, 0, 255),
                                (byte)MathE.Clamp(a.b + b.b, 0, 255),
                                (byte)MathE.Clamp(a.a + b.a, 0, 255));
        }
        public static R8G8B8A8 operator *(R8G8B8A8 a, R8G8B8A8 b)
        {
            const double inv255 = 0.00392156862745; // Constant for 1/255
            return new R8G8B8A8((byte)MathE.Clamp(a.r * b.r * inv255, 0, 255),
                                (byte)MathE.Clamp(a.g * b.g * inv255, 0, 255),
                                (byte)MathE.Clamp(a.b * b.b * inv255, 0, 255),
                                (byte)MathE.Clamp(a.a * b.a * inv255, 0, 255));
        }
        public static bool operator ==(R8G8B8A8 a, R8G8B8A8 b) => a.Equals(b);
        public static bool operator !=(R8G8B8A8 a, R8G8B8A8 b) => !a.Equals(b);
    }
}
