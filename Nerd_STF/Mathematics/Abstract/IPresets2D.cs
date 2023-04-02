namespace Nerd_STF.Mathematics.Abstract;

public interface IPresets2d<T> : IPresets1d<T> where T : IPresets2d<T>
{
    public static abstract T Down { get; }
    public static abstract T Left { get; }
    public static abstract T Right { get; }
    public static abstract T Up { get; }
}
