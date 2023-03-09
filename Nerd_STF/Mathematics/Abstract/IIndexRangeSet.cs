namespace Nerd_STF.Mathematics.Abstract;

public interface IIndexRangeSet<TSub>
{
    public TSub[] this[Range range] { set; }
}
