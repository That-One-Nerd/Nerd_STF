using System.Numerics;

namespace Nerd_STF.Mathematics.Abstract;

public interface IClampMagnitude<TSelf, TNumber>
    where TSelf : IClampMagnitude<TSelf, TNumber>
    where TNumber : INumber<TNumber>
{
    public static abstract TSelf ClampMagnitude(TSelf val, TNumber minMag, TNumber maxMag);
}
public interface IClampMagnitude<TSelf> where TSelf : IClampMagnitude<TSelf>
{
    public static abstract TSelf ClampMagnitude<TNumber>(TSelf val, TNumber minMag, TNumber maxMag)
        where TNumber : INumber<TNumber>;
}
