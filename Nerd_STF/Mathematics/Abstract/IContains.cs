namespace Nerd_STF.Mathematics.Abstract;

public interface IContains<T> where T : IEquatable<T>
{
    public bool Contains(T item);
}
