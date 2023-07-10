namespace Nerd_STF.Mathematics.Abstract;

public interface IEncapsulate<T, TE> : IContains<TE> where T : IEquatable<T> where TE : IEquatable<TE>
{
    public T Encapsulate(TE val);
}
