using System;
using System.Collections.Generic;

namespace Nerd_STF.Mathematics.Discrete
{
    public interface IFiniteSet<TSelf, TItem> : ISet<TSelf, TItem>, IEnumerable<TItem>
        where TSelf : IFiniteSet<TSelf, TItem>
        where TItem : IEquatable<TItem>
    {
        int Count { get; }
    }
}
