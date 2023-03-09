namespace Nerd_STF.Mathematics.Abstract;

public interface IClamp<T> where T : IClamp<T>
{
    public static abstract T Clamp(T val, T min, T max);
}
