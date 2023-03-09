namespace Nerd_STF.Mathematics.Geometry;

public struct Vert : ICloneable, IEquatable<Vert>, IGroup<float>
{
    public static Vert Back => new(0, 0, -1);
    public static Vert Down => new(0, -1, 0);
    public static Vert Forward => new(0, 0, 1);
    public static Vert Left => new(-1, 0, 0);
    public static Vert Right => new(1, 0, 0);
    public static Vert Up => new(0, 1, 0);

    public static Vert One => new(1, 1, 1);
    public static Vert Zero => new(0, 0, 0);

    public float Magnitude => position.Magnitude;
    public Vert Normalized => this / Magnitude;

    public Float3 position;

    public Vert(Float2 pos) : this((Float3)pos) { }
    public Vert(Float3 pos) => position = pos;
    public Vert(float x, float y) : this(new Float2(x, y)) { }
    public Vert(float x, float y, float z) : this(new Float3(x, y, z)) { }
    public Vert(Fill<float> fill) : this(new Float3(fill)) { }
    public Vert(Fill<int> fill) : this(new Float3(fill)) { }

    public float this[int index]
    {
        get => position[index];
        set => position[index] = value;
    }

    public static Vert Absolute(Vert val) => new(Float3.Absolute(val.position));
    public static Vert Average(params Vert[] vals) => Float3.Average(ToFloat3Array(vals));
    public static Vert Ceiling(Vert val) => new(Float3.Ceiling(val.position));
    public static Vert Clamp(Vert val, Vert min, Vert max) =>
        new(Float3.Clamp(val.position, min.position, max.position));
    public static Vert ClampMagnitude(Vert val, float minMag, float maxMag) =>
        new(Float3.ClampMagnitude(val.position, minMag, maxMag));
    public static Vert Cross(Vert a, Vert b, bool normalized = false) =>
        new(Float3.Cross(a.position, b.position, normalized));
    public static float Dot(Vert a, Vert b) => Float3.Dot(a.position, b.position);
    public static float Dot(params Vert[] vals) => Float3.Dot(ToFloat3Array(vals));
    public static Vert Floor(Vert val) => new(Float3.Floor(val.position));
    public static Vert Lerp(Vert a, Vert b, float t, bool clamp = true) =>
        new(Float3.Lerp(a.position, b.position, t, clamp));
    public static Vert Median(params Vert[] vals) =>
        Float3.Median(ToFloat3Array(vals));
    public static Vert Max(params Vert[] vals) =>
        Float3.Max(ToFloat3Array(vals));
    public static Vert Min(params Vert[] vals) =>
        Float3.Min(ToFloat3Array(vals));
    public static Vert Round(Vert val) =>
        Float3.Round(val);

    public static Float3[] ToFloat3Array(params Vert[] vals)
    {
        Float3[] floats = new Float3[vals.Length];
        for (int i = 0; i < vals.Length; i++) floats[i] = vals[i].position;
        return floats;
    }
    public static List<Float3> ToFloat3List(params Vert[] vals) => ToFloat3Array(vals).ToList();

    public override bool Equals([NotNullWhen(true)] object? obj)
    {
        if (obj == null || obj.GetType() != typeof(Vert)) return base.Equals(obj);
        return Equals((Vert)obj);
    }
    public bool Equals(Vert other) => position == other.position;
    public override int GetHashCode() => position.GetHashCode();
    public override string ToString() => position.ToString();

    public object Clone() => new Vert(position);

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    public IEnumerator<float> GetEnumerator() => position.GetEnumerator();

    public float[] ToArray() => position.ToArray();
    public Fill<float> ToFill()
    {
        Vert @this = this;
        return i => @this[i];
    }
    public List<float> ToList() => position.ToList();

    public Vector3d ToVector() => ((Float3)this).ToVector();

    public static Vert operator +(Vert a, Vert b) => new(a.position + b.position);
    public static Vert operator -(Vert d) => new(-d.position);
    public static Vert operator -(Vert a, Vert b) => new(a.position - b.position);
    public static Vert operator *(Vert a, Vert b) => new(a.position * b.position);
    public static Vert operator *(Vert a, float b) => new(a.position * b);
    public static Vert operator /(Vert a, Vert b) => new(a.position / b.position);
    public static Vert operator /(Vert a, float b) => new(a.position / b);
    public static bool operator ==(Vert a, Vert b) => a.Equals(b);
    public static bool operator !=(Vert a, Vert b) => !a.Equals(b);

    public static implicit operator Vert(Float2 val) => new(val);
    public static implicit operator Vert(Float3 val) => new(val);
    public static explicit operator Vert(Float4 val) => new(val.XYZ);
    public static implicit operator Vert(Int2 val) => new(val);
    public static implicit operator Vert(Int3 val) => new(val);
    public static explicit operator Vert(Int4 val) => new(val.XYZ);
    public static implicit operator Vert(Fill<float> fill) => new(fill);
    public static implicit operator Vert(Fill<int> fill) => new(fill);
}
