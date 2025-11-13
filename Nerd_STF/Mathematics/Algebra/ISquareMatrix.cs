namespace Nerd_STF.Mathematics.Algebra
{
    public interface ISquareMatrix<TSelf> : IMatrix<TSelf>
        where TSelf : ISquareMatrix<TSelf>
    {
        double Trace();
    }
}
