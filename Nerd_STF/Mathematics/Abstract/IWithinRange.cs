using System.Numerics;

namespace Nerd_STF.Mathematics.Abstract;

public interface IWithinRange<TSub>
{
    public bool WithinRange<TNumber>(TSub obj, TNumber range) where TNumber : INumber<TNumber>;
}
public interface IWithinRange<TSub, TNumber> where TNumber : INumber<TNumber>
{
    public bool WithinRange(TSub obj, TNumber range);
}
