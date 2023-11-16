namespace Nerd_STF.Mathematics.Geometry.Abstract;

public interface IGeometricModifiers2d<T> : IGeometricRotate2d<T>, IGeometricScale2d<T>,
    IGeometricTranslate2d<T>
    where T : IGeometricModifiers2d<T>
{ }
