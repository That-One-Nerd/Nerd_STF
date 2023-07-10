namespace Nerd_STF;

public interface IGroup2d<T> : IGroup<T>
{
    public T[,] ToArray2D();
    public Fill2d<T> ToFill2D();
}
