namespace Nerd_STF.Mathematics.Abstract;

public interface IProjectionMatrix<TThis, TBaseMatrix, TDim> : IMatrix<TThis>
    where TThis : IProjectionMatrix<TThis, TBaseMatrix, TDim>
    where TBaseMatrix : IMatrix<TBaseMatrix>
    where TDim : IGroup<float>
{
    public Fill<TDim> Project(Fill<TDim> toProject);
}
