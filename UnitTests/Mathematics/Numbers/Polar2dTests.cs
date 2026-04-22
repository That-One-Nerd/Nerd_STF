using Nerd_STF.Mathematics;
using Nerd_STF.Mathematics.Numbers;
using System;

namespace Nerd_STF.UnitTests.Mathematics.Numbers;

[TestClass]
public class Polar2dTests
{
    [TestMethod] public void TestConstants()
    {
        // Constants should have parity with themselves.
        Assert.AreEqual(Polar2d.Down, -Polar2d.Up);
        Assert.AreEqual(Polar2d.Left, -Polar2d.Right);
        Assert.AreEqual(Polar2d.Zero, Polar2d.Down + Polar2d.Up);
        Assert.AreEqual(Polar2d.Zero, Polar2d.Left + Polar2d.Right);
    }

    [TestMethod] public void TestSimpleProperties()
    {
        Random rand = Random.Shared;
        for (int i = 0; i < 10_000; i++)
        {
            Angle a = Angle.FromRevolutions(rand.NextDouble());
            double m = 10 * rand.NextDouble();

            Polar2d val = new(a, m);

            Assert.AreEqual(a, val.a);
            Assert.AreEqual(m, val.m);
            Assert.AreEqual(a, val.Theta);
            Assert.AreEqual(m, val.Magnitude);
        }
    }
}
