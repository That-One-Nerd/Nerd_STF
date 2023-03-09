namespace Nerd_STF.Mathematics.Algebra;

public record struct Vector2d : IAbsolute<Vector2d>, IAverage<Vector2d>,
    IClampMagnitude<Vector2d, float>, IComparable<Vector2d>, ICross<Vector2d, Vector3d>,
    IDot<Vector2d, float>, IEquatable<Vector2d>, IFromTuple<Vector2d, (Angle angle, float mag)>,
    ILerp<Vector2d, float>, IMax<Vector2d>, IMagnitude<float>, IMedian<Vector2d>, IMin<Vector2d>,
    IPresets2D<Vector2d>, ISplittable<Vector2d, (Angle[] rots, float[] mags)>, ISubtract<Vector2d>,
    ISum<Vector2d>
{
    public static Vector2d Down => new(Angle.Down);
    public static Vector2d Left => new(Angle.Left);
    public static Vector2d Right => new(Angle.Right);
    public static Vector2d Up => new(Angle.Up);

    public static Vector2d One => new(Angle.Zero);
    public static Vector2d Zero => new(Angle.Zero, 0);

    public float Magnitude
    {
        get => magnitude;
        set => magnitude = value;
    }

    public Vector2d Inverse => new(-theta, magnitude);
    public Vector2d Normalized => new(theta, 1);

    public Angle theta;
    public float magnitude;

    public Vector2d(Angle theta, float mag = 1)
    {
        this.theta = theta;
        magnitude = mag;
    }
    public Vector2d(float theta, Angle.Type rotType, float mag = 1) : this(new(theta, rotType), mag) { }

    public static Vector2d Absolute(Vector2d val) => new(Angle.Absolute(val.theta), Mathf.Absolute(val.magnitude));
    public static Vector2d Average(params Vector2d[] vals)
    {
        (Angle[] thetas, float[] Mags) = SplitArray(vals);
        return new(Angle.Average(thetas), Mathf.Average(Mags));
    }
    public static Vector2d Ceiling(Vector2d val, Angle.Type angleRound = Angle.Type.Degrees) =>
        new(Angle.Ceiling(val.theta, angleRound), Mathf.Ceiling(val.magnitude));
    public static Vector2d ClampMagnitude(Vector2d val, float minMag, float maxMag)
    {
        if (maxMag < minMag) throw new ArgumentOutOfRangeException(nameof(maxMag),
            nameof(maxMag) + " must be greater than or equal to " + nameof(minMag));
        float mag = Mathf.Clamp(val.magnitude, minMag, maxMag);
        return new(val.theta, mag);
    }
    public static Vector3d Cross(Vector2d a, Vector2d b, bool normalized = false) =>
        Float2.Cross(a.ToXYZ(), b.ToXYZ(), normalized).ToVector();
    public static float Dot(Vector2d a, Vector2d b) => Float2.Dot(a.ToXYZ(), b.ToXYZ());
    public static float Dot(params Vector2d[] vals)
    {
        Float2[] floats = new Float2[vals.Length];
        for (int i = 0; i < vals.Length; i++) floats[i] = vals[i].ToXYZ();
        return Float2.Dot(floats);
    }
    public static Vector2d Floor(Vector2d val, Angle.Type angleRound = Angle.Type.Degrees) =>
        new(Angle.Floor(val.theta, angleRound), Mathf.Floor(val.magnitude));
    public static Vector2d Lerp(Vector2d a, Vector2d b, float t, bool clamp = true) =>
        new(Angle.Lerp(a.theta, b.theta, t, clamp), Mathf.Lerp(a.magnitude, b.magnitude, t, clamp));
    public static Vector2d Median(params Vector2d[] vals)
    {
        float index = Mathf.Average(0, vals.Length - 1);
        Vector2d valA = vals[Mathf.Floor(index)], valB = vals[Mathf.Ceiling(index)];
        return Average(valA, valB);
    }
    public static Vector2d Max(params Vector2d[] vals)
    {
        if (vals.Length < 1) return Zero;
        Vector2d val = vals[0];
        foreach (Vector2d f in vals) val = f > val ? f : val;
        return val;
    }
    public static Vector2d Min(params Vector2d[] vals)
    {
        if (vals.Length < 1) return Zero;
        Vector2d val = vals[0];
        foreach (Vector2d f in vals) val = f < val ? f : val;
        return val;
    }
    public static Vector2d Round(Vector2d val, Angle.Type angleRound = Angle.Type.Degrees) =>
        new(Angle.Round(val.theta, angleRound), Mathf.Round(val.magnitude));
    public static Vector2d Subtract(Vector2d num, params Vector2d[] vals)
    {
        foreach (Vector2d v in vals) num -= v;
        return num;
    }
    public static Vector2d Sum(params Vector2d[] vals)
    {
        if (vals.Length < 1) return Zero;
        Vector2d val = One;
        foreach (Vector2d v in vals) val += v;
        return val;
    }

    public static (Angle[] rots, float[] mags) SplitArray(params Vector2d[] vals)
    {
        Angle[] rots = new Angle[vals.Length];
        float[] mags = new float[vals.Length];
        for (int i = 0; i < vals.Length; i++)
        {
            rots[i] = vals[i].theta;
            mags[i] = vals[i].magnitude;
        }
        return (rots, mags);
    }

    public int CompareTo(Vector2d other) => magnitude.CompareTo(other.magnitude);
    public bool Equals(Vector2d other) => theta == other.theta && magnitude == other.magnitude;
    public override int GetHashCode() => base.GetHashCode();
    public override string ToString() => ToString(Angle.Type.Degrees);
    public string ToString(Angle.Type outputType) =>
        nameof(Vector2d) + " { Mag = " + magnitude + ", Rot = " + theta.ToString(outputType) + " }";

    public Float2 ToXYZ() => new Float2(Mathf.Cos(theta), Mathf.Sin(theta)) * magnitude;

    public static Vector2d operator +(Vector2d a, Vector2d b) => new(a.theta + b.theta, a.magnitude + b.magnitude);
    public static Vector2d operator -(Vector2d v) => v.Inverse;
    public static Vector2d operator -(Vector2d a, Vector2d b) => new(a.theta - b.theta, a.magnitude - b.magnitude);
    public static Vector2d operator *(Vector2d a, float b) => new(a.theta, a.magnitude * b);
    public static Vector2d operator *(Vector2d a, Matrix b) => (Vector2d)((Matrix)a * b);
    public static Vector2d operator /(Vector2d a, float b) => new(a.theta, a.magnitude / b);
    public static Vector2d operator /(Vector2d a, Matrix b) => (Vector2d)((Matrix)a / b);
    public static bool operator >(Vector2d a, Vector2d b) => a.CompareTo(b) > 0;
    public static bool operator <(Vector2d a, Vector2d b) => a.CompareTo(b) < 0;
    public static bool operator >=(Vector2d a, Vector2d b) => a == b || a > b;
    public static bool operator <=(Vector2d a, Vector2d b) => a == b || a < b;

    public static explicit operator Vector2d(Complex val) => val.ToVector();
    public static explicit operator Vector2d(Float2 val) => val.ToVector();
    public static explicit operator Vector2d(Float3 val) => (Vector2d)val.ToVector();
    public static explicit operator Vector2d(Int2 val) => val.ToVector();
    public static explicit operator Vector2d(Int3 val) => (Vector2d)val.ToVector();
    public static explicit operator Vector2d(Matrix m) => ((Float2)m).ToVector();
    public static explicit operator Vector2d(Vert val) => (Vector2d)val.ToVector();
    public static explicit operator Vector2d(Vector3d val) => new(val.yaw, val.magnitude);
    public static implicit operator Vector2d((Angle angle, float mag) val) => new(val.angle, val.mag);
}
