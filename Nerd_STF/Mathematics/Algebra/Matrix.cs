using Nerd_STF.Mathematics.Algebra.Abstract;

namespace Nerd_STF.Mathematics.Algebra;

public class Matrix : IMatrix<Matrix, Matrix>
{
    public static Matrix Identity(Int2 size)
    {
        if (size.x != size.y) throw new InvalidSizeException("Can only create an identity matrix of a square matrix." +
            " You may want to use " + nameof(IdentityIsh) + " instead.");
        Matrix m = Zero(size);
        for (int i = 0; i < size.x; i++) m[i, i] = 1;
        return m;
    }
    public static Matrix IdentityIsh(Int2 size)
    {
        Matrix m = Zero(size);
        int max = Mathf.Min(size.x, size.y);
        for (int i = 0; i < max; i++) m[i, i] = 1;
        return m;
    }
    public static Matrix One(Int2 size) => new(size, 1);
    public static Matrix SignGrid(Int2 size) => new(size, delegate (int x)
    {
        float sgnValue = Fills.SignFill(x);
        if (size.y % 2 == 0 && x / size.y % 2 == 1) sgnValue *= -1;
        return sgnValue;
    });
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
        for (int r = 0; r < size.x; r++) for (int c = 0; c < size.y; c++) array[r, c] = all;
    }
    public Matrix(Int2 size, float[] vals)
    {
        Size = size;
        array = new float[size.x, size.y];

        if (vals.Length < size.x * size.y)
            throw new InvalidSizeException("Array must contain enough values to fill the matrix.");

        for (int r = 0; r < size.x; r++) for (int c = 0; c < size.y; c++) array[r, c] = vals[c + r * size.y];
    }
    public Matrix(Int2 size, int[] vals)
    {
        Size = size;
        array = new float[size.x, size.y];

        if (vals.Length < size.x * size.y)
            throw new InvalidSizeException("Array must contain enough values to fill the matrix.");

        for (int r = 0; r < size.x; r++) for (int c = 0; c < size.y; c++) array[r, c] = vals[c + r * size.y];
    }
    public Matrix(Int2 size, Fill<float> vals)
    {
        Size = size;
        array = new float[size.x, size.y];

        for (int r = 0; r < size.x; r++) for (int c = 0; c < size.y; c++) array[r, c] = vals(c + r * size.y);
    }
    public Matrix(Int2 size, Fill<int> vals)
    {
        Size = size;
        array = new float[size.x, size.y];

        for (int r = 0; r < size.x; r++) for (int c = 0; c < size.y; c++) array[r, c] = vals(c + r * size.y);
    }
    public Matrix(Int2 size, float[,] vals)
    {
        Size = size;
        array = new float[size.x, size.y];
        for (int r = 0; r < size.x; r++) for (int c = 0; c < size.y; c++) array[r, c] = vals[r, c];
    }
    public Matrix(Int2 size, int[,] vals)
    {
        Size = size;
        array = new float[size.x, size.y];
        for (int r = 0; r < size.x; r++) for (int c = 0; c < size.y; c++) array[r, c] = vals[r, c];
    }
    public Matrix(Int2 size, Fill2d<float> vals)
    {
        Size = size;
        array = new float[size.x, size.y];
        for (int r = 0; r < size.x; r++) for (int c = 0; c < size.y; c++) array[r, c] = vals(r, c);
    }
    public Matrix(Int2 size, Fill2d<int> vals)
    {
        Size = size;
        array = new float[size.x, size.y];
        for (int r = 0; r < size.x; r++) for (int c = 0; c < size.y; c++) array[r, c] = vals(r, c);
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
    public float this[Index r, Index c]
    {
        get
        {
            int row = r.IsFromEnd ? Size.x - r.Value : r.Value,
                col = c.IsFromEnd ? Size.y - c.Value : c.Value;
            return array[row, col];
        }
        set
        {
            int row = r.IsFromEnd ? Size.x - r.Value : r.Value,
                col = c.IsFromEnd ? Size.y - c.Value : c.Value;
            array[row, col] = value;
        }
    }
    public Matrix this[Range rs, Range cs]
    {
        get
        {
            int rowStart = rs.Start.IsFromEnd ? Size.x - rs.Start.Value : rs.Start.Value,
                rowEnd = rs.End.IsFromEnd ? Size.x - rs.End.Value : rs.End.Value,
                colStart = cs.Start.IsFromEnd ? Size.y - cs.Start.Value : cs.Start.Value,
                colEnd = cs.End.IsFromEnd ? Size.y - cs.End.Value : cs.End.Value;

            Matrix newMatrix = new((rowEnd - rowStart - 1, colEnd - colStart - 1));
            for (int r = rowStart; r < rowEnd; r++)
                for (int c = colStart; c < colEnd; c++)
                    newMatrix[r, c] = array[r, c];
            return newMatrix;
        }
        set
        {
            int rowStart = rs.Start.IsFromEnd ? Size.x - rs.Start.Value : rs.Start.Value,
                rowEnd = rs.End.IsFromEnd ? Size.x - rs.End.Value : rs.End.Value,
                colStart = cs.Start.IsFromEnd ? Size.y - cs.Start.Value : cs.Start.Value,
                colEnd = cs.End.IsFromEnd ? Size.y - cs.End.Value : cs.End.Value;

            if (value.Size != (rowEnd - rowStart - 1, colEnd - colStart - 1))
                throw new InvalidSizeException("Matrix has invalid size.");

            for (int r = rowStart; r < rowEnd; r++)
                for (int c = colStart; c < colEnd; c++)
                    array[r, c] = value[r, c];
        }
    }

    public static Matrix Get2dRotationMatrix(Angle rot) =>
        Matrix2x2.GenerateRotationMatrix(rot);
    public static Matrix Get3dRotationMatrix(Angle yaw, Angle pitch, Angle roll) =>
        Matrix3x3.GenerateRotationMatrix(yaw, pitch, roll);

    public static Matrix Absolute(Matrix val) => new(val.Size, (r, c) => Mathf.Absolute(val[r, c]));
    public static Matrix Ceiling(Matrix val) => new(val.Size, (r, c) => Mathf.Ceiling(val[r, c]));
    public static Matrix Clamp(Matrix val, Matrix min, Matrix max) =>
        new(val.Size, (r, c) => Mathf.Clamp(val[r, c], min[r, c], max[r, c]));
    public static Matrix Floor(Matrix val) => new(val.Size, (r, c) => Mathf.Floor(val[r, c]));
    public static Matrix Lerp(Matrix a, Matrix b, float t, bool clamp = true) =>
        new(a.Size, (r, c) => Mathf.Lerp(a[r, c], b[r, c], t, clamp));
    public static Matrix Round(Matrix val) => new(val.Size, (r, c) => Mathf.Round(val[r, c]));

    public void Apply(Modifier2d modifier)
    {
        for (int r = 0; r < Size.x; r++) for (int c = 0; c < Size.y; c++)
                array[r, c] = modifier(new(r, c), array[r, c]);
    }

    public float[] GetColumn(int column)
    {
        float[] vals = new float[Size.x];
        for (int i = 0; i < Size.x; i++) vals[i] = array[i, column];
        return vals;
    }
    public float[] GetRow(int row)
    {
        float[] vals = new float[Size.y];
        for (int i = 0; i < Size.y; i++) vals[i] = array[row, i];
        return vals;
    }

    public void SetColumn(int column, float[] vals)
    {
        if (vals.Length < Size.x)
            throw new InvalidSizeException("Array must contain enough values to fill the column.");
        for (int i = 0; i < Size.x; i++) array[i, column] = vals[i];
    }
    public void SetRow(int row, float[] vals)
    {
        if (vals.Length < Size.y)
            throw new InvalidSizeException("Array must contain enough values to fill the row.");
        for (int i = 0; i < Size.y; i++) array[row, i] = vals[i];
    }

    public Matrix Adjugate() => Cofactor().Transpose();
    public Matrix Cofactor()
    {
        Matrix dets = new(Size);
        Matrix[,] minors = Minors();
        for (int r = 0; r < Size.y; r++) for (int c = 0; c < Size.x; c++) dets[c, r] = minors[c, r].Determinant();
        return dets ^ SignGrid(Size);
    }
    public float Determinant()
    {
        if (!IsSquare) throw new InvalidSizeException("Matrix must be square to calculate determinant.");
        if (Size.x <= 0) return 0;
        if (Size.x == 1) return array[0, 0];

        Matrix[] minors = Minors().GetRow(0, Size.x);
        float det = 0;
        for (int i = 0; i < minors.Length; i++) det += array[0, i] * minors[i].Determinant() * (i % 2 == 0 ? 1 : -1);

        return det;
    }
    public Matrix? Inverse()
    {
        float d = Determinant();
        if (d == 0) return null;

        return Adjugate() / d;
    }
    public Matrix[,] Minors()
    {
        if (!HasMinors) return new Matrix[0,0];
        Matrix[,] minors = new Matrix[Size.x, Size.y];
        for (int r = 0; r < Size.x; r++) for (int c = 0; c < Size.y; c++) minors[r, c] = MinorOf(new(r, c));
        return minors;
    }
    public Matrix Transpose()
    {
        Matrix @this = this;
        return new(Size, (r, c) => @this[c, r]);
    }

    public Matrix MinorOf(Int2 index)
    {
        Matrix @this = this;
        return new(@this.Size - Int2.One, delegate (int r, int c)
        {
            if (r >= index.x) r++;
            if (c >= index.y) c++;
            return @this[r, c];
        });
    }

    public Matrix AddRow(int rowToChange, int referenceRow, float factor = 1)
    {
        Matrix @this = this;
        return new(Size, delegate (int r, int c)
        {
            if (r == rowToChange) return @this[r, c] += factor * @this[referenceRow, c];
            else return @this[r, c];
        });
    }
    public void AddRowMutable(int rowToChange, int referenceRow, float factor)
    {
        for (int c = 0; c < Size.y; c++) this[rowToChange, c] += this[referenceRow, c] * factor;
    }
    public Matrix ScaleRow(int rowIndex, float factor)
    {
        Matrix @this = this;
        return new(Size, delegate (int r, int c)
        {
            if (r == rowIndex) return @this[r, c] * factor;
            else return @this[r, c];
        });
    }
    public void ScaleRowMutable(int rowIndex, float factor)
    {
        for (int c = 0; c < Size.y; c++) this[rowIndex, c] *= factor;
    }
    public Matrix SwapRows(int rowA, int rowB)
    {
        Matrix @this = this;
        return new(Size, delegate (int r, int c)
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

    public Matrix SolveRowEchelon()
    {
        Matrix result = (Matrix)MemberwiseClone();

        // Scale the first row so the first element of that row is 1.
        result.ScaleRowMutable(0, 1 / result[0, 0]);
        
        // For each row afterwards, subtract the required amount from all rows before it and normalize.
        for (int r1 = 1; r1 < result.Size.x; r1++)
        {
            int min = Mathf.Min(r1, result.Size.y);
            for (int r2 = 0; r2 < min; r2++)
            {
                result.AddRowMutable(r1, r2, -result[r1, r2]);
            }
            result.ScaleRowMutable(r1, 1 / result[r1, r1]);
        }

        return result;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return base.Equals(obj);
        else if (obj is Matrix m) return Equals(m);
        else if (obj is Matrix2x2 m2x2) return Equals(m2x2);
        else if (obj is Matrix3x3 m3x3) return Equals(m3x3);
        else if (obj is Matrix4x4 m4x4) return Equals(m4x4);
        return false;
    }
    public bool Equals(Matrix? other)
    {
        if (other is null) return false;
        return array.Equals(other.array);
    }
    public override int GetHashCode() => array.GetHashCode();
    public override string ToString()
    {
        string res = "";
        for (int r = 0; r < Size.x; r++)
        {
            for (int c = 0; c < Size.y; c++) res += array[r, c] + " ";
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

    public float[] ToArray() => array.Flatten(new(Size.y, Size.x));
    public float[,] ToArray2D() => array;
    public Fill<float> ToFill() => ToFillExtension.ToFill(this);
    public Fill2d<float> ToFill2D()
    {
        Matrix @this = this;
        return (x, y) => @this[x, y];
    }
    public List<float> ToList() => ToArray().ToList();

    public static Matrix operator +(Matrix a, Matrix b) => new(a.Size, (r, c) => a[r, c] + b[r, c]);
    public static Matrix? operator -(Matrix m) => m.Inverse();
    public static Matrix operator -(Matrix a, Matrix b) => new(a.Size, (r, c) => a[r, c] - b[r, c]);
    public static Matrix operator *(Matrix a, float b) => new(a.Size, (r, c) => a[r, c] * b);
    public static Matrix operator *(Matrix a, Matrix b)
    {
        if (a.Size.y != b.Size.x) throw new InvalidSizeException("Incompatible Dimensions.");
        return new(new(a.Size.x, b.Size.y), (r, c) => Mathf.Dot(a.GetRow(r), b.GetColumn(c)));
    }
    public static Complex operator *(Matrix a, Complex b) => (Complex)(a * (Matrix)b);
    public static Quaternion operator *(Matrix a, Quaternion b) => (Quaternion)(a * (Matrix)b);
    public static Float2 operator *(Matrix a, Float2 b) => (Float2)(a * (Matrix)b);
    public static Float3 operator *(Matrix a, Float3 b) => (Float3)(a * (Matrix)b);
    public static Float4 operator *(Matrix a, Float4 b) => (Float4)(a * (Matrix)b);
    public static Vector2d operator *(Matrix a, Vector2d b) => (Vector2d)(a * (Matrix)b);
    public static Vector3d operator *(Matrix a, Vector3d b) => (Vector3d)(a * (Matrix)b);
    public static Matrix operator /(Matrix a, float b) => new(a.Size, (r, c) => a[r, c] / b);
    public static Matrix operator /(Matrix a, Matrix b)
    {
        Matrix? bInv = b.Inverse();
        if (bInv is null) throw new NoInverseException(b);
        return a * bInv;
    }
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
