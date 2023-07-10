namespace Nerd_STF.Mathematics.Abstract;

public interface IMatrix<T> : IAbsolute<T>, ICeiling<T>, IClamp<T>, IDivide<T>,
    IEquatable<T>, IFloor<T>, IGroup2d<float>, ILerp<T, float>, IProduct<T>, IRound<T>,
    ISubtract<T>, ISum<T>
    where T : IMatrix<T>
{
    public T Adjugate();
    public float Determinant();
    public T? Inverse();
    public T Transpose();
}

public interface IMatrix<This, TMinor> : IMatrix<This> where This : IMatrix<This, TMinor>
    where TMinor : IMatrix<TMinor>
{
    public TMinor[,] Minors();
}
