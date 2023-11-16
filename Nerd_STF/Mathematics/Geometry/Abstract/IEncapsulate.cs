namespace Nerd_STF.Mathematics.Geometry.Abstract;

public interface IEncapsulate<T, TE> : IContains<TE> where T : IEquatable<T> where TE : IEquatable<TE>
{
    public T Encapsulate(TE val);
}
