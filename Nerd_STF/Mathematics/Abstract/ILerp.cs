using System.Numerics;

namespace Nerd_STF.Mathematics.Abstract;

public interface ILerp<TSelf, TNumber>
    where TSelf : ILerp<TSelf, TNumber>
    where TNumber : INumber<TNumber>
{
    public static abstract TSelf Lerp(TSelf a, TSelf b, TNumber t, bool clamp = true);
}
public interface ILerp<TSelf> where TSelf : ILerp<TSelf>
{
    public static abstract TSelf Lerp<TNumber>(TSelf a, TSelf b, TNumber t, bool clamp = true)
        where TNumber : INumber<TNumber>;
}
