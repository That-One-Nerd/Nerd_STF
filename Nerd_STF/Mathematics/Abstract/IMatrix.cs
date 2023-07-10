namespace Nerd_STF.Mathematics.Abstract;

public interface IMatrix<T> : IAbsolute<T>, ICeiling<T>, IClamp<T>, IDivide<T>,
    IEquatable<T>, IFloor<T>, IGroup2d<float>, ILerp<T, float>, IProduct<T>, IRound<T>,
    ISubtract<T>, ISum<T>
    where T : IMatrix<T>
{
    public T Adjugate();
    public T Cofactor();
    public float Determinant();
    public T? Inverse();
    public T Transpose();

    public T AddRow(int rowToChange, int referenceRow, float factor = 1);
    public T ScaleRow(int rowIndex, float value);
    public T SwapRows(int rowA, int rowB);

    //public T SolveRowEchelon();
}

public interface IMatrix<This, TMinor> : IMatrix<This> where This : IMatrix<This, TMinor>
    where TMinor : IMatrix<TMinor>
{
    public TMinor[,] Minors();
}
