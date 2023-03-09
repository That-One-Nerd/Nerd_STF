namespace Nerd_STF.Mathematics.Abstract;

public interface IPresets2D<T> : IPresets1D<T> where T : IPresets2D<T>
{
    public static abstract T Down { get; }
    public static abstract T Left { get; }
    public static abstract T Right { get; }
    public static abstract T Up { get; }
}
