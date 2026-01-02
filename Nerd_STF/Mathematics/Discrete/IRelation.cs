using System.Collections.Generic;

namespace Nerd_STF.Mathematics.Discrete
{
    public interface IRelation<TItem1, TItem2>
    {
        IEnumerable<TItem2> Get(TItem1 item);
        bool IsRelated(TItem1 item1, TItem2 item2);

        bool IsReflexive();
        bool IsSymmetric();
        bool IsAntiSymmetric();
        //bool IsTransitive();
    }
}
