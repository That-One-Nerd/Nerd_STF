namespace Nerd_STF.Mathematics.Abstract;

public interface IPresets1D<T> where T : IPresets1D<T>
{
    public static abstract T One { get; }
    public static abstract T Zero { get; }
}
