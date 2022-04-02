namespace Nerd_STF
{
    public interface IGroup<T> : IEnumerable<T>
    {
        public T[] ToArray();
        public List<T> ToList();
    }
}
