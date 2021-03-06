namespace Nerd_STF;

public interface IEncapsulator<T, TE> : IContainer<TE> where T : IEquatable<T> where TE : IEquatable<TE>
{
    public T Encapsulate(TE val);
}
