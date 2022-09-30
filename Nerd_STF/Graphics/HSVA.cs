namespace Nerd_STF.Graphics;

public struct HSVA : IColor, IEquatable<HSVA>
{
    public static HSVA Black => new(Angle.Zero, 0, 0);
    public static HSVA Blue => new(new Angle(240), 1, 1);
    public static HSVA Clear => new(Angle.Zero, 0, 0, 0);
    public static HSVA Cyan => new(new Angle(180), 1, 1);
    public static HSVA Gray => new(Angle.Zero, 0, 0.5f);
    public static HSVA Green => new(new Angle(120), 1, 1);
    public static HSVA Magenta => new(new Angle(300), 1, 1);
    public static HSVA Orange => new(new Angle(30), 1, 1);
    public static HSVA Purple => new(new Angle(270), 1, 1);
    public static HSVA Red => new(Angle.Zero, 1, 1);
    public static HSVA White => new(Angle.Zero, 0, 1);
    public static HSVA Yellow => new(new Angle(60), 1, 1);

    public Angle H
    {
        get => p_h;
        set => p_h = value.Bounded;
    }
    public float S
    {
        get => p_s;
        set => p_s = Mathf.Clamp(value, 0, 1);
    }
    public float V
    {
        get => p_v;
        set => p_v = Mathf.Clamp(value, 0, 1);
    }
    public float A
    {
        get => p_a;
        set => p_a = Mathf.Clamp(value, 0, 1);
    }

    private Angle p_h;
    private float p_s, p_v, p_a;

    public bool HasColor => p_s != 0 && p_v != 0;
    public bool IsOpaque => p_a == 1;
    public bool IsVisible => p_a != 0;

    public HSVA() : this(Angle.Zero, 0, 0, 1) { }
    public HSVA(Angle h, float s, float v) : this(h, s, v, 1) { }
    public HSVA(Angle h, float s, float v, float a)
    {
        p_h = h.Bounded;
        p_s = Mathf.Clamp(s, 0, 1);
        p_v = Mathf.Clamp(v, 0, 1);
        p_a = Mathf.Clamp(a, 0, 1);
    }
    public HSVA(Angle h, Fill<float> fill) : this(h, fill(0), fill(1), fill(2)) { }
    public HSVA(Angle h, Fill<int> fill) : this(h, fill(0), fill(1), fill(2)) { }
    public HSVA(float h, float s, float v) : this(new Angle(h, Angle.Type.Normalized), s, v) { }
    public HSVA(float h, float s, float v, float a) : this(new Angle(h, Angle.Type.Normalized), s, v, a) { }
    public HSVA(Fill<float> fill) : this(fill(0), fill(1), fill(2), fill(3)) { }
    public HSVA(Fill<int> fill) : this(fill(0), fill(1), fill(2), fill(3)) { }

    public float this[int index]
    {
        get => index switch
        {
            0 => H.Normalized,
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
                    H = new(Mathf.Clamp(value, 0, 1), Angle.Type.Normalized);
                    break;

                case 1:
                    S = Mathf.Clamp(value, 0, 1);
                    break;

                case 2:
                    V = Mathf.Clamp(value, 0, 1);
                    break;

                case 3:
                    A = Mathf.Clamp(value, 0, 1);
                    break;

                default: throw new IndexOutOfRangeException(nameof(index));
            }
        }
    }

    public static HSVA Average(params HSVA[] vals)
    {
        HSVA val = new(Angle.Zero, 0, 0, 0);
        for (int i = 0; i < vals.Length; i++) val += vals[i];
        return val / vals.Length;
    }
    public static HSVA Ceiling(HSVA val, Angle.Type type = Angle.Type.Degrees) => new(Angle.Ceiling(val.H, type),
        Mathf.Ceiling(val.S), Mathf.Ceiling(val.V), Mathf.Ceiling(val.A));
    public static HSVA Clamp(HSVA val, HSVA min, HSVA max) =>
        new(Angle.Clamp(val.H, min.H, max.H),
            Mathf.Clamp(val.S, min.S, max.S),
            Mathf.Clamp(val.V, min.V, max.V),
            Mathf.Clamp(val.A, min.A, max.A));
    public static HSVA Floor(HSVA val, Angle.Type type = Angle.Type.Degrees) => new(Angle.Floor(val.H, type),
        Mathf.Floor(val.S), Mathf.Floor(val.V), Mathf.Floor(val.A));
    public static HSVA Lerp(HSVA a, HSVA b, float t, bool clamp = true) =>
        new(Angle.Lerp(a.H, b.H, t, clamp), Mathf.Lerp(a.S, b.S, t, clamp), Mathf.Lerp(a.V, b.V, t, clamp),
            Mathf.Lerp(a.A, b.A, t, clamp));
    public static HSVA Median(params HSVA[] vals)
    {
        float index = Mathf.Average(0, vals.Length - 1);
        HSVA valA = vals[Mathf.Floor(index)], valB = vals[Mathf.Ceiling(index)];
        return Average(valA, valB);
    }
    public static HSVA Max(params HSVA[] vals)
    {
        (Angle[] Hs, float[] Ss, float[] Vs, float[] As) = SplitArray(vals);
        return new(Angle.Max(Hs), Mathf.Max(Ss), Mathf.Max(Vs), Mathf.Max(As));
    }
    public static HSVA Min(params HSVA[] vals)
    {
        (Angle[] Hs, float[] Ss, float[] Vs, float[] As) = SplitArray(vals);
        return new(Angle.Min(Hs), Mathf.Min(Ss), Mathf.Min(Vs), Mathf.Min(As));
    }
    public static HSVA Round(HSVA val, Angle.Type type = Angle.Type.Degrees) => new(Angle.Round(val.H, type),
        Mathf.Round(val.S), Mathf.Round(val.V), Mathf.Round(val.A));

    public static (Angle[] Hs, float[] Ss, float[] Vs, float[] As) SplitArray(params HSVA[] vals)
    {
        Angle[] Hs = new Angle[vals.Length];
        float[] Ss = new float[vals.Length], Vs = new float[vals.Length],
            As = new float[vals.Length];
        for (int i = 0; i < vals.Length; i++)
        {
            Hs[i] = vals[i].H;
            Ss[i] = vals[i].S;
            Vs[i] = vals[i].V;
            As[i] = vals[i].A;
        }
        return (Hs, Ss, Vs, As);
    }
    public static (float[] Hs, float[] Ss, float[] Vs, float[] As) SplitArrayNormalized(params HSVA[] vals)
    {
        float[] Hs = new float[vals.Length], Ss = new float[vals.Length],
            Vs = new float[vals.Length], As = new float[vals.Length];
        for (int i = 0; i < vals.Length; i++)
        {
            Hs[i] = vals[i].H.Normalized;
            Ss[i] = vals[i].S;
            Vs[i] = vals[i].V;
            As[i] = vals[i].A;
        }
        return (Hs, Ss, Vs, As);
    }

    public bool Equals(IColor? col) => col != null && Equals(col.ToHSVA());
    public bool Equals(IColorByte? col) => col != null && Equals(col.ToHSVA());
    public bool Equals(HSVA col) => S == 0 && col.S == 0 || V == 0 && col.V == 0 || A == 0 && col.A == 0
        || H == col.H && S == col.S && V == col.V && A == col.A;
    public override bool Equals([NotNullWhen(true)] object? obj)
    {
        if (obj == null) return base.Equals(obj);
        Type t = obj.GetType();
        if (t == typeof(HSVA)) return Equals((HSVA)obj);
        else if (t == typeof(CMYKA)) return Equals((IColor)obj);
        else if (t == typeof(RGBA)) return Equals((IColor)obj);
        else if (t == typeof(IColor)) return Equals((IColor)obj);
        else if (t == typeof(RGBAByte)) return Equals((IColorByte)obj);
        else if (t == typeof(CMYKAByte)) return Equals((IColorByte)obj);
        else if (t == typeof(HSVAByte)) return Equals((IColorByte)obj);
        else if (t == typeof(IColorByte)) return Equals((IColorByte)obj);

        return base.Equals(obj);
    }
    public override int GetHashCode() => H.GetHashCode() ^ S.GetHashCode() ^ V.GetHashCode() ^ A.GetHashCode();
    public string ToString(IFormatProvider provider) => "H: " + H.ToString(provider) + " S: " + S.ToString(provider)
        + " V: " + V.ToString(provider) + " A: " + A.ToString(provider);
    public string ToString(string? provider) => "H: " + H.ToString(provider) + " S: " + S.ToString(provider)
        + " V: " + V.ToString(provider) + " A: " + A.ToString(provider);
    public override string ToString() => ToString((string?)null);

    public RGBA ToRGBA()
    {
        float d = H.Degrees, c = V * S, x = c * (1 - Mathf.Absolute(d / 60 % 2 - 1)), m = V - c;
        (float r, float g, float b) vals = (0, 0, 0);
        if (d < 60) vals = (c, x, 0);
        else if (d < 120) vals = (x, c, 0);
        else if (d < 180) vals = (0, c, x);
        else if (d < 240) vals = (0, x, c);
        else if (d < 300) vals = (x, 0, c);
        else if (d < 360) vals = (c, 0, x);
        return new(vals.r + m, vals.g + m, vals.b + m);
    }
    public CMYKA ToCMYKA() => ToRGBA().ToCMYKA();
    public HSVA ToHSVA() => this;

    public RGBAByte ToRGBAByte() => ToRGBA().ToRGBAByte();
    public CMYKAByte ToCMYKAByte() => ToRGBA().ToCMYKAByte();
    public HSVAByte ToHSVAByte() => new(Mathf.RoundInt(H.Normalized * 255), Mathf.RoundInt(S * 255),
        Mathf.RoundInt(V * 255), Mathf.RoundInt(A * 255));

    public float[] ToArray() => new[] { H.Normalized, S, V, A };
    public Fill<float> ToFill()
    {
        HSVA @this = this;
        return i => @this[i];
    }
    public List<float> ToList() => new() { H.Normalized, S, V, A };

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    public IEnumerator<float> GetEnumerator()
    {
        yield return H.Normalized;
        yield return S;
        yield return V;
        yield return A;
    }

    public object Clone() => new HSVA(H, S, V, A);

    public static HSVA operator +(HSVA a, HSVA b) => new(a.H + b.H, a.S + b.S, a.V + b.V, a.A + b.A);
    public static HSVA operator -(HSVA c) => new(1 - c.H.Normalized, 1 - c.S, 1 - c.V, c.A != 1 ? 1 - c.A : 1);
    public static HSVA operator -(HSVA a, HSVA b) => new(a.H - b.H, a.S - b.S, a.V - b.V, a.A - b.A);
    public static HSVA operator *(HSVA a, float b) => new(a.H * b, a.S * b, a.V * b, a.A * b);
    public static HSVA operator /(HSVA a, float b) => new(a.H / b, a.S / b, a.V / b, a.A / b);
    public static bool operator ==(HSVA a, RGBA b) => a.Equals(b);
    public static bool operator !=(HSVA a, RGBA b) => !a.Equals(b);
    public static bool operator ==(HSVA a, CMYKA b) => a.Equals(b);
    public static bool operator !=(HSVA a, CMYKA b) => !a.Equals(b);
    public static bool operator ==(HSVA a, HSVA b) => a.Equals(b);
    public static bool operator !=(HSVA a, HSVA b) => !a.Equals(b);
    public static bool operator ==(HSVA a, RGBAByte b) => a.Equals((IColorByte?)b);
    public static bool operator !=(HSVA a, RGBAByte b) => !a.Equals((IColorByte?)b);
    public static bool operator ==(HSVA a, CMYKAByte b) => a.Equals((IColorByte?)b);
    public static bool operator !=(HSVA a, CMYKAByte b) => !a.Equals((IColorByte?)b);
    public static bool operator ==(HSVA a, HSVAByte b) => a.Equals((IColorByte?)b);
    public static bool operator !=(HSVA a, HSVAByte b) => !a.Equals((IColorByte?)b);

    public static explicit operator HSVA(Float3 val) => new(val.x, val.y, val.z);
    public static explicit operator HSVA(Float4 val) => new(val.x, val.y, val.z, val.w);
    public static implicit operator HSVA(CMYKA val) => val.ToHSVA();
    public static implicit operator HSVA(RGBA val) => val.ToHSVA();
    public static implicit operator HSVA(RGBAByte val) => val.ToHSVA();
    public static implicit operator HSVA(CMYKAByte val) => val.ToHSVA();
    public static implicit operator HSVA(HSVAByte val) => val.ToHSVA();
    public static implicit operator HSVA(Fill<float> val) => new(val);
}
