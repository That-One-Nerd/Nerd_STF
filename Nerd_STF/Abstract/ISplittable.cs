#if CS11_OR_GREATER
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Nerd_STF.Abstract
{
    public interface ISplittable<TSelf, TTuple>
        where TSelf : ISplittable<TSelf, TTuple>
        where TTuple : struct, ITuple
    {
        static abstract TTuple SplitArray(IEnumerable<TSelf> values);
    }
}
#endif
