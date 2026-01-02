using System;
using System.Collections.Generic;

namespace Nerd_STF.Mathematics.Discrete
{
    public interface ISet<TSelf, TItem> : IEquatable<TSelf>
        where TSelf : ISet<TSelf, TItem>
        where TItem : IEquatable<TItem>
    {
        TSelf With(TItem item);

        bool Contains(TItem item);

        TSelf Union(TSelf other);
        TSelf Intersection(TSelf other);
        TSelf Difference(TSelf other);

#if CS11_OR_GREATER
        static abstract TSelf operator +(TSelf a, TItem b);
        static abstract TSelf operator &(TSelf a, TSelf b);
        static abstract TSelf operator |(TSelf a, TSelf b);
        static abstract TSelf operator -(TSelf a, TSelf b);
#endif
    }
}
