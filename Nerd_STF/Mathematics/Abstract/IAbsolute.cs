namespace Nerd_STF.Mathematics.Abstract;

public interface IAbsolute<T> where T : IAbsolute<T>
{
    public static abstract T Absolute(T val);
}
