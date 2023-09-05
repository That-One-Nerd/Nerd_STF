namespace Nerd_STF.Mathematics.Abstract;

public interface IStaticMatrix<T> : IAverage<T>, IEquatable<T>, IMatrix<T>, IMedian<T>,
    IMatrixPresets<T> where T : IStaticMatrix<T>
{
    
}
