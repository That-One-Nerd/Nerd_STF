namespace Nerd_STF.Mathematics.Abstract;

public interface IPresets0d<T> where T : IPresets0d<T>
{
    public static abstract T Unit { get; }
}
