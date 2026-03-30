using Nerd_STF.Mathematics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

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

    public static void AssertEnumeratorEquals(IEnumerator expected, IEnumerator actual, string message = "")
    {
        while (expected.MoveNext())
        {
            if (!actual.MoveNext()) Assert.Fail(message); // len(e1) > len(e2)
            Assert.AreEqual(expected.Current, actual.Current, message, expected.Current?.ToString() ?? "", actual.Current?.ToString() ?? "");
        }
        if (actual.MoveNext()) Assert.Fail(message);      // len(e1) < len(e2)
    }

    public static void TestGetHashCode<T>(Func<T> construct)
        where T : IEquatable<T>
    {
        // I dunno what a better GetHashCode test would be,
        // I don't use this function very often.
        for (int i = 0; i < 1000; i++)
        {
            // Technically this is not a correct test.
            // Two non-equal values are allowed to have the same hash code,
            // it should just be exceptionally rare. And this may happen sometimes,
            // meaning very occasionally the test will fail, then succeed on another attempt.
            T a = construct(),
              b = construct();
            if (a.Equals(b)) Assert.AreEqual(a.GetHashCode(), b.GetHashCode());
            else Assert.AreNotEqual(a.GetHashCode(), b.GetHashCode());
        }
    }

    public static IEnumerable<Type> GetTypesOfInterface(Type @interface)
    {
        if (!@interface.IsInterface) throw new ArgumentException("Expected an interface type.", nameof(@interface));

        // Get all types in Nerd_STF and return all that implement this interface.
        Assembly asm = Assembly.GetAssembly(typeof(MathE))!;
        return from t in asm.GetTypes()
               where t.GetInterface(@interface.FullName!) is not null
               select t;
    }
}
