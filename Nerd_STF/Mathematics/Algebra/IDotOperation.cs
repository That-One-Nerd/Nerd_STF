#if CS11_OR_GREATER
using System.Collections.Generic;
using System.Numerics;

namespace Nerd_STF.Mathematics.Algebra
{
    public interface IDotOperation<TSelf, TNumber>
        where TSelf : IDotOperation<TSelf, TNumber>
        where TNumber : INumber<TNumber>
    {
        static abstract TNumber Dot(TSelf a, TSelf b);
        static abstract TNumber Dot(IEnumerable<TSelf> vals);
    }
}
#endif
