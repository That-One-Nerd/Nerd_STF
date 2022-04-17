namespace Nerd_STF;

public interface IContainer<T> where T : IEquatable<T>
{
    public bool Contains(T item);
}
