using Nerd_STF.Mathematics;
using System;
using System.Collections.Generic;
using System.Numerics;
using static Nerd_STF.UnitTests.TestHelperMethods;

namespace Nerd_STF.UnitTests.Mathematics;

[TestClass]
public sealed class MathETests
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

    [TestMethod] public void TestGeneratePrimes()
    {
        // Manually check a few inputs.
        Assert.ArrayEquals([2, 3], MathE.GeneratePrimes(4));
        Assert.ArrayEquals([2, 3, 5, 7], MathE.GeneratePrimes(10));
        Assert.ArrayEquals([2, 3, 5, 7, 11], MathE.GeneratePrimes(11));
        Assert.ArrayEquals([2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31], MathE.GeneratePrimes(31));

        // Use a more basic prime detection system to determine the rest.
        // Should always be in parity.
        Assert.ArrayEquals(GetPrimesBasic(10000), MathE.GeneratePrimes(10000));

        static IEnumerable<int> GetPrimesBasic(int maximum)
        {
            for (int i = 2; i <= maximum; i++)
            {
                bool prime = true;
                for (int j = 2; j < i; j++)
                {
                    if (i % j == 0)
                    {
                        prime = false;
                        break;
                    }
                }
                if (prime) yield return i;
            }
        }
    }
}
