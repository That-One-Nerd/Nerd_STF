namespace Nerd_STF.Mathematics.Geometry.Abstract;

public interface IGeometricModifiers3d<T> where T : IGeometricModifiers3d<T>
{
    public void Scale(float factor);
    public void Scale(Float3 factor);
    public void Translate(Float3 offset);

    public T ScaleImmutable(float factor);
    public T ScaleImmutable(Float3 factor);
    public T TranslateImmutable(Float3 offset);
}
