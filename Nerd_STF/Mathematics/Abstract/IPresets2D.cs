#if CS11_OR_GREATER
namespace Nerd_STF.Mathematics.Abstract
{
    public interface IPresets2d<TSelf> : IPresets1d<TSelf>
        where TSelf : IPresets2d<TSelf>
    {
        public static abstract TSelf Down { get; }
        public static abstract TSelf Left { get; }
        public static abstract TSelf Right { get; }
        public static abstract TSelf Up { get; }
    }
}
#endif
