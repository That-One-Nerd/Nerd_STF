#if CS11_OR_GREATER
namespace Nerd_STF.Abstract
{
    public interface IPresets1d<TSelf> where TSelf : IPresets1d<TSelf>
    {
        static abstract TSelf One { get; }
        static abstract TSelf Zero { get; }
    }
}
#endif
