namespace Nerd_STF.Mathematics.Abstract;

public interface IIndexRangeGet<TSub>
{
    public TSub[] this[Range range] { get; }
}
