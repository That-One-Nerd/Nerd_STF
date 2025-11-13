using System.Collections.Generic;
using System.Numerics;

namespace Nerd_STF.Mathematics
{
    public interface INumberGroupBase<TItem>
#if CS11_OR_GREATER
        where TItem : INumber<TItem>
#endif
    {
        TItem this[int index] { get; set; }

        TItem[] ToArray();
        Fill<TItem> ToFill();
        List<TItem> ToList();
    }
}
