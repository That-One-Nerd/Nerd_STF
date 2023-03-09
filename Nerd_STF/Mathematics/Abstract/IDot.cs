using System.Numerics;

namespace Nerd_STF.Mathematics.Abstract;

public interface IDot<TSelf, TNumber>
    where TSelf : IDot<TSelf, TNumber>
    where TNumber : INumber<TNumber>
{
    public static abstract TNumber Dot(TSelf a, TSelf b);
    public static abstract TNumber Dot(TSelf[] vals);
}    
public interface IDot<TSelf> where TSelf : IDot<TSelf>
{
    public static abstract TNumber Dot<TNumber>(TSelf a, TSelf b)
        where TNumber : INumber<TNumber>;
    public static abstract TNumber Dot<TNumber>(TSelf[] vals)
        where TNumber : INumber<TNumber>;
}
