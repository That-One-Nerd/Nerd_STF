namespace Nerd_STF.Mathematics.Geometry.Abstract;

public interface IClosestTo<T> where T : IEquatable<T>
{
    public T ClosestTo(T item);
}
