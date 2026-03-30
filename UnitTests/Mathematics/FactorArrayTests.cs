using Nerd_STF.Mathematics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using static Nerd_STF.UnitTests.TestHelperMethods;

namespace Nerd_STF.UnitTests.Mathematics;

[TestClass]
public sealed class FactorArrayTests
{
    [TestMethod] public void TestGenerateFactors()
    {
        // MathE.PrimeFactorsE and FactorArray.GetPrimeFactors
        // should always be equivalent.
        for (int i = 0; i < 1000; i++)
        {
            int num = RandomInput(out int[] expected);
            AssertArrayEquals(expected, MathE.PrimeFactorsE(num));
            AssertArrayEquals(expected, FactorArray.GetPrimeFactors(num));
        }
    }

    [TestMethod] public void TestFactorCount()
    {
        FactorArray factors;
        for (int i = 0; i < 1000; i++)
        {
            factors = FactorArray.GetPrimeFactors(RandomInput(out int[] expected));

            Assert.AreEqual(expected.Length, factors.Count);
            Assert.AreEqual(expected.Distinct().Count(), factors.Distinct);
        }
    }

    [TestMethod] public void TestEnumerator()
    {
        FactorArray factors;
        for (int i = 0; i < 1000; i++)
        {
            factors = FactorArray.GetPrimeFactors(RandomInput(out int[] expected));
            AssertArrayEquals(expected, factors);
            AssertEnumeratorEquals(factors.GetEnumerator(), ((IEnumerable)factors).GetEnumerator());
        }
    }

    [TestMethod] public void TestFactors()
    {
        FactorArray factors;
        for (int i = 0; i < 1000; i++)
        {
            factors = FactorArray.GetPrimeFactors(RandomInput(out int[] expected));

            // Test the dictionary (all factors).
            int index = 0;
            foreach (KeyValuePair<int, int> factor in factors.GetFactors())
            {
                if (factor.Value < 1) Assert.Fail(); // Should never happen.

                for (int j = 0; j < factor.Value; j++, index++)
                {
                    Assert.AreEqual(expected[index], factor.Key);
                }
            }
            Assert.AreEqual(expected.Length, index);

            // Assert distinct factors.
            AssertArrayEquals(expected.Distinct(), factors.GetDistinctFactors());

            // Test multiplicity.
            index = 0;
            int count = 0, lastNum = -1;
            do
            {
                int n = index == expected.Length ? -1 : expected[index];
                index++;
                if (n != lastNum)
                {
                    if (lastNum != -1)
                    {
                        Assert.AreEqual(count, factors.GetMultiplicity(lastNum));
                    }
                    lastNum = n;
                    count = 1;
                }
                else count++;
            }
            while (index <= expected.Length);
        }
    }

    [TestMethod] public void TestConversions()
    {
        FactorArray factors;
        for (int i = 0; i < 1000; i++)
        {
            factors = FactorArray.GetPrimeFactors(RandomInput(out int[] expected));
            AssertArrayEquals(expected, factors.ToArray());
            AssertArrayEquals(expected, factors.ToList());

            // Technically, ToFill() tests all of these methods at once.
            // But it's still good to test individually.
            Fill<int> fill = factors.ToFill();
            for (int j = 0; j < expected.Length; j++)
            {
                Assert.AreEqual(expected[j], fill(j));
            }
            Assert.Throws<Exception>(() => fill(expected.Length));
        }
    }

    private static int RandomInput(out int[] expected)
    {
        Random rand = Random.Shared;
        int num = rand.Next(1, 100_000);

        // Crude prime factors solver.
        // Although, I'll be honest, it's quite a similar implementation to
        // MathE.PrimeFactorsE at the time of writing.
        int check = num;
        List<int> expectedList = [];
        int compare = 2;
        while (check > 1)
        {
            while (check % compare == 0)
            {
                expectedList.Add(compare);
                check /= compare;
            }
            compare++;
        }
        expected = [.. expectedList];
        return num;
    }
}
