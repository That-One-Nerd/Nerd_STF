using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nerd_STF.Mathematics.Geometry
{
    public struct Sphere : ICloneable, IClosest<Vert>, IComparable<Sphere>, IComparable<double>, IContainer<Vert>,
        IEquatable<Sphere>, IEquatable<double>
    {
        public static Sphere Unit => new(Vert.Zero, 1);

        public Vert center;
        public double radius;

        public double SurfaceArea => 4 * Mathf.Pi * radius * radius;
        public double Volume => 4 / 3 * (Mathf.Pi * radius * radius * radius);

        public static Sphere FromDiameter(Vert a, Vert b) => new(Vert.Average(a, b), (a - b).Magnitude / 2);
        public static Sphere FromRadius(Vert center, Vert radius) => new(center, (center - radius).Magnitude);

        public Sphere(Vert center, double radius)
        {
            this.center = center;
            this.radius = radius;
        }
        public Sphere(double cX, double cY, double radius) : this(new Vert(cX, cY), radius) { }
        public Sphere(double cX, double cY, double cZ, double radius) : this(new Vert(cX, cY, cZ), radius) { }
        public Sphere(Fill<double> fill, double radius) : this(new Vert(fill), radius) { }
        public Sphere(Fill<double> fill) : this(new Vert(fill), fill(3)) { }
        public Sphere(Fill<int> fill, double radius) : this(new Vert(fill), radius) { }
        public Sphere(Fill<int> fill) : this(new Vert(fill), fill(3)) { }
        public Sphere(Fill<Vert> fill, double radius) : this(fill(0), radius) { }
        public Sphere(Fill<Vert> fillA, Fill<double> fillB) : this(fillA(0), fillB(0)) { }

        public static Sphere Average(params Sphere[] vals)
        {
            (Vert[] centers, double[] radii) = SplitArray(vals);
            return new(Vert.Average(centers), Mathf.Average(radii));
        }
        public static Sphere Ceiling(Sphere val) => new(Vert.Ceiling(val.center), Mathf.Ceiling(val.radius));
        public static Sphere Clamp(Sphere val, Sphere min, Sphere max) =>
            new(Vert.Clamp(val.center, min.center, max.center), Mathf.Clamp(val.radius, min.radius, max.radius));
        public static Sphere Floor(Sphere val) => new(Vert.Floor(val.center), Mathf.Floor(val.radius));
        public static Sphere Lerp(Sphere a, Sphere b, double t, bool clamp = true) =>
            new(Vert.Lerp(a.center, b.center, t, clamp), Mathf.Lerp(a.radius, b.radius, t, clamp));
        public static Sphere Max(params Sphere[] vals)
        {
            (Vert[] centers, double[] radii) = SplitArray(vals);
            return new(Vert.Max(centers), Mathf.Max(radii));
        }
        public static Sphere Median(params Sphere[] vals)
        {
            (Vert[] centers, double[] radii) = SplitArray(vals);
            return new(Vert.Median(centers), Mathf.Median(radii));
        }
        public static Sphere Min(params Sphere[] vals)
        {
            (Vert[] centers, double[] radii) = SplitArray(vals);
            return new(Vert.Min(centers), Mathf.Min(radii));
        }

        public static (Vert[] centers, double[] radii) SplitArray(params Sphere[] spheres)
        {
            Vert[] centers = new Vert[spheres.Length];
            double[] radii = new double[spheres.Length];
            for (int i = 0; i < spheres.Length; i++)
            {
                centers[i] = spheres[i].center;
                radii[i] = spheres[i].radius;
            }
            return (centers, radii);
        }

        public override bool Equals([NotNullWhen(true)] object? obj)
        {
            if (obj == null) return false;
            Type type = obj.GetType();
            if (type == typeof(Sphere)) return Equals((Sphere)obj);
            if (type == typeof(double)) return Equals((double)obj);
            return false;
        }
        public bool Equals(double other) => Volume == other;
        public bool Equals(Sphere other) => center == other.center && radius == other.radius;
        public override int GetHashCode() => center.GetHashCode() ^ radius.GetHashCode();
        public override string ToString() => ToString((string?)null);
        public string ToString(string? provider) => "Center: " + center.ToString(provider)
            + " Radius: " + radius.ToString(provider);
        public string ToString(IFormatProvider provider) => "Center: " + center.ToString(provider)
            + " Radius: " + radius.ToString(provider);

        public object Clone() => new Sphere(center, radius);

        public int CompareTo(Sphere sphere) => Volume.CompareTo(sphere.Volume);
        public int CompareTo(double volume) => Volume.CompareTo(volume);

        public bool Contains(Vert vert) => (center - vert).Magnitude <= radius;

        public Vert ClosestTo(Vert vert) => Contains(vert) ? vert : ((vert - center).Normalized * radius) + vert;

        public static Sphere operator +(Sphere a, Sphere b) => new(a.center + b.center, a.radius + b.radius);
        public static Sphere operator +(Sphere a, Vert b) => new(a.center + b, a.radius);
        public static Sphere operator +(Sphere a, double b) => new(a.center, a.radius + b);
        public static Sphere operator -(Sphere a, Sphere b) => new(a.center + b.center, a.radius + b.radius);
        public static Sphere operator -(Sphere a, Vert b) => new(a.center + b, a.radius);
        public static Sphere operator -(Sphere a, double b) => new(a.center, a.radius + b);
        public static Sphere operator *(Sphere a, Sphere b) => new(a.center * b.center, a.radius * b.radius);
        public static Sphere operator *(Sphere a, double b) => new(a.center * b, a.radius * b);
        public static Sphere operator /(Sphere a, Sphere b) => new(a.center * b.center, a.radius * b.radius);
        public static Sphere operator /(Sphere a, double b) => new(a.center * b, a.radius * b);
        public static bool operator ==(Sphere a, Sphere b) => a.Equals(b);
        public static bool operator !=(Sphere a, Sphere b) => !a.Equals(b);
        public static bool operator ==(Sphere a, double b) => a.Equals(b);
        public static bool operator !=(Sphere a, double b) => !a.Equals(b);
        public static bool operator >(Sphere a, Sphere b) => a.CompareTo(b) > 0;
        public static bool operator <(Sphere a, Sphere b) => a.CompareTo(b) < 0;
        public static bool operator >(Sphere a, double b) => a.CompareTo(b) > 0;
        public static bool operator <(Sphere a, double b) => a.CompareTo(b) < 0;
        public static bool operator >=(Sphere a, Sphere b) => a > b || a == b;
        public static bool operator <=(Sphere a, Sphere b) => a < b || a == b;
        public static bool operator >=(Sphere a, double b) => a > b || a == b;
        public static bool operator <=(Sphere a, double b) => a < b || a == b;
    }
}
