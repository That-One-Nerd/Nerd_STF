namespace Nerd_STF.Mathematics.Algebra;

public struct Matrix3x3 : IMatrix<Matrix3x3, Matrix2x2>
{
    public static Matrix3x3 Identity => new(new[,]
    {
        { 1, 0, 0 },
        { 0, 1, 0 },
        { 0, 0, 1 }
    });
    public static Matrix3x3 One => new(1);
    public static Matrix3x3 SignGrid => new(new[,]
    {
        { +1, -1, +1 },
        { -1, +1, -1 },
        { +1, -1, +1 }
    });
    public static Matrix3x3 Zero => new(0);

    public Float3 Column1
    {
        get => new(r1c1, r2c1, r3c1);
        set
        {
            r1c1 = value.x;
            r2c1 = value.y;
            r3c1 = value.z;
        }
    }
    public Float3 Column2
    {
        get => new(r1c2, r2c2, r3c2);
        set
        {
            r1c2 = value.x;
            r2c2 = value.y;
            r3c2 = value.z;
        }
    }
    public Float3 Column3
    {
        get => new(r1c3, r2c3, r3c3);
        set
        {
            r1c3 = value.x;
            r2c3 = value.y;
            r3c3 = value.z;
        }
    }
    public Float3 Row1
    {
        get => new(r1c1, r1c2, r1c3);
        set
        {
            r1c1 = value.x;
            r1c2 = value.y;
            r1c3 = value.z;
        }
    }
    public Float3 Row2
    {
        get => new(r2c1, r2c2, r2c3);
        set
        {
            r2c1 = value.x;
            r2c2 = value.y;
            r2c3 = value.z;
        }
    }
    public Float3 Row3
    {
        get => new(r3c1, r3c2, r3c3);
        set
        {
            r3c1 = value.x;
            r3c2 = value.y;
            r3c3 = value.z;
        }
    }

    public float r1c1, r2c1, r3c1, r1c2, r2c2, r3c2, r1c3, r2c3, r3c3;

    public Matrix3x3(float all) : this(all, all, all, all, all, all, all, all, all) { }
    public Matrix3x3(float r1c1, float r2c1, float r3c1, float r1c2,
        float r2c2, float r3c2, float r1c3, float r2c3, float r3c3)
    {
        this.r1c1 = r1c1;
        this.r2c1 = r2c1;
        this.r3c1 = r3c1;
        this.r1c2 = r1c2;
        this.r2c2 = r2c2;
        this.r3c2 = r3c2;
        this.r1c3 = r1c3;
        this.r2c3 = r2c3;
        this.r3c3 = r3c3;
    }
    public Matrix3x3(float[] nums) : this(nums[0], nums[1], nums[2],
        nums[3], nums[4], nums[5], nums[6], nums[7], nums[8]) { }
    public Matrix3x3(int[] nums) : this(nums[0], nums[1], nums[2],
        nums[3], nums[4], nums[5], nums[6], nums[7], nums[8]) { }
    public Matrix3x3(Fill<float> fill) : this(fill(0), fill(1), fill(2),
        fill(3), fill(4), fill(5), fill(6), fill(7), fill(8)) { }
    public Matrix3x3(Fill<int> fill) : this(fill(0), fill(1), fill(2),
        fill(3), fill(4), fill(5), fill(6), fill(7), fill(8)) { }
    public Matrix3x3(float[,] nums) : this(nums[0, 0], nums[0, 1], nums[0, 2],
        nums[1, 0], nums[1, 1], nums[1, 2], nums[2, 0], nums[2, 1], nums[2, 2]) { }
    public Matrix3x3(int[,] nums) : this(nums[0, 0], nums[0, 1], nums[0, 2],
        nums[1, 0], nums[1, 1], nums[1, 2], nums[2, 0], nums[2, 1], nums[2, 2]) { }
    public Matrix3x3(Fill2D<float> fill) : this(fill(0, 0), fill(0, 1), fill(0, 2),
        fill(1, 0), fill(1, 1), fill(1, 2), fill(2, 0), fill(2, 1), fill(2, 2)) { }
    public Matrix3x3(Fill2D<int> fill) : this(fill(0, 0), fill(0, 1), fill(0, 2),
        fill(1, 0), fill(1, 1), fill(1, 2), fill(2, 0), fill(2, 1), fill(2, 2)) { }
    public Matrix3x3(Float3 c1, Float3 c2, Float3 c3) : this(c1.x, c1.y, c1.z, c2.x, c2.y, c2.z, c3.x, c3.y, c3.z) { }
    public Matrix3x3(Fill<Float3> fill) : this(fill(0), fill(1), fill(2)) { }
    public Matrix3x3(Fill<Int3> fill) : this((IEnumerable<int>)fill(0), fill(1), fill(2)) { }
    public Matrix3x3(IEnumerable<float> c1, IEnumerable<float> c2, IEnumerable<float> c3)
        : this(c1.ToFill(), c2.ToFill(), c3.ToFill()) { }
    public Matrix3x3(IEnumerable<int> c1, IEnumerable<int> c2, IEnumerable<int> c3)
        : this(c1.ToFill(), c2.ToFill(), c3.ToFill()) { }
    public Matrix3x3(Fill<float> c1, Fill<float> c2, Fill<float> c3)
        : this(c1(0), c1(1), c1(2), c2(0), c2(1), c2(2), c3(0), c3(1), c3(2)) { }
    public Matrix3x3(Fill<int> c1, Fill<int> c2, Fill<int> c3)
        : this(c1(0), c1(1), c1(2), c2(0), c2(1), c2(2), c3(0), c3(1), c3(2)) { }

    public float this[int r, int c]
    {
        get => ToArray2D()[c, r];
        set
        {
            // Maybe this could be improved?
            // It's definitely better than it was before. Trust me.
            switch ("r" + (r + 1) + "c" + (c + 1))
            {
                case "r1c1":
                    r1c1 = value;
                    break;

                case "r2c1":
                    r2c1 = value;
                    break;

                case "r3c1":
                    r3c1 = value;
                    break;

                case "r1c2":
                    r1c2 = value;
                    break;

                case "r2c2":
                    r2c2 = value;
                    break;

                case "r3c2":
                    r3c2 = value;
                    break;

                case "r1c3":
                    r1c3 = value;
                    break;

                case "r2c3":
                    r2c3 = value;
                    break;

                case "r3c3":
                    r3c3 = value;
                    break;

                default:
                    string @params = "";
                    if (r < 0 || r > 2) @params += r;
                    if (c < 0 || c > 2) @params += string.IsNullOrEmpty(@params) ? c : " and " + c;
                    throw new IndexOutOfRangeException(@params);
            }
        }
    }
    public float this[Int2 index]
    {
        get => this[index.x, index.y];
        set => this[index.x, index.y] = value;
    }

    public static Matrix3x3 Absolute(Matrix3x3 val) =>
        new(Mathf.Absolute(val.r1c1), Mathf.Absolute(val.r2c1), Mathf.Absolute(val.r3c1),
            Mathf.Absolute(val.r1c2), Mathf.Absolute(val.r2c2), Mathf.Absolute(val.r3c2),
            Mathf.Absolute(val.r1c3), Mathf.Absolute(val.r2c3), Mathf.Absolute(val.r3c3));
    public static Matrix3x3 Average(params Matrix3x3[] vals) => Sum(vals) / vals.Length;
    public static Matrix3x3 Ceiling(Matrix3x3 val) =>
        new(Mathf.Ceiling(val.r1c1), Mathf.Ceiling(val.r2c1), Mathf.Ceiling(val.r3c1),
            Mathf.Ceiling(val.r1c2), Mathf.Ceiling(val.r2c2), Mathf.Ceiling(val.r3c2),
            Mathf.Ceiling(val.r1c3), Mathf.Ceiling(val.r2c3), Mathf.Ceiling(val.r3c3));
    public static Matrix3x3 Clamp(Matrix3x3 val, Matrix3x3 min, Matrix3x3 max) =>
        new(Mathf.Clamp(val.r1c1, min.r1c1, max.r1c1), Mathf.Clamp(val.r2c1, min.r2c1, max.r2c1),
            Mathf.Clamp(val.r3c1, min.r3c1, max.r3c1), Mathf.Clamp(val.r1c2, min.r1c2, max.r1c2),
            Mathf.Clamp(val.r2c2, min.r2c2, max.r2c2), Mathf.Clamp(val.r3c2, min.r3c2, max.r3c2),
            Mathf.Clamp(val.r1c3, min.r1c3, max.r1c3), Mathf.Clamp(val.r2c3, min.r2c3, max.r2c3),
            Mathf.Clamp(val.r3c3, min.r3c3, max.r3c3));
    public static Matrix3x3 Divide(Matrix3x3 num, params Matrix3x3[] vals)
    {
        foreach (Matrix3x3 m in vals) num /= m;
        return num;
    }
    public static Matrix3x3 Floor(Matrix3x3 val) =>
        new(Mathf.Floor(val.r1c1), Mathf.Floor(val.r2c1), Mathf.Floor(val.r3c1),
            Mathf.Floor(val.r1c2), Mathf.Floor(val.r2c2), Mathf.Floor(val.r3c2),
            Mathf.Floor(val.r1c3), Mathf.Floor(val.r2c3), Mathf.Floor(val.r3c3));
    public static Matrix3x3 Lerp(Matrix3x3 a, Matrix3x3 b, float t, bool clamp = true) =>
        new(Mathf.Lerp(a.r1c1, b.r1c1, t, clamp), Mathf.Lerp(a.r2c1, b.r2c1, t, clamp),
            Mathf.Lerp(a.r3c1, b.r3c1, t, clamp), Mathf.Lerp(a.r1c2, b.r1c2, t, clamp),
            Mathf.Lerp(a.r2c2, b.r2c2, t, clamp), Mathf.Lerp(a.r3c2, b.r3c2, t, clamp),
            Mathf.Lerp(a.r1c3, b.r1c3, t, clamp), Mathf.Lerp(a.r2c3, b.r2c3, t, clamp),
            Mathf.Lerp(a.r3c3, b.r3c3, t, clamp));
    public static Matrix3x3 Median(params Matrix3x3[] vals)
    {
        float index = Mathf.Average(0, vals.Length - 1);
        Matrix3x3 valA = vals[Mathf.Floor(index)], valB = vals[Mathf.Ceiling(index)];
        return Average(valA, valB);
    }
    public static Matrix3x3 Product(params Matrix3x3[] vals)
    {
        if (vals.Length < 1) return Zero;
        Matrix3x3 val = Identity;
        foreach (Matrix3x3 m in vals) val *= m;
        return val;
    }
    public static Matrix3x3 Round(Matrix3x3 val) =>
        new(Mathf.Round(val.r1c1), Mathf.Round(val.r2c1), Mathf.Round(val.r3c1),
            Mathf.Round(val.r1c2), Mathf.Round(val.r2c2), Mathf.Round(val.r3c2),
            Mathf.Round(val.r1c3), Mathf.Round(val.r2c3), Mathf.Round(val.r3c3));
    public static Matrix3x3 Subtract(Matrix3x3 num, params Matrix3x3[] vals)
    {
        foreach (Matrix3x3 m in vals) num -= m;
        return num;
    }
    public static Matrix3x3 Sum(params Matrix3x3[] vals)
    {
        Matrix3x3 val = Zero;
        foreach (Matrix3x3 m in vals) val += m;
        return val;
    }

    public static (float[] r1c1s, float[] r2c1s, float[] r3c1s, float[] r1c2s, float[] r2c2s,
        float[] r3c2s, float[] r1c3s, float[] r2c3s, float[] r3c3s) SplitArray(params Matrix3x3[] vals)
    {
        float[] r1c1s = new float[vals.Length], r2c1s = new float[vals.Length], r3c1s = new float[vals.Length],
                r1c2s = new float[vals.Length], r2c2s = new float[vals.Length], r3c2s = new float[vals.Length],
                r1c3s = new float[vals.Length], r2c3s = new float[vals.Length], r3c3s = new float[vals.Length];
        for (int i = 0; i < vals.Length; i++)
        {
            r1c1s[i] = vals[i].r1c1;
            r2c1s[i] = vals[i].r2c1;
            r3c1s[i] = vals[i].r3c1;
            r1c2s[i] = vals[i].r1c2;
            r2c2s[i] = vals[i].r2c2;
            r3c2s[i] = vals[i].r3c2;
            r1c3s[i] = vals[i].r1c3;
            r2c3s[i] = vals[i].r2c3;
            r3c3s[i] = vals[i].r3c3;
        }
        return (r1c1s, r2c1s, r3c1s, r1c2s, r2c2s, r3c2s, r1c3s, r2c3s, r3c3s);
    }

    public Matrix3x3 Adjugate()
    {
        Matrix3x3 dets = new();
        Matrix2x2[,] minors = Minors();
        for (int r = 0; r < 3; r++) for (int c = 0; c < 3; c++) dets[r, c] = minors[r, c].Determinant();
        return dets ^ SignGrid;
    }
    public float Determinant()
    {
        Matrix2x2[,] minors = Minors();
        return (r1c1 * minors[0, 0].Determinant()) - (r1c2 * minors[1, 0].Determinant())
             + (r1c3 * minors[2, 0].Determinant());
    }
    public Matrix3x3 Inverse()
    {
        float d = Determinant();
        if (d == 0) throw new NoInverseException();
        return Transpose().Adjugate() / d;
    }
    public Matrix2x2[,] Minors() => new Matrix2x2[,]
    {
        { new(r2c2, r3c2, r2c3, r3c3), new(r2c1, r3c1, r2c3, r3c3), new(r2c1, r3c1, r2c2, r3c2) },
        { new(r1c2, r3c2, r1c3, r3c3), new(r1c1, r3c1, r1c3, r3c3), new(r1c1, r3c1, r1c2, r3c2) },
        { new(r1c2, r2c2, r1c3, r2c3), new(r1c1, r2c1, r1c3, r2c3), new(r1c1, r2c1, r1c2, r2c2) }
    };
    public Matrix3x3 Transpose() => new(new[,]
    {
        { r1c1, r2c1, r3c1 },
        { r1c2, r2c2, r3c2 },
        { r1c3, r2c3, r3c3 }
    });

    public override bool Equals([NotNullWhen(true)] object? obj)
    {
        if (obj == null || obj.GetType() != typeof(Matrix3x3)) return base.Equals(obj);
        return Equals((Matrix3x3)obj);
    }
    public bool Equals(Matrix3x3 other) =>
        r1c1 == other.r1c1 && r2c1 == other.r2c1 && r3c1 == other.r3c1 &&
        r1c2 == other.r1c2 && r2c2 == other.r2c2 && r3c2 == other.r3c2 &&
        r1c3 == other.r1c3 && r2c3 == other.r2c3 && r3c3 == other.r3c3;
    public override int GetHashCode() =>
        r1c1.GetHashCode() ^ r2c1.GetHashCode() ^ r3c1.GetHashCode() ^
        r1c2.GetHashCode() ^ r2c2.GetHashCode() ^ r3c2.GetHashCode() ^
        r1c3.GetHashCode() ^ r2c3.GetHashCode() ^ r3c3.GetHashCode();
    public override string ToString() => ToString((string?)null);
    public string ToString(string? provider) =>
        r1c1.ToString(provider) + " " + r1c2.ToString(provider) + " " + r1c3.ToString(provider) + "\n" +
        r2c1.ToString(provider) + " " + r2c2.ToString(provider) + " " + r2c3.ToString(provider) + "\n" +
        r3c1.ToString(provider) + " " + r3c2.ToString(provider) + " " + r3c3.ToString(provider);
    public string ToString(IFormatProvider provider) =>
        r1c1.ToString(provider) + " " + r1c2.ToString(provider) + " " + r1c3.ToString(provider) + "\n" +
        r2c1.ToString(provider) + " " + r2c2.ToString(provider) + " " + r2c3.ToString(provider) + "\n" +
        r3c1.ToString(provider) + " " + r3c2.ToString(provider) + " " + r3c3.ToString(provider);

    public object Clone() => new Matrix3x3(ToArray2D());

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    public IEnumerator<float> GetEnumerator()
    {
        yield return r1c1;
        yield return r2c1;
        yield return r3c1;
        yield return r1c2;
        yield return r2c2;
        yield return r3c2;
        yield return r1c3;
        yield return r2c3;
        yield return r3c3;
    }

    public float[] ToArray() => new[] { r1c1, r2c1, r3c1, r1c2, r2c2, r3c2, r1c3, r2c3, r3c3 };
    public float[,] ToArray2D() => new[,]
    {
        { r1c1, r1c2, r1c3 },
        { r2c1, r2c2, r2c3 },
        { r3c1, r3c2, r3c3 }
    };
    public Dictionary<Int2, float> ToDictionary()
    {
        Dictionary<Int2, float> dict = new();
        float[] arr = ToArray();
        for (int i = 0; i < arr.Length; i++) dict.Add(new(i % 3, i / 3), arr[i]);
        return dict;
    }
    public Fill<float> ToFill() => ToFillExtension.ToFill(this);
    public Fill2D<float> ToFill2D()
    {
        Matrix3x3 @this = this;
        return (x, y) => @this[x, y];
    }
    public List<float> ToList() => new() { r1c1, r2c1, r3c1, r1c2, r2c2, r3c2, r1c3, r2c3, r3c3 };

    public static Matrix3x3 operator +(Matrix3x3 a, Matrix3x3 b) =>
        new(a.r1c1 + b.r1c1, a.r2c1 + b.r2c1, a.r3c1 + b.r3c1,
            a.r1c2 + b.r1c2, a.r2c2 + b.r2c2, a.r3c2 + b.r3c2,
            a.r1c3 + b.r1c3, a.r2c3 + b.r2c3, a.r3c3 + b.r3c3);
    public static Matrix3x3 operator -(Matrix3x3 m) => m.Inverse();
    public static Matrix3x3 operator -(Matrix3x3 a, Matrix3x3 b) =>
        new(a.r1c1 - b.r1c1, a.r2c1 - b.r2c1, a.r3c1 - b.r3c1,
            a.r1c2 - b.r1c2, a.r2c2 - b.r2c2, a.r3c2 - b.r3c2,
            a.r1c3 - b.r1c3, a.r2c3 - b.r2c3, a.r3c3 - b.r3c3);
    public static Matrix3x3 operator *(Matrix3x3 a, float b) =>
        new(a.r1c1 * b, a.r2c1 * b, a.r3c1 * b,
            a.r1c2 * b, a.r2c2 * b, a.r3c2 * b,
            a.r1c3 * b, a.r2c3 * b, a.r3c3 * b);
    public static Matrix3x3 operator *(Matrix3x3 a, Matrix3x3 b) => new(new[,]
    {
        { Float3.Dot(a.Row1, b.Column1), Float3.Dot(a.Row1, b.Column2), Float3.Dot(a.Row1, b.Column3) },
        { Float3.Dot(a.Row2, b.Column1), Float3.Dot(a.Row2, b.Column2), Float3.Dot(a.Row2, b.Column3) },
        { Float3.Dot(a.Row3, b.Column1), Float3.Dot(a.Row3, b.Column2), Float3.Dot(a.Row3, b.Column3) },
    });
    public static Matrix3x3 operator /(Matrix3x3 a, float b) =>
        new(a.r1c1 / b, a.r2c1 / b, a.r3c1 / b,
            a.r1c2 / b, a.r2c2 / b, a.r3c2 / b,
            a.r1c3 / b, a.r2c3 / b, a.r3c3 / b);
    public static Matrix3x3 operator /(Matrix3x3 a, Matrix3x3 b) => a * b.Inverse();
    public static Matrix3x3 operator ^(Matrix3x3 a, Matrix3x3 b) => // Single number multiplication
        new(a.r1c1 * b.r1c1, a.r2c1 * b.r2c1, a.r3c1 * b.r3c1,
            a.r1c2 * b.r1c2, a.r2c2 * b.r2c2, a.r3c2 * b.r3c2,
            a.r1c3 * b.r1c3, a.r2c3 * b.r2c3, a.r3c3 * b.r3c3);
    public static bool operator ==(Matrix3x3 a, Matrix3x3 b) => a.Equals(b);
    public static bool operator !=(Matrix3x3 a, Matrix3x3 b) => !a.Equals(b);

    public static explicit operator Matrix3x3(Matrix m)
    {
        Matrix3x3 res = Zero, identity = Identity;
        for (int r = 0; r < 3; r++) for (int c = 0; c < 3; c++)
                res[c, r] = m.Size.x < c && m.Size.y < r ? m[r, c] : identity[r, c];
        return res;
    }
    public static implicit operator Matrix3x3(Matrix2x2 m)
    {
        Matrix3x3 identity = Identity;
        return new(new[,]
        {
            { m.r1c1, m.r1c2, identity.r1c3 },
            { m.r2c1, m.r2c2, identity.r2c3 },
            { identity.r3c1, identity.r3c2, identity.r3c3 }
        });
    }
    public static explicit operator Matrix3x3(Matrix4x4 m) => new(new[,]
    {
        { m.r1c1, m.r1c2, m.r1c3 },
        { m.r2c1, m.r2c2, m.r2c3 },
        { m.r3c1, m.r3c2, m.r3c3 }
    });
}
