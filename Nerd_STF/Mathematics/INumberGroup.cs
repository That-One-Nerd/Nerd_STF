using Nerd_STF.Mathematics.Algebra;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace Nerd_STF.Mathematics
{
    public interface INumberGroup<TSelf, TItem> : ICombinationIndexer<TItem>,
                                                  IEnumerable<TItem>,
                                                  IEquatable<TSelf>,
                                                  IMagnitudeOperators<TSelf>,
                                                  INumberGroupBase<TItem>
#if CS11_OR_GREATER
                                                 ,IInterpolable<TSelf>,
                                                  ISumOperation<TSelf>,
                                                  IProductOperation<TSelf>,
                                                  IVector<TSelf, TItem>
#endif
        where TSelf : INumberGroup<TSelf, TItem>
#if CS11_OR_GREATER
        where TItem : INumber<TItem>
#endif
    { }
}
