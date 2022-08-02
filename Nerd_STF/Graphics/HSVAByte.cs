namespace Nerd_STF.Graphics;

public struct HSVAByte : IColorByte, IEquatable<HSVAByte>
{
    public static HSVA Black => new(Angle.Zero, 0, 0);
    public static HSVA Blue => new(new Angle(240), 255, 255);
    public static HSVA Clear => new(Angle.Zero, 0, 0, 0);
    public static HSVA Cyan => new(new Angle(180), 255, 255);
    public static HSVA Gray => new(Angle.Zero, 0, 127);
    public static HSVA Green => new(new Angle(120), 255, 255);
    public static HSVA Magenta => new(new Angle(300), 255, 255);
    public static HSVA Orange => new(new Angle(30), 255, 255);
    public static HSVA Purple => new(new Angle(270), 255, 255);
    public static HSVA Red => new(Angle.Zero, 255, 255);
    public static HSVA White => new(Angle.Zero, 0, 255);
    public static HSVA Yellow => new(new Angle(60), 255, 255);

    public byte H, S, V, A;

    public bool HasColor => S != 0 && V != 0;
    public bool IsOpaque => A == 255;
    public bool IsVisible => A == 0;

    public HSVAByte() : this(0, 0, 0, 255) { }
    public HSVAByte(int all) : this(all, all, all, all) { }
    public HSVAByte(int all, int a) : this(all, all, all, a) { }
    public HSVAByte(int h, int s, int v) : this(h, s, v, 255) { }
    public HSVAByte(int h, int s, int v, int a)
    {
        H = (byte)Mathf.Clamp(h, 0, 255);
        S = (byte)Mathf.Clamp(s, 0, 255);
        V = (byte)Mathf.Clamp(v, 0, 255);
        A = (byte)Mathf.Clamp(a, 0, 255);
    }
    public HSVAByte(Angle h, int s, int v) : this(h, s, v, 255) { }
    public HSVAByte(Angle h, int s, int v, int a) : this(Mathf.RoundInt(h.Normalized * 255), s, v, a) { }
    public HSVAByte(Fill<byte> fill) : this(fill(0), fill(1), fill(2), fill(3)) { }
    public HSVAByte(Fill<int> fill) : this(fill(0), fill(1), fill(2), fill(3)) { }

    public byte this[int index]
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
    public static HSVAByte Lerp(HSVAByte a, HSVAByte b, byte t, bool clamp = true) =>
        new(Mathf.Lerp(a.H, b.H, t, clamp), Mathf.Lerp(a.S, b.S, t, clamp), Mathf.Lerp(a.V, b.V, t, clamp),
            Mathf.Lerp(a.A, b.A, t, clamp));
    public static HSVAByte LerpSquared(HSVAByte a, HSVAByte b, byte t, Angle.Type type, bool clamp = true) =>
        HSVA.LerpSquared(a.ToHSVA(), b.ToHSVA(), t, type, clamp).ToHSVAByte();
    public static HSVAByte Median(params HSVAByte[] vals)
    {
        float index = Mathf.Average(0, vals.Length - 1);
        HSVAByte valA = vals[Mathf.Floor(index)], valB = vals[Mathf.Ceiling(index)];
        return Average(valA, valB);
    }
    public static HSVAByte Max(params HSVAByte[] vals)
    {
        (int[] Hs, int[] Ss, int[] Vs, int[] As) = SplitArrayInt(vals);
        return new(Mathf.Max(Hs), Mathf.Max(Ss), Mathf.Max(Vs), Mathf.Max(As));
    }
    public static HSVAByte Min(params HSVAByte[] vals)
    {
        (int[] Hs, int[] Ss, int[] Vs, int[] As) = SplitArrayInt(vals);
        return new(Mathf.Min(Hs), Mathf.Min(Ss), Mathf.Min(Vs), Mathf.Min(As));
    }

    public static (byte[] Hs, byte[] Ss, byte[] Vs, byte[] As) SplitArray(params HSVAByte[] vals)
    {
        byte[] Hs = new byte[vals.Length], Ss = new byte[vals.Length],
                Vs = new byte[vals.Length], As = new byte[vals.Length];
        for (int i = 0; i < vals.Length; i++)
        {
            Hs[i] = vals[i].H;
            Ss[i] = vals[i].S;
            Vs[i] = vals[i].V;
            As[i] = vals[i].A;
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
    public bool Equals(IColorByte? col) => col != null && Equals(col.ToHSVAByte());
    public bool Equals(HSVAByte col) => S == 0 && col.S == 0 || V == 0 && col.V == 0 || A == 0 && col.A == 0
        || H == col.H && S == col.S && V == col.V && A == col.A;
    public override bool Equals([NotNullWhen(true)] object? obj)
    {
        if (obj == null) return base.Equals(obj);
        Type t = obj.GetType();
        if (t == typeof(HSVAByte)) return Equals((HSVAByte)obj);
        else if (t == typeof(CMYKA)) return Equals((IColor)obj);
        else if (t == typeof(RGBA)) return Equals((IColor)obj);
        else if (t == typeof(IColor)) return Equals((IColor)obj);
        else if (t == typeof(RGBAByte)) return Equals((IColorByte)obj);
        else if (t == typeof(CMYKAByte)) return Equals((IColorByte)obj);
        else if (t == typeof(HSVA)) return Equals((IColor)obj);
        else if (t == typeof(IColorByte)) return Equals((IColorByte)obj);

        return base.Equals(obj);
    }
    public override int GetHashCode() => H.GetHashCode() ^ S.GetHashCode() ^ V.GetHashCode() ^ A.GetHashCode();
    public string ToString(IFormatProvider provider) => "H: " + H.ToString(provider) + " S: " + S.ToString(provider)
        + " V: " + V.ToString(provider) + " A: " + A.ToString(provider);
    public string ToString(string? provider) => "H: " + H.ToString(provider) + " S: " + S.ToString(provider)
        + " V: " + V.ToString(provider) + " A: " + A.ToString(provider);
    public override string ToString() => ToString((string?)null);

    public RGBA ToRGBA() => ToHSVA().ToRGBA();
    public CMYKA ToCMYKA() => ToHSVA().ToCMYKA();
    public HSVA ToHSVA() => new(H / 255f, S / 255f, V / 255f, A / 255f);

    public RGBAByte ToRGBAByte() => ToHSVA().ToRGBAByte();
    public CMYKAByte ToCMYKAByte() => ToHSVA().ToCMYKAByte();
    public HSVAByte ToHSVAByte() => this;

    public byte[] ToArray() => new[] { H, S, V, A };
    public Fill<byte> ToFill()
    {
        HSVAByte @this = this;
        return i => @this[i];
    }
    public List<byte> ToList() => new() { H, S, V, A };

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    public IEnumerator<byte> GetEnumerator()
    {
        yield return H;
        yield return S;
        yield return V;
        yield return A;
    }

    public object Clone() => new HSVAByte(H, S, V, A);

    public static HSVAByte operator +(HSVAByte a, HSVAByte b) => new(a.H + b.H, a.S + b.S, a.V + b.V, a.A + b.A);
    public static HSVAByte operator -(HSVAByte c) => new(255 - c.H, 255 - c.S, 255 - c.V, c.A != 255 ? 255 - c.A : 255);
    public static HSVAByte operator -(HSVAByte a, HSVAByte b) => new(a.H - b.H, a.S - b.S, a.V - b.V, a.A - b.A);
    public static HSVAByte operator *(HSVAByte a, HSVAByte b) => new(a.H * b.H, a.S * b.S, a.V * b.V, a.A * b.A);
    public static HSVAByte operator *(HSVAByte a, int b) => new(a.H * b, a.S * b, a.V * b, a.A * b);
    public static HSVAByte operator *(HSVAByte a, float b) => (a.ToHSVA() * b).ToHSVAByte();
    public static HSVAByte operator /(HSVAByte a, HSVAByte b) => new(a.H / b.H, a.S / b.S, a.V / b.V, a.A / b.A);
    public static HSVAByte operator /(HSVAByte a, int b) => new(a.H / b, a.S / b, a.V / b, a.A / b);
    public static HSVAByte operator /(HSVAByte a, float b) => (a.ToHSVA() * b).ToHSVAByte();
    public static bool operator ==(HSVAByte a, RGBA b) => a.Equals((IColor?)b);
    public static bool operator !=(HSVAByte a, RGBA b) => !a.Equals((IColor?)b);
    public static bool operator ==(HSVAByte a, CMYKA b) => a.Equals((IColor?)b);
    public static bool operator !=(HSVAByte a, CMYKA b) => !a.Equals((IColor?)b);
    public static bool operator ==(HSVAByte a, HSVA b) => a.Equals((IColor?)b);
    public static bool operator !=(HSVAByte a, HSVA b) => !a.Equals((IColor?)b);
    public static bool operator ==(HSVAByte a, RGBAByte b) => a.Equals(b);
    public static bool operator !=(HSVAByte a, RGBAByte b) => !a.Equals(b);
    public static bool operator ==(HSVAByte a, CMYKAByte b) => a.Equals(b);
    public static bool operator !=(HSVAByte a, CMYKAByte b) => !a.Equals(b);
    public static bool operator ==(HSVAByte a, HSVAByte b) => a.Equals(b);
    public static bool operator !=(HSVAByte a, HSVAByte b) => !a.Equals(b);

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
