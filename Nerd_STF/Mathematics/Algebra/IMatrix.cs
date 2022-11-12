namespace Nerd_STF.Mathematics.Algebra;

public interface IMatrix<T> : ICloneable, IEnumerable, IEquatable<T>, IGroup2D<float>
    where T : IMatrix<T>
{
    public T Adjugate();
    public float Determinant();
    public T Inverse();
    public T Transpose();
}

public interface IMatrix<This, TMinor> : IMatrix<This> where This : IMatrix<This, TMinor>
    where TMinor : IMatrix<TMinor>
{
    public TMinor[,] Minors();
}
