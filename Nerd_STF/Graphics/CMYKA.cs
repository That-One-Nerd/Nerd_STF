namespace Nerd_STF.Graphics;

public record struct CMYKA : IAverage<CMYKA>, IClamp<CMYKA>, IColorFloat<CMYKA>, IColorPresets<CMYKA>,
    IEquatable<CMYKA>, IIndexAll<float>, IIndexRangeAll<float>, ILerp<CMYKA, float>, IMedian<CMYKA>,
    ISplittable<CMYKA, (float[] Cs, float[] Ms, float[] Ys, float[] Ks, float[] As)>
{
    public static CMYKA Black => new(0, 0, 0, 1);
    public static CMYKA Blue => new(1, 1, 0, 0);
    public static CMYKA Clear => new(0, 0, 0, 0, 0);
    public static CMYKA Cyan => new(1, 0, 0, 0);
    public static CMYKA Gray => new(0, 0, 0, 0.5f);
    public static CMYKA Green => new(1, 0, 1, 0);
    public static CMYKA Magenta => new(0, 1, 0, 0);
    public static CMYKA Orange => new(0, 0.5f, 1, 0);
    public static CMYKA Purple => new(0.5f, 1, 0, 0);
    public static CMYKA Red => new(0, 1, 1, 0);
    public static CMYKA White => new(0, 0, 0, 0);
    public static CMYKA Yellow => new(0, 0, 1, 0);

    public float C
    {
        get => p_c;
        set => p_c = Mathf.Clamp(value, 0, 1);
    }
    public float M
    {
        get => p_m;
        set => p_m = Mathf.Clamp(value, 0, 1);
    }
    public float Y
    {
        get => p_y;
        set => p_y = Mathf.Clamp(value, 0, 1);
    }
    public float K
    {
        get => p_k;
        set => p_k = Mathf.Clamp(value, 0, 1);
    }
    public float A
    {
        get => p_a;
        set => p_a = Mathf.Clamp(value, 0, 1);
    }

    public bool HasCyan => p_c > 0;
    public bool HasMagenta => p_m > 0;
    public bool HasYellow => p_y > 0;
    public bool HasBlack => p_k > 0;
    public bool IsOpaque => p_a == 1;
    public bool IsVisible => p_a != 0;

    private float p_c, p_m, p_y, p_k, p_a;

    public CMYKA() : this(0, 0, 0, 0, 1) { }
    public CMYKA(float all) : this(all, all, all, all, all) { }
    public CMYKA(float all, float a) : this(all, all, all, all, a) { }
    public CMYKA(float c, float m, float y, float k) : this(c, m, y, k, 1) { }
    public CMYKA(float c, float m, float y, float k, float a)
    {
        p_c = Mathf.Clamp(c, 0, 1);
        p_m = Mathf.Clamp(m, 0, 1);
        p_y = Mathf.Clamp(y, 0, 1);
        p_k = Mathf.Clamp(k, 0, 1);
        p_a = Mathf.Clamp(a, 0, 1);
    }
    public CMYKA(Fill<float> fill) : this(fill(0), fill(1), fill(2), fill(3), fill(4)) { }

    public float this[int index]
    {
        get => index switch
        {
            0 => C,
            1 => M,
            2 => Y,
            3 => K,
            4 => A,
            _ => throw new IndexOutOfRangeException(nameof(index)),
        };
        set
        {
            switch (index)
            {
                case 0:
                    C = value;
                    break;

                case 1:
                    M = value;
                    break;

                case 2:
                    Y = value;
                    break;

                case 3:
                    K = value;
                    break;

                case 4:
                    A = value;
                    break;

                default: throw new IndexOutOfRangeException(nameof(index));
            }
        }
    }
    public float this[Index index]
    {
        get => this[index.IsFromEnd ? 5 - index.Value : index.Value];
        set => this[index.IsFromEnd ? 5 - index.Value : index.Value] = value;
    }
    public float[] this[Range range]
    {
        get
        {
            int start = range.Start.IsFromEnd ? 5 - range.Start.Value : range.Start.Value;
            int end = range.End.IsFromEnd ? 5 - range.End.Value : range.End.Value;
            List<float> res = new();
            for (int i = start; i < end; i++) res.Add(this[i]);
            return res.ToArray();
        }
        set
        {
            int start = range.Start.IsFromEnd ? 5 - range.Start.Value : range.Start.Value;
            int end = range.End.IsFromEnd ? 5 - range.End.Value : range.End.Value;
            for (int i = start; i < end; i++) this[i] = value[i];
        }
    }

    public static CMYKA Average(params CMYKA[] vals)
    {
        CMYKA val = new(0, 0, 0, 0, 0);
        for (int i = 0; i < vals.Length; i++) val += vals[i];

        return val / vals.Length;
    }
    public static CMYKA Clamp(CMYKA val, CMYKA min, CMYKA max) =>
        new(Mathf.Clamp(val.C, min.C, max.C),
            Mathf.Clamp(val.M, min.M, max.M),
            Mathf.Clamp(val.Y, min.Y, max.Y),
            Mathf.Clamp(val.K, min.K, max.K),
            Mathf.Clamp(val.A, min.A, max.A));
    public static CMYKA Lerp(CMYKA a, CMYKA b, float t, bool clamp = true) =>
        new(Mathf.Lerp(a.C, b.C, t, clamp), Mathf.Lerp(a.M, b.M, t, clamp), Mathf.Lerp(a.Y, b.Y, t, clamp),
            Mathf.Lerp(a.K, b.K, t, clamp), Mathf.Lerp(a.A, b.A, t, clamp));
    public static CMYKA LerpSquared(CMYKA a, CMYKA b, float t, bool clamp = true)
    {
        CMYKA val = Lerp(a * a, b * b, t, clamp);
        float C = Mathf.Sqrt(val.C), M = Mathf.Sqrt(val.M), Y = Mathf.Sqrt(val.Y), K = Mathf.Sqrt(val.K), A = Mathf.Sqrt(val.A);
        return new(C, M, Y, K, A);
    }
    public static CMYKA Median(params CMYKA[] vals)
    {
        float index = Mathf.Average(0, vals.Length - 1);
        CMYKA valA = vals[Mathf.Floor(index)], valB = vals[Mathf.Ceiling(index)];
        return Average(valA, valB);
    }

    public static (float[] Cs, float[] Ms, float[] Ys, float[] Ks, float[] As) SplitArray(params CMYKA[] vals)
    {
        float[] Cs = new float[vals.Length], Ms = new float[vals.Length],
                Ys = new float[vals.Length], Ks = new float[vals.Length],
                As = new float[vals.Length];
        for (int i = 0; i < vals.Length; i++)
        {
            Cs[i] = vals[i].C;
            Ms[i] = vals[i].M;
            Ys[i] = vals[i].Y;
            Ks[i] = vals[i].K;
            As[i] = vals[i].A;
        }
        return (Cs, Ms, Ys, Ks, As);
    }

    public bool Equals(IColor? col) => col != null && Equals(col.ToCMYKA());
    public bool Equals(CMYKA col) => A == 0 && col.A == 0 || K == 1 && col.K == 1 || C == col.C && M == col.M
        && Y == col.Y && K == col.K && A == col.A;
    public override int GetHashCode() => base.GetHashCode();

    public RGBA ToRGBA()
    {
        float kInv = 1 - K, r = 1 - C, g = 1 - M, b = 1 - Y;
        return new(r * kInv, g * kInv, b * kInv, A);
    }
    public CMYKA ToCMYKA() => this;
    public HSVA ToHSVA() => ToRGBA().ToHSVA();

    public RGBAByte ToRGBAByte() => ToRGBA().ToRGBAByte();
    public CMYKAByte ToCMYKAByte() => new(Mathf.RoundInt(C * 255), Mathf.RoundInt(M * 255), Mathf.RoundInt(Y * 255),
        Mathf.RoundInt(K * 255), Mathf.RoundInt(A * 255));
    public HSVAByte ToHSVAByte() => ToRGBA().ToHSVAByte();

    public float[] ToArray() => new[] { C, M, Y, K, A };
    public Fill<float> ToFill()
    {
        CMYKA @this = this;
        return i => @this[i];
    }
    public List<float> ToList() => new() { C, M, Y, K, A };

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    public IEnumerator<float> GetEnumerator()
    {
        yield return C;
        yield return M;
        yield return Y;
        yield return K;
        yield return A;
    }

    private bool PrintMembers(StringBuilder builder)
    {
        builder.Append("C = ");
        builder.Append(C);
        builder.Append(", M = ");
        builder.Append(M);
        builder.Append(", Y = ");
        builder.Append(Y);
        builder.Append(", K = ");
        builder.Append(K);
        builder.Append(", A = ");
        builder.Append(A);
        return true;
    }

    public static CMYKA operator +(CMYKA a, CMYKA b) => new(a.C + b.C, a.M + b.M, a.Y + b.Y, a.K + b.K, a.A + b.A);
    public static CMYKA operator -(CMYKA c) => new(1 - c.C, 1 - c.M, 1 - c.Y, 1 - c.K, c.A != 1 ? 1 - c.A : 1);
    public static CMYKA operator -(CMYKA a, CMYKA b) => new(a.C - b.C, a.M - b.M, a.Y - b.Y, a.K - b.K, a.A - b.A);
    public static CMYKA operator *(CMYKA a, CMYKA b) => new(a.C * b.C, a.M * b.M, a.Y * b.Y, a.K * b.K, a.A * b.A);
    public static CMYKA operator *(CMYKA a, float b) => new(a.C * b, a.M * b, a.Y * b, a.K * b, a.A * b);
    public static CMYKA operator /(CMYKA a, CMYKA b) => new(a.C / b.C, a.M / b.M, a.Y / b.Y, a.K / b.K, a.A / b.A);
    public static CMYKA operator /(CMYKA a, float b) => new(a.C / b, a.M / b, a.Y / b, a.K / b, a.A / b);
    public static bool operator ==(CMYKA a, HSVA b) => a.Equals(b);
    public static bool operator !=(CMYKA a, HSVA b) => a.Equals(b);
    public static bool operator ==(CMYKA a, RGBA b) => a.Equals(b);
    public static bool operator !=(CMYKA a, RGBA b) => a.Equals(b);
    public static bool operator ==(CMYKA a, CMYKAByte b) => a.Equals(b);
    public static bool operator !=(CMYKA a, CMYKAByte b) => a.Equals(b);
    public static bool operator ==(CMYKA a, HSVAByte b) => a.Equals(b);
    public static bool operator !=(CMYKA a, HSVAByte b) => a.Equals(b);
    public static bool operator ==(CMYKA a, RGBAByte b) => a.Equals(b);
    public static bool operator !=(CMYKA a, RGBAByte b) => a.Equals(b);

    public static explicit operator CMYKA(Float3 val) => new(val.x, val.y, val.z, 0);
    public static implicit operator CMYKA(Float4 val) => new(val.x, val.y, val.z, val.w);
    public static implicit operator CMYKA(HSVA val) => val.ToCMYKA();
    public static implicit operator CMYKA(RGBA val) => val.ToCMYKA();
    public static implicit operator CMYKA(CMYKAByte val) => val.ToCMYKA();
    public static implicit operator CMYKA(HSVAByte val) => val.ToCMYKA();
    public static implicit operator CMYKA(RGBAByte val) => val.ToCMYKA();
    public static implicit operator CMYKA(Fill<float> val) => new(val);
}
