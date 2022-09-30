namespace Nerd_STF.Mathematics.NumberSystems;

public struct Quaternion : ICloneable, IComparable<Quaternion>, IEquatable<Quaternion>, IGroup<float>
{
    public static Quaternion Back => new(0, 0, -1, 0);
    public static Quaternion Down => new(0, -1, 0, 0);
    public static Quaternion Far => new(0, 0, 0, 1);
    public static Quaternion Forward => new(0, 0, 1, 0);
    public static Quaternion Left => new(-1, 0, 0, 0);
    public static Quaternion Near => new(0, 0, 0, -1);
    public static Quaternion Right => new(1, 0, 0, 0);
    public static Quaternion Up => new(0, 1, 0, 0);

    public static Quaternion One => new(1, 1, 1, 1);
    public static Quaternion Zero => new(0, 0, 0, 0);

    public Quaternion Conjugate => new(u, -i, -j, -k);
    public Quaternion Inverse => Conjugate / (u * u + i * i + j * j + k * k);
    public float Magnitude => Mathf.Sqrt(u * u + i * i + j * j + k * k);
    public Quaternion Normalized => this * Mathf.InverseSqrt(u * u + i * i + j * j + k * k);

    public Float3 IJK => new(i, j, k);

    public float u, i, j, k;

    public Quaternion(float all) : this(all, all, all, all) { }
    public Quaternion(float i, float j, float k) : this(0, i, j, k) { }
    public Quaternion(Float3 ijk) : this(0, ijk.x, ijk.y, ijk.z) { }
    public Quaternion(float u, Float3 ijk) : this(u, ijk.x, ijk.y, ijk.z) { }
    public Quaternion(float u, float i, float j, float k)
    {
        this.u = u;
        this.i = i;
        this.j = j;
        this.k = k;
    }
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

    [Obsolete("This method does not produce the correct output. Please update to a newer release.")]
    public static Quaternion FromAngles(Angle yaw, Angle pitch, Angle? roll = null)
    {
        roll ??= Angle.Zero;
        float cosYaw = Mathf.Cos(yaw), cosPitch = Mathf.Cos(pitch), cosRoll = Mathf.Cos(roll.Value),
              sinYaw = Mathf.Sin(yaw), sinPitch = Mathf.Sin(pitch), sinRoll = Mathf.Sin(roll.Value);

        float cosYawCosPitch = cosYaw * cosPitch,
              cosYawSinPitch = cosYaw * sinPitch,
              sinYawCosPitch = sinYaw * cosPitch,
              sinYawSinPitch = sinYaw * sinPitch;

        return new(cosYawCosPitch * cosRoll + sinYawSinPitch * sinRoll,
                   cosYawCosPitch * sinRoll + sinYawSinPitch * cosRoll,
                   cosYawSinPitch * cosRoll + sinYawCosPitch * sinRoll,
                   sinYawCosPitch * cosRoll + cosYawSinPitch * sinRoll);
    }
    [Obsolete("This method does not produce the correct output. Please update to a newer release.")]
    public static Quaternion FromAngles(Float3 vals, Angle.Type valType) =>
        FromAngles(new(vals.x, valType), new(vals.y, valType), new(vals.z, valType));
    [Obsolete("This method does not produce the correct output. Please update to a newer release.")]
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
    public override bool Equals([NotNullWhen(true)] object? obj)
    {
        if (obj == null || obj.GetType() != typeof(Quaternion)) return base.Equals(obj);
        return Equals((Quaternion)obj);
    }
    public bool Equals(Quaternion other) => u == other.u && i == other.i && j == other.j && k == other.k;
    public override int GetHashCode() => u.GetHashCode() ^ i.GetHashCode() ^ j.GetHashCode() ^ k.GetHashCode();
    public override string ToString() => ToString((string?)null);
    public string ToString(string? provider) => u.ToString(provider)
        + (i >= 0 ? " + " : " - ") + Mathf.Absolute(i).ToString(provider) + "i"
        + (j >= 0 ? " + " : " - ") + Mathf.Absolute(j).ToString(provider) + "j"
        + (k >= 0 ? " + " : " - ") + Mathf.Absolute(k).ToString(provider) + "k";
    public string ToString(IFormatProvider provider) => u.ToString(provider)
        + (i >= 0 ? " + " : " - ") + Mathf.Absolute(i).ToString(provider) + "i"
        + (j >= 0 ? " + " : " - ") + Mathf.Absolute(j).ToString(provider) + "j"
        + (k >= 0 ? " + " : " - ") + Mathf.Absolute(k).ToString(provider) + "k";

    public object Clone() => new Quaternion(u, i, j, k);

    [Obsolete("This method does not produce the correct output. Please update to a newer release.")]
    public Angle GetAngle() => Mathf.ArcCos(u) * 2;
    [Obsolete("This method does not produce the correct output. Please update to a newer release.")]
    public Float3 GetAxis()
    {
        Float3 axis = IJK;
        float mag = Magnitude;

        if (mag < 0) return Float3.Zero;
        return axis / mag;
    }
    [Obsolete("This method does not produce the correct output. Please update to a newer release.")]
    public (Angle yaw, Angle pitch, Angle roll) ToAngles()
    {
        Quaternion doubled = this;
        doubled.u *= u;
        doubled.i *= i;
        doubled.j *= j;
        doubled.k *= k;

        Matrix3x3 rotMatrix = new(new[,]
        {
            { doubled.u + doubled.i - doubled.j - doubled.k, 0, 0 },
            { 2 * (i * j + u * k), 0, 0 },
            { 2 * (i * k + u * j), 2 * (j * k + u * i), doubled.u - doubled.i - doubled.j + doubled.k }
        });

        Angle yaw, pitch, roll;

        float r3c1Abs = Mathf.Absolute(rotMatrix.r3c1);
        if (r3c1Abs >= 1)
        {
            rotMatrix.r1c2 = 2 * (i * j - u * k);
            rotMatrix.r1c3 = 2 * (i * k + u * j);

            yaw = Mathf.ArcTan2(-rotMatrix.r1c2, -rotMatrix.r3c1 * rotMatrix.r1c3);
            pitch = new(-Constants.HalfPi * rotMatrix.r3c1 / r3c1Abs, Angle.Type.Radians);
            roll = Angle.Zero;
        }
        else
        {
            yaw = Mathf.ArcTan2(rotMatrix.r2c1, rotMatrix.r1c1);
            pitch = Mathf.ArcSin(-rotMatrix.r3c1);
            roll = Mathf.ArcTan2(rotMatrix.r3c2, rotMatrix.r3c3);
        }

        return (yaw, pitch, roll);
    }
    [Obsolete("This method does not produce the correct output. Please update to a newer release.")]
    public Vector3d ToVector()
    {
        (Angle yaw, Angle pitch, _) = ToAngles();
        return new(yaw, pitch);
    }

    public Quaternion Rotate(Quaternion other) => other * this * other.Conjugate;

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

    public static Quaternion operator +(Quaternion a, Quaternion b) => new(a.u + b.u, a.i + b.i, a.j + b.j, a.k + b.k);
    public static Quaternion operator -(Quaternion q) => new(q.u, q.i, q.j, q.k);
    public static Quaternion operator -(Quaternion a, Quaternion b) => new(a.u - b.u, a.i - b.i, a.j - b.j, a.k - b.k);
    public static Quaternion operator *(Quaternion x, Quaternion y)
    {
        float a = x.u, b = x.i, c = x.j, d = x.k, e = y.u, f = y.i, g = y.j, h = y.k,
              u = a * e - b * f - c * g - d * h,
              i = a * f + b * e + c * h - d * g,
              j = a * g + c * e + b * h - d * f,
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
    public static bool operator ==(Quaternion a, Quaternion b) => a.Equals(b);
    public static bool operator !=(Quaternion a, Quaternion b) => !a.Equals(b);
    public static bool operator >(Quaternion a, Quaternion b) => a.CompareTo(b) > 0;
    public static bool operator <(Quaternion a, Quaternion b) => a.CompareTo(b) < 0;
    public static bool operator >=(Quaternion a, Quaternion b) => a == b || a > b;
    public static bool operator <=(Quaternion a, Quaternion b) => a == b || a < b;

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
}
