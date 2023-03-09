namespace Nerd_STF.Mathematics.Abstract;

public interface IIndexGet<TSub>
{
    public TSub this[int index] { get; }
    public TSub this[Index index] { get; }
}
