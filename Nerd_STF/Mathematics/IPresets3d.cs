#if CS11_OR_GREATER
namespace Nerd_STF.Mathematics
{
    public interface IPresets3d<TSelf> : IPresets2d<TSelf>
        where TSelf : IPresets3d<TSelf>
    {
        static abstract TSelf Backward { get; }
        static abstract TSelf Forward { get; }
    }
}
#endif
