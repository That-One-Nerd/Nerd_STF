using System.Collections;
using System.Collections.Generic;

namespace Nerd_STF.UnitTests;

internal static partial class _AssertExtensions
{
    extension(Assert)
    {
        public static void ArrayEquals<T>(IEnumerable<T> expected, IEnumerable<T> actual, string message = "")
        {
            IEnumerator<T> e1 = expected.GetEnumerator();
            IEnumerator<T> e2 = actual.GetEnumerator();

            while (e1.MoveNext())
            {
                if (!e2.MoveNext()) Assert.Fail(message); // len(e1) > len(e2)
                Assert.AreEqual(e1.Current, e2.Current, message, e1.Current?.ToString() ?? "", e2.Current?.ToString() ?? "");
            }
            if (e2.MoveNext()) Assert.Fail(message);      // len(e1) < len(e2)
        }

        public static void EnumeratorEquals(IEnumerator expected, IEnumerator actual, string message = "")
        {
            while (expected.MoveNext())
            {
                if (!actual.MoveNext()) Assert.Fail(message); // len(e1) > len(e2)
                Assert.AreEqual(expected.Current, actual.Current, message, expected.Current?.ToString() ?? "", actual.Current?.ToString() ?? "");
            }
            if (actual.MoveNext()) Assert.Fail(message);      // len(e1) < len(e2)
        }
    }
}
