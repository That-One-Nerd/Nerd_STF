using System.Numerics;

namespace Nerd_STF.Mathematics.Abstract;

public interface IProduct<T> : IMultiplyOperators<T, T, T>
    where T : IProduct<T>
{
    public static abstract T Product(params T[] vals);
}
