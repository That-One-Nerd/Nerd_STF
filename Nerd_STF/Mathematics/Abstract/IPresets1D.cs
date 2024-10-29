#if CS11_OR_GREATER
namespace Nerd_STF.Mathematics.Abstract
{
    public interface IPresets1d<TSelf> where TSelf : IPresets1d<TSelf>
    {
        public static abstract TSelf One { get; }
        public static abstract TSelf Zero { get; }
    }
}
#endif
