using Nerd_STF.UnitTests.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using static Nerd_STF.UnitTests.Helpers.TestHelperMethods;

namespace Nerd_STF.UnitTests;

[TestClass]
public sealed class ListTupleTests
{
    private static int[] RandomData
    {
        get
        {
            Random rand = Random.Shared;
            int[] expected = new int[rand.Next(8, 32)];
            for (int i = 0; i < expected.Length; i++) expected[i] = rand.Next();
            return expected;
        }
    }

    [TestMethod] public void TestConstructorEmpty()
    {
        ListTuple<int> tuple = new();
        Assert.AreEqual(0, tuple.Length);
        foreach (int _ in tuple) Assert.Fail(); // Should never happen, there are no items.
    }
    [TestMethod] public void TestConstructorIEnumerable()
    {
        int[] expected = RandomData;
        ListTuple<int> tuple = new((IEnumerable<int>)expected);
        Assert.AreEqual(expected.Length, tuple.Length);
        for (int i = 0; i < expected.Length; i++) Assert.AreEqual(expected[i], tuple[i]);
    }
    [TestMethod] public void TestConstructorArray()
    {
        int[] expected = RandomData;
        ListTuple<int> tuple = new(expected);
        Assert.AreEqual(expected.Length, tuple.Length);
        for (int i = 0; i < expected.Length; i++) Assert.AreEqual(expected[i], tuple[i]);
    }
    [TestMethod] public void TestConstructorFill()
    {
        int[] expected = RandomData;
        ListTuple<int> tuple = new(i => expected[i], expected.Length);
        Assert.AreEqual(expected.Length, tuple.Length);
        for (int i = 0; i < expected.Length; i++) Assert.AreEqual(expected[i], tuple[i]);
    }

    [TestMethod] public void TestIndexer()
    {
        // ListTuple[int].get
        int[] expected = RandomData;
        ListTuple<int> tuple = new(expected);
        Assert.AreEqual(expected.Length, tuple.Length);
        for (int i = 0; i < expected.Length; i++) Assert.AreEqual(expected[i], tuple[i]);

        // ListTuple[int].set
        int[] newExpected = new int[expected.Length];
        for (int i = 0; i < expected.Length; i++)
        {
            int val = Random.Shared.Next();
            newExpected[i] = val;
            tuple[i] = val;
        }

        // Verify set.
        for (int i = 0; i < expected.Length; i++) Assert.AreEqual(newExpected[i], tuple[i]);

        // ITuple[int].get
        for (int i = 0; i < expected.Length; i++) Assert.AreEqual(newExpected[i], ((ITuple)tuple)[i]);
    }

    [TestMethod] public void TestToString()
    {
        Assert.AreEqual("(1, 2, 3, 4)", new ListTuple<int>(1, 2, 3, 4).ToString());
    }

    [TestMethod] public void TestHashCode() => TestGetHashCode(() => new ListTuple<int>(RandomData));

    [TestMethod] public void TestEquality()
    {
        // Equals(ListTuple)
        Assert.IsTrue(new ListTuple<int>(1, 2, 3, 4, 5).Equals(new ListTuple<int>(1, 2, 3, 4, 5)));
        Assert.IsTrue(new ListTuple<int>(1, 2, 3, 4, 5) == new ListTuple<int>(1, 2, 3, 4, 5));
        Assert.IsFalse(new ListTuple<int>(1, 2, 3, 4, 5) != new ListTuple<int>(1, 2, 3, 4, 5));

        Assert.IsFalse(new ListTuple<int>(1, 2, 3, 4, 5).Equals(new ListTuple<int>(5, 4, 3, 2, 1)));
        Assert.IsFalse(new ListTuple<int>(1, 2, 3, 4, 5) == new ListTuple<int>(5, 4, 3, 2, 1));
        Assert.IsTrue(new ListTuple<int>(1, 2, 3, 4, 5) != new ListTuple<int>(5, 4, 3, 2, 1));
        Assert.IsFalse(new ListTuple<int>(1, 2, 3, 4, 5).Equals(new ListTuple<int>(1, 2, 3, 4)));
        Assert.IsFalse(new ListTuple<int>(1, 2, 3, 4, 5).Equals(new ListTuple<int>()));
        Assert.IsFalse(new ListTuple<int>().Equals(new ListTuple<int>(1, 2, 3, 4, 5)));

        Assert.IsTrue(new ListTuple<int?>(1, 2, null, 4, 5).Equals(new ListTuple<int?>(1, 2, null, 4, 5)));
        Assert.IsFalse(new ListTuple<int?>(1, 2, null, 4, 5).Equals(new ListTuple<int?>(1, 2, 3, 4, 5)));
        Assert.IsFalse(new ListTuple<int?>(1, 2, 3, 4, 5).Equals(new ListTuple<int?>(1, 2, null, 4, 5)));

        // Equals(object?)
        Assert.IsTrue(new ListTuple<int>(1, 2, 3, 4, 5).Equals((object?)new ListTuple<int>(1, 2, 3, 4, 5)));
        Assert.IsFalse(new ListTuple<int>(1, 2, 3, 4, 5).Equals((object?)null));
        Assert.IsFalse(new ListTuple<int>(1, 2, 3, 4, 5).Equals((object?)5));
    }

    [TestMethod] public void TestToArray()
    {
        int[] expected = RandomData;
        ListTuple<int> tuple = new(expected);

        int[] compare = tuple.ToArray();
        Assert.ArrayEquals(expected, compare);
    }
    [TestMethod] public void TestToList()
    {
        int[] expected = RandomData;
        ListTuple<int> tuple = new(expected);

        List<int> compare = tuple.ToList();
        Assert.ArrayEquals(expected, compare);
    }
    [TestMethod] public void TestToFill()
    {
        int[] expected = RandomData;
        ListTuple<int> tuple = new(expected);

        Fill<int> compare = tuple.ToFill();
        for (int i = 0; i < expected.Length; i++) Assert.AreEqual(expected[i], compare(i));
    }

    [TestMethod] public void TestCasts()
    {
        // ListTuple -> ValueTuple
        Assert.AreEqual<ValueTuple<int>>(new ValueTuple<int>(1), new ListTuple<int>(1));
        Assert.AreEqual<ValueTuple<int, int>>((1, 2), new ListTuple<int>(1, 2));
        Assert.AreEqual<ValueTuple<int, int, int>>((1, 2, 3), new ListTuple<int>(1, 2, 3));
        Assert.AreEqual<ValueTuple<int, int, int, int>>((1, 2, 3, 4), new ListTuple<int>(1, 2, 3, 4));
        Assert.AreEqual<ValueTuple<int, int, int, int, int>>((1, 2, 3, 4, 5), new ListTuple<int>(1, 2, 3, 4, 5));
        Assert.AreEqual<ValueTuple<int, int, int, int, int, int>>((1, 2, 3, 4, 5, 6), new ListTuple<int>(1, 2, 3, 4, 5, 6));
        Assert.AreEqual<ValueTuple<int, int, int, int, int, int, int>>((1, 2, 3, 4, 5, 6, 7), new ListTuple<int>(1, 2, 3, 4, 5, 6, 7));

        // ValueTuple -> ListTuple
        Assert.AreEqual<ListTuple<int>>(new ValueTuple<int>(1), new ListTuple<int>(1));
        Assert.AreEqual<ListTuple<int>>(new ListTuple<int>(1, 2), (1, 2));
        Assert.AreEqual<ListTuple<int>>(new ListTuple<int>(1, 2, 3), (1, 2, 3));
        Assert.AreEqual<ListTuple<int>>(new ListTuple<int>(1, 2, 3, 4), (1, 2, 3, 4));
        Assert.AreEqual<ListTuple<int>>(new ListTuple<int>(1, 2, 3, 4, 5), (1, 2, 3, 4, 5));
        Assert.AreEqual<ListTuple<int>>(new ListTuple<int>(1, 2, 3, 4, 5, 6), (1, 2, 3, 4, 5, 6));
        Assert.AreEqual<ListTuple<int>>(new ListTuple<int>(1, 2, 3, 4, 5, 6, 7), (1, 2, 3, 4, 5, 6, 7));

        // ListTuple <-> T[]
        Assert.ArrayEquals([1, 2, 3, 4, 5, 6, 7, 8], (int[])new ListTuple<int>(1, 2, 3, 4, 5, 6, 7, 8));
        Assert.ArrayEquals(new ListTuple<int>(1, 2, 3, 4, 5, 6, 7, 8), (ListTuple<int>)new int[] { 1, 2, 3, 4, 5, 6, 7, 8 });
    }

    [TestMethod] public void TestEnumerator()
    {
        int[] expected = RandomData;
        ListTuple<int> tuple = new(expected);
        Assert.AreEqual(expected.Length, tuple.Length);

        // Simple enumerator per-value check.
        IEnumerator<int> values = tuple.GetEnumerator();
        for (int i = 0; i < expected.Length; i++)
        {
            Assert.IsTrue(values.MoveNext());
            Assert.AreEqual(expected[i], values.Current);
            Assert.AreEqual(expected[i], ((IEnumerator)values).Current); // Test non-generic interface form.
        }
        Assert.IsFalse(values.MoveNext()); // At the end, there should be no remaining values.

        // When we reset the enumerator, the entire thing should reset.
        values.Reset();
        for (int i = 0; i < expected.Length; i++)
        {
            Assert.IsTrue(values.MoveNext());
            Assert.AreEqual(expected[i], values.Current);
            Assert.AreEqual(expected[i], ((IEnumerator)values).Current); // Test non-generic interface form.
        }
        Assert.IsFalse(values.MoveNext());

        // The value before MoveNext() should not exist.
        values.Reset();
        Assert.Throws<IndexOutOfRangeException>(() => values.Current);

        // There is no typical behavior for Dispose() on an enumerator.
        // Even the built-in ones, like List<T>.Enumerator don't do anything.

        // Test non-generic IEnumerator
        for (int i = 0; i < expected.Length; i++)
        {
            Assert.IsTrue(((IEnumerator)values).MoveNext());
            Assert.AreEqual(expected[i], values.Current);
            Assert.AreEqual(expected[i], ((IEnumerator)values).Current); // Test non-generic interface form.
        }
        Assert.IsFalse(values.MoveNext());

        // One more enumerator test.
        Assert.EnumeratorEquals(tuple.GetEnumerator(), ((IEnumerable)tuple).GetEnumerator());
    }
}
