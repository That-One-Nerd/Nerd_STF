#if CS11_OR_GREATER
namespace Nerd_STF.Mathematics
{
    public interface IPresets4d<TSelf> : IPresets3d<TSelf>
        where TSelf : IPresets4d<TSelf>
    {
        static abstract TSelf LowW { get; }
        static abstract TSelf HighW { get; }
    }
}
#endif
