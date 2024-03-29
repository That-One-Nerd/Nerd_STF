﻿using System.Data.Common;

namespace Nerd_STF.Mathematics.Algebra;

public record class Matrix4x4 : IStaticMatrix<Matrix4x4>
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
        get => new(r1c1, r1c2, r1c3, r1c4);
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
        get => new(r2c1, r2c2, r2c3, r2c4);
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
        get => new(r3c1, r3c2, r3c3, r3c4);
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
        get => new(r4c1, r4c2, r4c3, r4c4);
        set
        {
            r4c1 = value.x;
            r4c2 = value.y;
            r4c3 = value.z;
            r4c4 = value.w;
        }
    }

    public Int2 Size => (4, 4);

    public float r1c1, r2c1, r3c1, r4c1, r1c2, r2c2, r3c2, r4c2, r1c3, r2c3, r3c3, r4c3, r1c4, r2c4, r3c4, r4c4;

    public Matrix4x4(float all) : this(all, all, all, all, all,
        all, all, all, all, all, all, all, all, all, all, all) { }
    public Matrix4x4(float r1c1, float r1c2, float r1c3, float r1c4, float r2c1, float r2c2, float r2c3,
        float r2c4, float r3c1, float r3c2, float r3c3, float r3c4, float r4c1, float r4c2, float r4c3, float r4c4)
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
    public Matrix4x4(Fill2d<float> fill) : this(fill(0, 0), fill(0, 1), fill(0, 2), fill(0, 3), fill(1, 0),
        fill(1, 1), fill(1, 2), fill(1, 3), fill(2, 0), fill(2, 1), fill(2, 2), fill(2, 3), fill(3, 0),
        fill(3, 1), fill(3, 2), fill(3, 3)) { }
    public Matrix4x4(Fill2d<int> fill) : this(fill(0, 0), fill(0, 1), fill(0, 2), fill(0, 3), fill(1, 0),
        fill(1, 1), fill(1, 2), fill(1, 3), fill(2, 0), fill(2, 1), fill(2, 2), fill(2, 3), fill(3, 0),
        fill(3, 1), fill(3, 2), fill(3, 3)) { }
    public Matrix4x4(Float4 r1, Float4 r2, Float4 r3, Float4 r4) : this(r1.x, r1.y, r1.z,
        r1.w, r2.x, r2.y, r2.z, r2.w, r3.x, r3.y, r3.z, r3.w, r4.x, r4.y, r4.z, r4.w) { }
    public Matrix4x4(Fill<Float4> fill) : this(fill(0), fill(1), fill(2), fill(3)) { }
    public Matrix4x4(Fill<Int4> fill) : this((IEnumerable<int>)fill(0), fill(1), fill(2), fill(3)) { }
    public Matrix4x4(IEnumerable<float> r1, IEnumerable<float> r2, IEnumerable<float> r3, IEnumerable<float> r4)
        : this(r1.ToFill(), r2.ToFill(), r3.ToFill(), r4.ToFill()) { }
    public Matrix4x4(IEnumerable<int> r1, IEnumerable<int> r2, IEnumerable<int> r3, IEnumerable<int> r4)
        : this(r1.ToFill(), r2.ToFill(), r3.ToFill(), r4.ToFill()) { }
    public Matrix4x4(Fill<float> r1, Fill<float> r2, Fill<float> r3, Fill<float> r4) : this(r1(0), r1(1),
        r1(2), r1(3), r2(0), r2(1), r2(2), r2(3), r3(0), r3(1), r3(2), r3(3), r4(0), r4(1), r4(2), r4(3)) { }
    public Matrix4x4(Fill<int> r1, Fill<int> r2, Fill<int> r3, Fill<int> r4) : this(r1(0), r1(1),
        r1(2), r1(3), r2(0), r2(1), r2(2), r2(3), r3(0), r3(1), r3(2), r3(3), r4(0), r4(1), r4(2), r4(3)) { }

    public float this[int r, int c]
    {
        get => ToArray2D()[r, c];
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
    public float this[Index r, Index c]
    {
        get
        {
            int row = r.IsFromEnd ? 4 - r.Value : r.Value,
                col = c.IsFromEnd ? 4 - c.Value : c.Value;
            return this[row, col];
        }
        set
        {
            int row = r.IsFromEnd ? 4 - r.Value : r.Value,
                col = c.IsFromEnd ? 4 - c.Value : c.Value;
            this[row, col] = value;
        }
    }
    public float[,] this[Range rs, Range cs]
    {
        get
        {
            int rowStart = rs.Start.IsFromEnd ? 4 - rs.Start.Value : rs.Start.Value,
                rowEnd = rs.End.IsFromEnd ? 4 - rs.End.Value : rs.End.Value,
                colStart = cs.Start.IsFromEnd ? 4 - cs.Start.Value : cs.Start.Value,
                colEnd = cs.End.IsFromEnd ? 4 - cs.End.Value : cs.End.Value;

            float[,] vals = new float[rowEnd - rowStart - 1, colEnd - colStart - 1];
            for (int r = rowStart; r < rowEnd; r++)
                for (int c = colStart; c < colEnd; c++)
                    vals[r, c] = this[r, c];
            return vals;
        }
        set
        {
            int rowStart = rs.Start.IsFromEnd ? 4 - rs.Start.Value : rs.Start.Value,
                rowEnd = rs.End.IsFromEnd ? 4 - rs.End.Value : rs.End.Value,
                colStart = cs.Start.IsFromEnd ? 4 - cs.Start.Value : cs.Start.Value,
                colEnd = cs.End.IsFromEnd ? 4 - cs.End.Value : cs.End.Value;

            for (int r = rowStart; r < rowEnd; r++)
                for (int c = colStart; c < colEnd; c++)
                    this[r, c] = value[r, c];
        }
    }

    public static Matrix4x4 Absolute(Matrix4x4 val) =>
        new(Mathf.Absolute(val.r1c1), Mathf.Absolute(val.r1c2), Mathf.Absolute(val.r1c3), Mathf.Absolute(val.r1c4),
            Mathf.Absolute(val.r2c1), Mathf.Absolute(val.r2c2), Mathf.Absolute(val.r2c3), Mathf.Absolute(val.r2c4),
            Mathf.Absolute(val.r3c1), Mathf.Absolute(val.r3c2), Mathf.Absolute(val.r3c3), Mathf.Absolute(val.r3c4),
            Mathf.Absolute(val.r4c1), Mathf.Absolute(val.r4c2), Mathf.Absolute(val.r4c3), Mathf.Absolute(val.r4c4));
    public static Matrix4x4 Average(params Matrix4x4[] vals) => Sum(vals) / vals.Length;
    public static Matrix4x4 Ceiling(Matrix4x4 val) =>
        new(Mathf.Ceiling(val.r1c1), Mathf.Ceiling(val.r1c2), Mathf.Ceiling(val.r1c3), Mathf.Ceiling(val.r1c4),
            Mathf.Ceiling(val.r2c1), Mathf.Ceiling(val.r2c2), Mathf.Ceiling(val.r2c3), Mathf.Ceiling(val.r2c4),
            Mathf.Ceiling(val.r3c1), Mathf.Ceiling(val.r3c2), Mathf.Ceiling(val.r3c3), Mathf.Ceiling(val.r3c4),
            Mathf.Ceiling(val.r4c1), Mathf.Ceiling(val.r4c2), Mathf.Ceiling(val.r4c3), Mathf.Ceiling(val.r4c4));
    public static Matrix4x4 Clamp(Matrix4x4 val, Matrix4x4 min, Matrix4x4 max) =>
        new(Mathf.Clamp(val.r1c1, min.r1c1, max.r1c1), Mathf.Clamp(val.r1c2, min.r1c2, max.r1c2),
            Mathf.Clamp(val.r1c3, min.r1c3, max.r1c3), Mathf.Clamp(val.r1c4, min.r1c4, max.r1c4),
            Mathf.Clamp(val.r2c1, min.r2c1, max.r2c1), Mathf.Clamp(val.r2c2, min.r2c2, max.r2c2),
            Mathf.Clamp(val.r2c3, min.r2c3, max.r2c3), Mathf.Clamp(val.r2c4, min.r2c4, max.r2c4),
            Mathf.Clamp(val.r3c1, min.r3c1, max.r3c1), Mathf.Clamp(val.r3c2, min.r3c2, max.r3c2),
            Mathf.Clamp(val.r3c3, min.r3c3, max.r3c3), Mathf.Clamp(val.r3c4, min.r3c4, max.r3c4),
            Mathf.Clamp(val.r4c1, min.r4c1, max.r4c1), Mathf.Clamp(val.r4c2, min.r4c2, max.r4c2),
            Mathf.Clamp(val.r4c3, min.r4c3, max.r4c3), Mathf.Clamp(val.r4c4, min.r4c4, max.r4c4));
    public static Matrix4x4 Divide(Matrix4x4 num, params Matrix4x4[] vals)
    {
        foreach (Matrix4x4 m in vals) num /= m;
        return num;
    }
    public static Matrix4x4 Floor(Matrix4x4 val) =>
        new(Mathf.Floor(val.r1c1), Mathf.Floor(val.r1c2), Mathf.Floor(val.r1c3), Mathf.Floor(val.r1c4),
            Mathf.Floor(val.r2c1), Mathf.Floor(val.r2c2), Mathf.Floor(val.r2c3), Mathf.Floor(val.r2c4),
            Mathf.Floor(val.r3c1), Mathf.Floor(val.r3c2), Mathf.Floor(val.r3c3), Mathf.Floor(val.r3c4),
            Mathf.Floor(val.r4c1), Mathf.Floor(val.r4c2), Mathf.Floor(val.r4c3), Mathf.Floor(val.r4c4));
    public static Matrix4x4 Lerp(Matrix4x4 a, Matrix4x4 b, float t, bool clamp = true) =>
        new(Mathf.Lerp(a.r1c1, b.r1c1, t, clamp), Mathf.Lerp(a.r1c2, b.r1c2, t, clamp),
            Mathf.Lerp(a.r1c3, b.r1c3, t, clamp), Mathf.Lerp(a.r1c4, b.r1c4, t, clamp),
            Mathf.Lerp(a.r2c1, b.r2c1, t, clamp), Mathf.Lerp(a.r2c2, b.r2c2, t, clamp),
            Mathf.Lerp(a.r2c3, b.r2c3, t, clamp), Mathf.Lerp(a.r2c4, b.r2c4, t, clamp),
            Mathf.Lerp(a.r3c1, b.r3c1, t, clamp), Mathf.Lerp(a.r3c2, b.r3c2, t, clamp),
            Mathf.Lerp(a.r3c3, b.r3c3, t, clamp), Mathf.Lerp(a.r3c4, b.r3c4, t, clamp),
            Mathf.Lerp(a.r4c1, b.r4c1, t, clamp), Mathf.Lerp(a.r4c2, b.r4c2, t, clamp),
            Mathf.Lerp(a.r4c3, b.r4c3, t, clamp), Mathf.Lerp(a.r4c4, b.r4c4, t, clamp));
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
        new(Mathf.Round(val.r1c1), Mathf.Round(val.r1c2), Mathf.Round(val.r1c3), Mathf.Round(val.r1c4),
            Mathf.Round(val.r2c1), Mathf.Round(val.r2c2), Mathf.Round(val.r2c3), Mathf.Round(val.r2c4),
            Mathf.Round(val.r3c1), Mathf.Round(val.r3c2), Mathf.Round(val.r3c3), Mathf.Round(val.r3c4),
            Mathf.Round(val.r4c1), Mathf.Round(val.r4c2), Mathf.Round(val.r4c3), Mathf.Round(val.r4c4));
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

    public static (float[] r1c1s, float[] r1c2, float[] r1c3, float[] r1c4, float[] r2c1, float[] r2c2s,
        float[] r2c3, float[] r2c4, float[] r3c1, float[] r3c2, float[] r3c3s, float[] r3c4, float[] r4c1,
        float[] r4c2, float[] r4c3, float[] r4c4s) SplitArray(params Matrix4x4[] vals)
    {
        float[] r1c1s = new float[vals.Length], r1c2s = new float[vals.Length], r1c3s = new float[vals.Length],
                r1c4s = new float[vals.Length], r2c1s = new float[vals.Length], r2c2s = new float[vals.Length],
                r2c3s = new float[vals.Length], r2c4s = new float[vals.Length], r3c1s = new float[vals.Length],
                r3c2s = new float[vals.Length], r3c3s = new float[vals.Length], r3c4s = new float[vals.Length],
                r4c1s = new float[vals.Length], r4c2s = new float[vals.Length], r4c3s = new float[vals.Length],
                r4c4s = new float[vals.Length];
        for (int i = 0; i < vals.Length; i++)
        {
            r1c1s[i] = vals[i].r1c1;
            r1c2s[i] = vals[i].r1c2;
            r1c3s[i] = vals[i].r1c3;
            r1c4s[i] = vals[i].r1c4;
            r2c1s[i] = vals[i].r2c1;
            r2c2s[i] = vals[i].r2c2;
            r2c3s[i] = vals[i].r2c3;
            r2c4s[i] = vals[i].r2c4;
            r3c1s[i] = vals[i].r3c1;
            r3c2s[i] = vals[i].r3c2;
            r3c3s[i] = vals[i].r3c3;
            r3c4s[i] = vals[i].r3c4;
            r4c1s[i] = vals[i].r4c1;
            r4c2s[i] = vals[i].r4c2;
            r4c3s[i] = vals[i].r4c3;
            r4c4s[i] = vals[i].r4c4;
        }
        return (r1c1s, r1c2s, r1c3s, r1c4s, r2c1s, r2c2s, r2c3s, r2c4s,
            r3c1s, r3c2s, r3c3s, r3c4s, r4c1s, r4c2s, r4c3s, r4c4s);
    }

    public Matrix4x4 Adjugate() => Cofactor().Transpose();
    public Matrix4x4 Cofactor()
    {
        Matrix4x4 dets = Zero;
        Matrix3x3[,] minors = Minors();
        for (int r = 0; r < 4; r++) for (int c = 0; c < 4; c++) dets[r, c] = minors[r, c].Determinant();
        return dets ^ SignGrid;
    }
    public float Determinant()
    {
        Matrix3x3[,] minors = Minors();
        return (r1c1 * minors[0, 0].Determinant()) - (r1c2 * minors[0, 1].Determinant()) +
               (r1c3 * minors[0, 2].Determinant()) - (r1c4 * minors[0, 3].Determinant());
    }
    public Matrix4x4? Inverse()
    {
        float d = Determinant();
        if (d == 0) return null;
        return Adjugate() / d;
    }
    public Matrix3x3[,] Minors() => new Matrix3x3[,]
    {
        {
            new(r2c2, r2c3, r2c4, r3c2, r3c3, r3c4, r4c2, r4c3, r4c4),
            new(r2c1, r2c3, r2c4, r3c1, r3c3, r3c4, r4c1, r4c3, r4c4),
            new(r2c1, r2c2, r2c4, r3c1, r3c2, r3c4, r4c1, r4c2, r4c4),
            new(r2c1, r2c2, r2c3, r3c1, r3c2, r3c3, r4c1, r4c2, r4c3)
        },
        {
            new(r1c2, r1c3, r1c4, r3c2, r3c3, r3c4, r4c2, r4c3, r4c4),
            new(r1c1, r1c3, r1c4, r3c1, r3c3, r3c4, r4c1, r4c3, r4c4),
            new(r1c1, r1c2, r1c4, r3c1, r3c2, r3c4, r4c1, r4c2, r4c4),
            new(r1c1, r1c2, r1c3, r3c1, r3c2, r3c3, r4c1, r4c2, r4c3)
        },
        {
            new(r1c2, r1c3, r1c4, r2c2, r2c3, r2c4, r4c2, r4c3, r4c4),
            new(r1c1, r1c3, r1c4, r2c1, r2c3, r2c4, r4c1, r4c3, r4c4),
            new(r1c1, r1c2, r1c4, r2c1, r2c2, r2c4, r4c1, r4c2, r4c4),
            new(r1c1, r1c2, r1c3, r2c1, r2c2, r2c3, r4c1, r4c2, r4c3)
        },
        {
            new(r1c2, r1c3, r1c4, r2c2, r2c3, r2c4, r3c2, r3c3, r3c4),
            new(r1c1, r1c3, r1c4, r2c1, r2c3, r2c4, r3c1, r3c3, r3c4),
            new(r1c1, r1c2, r1c4, r2c1, r2c2, r2c4, r3c1, r3c2, r3c4),
            new(r1c1, r1c2, r1c3, r2c1, r2c2, r2c3, r3c1, r3c2, r3c3)
        }
    };
    public Matrix4x4 Transpose() => new(new[,]
    {
        { r1c1, r2c1, r3c1, r4c1 },
        { r1c2, r2c2, r3c2, r4c2 },
        { r1c3, r2c3, r3c3, r4c3 },
        { r1c4, r2c4, r3c4, r4c4 }
    });

    public float[] GetColumn(int column) =>
        new[] { this[0, column], this[1, column], this[2, column], this[3, column] };
    public float[] GetRow(int row) =>
        new[] { this[row, 0], this[row, 1], this[row, 2], this[row, 3] };

    public void SetColumn(int column, float[] vals)
    {
        if (vals.Length < 4)
            throw new InvalidSizeException("Array must contain enough values to fill the column.");

        this[0, column] = vals[0];
        this[1, column] = vals[1];
        this[2, column] = vals[2];
        this[3, column] = vals[3];
    }
    public void SetRow(int row, float[] vals)
    {
        if (vals.Length < 4)
            throw new InvalidSizeException("Array must contain enough values to fill the row.");

        this[row, 0] = vals[0];
        this[row, 1] = vals[1];
        this[row, 2] = vals[2];
        this[row, 3] = vals[3];
    }

    public Matrix4x4 AddRow(int rowToChange, int referenceRow, float factor = 1)
    {
        Matrix4x4 @this = this;
        return new(delegate (int r, int c)
        {
            if (r == rowToChange) return @this[r, c] += factor * @this[referenceRow, c];
            else return @this[r, c];
        });
    }
    public void AddRowMutable(int rowToChange, int referenceRow, float factor)
    {
        this[rowToChange, 0] += this[referenceRow, 0] * factor;
        this[rowToChange, 1] += this[referenceRow, 1] * factor;
        this[rowToChange, 2] += this[referenceRow, 2] * factor;
        this[rowToChange, 3] += this[referenceRow, 3] * factor;
    }
    public Matrix4x4 ScaleRow(int rowIndex, float factor)
    {
        Matrix4x4 @this = this;
        return new(delegate (int r, int c)
        {
            if (r == rowIndex) return @this[r, c] * factor;
            else return @this[r, c];
        });
    }
    public void ScaleRowMutable(int rowIndex, float factor)
    {
        this[rowIndex, 0] *= factor;
        this[rowIndex, 1] *= factor;
        this[rowIndex, 2] *= factor;
        this[rowIndex, 3] *= factor;
    }
    public Matrix4x4 SwapRows(int rowA, int rowB)
    {
        Matrix4x4 @this = this;
        return new(delegate (int r, int c)
        {
            if (r == rowA) return @this[rowB, c];
            else if (r == rowB) return @this[rowA, c];
            else return @this[r, c];
        });
    }
    public void SwapRowsMutable(int rowA, int rowB)
    {
        float[] dataA = GetRow(rowA), dataB = GetRow(rowB);
        SetRow(rowA, dataB);
        SetRow(rowB, dataA);
    }

    public virtual bool Equals(Matrix4x4? other)
    {
        if (other is null) return false;
        return r1c1 == other.r1c1 && r1c2 == other.r1c2 && r1c3 == other.r1c3 && r1c4 == other.r1c4 &&
               r2c1 == other.r2c1 && r2c2 == other.r2c2 && r2c3 == other.r2c3 && r2c4 == other.r2c4 &&
               r3c1 == other.r3c1 && r3c2 == other.r3c2 && r3c3 == other.r3c3 && r3c4 == other.r3c4 &&
               r4c1 == other.r4c1 && r4c2 == other.r4c2 && r4c3 == other.r4c3 && r4c4 == other.r4c4;
    }
    public override int GetHashCode() => base.GetHashCode();
    public override string ToString() =>
        r1c1 + " " + r1c2 + " " + r1c3 + " " + r1c4 + "\n" +
        r2c1 + " " + r2c2 + " " + r2c3 + " " + r2c4 + "\n" +
        r3c1 + " " + r3c2 + " " + r3c3 + " " + r3c4 + "\n" +
        r4c1 + " " + r4c2 + " " + r4c3 + " " + r4c4;

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    public IEnumerator<float> GetEnumerator()
    {
        yield return r1c1;
        yield return r1c2;
        yield return r1c3;
        yield return r1c4;
        yield return r2c1;
        yield return r2c2;
        yield return r2c3;
        yield return r2c4;
        yield return r3c1;
        yield return r3c2;
        yield return r3c3;
        yield return r3c4;
        yield return r4c1;
        yield return r4c2;
        yield return r4c3;
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
    public Fill<float> ToFill() => ToFillExtension.ToFill(this);
    public Fill2d<float> ToFill2D()
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
        new(a.r1c1 + b.r1c1, a.r1c2 + b.r1c2, a.r1c3 + b.r1c3, a.r1c4 + b.r1c4,
            a.r2c1 + b.r2c1, a.r2c2 + b.r2c2, a.r2c3 + b.r2c3, a.r2c4 + b.r2c4,
            a.r3c1 + b.r3c1, a.r3c2 + b.r3c2, a.r3c3 + b.r3c3, a.r3c4 + b.r3c4,
            a.r4c1 + b.r4c1, a.r4c2 + b.r4c2, a.r4c3 + b.r4c3, a.r4c4 + b.r4c4);
    public static Matrix4x4? operator -(Matrix4x4 m) => m.Inverse();
    public static Matrix4x4 operator -(Matrix4x4 a, Matrix4x4 b) =>
        new(a.r1c1 - b.r1c1, a.r1c2 - b.r1c2, a.r1c3 - b.r1c3, a.r1c4 - b.r1c4,
            a.r2c1 - b.r2c1, a.r2c2 - b.r2c2, a.r2c3 - b.r2c3, a.r2c4 - b.r2c4,
            a.r3c1 - b.r3c1, a.r3c2 - b.r3c2, a.r3c3 - b.r3c3, a.r3c4 - b.r3c4,
            a.r4c1 - b.r4c1, a.r4c2 - b.r4c2, a.r4c3 - b.r4c3, a.r4c4 - b.r4c4);
    public static Matrix4x4 operator *(Matrix4x4 a, float b) =>
        new(a.r1c1 * b, a.r1c2 * b, a.r1c3 * b, a.r1c4 * b,
            a.r2c1 * b, a.r2c2 * b, a.r2c3 * b, a.r2c4 * b,
            a.r3c1 * b, a.r3c2 * b, a.r3c3 * b, a.r3c4 * b,
            a.r4c1 * b, a.r4c2 * b, a.r4c3 * b, a.r4c4 * b);
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
    public static Float4 operator *(Matrix4x4 a, Float4 b) => (Matrix)a * b;
    public static Matrix4x4 operator /(Matrix4x4 a, float b) =>
        new(a.r1c1 / b, a.r1c2 / b, a.r1c3 / b, a.r1c4 / b,
            a.r2c1 / b, a.r2c2 / b, a.r2c3 / b, a.r2c4 / b,
            a.r3c1 / b, a.r3c2 / b, a.r3c3 / b, a.r3c4 / b,
            a.r4c1 / b, a.r4c2 / b, a.r4c3 / b, a.r4c4 / b);
    public static Matrix4x4 operator /(Matrix4x4 a, Matrix4x4 b)
    {
        Matrix4x4? bInv = b.Inverse();
        if (bInv is null) throw new NoInverseException(b);
        return a * bInv;
    }
    public static Float4 operator /(Matrix4x4 a, Float4 b) => (Matrix)a / b;
    public static Matrix4x4 operator ^(Matrix4x4 a, Matrix4x4 b) => // Single number multiplication
        new(a.r1c1 * b.r1c1, a.r1c2 * b.r1c2, a.r1c3 * b.r1c3, a.r1c4 * b.r1c4,
            a.r2c1 * b.r2c1, a.r2c2 * b.r2c2, a.r2c3 * b.r2c3, a.r2c4 * b.r2c4,
            a.r3c1 * b.r3c1, a.r3c2 * b.r3c2, a.r3c3 * b.r3c3, a.r3c4 * b.r3c4,
            a.r4c1 * b.r4c1, a.r4c2 * b.r4c2, a.r4c3 * b.r4c3, a.r4c4 * b.r4c4);

    public static explicit operator Matrix4x4(Matrix m)
    {
        Matrix4x4 res = Zero, identity = Identity;
        for (int r = 0; r < 4; r++) for (int c = 0; c < 4; c++)
                res[c, r] = m.Size.x > r && m.Size.y > c ? m[r, c] : identity[r, c];
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
