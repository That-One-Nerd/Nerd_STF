using System;
using System.Collections.Generic;

namespace Nerd_STF.Graphics.Formats
{
    public interface IColorFormat
    {
        int ChannelCount { get; }
        int BitLength { get; }
        Dictionary<ColorChannel, int> BitsPerChannel { get; }
        byte[] GetBitfield(ColorChannel channel);

        byte[] GetBits();
        IColor GetColor();

        // TODO: Bitwriter?
        // write to stream
    }

    public interface IColorFormat<TSelf, TColor> : IColorFormat,
                                                   IEquatable<TSelf>
        where TSelf : IColorFormat<TSelf, TColor>
        where TColor : struct, IColor<TColor>
    {
#if CS11_OR_GREATER
        new static abstract int BitLength { get; }
        int IColorFormat.BitLength => TSelf.BitLength;

        new static abstract Dictionary<ColorChannel, int> BitsPerChannel { get; }
        Dictionary<ColorChannel, int> IColorFormat.BitsPerChannel => TSelf.BitsPerChannel;

        new static abstract byte[] GetBitfield(ColorChannel channel);
        byte[] IColorFormat.GetBitfield(ColorChannel channel) => TSelf.GetBitfield(channel);

        static abstract TSelf FromColor(IColor color);
        static abstract TSelf FromColor(TColor color);
#endif

        new TColor GetColor();
#if CS8_OR_GREATER
        IColor IColorFormat.GetColor() => GetColor();
#endif
        void SetColor(TColor color);

#if CS11_OR_GREATER
        static abstract TSelf operator +(TSelf a, TSelf b);
        static abstract TSelf operator *(TSelf a, TSelf b);

        static abstract bool operator ==(TSelf a, TSelf b);
        static abstract bool operator !=(TSelf a, TSelf b);
#endif
    }
}
