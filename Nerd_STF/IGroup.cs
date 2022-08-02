namespace Nerd_STF;

public interface IGroup<T> : IEnumerable<T>
{
    public T[] ToArray();
    public Fill<T> ToFill();
    public List<T> ToList();
}
