using System;
using System.Collections;
using System.Collections.Generic;

namespace Nerd_STF.UnitTests;

[TestClass]
public sealed class ListTupleTests
{
    [TestMethod]
    public void TestIndexer()
    {
        Random rand = new();
        int[] expected = new int[rand.Next(8, 32)];
        for (int i = 0; i < expected.Length; i++) expected[i] = rand.Next();

        ListTuple<int> tuple = new(expected);
        Assert.AreEqual(expected.Length, tuple.Length);
        for (int i = 0; i < expected.Length; i++)
        {
            Assert.AreEqual(expected[i], tuple[i]);
        }
    }

    [TestMethod]
    public void TestEnumerator()
    {
        Random rand = new();
        int[] expected = new int[rand.Next(8, 32)];
        for (int i = 0; i < expected.Length; i++) expected[i] = rand.Next();

        ListTuple<int> tuple = new(expected);
        Assert.AreEqual(expected.Length, tuple.Length);

        // Simple enumerator per-value check.
        ListTuple<int>.Enumerator values = tuple.GetEnumerator();
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
    }
}
