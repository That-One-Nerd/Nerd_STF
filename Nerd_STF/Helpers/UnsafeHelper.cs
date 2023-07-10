namespace Nerd_STF.Helpers;

// These are all the unsafe functions I couldn't make safe. I don't want too much
// unsafe code, so this is where I put all of it that I require.
internal static unsafe class UnsafeHelper
{
    // Forcefully change the type of an object
    // without changing the data of the object.
    public static NT SwapType<CT, NT>(CT obj)
        where CT : unmanaged
        where NT : unmanaged
        => *(NT*)&obj;
}
