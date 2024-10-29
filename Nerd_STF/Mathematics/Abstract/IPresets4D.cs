#if CS11_OR_GREATER
namespace Nerd_STF.Mathematics.Abstract
{
    public interface IPresets4d<TSelf> : IPresets3d<TSelf>
        where TSelf : IPresets4d<TSelf>
    {
        public static abstract TSelf LowW { get; }
        public static abstract TSelf HighW { get; }
    }
}
#endif
