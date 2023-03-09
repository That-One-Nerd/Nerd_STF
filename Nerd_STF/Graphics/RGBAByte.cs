namespace Nerd_STF.Graphics;

public record struct RGBAByte : IAverage<RGBAByte>, IClamp<RGBAByte>, IColorByte<RGBAByte>, IColorPresets<RGBAByte>,
    IEquatable<RGBAByte>, IIndexAll<int>, IIndexRangeAll<int>, ILerp<RGBAByte, float>, IMedian<RGBAByte>,
    ISplittable<RGBAByte, (byte[] Rs, byte[] Gs, byte[] Bs, byte[] As)>
{
    public static RGBAByte Black => new(0, 0, 0);
    public static RGBAByte Blue => new(0, 0, 255);
    public static RGBAByte Clear => new(0, 0, 0, 0);
    public static RGBAByte Cyan => new(0, 255, 255);
    public static RGBAByte Gray => new(127, 127, 127);
    public static RGBAByte Green => new(0, 255, 0);
    public static RGBAByte Magenta => new(255, 0, 255);
    public static RGBAByte Orange => new(255, 127, 0);
    public static RGBAByte Purple => new(127, 0, 255);
    public static RGBAByte Red => new(255, 0, 0);
    public static RGBAByte White => new(255, 255, 255);
    public static RGBAByte Yellow => new(255, 255, 0);

    public int R
    {
        get => p_r;
        set => p_r = (byte)Mathf.Clamp(value, byte.MinValue, byte.MaxValue);
    }
    public int G
    {
        get => p_g;
        set => p_g = (byte)Mathf.Clamp(value, byte.MinValue, byte.MaxValue);
    }
    public int B
    {
        get => p_b;
        set => p_b = (byte)Mathf.Clamp(value, byte.MinValue, byte.MaxValue);
    }
    public int A
    {
        get => p_a;
        set => p_a = (byte)Mathf.Clamp(value, byte.MinValue, byte.MaxValue);
    }

    private byte p_r, p_g, p_b, p_a;

    public bool HasBlue => B > 0;
    public bool HasGreen => G > 0;
    public bool HasRed => R > 0;
    public bool IsOpaque => A == 255;
    public bool IsVisible => A != 0;

    public RGBAByte() : this(0, 0, 0, 255) { }
    public RGBAByte(int all) : this(all, all, all, all) { }
    public RGBAByte(int all, int a) : this(all, all, all, a) { }
    public RGBAByte(int r, int g, int b) : this(r, g, b, 255) { }
    public RGBAByte(int r, int g, int b, int a)
    {
        R = (byte)Mathf.Clamp(r, 0, 255);
        G = (byte)Mathf.Clamp(g, 0, 255);
        B = (byte)Mathf.Clamp(b, 0, 255);
        A = (byte)Mathf.Clamp(a, 0, 255);
    }
    public RGBAByte(Fill<byte> fill) : this(fill(0), fill(1), fill(2), fill(3)) { }
    public RGBAByte(Fill<int> fill) : this(fill(0), fill(1), fill(2), fill(3)) { }

    public int this[int index]
    {
        get => index switch
        {
            0 => R,
            1 => G,
            2 => B,
            3 => A,
            _ => throw new IndexOutOfRangeException(nameof(index)),
        };
        set
        {
            switch (index)
            {
                case 0:
                    R = value;
                    break;

                case 1:
                    G = value;
                    break;

                case 2:
                    B = value;
                    break;

                case 3:
                    A = value;
                    break;

                default: throw new IndexOutOfRangeException(nameof(index));
            }
        }
    }
    public int this[Index index]
    {
        get => this[index.IsFromEnd ? 4 - index.Value : index.Value];
        set => this[index.IsFromEnd ? 4 - index.Value : index.Value] = value;
    }
    public int[] this[Range range]
    {
        get
        {
            int start = range.Start.IsFromEnd ? 4 - range.Start.Value : range.Start.Value;
            int end = range.End.IsFromEnd ? 4 - range.End.Value : range.End.Value;
            List<int> res = new();
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

    public static RGBAByte Average(params RGBAByte[] vals)
    {
        RGBAByte val = new(0, 0, 0, 0);
        for (int i = 0; i < vals.Length; i++) val += vals[i];
        return val / vals.Length;
    }
    public static RGBAByte Clamp(RGBAByte val, RGBAByte min, RGBAByte max) =>
        new(Mathf.Clamp(val.R, min.R, max.R),
            Mathf.Clamp(val.G, min.G, max.G),
            Mathf.Clamp(val.B, min.B, max.B),
            Mathf.Clamp(val.A, min.A, max.A));
    public static RGBAByte Lerp(RGBAByte a, RGBAByte b, float t, bool clamp = true) =>
        new(Mathf.Lerp(a.R, b.R, t, clamp), Mathf.Lerp(a.G, b.G, t, clamp), Mathf.Lerp(a.B, b.B, t, clamp),
            Mathf.Lerp(a.A, b.A, t, clamp));
    public static RGBAByte LerpSquared(RGBAByte a, RGBAByte b, byte t, bool clamp = true) =>
        RGBA.LerpSquared(a.ToRGBA(), b.ToRGBA(), t, clamp).ToRGBAByte();
    public static RGBAByte Median(params RGBAByte[] vals)
    {
        float index = Mathf.Average(0, vals.Length - 1);
        RGBAByte valA = vals[Mathf.Floor(index)], valB = vals[Mathf.Ceiling(index)];
        return Average(valA, valB);
    }
    public static RGBAByte Max(params RGBAByte[] vals)
    {
        (int[] Rs, int[] Gs, int[] Bs, int[] As) = SplitArrayInt(vals);
        return new(Mathf.Max(Rs), Mathf.Max(Gs), Mathf.Max(Bs), Mathf.Max(As));
    }
    public static RGBAByte Min(params RGBAByte[] vals)
    {
        (int[] Rs, int[] Gs, int[] Bs, int[] As) = SplitArrayInt(vals);
        return new(Mathf.Min(Rs), Mathf.Min(Gs), Mathf.Min(Bs), Mathf.Min(As));
    }

    public static (byte[] Rs, byte[] Gs, byte[] Bs, byte[] As) SplitArray(params RGBAByte[] vals)
    {
        byte[] Rs = new byte[vals.Length], Gs = new byte[vals.Length],
                Bs = new byte[vals.Length], As = new byte[vals.Length];
        for (int i = 0; i < vals.Length; i++)
        {
            Rs[i] = vals[i].p_r;
            Gs[i] = vals[i].p_g;
            Bs[i] = vals[i].p_b;
            As[i] = vals[i].p_a;
        }
        return (Rs, Gs, Bs, As);
    }
    public static (int[] Rs, int[] Gs, int[] Bs, int[] As) SplitArrayInt(params RGBAByte[] vals)
    {
        int[] Rs = new int[vals.Length], Gs = new int[vals.Length],
              Bs = new int[vals.Length], As = new int[vals.Length];
        for (int i = 0; i < vals.Length; i++)
        {
            Rs[i] = vals[i].R;
            Gs[i] = vals[i].G;
            Bs[i] = vals[i].B;
            As[i] = vals[i].A;
        }
        return (Rs, Gs, Bs, As);
    }

    public bool Equals(IColor? col) => col != null && Equals(col.ToRGBAByte());
    public bool Equals(RGBAByte col) => A == 0 && col.A == 0 || R == col.R && G == col.G && B == col.B && A == col.A;
    public override int GetHashCode() => base.GetHashCode();

    public RGBA ToRGBA() => new(R / 255f, G / 255f, B / 255f, A / 255f);
    public CMYKA ToCMYKA() => ToRGBA().ToCMYKA();
    public HSVA ToHSVA() => ToRGBA().ToHSVA();

    public RGBAByte ToRGBAByte() => this;
    public CMYKAByte ToCMYKAByte() => ToRGBA().ToCMYKAByte();
    public HSVAByte ToHSVAByte() => ToRGBA().ToHSVAByte();

    public byte[] ToArray() => new[] { p_r, p_g, p_b, p_a };
    public int[] ToArrayInt() => new[] { R, G, B, A };
    public Fill<byte> ToFill()
    {
        RGBAByte @this = this;
        return i => (byte)@this[i];
    }
    public Fill<int> ToFillInt()
    {
        RGBAByte @this = this;
        return i => @this[i];
    }
    public List<byte> ToList() => new() { p_r, p_g, p_b, p_a };
    public List<int> ToListInt() => new() { R, G, B, A };

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    public IEnumerator<byte> GetEnumerator()
    {
        yield return p_r;
        yield return p_g;
        yield return p_b;
        yield return p_a;
    }

    public Vector3d ToVector() => ((RGBA)this).ToVector();

    private bool PrintMembers(StringBuilder builder)
    {
        builder.Append("R = ");
        builder.Append(R);
        builder.Append(", G = ");
        builder.Append(G);
        builder.Append(", B = ");
        builder.Append(B);
        builder.Append(", A = ");
        builder.Append(A);
        return true;
    }

    public static RGBAByte operator +(RGBAByte a, RGBAByte b) => new(a.R + b.R, a.G + b.G, a.B + b.B, a.A + b.A);
    public static RGBAByte operator -(RGBAByte c) => new(255 - c.R, 255 - c.G, 255 - c.B, c.A != 255 ? 255 - c.A : 255);
    public static RGBAByte operator -(RGBAByte a, RGBAByte b) => new(a.R - b.R, a.G - b.G, a.B - b.B, a.A - b.A);
    public static RGBAByte operator *(RGBAByte a, RGBAByte b) => new(a.R * b.R, a.G * b.G, a.B * b.B, a.A * b.A);
    public static RGBAByte operator *(RGBAByte a, int b) => new(a.R * b, a.G * b, a.B * b, a.A * b);
    public static RGBAByte operator *(RGBAByte a, float b) => (a.ToRGBA() * b).ToRGBAByte();
    public static RGBAByte operator /(RGBAByte a, RGBAByte b) => new(a.R / b.R, a.G / b.G, a.B / b.B, a.A / b.A);
    public static RGBAByte operator /(RGBAByte a, int b) => new(a.R / b, a.G / b, a.B / b, a.A / b);
    public static RGBAByte operator /(RGBAByte a, float b) => (a.ToRGBA() / b).ToRGBAByte();
    public static bool operator ==(RGBAByte a, CMYKA b) => a.Equals(b);
    public static bool operator !=(RGBAByte a, CMYKA b) => a.Equals(b);
    public static bool operator ==(RGBAByte a, HSVA b) => a.Equals(b);
    public static bool operator !=(RGBAByte a, HSVA b) => a.Equals(b);
    public static bool operator ==(RGBAByte a, RGBA b) => a.Equals(b);
    public static bool operator !=(RGBAByte a, RGBA b) => a.Equals(b);
    public static bool operator ==(RGBAByte a, CMYKAByte b) => a.Equals(b);
    public static bool operator !=(RGBAByte a, CMYKAByte b) => a.Equals(b);
    public static bool operator ==(RGBAByte a, HSVAByte b) => a.Equals(b);
    public static bool operator !=(RGBAByte a, HSVAByte b) => a.Equals(b);

    public static implicit operator RGBAByte(Int3 val) => new(val.x, val.y, val.z);
    public static implicit operator RGBAByte(Int4 val) => new(val.x, val.y, val.z, val.w);
    public static implicit operator RGBAByte(CMYKA val) => val.ToRGBAByte();
    public static implicit operator RGBAByte(HSVA val) => val.ToRGBAByte();
    public static implicit operator RGBAByte(RGBA val) => val.ToRGBAByte();
    public static implicit operator RGBAByte(CMYKAByte val) => val.ToRGBAByte();
    public static implicit operator RGBAByte(HSVAByte val) => val.ToRGBAByte();
    public static implicit operator RGBAByte(Fill<byte> val) => new(val);
    public static implicit operator RGBAByte(Fill<int> val) => new(val);
}
