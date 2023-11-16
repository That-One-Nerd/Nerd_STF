namespace Nerd_STF.Mathematics.Geometry.Abstract;

public interface ISubdivide<T>
{
    public T[] Subdivide();
    public T[] Subdivide(int iterations);
}
