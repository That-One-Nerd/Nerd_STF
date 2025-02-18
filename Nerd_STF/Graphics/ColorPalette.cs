using Nerd_STF.Graphics.Formats;
using Nerd_STF.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Nerd_STF.Graphics
{
    // TODO: Should this be a ref struct?
    public class ColorPalette<TColor> : IEnumerable<TColor>, IEquatable<ColorPalette<TColor>>
        where TColor : struct, IColor<TColor>
    {
        public int BitDepth { get; private set; }
        public int Length => colors.Length;

        private TColor[] colors;
        private IndexedColor<TColor>[] indexedColors;

#pragma warning disable CS8618
        private ColorPalette() { }
#pragma warning restore CS8618
        public ColorPalette(int colors)
        {
            int size = GetSizeFor(colors, out int bits);
            this.colors = new TColor[size];
            indexedColors = new IndexedColor<TColor>[size];
            for (int i = 0; i < size; i++) indexedColors[i] = new IndexedColor<TColor>(this, i);
            BitDepth = bits;
        }
        public ColorPalette(ReadOnlySpan<TColor> colors)
        {
            int size = GetSizeFor(colors.Length, out int bits);
            this.colors = new TColor[size];
            colors.CopyTo(this.colors);
            indexedColors = new IndexedColor<TColor>[size];
            for (int i = 0; i < size; i++) indexedColors[i] = new IndexedColor<TColor>(this, i);
            BitDepth = bits;
        }

        public static ColorPalette<TColor> FromBitDepth(int bits)
        {
            int size = 1 << bits;
            ColorPalette<TColor> palette = new ColorPalette<TColor>()
            {
                BitDepth = bits,
                colors = new TColor[size],
                indexedColors = new IndexedColor<TColor>[size]
            };
            for (int i = 0; i < size; i++) palette.indexedColors[i] = new IndexedColor<TColor>(palette, i);
            return palette;
        }

        public IndexedColor<TColor> this[int index] => indexedColors[index];
        public ref TColor Color(int index) => ref colors[index];

        public void Clear()
        {
            for (int i = 0; i < colors.Length; i++)
            {
                colors[i] = default;
            }
        }
        public bool Contains(TColor color)
        {
            for (int i = 0; i < Length; i++)
            {
                if (colors[i].Equals(color)) return true;
            }
            return false;
        }
        public bool Contains(Predicate<TColor> predicate)
        {
            for (int i = 0; i < Length; i++)
            {
                if (predicate(colors[i])) return true;
            }
            return false;
        }
        public void CopyTo(Span<TColor> destination) => CopyTo(0, destination, 0, Length);
        public void CopyTo(int sourceIndex, Span<TColor> destination, int destIndex, int count)
        {
            for (int i = 0; i < count; i++)
            {
                destination[destIndex + i] = colors[sourceIndex + i];
            }
        }
        public void Expand(int newSize)
        {
            int newLength = GetSizeFor(newSize, out int bits);
            if (newLength <= Length) return; // Contraction not currently supported.
            TColor[] newColors = new TColor[newLength];
            IndexedColor<TColor>[] newIndexedColors = new IndexedColor<TColor>[newLength];
            Array.Copy(colors, newColors, colors.Length);
            Array.Copy(indexedColors, newIndexedColors, indexedColors.Length);
            for (int i = Length; i < newLength; i++) newIndexedColors[i] = new IndexedColor<TColor>(this, i);
            colors = newColors;
            indexedColors = newIndexedColors;
            BitDepth = bits;
        }

#if CS8_OR_GREATER
        public bool ReferenceEquals(ColorPalette<TColor>? other)
#else
        public bool ReferenceEquals(ColorPalette<TColor> other)
#endif
        {
            return ReferenceEquals(this, other);
        }
#if CS8_OR_GREATER
        public bool Equals(ColorPalette<TColor>? other)
#else
        public bool Equals(ColorPalette<TColor> other)
#endif
        {
            if (other is null) return false;
            else if (Length != other.Length) return false;

            for (int i = 0; i < Length; i++)
            {
                if (!colors[i].Equals(other.colors[i])) return false;
            }

            return true;
        }
#if CS8_OR_GREATER
        public override bool Equals(object? other)
#else
        public override bool Equals(object other)
#endif
        {
            if (other is null) return false;
            else if (other is ColorPalette<TColor> otherColor) return Equals(otherColor);
            else return false;
        }
        public override int GetHashCode() => base.GetHashCode();
        public override string ToString() => $"{BitDepth} BPP Palette: {typeof(TColor).Name}[{Length}]";

        public IEnumerator<TColor> GetEnumerator()
        {
            for (int i = 0; i < colors.Length; i++) yield return colors[i];
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private static int GetSizeFor(int colors, out int bitDepth)
        {
            int maxSize = 1;
            bitDepth = 0;
            colors--;
            while (colors > 0)
            {
                maxSize <<= 1;
                colors >>= 1;
                bitDepth++;
            }
            return maxSize;
        }
    }
}
