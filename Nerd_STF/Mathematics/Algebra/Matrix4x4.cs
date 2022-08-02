namespace Nerd_STF.Mathematics.Algebra;

public struct Matrix4x4 : IMatrix<Matrix4x4, Matrix3x3>
{
    public static Matrix4x4 Identity => new(new[,]
    {
        { 1, 0, 0, 0 },
        { 0, 1, 0, 0 },
        { 0, 0, 1, 0 },
        { 0, 0, 0, 1 },
    });
    public static Matrix4x4 One => new(1);
    public static Matrix4x4 SignGrid => new(new[,]
    {
        { +1, -1, +1, -1 },
        { -1, +1, -1, +1 },
        { +1, -1, +1, -1 },
        { -1, +1, -1, +1 }
    });
    public static Matrix4x4 Zero => new(0);

    public Float4 Column1
    {
        get => new(r1c1, r2c1, r3c1, r4c1);
        set
        {
            r1c1 = value.x;
            r2c1 = value.y;
            r3c1 = value.z;
            r4c1 = value.w;
        }
    }
    public Float4 Column2
    {
        get => new(r1c2, r2c2, r3c2, r4c2);
        set
        {
            r1c2 = value.x;
            r2c2 = value.y;
            r3c2 = value.z;
            r4c2 = value.w;
        }
    }
    public Float4 Column3
    {
        get => new(r1c3, r2c3, r3c3, r4c3);
        set
        {
            r1c3 = value.x;
            r2c3 = value.y;
            r3c3 = value.z;
            r4c3 = value.w;
        }
    }
    public Float4 Column4
    {
        get => new(r1c4, r2c4, r3c4, r4c4);
        set
        {
            r1c4 = value.x;
            r2c4 = value.y;
            r3c4 = value.z;
            r4c4 = value.w;
        }
    }
    public Float4 Row1
    {
        get => new(r1c1, r1c2, r1c3, r1c3);
        set
        {
            r1c1 = value.x;
            r1c2 = value.y;
            r1c3 = value.z;
            r1c4 = value.w;
        }
    }
    public Float4 Row2
    {
        get => new(r2c1, r2c2, r2c3, r2c3);
        set
        {
            r2c1 = value.x;
            r2c2 = value.y;
            r2c3 = value.z;
            r2c4 = value.w;
        }
    }
    public Float4 Row3
    {
        get => new(r3c1, r3c2, r3c3, r3c3);
        set
        {
            r3c1 = value.x;
            r3c2 = value.y;
            r3c3 = value.z;
            r3c4 = value.w;
        }
    }
    public Float4 Row4
    {
        get => new(r4c1, r4c2, r4c3, r4c3);
        set
        {
            r4c1 = value.x;
            r4c2 = value.y;
            r4c3 = value.z;
            r4c4 = value.w;
        }
    }

    public float r1c1, r2c1, r3c1, r4c1, r1c2, r2c2, r3c2, r4c2, r1c3, r2c3, r3c3, r4c3, r1c4, r2c4, r3c4, r4c4;

    public Matrix4x4(float all) : this(all, all, all, all, all,
        all, all, all, all, all, all, all, all, all, all, all) { }
    public Matrix4x4(float r1c1, float r2c1, float r3c1, float r4c1, float r1c2, float r2c2, float r3c2,
        float r4c2, float r1c3, float r2c3, float r3c3, float r4c3, float r1c4, float r2c4, float r3c4, float r4c4)
    {
        this.r1c1 = r1c1;
        this.r2c1 = r2c1;
        this.r3c1 = r3c1;
        this.r4c1 = r4c1;
        this.r1c2 = r1c2;
        this.r2c2 = r2c2;
        this.r3c2 = r3c2;
        this.r4c2 = r4c2;
        this.r1c3 = r1c3;
        this.r2c3 = r2c3;
        this.r3c3 = r3c3;
        this.r4c3 = r4c3;
        this.r1c4 = r1c4;
        this.r2c4 = r2c4;
        this.r3c4 = r3c4;
        this.r4c4 = r4c4;
    }
    public Matrix4x4(float[] nums) : this(nums[0], nums[1], nums[2], nums[3], nums[4], nums[5], nums[6],
        nums[7], nums[8], nums[9], nums[10], nums[11], nums[12], nums[13], nums[14], nums[15]) { }
    public Matrix4x4(int[] nums) : this(nums[0], nums[1], nums[2], nums[3], nums[4], nums[5], nums[6],
        nums[7], nums[8], nums[9], nums[10], nums[11], nums[12], nums[13], nums[14], nums[15]) { }
    public Matrix4x4(Fill<float> fill) : this(fill(0), fill(1), fill(2), fill(3), fill(4), fill(5), fill(6),
        fill(7), fill(8), fill(9), fill(10), fill(11), fill(12), fill(13), fill(14), fill(15)) { }
    public Matrix4x4(Fill<int> fill) : this(fill(0), fill(1), fill(2), fill(3), fill(4), fill(5), fill(6),
        fill(7), fill(8), fill(9), fill(10), fill(11), fill(12), fill(13), fill(14), fill(15)) { }
    public Matrix4x4(float[,] nums) : this(nums[0, 0], nums[0, 1], nums[0, 2], nums[0, 3], nums[1, 0],
        nums[1, 1], nums[1, 2], nums[1, 3], nums[2, 0], nums[2, 1], nums[2, 2], nums[2, 3], nums[3, 0],
        nums[3, 1], nums[3, 2], nums[3, 3]) { }
    public Matrix4x4(int[,] nums) : this(nums[0, 0], nums[0, 1], nums[0, 2], nums[0, 3], nums[1, 0],
        nums[1, 1], nums[1, 2], nums[1, 3], nums[2, 0], nums[2, 1], nums[2, 2], nums[2, 3], nums[3, 0],
        nums[3, 1], nums[3, 2], nums[3, 3]) { }
    public Matrix4x4(Fill2D<float> fill) : this(fill(0, 0), fill(0, 1), fill(0, 2), fill(0, 3), fill(1, 0),
        fill(1, 1), fill(1, 2), fill(1, 3), fill(2, 0), fill(2, 1), fill(2, 2), fill(2, 3), fill(3, 0),
        fill(3, 1), fill(3, 2), fill(3, 3)) { }
    public Matrix4x4(Fill2D<int> fill) : this(fill(0, 0), fill(0, 1), fill(0, 2), fill(0, 3), fill(1, 0),
        fill(1, 1), fill(1, 2), fill(1, 3), fill(2, 0), fill(2, 1), fill(2, 2), fill(2, 3), fill(3, 0),
        fill(3, 1), fill(3, 2), fill(3, 3)) { }
    public Matrix4x4(Float4 c1, Float4 c2, Float4 c3, Float4 c4) : this(c1.x, c1.y, c1.z,
        c1.w, c2.x, c2.y, c2.z, c2.w, c3.x, c3.y, c3.z, c3.w, c4.x, c4.y, c4.z, c4.w) { }
    public Matrix4x4(Fill<Float4> fill) : this(fill(0), fill(1), fill(2), fill(3)) { }
    public Matrix4x4(Fill<Int4> fill) : this((IEnumerable<int>)fill(0), fill(1), fill(2), fill(3)) { }
    public Matrix4x4(IEnumerable<float> c1, IEnumerable<float> c2, IEnumerable<float> c3, IEnumerable<float> c4)
        : this(c1.ToFill(), c2.ToFill(), c3.ToFill(), c4.ToFill()) { }
    public Matrix4x4(IEnumerable<int> c1, IEnumerable<int> c2, IEnumerable<int> c3, IEnumerable<int> c4)
        : this(c1.ToFill(), c2.ToFill(), c3.ToFill(), c4.ToFill()) { }
    public Matrix4x4(Fill<float> c1, Fill<float> c2, Fill<float> c3, Fill<float> c4) : this(c1(0), c1(1),
        c1(2), c1(3), c2(0), c2(1), c2(2), c2(3), c3(0), c3(1), c3(2), c3(3), c4(0), c4(1), c4(2), c4(3)) { }
    public Matrix4x4(Fill<int> c1, Fill<int> c2, Fill<int> c3, Fill<int> c4) : this(c1(0), c1(1),
        c1(2), c1(3), c2(0), c2(1), c2(2), c2(3), c3(0), c3(1), c3(2), c3(3), c4(0), c4(1), c4(2), c4(3)) { }

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

                case "r4c1":
                    r4c1 = value;
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

                case "r4c2":
                    r4c2 = value;
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

                case "r4c3":
                    r4c3 = value;
                    break;

                case "r1c4":
                    r1c4 = value;
                    break;

                case "r2c4":
                    r2c4 = value;
                    break;

                case "r3c4":
                    r3c4 = value;
                    break;

                case "r4c4":
                    r4c4 = value;
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

    public static Matrix4x4 Absolute(Matrix4x4 val) =>
        new(Mathf.Absolute(val.r1c1), Mathf.Absolute(val.r2c1), Mathf.Absolute(val.r3c1), Mathf.Absolute(val.r4c1),
            Mathf.Absolute(val.r1c2), Mathf.Absolute(val.r2c2), Mathf.Absolute(val.r3c2), Mathf.Absolute(val.r4c2),
            Mathf.Absolute(val.r1c3), Mathf.Absolute(val.r2c3), Mathf.Absolute(val.r3c3), Mathf.Absolute(val.r4c2),
            Mathf.Absolute(val.r1c4), Mathf.Absolute(val.r2c4), Mathf.Absolute(val.r3c4), Mathf.Absolute(val.r4c4));
    public static Matrix4x4 Average(params Matrix4x4[] vals) => Sum(vals) / vals.Length;
    public static Matrix4x4 Ceiling(Matrix4x4 val) =>
        new(Mathf.Ceiling(val.r1c1), Mathf.Ceiling(val.r2c1), Mathf.Ceiling(val.r3c1), Mathf.Ceiling(val.r4c1),
            Mathf.Ceiling(val.r1c2), Mathf.Ceiling(val.r2c2), Mathf.Ceiling(val.r3c2), Mathf.Ceiling(val.r4c2),
            Mathf.Ceiling(val.r1c3), Mathf.Ceiling(val.r2c3), Mathf.Ceiling(val.r3c3), Mathf.Ceiling(val.r4c2),
            Mathf.Ceiling(val.r1c4), Mathf.Ceiling(val.r2c4), Mathf.Ceiling(val.r3c4), Mathf.Ceiling(val.r4c4));
    public static Matrix4x4 Clamp(Matrix4x4 val, Matrix4x4 min, Matrix4x4 max) =>
        new(Mathf.Clamp(val.r1c1, min.r1c1, max.r1c1), Mathf.Clamp(val.r2c1, min.r2c1, max.r2c1),
            Mathf.Clamp(val.r3c1, min.r3c1, max.r3c1), Mathf.Clamp(val.r4c1, min.r4c1, max.r4c1),
            Mathf.Clamp(val.r1c2, min.r1c2, max.r1c2), Mathf.Clamp(val.r2c2, min.r2c2, max.r2c2),
            Mathf.Clamp(val.r3c2, min.r3c2, max.r3c2), Mathf.Clamp(val.r4c2, min.r4c2, max.r4c2),
            Mathf.Clamp(val.r1c3, min.r1c3, max.r1c3), Mathf.Clamp(val.r2c3, min.r2c3, max.r2c3),
            Mathf.Clamp(val.r3c3, min.r3c3, max.r3c3), Mathf.Clamp(val.r4c3, min.r4c3, max.r4c3),
            Mathf.Clamp(val.r1c4, min.r1c4, max.r1c4), Mathf.Clamp(val.r2c4, min.r2c4, max.r2c4),
            Mathf.Clamp(val.r3c4, min.r3c4, max.r3c4), Mathf.Clamp(val.r4c4, min.r4c4, max.r4c4));
    public static Matrix4x4 Divide(Matrix4x4 num, params Matrix4x4[] vals)
    {
        foreach (Matrix4x4 m in vals) num /= m;
        return num;
    }
    public static Matrix4x4 Floor(Matrix4x4 val) =>
        new(Mathf.Floor(val.r1c1), Mathf.Floor(val.r2c1), Mathf.Floor(val.r3c1), Mathf.Floor(val.r4c1),
            Mathf.Floor(val.r1c2), Mathf.Floor(val.r2c2), Mathf.Floor(val.r3c2), Mathf.Floor(val.r4c2),
            Mathf.Floor(val.r1c3), Mathf.Floor(val.r2c3), Mathf.Floor(val.r3c3), Mathf.Floor(val.r4c2),
            Mathf.Floor(val.r1c4), Mathf.Floor(val.r2c4), Mathf.Floor(val.r3c4), Mathf.Floor(val.r4c4));
    public static Matrix4x4 Lerp(Matrix4x4 a, Matrix4x4 b, float t, bool clamp = true) =>
        new(Mathf.Lerp(a.r1c1, b.r1c1, t, clamp), Mathf.Lerp(a.r2c1, b.r2c1, t, clamp),
            Mathf.Lerp(a.r3c1, b.r3c1, t, clamp), Mathf.Lerp(a.r4c1, b.r4c1, t, clamp),
            Mathf.Lerp(a.r1c2, b.r1c2, t, clamp), Mathf.Lerp(a.r2c2, b.r2c2, t, clamp),
            Mathf.Lerp(a.r3c2, b.r3c2, t, clamp), Mathf.Lerp(a.r4c2, b.r4c2, t, clamp),
            Mathf.Lerp(a.r1c3, b.r1c3, t, clamp), Mathf.Lerp(a.r2c3, b.r2c3, t, clamp),
            Mathf.Lerp(a.r3c3, b.r3c3, t, clamp), Mathf.Lerp(a.r4c3, b.r4c3, t, clamp),
            Mathf.Lerp(a.r1c4, b.r1c4, t, clamp), Mathf.Lerp(a.r2c4, b.r2c4, t, clamp),
            Mathf.Lerp(a.r3c4, b.r3c4, t, clamp), Mathf.Lerp(a.r4c4, b.r4c4, t, clamp));
    public static Matrix4x4 Median(params Matrix4x4[] vals)
    {
        float index = Mathf.Average(0, vals.Length - 1);
        Matrix4x4 valA = vals[Mathf.Floor(index)], valB = vals[Mathf.Ceiling(index)];
        return Average(valA, valB);
    }
    public static Matrix4x4 Product(params Matrix4x4[] vals)
    {
        if (vals.Length < 1) return Zero;
        Matrix4x4 val = Identity;
        foreach (Matrix4x4 m in vals) val *= m;
        return val;
    }
    public static Matrix4x4 Round(Matrix4x4 val) =>
        new(Mathf.Round(val.r1c1), Mathf.Round(val.r2c1), Mathf.Round(val.r3c1), Mathf.Round(val.r4c1),
            Mathf.Round(val.r1c2), Mathf.Round(val.r2c2), Mathf.Round(val.r3c2), Mathf.Round(val.r4c2),
            Mathf.Round(val.r1c3), Mathf.Round(val.r2c3), Mathf.Round(val.r3c3), Mathf.Round(val.r4c2),
            Mathf.Round(val.r1c4), Mathf.Round(val.r2c4), Mathf.Round(val.r3c4), Mathf.Round(val.r4c4));
    public static Matrix4x4 Subtract(Matrix4x4 num, params Matrix4x4[] vals)
    {
        foreach (Matrix4x4 m in vals) num -= m;
        return num;
    }
    public static Matrix4x4 Sum(params Matrix4x4[] vals)
    {
        Matrix4x4 val = Zero;
        foreach (Matrix4x4 m in vals) val += m;
        return val;
    }

    public static (float[] r1c1s, float[] r2c1s, float[] r3c1s, float[] r4c1s, float[] r1c2s, float[] r2c2s,
        float[] r3c2s, float[] r4c2s, float[] r1c3s, float[] r2c3s, float[] r3c3s, float[] r4c3s, float[] r1c4s,
        float[] r2c4s, float[] r3c4s, float[] r4c4s) SplitArray(params Matrix4x4[] vals)
    {
        float[] r1c1s = new float[vals.Length], r2c1s = new float[vals.Length], r3c1s = new float[vals.Length],
                r4c1s = new float[vals.Length], r1c2s = new float[vals.Length], r2c2s = new float[vals.Length],
                r3c2s = new float[vals.Length], r4c2s = new float[vals.Length], r1c3s = new float[vals.Length],
                r2c3s = new float[vals.Length], r3c3s = new float[vals.Length], r4c3s = new float[vals.Length],
                r1c4s = new float[vals.Length], r2c4s = new float[vals.Length], r3c4s = new float[vals.Length],
                r4c4s = new float[vals.Length];
        for (int i = 0; i < vals.Length; i++)
        {
            r1c1s[i] = vals[i].r1c1;
            r2c1s[i] = vals[i].r2c1;
            r3c1s[i] = vals[i].r3c1;
            r4c1s[i] = vals[i].r4c1;
            r1c2s[i] = vals[i].r1c2;
            r2c2s[i] = vals[i].r2c2;
            r3c2s[i] = vals[i].r3c2;
            r4c2s[i] = vals[i].r4c2;
            r1c3s[i] = vals[i].r1c3;
            r2c3s[i] = vals[i].r2c3;
            r3c3s[i] = vals[i].r3c3;
            r4c3s[i] = vals[i].r4c3;
            r4c4s[i] = vals[i].r4c4;
        }
        return (r1c1s, r2c1s, r3c1s, r4c1s, r1c2s, r2c2s, r3c2s, r4c2s,
            r1c3s, r2c3s, r3c3s, r4c3s, r1c4s, r2c4s, r3c4s, r4c4s);
    }

    public Matrix4x4 Adjugate()
    {
        Matrix4x4 dets = new();
        Matrix3x3[,] minors = Minors();
        for (int r = 0; r < 4; r++) for (int c = 0; c < 4; c++) dets[r, c] = minors[r, c].Determinant();
        return dets ^ SignGrid;
    }
    public float Determinant()
    {
        Matrix3x3[,] minors = Minors();
        return (r1c1 * minors[0, 0].Determinant()) - (r1c2 * minors[1, 0].Determinant()) +
               (r1c3 * minors[2, 0].Determinant()) - (r1c4 * minors[3, 0].Determinant());
    }
    public Matrix4x4 Inverse()
    {
        float d = Determinant();
        if (d == 0) throw new NoInverseException();
        return Transpose().Adjugate() / d;
    }
    public Matrix3x3[,] Minors() => new Matrix3x3[,]
    {
        {
            new(r2c2, r3c2, r4c2, r2c3, r3c3, r4c3, r2c4, r3c4, r4c4),
            new(r2c1, r3c1, r4c1, r2c3, r3c3, r4c3, r2c4, r3c4, r4c4),
            new(r2c1, r3c1, r4c1, r2c2, r3c2, r4c2, r2c4, r3c4, r4c4),
            new(r2c1, r3c1, r4c1, r2c2, r3c2, r4c2, r2c3, r3c3, r4c3)
        },
        {
            new(r1c2, r3c2, r4c2, r1c3, r3c3, r4c3, r1c4, r3c4, r4c4),
            new(r1c1, r3c1, r4c1, r1c3, r3c3, r4c3, r1c4, r3c4, r4c4),
            new(r1c1, r3c1, r4c1, r1c2, r3c2, r4c2, r1c4, r3c4, r4c4),
            new(r1c1, r3c1, r4c1, r1c2, r3c2, r4c2, r1c3, r3c3, r4c3)
        },
        {
            new(r1c2, r2c2, r4c2, r1c3, r2c3, r4c3, r1c4, r2c4, r4c4),
            new(r1c1, r2c1, r4c1, r1c3, r2c3, r4c3, r1c4, r2c4, r4c4),
            new(r1c1, r2c1, r4c1, r1c2, r2c2, r4c2, r1c4, r2c4, r4c4),
            new(r1c1, r2c1, r4c1, r1c2, r2c2, r4c2, r1c3, r2c3, r4c3)
        },
        {
            new(r1c2, r2c2, r3c2, r1c3, r2c3, r3c3, r1c4, r2c4, r3c4),
            new(r1c1, r2c1, r3c1, r1c3, r2c3, r3c3, r1c4, r2c4, r3c4),
            new(r1c1, r2c1, r3c1, r1c2, r2c2, r3c2, r1c4, r2c4, r3c4),
            new(r1c1, r2c1, r3c1, r1c2, r2c2, r3c2, r1c3, r2c3, r3c3)
        }
    };
    public Matrix4x4 Transpose() => new(new[,]
    {
        { r1c1, r2c1, r3c1, r4c1 },
        { r1c2, r2c2, r3c2, r4c2 },
        { r1c3, r2c3, r3c3, r4c3 },
        { r1c4, r2c4, r3c4, r4c4 }
    });

    public override bool Equals([NotNullWhen(true)] object? obj)
    {
        if (obj == null || obj.GetType() != typeof(Matrix4x4)) return base.Equals(obj);
        return Equals((Matrix4x4)obj);
    }
    public bool Equals(Matrix4x4 other) =>
        r1c1 == other.r1c1 && r2c1 == other.r2c1 && r3c1 == other.r3c1 && r4c1 == other.r4c1 &&
        r1c2 == other.r1c2 && r2c2 == other.r2c2 && r3c2 == other.r3c2 && r4c2 == other.r4c2 &&
        r1c3 == other.r1c3 && r2c3 == other.r2c3 && r3c3 == other.r3c3 && r4c3 == other.r4c3 &&
        r1c4 == other.r1c4 && r2c3 == other.r2c4 && r3c4 == other.r3c4 && r4c4 == other.r4c4;
    public override int GetHashCode() =>
        r1c1.GetHashCode() ^ r2c1.GetHashCode() ^ r3c1.GetHashCode() ^ r4c1.GetHashCode() ^
        r1c2.GetHashCode() ^ r2c2.GetHashCode() ^ r3c2.GetHashCode() ^ r4c2.GetHashCode() ^
        r1c3.GetHashCode() ^ r2c3.GetHashCode() ^ r3c3.GetHashCode() ^ r4c3.GetHashCode() ^
        r1c4.GetHashCode() ^ r2c4.GetHashCode() ^ r3c4.GetHashCode() ^ r4c4.GetHashCode();
    public string ToString(string? provider) =>
        r1c1.ToString(provider) + " " + r1c2.ToString(provider) + " " + r1c3.ToString(provider) + " " +
        r1c4.ToString(provider) + "\n" + r2c1.ToString(provider) + " " + r2c2.ToString(provider) + " " +
        r2c3.ToString(provider) + " " + r2c4.ToString(provider) + "\n" + r3c1.ToString(provider) + " " +
        r3c2.ToString(provider) + " " + r3c3.ToString(provider) + " " + r3c4.ToString(provider) + "\n" +
        r4c1.ToString(provider) + " " + r4c2.ToString(provider) + " " + r4c3.ToString(provider) + " " +
        r4c4.ToString(provider);
    public string ToString(IFormatProvider provider) =>
        r1c1.ToString(provider) + " " + r1c2.ToString(provider) + " " + r1c3.ToString(provider) + " " +
        r1c4.ToString(provider) + "\n" + r2c1.ToString(provider) + " " + r2c2.ToString(provider) + " " +
        r2c3.ToString(provider) + " " + r2c4.ToString(provider) + "\n" + r3c1.ToString(provider) + " " +
        r3c2.ToString(provider) + " " + r3c3.ToString(provider) + " " + r3c4.ToString(provider) + "\n" +
        r4c1.ToString(provider) + " " + r4c2.ToString(provider) + " " + r4c3.ToString(provider) + " " +
        r4c4.ToString(provider);

    public object Clone() => new Matrix4x4(ToArray2D());

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    public IEnumerator<float> GetEnumerator()
    {
        yield return r1c1;
        yield return r2c1;
        yield return r3c1;
        yield return r4c1;
        yield return r1c2;
        yield return r2c2;
        yield return r3c2;
        yield return r4c2;
        yield return r1c3;
        yield return r2c3;
        yield return r3c3;
        yield return r4c3;
        yield return r1c4;
        yield return r2c4;
        yield return r3c4;
        yield return r4c4;
    }

    public float[] ToArray() => new[]
    {
        r1c1, r2c1, r3c1, r4c1,
        r1c2, r2c2, r3c2, r4c2,
        r1c3, r2c3, r3c3, r4c3,
        r1c4, r2c4, r3c4, r4c4
    };
    public float[,] ToArray2D() => new[,]
    {
        { r1c1, r1c2, r1c3, r1c4 },
        { r2c1, r2c2, r2c3, r2c4 },
        { r3c1, r3c2, r3c3, r3c4 },
        { r4c1, r4c2, r4c3, r4c4 }
    };
    public Dictionary<Int2, float> ToDictionary()
    {
        Dictionary<Int2, float> dict = new();
        float[] arr = ToArray();
        for (int i = 0; i < arr.Length; i++) dict.Add(new(i % 4, i / 4), arr[i]);
        return dict;
    }
    public Fill<float> ToFill() => ToFillExtension.ToFill(this);
    public Fill2D<float> ToFill2D()
    {
        Matrix4x4 @this = this;
        return (x, y) => @this[x, y];
    }
    public List<float> ToList() => new()
    {
        r1c1, r2c1, r3c1, r4c1,
        r1c2, r2c2, r3c2, r4c2,
        r1c3, r2c3, r3c3, r4c3,
        r1c4, r2c4, r3c4, r4c4
    };

    public static Matrix4x4 operator +(Matrix4x4 a, Matrix4x4 b) =>
        new(a.r1c1 + b.r1c1, a.r2c1 + b.r2c1, a.r3c1 + b.r3c1, a.r4c1 + b.r4c1,
            a.r1c2 + b.r1c2, a.r2c2 + b.r2c2, a.r3c2 + b.r3c2, a.r4c2 + b.r4c2,
            a.r1c3 + b.r1c3, a.r2c3 + b.r2c3, a.r3c3 + b.r3c3, a.r4c3 + b.r4c3,
            a.r1c4 + b.r1c4, a.r2c4 + b.r2c4, a.r3c4 + b.r3c4, a.r4c4 + b.r4c4);
    public static Matrix4x4 operator -(Matrix4x4 m) => m.Inverse();
    public static Matrix4x4 operator -(Matrix4x4 a, Matrix4x4 b) =>
        new(a.r1c1 - b.r1c1, a.r2c1 - b.r2c1, a.r3c1 - b.r3c1, a.r4c1 - b.r4c1,
            a.r1c2 - b.r1c2, a.r2c2 - b.r2c2, a.r3c2 - b.r3c2, a.r4c2 - b.r4c2,
            a.r1c3 - b.r1c3, a.r2c3 - b.r2c3, a.r3c3 - b.r3c3, a.r4c3 - b.r4c3,
            a.r1c4 - b.r1c4, a.r2c4 - b.r2c4, a.r3c4 - b.r3c4, a.r4c4 - b.r4c4);
    public static Matrix4x4 operator *(Matrix4x4 a, float b) =>
        new(a.r1c1 * b, a.r2c1 * b, a.r3c1 * b, a.r4c1 * b,
            a.r1c2 * b, a.r2c2 * b, a.r3c2 * b, a.r4c2 * b,
            a.r1c3 * b, a.r2c3 * b, a.r3c3 * b, a.r4c3 * b,
            a.r1c4 * b, a.r2c4 * b, a.r3c4 * b, a.r4c4 * b);
    public static Matrix4x4 operator *(Matrix4x4 a, Matrix4x4 b) => new(new[,]
    {
        { Float4.Dot(a.Row1, b.Column1), Float4.Dot(a.Row1, b.Column2),
          Float4.Dot(a.Row1, b.Column3), Float4.Dot(a.Row1, b.Column4) },
        { Float4.Dot(a.Row2, b.Column1), Float4.Dot(a.Row2, b.Column2),
          Float4.Dot(a.Row2, b.Column3), Float4.Dot(a.Row2, b.Column4) },
        { Float4.Dot(a.Row3, b.Column1), Float4.Dot(a.Row3, b.Column2),
          Float4.Dot(a.Row3, b.Column3), Float4.Dot(a.Row3, b.Column4) },
        { Float4.Dot(a.Row4, b.Column1), Float4.Dot(a.Row4, b.Column2),
          Float4.Dot(a.Row4, b.Column3), Float4.Dot(a.Row4, b.Column4) }
    });
    public static Matrix4x4 operator /(Matrix4x4 a, float b) =>
        new(a.r1c1 / b, a.r2c1 / b, a.r3c1 / b, a.r4c1 / b,
            a.r1c2 / b, a.r2c2 / b, a.r3c2 / b, a.r4c2 / b,
            a.r1c3 / b, a.r2c3 / b, a.r3c3 / b, a.r4c3 / b,
            a.r1c4 / b, a.r2c4 / b, a.r3c4 / b, a.r4c4 / b);
    public static Matrix4x4 operator /(Matrix4x4 a, Matrix4x4 b) => a * b.Inverse();
    public static Matrix4x4 operator ^(Matrix4x4 a, Matrix4x4 b) => // Single number multiplication
        new(a.r1c1 * b.r1c1, a.r2c1 * b.r2c1, a.r3c1 * b.r3c1, a.r4c1 * b.r4c1,
            a.r1c2 * b.r1c2, a.r2c2 * b.r2c2, a.r3c2 * b.r3c2, a.r4c2 * b.r4c2,
            a.r1c3 * b.r1c3, a.r2c3 * b.r2c3, a.r3c3 * b.r3c3, a.r4c3 * b.r4c3,
            a.r1c4 * b.r1c4, a.r2c4 * b.r2c4, a.r3c4 * b.r3c4, a.r4c4 * b.r4c4);
    public static bool operator ==(Matrix4x4 a, Matrix4x4 b) => a.Equals(b);
    public static bool operator !=(Matrix4x4 a, Matrix4x4 b) => !a.Equals(b);

    public static explicit operator Matrix4x4(Matrix m)
    {
        Matrix4x4 res = Zero, identity = Identity;
        for (int r = 0; r < 4; r++) for (int c = 0; c < 4; c++)
                res[c, r] = m.Size.x < c && m.Size.y < r ? m[r, c] : identity[r, c];
        return res;
    }
    public static implicit operator Matrix4x4(Matrix2x2 m)
    {
        Matrix4x4 identity = Identity;
        return new(new[,]
        {
            { m.r1c1, m.r1c2, identity.r1c3, identity.r1c4 },
            { m.r2c1, m.r2c2, identity.r2c3, identity.r2c4 },
            { identity.r3c1, identity.r3c2, identity.r3c3, identity.r3c4 },
            { identity.r4c1, identity.r4c2, identity.r4c3, identity.r4c4 }
        });
    }
    public static implicit operator Matrix4x4(Matrix3x3 m)
    {
        Matrix4x4 identity = Identity;
        return new(new[,]
        {
            { m.r1c1, m.r1c2, m.r1c3, identity.r1c4 },
            { m.r2c1, m.r2c2, m.r2c3, identity.r2c4 },
            { m.r3c1, m.r3c2, m.r3c3, identity.r3c4 },
            { identity.r4c1, identity.r4c2, identity.r4c3, identity.r4c4 }
        });
    }
}
