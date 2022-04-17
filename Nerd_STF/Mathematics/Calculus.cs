using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nerd_STF.Mathematics
{
    public static class Calculus
    {
        public const double DefaultStep = 0.001;

        public static Equation GetDerivative(Equation equ, double min, double max, double step = DefaultStep)
        {
            Dictionary<double, double> vals = new();
            for (double x = min; x <= max; x += step)
            {
                double val1 = equ(x), val2 = equ(x + step), change = (val2 - val1) / step;
                vals.Add(x, change);
            }
            return Mathf.MakeEquation(vals);
        }
        public static double GetDerivativeAtPoint(Equation equ, double x, double step = DefaultStep) =>
            (equ(x + DefaultStep) - equ(x)) / step;

        public static double GetIntegral(Equation equ, double lowerBound, double upperBound, double step = DefaultStep)
        {
            double val = 0;
            for (double x = lowerBound; x <= upperBound; x += step) val += equ(x) * step;
            return val;
        }

        public static double GradientDescent(Equation equ, double initial, double rate, double stepCount = 1000,
            double step = DefaultStep)
        {
            double val = initial;
            for (int i = 0; i < stepCount; i++) val -= GetDerivativeAtPoint(equ, val, step) * rate;
            return val;
        }
    }
}
