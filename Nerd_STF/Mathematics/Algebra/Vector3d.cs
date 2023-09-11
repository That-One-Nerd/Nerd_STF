namespace Nerd_STF.Mathematics.Algebra;

public record struct Vector3d : IAbsolute<Vector3d>, IAverage<Vector3d>, IClampMagnitude<Vector3d, float>,
    IComparable<Vector3d>, ICross<Vector3d>, IDot<Vector3d, float>, IEquatable<Vector3d>,
    IFromTuple<Vector3d, (Angle yaw, Angle pitch, float mag)>, IIndexAll<Angle>, IIndexRangeAll<Angle>,
    ILerp<Vector3d, float>, IMagnitude<float>, IMax<Vector3d>, IMedian<Vector3d>, IMin<Vector3d>,
    IPresets3d<Vector3d>
{
    public static Vector3d Back => new(Angle.Zero, Angle.Up);
    public static Vector3d Down => new(Angle.Down, Angle.Zero);
    public static Vector3d Forward => new(Angle.Zero, Angle.Down);
    public static Vector3d Left => new(Angle.Left, Angle.Zero);
    public static Vector3d Right => new(Angle.Right, Angle.Zero);
    public static Vector3d Up => new(Angle.Up, Angle.Zero);

    public static Vector3d One => new(Angle.Zero);
    public static Vector3d Zero => new(Angle.Zero, 0);

    public float Magnitude
    {
        get => magnitude;
        set => magnitude = value;
    }

    public Vector3d Inverse => new(-yaw, -pitch, magnitude);
    public Vector3d Normalized => new(yaw, pitch, 1);

    public Angle yaw, pitch;
    public float magnitude;

    public Vector3d(Angle allRot, float mag = 1) : this(allRot, allRot, mag) { }
    public Vector3d(float allRot, Angle.Type rotType, float mag = 1) : this(allRot, allRot, rotType, mag) { }
    public Vector3d(Angle yaw, Angle pitch, float mag = 1)
    {
        this.yaw = yaw;
        this.pitch = pitch;
        magnitude = mag;
    }
    public Vector3d(float yaw, float pitch, Angle.Type rotType, float mag = 1)
        : this(new Angle(yaw, rotType), new(pitch, rotType), mag) { }
    public Vector3d(Float2 rots, Angle.Type rotType, float mag = 1) : this(rots.x, rots.y, rotType, mag) { }
    public Vector3d(Fill<Angle> fill, float mag = 1) : this(fill(0), fill(1), mag) { }
    public Vector3d(Fill<float> fill, Angle.Type rotType, float mag = 1) : this(fill(0), fill(1), rotType, mag) { }

    public Angle this[int index]
    {
        get => index switch
        {
            0 => yaw,
            1 => pitch,
            _ => throw new IndexOutOfRangeException(nameof(index)),
        };
        set
        {
            switch (index)
            {
                case 0:
                    yaw = value;
                    break;

                case 1:
                    pitch = value;
                    break;

                default: throw new IndexOutOfRangeException(nameof(index));
            }
        }
    }
    public Angle this[Index index]
    {
        get => this[index.IsFromEnd ? 2 - index.Value : index.Value];
        set => this[index.IsFromEnd ? 2 - index.Value : index.Value] = value;
    }
    public Angle[] this[Range range]
    {
        get
        {
            int start = range.Start.IsFromEnd ? 2 - range.Start.Value : range.Start.Value;
            int end = range.End.IsFromEnd ? 2 - range.End.Value : range.End.Value;
            List<Angle> res = new();
            for (int i = start; i < end; i++) res.Add(this[i]);
            return res.ToArray();
        }
        set
        {
            int start = range.Start.IsFromEnd ? 2 - range.Start.Value : range.Start.Value;
            int end = range.End.IsFromEnd ? 2 - range.End.Value : range.End.Value;
            for (int i = start; i < end; i++) this[i] = value[i];
        }
    }

    public static Vector3d Absolute(Vector3d val) => new(Angle.Absolute(val.yaw), Angle.Absolute(val.pitch),
        Mathf.Absolute(val.magnitude));
    public static Vector3d Average(params Vector3d[] vals)
    {
        (Angle[] Thetas, Angle[] Phis, float[] Mags) = SplitArray(vals);
        return new(Angle.Average(Thetas), Angle.Average(Phis), Mathf.Average(Mags));
    }
    public static Vector3d Ceiling(Vector3d val, Angle.Type angleRound = Angle.Type.Degrees) =>
        new(Angle.Ceiling(val.yaw, angleRound), Angle.Ceiling(val.pitch, angleRound),
            Mathf.Ceiling(val.magnitude));
    public static Vector3d ClampMagnitude(Vector3d val, float minMag, float maxMag)
    {
        if (maxMag < minMag) throw new ArgumentOutOfRangeException(nameof(maxMag),
            nameof(maxMag) + " must be greater than or equal to " + nameof(minMag));
        float mag = Mathf.Clamp(val.magnitude, minMag, maxMag);
        return new(val.yaw, val.pitch, mag);
    }
    public static Vector3d Cross(Vector3d a, Vector3d b, bool normalized = false) =>
        Float3.Cross(a.ToXYZ(), b.ToXYZ(), normalized).ToVector();
    public static float Dot(Vector3d a, Vector3d b) => Float3.Dot(a.ToXYZ(), b.ToXYZ());
    public static float Dot(params Vector3d[] vals)
    {
        Float3[] floats = new Float3[vals.Length];
        for (int i = 0; i < vals.Length; i++) floats[i] = vals[i].ToXYZ();
        return Float3.Dot(floats);
    }
    public static Vector3d Floor(Vector3d val, Angle.Type angleRound = Angle.Type.Degrees) =>
        new(Angle.Floor(val.yaw, angleRound), Angle.Floor(val.pitch, angleRound), Mathf.Floor(val.magnitude));
    public static Vector3d Lerp(Vector3d a, Vector3d b, float t, bool clamp = true) =>
        new(Angle.Lerp(a.yaw, b.yaw, t, clamp), Angle.Lerp(a.pitch, b.pitch, t, clamp),
            Mathf.Lerp(a.magnitude, b.magnitude, t, clamp));
    public static Vector3d Median(params Vector3d[] vals)
    {
        float index = Mathf.Average(0, vals.Length - 1);
        Vector3d valA = vals[Mathf.Floor(index)], valB = vals[Mathf.Ceiling(index)];
        return Average(valA, valB);
    }
    public static Vector3d Max(params Vector3d[] vals)
    {
        if (vals.Length < 1) return Zero;
        Vector3d val = vals[0];
        foreach (Vector3d f in vals) val = f > val ? f : val;
        return val;
    }
    public static Vector3d Min(params Vector3d[] vals)
    {
        if (vals.Length < 1) return Zero;
        Vector3d val = vals[0];
        foreach (Vector3d f in vals) val = f < val ? f : val;
        return val;
    }
    public static Vector3d Round(Vector3d val, Angle.Type angleRound = Angle.Type.Degrees) =>
        new(Angle.Round(val.yaw, angleRound), Angle.Round(val.pitch, angleRound), Mathf.Round(val.magnitude));

    public static (Angle[] yaws, Angle[] pitches, float[] mags) SplitArray(params Vector3d[] vals)
    {
        Angle[] yaws = new Angle[vals.Length], pitches = new Angle[vals.Length];
        float[] mags = new float[vals.Length];
        for (int i = 0; i < vals.Length; i++)
        {
            yaws[i] = vals[i].yaw;
            pitches[i] = vals[i].pitch;
            mags[i] = vals[i].magnitude;
        }
        return (yaws, pitches, mags);
    }

    public int CompareTo(Vector3d other) => magnitude.CompareTo(other.magnitude);
    public bool Equals(Vector3d other) => yaw == other.yaw && pitch == other.pitch
        && magnitude == other.magnitude;
    public override int GetHashCode() => base.GetHashCode();
    public override string ToString() => ToString(Angle.Type.Degrees);
    public string ToString(Angle.Type outputType) =>
        nameof(Vector3d) + " { Mag = " + magnitude + ", Yaw = " + yaw.ToString(outputType) +
        ", Pitch = " + pitch.ToString(outputType) + " }";

    public Float3 ToXYZ() => new Float3(Mathf.Sin(pitch) * Mathf.Sin(yaw),
                                        Mathf.Cos(yaw),
                                        Mathf.Cos(pitch) * Mathf.Sin(yaw)) * magnitude;

    public static Vector3d operator +(Vector3d a, Vector3d b) => new(a.yaw + b.yaw, a.pitch + b.pitch,
        a.magnitude + b.magnitude);
    public static Vector3d operator -(Vector3d v) => v.Inverse;
    public static Vector3d operator -(Vector3d a, Vector3d b) => new(a.yaw - b.yaw, a.pitch - b.pitch,
        a.magnitude - b.magnitude);
    public static Vector3d operator *(Vector3d a, float b) => new(a.yaw, a.pitch, a.magnitude * b);
    public static Vector3d operator *(Vector3d a, Matrix b) => (Vector3d)((Matrix)a * b);
    public static Vector3d operator /(Vector3d a, float b) => new(a.yaw, a.pitch, a.magnitude / b);
    public static Vector3d operator /(Vector3d a, Matrix b) => (Vector3d)((Matrix)a / b);
    public static bool operator >(Vector3d a, Vector3d b) => a.CompareTo(b) > 0;
    public static bool operator <(Vector3d a, Vector3d b) => a.CompareTo(b) < 0;
    public static bool operator >=(Vector3d a, Vector3d b) => a == b || a > b;
    public static bool operator <=(Vector3d a, Vector3d b) => a == b || a < b;

    public static explicit operator Vector3d(Complex val) => val.ToVector();
    public static explicit operator Vector3d(Float2 val) => val.ToVector();
    public static explicit operator Vector3d(Float3 val) => val.ToVector();
    public static explicit operator Vector3d(Int2 val) => val.ToVector();
    public static explicit operator Vector3d(Int3 val) => val.ToVector();
    public static explicit operator Vector3d(Matrix m) => ((Float3)m).ToVector();
    public static implicit operator Vector3d(Vector2d v) => new(v.theta, Angle.Zero, v.magnitude);
    public static implicit operator Vector3d((Angle yaw, Angle pitch, float mag) val) =>
        new(val.yaw, val.pitch, val.mag);
}
