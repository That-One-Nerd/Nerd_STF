namespace Nerd_STF.Mathematics.Abstract;

public interface IPresets4d<T> : IPresets2d<T>, IPresets3d<T> where T : IPresets4d<T>
{
    public static abstract T HighW { get; }
    public static abstract T LowW { get; }
}
