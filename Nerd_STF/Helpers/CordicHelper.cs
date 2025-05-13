using Nerd_STF.Mathematics;

namespace Nerd_STF.Helpers
{
    internal static class CordicHelper
    {
        // Starts at 4 radians. Each index downwards is half that.
        // Goes from 2^2 to 2^-19.
        // Got the values from Desmos. Thanks guys!
        private static readonly double[] cosTable = new double[]
        {
            -0.653643620864,
            -0.416146836547,
            0.540302305868,
            0.87758256189,
            0.968912421711,
            0.992197667229,
            0.9980475107,
            0.999511758485,
            0.999877932171,
            0.999969482577,
            0.999992370615,
            0.999998092652,
            0.999999523163,
            0.999999880791,
            0.999999970198,
            0.999999992549,
            0.999999998137,
            0.999999999534,
            0.999999999884,
            0.999999999971,
            0.999999999993,
            0.999999999998
        };
        private static readonly double[] sinTable = new double[]
        {
            -0.756802495308,
            0.909297426826,
            0.841470984808,
            0.479425538604,
            0.247403959255,
            0.124674733385,
            0.0624593178424,
            0.0312449139853,
            0.0156243642249,
            0.00781242052738,
            0.0039062400659,
            0.00195312375824,
            0.00097656234478,
            0.000488281230597,
            0.000244140622575,
            0.000122070312197,
            0.0000610351562121,
            0.0000305175781203,
            0.0000152587890619,
            0.00000762939453118,
            0.00000381469726562,
            0.00000190734863281
        };

        // Unused sin(x) and cos(x) CORDIC approximator.
        // Not as fast as a taylor series for the same precision
        // unfortunately, so it won't be used.
        public static (double cos, double sin) SinAndCos(double theta, int maxTableIndex = int.MaxValue)
        {
            double curTheta = 0, curCos = 1, curSin = 0;
            double deltaTheta = 4;

            // Crazy for loop, but that's for optimization purposes.
            for (int index = 0, countedIndex = 0;
                 index < cosTable.Length && countedIndex < maxTableIndex;
                 index++, deltaTheta *= 0.5)
            {
                if (curTheta + deltaTheta > theta) continue;

                // cos(a + b) = cos(a)cos(b) - sin(a)sin(b)
                // sin(a + b) = cos(a)sin(b) + sin(a)cos(b)
                double deltaCos = cosTable[index],
                       deltaSin = sinTable[index];
                double newCos = curCos * deltaCos - curSin * deltaSin,
                       newSin = curCos * deltaSin + curSin * deltaCos;
                curCos = newCos;
                curSin = newSin;
                curTheta += deltaTheta;
                countedIndex++;
            }
            return (curCos, curSin);
        }

        private static readonly double[] powETable = new double[]
        {
            54.5981500331,
            7.38905609893,
            2.71828182846,
            1.6487212707,
            1.28402541669,
            1.13314845307,
            1.06449445892,
            1.0317434075,
            1.01574770859,
            1.00784309721,
            1.00391388934,
            1.00195503359,
            1.00097703949,
            1.00048840048,
            1.00024417043,
            1.00012207776,
            1.00006103702,
            1.00003051804,
            1.00001525891,
            1.00000762942,
            1.0000038147,
            1.00000190735
        };
        public static double PowE(double pow, int maxTableIndex = int.MaxValue)
        {
            double curPow = 0, curResult = 1;
            double deltaPow = 4;

            int index = 0, countedIndex = 0;
            while (index < powETable.Length && countedIndex < maxTableIndex)
            {
                if (curPow + deltaPow > pow)
                {
                    index++;
                    deltaPow *= 0.5;
                    continue;
                }

                curResult *= powETable[index];
                curPow += deltaPow;

                if (index > 0)
                {
                    index++;
                    deltaPow *= 0.5;
                }
            }
            return curResult;
        }
    
        // Generates a CORDIC table on demand.
        public static double PowAnyBase(double bass, double pow, int maxTableIndex)
        {
            if (pow % 1 == 0) return MathE.Pow(bass, (int)pow);
            else if (bass < 0) return double.NaN;
            else if (pow < 0) return 1 / PowAnyBase(bass, -pow, maxTableIndex);

            double curPow = 0, curResult = 1;
            double deltaResult = bass, deltaPow = 1;

            int countedIndex = 0;
            while (countedIndex < maxTableIndex)
            {
                if (curPow + deltaPow > pow)
                {
                    deltaPow *= 0.5;
                    deltaResult = MathE.Sqrt(deltaResult);
                    countedIndex++;
                    continue;
                }

                curResult *= deltaResult;
                curPow += deltaPow;

                if (countedIndex > 0)
                {
                    deltaPow *= 0.5;
                    deltaResult = MathE.Sqrt(deltaResult);
                    countedIndex++;
                }
            }
            return curResult;
        }
    }
}
