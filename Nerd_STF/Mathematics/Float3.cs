using System;

namespace Nerd_STF.Mathematics;

public record struct Float3 : IAbsolute<Float3>, IAverage<Float3>,
    ICeiling<Float3, Int3>, IClamp<Float3>, IClampMagnitude<Float3, float>, IComparable<Float3>,
    ICross<Float3>, IDot<Float3, float>, IEquatable<Float3>,
    IFloor<Float3, Int3>, IFromTuple<Float3, (float x, float y, float z)>, IGroup<float>,
    IIndexAll<float>, IIndexRangeAll<float>, ILerp<Float3, float>, IMathOperators<Float3>, IMax<Float3>,
    IMedian<Float3>, IMin<Float3>, IPresets3d<Float3>, IRound<Float3, Int3>,
    ISplittable<Float3, (float[] Xs, float[] Ys, float[] Zs)>
{
    public static Float3 Back => new(0, 0, -1);
    public static Float3 Down => new(0, -1, 0);
    public static Float3 Forward => new(0, 0, 1);
    public static Float3 Left => new(-1, 0, 0);
    public static Float3 Right => new(1, 0, 0);
    public static Float3 Up => new(0, 1, 0);

    public static Float3 One => new(1, 1, 1);
    public static Float3 Zero => new(0, 0, 0);

    public float InverseMagnitude => Mathf.InverseSqrt(x * x + y * y + z * z);
    public float Magnitude => Mathf.Sqrt(x * x + y * y + z * z);
    public Float3 Normalized => this * Mathf.InverseSqrt(x * x + y * y + z * z);

    public Float2 XY
    {
        get => (x, y);
        set
        {
            x = value.x;
            y = value.y;
        }
    }
    public Float2 XZ
    {
        get => (x, z);
        set
        {
            x = value.x;
            z = value.y;
        }
    }
    public Float2 YZ
    {
        get => (y, z);
        set
        {
            y = value.x;
            z = value.y;
        }
    }

    public float x, y, z;

    public Float3(float all) : this(all, all, all) { }
    public Float3(float x, float y) : this(x, y, 0) { }
    public Float3(float x, float y, float z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }
    public Float3(Fill<float> fill) : this(fill(0), fill(1), fill(2)) { }
    public Float3(Fill<int> fill) : this(fill(0), fill(1), fill(2)) { }

    public float this[int index]
    {
        get => index switch
        {
            0 => x,
            1 => y,
            2 => z,
            _ => throw new IndexOutOfRangeException(nameof(index)),
        };
        set
        {
            switch (index)
            {
                case 0:
                    x = value;
                    break;

                case 1:
                    y = value;
                    break;

                case 2:
                    z = value;
                    break;

                default: throw new IndexOutOfRangeException(nameof(index));
            }
        }
    }
    public float this[Index index]
    {
        get => this[index.IsFromEnd ? 3 - index.Value : index.Value];
        set => this[index.IsFromEnd ? 3 - index.Value : index.Value] = value;
    }
    public float[] this[Range range]
    {
        get
        {
            int start = range.Start.IsFromEnd ? 3 - range.Start.Value : range.Start.Value;
            int end = range.End.IsFromEnd ? 3 - range.End.Value : range.End.Value;
            List<float> res = new();
            for (int i = start; i < end; i++) res.Add(this[i]);
            return res.ToArray();
        }
        set
        {
            int start = range.Start.IsFromEnd ? 3 - range.Start.Value : range.Start.Value;
            int end = range.End.IsFromEnd ? 3 - range.End.Value : range.End.Value;
            for (int i = start; i < end; i++) this[i] = value[i];
        }
    }

    public static Float3 Absolute(Float3 val) =>
        new(Mathf.Absolute(val.x), Mathf.Absolute(val.y), Mathf.Absolute(val.z));
    public static Float3 Average(params Float3[] vals) => Sum(vals) / vals.Length;
    public static Int3 Ceiling(Float3 val) =>
        new(Mathf.Ceiling(val.x), Mathf.Ceiling(val.y), Mathf.Ceiling(val.z));
    public static Float3 Clamp(Float3 val, Float3 min, Float3 max) =>
        new(Mathf.Clamp(val.x, min.x, max.x),
            Mathf.Clamp(val.y, min.y, max.y),
            Mathf.Clamp(val.z, min.z, max.z));
    public static Float3 ClampMagnitude(Float3 val, float minMag, float maxMag)
    {
        if (maxMag < minMag) throw new ArgumentOutOfRangeException(nameof(maxMag),
            nameof(maxMag) + " must be greater than or equal to " + nameof(minMag));
        float mag = val.Magnitude;
        if (mag >= minMag && mag <= maxMag) return val;
        val = val.Normalized;
        if (mag < minMag) val *= minMag;
        else if (mag > maxMag) val *= maxMag;
        return val;
    }
    public static Float3 Cross(Float3 a, Float3 b, bool normalized = false)
    {
        Float3 val = new(a.y * b.z - b.y * a.z,
                          b.x * a.z - a.x * b.z,
                          a.x * b.y - b.x * a.y);
        return normalized ? val.Normalized : val;
    }
    public static float Dot(Float3 a, Float3 b) => a.x * b.x + a.y * b.y + a.z * b.z;
    public static float Dot(params Float3[] vals)
    {
        if (vals.Length < 1) return 0;
        float x = 1, y = 1, z = 1;
        foreach (Float3 d in vals)
        {
            x *= d.x;
            y *= d.y;
            z *= d.z;
        }
        return x + y + z;
    }
    public static Int3 Floor(Float3 val) =>
        new(Mathf.Floor(val.x), Mathf.Floor(val.y), Mathf.Floor(val.z));
    public static Float3 Lerp(Float3 a, Float3 b, float t, bool clamp = true) =>
        new(Mathf.Lerp(a.x, b.x, t, clamp), Mathf.Lerp(a.y, b.y, t, clamp), Mathf.Lerp(a.z, b.z, t, clamp));
    public static Float3 Median(params Float3[] vals)
    {
        float index = (vals.Length - 1) * 0.5f;
        Float3 valA = vals[Mathf.Floor(index)], valB = vals[Mathf.Ceiling(index)];
        return (valA + valB) * 0.5f;
    }
    public static Float3 Max(params Float3[] vals)
    {
        if (vals.Length < 1) return Zero;
        Float3 val = vals[0];
        foreach (Float3 d in vals) val = d.Magnitude > val.Magnitude ? d : val;
        return val;
    }
    public static Float3 Min(params Float3[] vals)
    {
        if (vals.Length < 1) return Zero;
        Float3 val = vals[0];
        foreach (Float3 d in vals) val = d.Magnitude < val.Magnitude ? d : val;
        return val;
    }
    public static Int3 Round(Float3 val) =>
        new(Mathf.RoundInt(val.x), Mathf.RoundInt(val.y), Mathf.RoundInt(val.z));

    public static (float[] Xs, float[] Ys, float[] Zs) SplitArray(params Float3[] vals)
    {
        float[] Xs = new float[vals.Length], Ys = new float[vals.Length], Zs = new float[vals.Length];
        for (int i = 0; i < vals.Length; i++)
        {
            Xs[i] = vals[i].x;
            Ys[i] = vals[i].y;
            Zs[i] = vals[i].z;
        }
        return (Xs, Ys, Zs);
    }

    public int CompareTo(Float3 other) => Magnitude.CompareTo(other.Magnitude);
    public bool Equals(Float3 other) => x == other.x && y == other.y && z == other.z;
    public override int GetHashCode() => base.GetHashCode();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    public IEnumerator<float> GetEnumerator()
    {
        yield return x;
        yield return y;
        yield return z;
    }

    public float[] ToArray() => new[] { x, y, z };
    public Fill<float> ToFill()
    {
        Float3 @this = this;
        return i => @this[i];
    }
    public List<float> ToList() => new() { x, y, z };

    public Vector3d ToVector()
    {
        float mag = Magnitude;
        Float3 normalized = Normalized;
        Angle yaw = Mathf.ArcCos(normalized.y);
        Angle pitch = Mathf.ArcSin(normalized.x / Mathf.Sin(yaw));
        return new(yaw, pitch, mag);
    }

    private bool PrintMembers(StringBuilder builder)
    {
        builder.Append("x = ");
        builder.Append(x);
        builder.Append(", y = ");
        builder.Append(y);
        builder.Append(", z = ");
        builder.Append(z);
        return true;
    }

    public static Float3 operator +(Float3 a, Float3 b) => new(a.x + b.x, a.y + b.y, a.z + b.z);
    public static Float3 operator -(Float3 d) => new(-d.x, -d.y, -d.z);
    public static Float3 operator -(Float3 a, Float3 b) => new(a.x - b.x, a.y - b.y, a.z - b.z);
    public static Float3 operator *(Float3 a, Float3 b) => new(a.x * b.x, a.y * b.y, a.z * b.z);
    public static Float3 operator *(Float3 a, float b) => new(a.x * b, a.y * b, a.z * b);
    public static Float3 operator *(Float3 a, Matrix b) => (Float3)((Matrix)a * b);
    public static Quaternion operator *(Float3 a, Quaternion b) => (Quaternion)a * b;
    public static Float3 operator /(Float3 a, Float3 b) => new(a.x / b.x, a.y / b.y, a.z / b.z);
    public static Float3 operator /(Float3 a, float b) => new(a.x / b, a.y / b, a.z / b);
    public static Float3 operator /(Float3 a, Matrix b) => (Float3)((Matrix)a / b);

    public static implicit operator Float3(Complex val) => new(val.u, val.i, 0);
    public static explicit operator Float3(Quaternion val) => new(val.u, val.i, val.j);
    public static implicit operator Float3(Float2 val) => new(val.x, val.y, 0);
    public static explicit operator Float3(Float4 val) => new(val.x, val.y, val.z);
    public static implicit operator Float3(Int2 val) => new(val.x, val.y, 0);
    public static implicit operator Float3(Int3 val) => new(val.x, val.y, val.z);
    public static explicit operator Float3(Int4 val) => new(val.x, val.y, val.z);
    public static explicit operator Float3(Matrix m) => new(m[0, 0], m[1, 0], m[2, 0]);
    public static explicit operator Float3(Vector2d val) => val.ToXYZ();
    public static explicit operator Float3(RGBA val) => new(val.R, val.G, val.B);
    public static explicit operator Float3(HSVA val) => new(val.H.Normalized, val.S, val.V);
    public static explicit operator Float3(RGBAByte val) => (Float3)val.ToRGBA();
    public static explicit operator Float3(HSVAByte val) => (Float3)val.ToHSVA();
    public static implicit operator Float3(Fill<float> fill) => new(fill);
    public static implicit operator Float3(Fill<int> fill) => new(fill);
    public static implicit operator Float3((float x, float y, float z) val) =>
        new(val.x, val.y, val.z);
}
