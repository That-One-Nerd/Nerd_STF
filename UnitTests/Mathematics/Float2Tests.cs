using Nerd_STF.Mathematics;
using System.Collections.Generic;

namespace Nerd_STF.UnitTests.Mathematics
{
    [TestClass]
    public sealed class Float2Tests
    {
        // TODO
    }
}

namespace Nerd_STF.UnitTests.Helpers
{
    // FIXME: Do we want a delta check in the actual Float2 object?
    //        I really dunno. I should ask around (more than like 3 people).
    public static partial class Compare
    {
        public static IEqualityComparer<Float2> Float2(double delta = 1e-3) => new Float2EqualityComparer(delta);

        private class Float2EqualityComparer(double delta) : IEqualityComparer<Float2>
        {
            public bool Equals(Float2 a, Float2 b)
            {
                double dx = a.x - b.x,
                       dy = a.y - b.y;
                return (dx * dx) + (dy * dy) <= delta;
            }
            public int GetHashCode(Float2 val) => val.GetHashCode();
        }
    }
}
