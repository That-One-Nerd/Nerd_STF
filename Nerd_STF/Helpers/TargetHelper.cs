using System;
using System.Runtime.CompilerServices;

namespace Nerd_STF.Helpers
{
    internal static class TargetHelper
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsFinite(double d)
        {
#if CS11_OR_GREATER
            return double.IsFinite(d);
#else
            long bits = BitConverter.DoubleToInt64Bits(d);
            return (bits & 0x7FFFFFFFFFFFFFFF) < 0x7FF0000000000000;
#endif
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsInfinity(double d)
        {
#if CS11_OR_GREATER
            return double.IsInfinity(d);
#else
            long bits = BitConverter.DoubleToInt64Bits(d);
            return (bits & 0x7FFFFFFFFFFFFFFF) == 0x7FF0000000000000;
#endif
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteLine(string content)
        {
#if NETSTANDARD1_1
#else
            Console.WriteLine(content);
#endif
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[] EmptyArray<T>()
        {
#if NETSTANDARD1_1
            return new T[0];
#else
            return Array.Empty<T>();
#endif
        }
    }
}
