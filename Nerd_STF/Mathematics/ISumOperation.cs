#if CS11_OR_GREATER
using System.Collections.Generic;
using System.Numerics;

namespace Nerd_STF.Mathematics
{
    public interface ISumOperation<TSelf> : IAdditionOperators<TSelf, TSelf, TSelf>
        where TSelf : ISumOperation<TSelf>
    {
        static abstract TSelf Sum(IEnumerable<TSelf> vals);
    }
}
#endif
