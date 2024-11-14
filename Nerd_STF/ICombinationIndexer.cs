namespace Nerd_STF
{
    public interface ICombinationIndexer<TItem>
    {
        ListTuple<TItem> this[string key] { get; set; }
    }
}
