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
            Assert.ArrayEquals(expected, MathE.PrimeFactorsE(num));
            Assert.ArrayEquals(expected, FactorArray.GetPrimeFactors(num));
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
            Assert.ArrayEquals(expected, factors);
            Assert.EnumeratorEquals(factors.GetEnumerator(), ((IEnumerable)factors).GetEnumerator());
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
            Assert.ArrayEquals(expected.Distinct(), factors.GetDistinctFactors());

            // Test multiplicity and IsFactor.
            index = 0;
            int count = 0, lastNum = -1;
            do
            {
                int n;
                if (index == expected.Length) n = -1;
                else
                {
                    n = expected[index];
                    Assert.IsTrue(factors.IsFactor(n));
                }
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
            Assert.ArrayEquals(expected, factors.ToArray());
            Assert.ArrayEquals(expected, factors.ToList());

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

    [TestMethod] public void TestEquals()
    {
        FactorArray factors;
        for (int i = 0; i < 1000; i++)
        {
            int num1 = RandomInput(out _), num2;
            do { num2 = RandomInput(out _); } while (num1 == num2);

            factors = FactorArray.GetPrimeFactors(num1);
            Assert.IsTrue(factors.Equals(factors));                                    // Equals(FactorArray)
            Assert.IsTrue(factors.Equals(FactorArray.GetPrimeFactors(num1)));          // Equals(FactorArray)
            Assert.IsTrue(factors.Equals((object)FactorArray.GetPrimeFactors(num1)));  // Equals(object)
            Assert.IsTrue(factors == FactorArray.GetPrimeFactors(num1));               // FactorArray == FactorArray
            Assert.IsTrue(factors != FactorArray.GetPrimeFactors(num2));               // FactorArray != FactorArray

            Assert.IsFalse(factors.Equals(FactorArray.GetPrimeFactors(num2)));         // Equals(FactorArray)
            Assert.IsFalse(factors.Equals(null));                                      // Equals(FactorArray)
            Assert.IsFalse(factors.Equals((object)FactorArray.GetPrimeFactors(num2))); // Equals(object)
            Assert.IsFalse(factors.Equals((object?)null));                             // Equals(object)
            Assert.IsFalse(factors == FactorArray.GetPrimeFactors(num2));              // FactorArray == FactorArray
            Assert.IsFalse(factors != FactorArray.GetPrimeFactors(num1));              // FactorArray != FactorArray
        }
    }

    [TestMethod] public void TestHashCode() => TestGetHashCode(() => FactorArray.GetPrimeFactors(RandomInput(out _)));

    [TestMethod] public void TestToString()
    {
        // Hard-coded example that tests most of the possibilities.
        Assert.AreEqual("2^3 * 3 * 5^2 * 7^2 * 11 * 19^2", FactorArray.GetPrimeFactors(116_747_400).ToString());
        Assert.AreEqual("50087", FactorArray.GetPrimeFactors(50087).ToString());
    }

    [TestMethod] public void TestCasts()
    {
        FactorArray factors;
        for (int i = 0; i < 1000; i++)
        {
            factors = FactorArray.GetPrimeFactors(RandomInput(out int[] expected));
            Assert.ArrayEquals(expected, (int[])factors);
            Assert.ArrayEquals(expected, (List<int>)factors);
            Assert.ArrayEquals(expected, (ListTuple<int>)factors);
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
