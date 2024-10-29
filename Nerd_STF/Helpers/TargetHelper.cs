using System;

namespace Nerd_STF.Helpers
{
    internal static class TargetHelper
    {
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
