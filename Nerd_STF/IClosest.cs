namespace Nerd_STF;

public interface IClosest<T> where T : IEquatable<T>
{
    public T ClosestTo(T item);
}
