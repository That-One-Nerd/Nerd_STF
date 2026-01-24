#if CS11_OR_GREATER
using System.Numerics;

namespace Nerd_STF.Mathematics.Algebra
{
    public interface IVector<TSelf> : IVector<TSelf, double> where TSelf : IVector<TSelf> { }
    public interface IVector<TSelf, TNumber> : IDotOperation<TSelf, TNumber>,
                                               IMagnitudeOperators<TSelf>,
                                             //IMultiplyOperators<TSelf, TNumber, TSelf>, // stupid "unification" problem in INumberGroup if uncommented
                                               ISumOperation<TSelf>
        where TSelf : IVector<TSelf, TNumber>
        where TNumber : INumber<TNumber>
    {
        static abstract TSelf operator *(TSelf a, TNumber b);
        static abstract TSelf operator /(TSelf a, TNumber b);
    }
}
#endif
