#if CS11_OR_GREATER
using System.Runtime.CompilerServices;

namespace Nerd_STF.Mathematics.Abstract
{
    public interface IFromTuple<TSelf, TTuple>
        where TSelf : IFromTuple<TSelf, TTuple>
        where TTuple : struct, ITuple
    {
        public static abstract implicit operator TSelf(TTuple tuple);
        public static abstract implicit operator TTuple(TSelf tuple);
    }
}
#endif
