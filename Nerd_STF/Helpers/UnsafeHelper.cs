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

    // Fast square root thing. Uses the classic Quake 3 algorithm.
    // I'll be preserving variable names and comments for funzies.
    public static float Q_rsqrt(float number)
    {
        int i;
        float x2, y;
        const float threehalfs = 1.5F;

        x2 = number * 0.5F;
        y = number;
        i = * ( int * ) &y;                        // evil floating point bit hack
        i = 0x5f3759df - ( i >> 1 );               // what the fuck?
        y = * ( float * ) &i;
        y = y * ( threehalfs - ( x2 * y * y ) );   // 1st iteration
    //  y = y * ( threehalfs - ( x2 * y * y ) );   // 2nd iteration, can be removed.

        return y;
    }
}
