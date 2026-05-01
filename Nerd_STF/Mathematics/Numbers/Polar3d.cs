using Nerd_STF.Mathematics.Algebra;
using System;

namespace Nerd_STF.Mathematics.Numbers
{
    public struct Polar3d/* : IVector<Polar3d>, IEquatable<Polar3d>
#if CS11_OR_GREATER
                           ,IFromTuple<Polar3d, (double, Angle, Angle)>,
                            IPresets3d<Polar3d>
#endif*/
    {
        public double Magnitude { get; set; }
        public Angle Theta { get; set; }
        public Angle Phi { get; set; }

        public Polar3d(double magnitude, Angle theta, Angle phi)
        {
            Theta = theta;
            Phi = phi;
            Magnitude = magnitude;
        }
        public Polar3d(double magnitude, double theta, double phi)
        {
            Magnitude = magnitude;
            Theta = Angle.FromRadians(theta);
            Phi = Angle.FromRadians(phi);
        }
        public Polar3d(Fill<double> fill)
        {
            Magnitude = fill(0);
            Theta = Angle.FromRadians(fill(1));
            Phi = Angle.FromRadians(fill(2));
        }
    }
}
