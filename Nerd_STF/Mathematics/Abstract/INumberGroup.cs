using System;
using System.Collections.Generic;

namespace Nerd_STF.Mathematics.Abstract
{
    public interface INumberGroup<TSelf, TItem> : ICombinationIndexer<TItem>,
                                                  IEnumerable<TItem>,
                                                  IEquatable<TSelf>
        where TSelf : INumberGroup<TSelf, TItem>
    {
        TItem this[int index] { get; set; }

        TItem[] ToArray();
        List<TItem> ToList();
    }
}
