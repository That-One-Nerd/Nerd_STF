#if CS11_OR_GREATER
using System.Collections.Generic;

namespace Nerd_STF.Mathematics.Algebra
{
    public interface IMatrixOperations<TSelf> : ISimpleMathOperations<TSelf>
        where TSelf : IMatrix<TSelf>, IMatrixOperations<TSelf>
    {
        static abstract TSelf Average(IEnumerable<TSelf> vals);
        static abstract TSelf Lerp(TSelf a, TSelf b, double t, bool clamp = true);

        static abstract TSelf operator *(TSelf a, double b);
        static abstract TSelf operator /(TSelf a, double b);
        static abstract TSelf? operator ~(TSelf m);
    }
}
#endif
