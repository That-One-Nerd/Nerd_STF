using System.Numerics;

namespace Nerd_STF.Mathematics.Abstract;

public interface ISum<T> : IAdditionOperators<T, T, T>
    where T : ISum<T>
{
    public static abstract T Sum(params T[] vals);
}
