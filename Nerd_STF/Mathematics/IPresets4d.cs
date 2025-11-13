#if CS11_OR_GREATER
namespace Nerd_STF.Mathematics
{
    public interface IPresets4d<TSelf> : IPresets3d<TSelf>
        where TSelf : IPresets4d<TSelf>
    {
        // TODO: The HighW and LowW vectors could also be called "ana" and "kata."
        static abstract TSelf LowW { get; } // Kata
        static abstract TSelf HighW { get; } // Ana
    }
}
#endif
