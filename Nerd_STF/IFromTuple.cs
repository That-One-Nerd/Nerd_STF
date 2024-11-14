#if CS11_OR_GREATER
using System.Runtime.CompilerServices;

namespace Nerd_STF
{
    public interface IFromTuple<TSelf, TTuple>
        where TSelf : IFromTuple<TSelf, TTuple>
        where TTuple : struct, ITuple
    {
        static abstract implicit operator TSelf(TTuple tuple);
        static abstract implicit operator TTuple(TSelf tuple);
    }
}
#endif
