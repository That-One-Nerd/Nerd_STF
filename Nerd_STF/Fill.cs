using System;
using System.Collections.Generic;

namespace Nerd_STF
{
    public delegate T Fill<T>(int index);
    public delegate T Fill2d<T>(int x, int y);

    public static class FillExtensions
    {
        public static IEnumerable<T> Enumerate<T>(this Fill<T> fill) => Enumerate(fill, int.MaxValue);
        public static IEnumerable<T> Enumerate<T>(this Fill<T> fill, int max)
        {
            int i = 0;
            T obj;
            while (i < max)
            {
                try { obj = fill(i++); }
                catch (ArgumentOutOfRangeException) { yield break; }
                yield return obj;
            }
        }
    }
}
