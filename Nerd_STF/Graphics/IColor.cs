using Nerd_STF.Mathematics;
using System;
using System.Collections.Generic;

namespace Nerd_STF.Graphics
{
    public interface IColor : INumberGroupBase<double>
    {
        Dictionary<ColorChannel, double> GetChannels();

        TColor AsColor<TColor>() where TColor : struct, IColor<TColor>;
        ColorRGB AsRgb();
        ColorHSV AsHsv();
        ColorCMYK AsCmyk();

        string HexCode();

        bool Equals(IColor other);
    }

    public interface IColor<TSelf> : IColor,
                                     IEquatable<TSelf>
#if CS11_OR_GREATER
                                    ,IColorOperators<TSelf>,
                                     IColorPresets<TSelf>,
                                     IInterpolable<TSelf>
#endif
        where TSelf : struct, IColor<TSelf>
    {
#if CS11_OR_GREATER
        static abstract int ChannelCount { get; }

        static abstract TSelf Average(IEnumerable<TSelf> colors);
#endif
    }
}
