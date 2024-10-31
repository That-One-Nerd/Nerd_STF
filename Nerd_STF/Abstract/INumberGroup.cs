using System;
using System.Collections.Generic;
using System.Numerics;

namespace Nerd_STF.Abstract
{
    public interface INumberGroup<TSelf, TItem> : ICombinationIndexer<TItem>,
                                                  IEnumerable<TItem>,
                                                  IEquatable<TSelf>,
                                                  IModifiable<TSelf>
#if CS11_OR_GREATER
                                                 ,IInterpolable<TSelf>,
                                                  ISimpleMathOperations<TSelf>,
                                                  IVectorOperations<TSelf>
#endif
        where TSelf : INumberGroup<TSelf, TItem>
#if CS11_OR_GREATER
        where TItem : INumber<TItem>
#endif
    {
        TItem this[int index] { get; set; }

        TItem[] ToArray();
        List<TItem> ToList();
    }
}
