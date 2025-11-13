#if CS11_OR_GREATER
namespace Nerd_STF.Mathematics
{
    public interface IRefRoundable<TSelf>
        where TSelf : IRefRoundable<TSelf>
    {
        static abstract void Ceiling(ref TSelf val);
        static abstract void Floor(ref TSelf val);
        static abstract void Round(ref TSelf val);
    }
}
#endif