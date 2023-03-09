namespace Nerd_STF.Mathematics.Abstract;

public interface IIndexSet<TSub>
{
    public TSub this[int index] { set; }
    public TSub this[Index index] { set; }
}
