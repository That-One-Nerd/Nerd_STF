#if CS11_OR_GREATER
namespace Nerd_STF.Mathematics.Abstract
{
    public interface IPresets3d<TSelf> : IPresets2d<TSelf>
        where TSelf : IPresets3d<TSelf>
    {
        public static abstract TSelf Backward { get; }
        public static abstract TSelf Forward { get; }
    }
}
#endif
