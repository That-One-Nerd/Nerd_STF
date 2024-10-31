using System.Collections.Generic;

namespace Nerd_STF.Abstract
{
    public interface ICombinationIndexer<TItem>
    {
        IEnumerable<TItem> this[string key] { get; set; }
    }
}
