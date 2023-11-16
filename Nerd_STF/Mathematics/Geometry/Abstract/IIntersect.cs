namespace Nerd_STF.Mathematics.Geometry.Abstract;

public interface IIntersect<T> where T : IEquatable<T>
{
    public bool Intersects(T other);
}
