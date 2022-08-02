namespace Nerd_STF;

public interface IGroup2D<T> : IGroup<T>
{
    public T[,] ToArray2D();
    public Fill2D<T> ToFill2D();
}
