using System.Numerics;

namespace Nerd_STF.Mathematics.Abstract;

public interface IMathOperators<TSelf> : IAdditionOperators<TSelf, TSelf, TSelf>,
    ISubtractionOperators<TSelf, TSelf, TSelf>, IMultiplyOperators<TSelf, TSelf, TSelf>,
    IDivisionOperators<TSelf, TSelf, TSelf>
    where TSelf : IMathOperators<TSelf> { }
