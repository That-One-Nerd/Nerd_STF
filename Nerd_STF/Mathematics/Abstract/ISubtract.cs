using System.Numerics;

namespace Nerd_STF.Mathematics.Abstract;

public interface ISubtract<T> : ISubtractionOperators<T, T, T> where T : ISubtract<T>
{
    public static abstract T Subtract(T num, params T[] vals);
}
