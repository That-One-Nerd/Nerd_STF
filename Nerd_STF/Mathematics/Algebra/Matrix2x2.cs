using Nerd_STF.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Nerd_STF.Mathematics.Algebra
{
    public class Matrix2x2 : IMatrix<Matrix2x2>,
                             ISquareMatrix<Matrix2x2>
#if CS11_OR_GREATER
                            ,ISplittable<Matrix2x2, (double[] r0c0, double[] r0c1, double[] r1c0, double[] r1c1)>,
                             IStaticMatrix<Matrix2x2>
#endif
    {
        public static Matrix2x2 Identity =>
            new Matrix2x2(1, 0,
                          0, 1);
        public static Matrix2x2 SignField =>
            new Matrix2x2(+1, -1,
                          -1, +1);

        public static Matrix2x2 One => new Matrix2x2(1, 1, 1, 1);
        public static Matrix2x2 Zero => new Matrix2x2(0, 0, 0, 0);

        public Int2 Size => (2, 2);

        public double r0c0, r0c1,
                      r1c0, r1c1;

        public Matrix2x2()
        {
            r0c0 = 0; r0c1 = 0;
            r1c0 = 0; r1c1 = 0;
        }
        public Matrix2x2(Matrix2x2 copy)
        {
            r0c0 = copy.r0c0; r0c1 = copy.r0c1;
            r1c0 = copy.r1c0; r1c1 = copy.r1c1;
        }
        public Matrix2x2(double r0c0, double r0c1, double r1c0, double r1c1)
        {
            this.r0c0 = r0c0;
            this.r0c1 = r0c1;
            this.r1c0 = r1c0;
            this.r1c1 = r1c1;
        }
        /// <param name="byRows"><see langword="true"/> if the array is of the form [c, r], <see langword="false"/> if the array is of the form [r, c].</param>
        public Matrix2x2(double[,] vals, bool byRows = false)
        {
            if (byRows) // Collection of rows ([c, r])
            {
                r0c0 = vals[0, 0]; r0c1 = vals[1, 0];
                r1c0 = vals[0, 1]; r1c1 = vals[1, 1];
            }
            else        // Collection of columns ([r, c])
            {
                r0c0 = vals[0, 0]; r0c1 = vals[0, 1];
                r1c0 = vals[1, 0]; r1c1 = vals[1, 1];
            }
        }
        /// <param name="byRows"><see langword="true"/> if the enumerable is a collection of rows (form [c, r]), <see langword="false"/> if the enumerable is a collection of columns (form [r, c]).</param>
        public Matrix2x2(IEnumerable<IEnumerable<double>> vals, bool byRows = false)
        {
            MatrixHelper.SetMatrixValues(this, vals, byRows);
        }
        /// <param name="byRows"><see langword="true"/> if the enumerable is a collection of rows (form [c, r]), <see langword="false"/> if the enumerable is a collection of columns (form [r, c]).</param>
        public Matrix2x2(IEnumerable<ListTuple<double>> vals, bool byRows = false)
        {
            MatrixHelper.SetMatrixValues(this, vals, byRows);
        }
        /// <param name="byRows"><see langword="true"/> if the fill goes through columns for each row, <see langword="false"/> if the fill goes through rows for each column.</param>
        public Matrix2x2(Fill<double> fill, bool byRows = false)
        {
            if (byRows)
            {
                r0c0 = fill(0); r0c1 = fill(2);
                r1c0 = fill(1); r1c1 = fill(3);
            }
            else
            {
                r0c0 = fill(0); r0c1 = fill(1);
                r1c0 = fill(2); r1c1 = fill(3);
            }
        }
        /// <param name="byRows"><see langword="true"/> if the fill is a collection of rows (form [c, r]), <see langword="false"/> if the fill is a collection of columns (form [r, c]).</param>
        public Matrix2x2(Fill2d<double> fill, bool byRows = false)
        {
            if (byRows)
            {
                r0c0 = fill(0, 0); r0c1 = fill(1, 0);
                r1c0 = fill(0, 1); r1c1 = fill(1, 1);
            }
            else
            {
                r0c0 = fill(0, 0); r0c1 = fill(0, 1);
                r1c0 = fill(1, 0); r1c1 = fill(1, 1);
            }
        }

        public double this[int r, int c]
        {
            get
            {
                switch (r)
                {
                    case 0:
                        switch (c)
                        {
                            case 0: return r0c0;
                            case 1: return r0c1;
                            default: throw new ArgumentOutOfRangeException(nameof(c));
                        }
                    case 1:
                        switch (c)
                        {
                            case 0: return r1c0;
                            case 1: return r1c1;
                            default: throw new ArgumentOutOfRangeException(nameof(c));
                        }
                    default: throw new ArgumentOutOfRangeException(nameof(r));
                }
            }
            set
            {
                switch (r)
                {
                    case 0:
                        switch (c)
                        {
                            case 0: r0c0 = value; return;
                            case 1: r0c1 = value; return;
                            default: throw new ArgumentOutOfRangeException(nameof(c));
                        }
                    case 1:
                        switch (c)
                        {
                            case 0: r1c0 = value; return;
                            case 1: r1c1 = value; return;
                            default: throw new ArgumentOutOfRangeException(nameof(c));
                        }
                    default: throw new ArgumentOutOfRangeException(nameof(r));
                }
            }
        }
        public double this[Int2 index]
        {
            get => this[index.x, index.y];
            set => this[index.x, index.y] = value;
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

        public static Matrix2x2 Average(IEnumerable<Matrix2x2> vals)
        {
            Matrix2x2 sum = Zero;
            int count = 0;
            foreach (Matrix2x2 m in vals)
            {
                sum += m;
                count++;
            }
            return sum / count;
        }
        public static Matrix2x2 Lerp(Matrix2x2 a, Matrix2x2 b, double t, bool clamp = true) =>
            new Matrix2x2(MathE.Lerp(a.r0c0, b.r0c0, t, clamp), MathE.Lerp(a.r0c1, b.r0c1, t, clamp),
                          MathE.Lerp(a.r1c0, b.r1c0, t, clamp), MathE.Lerp(a.r1c1, b.r1c1, t, clamp));
        public static Matrix2x2 Product(IEnumerable<Matrix2x2> vals)
        {
            bool any = false;
            Matrix2x2 result = One;
            foreach (Matrix2x2 m in vals)
            {
                any = true;
                result *= m;
            }
            return any ? result : Zero;
        }
        public static Matrix2x2 Sum(IEnumerable<Matrix2x2> vals)
        {
            Matrix2x2 result = Zero;
            foreach (Matrix2x2 m in vals) result += m;
            return result;
        }

        public static (double[] r0c0, double[] r0c1, double[] r1c0, double[] r1c1) SplitArray(IEnumerable<Matrix2x2> vals)
        {
            int count = vals.Count();
            double[] r0c0 = new double[count], r0c1 = new double[count],
                     r1c0 = new double[count], r1c1 = new double[count];
            int index = 0;
            foreach (Matrix2x2 m in vals)
            {
                r0c0[index] = m.r0c0; r0c1[index] = m.r0c1;
                r1c0[index] = m.r1c0; r1c1[index] = m.r1c1;
            }
            return (r0c0, r0c1,
                    r1c0, r1c1);
        }

        public ListTuple<double> GetRow(int row)
        {
            double[] vals;
            switch (row)
            {
                case 0: vals = new double[] { r0c0, r0c1 }; break;
                case 1: vals = new double[] { r1c0, r1c1 }; break;
                default: throw new ArgumentOutOfRangeException(nameof(row));
            }
            return new ListTuple<double>(vals);
        }
        public ListTuple<double> GetColumn(int column)
        {
            double[] vals;
            switch (column)
            {
                case 0: vals = new double[] { r0c0, r1c0 }; break;
                case 1: vals = new double[] { r0c1, r1c1 }; break;
                default: throw new ArgumentOutOfRangeException(nameof(column));
            }
            return new ListTuple<double>(vals);
        }
        public void SetRow(int row, IEnumerable<double> vals) => MatrixHelper.SetRow(this, row, vals);
        public void SetColumn(int column, IEnumerable<double> vals) => MatrixHelper.SetColumn(this, column, vals);
        public void SetRow(int row, ListTuple<double> vals)
        {
            switch (row)
            {
                case 0: r0c0 = vals[0]; r0c1 = vals[1]; break;
                case 1: r1c0 = vals[0]; r1c1 = vals[1]; break;
                default: throw new ArgumentOutOfRangeException(nameof(row));
            }
        }
        public void SetColumn(int column, ListTuple<double> vals)
        {
            switch (column)
            {
                case 0: r0c0 = vals[0]; r1c0 = vals[1]; break;
                case 1: r0c1 = vals[0]; r1c1 = vals[1]; break;
                default: throw new ArgumentOutOfRangeException(nameof(column));
            }
        }

        public double Determinant() => r0c0 * r1c1 - r0c1 * r1c0;

        public Matrix2x2 Adjoint() =>
            new Matrix2x2( r1c1, -r0c1,
                          -r1c0,  r0c0);
        public Matrix2x2 Cofactor() =>
            new Matrix2x2( r1c1, -r1c0,
                          -r0c1,  r0c0);
        public Matrix2x2 Inverse()
        {
            double invDet = 1 / Determinant();
            return new Matrix2x2( r1c1 * invDet, -r0c1 * invDet,
                                 -r1c0 * invDet,  r0c0 * invDet);
        }
        public Matrix2x2 Transpose() =>
            new Matrix2x2(r0c0, r1c0,
                          r0c1, r1c1);
        public double Trace() => r0c0 + r1c1;

        public IEnumerator<double> GetEnumerator()
        {
            yield return r0c0; yield return r0c1;
            yield return r1c0; yield return r1c1;
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

#if CS8_OR_GREATER
        public bool Equals(Matrix2x2? other) =>
#else
        public bool Equals(Matrix2x2 other) =>
#endif
            !(other is null) &&
            r0c0 == other.r0c0 && r0c1 == other.r0c1 &&
            r1c0 == other.r1c0 && r1c1 == other.r1c1;
#if CS8_OR_GREATER
        public override bool Equals(object? other)
#else
        public override bool Equals(object other)
#endif
        {
            if (other is null) return false;
            else if (other is Matrix2x2 otherMat) return Equals(otherMat);
            else return false;
        }
        public override int GetHashCode() =>
            (int)((uint)r0c0.GetHashCode() & 0xFF000000 |
                  (uint)r0c1.GetHashCode() & 0x00FF0000 |
                  (uint)r1c0.GetHashCode() & 0x0000FF00 |
                  (uint)r1c1.GetHashCode() & 0x000000FF);
        public override string ToString() => ToStringHelper.MatrixToString(this, null);
#if CS8_OR_GREATER
        public string ToString(string? format) => ToStringHelper.MatrixToString(this, format);
        public string ToString(string? format, IFormatProvider? provider) => ToStringHelper.MatrixToString(this, format);
#else
        public string ToString(string format) => ToStringHelper.MatrixToString(this, format);
        public string ToString(string format, IFormatProvider provider) => ToStringHelper.MatrixToString(this, format);
#endif

        public static Matrix2x2 operator +(Matrix2x2 a) =>
            new Matrix2x2(a.r0c0, a.r0c1,
                          a.r1c0, a.r1c1);
        public static Matrix2x2 operator +(Matrix2x2 a, Matrix2x2 b) =>
            new Matrix2x2(a.r0c0 + b.r0c0, a.r0c1 + b.r0c1,
                          a.r1c0 + b.r1c0, a.r1c1 + b.r1c1);
        public static Matrix2x2 operator -(Matrix2x2 a) =>
            new Matrix2x2(-a.r0c0, -a.r0c1,
                          -a.r1c0, -a.r1c1);
        public static Matrix2x2 operator -(Matrix2x2 a, Matrix2x2 b) =>
            new Matrix2x2(a.r0c0 - b.r0c0, a.r0c1 - b.r0c1,
                          a.r1c0 - b.r1c0, a.r1c1 - b.r1c1);
        public static Matrix2x2 operator *(Matrix2x2 a, double b) =>
            new Matrix2x2(a.r0c0 * b, a.r0c1 * b,
                          a.r1c0 * b, a.r1c1 * b);
        public static Float2 operator *(Matrix2x2 a, Float2 b) =>
            new Float2(a.r0c0 * b.x + a.r0c1 * b.y,
                       a.r1c0 * b.x + a.r1c1 * b.y);
        public static Matrix2x2 operator *(Matrix2x2 a, Matrix2x2 b) =>
            new Matrix2x2(a.r0c0 * b.r0c0 + a.r0c1 * b.r1c0, a.r0c0 * b.r0c1 + a.r0c1 * b.r1c1,
                          a.r1c0 * b.r0c0 + a.r1c1 * b.r1c0, a.r1c0 * b.r0c1 + a.r1c1 * b.r1c1);
        public static Matrix2x2 operator /(Matrix2x2 a, double b) =>
            new Matrix2x2(a.r0c0 / b, a.r0c1 / b,
                          a.r1c0 / b, a.r1c1 / b);
        public static Matrix2x2 operator ^(Matrix2x2 a, Matrix2x2 b) =>
            new Matrix2x2(a.r0c0 * b.r0c0, a.r0c1 * b.r0c1,
                          a.r1c0 * b.r1c0, a.r1c1 * b.r1c1);
        public static Matrix2x2 operator ~(Matrix2x2 a) => a.Inverse();
        public static bool operator ==(Matrix2x2 a, Matrix2x2 b) => a.Equals(b);
        public static bool operator !=(Matrix2x2 a, Matrix2x2 b) => !a.Equals(b);

        public static explicit operator Matrix2x2(Matrix3x3 mat) =>
            new Matrix2x2(mat.r0c0, mat.r0c1,
                          mat.r1c0, mat.r1c1);
        public static explicit operator Matrix2x2(Matrix4x4 mat) =>
            new Matrix2x2(mat.r0c0, mat.r0c1,
                          mat.r1c0, mat.r1c1);
    }
}
