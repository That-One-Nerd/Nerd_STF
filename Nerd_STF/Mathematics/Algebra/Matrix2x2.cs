namespace Nerd_STF.Mathematics.Algebra;

public record class Matrix2x2 : IStaticMatrix<Matrix2x2>
{
    public static Matrix2x2 Identity => new(new[,]
    {
        { 1, 0 },
        { 0, 1 }
    });
    public static Matrix2x2 One => new(1);
    public static Matrix2x2 SignGrid => new(new[,]
    {
        { +1, -1 },
        { -1, +1 }
    });
    public static Matrix2x2 Zero => new(0);

    public Float2 Column1
    {
        get => new(r1c1, r2c1);
        set
        {
            r1c1 = value.x;
            r2c1 = value.y;
        }
    }
    public Float2 Column2
    {
        get => new(r1c2, r2c2);
        set
        {
            r1c2 = value.x;
            r2c2 = value.y;
        }
    }
    public Float2 Row1
    {
        get => new(r1c1, r1c2);
        set
        {
            r1c1 = value.x;
            r1c2 = value.y;
        }
    }
    public Float2 Row2
    {
        get => new(r2c1, r2c2);
        set
        {
            r2c1 = value.x;
            r2c2 = value.y;
        }
    }

    public Int2 Size => (2, 2);

    public float r1c1, r2c1, r1c2, r2c2;

    public Matrix2x2(float all) : this(all, all, all, all) { }
    public Matrix2x2(float r1c1, float r1c2, float r2c1, float r2c2)
    {
        this.r1c1 = r1c1;
        this.r1c2 = r1c2;
        this.r2c1 = r2c1;
        this.r2c2 = r2c2;
    }
    public Matrix2x2(float[] nums) : this(nums[0], nums[1], nums[2], nums[3]) { }
    public Matrix2x2(int[] nums) : this(nums[0], nums[1], nums[2], nums[3]) { }
    public Matrix2x2(Fill<float> fill) : this(fill(0), fill(1), fill(2), fill(3)) { }
    public Matrix2x2(Fill<int> fill) : this(fill(0), fill(1), fill(2), fill(3)) { }
    public Matrix2x2(float[,] nums) : this(nums[0, 0], nums[0, 1], nums[1, 0], nums[1, 1]) { }
    public Matrix2x2(int[,] nums) : this(nums[0, 0], nums[0, 1], nums[1, 0], nums[1, 1]) { }
    public Matrix2x2(Fill2d<float> fill) : this(fill(0, 0), fill(0, 1), fill(1, 0), fill(1, 1)) { }
    public Matrix2x2(Fill2d<int> fill) : this(fill(0, 0), fill(0, 1), fill(1, 0), fill(1, 1)) { }
    public Matrix2x2(Float2 r1, Float2 r2) : this(r1.x, r1.y, r2.x, r2.y) { }
    public Matrix2x2(Fill<Float2> fill) : this(fill(0), fill(1)) { }
    public Matrix2x2(Fill<Int2> fill) : this((IEnumerable<int>)fill(0), fill(1)) { }
    public Matrix2x2(IEnumerable<float> r1, IEnumerable<float> r2) : this(r1.ToFill(), r2.ToFill()) { }
    public Matrix2x2(IEnumerable<int> r1, IEnumerable<int> r2) : this(r1.ToFill(), r2.ToFill()) { }
    public Matrix2x2(Fill<float> r1, Fill<float> r2) : this(r1(0), r1(1), r2(0), r2(1)) { }
    public Matrix2x2(Fill<int> r1, Fill<int> r2) : this(r1(0), r1(1), r2(0), r2(1)) { }

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

                case "r1c2":
                    r1c2 = value;
                    break;

                case "r2c2":
                    r2c2 = value;
                    break;

                default:
                    string @params = "";
                    if (r < 0 || r > 1) @params += r;
                    if (c < 0 || c > 1) @params += string.IsNullOrEmpty(@params) ? c : " and " + c;
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
            int row = r.IsFromEnd ? 2 - r.Value : r.Value,
                col = c.IsFromEnd ? 2 - c.Value : c.Value;
            return this[row, col];
        }
        set
        {
            int row = r.IsFromEnd ? 2 - r.Value : r.Value,
                col = c.IsFromEnd ? 2 - c.Value : c.Value;
            this[row, col] = value;
        }
    }
    public float[,] this[Range rs, Range cs]
    {
        get
        {
            int rowStart = rs.Start.IsFromEnd ? 2 - rs.Start.Value : rs.Start.Value,
                rowEnd = rs.End.IsFromEnd ? 2 - rs.End.Value : rs.End.Value,
                colStart = cs.Start.IsFromEnd ? 2 - cs.Start.Value : cs.Start.Value,
                colEnd = cs.End.IsFromEnd ? 2 - cs.End.Value : cs.End.Value;

            float[,] vals = new float[rowEnd - rowStart - 1, colEnd - colStart - 1];
            for (int r = rowStart; r < rowEnd; r++)
                for (int c = colStart; c < colEnd; c++)
                    vals[r, c] = this[r, c];
            return vals;
        }
        set
        {
            int rowStart = rs.Start.IsFromEnd ? 2 - rs.Start.Value : rs.Start.Value,
                rowEnd = rs.End.IsFromEnd ? 2 - rs.End.Value : rs.End.Value,
                colStart = cs.Start.IsFromEnd ? 2 - cs.Start.Value : cs.Start.Value,
                colEnd = cs.End.IsFromEnd ? 2 - cs.End.Value : cs.End.Value;

            for (int r = rowStart; r < rowEnd; r++)
                for (int c = colStart; c < colEnd; c++)
                    this[r, c] = value[r, c];
        }
    }

    public static Matrix2x2 Absolute(Matrix2x2 val) => new(Mathf.Absolute(val.r1c1), Mathf.Absolute(val.r1c2),
        Mathf.Absolute(val.r2c1), Mathf.Absolute(val.r2c2));
    public static Matrix2x2 Average(params Matrix2x2[] vals) => Sum(vals) / vals.Length;
    public static Matrix2x2 Ceiling(Matrix2x2 val) => new(Mathf.Ceiling(val.r1c1), Mathf.Ceiling(val.r1c2),
        Mathf.Ceiling(val.r2c1), Mathf.Ceiling(val.r2c2));
    public static Matrix2x2 Clamp(Matrix2x2 val, Matrix2x2 min, Matrix2x2 max) =>
        new(Mathf.Clamp(val.r1c1, min.r1c1, max.r1c1), Mathf.Clamp(val.r1c2, min.r1c2, max.r1c2),
            Mathf.Clamp(val.r2c1, min.r2c1, max.r2c1), Mathf.Clamp(val.r2c2, min.r2c2, max.r2c2));
    public static Matrix2x2 Divide(Matrix2x2 num, params Matrix2x2[] vals)
    {
        foreach (Matrix2x2 m in vals) num /= m;
        return num;
    }
    public static Matrix2x2 Floor(Matrix2x2 val) => new(Mathf.Floor(val.r1c1), Mathf.Floor(val.r1c2),
        Mathf.Floor(val.r2c1), Mathf.Floor(val.r2c2));
    public static Matrix2x2 Lerp(Matrix2x2 a, Matrix2x2 b, float t, bool clamp = true) =>
        new(Mathf.Lerp(a.r1c1, b.r1c1, t, clamp), Mathf.Lerp(a.r1c2, b.r1c2, t, clamp),
            Mathf.Lerp(a.r2c1, b.r2c1, t, clamp), Mathf.Lerp(a.r2c2, b.r2c2, t, clamp));
    public static Matrix2x2 Median(params Matrix2x2[] vals)
    {
        float index = Mathf.Average(0, vals.Length - 1);
        Matrix2x2 valA = vals[Mathf.Floor(index)], valB = vals[Mathf.Ceiling(index)];
        return Average(valA, valB);
    }
    public static Matrix2x2 Product(params Matrix2x2[] vals)
    {
        if (vals.Length < 1) return Zero;
        Matrix2x2 val = Identity;
        foreach (Matrix2x2 m in vals) val *= m;
        return val;
    }
    public static Matrix2x2 Round(Matrix2x2 val) => new(Mathf.Round(val.r1c1), Mathf.Round(val.r1c2),
        Mathf.Round(val.r2c1), Mathf.Round(val.r2c2));
    public static Matrix2x2 Subtract(Matrix2x2 num, params Matrix2x2[] vals)
    {
        foreach (Matrix2x2 m in vals) num -= m;
        return num;
    }
    public static Matrix2x2 Sum(params Matrix2x2[] vals)
    {
        Matrix2x2 val = Zero;
        foreach (Matrix2x2 m in vals) val += m;
        return val;
    }

    public static (float[] r1c1s, float[] r1c2s, float[] r2c1s, float[] r2c2s) SplitArray(params Matrix2x2[] vals)
    {
        float[] r1c1s = new float[vals.Length], r1c2s = new float[vals.Length], r2c1s = new float[vals.Length],
                r2c2s = new float[vals.Length];
        for (int i = 0; i < vals.Length; i++)
        {
            r1c1s[i] = vals[i].r1c1;
            r1c2s[i] = vals[i].r1c2;
            r2c1s[i] = vals[i].r2c1;
            r2c2s[i] = vals[i].r2c2;
        }
        return (r1c1s, r1c2s, r2c1s, r2c2s);
    }

    public Matrix2x2 Adjugate() => Cofactor().Transpose();
    public Matrix2x2 Cofactor()
    {
        Matrix2x2 swapped = new(new[,]
        {
            { r2c2, r2c1 },
            { r1c2, r1c1 }
        });
        return swapped ^ SignGrid;
    }
    public float Determinant() => r1c1 * r2c2 - r1c2 * r2c1;
    public Matrix2x2? Inverse()
    {
        float d = Determinant();
        if (d == 0) return null;
        return Transpose().Adjugate() / d;
    }
    public Matrix2x2 Transpose() => new(new[,]
    {
        { r1c1, r2c1 },
        { r1c2, r2c2 }
    });

    public float[] GetColumn(int column) => new[] { this[0, column], this[1, column] };
    public float[] GetRow(int row) => new[] { this[row, 0], this[row, 1] };

    public void SetColumn(int column, float[] vals)
    {
        if (vals.Length < 2)
            throw new InvalidSizeException("Array must contain enough values to fill the column.");
        this[0, column] = vals[0];
        this[1, column] = vals[1];
    }
    public void SetRow(int row, float[] vals)
    {
        if (vals.Length < 2)
            throw new InvalidSizeException("Array must contain enough values to fill the row.");
        this[row, 0] = vals[0];
        this[row, 1] = vals[1];
    }

    public Matrix2x2 AddRow(int rowToChange, int referenceRow, float factor = 1)
    {
        Matrix2x2 @this = this;
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
    }
    public Matrix2x2 ScaleRow(int rowIndex, float factor)
    {
        Matrix2x2 @this = this;
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
    }
    public Matrix2x2 SwapRows(int rowA, int rowB)
    {
        Matrix2x2 @this = this;
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

    public virtual bool Equals(Matrix2x2? other)
    {
        if (other is null) return false;
        return r1c1 == other.r1c1 && r1c2 == other.r1c2 && r2c1 == other.r2c1 && r2c2 == other.r2c2;
    }
    public override int GetHashCode() => base.GetHashCode();
    public override string ToString() => r1c1 + " " + r1c2 + "\n" + r2c1 + " " + r2c2;

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    public IEnumerator<float> GetEnumerator()
    {
        yield return r1c1;
        yield return r1c2;
        yield return r2c1;
        yield return r2c2;
    }

    public float[] ToArray() => new[] { r1c1, r1c2, r2c1, r2c2 };
    public float[,] ToArray2D() => new[,]
    {
        { r1c1, r1c2 },
        { r2c1, r2c2 }
    };
    public Fill<float> ToFill() => ToFillExtension.ToFill(this);
    public Fill2d<float> ToFill2D()
    {
        Matrix2x2 @this = this;
        return (x, y) => @this[x, y];
    }
    public List<float> ToList() => new() { r1c1, r1c2, r2c1, r2c2 };

    public static Matrix2x2 operator +(Matrix2x2 a, Matrix2x2 b) => new(a.r1c1 + b.r1c1, a.r1c2 + b.r1c2,
        a.r2c1 + b.r2c1, a.r2c2 + b.r2c2);
    public static Matrix2x2? operator -(Matrix2x2 m) => m.Inverse();
    public static Matrix2x2 operator -(Matrix2x2 a, Matrix2x2 b) => new(a.r1c1 - b.r1c1, a.r1c2 - b.r1c2,
        a.r2c1 - b.r2c1, a.r2c2 - b.r2c2);
    public static Matrix2x2 operator *(Matrix2x2 a, float b) => new(a.r1c1 * b, a.r1c2 * b, a.r2c1 * b, a.r2c2 * b);
    public static Matrix2x2 operator *(Matrix2x2 a, Matrix2x2 b) => new(new[,]
    {
        { Float2.Dot(a.Row1, b.Column1), Float2.Dot(a.Row1, b.Column2) },
        { Float2.Dot(a.Row2, b.Column1), Float2.Dot(a.Row2, b.Column2) },
    });
    public static Float2 operator *(Matrix2x2 a, Float2 b) => (Matrix)a * b;
    public static Matrix2x2 operator /(Matrix2x2 a, float b) => new(a.r1c1 / b, a.r1c2 / b, a.r2c1 / b, a.r2c2 / b);
    public static Matrix2x2 operator /(Matrix2x2 a, Matrix2x2 b)
    {
        Matrix2x2? bInv = b.Inverse();
        if (bInv is null) throw new NoInverseException(b);
        return a * bInv;
    }
    public static Float2 operator /(Matrix2x2 a, Float2 b) => (Matrix)a / b;
    public static Matrix2x2 operator ^(Matrix2x2 a, Matrix2x2 b) => // Single number multiplication.
        new(a.r1c1 * b.r1c1, a.r1c2 * b.r1c2, a.r2c1 * b.r2c1, a.r2c2 * b.r2c2);

    public static explicit operator Matrix2x2(Matrix m)
    {
        Matrix2x2 res = Zero, identity = Identity;
        for (int r = 0; r < 2; r++) for (int c = 0; c < 2; c++)
                res[c, r] = m.Size.x > r && m.Size.y > c ? m[r, c] : identity[r, c];
        return res;
    }
    public static explicit operator Matrix2x2(Matrix3x3 m) => new(m.r1c1, m.r2c1, m.r1c2, m.r2c2);
    public static explicit operator Matrix2x2(Matrix4x4 m) => new(m.r1c1, m.r2c1, m.r1c2, m.r2c2);
}
