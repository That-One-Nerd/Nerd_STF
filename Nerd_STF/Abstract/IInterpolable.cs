#if CS11_OR_GREATER
namespace Nerd_STF.Abstract
{
    public interface IInterpolable<TSelf>
        where TSelf : IInterpolable<TSelf>
    {
        static abstract TSelf Lerp(TSelf a, TSelf b, double t, bool clamp = true);
    }
}
#endif
