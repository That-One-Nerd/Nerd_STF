namespace Nerd_STF.Mathematics.Abstract;

public interface IIntersect<T> where T : IEquatable<T>
{
    public bool Intersects(T other);
}
