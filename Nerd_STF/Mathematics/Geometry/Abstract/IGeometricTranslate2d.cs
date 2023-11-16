namespace Nerd_STF.Mathematics.Geometry.Abstract;

public interface IGeometricTranslate2d<T> where T : IGeometricTranslate2d<T>
{
    public void Translate(Float2 offset);
    public T TranslateImmutable(Float2 offset);
}
