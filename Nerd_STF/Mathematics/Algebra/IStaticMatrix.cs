#if CS11_OR_GREATER
namespace Nerd_STF.Mathematics.Algebra
{
    public interface IStaticMatrix<TSelf> : IPresets1d<TSelf>, IMatrix<TSelf>
        where TSelf : IStaticMatrix<TSelf>
    {
        static abstract TSelf Identity { get; }
        static abstract TSelf SignField { get; }
    }
}
#endif
