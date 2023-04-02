using System.Numerics;

namespace Nerd_STF.Mathematics.Abstract;

public interface IShape2d<TNumber> where TNumber : INumber<TNumber>
{
    public TNumber Area { get; }
    public TNumber Perimeter { get; }
}
