namespace Nerd_STF.Mathematics.Abstract;

public interface ISubdivide<T>
{
    public T[] Subdivide();
    public T[] Subdivide(int iterations);
}
