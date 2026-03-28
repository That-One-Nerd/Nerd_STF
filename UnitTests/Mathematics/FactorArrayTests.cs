using Nerd_STF.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nerd_STF.UnitTests.Mathematics;

[TestClass]
public sealed class FactorArrayTests
{
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
