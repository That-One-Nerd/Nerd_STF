using System.Numerics;

namespace Nerd_STF.Mathematics.Abstract;

public interface IMagnitude<TNumber> where TNumber : INumber<TNumber>
{
    public TNumber Magnitude { get; }
}
