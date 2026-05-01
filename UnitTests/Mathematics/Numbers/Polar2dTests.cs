using Nerd_STF.Mathematics;
using Nerd_STF.Mathematics.Algebra;
using Nerd_STF.Mathematics.Numbers;
using Nerd_STF.UnitTests.Helpers;
using System;
using static Nerd_STF.UnitTests.Helpers.TestHelperMethods;

namespace Nerd_STF.UnitTests.Mathematics.Numbers;

[TestClass]
public sealed class Polar2dTests
{
    public static Polar2d RandomData
    {
        get
        {
            Random rand = Random.Shared;
            return new(10 * rand.NextDouble(), Angle.FromRevolutions(rand.NextDouble()));
        }
    }

    [TestMethod] public void TestConstants()
    {
        // Constants should have parity with themselves.
        Assert.AreEqual(Polar2d.Down, -Polar2d.Up);
        Assert.AreEqual(Polar2d.Left, -Polar2d.Right);
        Assert.AreEqual(Polar2d.Zero, Polar2d.Down + Polar2d.Up);
        Assert.AreEqual(Polar2d.Zero, Polar2d.Left + Polar2d.Right);
    }

    [TestMethod] public void TestConstructors()
    {
        Random rand = Random.Shared;
        for (int i = 0; i < BulkTestCount; i++)
        {
            // 6.29 is intentional here. It's better to overshoot our
            // input than to slightly undershoot it.
            double aV = 6.29 * rand.NextDouble(), m = 10 * rand.NextDouble();
            Angle a = Angle.FromRadians(aV);

            Assert.AreEqual(a, new Polar2d(m, a).a);
            Assert.AreEqual(m, new Polar2d(m, a).m);
            Assert.AreEqual(a, new Polar2d(m, aV).a);
            Assert.AreEqual(m, new Polar2d(m, aV).m);

            double fill(int i) => i switch
            {
                0 => m,
                1 => aV,
                _ => throw new ArgumentOutOfRangeException()
            };

            Assert.AreEqual(a, new Polar2d(fill).a);
            Assert.AreEqual(m, new Polar2d(fill).m);
        }
    }

    [TestMethod] public void TestProperties()
    {
        Random rand = Random.Shared;
        for (int i = 0; i < BulkTestCount; i++)
        {
            double m = 10 * rand.NextDouble();
            Angle a = Angle.FromRevolutions(rand.NextDouble());

            Polar2d val = new(m, a);

            // Test getting properties.
            Assert.AreEqual(a, val.a);
            Assert.AreEqual(m, val.m);
            Assert.AreEqual(a, val.Theta);
            Assert.AreEqual(m, val.Magnitude);

            // Test setting properties.
            val.Theta += Angle.Quarter;
            val.Magnitude += 1;
            Assert.AreEqual(a + Angle.Quarter, val.a);
            Assert.AreEqual(m + 1, val.m);

            // Test that Magnitude^2 == MagnitudeSqr.
            Assert.AreEqual(((IMagnitudeOperators<Polar2d>)val).MagnitudeSqr, val.Magnitude * val.Magnitude);

            // Test normalizing makes Magnitude == 1.
            Assert.AreEqual(1, val.Normalized.Magnitude);
        }
    }

    [TestMethod] public void TestOperators()
    {
        Random rand = Random.Shared;
        for (int i = 0; i < BulkTestCount; i++)
        {
            Polar2d a = RandomData, b = RandomData;
            double c = rand.NextDouble();

            // Compare parity with Float2.
            Assert.AreEqual(a.ToXyz() + b.ToXyz(), (a + b).ToXyz(), Compare.Float2());
            Assert.AreEqual(-(a.ToXyz()), (-a).ToXyz(), Compare.Float2());
            Assert.AreEqual(a.ToXyz() - b.ToXyz(), (a - b).ToXyz(), Compare.Float2());
            Assert.AreEqual(a.ToXyz() * c, (a * c).ToXyz(), Compare.Float2());
            Assert.AreEqual(c * a.ToXyz(), (c * a).ToXyz(), Compare.Float2());
            Assert.AreEqual(a.ToXyz() / c, (a / c).ToXyz(), Compare.Float2());
        }
    }
}
