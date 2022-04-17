using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nerd_STF.Mathematics.Geometry
{
    public struct Box2D : ICloneable, IContainer<Vert>, IEquatable<Box2D>
    {
        public static Box2D Unit => new(Vert.Zero, Double2.One);

        public Vert MaxVert
        {
            get => center + (size / 2);
            set
            {
                Vert diff = center - value;
                size = (Double2)diff.position * 2;
            }
        }
        public Vert MinVert
        {
            get => center - (size / 2);
            set
            {
                Vert diff = center + value;
                size = (Double2)diff.position * 2;
            }
        }

        public double Area => size.x * size.y;
        public double Perimeter => size.x * 2 + size.y * 2;

        public Vert center;
        public Double2 size;

        public Box2D(Vert min, Vert max) : this(Vert.Average(min, max), (Double2)(min - max)) { }
        public Box2D(Vert center, Double2 size)
        {
            this.center = center;
            this.size = size;
        }
        public Box2D(Fill<double> fill) : this(fill, new Double2(fill(3), fill(4))) { }

        public double this[int index]
        {
            get => size[index];
            set => size[index] = value;
        }

        public static Box2D Absolute(Box2D val) => new(Vert.Absolute(val.MinVert), Vert.Absolute(val.MaxVert));
        public static Box2D Average(params Box2D[] vals)
        {
            (Vert[] centers, Double2[] sizes) = SplitArray(vals);
            return new(Vert.Average(centers), Double2.Average(sizes));
        }
        public static Box2D Ceiling(Box2D val) => new(Vert.Ceiling(val.center), Double2.Ceiling(val.size));
        public static Box2D Clamp(Box2D val, Box2D min, Box2D max) =>
            new(Vert.Clamp(val.center, min.center, max.center), Double2.Clamp(val.size, min.size, max.size));
        public static Box2D Floor(Box2D val) => new(Vert.Floor(val.center), Double2.Floor(val.size));
        public static Box2D Lerp(Box2D a, Box2D b, float t, bool clamp = true) =>
            new(Vert.Lerp(a.center, b.center, t, clamp), Double2.Lerp(a.size, b.size, t, clamp));
        public static Box2D Median(params Box2D[] vals)
        {
            (Vert[] verts, Double2[] sizes) = SplitArray(vals);
            return new(Vert.Median(verts), Double2.Median(sizes));
        }
        public static Box2D Max(params Box2D[] vals)
        {
            (Vert[] verts, Double2[] sizes) = SplitArray(vals);
            return new(Vert.Max(verts), Double2.Max(sizes));
        }
        public static Box2D Min(params Box2D[] vals)
        {
            (Vert[] verts, Double2[] sizes) = SplitArray(vals);
            return new(Vert.Min(verts), Double2.Min(sizes));
        }
        public static (Vert[] centers, Double2[] sizes) SplitArray(params Box2D[] vals)
        {
            Vert[] centers = new Vert[vals.Length];
            Double2[] sizes = new Double2[vals.Length];

            for (int i = 0; i < vals.Length; i++)
            {
                centers[i] = vals[i].center;
                sizes[i] = vals[i].size;
            }

            return (centers, sizes);
        }

        public override bool Equals([NotNullWhen(true)] object? obj)
        {
            if (obj == null || obj.GetType() != typeof(Box2D)) return false;
            return Equals((Box2D)obj);
        }
        public bool Equals(Box2D other) => center == other.center && size == other.size;
        public override int GetHashCode() => center.GetHashCode() ^ size.GetHashCode();
        public override string ToString() => ToString((string?)null);
        public string ToString(string? provider) =>
            "Min: " + MinVert.ToString(provider) + " Max: " + MaxVert.ToString(provider);
        public string ToString(IFormatProvider provider) =>
            "Min: " + MinVert.ToString(provider) + " Max: " + MaxVert.ToString(provider);

        public bool Contains(Vert vert)
        {
            Double2 diff = Double2.Absolute((Double2)(center - vert));
            return diff.x <= size.x && diff.y <= size.y;
        }

        public object Clone() => new Box2D(center, size);

        public static Box2D operator +(Box2D a, Vert b) => new(a.center + b, a.size);
        public static Box2D operator +(Box2D a, Double2 b) => new(a.center, a.size + b);
        public static Box2D operator -(Box2D b) => new(-b.MaxVert, -b.MinVert);
        public static Box2D operator -(Box2D a, Vert b) => new(a.center - b, a.size);
        public static Box2D operator -(Box2D a, Double2 b) => new(a.center, a.size - b);
        public static Box2D operator *(Box2D a, double b) => new(a.center * b, a.size * b);
        public static Box2D operator *(Box2D a, Double2 b) => new(a.center, a.size * b);
        public static Box2D operator /(Box2D a, double b) => new(a.center / b, a.size / b);
        public static Box2D operator /(Box2D a, Double2 b) => new(a.center, a.size / b);
        public static bool operator ==(Box2D a, Box2D b) => a.Equals(b);
        public static bool operator !=(Box2D a, Box2D b) => !a.Equals(b);

        public static implicit operator Box2D(Fill<double> fill) => new(fill);
        public static explicit operator Box2D(Box3D box) => new(box.center, (Double2)box.size);
    }
}
