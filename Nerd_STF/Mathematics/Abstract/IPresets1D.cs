namespace Nerd_STF.Mathematics.Abstract;

public interface IPresets1d<T> where T : IPresets1d<T>
{
    public static abstract T One { get; }
    public static abstract T Zero { get; }
}
