namespace Nerd_STF.Mathematics.Geometry;

public interface ISubdividable<T>
{
    public T Subdivide();
    public T Subdivide(int iterations);
}
