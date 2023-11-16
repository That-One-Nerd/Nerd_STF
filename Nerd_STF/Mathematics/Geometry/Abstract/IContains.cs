namespace Nerd_STF.Mathematics.Geometry.Abstract;

public interface IContains<T> where T : IEquatable<T>
{
    public bool Contains(T item);
}
