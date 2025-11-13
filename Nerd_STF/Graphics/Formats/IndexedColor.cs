using Nerd_STF.Helpers;
using Nerd_STF.Mathematics;
using System;
using System.Collections.Generic;

namespace Nerd_STF.Graphics.Formats
{
    public class IndexedColor<TColor> : IColorFormat, IEquatable<IndexedColor<TColor>>
        where TColor : struct, IColor<TColor>
    {
        public int BitLength => palette.BitDepth;
        public int Index { get; }

        int IColorFormat.ChannelCount => 1;
        Dictionary<ColorChannel, int> IColorFormat.BitsPerChannel => new Dictionary<ColorChannel, int>()
        {
            { ColorChannel.Index, palette.BitDepth }
        };
        byte[] IColorFormat.GetBitfield(ColorChannel channel)
        {
            byte[] buf = new byte[MathE.Ceiling(palette.BitDepth / 8.0)];
            if (channel != ColorChannel.Index) return buf; // All zeroes.
            int wholes = palette.BitDepth / 8, parts = palette.BitDepth % 8;
            for (int i = 0; i < wholes; i++) buf[i] = 0xFF;
            for (int i = 0; i < parts; i++) buf[wholes] = (byte)((buf[wholes] << 1) + 1);
            return buf;
        }

        private readonly ColorPalette<TColor> palette;

        public IndexedColor(ColorPalette<TColor> palette, int index)
        {
            this.palette = palette;
            Index = index;
        }

        public ColorPalette<TColor> GetPalette() => palette;

        public ref TColor Color() => ref palette.Color(Index);
        IColor IColorFormat.GetColor() => Color();
        public byte[] GetBits()
        {
            byte[] buf = new byte[MathE.Ceiling(palette.BitDepth / 8.0)];
            int bitIndex = 0, byteIndex = 0, remaining = Index;
            while (remaining > 0)
            {
                buf[byteIndex] |= (byte)((remaining & 1) << bitIndex);
                remaining >>= 1;
                bitIndex++;
                if (bitIndex == 8)
                {
                    bitIndex = 0;
                    byteIndex++;
                }
            }
            return buf;
        }

        public bool ReferenceEquals(IndexedColor<TColor> other) => ReferenceEquals(this, other);
#if CS8_OR_GREATER
        public bool Equals(IndexedColor<TColor>? other)
#else
        public bool Equals(IndexedColor<TColor> other)
#endif
            => !(other is null) && Color().Equals(other.Color());
#if CS8_OR_GREATER
        public override bool Equals(object? other)
#else
        public override bool Equals(object other)
#endif
        {
            if (other is null) return false;
            else if (other is IndexedColor<TColor> otherIndexed) return Equals(otherIndexed);
            else if (other is TColor otherColor) return Color().Equals(otherColor);
            else return false;
        }
        public override int GetHashCode() => base.GetHashCode();
        public override string ToString() => $"#0x{Index:X}: {Color()}";
    }
}
