#if CS11_OR_GREATER
using System.Collections.Generic;

namespace Nerd_STF.Mathematics.Algebra
{
    public interface IVectorOperations<TSelf> : ISimpleMathOperations<TSelf>
        where TSelf : IVectorOperations<TSelf>
    {
        double Magnitude { get; }

        static abstract TSelf ClampMagnitude(TSelf val, double minMag, double maxMag);
        static abstract double Dot(TSelf a, TSelf b);
        static abstract double Dot(IEnumerable<TSelf> vals);
    }
}
#endif
