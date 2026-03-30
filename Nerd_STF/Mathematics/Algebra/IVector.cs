using System.Numerics;

namespace Nerd_STF.Mathematics.Algebra
{
    public interface IVector<TSelf> : IVector<TSelf, double>, INumberGroup<TSelf, double> where TSelf : IVector<TSelf>
    {
        TSelf Normalized { get; }

        void Normalize();
    }
    public interface IVector<TSelf, TNumber> : IMagnitudeOperators<TSelf>,
                                               INumberGroupBase<TNumber>
#if CS11_OR_GREATER
                                              ,IDotOperation<TSelf, TNumber>,
                                             //IMultiplyOperators<TSelf, TNumber, TSelf>, // stupid "unification" problem in INumberGroup if uncommented
                                               ISumOperation<TSelf>
#endif
        where TSelf : IVector<TSelf, TNumber>
#if CS11_OR_GREATER
        where TNumber : INumber<TNumber>
#endif
    {
#if CS11_OR_GREATER
        static abstract TSelf operator *(TSelf a, TNumber b);
        static abstract TSelf operator *(TNumber a, TSelf b);
        static abstract TSelf operator /(TSelf a, TNumber b);
#endif
    }
}
