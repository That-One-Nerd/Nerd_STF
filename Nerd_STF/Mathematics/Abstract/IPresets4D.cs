namespace Nerd_STF.Mathematics.Abstract;

public interface IPresets4D<T> : IPresets2D<T>, IPresets3D<T> where T : IPresets4D<T>
{
    public static abstract T HighW { get; }
    public static abstract T LowW { get; }
}
