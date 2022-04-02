namespace Nerd_STF.Mathematics
{
    public static class Mathf
    {
        public const double DegToRad = 0.0174532925199; // Pi / 180
        public const double E = 2.71828182846;
        public const double GoldenRatio = 1.61803398875; // (1 + Sqrt(5)) / 2
        public const double HalfPi = 1.57079632679; // Pi / 2
        public const double Pi = 3.14159265359;
        public const double RadToDeg = 57.2957795131; // 180 / Pi
        public const double Tau = 6.28318530718; // 2 * Pi

        public static double Absolute(double val) => val < 0 ? -val : val;
        public static int Absolute(int val) => val < 0 ? -val : val;

        public static double ArcCos(double value) => -ArcSin(value) + HalfPi;

        public static double ArcCot(double value) => ArcCos(value / Sqrt(1 + value * value));

        public static double ArcCsc(double value) => ArcSin(1 / value);

        public static double ArcSec(double value) => ArcCos(1 / value);

        // Maybe one day I'll have a polynomial for this, but the RMSE for an order 10 polynomial is only 0.00876.
        public static double ArcSin(double value) => Math.Asin(value);

        public static double ArcTan(double value) => ArcSin(value / Sqrt(1 + value * value));

        public static double Average(params double[] vals) => Sum(vals) / vals.Length;
        public static int Average(params int[] vals) => Sum(vals) / vals.Length;

        public static int Ceiling(double val) => (int)(val + (1 - (val % 1)));

        public static double Clamp(double val, double min, double max)
        {
            if (max < min) throw new ArgumentOutOfRangeException(nameof(max),
                nameof(max) + " must be greater than or equal to " + nameof(min));
            val = val < min ? min : val;
            val = val > max ? max : val;
            return val;
        }
        public static int Clamp(int val, int min, int max)
        {
            if (max < min) throw new ArgumentOutOfRangeException(nameof(max),
                nameof(max) + " must be greater than or equal to " + nameof(min));
            val = val < min ? min : val;
            val = val > max ? max : val;
            return val;
        }

        public static double Cos(double radians) => Sin(radians + HalfPi);

        public static double Cot(double radians) => Cos(radians) / Sin(radians);

        public static double Csc(double radians) => 1 / Sin(radians);

        public static double Divide(double val, params double[] dividends)
        {
            foreach (double d in dividends) val /= d;
            return val;
        }
        public static int Divide(int val, params int[] dividends)
        {
            foreach (int i in dividends) val /= i;
            return val;
        }

        public static int Factorial(int amount)
        {
            if (amount < 0) return 0;
            int val = 1;
            for (int i = 2; i <= amount; i++) val *= i;
            return val;
        }

        public static int Floor(double val) => (int)(val - (val % 1));

        public static double Lerp(double a, double b, double t, bool clamp = true)
        {
            double v = a + t * (b - a);
            if (clamp) v = Clamp(v, a, b);
            return v;
        }
        public static int Lerp(int a, int b, double value, bool clamp = true) => Floor(Lerp(a, b, value, clamp));

        public static double Max(params double[] vals)
        {
            if (vals.Length < 1) return 0;
            double val = vals[0];
            foreach (double d in vals) val = d > val ? d : val;
            return val;
        }
        public static int Max(params int[] vals)
        {
            if (vals.Length < 1) return 0;
            int val = vals[0];
            foreach (int i in vals) val = i > val ? i : val;
            return val;
        }

        public static double Median(params double[] vals)
        {
            double index = Average(0, vals.Length - 1);
            double valA = vals[Floor(index)], valB = vals[Ceiling(index)];
            return Average(valA, valB);
        }
        public static int Median(params int[] vals) => vals[Floor(Average(0, vals.Length - 1))];

        public static double Min(params double[] vals)
        {
            if (vals.Length < 1) return 0;
            double val = vals[0];
            foreach (double d in vals) val = d < val ? d : val;
            return val;
        }
        public static int Min(params int[] vals)
        {
            if (vals.Length < 1) return 0;
            int val = vals[0];
            foreach (int i in vals) val = i < val ? i : val;
            return val;
        }

        public static double Multiply(params double[] vals)
        {
            if (vals.Length < 1) return 0;
            double val = 1;
            foreach (double d in vals) val *= d;
            return val;
        }
        public static int Multiply(params int[] vals)
        {
            if (vals.Length < 1) return 0;
            int val = 1;
            foreach (int i in vals) val *= i;
            return val;
        }

        public static double Power(double num, double pow) => Math.Pow(num, pow);
        public static int Power(int num, int pow)
        {
            if (pow < 0) return 0;
            int val = 1;
            for (int i = 0; i < Absolute(pow); i++) val *= num;
            return val;
        }

        public static double Root(double value, double index) => Math.Exp(index * Math.Log(value));

        public static double Round(double num) => num % 1 >= 0.5 ? Ceiling(num) : Floor(num);
        public static double Round(double num, double nearest) => nearest * Round(num / nearest);
        public static int RoundInt(double num) => (int)Round(num);

        public static double Sec(double radians) => 1 / Cos(radians);

        public static double Sin(double radians)
        {
            // Really close polynomial to sin(x) (when modded by 2pi). RMSE of 0.000003833
            const double a =  0.000013028,
                         b =  0.999677,
                         c =  0.00174164,
                         d = -0.170587,
                         e =  0.0046494,
                         f =  0.00508955,
                         g =  0.00140205,
                         h = -0.000577413,
                         i =  0.0000613134,
                         j = -0.00000216852;
            double x = radians % Tau;

            return
                a + (b * x) + (c * x * x) + (d * x * x * x) + (e * x * x * x * x) + (f * x * x * x * x * x)
                + (g * x * x * x * x * x * x) + (h * x * x * x * x * x * x * x) + (i * x * x * x * x * x * x * x * x)
                + (j * x * x * x * x * x * x * x * x * x);
        }

        public static double Sqrt(double value) => Root(value, 2);

        public static double Subtract(double num, params double[] vals)
        {
            foreach (double d in vals) num -= d;
            return num;
        }
        public static int Subtract(int num, params int[] vals)
        {
            foreach (int i in vals) num -= i;
            return num;
        }

        public static double Sum(params double[] vals)
        {
            double val = 0;
            foreach (double d in vals) val += d;
            return val;
        }
        public static int Sum(params int[] vals)
        {
            int val = 0;
            foreach (int i in vals) val += i;
            return val;
        }

        public static double Tan(double radians) => Sin(radians) / Cos(radians);
    }
}
