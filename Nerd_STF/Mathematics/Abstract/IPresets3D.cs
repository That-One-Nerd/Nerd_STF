namespace Nerd_STF.Mathematics.Abstract;

public interface IPresets3d<T> : IPresets2d<T> where T : IPresets3d<T>
{
    public static abstract T Back { get; }
    public static abstract T Forward { get; }
}
