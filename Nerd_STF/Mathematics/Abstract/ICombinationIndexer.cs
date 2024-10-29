using System.Collections.Generic;

namespace Nerd_STF.Mathematics.Abstract
{
    public interface ICombinationIndexer<TItem>
    {
        IEnumerable<TItem> this[string key] { get; set; }
    }
}
