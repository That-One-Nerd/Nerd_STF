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

        bool Equals(IColor other);
    }

    public interface IColor<TSelf> : IColor,
                                     IEquatable<TSelf>,
                                     INumberGroup<TSelf, double>
#if CS11_OR_GREATER
                                    ,IColorPresets<TSelf>
#endif
        where TSelf : struct, IColor<TSelf>
    {
#if CS11_OR_GREATER
        static abstract int ChannelCount { get; }

        // TODO: Do all color formats have a gamma value?
        static abstract TSelf Average(double gamma, IEnumerable<TSelf> colors);
        static abstract TSelf Lerp(double gamma, TSelf a, TSelf b, double t, bool clamp = true);
#endif
    }
}
