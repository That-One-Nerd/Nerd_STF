using Nerd_STF.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Nerd_STF.Mathematics.Algebra
{
    public class Matrix3x3 : IMatrix<Matrix3x3>,
                             ISquareMatrix<Matrix3x3>,
                             ISubmatrixOperations<Matrix3x3, Matrix2x2>
#if CS11_OR_GREATER
                            ,ISplittable<Matrix3x3, (double[] r0c0, double[] r0c1, double[] r0c2, double[] r1c0, double[] r1c1, double[] r1c2, double[] r2c0, double[] r2c1, double[] r2c2)>,
                             IStaticMatrix<Matrix3x3>
#endif
    {
        public static Matrix3x3 Identity =>
            new Matrix3x3(1, 0, 0,
                          0, 1, 0,
                          0, 0, 1);
        public static Matrix3x3 SignField =>
            new Matrix3x3(+1, -1, +1,
                          -1, +1, -1,
                          +1, -1, +1);

        public static Matrix3x3 One => new Matrix3x3(1, 1, 1, 1, 1, 1, 1, 1, 1);
        public static Matrix3x3 Zero => new Matrix3x3(0, 0, 0, 0, 0, 0, 0, 0, 0);

        public Int2 Size => (3, 3);

        public double r0c0, r0c1, r0c2,
                      r1c0, r1c1, r1c2,
                      r2c0, r2c1, r2c2;

        public Matrix3x3()
        {
            r0c0 = 0; r0c1 = 0; r0c2 = 0;
            r1c0 = 0; r1c1 = 0; r1c2 = 0;
            r2c0 = 0; r2c1 = 0; r2c2 = 0;
        }
        public Matrix3x3(Matrix3x3 copy)
        {
            r0c0 = copy.r0c0; r0c1 = copy.r0c1; r0c2 = copy.r0c2;
            r1c0 = copy.r1c0; r1c1 = copy.r1c1; r1c2 = copy.r1c2;
            r2c0 = copy.r2c0; r2c1 = copy.r2c1; r2c2 = copy.r2c2;
        }
        public Matrix3x3(double r0c0, double r0c1, double r0c2, double r1c0, double r1c1, double r1c2, double r2c0, double r2c1, double r2c2)
        {
            this.r0c0 = r0c0; this.r0c1 = r0c1; this.r0c2 = r0c2;
            this.r1c0 = r1c0; this.r1c1 = r1c1; this.r1c2 = r1c2;
            this.r2c0 = r2c0; this.r2c1 = r2c1; this.r2c2 = r2c2;
        }
        /// <param name="byRows"><see langword="true"/> if the array is of the form [c, r], <see langword="false"/> if the array is of the form [r, c].</param>
        public Matrix3x3(double[,] vals, bool byRows = false)
        {
            if (byRows) // Collection of rows ([c, r])
            {
                r0c0 = vals[0, 0]; r0c1 = vals[1, 0]; r0c2 = vals[2, 0];
                r1c0 = vals[0, 1]; r1c1 = vals[1, 1]; r1c2 = vals[2, 1];
                r2c0 = vals[0, 2]; r2c1 = vals[1, 2]; r2c2 = vals[2, 2];
            }
            else        // Collection of columns ([r, c])
            {
                r0c0 = vals[0, 0]; r0c1 = vals[0, 1]; r0c2 = vals[0, 2];
                r1c0 = vals[1, 0]; r1c1 = vals[1, 1]; r1c2 = vals[1, 2];
                r2c0 = vals[2, 0]; r2c1 = vals[2, 1]; r2c2 = vals[2, 2];
            }
        }
        /// <param name="byRows"><see langword="true"/> if the enumerable is a collection of rows (form [c, r]), <see langword="false"/> if the enumerable is a collection of columns (form [r, c]).</param>
        public Matrix3x3(IEnumerable<IEnumerable<double>> vals, bool byRows = false)
        {
            MatrixHelper.SetMatrixValues(this, vals, byRows);
        }
        /// <param name="byRows"><see langword="true"/> if the enumerable is a collection of rows (form [c, r]), <see langword="false"/> if the enumerable is a collection of columns (form [r, c]).</param>
        public Matrix3x3(IEnumerable<ListTuple<double>> vals, bool byRows = false)
        {
            MatrixHelper.SetMatrixValues(this, vals, byRows);
        }
        /// <param name="byRows"><see langword="true"/> if the fill goes through columns for each row, <see langword="false"/> if the fill goes through rows for each column.</param>
        public Matrix3x3(Fill<double> fill, bool byRows = false)
        {
            if (byRows)
            {
                r0c0 = fill(0); r0c1 = fill(3); r0c2 = fill(6);
                r1c0 = fill(1); r1c1 = fill(4); r1c2 = fill(7);
                r2c0 = fill(2); r2c1 = fill(5); r2c2 = fill(8);
            }
            else
            {
                r0c0 = fill(0); r0c1 = fill(1); r0c2 = fill(2);
                r1c0 = fill(3); r1c1 = fill(4); r1c2 = fill(5);
                r2c0 = fill(6); r2c1 = fill(7); r2c2 = fill(8);
            }
        }
        /// <param name="byRows"><see langword="true"/> if the fill is a collection of rows (form [c, r]), <see langword="false"/> if the fill is a collection of columns (form [r, c]).</param>
        public Matrix3x3(Fill2d<double> fill, bool byRows = false)
        {
            if (byRows)
            {
                r0c0 = fill(0, 0); r0c1 = fill(1, 0); r0c2 = fill(2, 0);
                r1c0 = fill(0, 1); r1c1 = fill(1, 1); r1c2 = fill(2, 1);
                r2c0 = fill(0, 2); r2c1 = fill(1, 2); r2c2 = fill(2, 2);
            }
            else
            {
                r0c0 = fill(0, 0); r0c1 = fill(0, 1); r0c2 = fill(0, 2);
                r1c0 = fill(1, 0); r1c1 = fill(1, 1); r1c2 = fill(1, 2);
                r2c0 = fill(2, 0); r2c1 = fill(2, 1); r2c2 = fill(2, 2);
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
                            case 2: return r0c2;
                            default: throw new ArgumentOutOfRangeException(nameof(c));
                        }
                    case 1:
                        switch (c)
                        {
                            case 0: return r1c0;
                            case 1: return r1c1;
                            case 2: return r1c2;
                            default: throw new ArgumentOutOfRangeException(nameof(c));
                        }
                    case 2:
                        switch (c)
                        {
                            case 0: return r2c0;
                            case 1: return r2c1;
                            case 2: return r2c2;
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
                            case 2: r0c2 = value; return;
                            default: throw new ArgumentOutOfRangeException(nameof(c));
                        }
                    case 1:
                        switch (c)
                        {
                            case 0: r1c0 = value; return;
                            case 1: r1c1 = value; return;
                            case 2: r1c2 = value; return;
                            default: throw new ArgumentOutOfRangeException(nameof(c));
                        }
                    case 2:
                        switch (c)
                        {
                            case 0: r2c0 = value; return;
                            case 1: r2c1 = value; return;
                            case 2: r2c2 = value; return;
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

        public static Matrix3x3 Average(IEnumerable<Matrix3x3> vals)
        {
            Matrix3x3 result = Zero;
            int count = 0;
            foreach (Matrix3x3 m in vals)
            {
                result += m;
                count++;
            }
            return result / count;
        }
        public static Matrix3x3 Lerp(Matrix3x3 a, Matrix3x3 b, double t, bool clamp = true) =>
            new Matrix3x3(MathE.Lerp(a.r0c0, b.r0c0, t, clamp), MathE.Lerp(a.r0c1, b.r0c1, t, clamp), MathE.Lerp(a.r0c2, b.r0c2, t, clamp),
                          MathE.Lerp(a.r1c0, b.r1c0, t, clamp), MathE.Lerp(a.r1c1, b.r1c1, t, clamp), MathE.Lerp(a.r1c2, b.r1c2, t, clamp),
                          MathE.Lerp(a.r2c0, b.r2c0, t, clamp), MathE.Lerp(a.r2c1, b.r2c1, t, clamp), MathE.Lerp(a.r2c2, b.r2c2, t, clamp));
        public static Matrix3x3 Product(IEnumerable<Matrix3x3> vals)
        {
            Matrix3x3 result = One;
            bool any = false;
            foreach (Matrix3x3 m in vals)
            {
                any = true;
                result *= m;
            }
            return any ? result : Zero;
        }
        public static Matrix3x3 Sum(IEnumerable<Matrix3x3> vals)
        {
            Matrix3x3 result = Zero;
            foreach (Matrix3x3 m in vals) result += m;
            return result;
        }

        public static (double[] r0c0, double[] r0c1, double[] r0c2, double[] r1c0, double[] r1c1, double[] r1c2, double[] r2c0, double[] r2c1, double[] r2c2) SplitArray(IEnumerable<Matrix3x3> vals)
        {
            int count = vals.Count();
            double[] r0c0 = new double[count], r0c1 = new double[count], r0c2 = new double[count],
                     r1c0 = new double[count], r1c1 = new double[count], r1c2 = new double[count],
                     r2c0 = new double[count], r2c1 = new double[count], r2c2 = new double[count];
            int index = 0;
            foreach (Matrix3x3 m in vals)
            {
                r0c0[index] = m.r0c0; r0c1[index] = m.r0c1; r0c2[index] = m.r0c2;
                r1c0[index] = m.r1c0; r1c1[index] = m.r1c1; r1c2[index] = m.r1c2;
                r2c0[index] = m.r2c0; r2c1[index] = m.r2c1; r2c2[index] = m.r2c2;
            }
            return (r0c0, r0c1, r0c2,
                    r1c0, r1c1, r1c2,
                    r2c0, r2c1, r2c2);
        }

        public ListTuple<double> GetRow(int row)
        {
            double[] vals;
            switch (row)
            {
                case 0: vals = new double[] { r0c0, r0c1, r0c2 }; break;
                case 1: vals = new double[] { r1c0, r1c1, r1c2 }; break;
                case 2: vals = new double[] { r2c0, r2c1, r2c2 }; break;
                default: throw new ArgumentOutOfRangeException(nameof(row));
            }
            return new ListTuple<double>(vals);
        }
        public ListTuple<double> GetColumn(int column)
        {
            double[] vals;
            switch (column)
            {
                case 0: vals = new double[] { r0c0, r1c0, r2c0 }; break;
                case 1: vals = new double[] { r0c1, r1c1, r2c1 }; break;
                case 2: vals = new double[] { r0c2, r1c2, r2c2 }; break;
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
                case 0: r0c0 = vals[0]; r0c1 = vals[1]; r0c2 = vals[2]; break;
                case 1: r1c0 = vals[0]; r1c1 = vals[1]; r1c2 = vals[2]; break;
                case 2: r2c0 = vals[0]; r2c1 = vals[1]; r2c2 = vals[2]; break;
                default: throw new ArgumentOutOfRangeException(nameof(row));
            }
        }
        public void SetColumn(int column, ListTuple<double> vals)
        {
            switch (column)
            {
                case 0: r0c0 = vals[0]; r1c0 = vals[1]; r2c0 = vals[2]; break;
                case 1: r0c1 = vals[0]; r1c1 = vals[1]; r2c1 = vals[2]; break;
                case 2: r0c2 = vals[0]; r1c2 = vals[1]; r2c2 = vals[2]; break;
                default: throw new ArgumentOutOfRangeException(nameof(column));
            }
        }

        public double Determinant() => // Alternating sum of the determinants of the first row of submatrices.
            r0c0 * (r1c1 * r2c2 - r1c2 * r2c1) -
            r0c1 * (r1c0 * r2c2 - r1c2 * r2c0) +
            r0c2 * (r1c0 * r2c1 - r1c1 * r2c0);

        public Matrix3x3 Adjoint() => // Transpose(Cofactor)
            new Matrix3x3(r1c1 * r2c2 - r1c2 * r2c1, r0c2 * r2c1 - r0c1 * r2c2, r0c1 * r1c2 - r0c2 * r1c1,
                          r1c2 * r2c0 - r1c0 * r2c2, r0c0 * r2c2 - r0c2 * r2c0, r0c2 * r1c0 - r0c0 * r1c2,
                          r1c0 * r2c1 - r1c1 * r2c0, r0c1 * r2c0 - r0c0 * r2c1, r0c0 * r1c1 - r0c1 * r1c0);
        public Matrix3x3 Cofactor() => // [r, c] = Determinant(Submatrix(r, c))
            new Matrix3x3(r1c1 * r2c2 - r1c2 * r2c1, r1c2 * r2c0 - r1c0 * r2c2, r1c0 * r2c1 - r1c1 * r2c0,
                          r0c2 * r2c1 - r0c1 * r2c2, r0c0 * r2c2 - r0c2 * r2c0, r0c1 * r2c0 - r0c0 * r2c1,
                          r0c1 * r1c2 - r0c2 * r1c1, r0c2 * r1c0 - r0c0 * r1c2, r0c0 * r1c1 - r0c1 * r1c0);
        public Matrix3x3 Inverse() // Adjoint / Determinant
        {
            double invDet = 1 / Determinant();
            return new Matrix3x3(invDet * (r1c1 * r2c2 - r1c2 * r2c1), invDet * (r0c2 * r2c1 - r0c1 * r2c2), invDet * (r0c1 * r1c2 - r0c2 * r1c1),
                                 invDet * (r1c2 * r2c0 - r1c0 * r2c2), invDet * (r0c0 * r2c2 - r0c2 * r2c0), invDet * (r0c2 * r1c0 - r0c0 * r1c2),
                                 invDet * (r1c0 * r2c1 - r1c1 * r2c0), invDet * (r0c1 * r2c0 - r0c0 * r2c1), invDet * (r0c0 * r1c1 - r0c1 * r1c0));
        }
        public Matrix3x3 Transpose() =>
            new Matrix3x3(r0c0, r1c0, r2c0,
                          r0c1, r1c1, r2c1,
                          r0c2, r1c2, r2c2);
        public Matrix2x2 Submatrix(int row, int column)
        {
            switch (row)
            {
                case 0:
                    switch (column)
                    {
                        case 0: return new Matrix2x2(r1c1, r1c2, r2c1, r2c2);
                        case 1: return new Matrix2x2(r1c0, r1c2, r2c0, r2c2);
                        case 2: return new Matrix2x2(r1c0, r1c1, r2c0, r2c1);
                        default: throw new ArgumentOutOfRangeException(nameof(column));
                    }
                case 1:
                    switch (column)
                    {
                        case 0: return new Matrix2x2(r0c1, r0c2, r2c1, r2c2);
                        case 1: return new Matrix2x2(r0c0, r0c2, r2c0, r2c2);
                        case 2: return new Matrix2x2(r0c0, r0c1, r2c0, r2c1);
                        default: throw new ArgumentOutOfRangeException(nameof(column));
                    }
                case 2:
                    switch (column)
                    {
                        case 0: return new Matrix2x2(r0c1, r0c2, r1c1, r1c2);
                        case 1: return new Matrix2x2(r0c0, r0c2, r1c0, r1c2);
                        case 2: return new Matrix2x2(r0c0, r0c1, r1c0, r1c1);
                        default: throw new ArgumentOutOfRangeException(nameof(column));
                    }
                default: throw new ArgumentOutOfRangeException(nameof(column));
            }
        }
        public double Trace() => r0c0 + r1c1 + r2c2;
    
        public IEnumerator<double> GetEnumerator()
        {
            yield return r0c0; yield return r0c1; yield return r0c2;
            yield return r1c0; yield return r1c1; yield return r1c2;
            yield return r2c0; yield return r2c1; yield return r2c2;
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

#if CS8_OR_GREATER
        public bool Equals(Matrix3x3? other) =>
#else
        public bool Equals(Matrix3x3 other) =>
#endif
            !(other is null) &&
            r0c0 == other.r0c0 && r0c1 == other.r0c1 && r0c2 == other.r0c2 &&
            r1c0 == other.r1c0 && r1c1 == other.r1c1 && r1c2 == other.r1c2 &&
            r2c0 == other.r2c0 && r2c1 == other.r2c1 && r2c2 == other.r2c2;
#if CS8_OR_GREATER
        public override bool Equals(object? other)
#else
        public override bool Equals(object other)
#endif
        {
            if (other is null) return false;
            else if (other is Matrix3x3 otherMat) return Equals(otherMat);
            else return false;
        }
        public override int GetHashCode() =>
            (int)((uint)r0c0.GetHashCode() & 0xE0000000 |
                  (uint)r0c1.GetHashCode() & 0x1E000000 |
                  (uint)r0c2.GetHashCode() & 0x01C00000 |
                  (uint)r1c0.GetHashCode() & 0x003C0000 |
                  (uint)r1c1.GetHashCode() & 0x00038000 |
                  (uint)r1c2.GetHashCode() & 0x00007800 |
                  (uint)r2c0.GetHashCode() & 0x00000700 |
                  (uint)r2c1.GetHashCode() & 0x000000F0 |
                  (uint)r2c2.GetHashCode() & 0x0000000F);
        public override string ToString() => ToStringHelper.MatrixToString(this, null);
#if CS8_OR_GREATER
        public string ToString(string? format) => ToStringHelper.MatrixToString(this, format);
        public string ToString(string? format, IFormatProvider? provider) => ToStringHelper.MatrixToString(this, format);
#else
        public string ToString(string format) => ToStringHelper.MatrixToString(this, format);
        public string ToString(string format, IFormatProvider provider) => ToStringHelper.MatrixToString(this, format);
#endif

        public static Matrix3x3 operator +(Matrix3x3 a) =>
            new Matrix3x3(a.r0c0, a.r0c1, a.r0c2,
                          a.r1c0, a.r1c1, a.r1c2,
                          a.r2c0, a.r2c1, a.r2c2);
        public static Matrix3x3 operator +(Matrix3x3 a, Matrix3x3 b) =>
            new Matrix3x3(a.r0c0 + b.r0c0, a.r0c1 + b.r0c1, a.r0c2 + b.r0c2,
                          a.r1c0 + b.r1c0, a.r1c1 + b.r1c1, a.r1c2 + b.r1c2,
                          a.r2c0 + b.r2c0, a.r2c1 + b.r2c1, a.r2c2 + b.r2c2);
        public static Matrix3x3 operator -(Matrix3x3 a) =>
            new Matrix3x3(-a.r0c0, -a.r0c1, -a.r0c2,
                          -a.r1c0, -a.r1c1, -a.r1c2,
                          -a.r2c0, -a.r2c1, -a.r2c2);
        public static Matrix3x3 operator -(Matrix3x3 a, Matrix3x3 b) =>
            new Matrix3x3(a.r0c0 - b.r0c0, a.r0c1 - b.r0c1, a.r0c2 - b.r0c2,
                          a.r1c0 - b.r1c0, a.r1c1 - b.r1c1, a.r1c2 - b.r1c2,
                          a.r2c0 - b.r2c0, a.r2c1 - b.r2c1, a.r2c2 - b.r2c2);
        public static Matrix3x3 operator *(Matrix3x3 a, double b) =>
            new Matrix3x3(a.r0c0 * b, a.r0c1 * b, a.r0c2 * b,
                          a.r1c0 * b, a.r1c1 * b, a.r1c2 * b,
                          a.r2c0 * b, a.r2c1 * b, a.r2c2 * b);
        public static Float3 operator *(Matrix3x3 a, Float3 b) =>
            new Float3(a.r0c0 * b.x + a.r0c1 * b.y + a.r0c2 * b.z,
                       a.r1c0 * b.x + a.r1c1 * b.y + a.r1c2 * b.z,
                       a.r2c0 * b.x + a.r2c1 * b.y + a.r2c2 * b.z);
        public static Matrix3x3 operator *(Matrix3x3 a, Matrix3x3 b) =>
            new Matrix3x3(a.r0c0 * b.r0c0 + a.r0c1 * b.r1c0 + a.r0c2 * b.r2c0, a.r0c0 * b.r0c1 + a.r0c1 * b.r1c1 + a.r0c2 * b.r2c1, a.r0c0 * b.r0c2 + a.r0c1 * b.r1c2 + a.r0c2 * b.r2c2,
                          a.r1c0 * b.r0c0 + a.r1c1 * b.r1c0 + a.r1c2 * b.r2c0, a.r1c0 * b.r0c1 + a.r1c1 * b.r1c1 + a.r1c2 * b.r2c1, a.r1c0 * b.r0c2 + a.r1c1 * b.r1c2 + a.r1c2 * b.r2c2,
                          a.r2c0 * b.r0c0 + a.r2c1 * b.r1c0 + a.r2c2 * b.r2c0, a.r2c0 * b.r0c1 + a.r2c1 * b.r1c1 + a.r2c2 * b.r2c1, a.r2c0 * b.r0c2 + a.r2c1 * b.r1c2 + a.r2c2 * b.r2c2);
        public static Matrix3x3 operator /(Matrix3x3 a, double b) =>
            new Matrix3x3(a.r0c0 / b, a.r0c1 / b, a.r0c2 / b,
                          a.r1c0 / b, a.r1c1 / b, a.r1c2 / b,
                          a.r2c0 / b, a.r2c1 / b, a.r2c2 / b);
        public static Matrix3x3 operator ^(Matrix3x3 a, Matrix3x3 b) =>
            new Matrix3x3(a.r0c0 * b.r0c0, a.r0c1 * b.r0c1, a.r0c2 * b.r0c2,
                          a.r1c0 * b.r1c0, a.r1c1 * b.r1c1, a.r1c2 * b.r1c2,
                          a.r2c0 * b.r2c0, a.r2c1 * b.r2c1, a.r2c2 * b.r2c2);
        public static Matrix3x3 operator ~(Matrix3x3 a) => a.Inverse();
        public static bool operator ==(Matrix3x3 a, Matrix3x3 b) => a.Equals(b);
        public static bool operator !=(Matrix3x3 a, Matrix3x3 b) => !a.Equals(b);

        public static explicit operator Matrix3x3(Matrix mat) =>
            new Matrix3x3(mat.TryGet(0, 0), mat.TryGet(0, 1), mat.TryGet(0, 2),
                          mat.TryGet(1, 0), mat.TryGet(1, 1), mat.TryGet(1, 2),
                          mat.TryGet(2, 0), mat.TryGet(2, 1), mat.TryGet(2, 2));
        public static implicit operator Matrix3x3(Matrix2x2 mat) =>
            new Matrix3x3(mat.r0c0, mat.r0c1, 0,
                          mat.r1c0, mat.r1c1, 0,
                          0       , 0       , 1);
        public static explicit operator Matrix3x3(Matrix4x4 mat) =>
            new Matrix3x3(mat.r0c0, mat.r0c1, mat.r0c2,
                          mat.r1c0, mat.r1c1, mat.r1c2,
                          mat.r2c0, mat.r2c1, mat.r2c2);
    }
}
