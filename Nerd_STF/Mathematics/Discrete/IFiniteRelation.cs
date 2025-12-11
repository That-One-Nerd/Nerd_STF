using Nerd_STF.Mathematics.Algebra;
using System.Collections.Generic;

namespace Nerd_STF.Mathematics.Discrete
{
    public interface IFiniteRelation<TItem1, TItem2> : IRelation<TItem1, TItem2>, IEnumerable<(TItem1, TItem2)>
    {
        Matrix GetMatrix();
        (IEnumerable<TItem1>, IEnumerable<TItem2>) Distinct();
    }
}
