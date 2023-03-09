namespace Nerd_STF.Graphics;

public record struct HSVAByte : IAverage<HSVAByte>, IClamp<HSVAByte>, IColorByte<HSVAByte>, IColorPresets<HSVAByte>,
    IEquatable<HSVAByte>, IIndexAll<int>, IIndexRangeAll<int>, ILerp<HSVAByte, float>, IMedian<HSVAByte>,
    ISplittable<HSVAByte, (byte[] Hs, byte[] Ss, byte[] Vs, byte[] As)>
{
    public static HSVAByte Black => new(Angle.Zero, 0, 0);
    public static HSVAByte Blue => new(new Angle(240), 255, 255);
    public static HSVAByte Clear => new(Angle.Zero, 0, 0, 0);
    public static HSVAByte Cyan => new(new Angle(180), 255, 255);
    public static HSVAByte Gray => new(Angle.Zero, 0, 127);
    public static HSVAByte Green => new(new Angle(120), 255, 255);
    public static HSVAByte Magenta => new(new Angle(300), 255, 255);
    public static HSVAByte Orange => new(new Angle(30), 255, 255);
    public static HSVAByte Purple => new(new Angle(270), 255, 255);
    public static HSVAByte Red => new(Angle.Zero, 255, 255);
    public static HSVAByte White => new(Angle.Zero, 0, 255);
    public static HSVAByte Yellow => new(new Angle(60), 255, 255);

    public int H
    {
        get => p_h;
        set => p_h = (byte)Mathf.Clamp(value, byte.MinValue, byte.MaxValue);
    }
    public int S
    {
        get => p_s;
        set => p_s = (byte)Mathf.Clamp(value, byte.MinValue, byte.MaxValue);
    }
    public int V
    {
        get => p_v;
        set => p_v = (byte)Mathf.Clamp(value, byte.MinValue, byte.MaxValue);
    }
    public int A
    {
        get => p_a;
        set => p_a = (byte)Mathf.Clamp(value, byte.MinValue, byte.MaxValue);
    }

    private byte p_h, p_s, p_v, p_a;

    public bool HasColor => S != 0 && V != 0;
    public bool IsOpaque => A == 255;
    public bool IsVisible => A == 0;

    public HSVAByte() : this(0, 0, 0, 255) { }
    public HSVAByte(int all) : this(all, all, all, all) { }
    public HSVAByte(int all, int a) : this(all, all, all, a) { }
    public HSVAByte(int h, int s, int v) : this(h, s, v, 255) { }
    public HSVAByte(int h, int s, int v, int a)
    {
        p_h = (byte)Mathf.Clamp(h, 0, 255);
        p_s = (byte)Mathf.Clamp(s, 0, 255);
        p_v = (byte)Mathf.Clamp(v, 0, 255);
        p_a = (byte)Mathf.Clamp(a, 0, 255);
    }
    public HSVAByte(Angle h, int s, int v) : this(h, s, v, 255) { }
    public HSVAByte(Angle h, int s, int v, int a) : this(Mathf.RoundInt(h.Normalized * 255), s, v, a) { }
    public HSVAByte(Fill<byte> fill) : this(fill(0), fill(1), fill(2), fill(3)) { }
    public HSVAByte(Fill<int> fill) : this(fill(0), fill(1), fill(2), fill(3)) { }

    public int this[int index]
    {
        get => index switch
        {
            0 => H,
            1 => S,
            2 => V,
            3 => A,
            _ => throw new IndexOutOfRangeException(nameof(index)),
        };
        set
        {
            switch (index)
            {
                case 0:
                    H = value;
                    break;

                case 1:
                    S = value;
                    break;

                case 2:
                    V = value;
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

    public static HSVAByte Average(params HSVAByte[] vals)
    {
        HSVAByte val = new(0, 0, 0, 0);
        for (int i = 0; i < vals.Length; i++) val += vals[i];
        return val / vals.Length;
    }
    public static HSVAByte Clamp(HSVAByte val, HSVAByte min, HSVAByte max) =>
        new(Mathf.Clamp(val.H, min.H, max.H),
            Mathf.Clamp(val.S, min.S, max.S),
            Mathf.Clamp(val.V, min.V, max.V),
            Mathf.Clamp(val.A, min.A, max.A));
    public static HSVAByte Lerp(HSVAByte a, HSVAByte b, float t, bool clamp = true) =>
        new(Mathf.Lerp(a.H, b.H, t, clamp), Mathf.Lerp(a.S, b.S, t, clamp), Mathf.Lerp(a.V, b.V, t, clamp),
            Mathf.Lerp(a.A, b.A, t, clamp));
    public static HSVAByte Median(params HSVAByte[] vals)
    {
        float index = Mathf.Average(0, vals.Length - 1);
        HSVAByte valA = vals[Mathf.Floor(index)], valB = vals[Mathf.Ceiling(index)];
        return Average(valA, valB);
    }

    public static (byte[] Hs, byte[] Ss, byte[] Vs, byte[] As) SplitArray(params HSVAByte[] vals)
    {
        byte[] Hs = new byte[vals.Length], Ss = new byte[vals.Length],
                Vs = new byte[vals.Length], As = new byte[vals.Length];
        for (int i = 0; i < vals.Length; i++)
        {
            Hs[i] = vals[i].p_h;
            Ss[i] = vals[i].p_s;
            Vs[i] = vals[i].p_v;
            As[i] = vals[i].p_a;
        }
        return (Hs, Ss, Vs, As);
    }
    public static (int[] Hs, int[] Ss, int[] Vs, int[] As) SplitArrayInt(params HSVAByte[] vals)
    {
        int[] Hs = new int[vals.Length], Ss = new int[vals.Length],
              Vs = new int[vals.Length], As = new int[vals.Length];
        for (int i = 0; i < vals.Length; i++)
        {
            Hs[i] = vals[i].H;
            Ss[i] = vals[i].S;
            Vs[i] = vals[i].V;
            As[i] = vals[i].A;
        }
        return (Hs, Ss, Vs, As);
    }

    public bool Equals(IColor? col) => col != null && Equals(col.ToHSVAByte());
    public bool Equals(HSVAByte col) => S == 0 && col.S == 0 || V == 0 && col.V == 0 || A == 0 && col.A == 0
        || H == col.H && S == col.S && V == col.V && A == col.A;
    public override int GetHashCode() => base.GetHashCode();

    public RGBA ToRGBA() => ToHSVA().ToRGBA();
    public CMYKA ToCMYKA() => ToHSVA().ToCMYKA();
    public HSVA ToHSVA() => new(H / 255f, S / 255f, V / 255f, A / 255f);

    public RGBAByte ToRGBAByte() => ToHSVA().ToRGBAByte();
    public CMYKAByte ToCMYKAByte() => ToHSVA().ToCMYKAByte();
    public HSVAByte ToHSVAByte() => this;

    public byte[] ToArray() => new[] { p_h, p_s, p_v, p_a };
    public int[] ToArrayInt() => new[] { H, S, V, A };
    public Fill<byte> ToFill()
    {
        HSVAByte @this = this;
        return i => (byte)@this[i];
    }
    public Fill<int> ToFillInt()
    {
        HSVAByte @this = this;
        return i => @this[i];
    }
    public List<byte> ToList() => new() { p_h, p_s, p_v, p_a };
    public List<int> ToListInt() => new() { H, S, V, A };

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    public IEnumerator<byte> GetEnumerator()
    {
        yield return p_h;
        yield return p_s;
        yield return p_v;
        yield return p_a;
    }

    private bool PrintMembers(StringBuilder builder)
    {
        builder.Append("H = ");
        builder.Append(H);
        builder.Append(", S = ");
        builder.Append(S);
        builder.Append(", V = ");
        builder.Append(V);
        builder.Append(", A = ");
        builder.Append(A);
        return true;
    }

    public static HSVAByte operator +(HSVAByte a, HSVAByte b) => new(a.H + b.H, a.S + b.S, a.V + b.V, a.A + b.A);
    public static HSVAByte operator -(HSVAByte c) => new(255 - c.H, 255 - c.S, 255 - c.V, c.A != 255 ? 255 - c.A : 255);
    public static HSVAByte operator -(HSVAByte a, HSVAByte b) => new(a.H - b.H, a.S - b.S, a.V - b.V, a.A - b.A);
    public static HSVAByte operator *(HSVAByte a, HSVAByte b) => new(a.H * b.H, a.S * b.S, a.V * b.V, a.A * b.A);
    public static HSVAByte operator *(HSVAByte a, int b) => new(a.H * b, a.S * b, a.V * b, a.A * b);
    public static HSVAByte operator *(HSVAByte a, float b) => (a.ToHSVA() * b).ToHSVAByte();
    public static HSVAByte operator /(HSVAByte a, HSVAByte b) => new(a.H / b.H, a.S / b.S, a.V / b.V, a.A / b.A);
    public static HSVAByte operator /(HSVAByte a, int b) => new(a.H / b, a.S / b, a.V / b, a.A / b);
    public static HSVAByte operator /(HSVAByte a, float b) => (a.ToHSVA() * b).ToHSVAByte();
    public static bool operator ==(HSVAByte a, CMYKA b) => a.Equals(b);
    public static bool operator !=(HSVAByte a, CMYKA b) => a.Equals(b);
    public static bool operator ==(HSVAByte a, HSVA b) => a.Equals(b);
    public static bool operator !=(HSVAByte a, HSVA b) => a.Equals(b);
    public static bool operator ==(HSVAByte a, RGBA b) => a.Equals(b);
    public static bool operator !=(HSVAByte a, RGBA b) => a.Equals(b);
    public static bool operator ==(HSVAByte a, CMYKAByte b) => a.Equals(b);
    public static bool operator !=(HSVAByte a, CMYKAByte b) => a.Equals(b);
    public static bool operator ==(HSVAByte a, RGBAByte b) => a.Equals(b);
    public static bool operator !=(HSVAByte a, RGBAByte b) => a.Equals(b);

    public static implicit operator HSVAByte(Int3 val) => new(val.x, val.y, val.z);
    public static implicit operator HSVAByte(Int4 val) => new(val.x, val.y, val.z, val.w);
    public static implicit operator HSVAByte(CMYKA val) => val.ToHSVAByte();
    public static implicit operator HSVAByte(HSVA val) => val.ToHSVAByte();
    public static implicit operator HSVAByte(RGBA val) => val.ToHSVAByte();
    public static implicit operator HSVAByte(CMYKAByte val) => val.ToHSVAByte();
    public static implicit operator HSVAByte(RGBAByte val) => val.ToHSVAByte();
    public static implicit operator HSVAByte(Fill<byte> val) => new(val);
    public static implicit operator HSVAByte(Fill<int> val) => new(val);
}
