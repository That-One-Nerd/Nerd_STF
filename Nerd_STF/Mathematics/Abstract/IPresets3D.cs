namespace Nerd_STF.Mathematics.Abstract;

public interface IPresets3D<T> : IPresets2D<T> where T : IPresets3D<T>
{
    public static abstract T Back { get; }
    public static abstract T Forward { get; }
}
