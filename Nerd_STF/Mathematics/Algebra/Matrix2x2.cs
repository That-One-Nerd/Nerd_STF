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
                            ,ISplittable<Matrix2x2, (double[] r1c1, double[] r1c2, double[] r2c1, double[] r2c2)>,
                             IStaticMatrix<Matrix2x2>
#endif
    {
        public static Matrix2x2 Identity => new Matrix2x2(1, 0, 0, 1);
        public static Matrix2x2 SignField => new Matrix2x2(1, -1, -1, 1);

        public static Matrix2x2 One => new Matrix2x2(1, 1, 1, 1);
        public static Matrix2x2 Zero => new Matrix2x2(0, 0, 0, 0);

        public Int2 Size => (2, 2);

        public double r1c1, r1c2,
                      r2c1, r2c2;

        public Matrix2x2()
        {
            r1c1 = 0;
            r1c2 = 0;
            r2c1 = 0;
            r2c2 = 0;
        }
        public Matrix2x2(Matrix2x2 copy)
        {
            r1c1 = copy.r1c1;
            r1c2 = copy.r1c2;
            r2c1 = copy.r2c1;
            r2c2 = copy.r2c2;
        }
        public Matrix2x2(double r1c1, double r1c2, double r2c1, double r2c2)
        {
            this.r1c1 = r1c1;
            this.r1c2 = r1c2;
            this.r2c1 = r2c1;
            this.r2c2 = r2c2;
        }
        /// <param name="byRows"><see langword="true"/> if the array is of the form [c, r], <see langword="false"/> if the array is of the form [r, c].</param>
        public Matrix2x2(double[,] vals, bool byRows = true)
        {
            if (byRows) // Collection of rows ([c, r])
            {
                r1c1 = vals[0, 0]; r1c2 = vals[0, 1];
                r2c1 = vals[1, 0]; r2c2 = vals[1, 1];
            }
            else        // Collection of columns ([r, c])
            {
                r1c1 = vals[0, 0]; r1c2 = vals[1, 0];
                r2c1 = vals[0, 1]; r2c2 = vals[1, 1];
            }
        }
        /// <param name="byRows"><see langword="true"/> if the enumerable is a collection of rows (form [c, r]), <see langword="false"/> if the enumerable is a collection of columns (form [r, c]).</param>
        public Matrix2x2(IEnumerable<IEnumerable<double>> vals, bool byRows = true)
        {
            int x = 0;
            foreach (IEnumerable<double> part in vals)
            {
                int y = 0;
                foreach (double v in part)
                {
                    this[byRows ? (x, y) : (y, x)] = v;
                    y++;
                    if (y >= 2) break;
                }
                x++;
                if (x >= 2) break;
            }
        }
        /// <param name="byRows"><see langword="true"/> if the enumerable is a collection of rows (form [c, r]), <see langword="false"/> if the enumerable is a collection of columns (form [r, c]).</param>
        public Matrix2x2(IEnumerable<ListTuple<double>> vals, bool byRows = true)
        {
            int x = 0;
            foreach (IEnumerable<double> part in vals)
            {
                int y = 0;
                foreach (double v in part)
                {
                    this[byRows ? (x, y) : (y, x)] = v;
                    y++;
                    if (y >= 2) break;
                }
                x++;
                if (x >= 2) break;
            }
        }
        
        public double this[int r, int c]
        {
            get => this[(r, c)];
            set => this[(r, c)] = value;
        }
        public double this[Int2 index]
        {
            get
            {
                switch (index.x)         // (r, c)
                {
                    case 0:
                        switch (index.y)
                        {
                            case 0: return r1c1;
                            case 1: return r1c2;
                            default: throw new ArgumentOutOfRangeException(nameof(index));
                        }
                    case 1:
                        switch (index.y)
                        {
                            case 0: return r2c1;
                            case 1: return r2c2;
                            default: throw new ArgumentOutOfRangeException(nameof(index));
                        }
                    default: throw new ArgumentOutOfRangeException(nameof(index));
                }
            }
            set
            {
                switch (index.x)         // (r, c)
                {
                    case 0:
                        switch (index.y)
                        {
                            case 0: r1c1 = value; return;
                            case 1: r1c2 = value; return;
                            default: throw new ArgumentOutOfRangeException(nameof(index));
                        }
                    case 1:
                        switch (index.y)
                        {
                            case 0: r2c1 = value; return;
                            case 1: r2c2 = value; return;
                            default: throw new ArgumentOutOfRangeException(nameof(index));
                        }
                    default: throw new ArgumentOutOfRangeException(nameof(index));
                }
            }
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
            new Matrix2x2(MathE.Lerp(a.r1c1, b.r1c1, t, clamp), MathE.Lerp(a.r1c2, b.r1c2, t, clamp),
                          MathE.Lerp(a.r2c1, b.r2c1, t, clamp), MathE.Lerp(a.r2c2, b.r2c2, t, clamp));
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

        public static (double[] r1c1, double[] r1c2, double[] r2c1, double[] r2c2) SplitArray(IEnumerable<Matrix2x2> vals)
        {
            int count = vals.Count();
            double[] r1c1 = new double[count], r1c2 = new double[count],
                     r2c1 = new double[count], r2c2 = new double[count];
            int index = 0;
            foreach (Matrix2x2 m in vals)
            {
                r1c1[index] = m.r1c1;
                r1c2[index] = m.r1c2;
                r2c1[index] = m.r2c1;
                r2c2[index] = m.r2c2;
            }
            return (r1c1, r1c2, r2c1, r2c2);
        }

        public ListTuple<double> GetRow(int row)
        {
            double[] vals;
            switch (row)
            {
                case 0: vals = new double[] { r1c1, r1c2 }; break;
                case 1: vals = new double[] { r2c1, r2c2 }; break;
                default: throw new ArgumentOutOfRangeException(nameof(row));
            }
            return new ListTuple<double>(vals);
        }
        public ListTuple<double> GetColumn(int column)
        {
            double[] vals;
            switch (column)
            {
                case 0: vals = new double[] { r1c1, r2c1 }; break;
                case 1: vals = new double[] { r1c2, r2c2 }; break;
                default: throw new ArgumentOutOfRangeException(nameof(column));
            }
            return new ListTuple<double>(vals);
        }
        public void SetRow(int row, IEnumerable<double> vals)
        {
            int col = 0;
            foreach (double v in vals)
            {
                this[(row, col)] = v;
                col++;
                if (col >= 2) return;
            }
        }
        public void SetColumn(int column, IEnumerable<double> vals)
        {
            int row = 0;
            foreach (double v in vals)
            {
                this[(row, column)] = v;
                row++;
                if (row >= 2) return;
            }
        }
        public void SetRow(int row, ListTuple<double> vals) => SetRow(row, (IEnumerable<double>)vals);
        public void SetColumn(int row, ListTuple<double> vals) => SetColumn(row, (IEnumerable<double>)vals);

        public double Determinant() => r1c1 * r2c2 - r1c2 * r2c1;

        public Matrix2x2 Adjoint() =>
            new Matrix2x2( r2c2, -r1c2,
                          -r2c1,  r1c1);
        public Matrix2x2 Cofactor() =>
            new Matrix2x2( r2c2, -r2c1,
                          -r1c2,  r1c1);
        public Matrix2x2 Inverse()
        {
            double invDet = 1 / Determinant();
            return new Matrix2x2( r2c2 * invDet, -r1c2 * invDet,
                                 -r2c1 * invDet,  r1c1 * invDet);
        }
        public Matrix2x2 Transpose() =>
            new Matrix2x2(r1c1, r2c1,
                          r1c2, r2c2);
        public double Trace() => r1c1 + r2c2;

        public IEnumerator<double> GetEnumerator()
        {
            yield return r1c1;
            yield return r1c2;
            yield return r2c1;
            yield return r2c2;
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

#if CS8_OR_GREATER
        public bool Equals(Matrix2x2? other) =>
#else
        public bool Equals(Matrix2x2 other) =>
#endif
            !(other is null) &&
            r1c1 == other.r1c1 && r1c2 == other.r1c2 &&
            r2c1 == other.r2c1 && r2c2 == other.r2c2;
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
            (int)((uint)r1c1.GetHashCode() & 0xFF000000 |
                  (uint)r1c2.GetHashCode() & 0x00FF0000 |
                  (uint)r2c1.GetHashCode() & 0x0000FF00 |
                  (uint)r2c2.GetHashCode() & 0x000000FF);
        public override string ToString() => ToStringHelper.MatrixToString(this, null);
#if CS8_OR_GREATER
        public string ToString(string? format) => ToStringHelper.MatrixToString(this, format);
#else
        public string ToString(string format) => ToStringHelper.MatrixToString(this, format);
#endif
#if CS8_OR_GREATER
        public string ToString(string? format, IFormatProvider? provider) => ToStringHelper.MatrixToString(this, format);
#else
        public string ToString(string format, IFormatProvider provider) => ToStringHelper.MatrixToString(this, format);
#endif

        public static Matrix2x2 operator +(Matrix2x2 a) =>
            new Matrix2x2(a.r1c1, a.r1c2,
                          a.r2c1, a.r2c2);
        public static Matrix2x2 operator +(Matrix2x2 a, Matrix2x2 b) =>
            new Matrix2x2(a.r1c1 + b.r1c1, a.r1c2 + b.r1c2,
                          a.r2c1 + b.r2c1, a.r2c2 + b.r2c2);
        public static Matrix2x2 operator -(Matrix2x2 a) =>
            new Matrix2x2(-a.r1c1, -a.r1c2,
                          -a.r2c1, -a.r2c2);
        public static Matrix2x2 operator -(Matrix2x2 a, Matrix2x2 b) =>
            new Matrix2x2(a.r1c1 - b.r1c1, a.r1c2 - b.r1c2,
                          a.r2c1 - b.r2c1, a.r2c2 - b.r2c2);
        public static Matrix2x2 operator *(Matrix2x2 a, Matrix2x2 b) =>
            new Matrix2x2(a.r1c1 * b.r1c1 + a.r1c2 * b.r2c1, a.r1c1 * b.r1c2 + a.r1c2 * b.r2c2,
                          a.r2c1 * b.r1c1 + a.r2c2 * b.r2c1, a.r2c1 * b.r1c2 + a.r2c2 * b.r2c2);
        public static Matrix2x2 operator *(Matrix2x2 a, double b) =>
            new Matrix2x2(a.r1c1 * b, a.r1c2 * b,
                          a.r2c1 * b, a.r2c2 * b);
        public static Float2 operator *(Matrix2x2 a, Float2 b) =>
            new Float2(a.r1c1 * b.x + a.r1c2 * b.y,
                       a.r2c1 * b.x + a.r2c2 * b.y);
        public static Matrix2x2 operator /(Matrix2x2 a, double b) =>
            new Matrix2x2(a.r1c1 / b, a.r1c2 / b,
                          a.r2c1 / b, a.r2c2 / b);
        public static Matrix2x2 operator ~(Matrix2x2 a) => a.Inverse();
    }
}
