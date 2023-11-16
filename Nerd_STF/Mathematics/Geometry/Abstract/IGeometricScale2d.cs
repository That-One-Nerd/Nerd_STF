namespace Nerd_STF.Mathematics.Geometry.Abstract;

public interface IGeometricScale2d<T> where T : IGeometricScale2d<T>
{
    public void Scale(float factor);
    public void Scale(Float2 factor);

    public T ScaleImmutable(float factor);
    public T ScaleImmutable(Float2 factor);
}
