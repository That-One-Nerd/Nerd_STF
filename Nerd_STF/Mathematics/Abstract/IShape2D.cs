using System.Numerics;

namespace Nerd_STF.Mathematics.Abstract;

public interface IShape2D<TNumber> where TNumber : INumber<TNumber>
{
    public TNumber Area { get; }
    public TNumber Perimeter { get; }
}
