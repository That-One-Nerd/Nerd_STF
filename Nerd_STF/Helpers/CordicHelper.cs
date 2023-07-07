namespace Nerd_STF.Helpers;

// TODO: Make this internal

// Putting this in here for future reference:
// CORDIC is basically just splitting up an
// operation into more smaller operations.
// For example, turning sin(5rad) into
// sin(4rad + 1rad), which can be turned into
// the formula cos(4rad)cos(1rad) - sin(4rad)sin(1rad),
// to which we know the values of each part.
// Then we just do this iteratively on a bunch
// of powers of 2.
public static class CordicHelper
{
    private static readonly float[] p_cosTable =
    {
        0.540302305868f, // cos(2^0)
        0.87758256189f,  // cos(2^-1)
        0.968912421711f, // cos(2^-2)
        0.992197667229f, // cos(2^-3)
        0.9980475107f,   // cos(2^-4)
        0.999511758485f, // cos(2^-5)
        0.999877932171f, // cos(2^-6)
        0.999969482577f, // cos(2^-7)
        0.999992370615f, // cos(2^-8)
        0.999998092652f, // cos(2^-9)
        0.999999523163f, // cos(2^-10)
        0.999999880791f, // cos(2^-11)
        0.999999970198f, // cos(2^-12)
        0.999999992549f, // cos(2^-13)
        0.999999998137f, // cos(2^-14)
        0.999999999534f  // cos(2^-15)
    };
    private static readonly float[] p_sinTable =
    {
        0.841470984808f,     // sin(2^0)
        0.479425538604f,     // sin(2^-1)
        0.247403959255f,     // sin(2^-2)
        0.124674733385f,     // sin(2^-3)
        0.0624593178424f,    // sin(2^-4)
        0.0312449139853f,    // sin(2^-5)
        0.0156243642249f,    // sin(2^-6)
        0.00781242052738f,   // sin(2^-7)
        0.0039062400659f,    // sin(2^-8)
        0.00195312375824f,   // sin(2^-9)
        0.00097656234478f,   // sin(2^-10)
        0.000488281230597f,  // sin(2^-11)
        0.000244140622575f,  // sin(2^-12)
        0.000122070312197f,  // sin(2^-13)
        0.0000610351562121f, // sin(2^-14)
        0.0000305175781203f  // sin(2^-15)
    };

    private static readonly float[] p_coshTable =
    {
        1.54308063482f, // cosh(2^0)
        1.12762596521f, // cosh(2^-1)
        1.03141309988f, // cosh(2^-2)
        1.00782267783f, // cosh(2^-3)
        1.00195376087f, // cosh(2^-4)
        1.00048832099f, // cosh(2^-5)
        1.0001220728f,  // cosh(2^-6)
        1.00003051773f, // cosh(2^-7)
        1.0000076294f,  // cosh(2^-8)
        1.00000190735f, // cosh(2^-9)
        1.00000047684f, // cosh(2^-10)
        1.00000011921f, // cosh(2^-11)
        1.0000000298f,  // cosh(2^-12)
        1.00000000745f, // cosh(2^-13)
        1.00000000186f, // cosh(2^-14)
        1.00000000047f, // cosh(2^-15)
    };
    private static readonly float[] p_sinhTable =
    {
        1.17520119364f,      // sinh(2^0)
        0.521095305494f,     // sinh(2^-1)
        0.252612316808f,     // sinh(2^-2)
        0.125325775241f,     // sinh(2^-3)
        0.0625406980522f,    // sinh(2^-4)
        0.0312550865114f,    // sinh(2^-5)
        0.0156256357906f,    // sinh(2^-6)
        0.0078125794731f,    // sinh(2^-7)
        0.00390625993412f,   // sinh(2^-8)
        0.00195312624176f,   // sinh(2^-9)
        0.00097656265522f,   // sinh(2^-10)
        0.000488281269403f,  // sinh(2^-11)
        0.000244140627425f,  // sinh(2^-12)
        0.000122070312803f,  // sinh(2^-13)
        0.0000610351562879f, // sinh(2^-14)
        0.0000305175781297f, // sinh(2^-15)
    };

    private static readonly Dictionary<(float bas, int depth), float[]> p_expTables;

    static CordicHelper()
    {
        p_expTables = new();
    }

    // This was originally intended to replace the Mathf.Cos
    // and Mathf.Sin functions, but it ended up being considerably
    // slower. In the future if it gets optimized, I might then
    // choose to replace it.
    // REMEMBER: When implementing, remember to use Mathf.AbsoluteMod,
    //           because that's what I was intending when I wrote this.
    public static (float sin, float cos) CalculateTrig(float x, int iterations)
    {
        float approximateX = 0,
              approximateCos = 1,
              approximateSin = 0;

        // Iterate continuously until it gets better.
        for (int i = 0; i < iterations; i++)
        {
            // We need to find the biggest step that'll move us
            // closer to the real X (without overshooting).
            float diffX = x - approximateX;

            // This is assuming that cosTable and sinTable
            // have the same length.
            for (int j = 0; j < p_cosTable.Length; j++)
            {
                // The amount the difference will shrink.
                float incX = FastGenExp2((sbyte)-j);

                if (diffX >= incX)
                {
                    // Because here we go big to small, the first one that triggers
                    // this if statement should also be the biggest one that can.

                    // Get the sin and cos values for this power of two.
                    float valCos = p_cosTable[j],
                          valSin = p_sinTable[j];

                    // Do the products.
                    float newCos = approximateCos * valCos - approximateSin * valSin,
                          newSin = approximateCos * valSin + approximateSin * valCos;

                    // Apply differences
                    approximateX += incX;
                    approximateCos = newCos;
                    approximateSin = newSin;
                    break;
                }
            }
        }

        // Sin and cos should be pretty accurate by now,
        // so we can return them.
        return (approximateSin, approximateCos);
    }

    public static (float sinh, float cosh) CalculateHyperTrig(float x, int iterations)
    {
        float approximateX = 0,
              approximateCosh = 1,
              approximateSinh = 0;

        // Iterate continuously until it gets better.
        for (int i = 0; i < iterations; i++)
        {
            // We need to find the biggest step that'll move us
            // closer to the real X (without overshooting).
            float diffX = x - approximateX;

            // This is assuming that cosTable and sinTable
            // have the same length.
            for (int j = 0; j < p_coshTable.Length; j++)
            {
                // The amount the difference will shrink.
                float incX = FastGenExp2((sbyte)-j);

                if (diffX >= incX)
                {
                    // Because here we go big to small, the first one that triggers
                    // this if statement should also be the biggest one that can.

                    // Get the sin and cos values for this power of two.
                    float valCosh = p_coshTable[j],
                          valSinh = p_sinhTable[j];

                    // Do the products.
                    float newCosh = approximateCosh * valCosh + approximateSinh * valSinh,
                          newSinh = approximateCosh * valSinh + approximateSinh * valCosh;

                    // Apply differences
                    approximateX += incX;
                    approximateCosh = newCosh;
                    approximateSinh = newSinh;
                    break;
                }
            }
        }

        // Sin and cos should be pretty accurate by now,
        // so we can return them.
        return (approximateSinh, approximateCosh);
    }

    public static float ExpAnyBase(float bas, float pow, int tableDepth, int iterations)
    {
        // We need to auto-generate a table of values for this number the user enters.
        float[] table;
        if (p_expTables.ContainsKey((bas, tableDepth)))
        {
            // Table was already generated, so we can reuse it.
            table = p_expTables[(bas, tableDepth)];
        }
        else
        {
            // Calculate a table for the CORDIC system by
            // applying sequential square roots.
            table = new float[tableDepth];
            table[0] = bas;
            for (int i = 1; i < tableDepth; i++) table[i] = Mathf.Sqrt(table[i - 1]);
            p_expTables.Add((bas, tableDepth), table);
        }

        // Now we can perform the CORDIC method.
        float approximateX = 0, approximateVal = 1;

        // Iterate continuously until it gets better.
        for (int i = 0; i < iterations; i++)
        {
            // We need to find the biggest step that'll move us
            // closer to the real X (without overshooting).
            float diffX = pow - approximateX;

            for (int j = 0; j < tableDepth; j++)
            {
                // The amount the difference will shrink.
                float incX = FastGenExp2((sbyte)-j);

                if (diffX >= incX)
                {
                    // Because here we go big to small, the first one that triggers
                    // this if statement should also be the biggest one that can.

                    // Get the power value for this power of two.
                    float val = table[j];

                    // Apply our value.
                    approximateX += incX;
                    approximateVal *= val;
                    break;
                }
            }
        }

        // Value should be pretty accurate by now,
        // so we can return it.
        return approximateVal;
    }
    public static float LogAnyBase(float bas, float val, int tableDepth, int iterations)
    {
        // We need to auto-generate a table of values for this number the user enters.
        // However, we can use the already existing exponent tables and just swap the
        // indexes and the values.
        float[] table;
        if (p_expTables.ContainsKey((bas, tableDepth)))
        {
            // Table was already generated, so we can reuse it.
            table = p_expTables[(bas, tableDepth)];
        }
        else
        {
            // Calculate a table for the CORDIC system by
            // applying sequential square roots.
            table = new float[tableDepth];
            table[0] = bas;
            for (int i = 1; i < tableDepth; i++) table[i] = Mathf.Sqrt(table[i - 1]);
            p_expTables.Add((bas, tableDepth), table);
        }

        // Now we can perform the CORDIC method.
        float approximateX = 0, approximateVal = 1;

        // Iterate continuously until it gets better.
        for (int i = 0; i < iterations; i++)
        {
            float diffY = val / approximateVal;

            for (int j = 0; j < table.Length; j++)
            {
                // The amount the difference will shrink.
                float incX = FastGenExp2((sbyte)-j);
                float newVal = table[j];

                if (diffY >= newVal)
                {
                    // Because here we go big to small, the first one that triggers
                    // this if statement should also be the biggest one that can.

                    // Apply our value.
                    approximateX += incX;
                    approximateVal *= newVal;
                    break;
                }
            }
        }

        // Value should be pretty accurate by now,
        // so we can return it.
        return approximateX;
    }

    // An extremely fast way to generate 2 to
    // the power of p. I say "generate" because I'm
    // just messing with the mantissa's data and
    // not doing any real math.
    private static float FastGenExp2(sbyte p)
    {
        int data = (((p - 1) ^ 0b10000000) << 23) & ~(1 << 31);
        return UnsafeHelper.SwapType<int, float>(data);
    }
}
