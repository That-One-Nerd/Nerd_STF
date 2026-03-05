using Nerd_STF.Mathematics;
using System;
using System.Numerics;

namespace Nerd_STF.UnitTests;

[TestClass]
public sealed class MathTests
{
    [TestMethod] public void TestIntAbs()
    {
        // Test 10,000 randomly sampled integers.
        // There should always be parity with Math.Abs
        Random rand = new();
        for (int i = 0; i < 10_000; i++)
        {
            int number = rand.Next();
            int expected = number < 0 ? -number : number;

            Assert.AreEqual(expected, MathE.Abs(number), $"{nameof(MathE.Abs)} is wrong.");
            Assert.AreEqual(Math.Abs(number), MathE.Abs(number), $"{nameof(MathE.Abs)} does not have parity with {nameof(Math.Abs)}.");
        }
    }
    [TestMethod] public void TestDoubleAbs()
    {
        // Test 10,000 randomly sampled doubles with range [-1e5,1e5).
        // There should always be parity with Math.Abs
        Random rand = new();
        for (int i = 0; i < 10_000; i++)
        {
            double number = rand.NextDouble() * 2e5 - 1e5;
            double expected = number < 0 ? -number : number;

            // Tolerance of exactly 0 here.
            Assert.AreEqual(expected, MathE.Abs(number), 0, $"{nameof(MathE.Abs)} is wrong.");
            Assert.AreEqual(Math.Abs(number), MathE.Abs(number), 0, $"{nameof(MathE.Abs)} does not have parity with {nameof(Math.Abs)}.");
        }
    }
    [TestMethod] public void TestGenericAbs()
    {
        // Just try a bunch.
        TestAbs<sbyte>();
        TestAbs<short>();
        TestAbs<int>();
        TestAbs<long>();
        TestAbs<Int128>();
        TestAbs<Half>();
        TestAbs<float>();
        TestAbs<double>();
        TestAbs<decimal>();
    }
    private void TestAbs<T>() where T : INumber<T>
    {
        // Test 10,000 randomly sampled doubles with range [-1e5,1e5).
        // Convert to the desired type, trucating if necessary.
        Random rand = new();
        for (int i = 0; i < 10_000; i++)
        {
            double rawNumber = rand.NextDouble() * 2e5 - 1e5;

            T number = T.CreateTruncating(rawNumber);
            T expected = number < T.Zero ? -number : number;
            Assert.AreEqual(expected, MathE.Abs(number), $"{nameof(MathE.Abs)} is wrong.");
            // Can't assert parity since Math.Abs isn't guaranteed to support this type.
        }
    }
}
