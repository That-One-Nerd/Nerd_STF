using System.Numerics;

namespace Nerd_STF.Mathematics.Abstract;

public interface IShape3D<TNumber> where TNumber : INumber<TNumber>
{
    public TNumber SurfaceArea { get; }
    public TNumber Volume { get; }
}
