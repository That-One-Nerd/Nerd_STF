#if CS11_OR_GREATER
using System.Collections.Generic;
using System.Numerics;

namespace Nerd_STF.Mathematics
{
    public interface IProductOperation<TSelf> : IMultiplyOperators<TSelf, TSelf, TSelf>
        where TSelf : IProductOperation<TSelf>
    {
        static abstract TSelf Product(IEnumerable<TSelf> vals);
    }
}
#endif
