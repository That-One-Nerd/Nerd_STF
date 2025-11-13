#if CS11_OR_GREATER
namespace Nerd_STF.Mathematics
{
    public interface IPresets2d<TSelf> : IPresets1d<TSelf>
        where TSelf : IPresets2d<TSelf>
    {
        static abstract TSelf Down { get; }
        static abstract TSelf Left { get; }
        static abstract TSelf Right { get; }
        static abstract TSelf Up { get; }
    }
}
#endif
