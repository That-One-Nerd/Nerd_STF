using System.Numerics;

namespace Nerd_STF.Mathematics.Abstract;

public interface IDivide<T> : IDivisionOperators<T, T, T> where T : IDivide<T>
{
    public static abstract T Divide(T num, params T[] vals);
}
