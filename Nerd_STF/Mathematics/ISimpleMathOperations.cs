#if CS11_OR_GREATER
using System.Collections.Generic;
using System.Numerics;

namespace Nerd_STF.Mathematics
{
    public interface ISimpleMathOperations<TSelf> : IAdditionOperators<TSelf, TSelf, TSelf>,
                                                    IMultiplyOperators<TSelf, TSelf, TSelf>
        where TSelf : ISimpleMathOperations<TSelf>
    {
        static abstract TSelf Product(IEnumerable<TSelf> vals);
        static abstract TSelf Sum(IEnumerable<TSelf> vals);
    }
}
#endif
