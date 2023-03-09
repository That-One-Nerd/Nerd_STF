namespace Nerd_STF.Mathematics.Abstract;

public interface IMatrixPresets<T> where T : IMatrix<T>, IMatrixPresets<T>
{
    public static abstract T Identity { get; }
    public static abstract T One { get; }
    public static abstract T SignGrid { get; }
    public static abstract T Zero { get; }
}
