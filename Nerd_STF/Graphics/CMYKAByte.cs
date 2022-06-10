namespace Nerd_STF.Graphics;

public struct CMYKAByte : IColorByte, IEquatable<CMYKAByte>
{
    public static CMYKA Black => new(0, 0, 0, 255);
    public static CMYKA Blue => new(255, 255, 0, 0);
    public static CMYKA Clear => new(0, 0, 0, 0, 0);
    public static CMYKA Cyan => new(255, 0, 0, 0);
    public static CMYKA Gray => new(0, 0, 0, 127);
    public static CMYKA Green => new(255, 0, 255, 0);
    public static CMYKA Magenta => new(0, 255, 0, 0);
    public static CMYKA Orange => new(0, 127, 255, 0);
    public static CMYKA Purple => new(127, 255, 0, 0);
    public static CMYKA Red => new(0, 255, 255, 0);
    public static CMYKA White => new(0, 0, 0, 0);
    public static CMYKA Yellow => new(0, 0, 255, 0);

    public byte C, M, Y, K, A;

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
        C = (byte)Mathf.Clamp(c, 0, 255);
        M = (byte)Mathf.Clamp(m, 0, 255);
        Y = (byte)Mathf.Clamp(y, 0, 255);
        K = (byte)Mathf.Clamp(k, 0, 255);
        A = (byte)Mathf.Clamp(a, 0, 255);
    }
    public CMYKAByte(Fill<byte> fill) : this(fill(0), fill(1), fill(2), fill(3), fill(4)) { }
    public CMYKAByte(Fill<int> fill) : this(fill(0), fill(1), fill(2), fill(3), fill(4)) { }

    public byte this[int index]
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
    public static CMYKAByte Max(params CMYKAByte[] vals)
    {
        (int[] Cs, int[] Ms, int[] Ys, int[] Ks, int[] As) = SplitArrayInt(vals);
        return new(Mathf.Max(Cs), Mathf.Max(Ms), Mathf.Max(Ys), Mathf.Max(Ks), Mathf.Max(As));
    }
    public static CMYKAByte Min(params CMYKAByte[] vals)
    {
        (int[] Cs, int[] Ms, int[] Ys, int[] Ks, int[] As) = SplitArrayInt(vals);
        return new(Mathf.Min(Cs), Mathf.Min(Ms), Mathf.Min(Ys), Mathf.Min(Ks), Mathf.Min(As));
    }

    public static (byte[] Cs, byte[] Ms, byte[] Ys, byte[] Ks, byte[] As) SplitArray(params CMYKAByte[] vals)
    {
        byte[] Cs = new byte[vals.Length], Ms = new byte[vals.Length],
               Ys = new byte[vals.Length], Ks = new byte[vals.Length],
               As = new byte[vals.Length];
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

    public bool Equals(IColor? col) => col != null && Equals(col.ToCMYKAByte());
    public bool Equals(IColorByte? col) => col != null && Equals(col.ToCMYKAByte());
    public bool Equals(CMYKAByte col) => A == 0 && col.A == 0 || K == 1 && col.K == 255 || C == col.C && M == col.M
       && Y == col.Y && K == col.K && A == col.A;
    public override bool Equals([NotNullWhen(true)] object? obj)
    {
        if (obj == null) return false;
        Type t = obj.GetType();
        if (t == typeof(CMYKAByte)) return Equals((CMYKAByte)obj);
        else if (t == typeof(RGBA)) return Equals((IColor)obj);
        else if (t == typeof(HSVA)) return Equals((IColor)obj);
        else if (t == typeof(IColor)) return Equals((IColor)obj);
        else if (t == typeof(RGBAByte)) return Equals((IColorByte)obj);
        else if (t == typeof(CMYKA)) return Equals((IColor)obj);
        else if (t == typeof(HSVAByte)) return Equals((IColorByte)obj);
        else if (t == typeof(IColorByte)) return Equals((IColorByte)obj);

        return false;
    }
    public override int GetHashCode() => C.GetHashCode() ^ M.GetHashCode() ^ Y.GetHashCode()
        ^ K.GetHashCode() ^ A.GetHashCode();
    public string ToString(IFormatProvider provider) => "C: " + C.ToString(provider)
        + " M: " + M.ToString(provider) + " Y: " + Y.ToString(provider)
        + " K: " + K.ToString(provider) + " A: " + A.ToString(provider);
    public string ToString(string? provider) => "C: " + C.ToString(provider)
        + " M: " + M.ToString(provider) + " Y: " + Y.ToString(provider)
        + " K: " + K.ToString(provider) + " A: " + A.ToString(provider);
    public override string ToString() => ToString((string?)null);

    public RGBA ToRGBA() => ToCMYKA().ToRGBA();
    public CMYKA ToCMYKA() => new(C / 255f, M / 255f, Y / 255f, K / 255f, A / 255f);
    public HSVA ToHSVA() => ToRGBA().ToHSVA();

    public RGBAByte ToRGBAByte() => ToCMYKA().ToRGBAByte();
    public CMYKAByte ToCMYKAByte() => this;
    public HSVAByte ToHSVAByte() => ToRGBA().ToHSVAByte();

    public byte[] ToArray() => new[] { C, M, Y, K, A };
    public List<byte> ToList() => new() { C, M, Y, K, A };

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    public IEnumerator<byte> GetEnumerator()
    {
        yield return C;
        yield return M;
        yield return Y;
        yield return K;
        yield return A;
    }

    public object Clone() => new CMYKAByte(C, M, Y, K, A);

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
    public static bool operator ==(CMYKAByte a, RGBA b) => a.Equals((IColor?)b);
    public static bool operator !=(CMYKAByte a, RGBA b) => !a.Equals((IColor?)b);
    public static bool operator ==(CMYKAByte a, CMYKA b) => a.Equals((IColor?)b);
    public static bool operator !=(CMYKAByte a, CMYKA b) => !a.Equals((IColor?)b);
    public static bool operator ==(CMYKAByte a, HSVA b) => a.Equals((IColor?)b);
    public static bool operator !=(CMYKAByte a, HSVA b) => !a.Equals((IColor?)b);
    public static bool operator ==(CMYKAByte a, RGBAByte b) => a.Equals(b);
    public static bool operator !=(CMYKAByte a, RGBAByte b) => !a.Equals(b);
    public static bool operator ==(CMYKAByte a, CMYKAByte b) => a.Equals(b);
    public static bool operator !=(CMYKAByte a, CMYKAByte b) => !a.Equals(b);
    public static bool operator ==(CMYKAByte a, HSVAByte b) => a.Equals(b);
    public static bool operator !=(CMYKAByte a, HSVAByte b) => !a.Equals(b);

    public static explicit operator CMYKAByte(Int3 val) => new(val.x, val.y, val.z, 0);
    public static implicit operator CMYKAByte(Int4 val) => new(val.x, val.y, val.z, val.w);
    public static implicit operator CMYKAByte(RGBA val) => val.ToCMYKAByte();
    public static implicit operator CMYKAByte(HSVA val) => val.ToCMYKAByte();
    public static implicit operator CMYKAByte(RGBAByte val) => val.ToCMYKAByte();
    public static implicit operator CMYKAByte(CMYKA val) => val.ToCMYKAByte();
    public static implicit operator CMYKAByte(HSVAByte val) => val.ToCMYKAByte();
    public static implicit operator CMYKAByte(Fill<byte> val) => new(val);
    public static implicit operator CMYKAByte(Fill<int> val) => new(val);
}
