using Nerd_STF.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Nerd_STF.Mathematics.Algebra
{
    public class Matrix : IMatrix<Matrix>,
                          ISubmatrixOperations<Matrix, Matrix>
    {
        public static Matrix Identity(int size) =>
            new Matrix((size, size), (int r, int c) => r == c ? 1 : 0);
        public static Matrix IdentityIsh(Int2 size) =>
            new Matrix(size, (int r, int c) => r == c ? 1 : 0);

        public static Matrix Empty => new Matrix();
        public static Matrix Zero(Int2 size) => new Matrix(size);

        public Int2 Size => size;

        private readonly double[] terms;
        private readonly Int2 size;

        public Matrix()
        {
            size = (0, 0);
            terms = TargetHelper.EmptyArray<double>();
        }
        public Matrix(Matrix copy)
        {
            size = copy.size;
            terms = new double[copy.terms.Length];
            Array.Copy(copy.terms, terms, copy.terms.Length);
        }
        public Matrix(Matrix2x2 copy)
        {
            size = (2, 2);
            terms = new double[]
            {
                copy.r0c0, copy.r0c1,
                copy.r1c0, copy.r1c1
            };
        }
        public Matrix(Matrix3x3 copy)
        {
            size = (3, 3);
            terms = new double[]
            {
                copy.r0c0, copy.r0c1, copy.r0c2,
                copy.r1c0, copy.r1c1, copy.r1c2,
                copy.r2c0, copy.r2c1, copy.r2c2
            };
        }
        public Matrix(Matrix4x4 copy)
        {
            size = (4, 4);
            terms = new double[]
            {
                copy.r0c0, copy.r0c1, copy.r0c2, copy.r0c3,
                copy.r1c0, copy.r1c1, copy.r1c2, copy.r1c3,
                copy.r2c0, copy.r2c1, copy.r2c2, copy.r2c3,
                copy.r3c0, copy.r3c1, copy.r3c2, copy.r3c3
            };
        }
        public Matrix(Int2 size)
        {
            this.size = size;
            terms = new double[size.x * size.y];
        }
        /// <param name="byRows"><see langword="true"/> if the array is of the form [c, r], <see langword="false"/> if the array is of the form [r, c].</param>
        public Matrix(Int2 size, double[,] terms, bool byRows = false)
        {
            this.size = size;
            this.terms = new double[size.x * size.y];
            for (int r = 0; r < size.x; r++)
            {
                for (int c = 0; c < size.y; c++)
                {
                    if (byRows) this.terms[FlattenIndex(r, c)] = terms[c, r];
                    else this.terms[FlattenIndex(r, c)] = terms[r, c];
                }
            }
        }
        /// <param name="byRows"><see langword="true"/> if the enumerable is a collection of rows (form [c, r]), <see langword="false"/> if the enumerable is a collection of columns (form [r, c]).</param>
        public Matrix(Int2 size, IEnumerable<IEnumerable<double>> vals, bool byRows = false)
        {
            this.size = size;
            terms = new double[size.x * size.y];
            MatrixHelper.SetMatrixValues(this, vals, byRows);
        }
        /// <param name="byRows"><see langword="true"/> if the enumerable is a collection of rows (form [c, r]), <see langword="false"/> if the enumerable is a collection of columns (form [r, c]).</param>
        public Matrix(Int2 size, IEnumerable<ListTuple<double>> vals, bool byRows = false)
        {
            this.size = size;
            terms = new double[size.x * size.y];
            MatrixHelper.SetMatrixValues(this, vals, byRows);
        }
        /// <param name="byRows"><see langword="true"/> if the fill is a collection of rows (form [c, r]), <see langword="false"/> if the fill is a collection of columns (form [r, c]).</param>
        public Matrix(Int2 size, Fill2d<double> fill, bool byRows = false)
        {
            this.size = size;
            terms = new double[size.x * size.y];
            for (int r = 0; r < size.x; r++)
            {
                for (int c = 0; c < size.y; c++)
                {
                    if (byRows) terms[FlattenIndex(r, c)] = fill(c, r);
                    else terms[FlattenIndex(r, c)] = fill(r, c);
                }
            }
        }
        private Matrix(Int2 size, double[] terms)
        {
            this.size = size;
            this.terms = terms;
        }

        public double this[int r, int c]
        {
            get => terms[FlattenIndex(r, c)];
            set => terms[FlattenIndex(r, c)] = value;
        }
        public double this[Int2 index]
        {
            get => terms[FlattenIndex(index.x, index.y)];
            set => terms[FlattenIndex(index.x, index.y)] = value;
        }
        public ListTuple<double> this[int index, RowColumn direction]
        {
            get
            {
                switch (direction)
                {
                    case RowColumn.Row: return GetRow(index);
                    case RowColumn.Column: return GetColumn(index);
                    default: throw new ArgumentException($"Invalid direction {direction}.");
                }
            }
            set
            {
                switch (direction)
                {
                    case RowColumn.Row: SetRow(index, value); break;
                    case RowColumn.Column: SetColumn(index, value); break;
                    default: throw new ArgumentException($"Invalid direction {direction}.");
                }
            }
        }

        public double TryGet(int r, int c)
        {
            if (r >= Size.x || c >= Size.y)
            {
                if (r == c) return 1;
                else return 0;
            }
            else return terms[FlattenIndex(r, c)];
        }

        public static Matrix FromMatrix<T>(T mat)
            where T : IMatrix<T> =>
            new Matrix(mat.Size, (int r, int c) => mat[r, c]);

        public static Matrix Average(IEnumerable<Matrix> vals)
        {
#if CS8_OR_GREATER
            Matrix? result = null;
#else
            Matrix result = null;
#endif
            int count = 0;
            foreach (Matrix m in vals)
            {
                if (result is null) result = m;
                else ThrowIfSizeDifferent(result, m);
                result += m;
                count++;
            }
            return result is null ? Empty : result / count;
        }
        public static Matrix Lerp(Matrix a, Matrix b, double t, bool clamp = true)
        {
            ThrowIfSizeDifferent(a, b);
            if (clamp) MathE.Clamp(ref t, 0, 1);
            double[] vals = new double[a.terms.Length];
            for (int i = 0; i < vals.Length; i++)
            {
                vals[i] = a.terms[i] + t * (b.terms[i] - a.terms[i]);
            }
            return new Matrix(a.size, vals);
        }
        public static Matrix Product(IEnumerable<Matrix> vals)
        {
#if CS8_OR_GREATER
            Matrix? result = null;
#else
            Matrix result = null;
#endif
            foreach (Matrix m in vals)
            {
                if (result is null) result = m;
                else ThrowIfSizeDifferent(result, m);
                result *= m;
            }
            return result ?? Empty;
        }
        public static Matrix Sum(IEnumerable<Matrix> vals)
        {
#if CS8_OR_GREATER
            Matrix? result = null;
#else
            Matrix result = null;
#endif
            foreach (Matrix m in vals)
            {
                if (result is null) result = m;
                else ThrowIfSizeDifferent(result, m);
                result += m;
            }
            return result ?? Empty;
        }

        public ListTuple<double> GetRow(int row)
        {
            double[] vals = new double[size.y];
            for (int c = 0; c < size.y; c++) vals[c] = terms[FlattenIndex(row, c)];
            return new ListTuple<double>(vals);
        }
        public ListTuple<double> GetColumn(int col)
        {
            double[] vals = new double[size.x];
            for (int r = 0; r < size.x; r++) vals[r] = terms[FlattenIndex(r, col)];
            return new ListTuple<double>(vals);
        }
        public void SetRow(int row, IEnumerable<double> vals) => MatrixHelper.SetRow(this, row, vals);
        public void SetColumn(int column, IEnumerable<double> vals) => MatrixHelper.SetColumn(this, column, vals);
        public void SetRow(int row, ListTuple<double> vals)
        {
            for (int c = 0; c < size.y; c++) terms[FlattenIndex(row, c)] = vals[c];
        }
        public void SetColumn(int col, ListTuple<double> vals)
        {
            for (int r = 0; r < size.x; r++) terms[FlattenIndex(r, col)] = vals[r];
        }

        public double Determinant()
        {
            ThrowIfNotSquare();

            if (size.x == 1) return terms[0];
            else if (size.x == 2) return terms[0] * terms[3] - terms[1] * terms[2];
            else
            {
                double sum = 0;
                for (int c = 0; c < size.y; c++)
                {
                    Matrix sub = Submatrix(0, c);
                    if (c % 2 == 0) sum += terms[FlattenIndex(0, c)] * sub.Determinant();
                    else sum -= terms[FlattenIndex(0, c)] * sub.Determinant();
                }
                return sum;
            }
        }

        public Matrix Adjoint() => Cofactor().Transpose();
        public Matrix Cofactor() =>
            new Matrix(size, (int r, int c) => Submatrix(r, c).Determinant() * ((r + c) % 2 == 0 ? 1 : -1));
        public Matrix Inverse() => Adjoint() / Determinant();
        public Matrix Transpose() =>
            new Matrix((size.y, size.x), (int r, int c) => terms[FlattenIndex(c, r)]);
        public Matrix Submatrix(int r, int c)
        {
            if (size.x <= 1 || size.y <= 1) throw new InvalidOperationException($"This matrix is too small to contain any sub-matrices.");
            Matrix smaller = new Matrix((size.x - 1, size.y - 1));

            bool pastR = false;
            for (int r2 = 0; r2 < size.x; r2++)
            {
                if (r2 == r)
                {
                    pastR = true;
                    continue;
                }

                bool pastC = false;
                for (int c2 = 0; c2 < size.y; c2++)
                {
                    if (c2 == c)
                    {
                        pastC = true;
                        continue;
                    }

                    int trueR = pastR ? r2 - 1 : r2,
                        trueC = pastC ? c2 - 1 : c2;
                    smaller.terms[smaller.FlattenIndex(trueR, trueC)] = terms[FlattenIndex(r2, c2)];
                }
            }
            return smaller;
        }
        public double Trace()
        {
            ThrowIfNotSquare();
            double sum = 0;
            for (int i = 0; i < size.x; i++) sum += terms[FlattenIndex(i, i)];
            return sum;
        }
        public double TraceIsh()
        {
            double sum = 0;
            for (int i = 0; i < MathE.Min(size.x, size.y); i++) sum += terms[FlattenIndex(i, i)];
            return sum;
        }

        public IEnumerator<double> GetEnumerator()
        {
            for (int i = 0; i < terms.Length; i++) yield return terms[i];
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

#if CS8_OR_GREATER
        public bool Equals(Matrix? other)
#else
        public bool Equals(Matrix other)
#endif
        {
            if (other is null) return false;
            else if (size != other.size) return false;

            for (int i = 0; i < terms.Length; i++)
                if (terms[i] != other.terms[i]) return false;
            return true;
        }
#if CS8_OR_GREATER
        public override bool Equals(object? other)
#else
        public override bool Equals(object other)
#endif
        {
            if (other is null) return false;
            else if (other is Matrix otherMat) return Equals(otherMat);
            else return false;
        }
        public override int GetHashCode()
        {
            int total = 0;
            for (int i = 0; i < terms.Length; i++)
            {
                total ^= terms[i].GetHashCode() & i.GetHashCode();
            }
            return total;
        }
        public override string ToString() => ToStringHelper.MatrixToString(this, null);
#if CS8_OR_GREATER
        public string ToString(string? format) => ToStringHelper.MatrixToString(this, format);
        public string ToString(string? format, IFormatProvider? provider) => ToStringHelper.MatrixToString(this, format);
#else
        public string ToString(string format) => ToStringHelper.MatrixToString(this, format);
        public string ToString(string format, IFormatProvider provider) => ToStringHelper.MatrixToString(this, format);
#endif

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private int FlattenIndex(int r, int c) => r * size.y + c;

        private void ThrowIfNotSquare()
        {
            if (size.x != size.y) throw new InvalidOperationException("This operation only applies to a square matrix.");
        }
        private static void ThrowIfSizeDifferent(Matrix a, Matrix b)
        {
            if (a.size != b.size) throw new InvalidOperationException("This operation only applies to matrices with the same dimensions.");
        }

        public static Matrix operator +(Matrix a) => new Matrix(a);
        public static Matrix operator +(Matrix a, Matrix b)
        {
            ThrowIfSizeDifferent(a, b);
            double[] terms = new double[a.terms.Length];
            for (int i = 0; i < a.terms.Length; i++) terms[i] = a.terms[i] + b.terms[i];
            return new Matrix(a.size, terms);
        }
        public static Matrix operator -(Matrix a)
        {
            double[] terms = new double[a.terms.Length];
            for (int i = 0; i < a.terms.Length; i++) terms[i] = -a.terms[i];
            return new Matrix(a.size, terms);
        }
        public static Matrix operator -(Matrix a, Matrix b)
        {
            ThrowIfSizeDifferent(a, b);
            double[] terms = new double[a.terms.Length];
            for (int i = 0; i < a.terms.Length; i++) terms[i] = a.terms[i] - b.terms[i];
            return new Matrix(a.size, terms);
        }
        public static Matrix operator *(Matrix a, double b)
        {
            double[] terms = new double[a.terms.Length];
            for (int i = 0; i < a.terms.Length; i++) terms[i] = a.terms[i] * b;
            return new Matrix(a.size, terms);
        }
        public static Matrix operator *(Matrix a, Matrix b)
        {
            if (a.size.y != b.size.x) throw new InvalidOperationException("The dimensions of these matrices are incompatible with one another.");
            return new Matrix((a.size.x, b.size.y), (int r, int c) => MathE.Dot(a.GetRow(r), b.GetColumn(c)));
        }
        public static Matrix operator /(Matrix a, double b)
        {
            double[] terms = new double[a.terms.Length];
            for (int i = 0; i < a.terms.Length; i++) terms[i] = a.terms[i] / b;
            return new Matrix(a.size, terms);
        }
        public static Matrix operator ^(Matrix a, Matrix b)
        {
            ThrowIfSizeDifferent(a, b);
            double[] terms = new double[a.terms.Length];
            for (int i = 0; i < a.terms.Length; i++) terms[i] = a.terms[i] * b.terms[i];
            return new Matrix(a.size, terms);
        }
        public static Matrix operator ~(Matrix a) => a.Inverse();
        public static bool operator ==(Matrix a, Matrix b) => a.Equals(b);
        public static bool operator !=(Matrix a, Matrix b) => !a.Equals(b);

        public static implicit operator Matrix(Float2 vec) => new Matrix((2, 1), new double[] { vec.x, vec.y });
        public static implicit operator Matrix(Float3 vec) => new Matrix((3, 1), new double[] { vec.x, vec.y, vec.z });
        public static implicit operator Matrix(Float4 vec) => new Matrix((4, 1), new double[] { vec.x, vec.y, vec.z, vec.w });
        public static implicit operator Matrix(Matrix2x2 mat) => new Matrix((2, 2), new double[]
        {
            mat.r0c0, mat.r0c1,
            mat.r1c0, mat.r1c1
        });
        public static implicit operator Matrix(Matrix3x3 mat) => new Matrix((3, 3), new double[]
        {
            mat.r0c0, mat.r0c1, mat.r0c2,
            mat.r1c0, mat.r1c1, mat.r1c2,
            mat.r2c0, mat.r2c1, mat.r2c2
        });
        public static implicit operator Matrix(Matrix4x4 mat) => new Matrix((4, 4), new double[]
        {
            mat.r0c0, mat.r0c1, mat.r0c2, mat.r0c3,
            mat.r1c0, mat.r1c1, mat.r1c2, mat.r1c3,
            mat.r2c0, mat.r2c1, mat.r2c2, mat.r2c3,
            mat.r3c0, mat.r3c1, mat.r3c2, mat.r3c3
        });
    }
}
