namespace Nerd_STF.Mathematics.Abstract;

public interface IStaticMatrix<T> : IAverage<T>, IMatrix<T>, IMedian<T>,
    IMatrixPresets<T> where T : IStaticMatrix<T>
{
    
}
