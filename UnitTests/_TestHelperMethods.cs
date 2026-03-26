using System;
using System.Collections.Generic;

namespace Nerd_STF.UnitTests;

internal static class TestHelperMethods
{
    public static void AssertArrayEquals<T>(IEnumerable<T> expected, IEnumerable<T> actual, string message = "")
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
}
