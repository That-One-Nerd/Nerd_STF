namespace Nerd_STF.Graphics;

public record struct CMYKAByte : IAverage<CMYKAByte>, IClamp<CMYKAByte>, IColorByte<CMYKAByte>,
    IColorPresets<CMYKAByte>, IEquatable<CMYKAByte>, IIndexAll<int>, IIndexRangeAll<int>,
    ILerp<CMYKAByte, float>, IMedian<CMYKAByte>,
    ISplittable<CMYKAByte, (byte[] Cs, byte[] Ms, byte[] Ys, byte[] Ks, byte[] As)>
{
    public static CMYKAByte Black => new(0, 0, 0, 255);
    public static CMYKAByte Blue => new(255, 255, 0, 0);
    public static CMYKAByte Clear => new(0, 0, 0, 0, 0);
    public static CMYKAByte Cyan => new(255, 0, 0, 0);
    public static CMYKAByte Gray => new(0, 0, 0, 127);
    public static CMYKAByte Green => new(255, 0, 255, 0);
    public static CMYKAByte Magenta => new(0, 255, 0, 0);
    public static CMYKAByte Orange => new(0, 127, 255, 0);
    public static CMYKAByte Purple => new(127, 255, 0, 0);
    public static CMYKAByte Red => new(0, 255, 255, 0);
    public static CMYKAByte White => new(0, 0, 0, 0);
    public static CMYKAByte Yellow => new(0, 0, 255, 0);

    public int C
    {
        get => p_c;
        set => p_c = (byte)Mathf.Clamp(value, byte.MinValue, byte.MaxValue);
    }
    public int M
    {
        get => p_m;
        set => p_m = (byte)Mathf.Clamp(value, byte.MinValue, byte.MaxValue);
    }
    public int Y
    {
        get => p_y;
        set => p_y = (byte)Mathf.Clamp(value, byte.MinValue, byte.MaxValue);
    }
    public int K
    {
        get => p_k;
        set => p_k = (byte)Mathf.Clamp(value, byte.MinValue, byte.MaxValue);
    }
    public int A
    {
        get => p_a;
        set => p_a = (byte)Mathf.Clamp(value, byte.MinValue, byte.MaxValue);
    }

    private byte p_c, p_m, p_y, p_k, p_a;

    public bool HasCyan => C > 0;
    public bool HasMagenta => M > 0;
    public bool HasYellow => Y > 0;
    public bool HasBlack => K > 0;
    public bool IsOpaque => A == 255;
    public bool IsVisible => A != 0;

    public CMYKAByte() : this(0, 0, 0, 0, 255) { }
    public CMYKAByte(int all) : this(all, all, all, all, all) { }
    public CMYKAByte(int all, int a) : this(all, all, all, all, a) { }
    public CMYKAByte(int c, int m, int y, int k) : this(c, m, y, k, 255) { }
    public CMYKAByte(int c, int m, int y, int k, int a)
    {
        p_c = (byte)Mathf.Clamp(c, 0, 255);
        p_m = (byte)Mathf.Clamp(m, 0, 255);
        p_y = (byte)Mathf.Clamp(y, 0, 255);
        p_k = (byte)Mathf.Clamp(k, 0, 255);
        p_a = (byte)Mathf.Clamp(a, 0, 255);
    }
    public CMYKAByte(Fill<byte> fill) : this(fill(0), fill(1), fill(2), fill(3), fill(4)) { }
    public CMYKAByte(Fill<int> fill) : this(fill(0), fill(1), fill(2), fill(3), fill(4)) { }

    public int this[int index]
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
    public int this[Index index]
    {
        get => this[index.IsFromEnd ? 5 - index.Value : index.Value];
        set => this[index.IsFromEnd ? 5 - index.Value : index.Value] = value;
    }
    public int[] this[Range range]
    {
        get
        {
            int start = range.Start.IsFromEnd ? 5 - range.Start.Value : range.Start.Value;
            int end = range.End.IsFromEnd ? 5 - range.End.Value : range.End.Value;
            List<int> res = new();
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

    public static CMYKAByte Average(params CMYKAByte[] vals)
    {
        CMYKAByte val = new(0, 0, 0, 0, 0);
        for (int i = 0; i < vals.Length; i++) val += vals[i];
        return val / vals.Length;
    }
    public static CMYKAByte Clamp(CMYKAByte val, CMYKAByte min, CMYKAByte max) =>
        new(Mathf.Clamp(val.C, min.C, max.C),
            Mathf.Clamp(val.M, min.M, max.M),
            Mathf.Clamp(val.Y, min.Y, max.Y),
            Mathf.Clamp(val.K, min.K, max.K),
            Mathf.Clamp(val.A, min.A, max.A));
    public static CMYKAByte Lerp(CMYKAByte a, CMYKAByte b, float t, bool clamp = true) =>
        new(Mathf.Lerp(a.C, b.C, t, clamp), Mathf.Lerp(a.M, b.M, t, clamp), Mathf.Lerp(a.Y, b.Y, t, clamp),
            Mathf.Lerp(a.K, b.K, t, clamp), Mathf.Lerp(a.A, b.A, t, clamp));
    public static CMYKAByte LerpSquared(CMYKAByte a, CMYKAByte b, float t, bool clamp = true) => CMYKA.LerpSquared(a.ToCMYKA(), b.ToCMYKA(), t, clamp).ToCMYKAByte();
    public static CMYKAByte Median(params CMYKAByte[] vals)
    {
        float index = Mathf.Average(0, vals.Length - 1);
        CMYKAByte valA = vals[Mathf.Floor(index)], valB = vals[Mathf.Ceiling(index)];
        return Average(valA, valB);
    }

    public static (byte[] Cs, byte[] Ms, byte[] Ys, byte[] Ks, byte[] As) SplitArray(params CMYKAByte[] vals)
    {
        byte[] Cs = new byte[vals.Length], Ms = new byte[vals.Length],
               Ys = new byte[vals.Length], Ks = new byte[vals.Length],
               As = new byte[vals.Length];
        for (int i = 0; i < vals.Length; i++)
        {
            Cs[i] = vals[i].p_c;
            Ms[i] = vals[i].p_m;
            Ys[i] = vals[i].p_y;
            Ks[i] = vals[i].p_k;
            As[i] = vals[i].p_a;
        }
        return (Cs, Ms, Ys, Ks, As);
    }
    public static (int[] Cs, int[] Ms, int[] Ys, int[] Ks, int[] As) SplitArrayInt(params CMYKAByte[] vals)
    {
        int[] Cs = new int[vals.Length], Ms = new int[vals.Length],
              Ys = new int[vals.Length], Ks = new int[vals.Length],
              As = new int[vals.Length];
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

    public bool Equals(CMYKAByte col) => A == 0 && col.A == 0 || K == 1 && col.K == 255 || C == col.C && M == col.M
       && Y == col.Y && K == col.K && A == col.A;
    public bool Equals(IColor? col) => col != null && Equals(col.ToCMYKAByte());
    public override int GetHashCode() => base.GetHashCode();

    public RGBA ToRGBA() => ToCMYKA().ToRGBA();
    public CMYKA ToCMYKA() => new(C / 255f, M / 255f, Y / 255f, K / 255f, A / 255f);
    public HSVA ToHSVA() => ToCMYKA().ToHSVA();

    public RGBAByte ToRGBAByte() => ToCMYKA().ToRGBAByte();
    public CMYKAByte ToCMYKAByte() => this;
    public HSVAByte ToHSVAByte() => ToCMYKA().ToHSVAByte();

    public byte[] ToArray() => new[] { p_c, p_m, p_y, p_k, p_a };
    public int[] ToArrayInt() => new[] { C, M, Y, K, A };
    public Fill<byte> ToFill()
    {
        CMYKAByte @this = this;
        return i => (byte)@this[i];
    }
    public Fill<int> ToFillInt()
    {
        CMYKAByte @this = this;
        return i => @this[i];
    }
    public List<byte> ToList() => new() { p_c, p_m, p_y, p_k, p_a };
    public List<int> ToListInt() => new() { C, M, Y, K, A };

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    public IEnumerator<byte> GetEnumerator()
    {
        yield return p_c;
        yield return p_m;
        yield return p_y;
        yield return p_k;
        yield return p_a;
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

    public static CMYKAByte operator +(CMYKAByte a, CMYKAByte b) =>
        new(a.C + b.C, a.M + b.M, a.Y + b.Y, a.K + b.K, a.A + b.A);
    public static CMYKAByte operator -(CMYKAByte c) =>
        new(255 - c.C, 255 - c.M, 255 - c.Y, 255 - c.K, c.A != 255 ? 255 - c.A : 255);
    public static CMYKAByte operator -(CMYKAByte a, CMYKAByte b) =>
        new(a.C - b.C, a.M - b.M, a.Y - b.Y, a.K - b.K, a.A - b.A);
    public static CMYKAByte operator *(CMYKAByte a, CMYKAByte b) =>
        new(a.C * b.C, a.M * b.M, a.Y * b.Y, a.K * b.K, a.A * b.A);
    public static CMYKAByte operator *(CMYKAByte a, int b) =>
        new(a.C * b, a.M * b, a.Y * b, a.K * b, a.A * b);
    public static CMYKAByte operator *(CMYKAByte a, float b) => (a.ToCMYKA() * b).ToCMYKAByte();
    public static CMYKAByte operator /(CMYKAByte a, CMYKAByte b) =>
        new(a.C / b.C, a.M / b.M, a.Y / b.Y, a.K / b.K, a.A / b.A);
    public static CMYKAByte operator /(CMYKAByte a, int b) =>
        new(a.C / b, a.M / b, a.Y / b, a.K / b, a.A / b);
    public static CMYKAByte operator /(CMYKAByte a, float b) => (a.ToCMYKA() / b).ToCMYKAByte();
    public static bool operator ==(CMYKAByte a, CMYKA b) => a.Equals(b);
    public static bool operator !=(CMYKAByte a, CMYKA b) => a.Equals(b);
    public static bool operator ==(CMYKAByte a, HSVA b) => a.Equals(b);
    public static bool operator !=(CMYKAByte a, HSVA b) => a.Equals(b);
    public static bool operator ==(CMYKAByte a, RGBA b) => a.Equals(b);
    public static bool operator !=(CMYKAByte a, RGBA b) => a.Equals(b);
    public static bool operator ==(CMYKAByte a, HSVAByte b) => a.Equals(b);
    public static bool operator !=(CMYKAByte a, HSVAByte b) => a.Equals(b);
    public static bool operator ==(CMYKAByte a, RGBAByte b) => a.Equals(b);
    public static bool operator !=(CMYKAByte a, RGBAByte b) => a.Equals(b);

    public static explicit operator CMYKAByte(Int3 val) => new(val.x, val.y, val.z, 0);
    public static implicit operator CMYKAByte(Int4 val) => new(val.x, val.y, val.z, val.w);
    public static implicit operator CMYKAByte(HSVA val) => val.ToCMYKAByte();
    public static implicit operator CMYKAByte(RGBA val) => val.ToCMYKAByte();
    public static implicit operator CMYKAByte(CMYKA val) => val.ToCMYKAByte();
    public static implicit operator CMYKAByte(HSVAByte val) => val.ToCMYKAByte();
    public static implicit operator CMYKAByte(RGBAByte val) => val.ToCMYKAByte();
    public static implicit operator CMYKAByte(Fill<byte> val) => new(val);
    public static implicit operator CMYKAByte(Fill<int> val) => new(val);
}
