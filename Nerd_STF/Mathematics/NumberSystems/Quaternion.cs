using SystemQuaternion = System.Numerics.Quaternion;

namespace Nerd_STF.Mathematics.NumberSystems;

public record struct Quaternion(float u, float i, float j, float k) : IAbsolute<Quaternion>, IAverage<Quaternion>,
    ICeiling<Quaternion>, IClamp<Quaternion>, IClampMagnitude<Quaternion, float>, IComparable<Quaternion>,
    IDivide<Quaternion>, IDot<Quaternion, float>, IEquatable<Quaternion>, IFloor<Quaternion>, IGroup<float>,
    IIndexAll<float>, IIndexRangeAll<float>, ILerp<Quaternion, float>, IMax<Quaternion>, IMedian<Quaternion>,
    IMin<Quaternion>, IPresets4d<Quaternion>, IProduct<Quaternion>, IRound<Quaternion>,
    ISplittable<Quaternion, (float[] Us, float[] Is, float[] Js, float[] Ks)>, ISum<Quaternion>
{
    public static Quaternion Back => new(0, 0, -1, 0);
    public static Quaternion Down => new(0, -1, 0, 0);
    public static Quaternion Forward => new(0, 0, 1, 0);
    public static Quaternion HighW => new(0, 0, 0, 1);
    public static Quaternion Left => new(-1, 0, 0, 0);
    public static Quaternion LowW => new(0, 0, 0, -1);
    public static Quaternion Right => new(1, 0, 0, 0);
    public static Quaternion Up => new(0, 1, 0, 0);

    public static Quaternion One => new(1, 1, 1, 1);
    public static Quaternion Zero => new(0, 0, 0, 0);

    public Quaternion Conjugate => new(u, -IJK);
    public Quaternion Inverse => Conjugate / (u * u + i * i + j * j + k * k);
    public float Magnitude => Mathf.Sqrt(u * u + i * i + j * j + k * k);
    public Quaternion Normalized => this * Mathf.InverseSqrt(u * u + i * i + j * j + k * k);

    public Float3 IJK
    {
        get => new(i, j, k);
        set
        {
            i = value.x;
            j = value.y;
            k = value.z;
        }
    }

    public float u = u;
    public float i = i;
    public float j = j;
    public float k = k;

    public Quaternion(float all) : this(all, all, all, all) { }
    public Quaternion(float i, float j, float k) : this(0, i, j, k) { }
    public Quaternion(Float3 ijk) : this(0, ijk.x, ijk.y, ijk.z) { }
    public Quaternion(float u, Float3 ijk) : this(u, ijk.x, ijk.y, ijk.z) { }
    public Quaternion(Fill<float> fill) : this(fill(0), fill(1), fill(2)) { }
    public Quaternion(Fill<int> fill) : this(fill(0), fill(1), fill(2)) { }

    public float this[int index]
    {
        get => index switch
        {
            0 => u,
            1 => i,
            2 => j,
            3 => k,
            _ => throw new IndexOutOfRangeException(nameof(index)),
        };
        set
        {
            switch (index)
            {
                case 0:
                    u = value;
                    break;

                case 1:
                    i = value;
                    break;

                case 2:
                    j = value;
                    break;

                case 3:
                    k = value;
                    break;

                default: throw new IndexOutOfRangeException(nameof(index));
            }
        }
    }
    public float this[Index index]
    {
        get => this[index.IsFromEnd ? 4 - index.Value : index.Value];
        set => this[index.IsFromEnd ? 4 - index.Value : index.Value] = value;
    }
    public float[] this[Range range]
    {
        get
        {
            int start = range.Start.IsFromEnd ? 4 - range.Start.Value : range.Start.Value;
            int end = range.End.IsFromEnd ? 4 - range.End.Value : range.End.Value;
            List<float> res = new();
            for (int i = start; i < end; i++) res.Add(this[i]);
            return res.ToArray();
        }
        set
        {
            int start = range.Start.IsFromEnd ? 4 - range.Start.Value : range.Start.Value;
            int end = range.End.IsFromEnd ? 4 - range.End.Value : range.End.Value;
            for (int i = start; i < end; i++) this[i] = value[i];
        }
    }

    public static Quaternion Absolute(Quaternion val) => Float4.Absolute(val);
    public static Quaternion Average(params Quaternion[] vals)
    {
        List<Float4> floats = new();
        foreach (Quaternion q in vals) floats.Add(q);
        return Float4.Average(floats.ToArray());
    }
    public static Quaternion Ceiling(Quaternion val) => Float4.Ceiling(val);
    public static Quaternion Clamp(Quaternion val, Quaternion min, Quaternion max) => Float4.Clamp(val, min, max);
    public static Quaternion ClampMagnitude(Quaternion val, float minMag, float maxMag) =>
        Float4.ClampMagnitude(val, minMag, maxMag);
    public static Quaternion Divide(Quaternion num, params Quaternion[] vals)
    {
        List<Float4> floats = new();
        foreach (Quaternion q in vals) floats.Add(q);
        return Float4.Divide(num, floats.ToArray());
    }
    public static float Dot(Quaternion a, Quaternion b) => a.u * b.u + a.i * b.i + a.j * b.j + a.k * b.k;
    public static float Dot(params Quaternion[] vals)
    {
        List<Float4> floats = new();
        foreach (Quaternion q in vals) floats.Add(q);
        return Float4.Dot(floats.ToArray());
    }
    public static Quaternion Floor(Quaternion val) => Float4.Floor(val);
    public static Quaternion Lerp(Quaternion a, Quaternion b, float t, bool clamp = true) => Float4.Lerp(a, b, t, clamp);
    public static Quaternion Median(params Quaternion[] vals)
    {
        List<Float4> floats = new();
        foreach (Quaternion q in vals) floats.Add(q);
        return Float4.Median(floats.ToArray());
    }
    public static Quaternion Max(params Quaternion[] vals)
    {
        List<Float4> floats = new();
        foreach (Quaternion q in vals) floats.Add(q);
        return Float4.Max(floats.ToArray());
    }
    public static Quaternion Min(params Quaternion[] vals)
    {
        List<Float4> floats = new();
        foreach (Quaternion q in vals) floats.Add(q);
        return Float4.Min(floats.ToArray());
    }
    public static Quaternion Product(params Quaternion[] vals)
    {
        List<Float4> floats = new();
        foreach (Quaternion q in vals) floats.Add(q);
        return Float4.Product(floats.ToArray());
    }
    public static Quaternion Round(Quaternion val) => Float4.Round(val);
    public static Quaternion Subtract(Quaternion num, params Quaternion[] vals)
    {
        List<Float4> floats = new();
        foreach (Quaternion q in vals) floats.Add(q);
        return Float4.Subtract(num, floats.ToArray());
    }
    public static Quaternion Sum(params Quaternion[] vals)
    {
        List<Float4> floats = new();
        foreach (Quaternion q in vals) floats.Add(q);
        return Float4.Sum(floats.ToArray());
    }

    public static Quaternion FromAngles(Angle yaw, Angle pitch, Angle? roll = null)
    {
        roll ??= Angle.Zero;
        float cosYaw, cosPitch, cosRoll, sinYaw, sinPitch, sinRoll,
              cosYawCosPitch, sinYawSinPitch, cosYawSinPitch, sinYawCosPitch;

        cosYaw = Mathf.Cos(yaw * 0.5f);
        cosPitch = Mathf.Cos(pitch * 0.5f);
        cosRoll = Mathf.Cos(roll.Value * 0.5f);
        sinYaw = Mathf.Sin(yaw * 0.5f);
        sinPitch = Mathf.Sin(pitch * 0.5f);
        sinRoll = Mathf.Sin(roll.Value * 0.5f);

        cosYawCosPitch = cosYaw * cosPitch;
        sinYawSinPitch = sinYaw * sinPitch;
        cosYawSinPitch = cosYaw * sinPitch;
        sinYawCosPitch = sinYaw * cosPitch;

        return new(cosYawCosPitch * cosRoll + sinYawSinPitch * sinRoll,
                   cosYawCosPitch * sinRoll - sinYawSinPitch * cosRoll,
                   cosYawSinPitch * cosRoll + sinYawCosPitch * sinRoll,
                   sinYawCosPitch * cosRoll - cosYawSinPitch * sinRoll);
    }
    public static Quaternion FromAngles(Float3 vals, Angle.Type valType) =>
        FromAngles(new(vals.x, valType), new(vals.y, valType), new(vals.z, valType));
    public static Quaternion FromVector(Vector3d vec) => FromAngles(vec.yaw, vec.pitch);

    public static (float[] Us, float[] Is, float[] Js, float[] Ks) SplitArray(params Quaternion[] vals)
    {
        float[] Us = new float[vals.Length], Is = new float[vals.Length], Js = new float[vals.Length],
                Ks = new float[vals.Length];
        for (int i = 0; i < vals.Length; i++)
        {
            Us[i] = vals[i].u;
            Is[i] = vals[i].i;
            Js[i] = vals[i].j;
            Ks[i] = vals[i].k;
        }
        return (Us, Is, Js, Ks);
    }

    public int CompareTo(Quaternion other) => Magnitude.CompareTo(other.Magnitude);
    public bool Equals(Quaternion other) => u == other.u && i == other.i && j == other.j && k == other.k;
    public override int GetHashCode() => base.GetHashCode();

    public Angle GetAngle() => Mathf.ArcCos(u) * 2;
    public Float3 GetAxis() => IJK.Normalized;
    public (Angle yaw, Angle pitch, Angle roll) ToAngles()
    {
        Quaternion doubled = this;
        doubled.u *= u;
        doubled.i *= i;
        doubled.j *= j;
        doubled.k *= k;

        Matrix3x3 rot = new(new[,]
        {
            { u * u + i * i - j * j - k * k, 2 * i * j + 2 * k * u, 2 * i * k - 2 * j * u },
            { 2 * i * j - 2 * k * u, u * u - i * i + j * j - k * k, 2 * j * k + 2 * i * u },
            { 2 * k * i + 2 * j * u, 2 * k * j - 2 * i * u, u * u - i * i - j * j + k * k }
        });

        Angle yaw, pitch, roll;

        float absr3c1 = Mathf.Absolute(rot.r3c1);
        if (absr3c1 >= 1)
        {
            yaw = Mathf.ArcTan2(-rot.r1c2, -rot.r3c1 * rot.r1c3);
            pitch = new(-Constants.HalfPi * rot.r3c1 / absr3c1, Angle.Type.Radians);
            roll = Angle.Zero;
        }
        else
        {
            yaw = Mathf.ArcTan2(rot.r2c1, rot.r1c1);
            pitch = Mathf.ArcSin(-rot.r3c1);
            roll = Mathf.ArcTan2(rot.r3c2, rot.r3c3);
        }

        return (yaw, pitch, roll);
    }
    public Float3 ToAnglesFloat3(Angle.Type type = Angle.Type.Degrees)
    {
        (Angle yaw, Angle pitch, Angle roll) = ToAngles();
        return new(yaw.ValueFromType(type), pitch.ValueFromType(type), roll.ValueFromType(type));
    }

    public Quaternion Rotate(Quaternion other) => this * other * Conjugate;
    public Float3 Rotate(Float3 other)
    {
        const float tolerance = 0.001f;
        Quaternion res = this * other * Conjugate;
        if (Mathf.Absolute(res.u) > tolerance)
            throw new MathException("A rotated vector should never have non-zero scalar part.");
        return res.IJK;
    }
    public Vector3d Rotate(Vector3d other) => Rotate(other.ToXYZ()).ToVector();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    public IEnumerator<float> GetEnumerator()
    {
        yield return u;
        yield return i;
        yield return j;
        yield return k;
    }

    public float[] ToArray() => new[] { u, i, j, k };
    public Fill<float> ToFill()
    {
        Quaternion @this = this;
        return i => @this[i];
    }
    public List<float> ToList() => new() { u, i, j, k };

    private bool PrintMembers(StringBuilder builder)
    {
        builder.Append(u);
        builder.Append(i >= 0 ? " + " : " - ");
        builder.Append(Mathf.Absolute(i));
        builder.Append('i');
        builder.Append(j >= 0 ? " + " : " - ");
        builder.Append(Mathf.Absolute(j));
        builder.Append('j');
        builder.Append(k >= 0 ? " + " : " - ");
        builder.Append('k');
        builder.Append(Mathf.Absolute(k));
        return true;
    }

    public static Quaternion operator +(Quaternion a, Quaternion b) => new(a.u + b.u, a.i + b.i, a.j + b.j, a.k + b.k);
    public static Quaternion operator -(Quaternion q) => new(-q.u, -q.i, -q.j, -q.k);
    public static Quaternion operator -(Quaternion a, Quaternion b) => new(a.u - b.u, a.i - b.i, a.j - b.j, a.k - b.k);
    public static Quaternion operator *(Quaternion x, Quaternion y)
    {
        float a = x.u, b = x.i, c = x.j, d = x.k, e = y.u, f = y.i, g = y.j, h = y.k,
              u = a * e - b * f - c * g - d * h,
              i = a * f + b * e + c * h - d * g,
              j = a * g - b * h + c * e + d * f,
              k = a * h + b * g + d * e - c * f;
        return new(u, i, j, k);
    }
    public static Quaternion operator *(Quaternion a, float b) => new(a.u * b, a.i * b, a.j * b, a.k * b);
    public static Quaternion operator *(Quaternion a, Matrix b) => (Quaternion)((Matrix)a * b);
    public static Quaternion operator *(Quaternion a, Float3 b) => a * new Quaternion(b);
    public static Quaternion operator /(Quaternion x, Quaternion y) => x * y.Inverse;
    public static Quaternion operator /(Quaternion a, float b) => new(a.u / b, a.i / b, a.j / b, a.k / b);
    public static Quaternion operator /(Quaternion a, Matrix b) => (Quaternion)((Matrix)a / b);
    public static Quaternion operator /(Quaternion a, Float3 b) => a / new Quaternion(b);
    public static Quaternion operator ~(Quaternion v) => v.Conjugate;

    public static implicit operator Quaternion(Complex val) => new(val.u, val.i, 0, 0);
    public static implicit operator Quaternion(Int2 val) => new(val);
    public static implicit operator Quaternion(Int3 val) => new(val);
    public static implicit operator Quaternion(Int4 val) => new(val.x, val.y, val.z, val.w);
    public static explicit operator Quaternion(Float2 val) => new(val);
    public static explicit operator Quaternion(Float3 val) => new(val);
    public static implicit operator Quaternion(Float4 val) => new(val.x, val.y, val.z, val.w);
    public static explicit operator Quaternion(Matrix m) => new(m[0, 0], m[1, 0], m[2, 0], m[3, 0]);
    public static explicit operator Quaternion(Vector2d val) => (Quaternion)val.ToXYZ();
    public static implicit operator Quaternion(Vert val) => new(val);
    public static implicit operator Quaternion(Fill<float> fill) => new(fill);
    public static implicit operator Quaternion(Fill<int> fill) => new(fill);
    public static implicit operator Quaternion((float u, float i, float j, float k) val) =>
        new(val.u, val.i, val.j, val.k);

    public static implicit operator Quaternion(SystemQuaternion val) => (val.X, val.Y, val.Z, val.W);
    public static implicit operator SystemQuaternion(Quaternion val) => new(val.u, val.i, val.j, val.k);
}
