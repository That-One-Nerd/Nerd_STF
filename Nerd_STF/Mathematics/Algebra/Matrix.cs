namespace Nerd_STF.Mathematics.Algebra;

public struct Matrix : IMatrix<Matrix, Matrix>
{
    public static Matrix Identity(Int2 size)
    {
        Matrix m = Zero(size);
        int max = Mathf.Min(size.x, size.y);
        for (int i = 0; i < max; i++) m[i, i] = 1;
        return m;
    }
    public static Matrix One(Int2 size) => new(size, 1);
    public static Matrix SignGrid(Int2 size) => new(size, Equations.SgnFill);
    public static Matrix Zero(Int2 size) => new(size);

    public bool HasMinors => Size.x > 1 && Size.y > 1;
    public bool IsSquare => Size.x == Size.y;

    public Int2 Size { get; private init; }

    private readonly float[,] array;

    public Matrix() : this(Int2.Zero) { }
    public Matrix(Int2 size, float all = 0)
    {
        Size = size;
        array = new float[size.x, size.y];

        if (all == 0) return;
        for (int r = 0; r < size.y; r++) for (int c = 0; c < size.x; c++) array[c, r] = all;
    }
    public Matrix(Int2 size, float[] vals)
    {
        Size = size;
        array = new float[size.x, size.y];

        if (vals.Length < size.x * size.y)
            throw new InvalidSizeException("Array must contain enough values to fill the matrix.");

        for (int r = 0; r < size.y; r++) for (int c = 0; c < size.x; c++) array[c, r] = vals[c + r * size.y];
    }
    public Matrix(Int2 size, int[] vals)
    {
        Size = size;
        array = new float[size.x, size.y];

        if (vals.Length < size.x * size.y)
            throw new InvalidSizeException("Array must contain enough values to fill the matrix.");

        for (int r = 0; r < size.y; r++) for (int c = 0; c < size.x; c++) array[c, r] = vals[c + r * size.y];
    }
    public Matrix(Int2 size, Fill<float> vals)
    {
        Size = size;
        array = new float[size.x, size.y];

        for (int r = 0; r < size.y; r++) for (int c = 0; c < size.x; c++) array[c, r] = vals(c + r * size.y);
    }
    public Matrix(Int2 size, Fill<int> vals)
    {
        Size = size;
        array = new float[size.x, size.y];

        for (int r = 0; r < size.y; r++) for (int c = 0; c < size.x; c++) array[c, r] = vals(c + r * size.y);
    }
    public Matrix(Int2 size, float[,] vals)
    {
        Size = size;
        array = new float[size.x, size.y];
        for (int r = 0; r < size.y; r++) for (int c = 0; c < size.x; c++) array[c, r] = vals[c, r];
    }
    public Matrix(Int2 size, int[,] vals)
    {
        Size = size;
        array = new float[size.x, size.y];
        for (int r = 0; r < size.y; r++) for (int c = 0; c < size.x; c++) array[c, r] = vals[c, r];
    }
    public Matrix(Int2 size, Fill2D<float> vals)
    {
        Size = size;
        array = new float[size.x, size.y];
        for (int r = 0; r < size.y; r++) for (int c = 0; c < size.x; c++) array[c, r] = vals(c, r);
    }
    public Matrix(Int2 size, Fill2D<int> vals)
    {
        Size = size;
        array = new float[size.x, size.y];
        for (int r = 0; r < size.y; r++) for (int c = 0; c < size.x; c++) array[c, r] = vals(c, r);
    }

    public float this[int r, int c]
    {
        get => array[r, c];
        set => array[r, c] = value;
    }
    public float this[Int2 index]
    {
        get => this[index.x, index.y];
        set => this[index.x, index.y] = value;
    }

    public static Matrix Absolute(Matrix val) => new(val.Size, (r, c) => Mathf.Absolute(val[r, c]));
    public static Matrix Ceiling(Matrix val) => new(val.Size, (r, c) => Mathf.Ceiling(val[r, c]));
    public static Matrix Clamp(Matrix val, Matrix min, Matrix max) =>
        new(val.Size, (r, c) => Mathf.Clamp(val[r, c], min[r, c], max[r, c]));
    public static Matrix Divide(Matrix num, params Matrix[] vals)
    {
        foreach (Matrix m in vals) num /= m;
        return num;
    }
    public static Matrix Floor(Matrix val) => new(val.Size, (r, c) => Mathf.Floor(val[r, c]));
    public static Matrix Lerp(Matrix a, Matrix b, float t, bool clamp = true) =>
        new(a.Size, (r, c) => Mathf.Lerp(a[r, c], b[r, c], t, clamp));
    public static Matrix Product(params Matrix[] vals)
    {
        if (vals.Length < 1) throw new InvalidSizeException("Array must contain at least one matrix.");
        if (!CheckSize(vals)) throw new InvalidSizeException("All matricies must be the same size.");
        Matrix val = Identity(vals[0].Size);
        foreach (Matrix m in vals) val *= m;
        return val;
    }
    public static Matrix Round(Matrix val) => new(val.Size, (r, c) => Mathf.Round(val[r, c]));
    public static Matrix Subtract(Matrix num, params Matrix[] vals)
    {
        foreach (Matrix m in vals) num -= m;
        return num;
    }
    public static Matrix Sum(params Matrix[] vals)
    {
        if (!CheckSize(vals)) throw new InvalidSizeException("All matricies must be the same size.");
        Matrix val = Zero(vals[0].Size);
        foreach (Matrix m in vals) val += m;
        return val;
    }

    public void Apply(Modifier2D modifier)
    {
        for (int r = 0; r < Size.y; r++) for (int c = 0; c < Size.x; c++)
                array[r, c] = modifier(new(r, c), array[r, c]);
    }

    public float[] GetColumn(int column)
    {
        float[] vals = new float[Size.y];
        for (int i = 0; i < Size.y; i++) vals[i] = array[i, column];
        return vals;
    }
    public float[] GetRow(int row)
    {
        float[] vals = new float[Size.x];
        for (int i = 0; i < Size.x; i++) vals[i] = array[row, i];
        return vals;
    }

    public void SetColumn(int column, float[] vals)
    {
        if (vals.Length < Size.y)
            throw new InvalidSizeException("Array must contain enough values to fill the column.");
        for (int i = 0; i < Size.y; i++) array[i, column] = vals[i];
    }
    public void SetRow(int row, float[] vals)
    {
        if (vals.Length < Size.x)
            throw new InvalidSizeException("Array must contain enough values to fill the row.");
        for (int i = 0; i < Size.x; i++) array[row, i] = vals[i];
    }
    
    public Matrix Adjugate()
    {
        Matrix dets = new(Size);
        Matrix[,] minors = Minors();
        for (int r = 0; r < Size.y; r++) for (int c = 0; c < Size.x; c++) dets[c, r] = minors[c, r].Determinant();
        return dets ^ SignGrid(Size);
    }
    public float Determinant()
    {
        if (!IsSquare) throw new InvalidSizeException("Matrix must be square to calculate determinant.");
        if (Size.x <= 0 || Size.y <= 0) return 0;
        if (Size.x == 1 || Size.y == 1) return array[0, 0];

        Matrix[] minors = Minors().GetRow(0, Size.x);
        float det = 0;
        for (int i = 0; i < minors.Length; i++) det += minors[i].Determinant() * (i % 2 == 0 ? 1 : -1);

        return det;
    }
    public Matrix Inverse()
    {
        float d = Determinant();
        if (d == 0) throw new NoInverseException();
        return Transpose().Adjugate() / d;
    }
    public Matrix[,] Minors()
    {
        // This will absolutely blow my mind if it works.
        // Remember that whole "don't have a way to test" thing?

        if (!HasMinors) return new Matrix[0,0];

        Int2 newSize = Size - Int2.One;
        Matrix[,] array = new Matrix[Size.x, Size.y];
        for (int r1 = 0; r1 < Size.y; r1++) for (int c1 = 0; c1 < Size.x; c1++)
            {
                Matrix m = new(newSize);
                for (int r2 = 0; r2 < newSize.y; r2++) for (int c2 = 0; c2 < newSize.x; c2++)
                    {
                        int toSkip = c2 + r2 * newSize.y;
                        for (int r3 = 0; r3 < newSize.y; r3++) for (int c3 = 0; c3 < newSize.x; c3++)
                            {
                                if (r3 == r1 || c3 == c1) continue;
                                if (toSkip > 0)
                                {
                                    toSkip--;
                                    continue;
                                }
                                m[c2, r2] = this.array[c3, r3];
                                break;
                            }
                    }
                array[c1, r1] = m;
            }
        return array;
    }
    public Matrix Transpose()
    {
        Matrix m = new(new(Size.y, Size.x));
        for (int r = 0; r < Size.y; r++) m.SetColumn(r, GetRow(r));
        for (int c = 0; c < Size.x; c++) m.SetRow(c, GetColumn(c));
        return m;
    }

    public override bool Equals([NotNullWhen(true)] object? obj)
    {
        if (obj == null) return base.Equals(obj);
        Type t = obj.GetType();
        if (t == typeof(Matrix)) return Equals((Matrix)obj);
        else if (t == typeof(Matrix2x2)) return Equals((Matrix)obj);
        else if (t == typeof(Matrix3x3)) return Equals((Matrix)obj);
        else if (t == typeof(Matrix4x4)) return Equals((Matrix)obj);

        return base.Equals(obj);
    }
    public bool Equals(Matrix other) => array == other.array;
    public override int GetHashCode() => array.GetHashCode();
    public override string ToString() => ToString((string?)null);
    public string ToString(string? provider)
    {
        string res = "";
        for (int r = 0; r < Size.y; r++)
        {
            for (int c = 0; c < Size.x; c++) res += array[c, r].ToString(provider) + " ";
            res += "\n";
        }
        return res;
    }
    public string ToString(IFormatProvider provider)
    {
        string res = "";
        for (int r = 0; r < Size.y; r++)
        {
            for (int c = 0; c < Size.x; c++) res += array[c, r].ToString(provider) + " ";
            res += "\n";
        }
        return res;
    }

    public object Clone() => new Matrix(Size, array);

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    public IEnumerator<float> GetEnumerator()
    {
        for (int r = 0; r < Size.y; r++) for (int c = 0; c < Size.x; c++) yield return array[c, r];
    }

    public float[] ToArray() => array.Flatten(Size);
    public float[,] ToArray2D() => array;
    public Dictionary<Int2, float> ToDictionary()
    {
        Dictionary<Int2, float> dict = new();
        for (int r = 0; r < Size.y; r++) for (int c = 0; c < Size.x; c++) dict.Add(new(c, r), array[c, r]);
        return dict;
    }
    public Fill<float> ToFill() => ToFillExtension.ToFill(this);
    public Fill2D<float> ToFill2D()
    {
        Matrix @this = this;
        return (x, y) => @this[x, y];
    }
    public List<float> ToList() => ToArray().ToList();

    public static Matrix operator +(Matrix a, Matrix b) => new(a.Size, (r, c) => a[r, c] + b[r, c]);
    public static Matrix operator -(Matrix m) => m.Inverse();
    public static Matrix operator -(Matrix a, Matrix b) => new(a.Size, (r, c) => a[r, c] - b[r, c]);
    public static Matrix operator *(Matrix a, float b) => new(a.Size, (r, c) => a[r, c] * b);
    public static Matrix operator *(Matrix a, Matrix b) =>
        new(new(a.Size.y, b.Size.x), (r, c) => Mathf.Dot(a.GetRow(r), b.GetColumn(c)));
    public static Complex operator *(Matrix a, Complex b) => (Complex)(a * (Matrix)b);
    public static Quaternion operator *(Matrix a, Quaternion b) => (Quaternion)(a * (Matrix)b);
    public static Float2 operator *(Matrix a, Float2 b) => (Float2)(a * (Matrix)b);
    public static Float3 operator *(Matrix a, Float3 b) => (Float3)(a * (Matrix)b);
    public static Float4 operator *(Matrix a, Float4 b) => (Float4)(a * (Matrix)b);
    public static Vector2d operator *(Matrix a, Vector2d b) => (Vector2d)(a * (Matrix)b);
    public static Vector3d operator *(Matrix a, Vector3d b) => (Vector3d)(a * (Matrix)b);
    public static Matrix operator /(Matrix a, float b) => new(a.Size, (r, c) => a[r, c] / b);
    public static Matrix operator /(Matrix a, Matrix b) => a * b.Inverse();
    public static Complex operator /(Matrix a, Complex b) => (Complex)(a / (Matrix)b);
    public static Quaternion operator /(Matrix a, Quaternion b) => (Quaternion)(a / (Matrix)b);
    public static Float2 operator /(Matrix a, Float2 b) => (Float2)(a / (Matrix)b);
    public static Float3 operator /(Matrix a, Float3 b) => (Float3)(a / (Matrix)b);
    public static Float4 operator /(Matrix a, Float4 b) => (Float4)(a / (Matrix)b);
    public static Vector2d operator /(Matrix a, Vector2d b) => (Vector2d)(a / (Matrix)b);
    public static Vector3d operator /(Matrix a, Vector3d b) => (Vector3d)(a / (Matrix)b);
    // Single number multiplication
    public static Matrix operator ^(Matrix a, Matrix b) => new(a.Size, (r, c) => a[r, c] * b[r, c]);
    public static bool operator ==(Matrix a, Matrix b) => a.Equals(b);
    public static bool operator !=(Matrix a, Matrix b) => !a.Equals(b);

    public static explicit operator Matrix(Complex c) => (Matrix)(Float2)c;
    public static explicit operator Matrix(Quaternion c) => (Matrix)(Float4)c;
    public static explicit operator Matrix(Float2 f) => new(new(2, 1), i => f[i]);
    public static explicit operator Matrix(Float3 f) => new(new(3, 1), i => f[i]);
    public static explicit operator Matrix(Float4 f) => new(new(4, 1), i => f[i]);
    public static implicit operator Matrix(Matrix2x2 m) => new(new(2, 2), m.ToFill2D());
    public static implicit operator Matrix(Matrix3x3 m) => new(new(3, 3), m.ToFill2D());
    public static implicit operator Matrix(Matrix4x4 m) => new(new(4, 4), m.ToFill2D());
    public static explicit operator Matrix(Vector2d v) => (Matrix)v.ToXYZ();
    public static explicit operator Matrix(Vector3d v) => (Matrix)v.ToXYZ();

    private static bool CheckSize(params Matrix[] vals)
    {
        if (vals.Length <= 1) return true;
        Int2 size = vals[0].Size;
        for (int i = 1; i < vals.Length; i++) if (size != vals[i].Size) return false;

        return true;
    }
}
