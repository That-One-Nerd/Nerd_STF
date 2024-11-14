namespace Nerd_STF.Mathematics.Algebra
{
    public interface ISubmatrixOperations<TSelf, TSmaller>
        where TSelf : IMatrix<TSelf>, ISubmatrixOperations<TSelf, TSmaller>
        where TSmaller : IMatrix<TSmaller>
    {
        TSmaller Submatrix(int r, int c);
    }
}
