#if CS11_OR_GREATER
namespace Nerd_STF.Mathematics.Algebra
{
    public interface IMatrixCast<TSelf>
        where TSelf : IMatrixCast<TSelf>
    {
        static abstract implicit operator Matrix(TSelf self);
    }
}
#endif
