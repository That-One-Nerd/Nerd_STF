namespace Nerd_STF.Mathematics.Geometry;

public struct Vert : ICloneable, IEquatable<Vert>, IGroup<double>
{
    public static Vert Back => new(0, 0, -1);
    public static Vert Down => new(0, -1, 0);
    public static Vert Forward => new(0, 0, 1);
    public static Vert Left => new(-1, 0, 0);
    public static Vert Right => new(1, 0, 0);
    public static Vert Up => new(0, 1, 0);

    public static Vert One => new(1, 1, 1);
    public static Vert Zero => new(0, 0, 0);

    public double Magnitude => position.Magnitude;
    public Vert Normalized => new(this / Magnitude);

    public Double3 position;

    public Vert(Double2 pos) : this(pos.x, pos.y, 0) { }
    public Vert(Double3 pos) => position = pos;
    public Vert(double x, double y) : this(new Double2(x, y)) { }
    public Vert(double x, double y, double z) : this(new Double3(x, y, z)) { }
    public Vert(Fill<double> fill) : this(new Double3(fill)) { }
    public Vert(Fill<int> fill) : this(new Double3(fill)) { }

    public double this[int index]
    {
        get => position[index];
        set => position[index] = value;
    }

    public static Vert Absolute(Vert val) => new(Double3.Absolute(val.position));
    public static Vert Average(params Vert[] vals) => Double3.Average(ToDouble3Array(vals));
    public static Vert Ceiling(Vert val) => new(Double3.Ceiling(val.position));
    public static Vert Clamp(Vert val, Vert min, Vert max) =>
        new(Double3.Clamp(val.position, min.position, max.position));
    public static Vert ClampMagnitude(Vert val, double minMag, double maxMag) =>
        new(Double3.ClampMagnitude(val.position, minMag, maxMag));
    public static Vert Cross(Vert a, Vert b, bool normalized = false) =>
        new(Double3.Cross(a.position, b.position, normalized));
    public static double Dot(Vert a, Vert b) => Double3.Dot(a.position, b.position);
    public static double Dot(params Vert[] vals) => Double3.Dot(ToDouble3Array(vals));
    public static Vert Floor(Vert val) => new(Double3.Floor(val.position));
    public static Vert Lerp(Vert a, Vert b, double t, bool clamp = true) =>
        new(Double3.Lerp(a.position, b.position, t, clamp));
    public static Vert Median(params Vert[] vals) =>
        Double3.Median(ToDouble3Array(vals));
    public static Vert Max(params Vert[] vals) =>
        Double3.Max(ToDouble3Array(vals));
    public static Vert Min(params Vert[] vals) =>
        Double3.Min(ToDouble3Array(vals));
    public static Double3[] ToDouble3Array(params Vert[] vals)
    {
        Double3[] doubles = new Double3[vals.Length];
        for (int i = 0; i < vals.Length; i++) doubles[i] = vals[i].position;
        return doubles;
    }
    public static List<Double3> ToDouble3List(params Vert[] vals) => ToDouble3Array(vals).ToList();

    public override bool Equals([NotNullWhen(true)] object? obj)
    {
        if (obj == null || obj.GetType() != typeof(Vert)) return false;
        return Equals((Vert)obj);
    }
    public bool Equals(Vert other) => position == other.position;
    public override int GetHashCode() => position.GetHashCode();
    public override string ToString() => ToString((string?)null);
    public string ToString(string? provider) => position.ToString(provider);
    public string ToString(IFormatProvider provider) => position.ToString(provider);

    public object Clone() => new Vert(position);

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    public IEnumerator<double> GetEnumerator() => position.GetEnumerator();

    public double[] ToArray() => position.ToArray();
    public List<double> ToList() => position.ToList();

    public static Vert operator +(Vert a, Vert b) => new(a.position + b.position);
    public static Vert operator -(Vert d) => new(-d.position);
    public static Vert operator -(Vert a, Vert b) => new(a.position - b.position);
    public static Vert operator *(Vert a, Vert b) => new(a.position * b.position);
    public static Vert operator *(Vert a, double b) => new(a.position * b);
    public static Vert operator /(Vert a, Vert b) => new(a.position / b.position);
    public static Vert operator /(Vert a, double b) => new(a.position / b);
    public static bool operator ==(Vert a, Vert b) => a.Equals(b);
    public static bool operator !=(Vert a, Vert b) => !a.Equals(b);

    public static implicit operator Vert(Double2 val) => new(val);
    public static implicit operator Vert(Double3 val) => new(val);
    public static explicit operator Vert(Double4 val) => new(val.XYZ);
    public static implicit operator Vert(Int2 val) => new(val);
    public static implicit operator Vert(Int3 val) => new(val);
    public static explicit operator Vert(Int4 val) => new(val.XYZ);
    public static implicit operator Vert(Fill<double> fill) => new(fill);
    public static implicit operator Vert(Fill<int> fill) => new(fill);
}
