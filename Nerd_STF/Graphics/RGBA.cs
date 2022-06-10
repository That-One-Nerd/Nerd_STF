namespace Nerd_STF.Graphics;

public struct RGBA : IColor, IEquatable<RGBA>
{
    public static RGBA Black => new(0, 0, 0);
    public static RGBA Blue => new(0, 0, 1);
    public static RGBA Clear => new(0, 0, 0, 0);
    public static RGBA Cyan => new(0, 1, 1);
    public static RGBA Gray => new(0.5f, 0.5f, 0.5f);
    public static RGBA Green => new(0, 1, 0);
    public static RGBA Magenta => new(1, 0, 1);
    public static RGBA Orange => new(1, 0.5f, 0);
    public static RGBA Purple => new(0.5f, 0, 1);
    public static RGBA Red => new(1, 0, 0);
    public static RGBA White => new(1, 1, 1);
    public static RGBA Yellow => new(1, 1, 0);

    public float R
    {
        get => p_r;
        set => p_r = Mathf.Clamp(value, 0, 1);
    }
    public float G
    {
        get => p_g;
        set => p_g = Mathf.Clamp(value, 0, 1);
    }
    public float B
    {
        get => p_b;
        set => p_b = Mathf.Clamp(value, 0, 1);
    }
    public float A
    {
        get => p_a;
        set => p_a = Mathf.Clamp(value, 0, 1);
    }

    public bool HasBlue => p_b > 0;
    public bool HasGreen => p_g > 0;
    public bool HasRed => p_r > 0;
    public bool IsOpaque => p_a == 1;
    public bool IsVisible => p_a != 0;

    private float p_r, p_g, p_b, p_a;

    public RGBA() : this(0, 0, 0, 1) { }
    public RGBA(float all) : this(all, all, all, all) { }
    public RGBA(float all, float a) : this(all, all, all, a) { }
    public RGBA(float r, float g, float b) : this(r, g, b, 1) { }
    public RGBA(float r, float g, float b, float a)
    {
        p_r = Mathf.Clamp(r, 0, 1);
        p_g = Mathf.Clamp(g, 0, 1);
        p_b = Mathf.Clamp(b, 0, 1);
        p_a = Mathf.Clamp(a, 0, 1);
    }
    public RGBA(Fill<float> fill) : this(fill(0), fill(1), fill(2), fill(3)) { }

    public float this[int index]
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

    public static RGBA Average(params RGBA[] vals)
    {
        RGBA val = new(0, 0, 0, 0);
        for (int i = 0; i < vals.Length; i++) val += vals[i];
        return val / vals.Length;
    }
    public static RGBA Ceiling(RGBA val) =>
        new(Mathf.Ceiling(val.R), Mathf.Ceiling(val.G), Mathf.Ceiling(val.B), Mathf.Ceiling(val.A));
    public static RGBA Clamp(RGBA val, RGBA min, RGBA max) =>
        new(Mathf.Clamp(val.R, min.R, max.R),
            Mathf.Clamp(val.G, min.G, max.G),
            Mathf.Clamp(val.B, min.B, max.B),
            Mathf.Clamp(val.A, min.A, max.A));
    public static RGBA Floor(RGBA val) =>
        new(Mathf.Floor(val.R), Mathf.Floor(val.G), Mathf.Floor(val.B), Mathf.Floor(val.A));
    public static RGBA Lerp(RGBA a, RGBA b, float t, bool clamp = true) =>
        new(Mathf.Lerp(a.R, b.R, t, clamp), Mathf.Lerp(a.G, b.G, t, clamp), Mathf.Lerp(a.B, b.B, t, clamp),
            Mathf.Lerp(a.A, b.A, t, clamp));
    public static RGBA LerpSquared(RGBA a, RGBA b, float t, bool clamp = true)
    {
        RGBA val = Lerp(a * a, b * b, t, clamp);
        float R = Mathf.Sqrt(val.R), G = Mathf.Sqrt(val.G), B = Mathf.Sqrt(val.B), A = Mathf.Sqrt(val.A);
        return new(R, G, B, A);
    }
    public static RGBA Median(params RGBA[] vals)
    {
        float index = Mathf.Average(0, vals.Length - 1);
        RGBA valA = vals[Mathf.Floor(index)], valB = vals[Mathf.Ceiling(index)];
        return Average(valA, valB);
    }
    public static RGBA Max(params RGBA[] vals)
    {
        (float[] Rs, float[] Gs, float[] Bs, float[] As) = SplitArray(vals);
        return new(Mathf.Max(Rs), Mathf.Max(Gs), Mathf.Max(Bs), Mathf.Max(As));
    }
    public static RGBA Min(params RGBA[] vals)
    {
        (float[] Rs, float[] Gs, float[] Bs, float[] As) = SplitArray(vals);
        return new(Mathf.Min(Rs), Mathf.Min(Gs), Mathf.Min(Bs), Mathf.Min(As));
    }

    public static (float[] Rs, float[] Gs, float[] Bs, float[] As) SplitArray(params RGBA[] vals)
    {
        float[] Rs = new float[vals.Length], Gs = new float[vals.Length],
                Bs = new float[vals.Length], As = new float[vals.Length];
        for (int i = 0; i < vals.Length; i++)
        {
            Rs[i] = vals[i].R;
            Gs[i] = vals[i].G;
            Bs[i] = vals[i].B;
            As[i] = vals[i].A;
        }
        return (Rs, Gs, Bs, As);
    }

    public bool Equals(IColor? col) => col != null && Equals(col.ToRGBA());
    public bool Equals(IColorByte? col) => col != null && Equals(col.ToRGBA());
    public bool Equals(RGBA col) => A == 0 && col.A == 0 || R == col.R && G == col.G && B == col.B && A == col.A;
    public override bool Equals([NotNullWhen(true)] object? obj)
    {
        if (obj == null) return false;
        Type t = obj.GetType();
        if (t == typeof(RGBA)) return Equals((RGBA)obj);
        else if (t == typeof(CMYKA)) return Equals((IColor)obj);
        else if (t == typeof(HSVA)) return Equals((IColor)obj);
        else if (t == typeof(IColor)) return Equals((IColor)obj);
        else if (t == typeof(RGBAByte)) return Equals((IColorByte)obj);
        else if (t == typeof(CMYKAByte)) return Equals((IColorByte)obj);
        else if (t == typeof(HSVAByte)) return Equals((IColorByte)obj);
        else if (t == typeof(IColorByte)) return Equals((IColorByte)obj);

        return false;
    }
    public override int GetHashCode() => R.GetHashCode() ^ G.GetHashCode() ^ B.GetHashCode() ^ A.GetHashCode();
    public string ToString(IFormatProvider provider) => "R: " + R.ToString(provider) + " G: " + G.ToString(provider) +
        " B: " + B.ToString(provider) + " A: " + A.ToString(provider);
    public string ToString(string? provider) => "R: " + R.ToString(provider) + " G: " + G.ToString(provider) +
        " B: " + B.ToString(provider) + " A: " + A.ToString(provider);
    public override string ToString() => ToString((string?)null);

    public RGBA ToRGBA() => this;
    public CMYKA ToCMYKA()
    {
        float v = Mathf.Max(R, G, B), k = 1 - v;
        if (v == 1) return new(0, 0, 0, 1, A);

        float kInv = 1 / v, c = 1 - R - k, m = 1 - G - k, y = 1 - B - k;
        return new(c * kInv, m * kInv, y * kInv, k, A);
    }
    public HSVA ToHSVA()
    {
        float cMax = Mathf.Max(R, G, B), cMin = Mathf.Min(R, G, B), delta = cMax - cMin;
        Angle hue = Angle.Zero;
        if (delta != 0)
        {
            float val = 0;
            if (cMax == R) val = (G - B) / delta % 6;
            else if (cMax == G) val = (B - R) / delta + 2;
            else if (cMax == B) val = (R - G) / delta + 4;
            hue = new(val * 60);
        }

        float sat = cMax == 0 ? 0 : delta / cMax;

        return new(hue, sat, cMax, A);
    }

    public RGBAByte ToRGBAByte() => new(Mathf.RoundInt(R * 255), Mathf.RoundInt(G * 255), Mathf.RoundInt(B * 255),
        Mathf.RoundInt(A * 255));
    public CMYKAByte ToCMYKAByte() => ToCMYKA().ToCMYKAByte();
    public HSVAByte ToHSVAByte() => ToHSVA().ToHSVAByte();

    public float[] ToArray() => new[] { R, G, B, A };
    public List<float> ToList() => new() { R, G, B, A };

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    public IEnumerator<float> GetEnumerator()
    {
        yield return R;
        yield return G;
        yield return B;
        yield return A;
    }

    public object Clone() => new RGBA(R, G, B, A);

    public static RGBA operator +(RGBA a, RGBA b) => new(a.R + b.R, a.G + b.G, a.B + b.B, a.A + b.A);
    public static RGBA operator -(RGBA c) => new(1 - c.R, 1 - c.G, 1 - c.B, c.A != 1 ? 1 - c.A : 1);
    public static RGBA operator -(RGBA a, RGBA b) => new(a.R - b.R, a.G - b.G, a.B - b.B, a.A - b.A);
    public static RGBA operator *(RGBA a, RGBA b) => new(a.R * b.R, a.G * b.G, a.B * b.B, a.A * b.A);
    public static RGBA operator *(RGBA a, float b) => new(a.R * b, a.G * b, a.B * b, a.A * b);
    public static RGBA operator /(RGBA a, RGBA b) => new(a.R / b.R, a.G / b.G, a.B / b.B, a.A / b.A);
    public static RGBA operator /(RGBA a, float b) => new(a.R / b, a.G / b, a.B / b, a.A / b);
    public static bool operator ==(RGBA a, RGBA b) => a.Equals(b);
    public static bool operator !=(RGBA a, RGBA b) => !a.Equals(b);
    public static bool operator ==(RGBA a, CMYKA b) => a.Equals(b);
    public static bool operator !=(RGBA a, CMYKA b) => !a.Equals(b);
    public static bool operator ==(RGBA a, HSVA b) => a.Equals(b);
    public static bool operator !=(RGBA a, HSVA b) => !a.Equals(b);
    public static bool operator ==(RGBA a, RGBAByte b) => a.Equals((IColorByte?)b);
    public static bool operator !=(RGBA a, RGBAByte b) => !a.Equals((IColorByte?)b);
    public static bool operator ==(RGBA a, CMYKAByte b) => a.Equals((IColorByte?)b);
    public static bool operator !=(RGBA a, CMYKAByte b) => !a.Equals((IColorByte?)b);
    public static bool operator ==(RGBA a, HSVAByte b) => a.Equals((IColorByte?)b);
    public static bool operator !=(RGBA a, HSVAByte b) => !a.Equals((IColorByte?)b);

    public static implicit operator RGBA(Float3 val) => new(val.x, val.y, val.z);
    public static implicit operator RGBA(Float4 val) => new(val.x, val.y, val.z, val.w);
    public static implicit operator RGBA(CMYKA val) => val.ToRGBA();
    public static implicit operator RGBA(HSVA val) => val.ToRGBA();
    public static implicit operator RGBA(RGBAByte val) => val.ToRGBA();
    public static implicit operator RGBA(CMYKAByte val) => val.ToRGBA();
    public static implicit operator RGBA(HSVAByte val) => val.ToRGBA();
    public static implicit operator RGBA(Fill<float> val) => new(val);
}
