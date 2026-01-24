using Nerd_STF.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Nerd_STF.Mathematics.Algebra
{
    public class Matrix4x4 : IMatrix<Matrix4x4>,
                             ISquareMatrix<Matrix4x4>,
                             ISubmatrixOperations<Matrix4x4, Matrix3x3>
#if CS11_OR_GREATER
                            ,IStaticMatrix<Matrix4x4>
#endif
    {
        public static Matrix4x4 Identity =>
            new Matrix4x4(1, 0, 0, 0,
                          0, 1, 0, 0,
                          0, 0, 1, 0,
                          0, 0, 0, 1);
        public static Matrix4x4 SignField =>
            new Matrix4x4(+1, -1, +1, -1,
                          -1, +1, -1, +1,
                          +1, -1, +1, -1,
                          -1, +1, -1, +1);

        public static Matrix4x4 One => new Matrix4x4(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1);
        public static Matrix4x4 Zero => new Matrix4x4(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);

        public Int2 Size => (4, 4);

        public double r0c0, r0c1, r0c2, r0c3,
                      r1c0, r1c1, r1c2, r1c3,
                      r2c0, r2c1, r2c2, r2c3,
                      r3c0, r3c1, r3c2, r3c3;

        public Matrix4x4()
        {
            r0c0 = 0; r0c1 = 0; r0c2 = 0; r0c3 = 0;
            r1c0 = 0; r1c1 = 0; r1c2 = 0; r1c3 = 0;
            r2c0 = 0; r2c1 = 0; r2c2 = 0; r2c3 = 0;
            r3c0 = 0; r3c1 = 0; r3c2 = 0; r3c3 = 0;
        }
        public Matrix4x4(Matrix4x4 copy)
        {
            r0c0 = copy.r0c0; r0c1 = copy.r0c1; r0c2 = copy.r0c2; r0c3 = copy.r0c3;
            r1c0 = copy.r1c0; r1c1 = copy.r1c1; r1c2 = copy.r1c2; r1c3 = copy.r1c3;
            r2c0 = copy.r2c0; r2c1 = copy.r2c1; r2c2 = copy.r2c2; r2c3 = copy.r2c3;
            r3c0 = copy.r3c0; r3c1 = copy.r3c1; r3c2 = copy.r3c2; r3c3 = copy.r3c3;
        }
        public Matrix4x4(double r0c0, double r0c1, double r0c2, double r0c3, double r1c0, double r1c1, double r1c2, double r1c3, double r2c0, double r2c1, double r2c2, double r2c3, double r3c0, double r3c1, double r3c2, double r3c3)
        {
            this.r0c0 = r0c0; this.r0c1 = r0c1; this.r0c2 = r0c2; this.r0c3 = r0c3;
            this.r1c0 = r1c0; this.r1c1 = r1c1; this.r1c2 = r1c2; this.r1c3 = r1c3;
            this.r2c0 = r2c0; this.r2c1 = r2c1; this.r2c2 = r2c2; this.r2c3 = r2c3;
            this.r3c0 = r3c0; this.r3c1 = r3c1; this.r3c2 = r3c2; this.r3c3 = r3c3;
        }
        /// <param name="byRows"><see langword="true"/> if the array is of the form [c, r], <see langword="false"/> if the array is of the form [r, c].</param>
        public Matrix4x4(double[,] vals, bool byRows = false)
        {
            if (byRows) // Collection of rows ([c, r])
            {
                r0c0 = vals[0, 0]; r0c1 = vals[1, 0]; r0c2 = vals[2, 0]; r0c3 = vals[3, 0];
                r1c0 = vals[0, 1]; r1c1 = vals[1, 1]; r1c2 = vals[2, 1]; r1c3 = vals[3, 1];
                r2c0 = vals[0, 2]; r2c1 = vals[1, 2]; r2c2 = vals[2, 2]; r2c3 = vals[3, 2];
                r3c0 = vals[0, 3]; r3c1 = vals[1, 3]; r3c2 = vals[2, 3]; r3c3 = vals[3, 3];
            }
            else
            {
                r0c0 = vals[0, 0]; r0c1 = vals[0, 1]; r0c2 = vals[0, 2]; r0c3 = vals[0, 3];
                r1c0 = vals[1, 0]; r1c1 = vals[1, 1]; r1c2 = vals[1, 2]; r1c3 = vals[1, 3];
                r2c0 = vals[2, 0]; r2c1 = vals[2, 1]; r2c2 = vals[2, 2]; r2c3 = vals[2, 3];
                r3c0 = vals[3, 0]; r3c1 = vals[3, 1]; r3c2 = vals[3, 2]; r3c3 = vals[3, 3];
            }
        }
        /// <param name="byRows"><see langword="true"/> if the enumerable is a collection of rows (form [c, r]), <see langword="false"/> if the enumerable is a collection of columns (form [r, c]).</param>
        public Matrix4x4(IEnumerable<IEnumerable<double>> vals, bool byRows = false)
        {
            MatrixHelper.SetMatrixValues(this, vals, byRows);
        }
        /// <param name="byRows"><see langword="true"/> if the enumerable is a collection of rows (form [c, r]), <see langword="false"/> if the enumerable is a collection of columns (form [r, c]).</param>
        public Matrix4x4(IEnumerable<ListTuple<double>> vals, bool byRows = false)
        {
            MatrixHelper.SetMatrixValues(this, vals, byRows);
        }
        /// <param name="byRows"><see langword="true"/> if the fill goes through columns for each row, <see langword="false"/> if the fill goes through rows for each column.</param>
        public Matrix4x4(Fill<double> fill, bool byRows = false)
        {
            if (byRows)
            {
                r0c0 = fill(0); r0c1 = fill(4); r0c2 = fill( 8); r0c3 = fill(12);
                r1c0 = fill(1); r1c1 = fill(5); r1c2 = fill( 9); r1c3 = fill(13);
                r2c0 = fill(2); r2c1 = fill(6); r2c2 = fill(10); r2c3 = fill(14);
                r3c0 = fill(3); r3c1 = fill(7); r3c2 = fill(11); r3c3 = fill(15);
            }
            else
            {
                r0c0 = fill( 0); r0c1 = fill( 1); r0c2 = fill( 2); r0c3 = fill( 3);
                r1c0 = fill( 4); r1c1 = fill( 5); r1c2 = fill( 6); r1c3 = fill( 7);
                r2c0 = fill( 8); r2c1 = fill( 9); r2c2 = fill(10); r2c3 = fill(11);
                r3c0 = fill(12); r3c1 = fill(13); r3c2 = fill(14); r3c3 = fill(15);
            }
        }
        /// <param name="byRows"><see langword="true"/> if the fill is a collection of rows (form [c, r]), <see langword="false"/> if the fill is a collection of columns (form [r, c]).</param>
        public Matrix4x4(Fill2d<double> fill, bool byRows = false)
        {
            if (byRows)
            {
                r0c0 = fill(0, 0); r0c1 = fill(1, 0); r0c2 = fill(2, 0); r0c3 = fill(3, 0);
                r1c0 = fill(0, 1); r1c1 = fill(1, 1); r1c2 = fill(2, 1); r1c3 = fill(3, 1);
                r2c0 = fill(0, 2); r2c1 = fill(1, 2); r2c2 = fill(2, 2); r2c3 = fill(3, 2);
                r3c0 = fill(0, 3); r3c1 = fill(1, 3); r3c2 = fill(2, 3); r3c3 = fill(3, 3);
            }
            else
            {
                r0c0 = fill(0, 0); r0c1 = fill(0, 1); r0c2 = fill(0, 2); r0c3 = fill(0, 3);
                r1c0 = fill(1, 0); r1c1 = fill(1, 1); r1c2 = fill(1, 2); r1c3 = fill(1, 3);
                r2c0 = fill(2, 0); r2c1 = fill(2, 1); r2c2 = fill(2, 2); r2c3 = fill(2, 3);
                r3c0 = fill(3, 0); r3c1 = fill(3, 1); r3c2 = fill(3, 2); r3c3 = fill(3, 3);
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
                            case 3: return r0c3;
                            default: throw new ArgumentOutOfRangeException(nameof(c));
                        }
                    case 1:
                        switch (c)
                        {
                            case 0: return r1c0;
                            case 1: return r1c1;
                            case 2: return r1c2;
                            case 3: return r1c3;
                            default: throw new ArgumentOutOfRangeException(nameof(c));
                        }
                    case 2:
                        switch (c)
                        {
                            case 0: return r2c0;
                            case 1: return r2c1;
                            case 2: return r2c2;
                            case 3: return r2c3;
                            default: throw new ArgumentOutOfRangeException(nameof(c));
                        }
                    case 3:
                        switch (c)
                        {
                            case 0: return r3c0;
                            case 1: return r3c1;
                            case 2: return r3c2;
                            case 3: return r3c3;
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
                            case 3: r0c3 = value; return;
                            default: throw new ArgumentOutOfRangeException(nameof(c));
                        }
                    case 1:
                        switch (c)
                        {
                            case 0: r1c0 = value; return;
                            case 1: r1c1 = value; return;
                            case 2: r1c2 = value; return;
                            case 3: r1c3 = value; return;
                            default: throw new ArgumentOutOfRangeException(nameof(c));
                        }
                    case 2:
                        switch (c)
                        {
                            case 0: r2c0 = value; return;
                            case 1: r2c1 = value; return;
                            case 2: r2c2 = value; return;
                            case 3: r2c3 = value; return;
                            default: throw new ArgumentOutOfRangeException(nameof(c));
                        }
                    case 3:
                        switch (c)
                        {
                            case 0: r3c0 = value; return;
                            case 1: r3c1 = value; return;
                            case 2: r3c2 = value; return;
                            case 3: r3c3 = value; return;
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

        public static Matrix4x4 Average(IEnumerable<Matrix4x4> vals)
        {
            Matrix4x4 result = Zero;
            int count = 0;
            foreach (Matrix4x4 m in vals)
            {
                result += m;
                count++;
            }
            return result / count;
        }
        public static Matrix4x4 Lerp(Matrix4x4 a, Matrix4x4 b, double t, bool clamp = true) =>
            new Matrix4x4(MathE.Lerp(a.r0c0, b.r0c0, t, clamp), MathE.Lerp(a.r0c1, b.r0c1, t, clamp), MathE.Lerp(a.r0c2, b.r0c2, t, clamp), MathE.Lerp(a.r0c3, b.r0c3, t, clamp),
                          MathE.Lerp(a.r1c0, b.r1c0, t, clamp), MathE.Lerp(a.r1c1, b.r1c1, t, clamp), MathE.Lerp(a.r1c2, b.r1c2, t, clamp), MathE.Lerp(a.r1c3, b.r1c3, t, clamp),
                          MathE.Lerp(a.r2c0, b.r2c0, t, clamp), MathE.Lerp(a.r2c1, b.r2c1, t, clamp), MathE.Lerp(a.r2c2, b.r2c2, t, clamp), MathE.Lerp(a.r2c3, b.r2c3, t, clamp),
                          MathE.Lerp(a.r3c0, b.r3c0, t, clamp), MathE.Lerp(a.r3c1, b.r3c1, t, clamp), MathE.Lerp(a.r3c2, b.r3c2, t, clamp), MathE.Lerp(a.r3c3, b.r3c3, t, clamp));
        public static Matrix4x4 Product(IEnumerable<Matrix4x4> vals)
        {
            Matrix4x4 result = One;
            bool any = false;
            foreach (Matrix4x4 m in vals)
            {
                any = true;
                result *= m;
            }
            return any ? result : Zero;
        }
        public static Matrix4x4 Sum(IEnumerable<Matrix4x4> vals)
        {
            Matrix4x4 result = Zero;
            foreach (Matrix4x4 m in vals) result += m;
            return result;
        }

        public ListTuple<double> GetRow(int row)
        {
            double[] vals;
            switch (row)
            {
                case 0: vals = new double[] { r0c0, r0c1, r0c2, r0c3 }; break;
                case 1: vals = new double[] { r1c0, r1c1, r1c2, r1c3 }; break;
                case 2: vals = new double[] { r2c0, r2c1, r2c2, r2c3 }; break;
                case 3: vals = new double[] { r3c0, r3c1, r3c2, r3c3 }; break;
                default: throw new ArgumentOutOfRangeException(nameof(row));
            }
            return new ListTuple<double>(vals);
        }
        public ListTuple<double> GetColumn(int column)
        {
            double[] vals;
            switch (column)
            {
                case 0: vals = new double[] { r0c0, r1c0, r2c0, r3c0 }; break;
                case 1: vals = new double[] { r0c1, r1c1, r2c1, r3c1 }; break;
                case 2: vals = new double[] { r0c2, r1c2, r2c2, r3c2 }; break;
                case 3: vals = new double[] { r0c3, r1c3, r2c3, r3c3 }; break;
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
                case 0: r0c0 = vals[0]; r0c1 = vals[1]; r0c2 = vals[2]; r0c3 = vals[3]; break;
                case 1: r1c0 = vals[0]; r1c1 = vals[1]; r1c2 = vals[2]; r1c3 = vals[3]; break;
                case 2: r2c0 = vals[0]; r2c1 = vals[1]; r2c2 = vals[2]; r2c3 = vals[3]; break;
                case 3: r3c0 = vals[0]; r3c1 = vals[1]; r3c2 = vals[2]; r3c3 = vals[3]; break;
                default: throw new ArgumentOutOfRangeException(nameof(row));
            }
        }
        public void SetColumn(int column, ListTuple<double> vals)
        {
            switch (column)
            {
                case 0: r0c0 = vals[0]; r1c0 = vals[1]; r2c0 = vals[2]; r3c0 = vals[3]; break;
                case 1: r0c1 = vals[0]; r1c1 = vals[1]; r2c1 = vals[2]; r3c1 = vals[3]; break;
                case 2: r0c2 = vals[0]; r1c2 = vals[1]; r2c2 = vals[2]; r3c2 = vals[3]; break;
                case 3: r0c3 = vals[0]; r1c3 = vals[1]; r2c3 = vals[2]; r3c3 = vals[3]; break;
                default: throw new ArgumentOutOfRangeException(nameof(column));
            }
        }

        // Sorry some of these are huge. I just want to inline
        // it as much as physically possible.
        public double Determinant()
        {
            double A = r2c2 * r3c3 - r2c3 * r3c2,
                   B = r2c1 * r3c3 - r2c3 * r3c1,
                   C = r2c1 * r3c2 - r2c2 * r3c1,
                   D = r2c0 * r3c3 - r2c3 * r3c0,
                   E = r2c0 * r3c2 - r2c2 * r3c0,
                   F = r2c0 * r3c1 - r2c1 * r3c0;
            return r0c0 * (r1c1 * A - r1c2 * B + r1c3 * C) -
                   r0c1 * (r1c0 * A - r1c2 * D + r1c3 * E) +
                   r0c2 * (r1c0 * B - r1c1 * D + r1c3 * F) -
                   r0c3 * (r1c0 * C - r1c1 * E + r1c2 * F);
        }

        public Matrix4x4 Adjoint() // Transpose(Cofactor)
        {
            double A = r2c2 * r3c3 - r2c3 * r3c2,
                   B = r2c1 * r3c3 - r2c3 * r3c1,
                   C = r2c1 * r3c2 - r2c2 * r3c1,
                   D = r2c0 * r3c3 - r2c3 * r3c0,
                   E = r2c0 * r3c2 - r2c2 * r3c0,
                   F = r2c0 * r3c1 - r2c1 * r3c0,
                   G = r1c2 * r3c3 - r1c3 * r3c2,
                   H = r1c1 * r3c3 - r1c3 * r3c1,
                   I = r1c1 * r3c2 - r1c2 * r3c1,
                   J = r1c0 * r3c3 - r1c3 * r3c0,
                   K = r1c0 * r3c2 - r1c2 * r3c0,
                   L = r1c0 * r3c1 - r1c1 * r3c0,
                   M = r1c2 * r2c3 - r1c3 * r2c2,
                   N = r1c1 * r2c3 - r1c3 * r2c1,
                   O = r1c1 * r2c2 - r1c2 * r2c1,
                   P = r1c0 * r2c3 - r1c3 * r2c0,
                   Q = r1c0 * r2c2 - r1c2 * r2c0,
                   R = r1c0 * r2c1 - r1c1 * r2c0;
            return new Matrix4x4(r1c1 * A - r1c2 * B + r1c3 * C, r0c2 * B - r0c3 * C - r0c1 * A, r0c1 * G - r0c2 * H + r0c3 * I, r0c2 * N - r0c3 * O - r0c1 * M,
                                 r1c2 * D - r1c3 * E - r1c0 * A, r0c0 * A - r0c2 * D + r0c3 * E, r0c2 * J - r0c3 * K - r0c0 * G, r0c0 * M - r0c2 * P + r0c3 * Q,
                                 r1c0 * B - r1c1 * D + r1c3 * F, r0c1 * D - r0c3 * F - r0c0 * B, r0c0 * H - r0c1 * J + r0c3 * L, r0c1 * P - r0c3 * R - r0c0 * N,
                                 r1c1 * E - r1c2 * F - r1c0 * C, r0c0 * C - r0c1 * E + r0c2 * F, r0c1 * K - r0c2 * L - r0c0 * I, r0c0 * O - r0c1 * Q + r0c2 * R);
        }
        public Matrix4x4 Cofactor() // [r, c] = Determinant(Submatrix(r, c))
        {
            double A = r2c2 * r3c3 - r2c3 * r3c2,
                   B = r2c1 * r3c3 - r2c3 * r3c1,
                   C = r2c1 * r3c2 - r2c2 * r3c1,
                   D = r2c0 * r3c3 - r2c3 * r3c0,
                   E = r2c0 * r3c2 - r2c2 * r3c0,
                   F = r2c0 * r3c1 - r2c1 * r3c0,
                   G = r1c2 * r3c3 - r1c3 * r3c2,
                   H = r1c1 * r3c3 - r1c3 * r3c1,
                   I = r1c1 * r3c2 - r1c2 * r3c1,
                   J = r1c0 * r3c3 - r1c3 * r3c0,
                   K = r1c0 * r3c2 - r1c2 * r3c0,
                   L = r1c0 * r3c1 - r1c1 * r3c0,
                   M = r1c2 * r2c3 - r1c3 * r2c2,
                   N = r1c1 * r2c3 - r1c3 * r2c1,
                   O = r1c1 * r2c2 - r1c2 * r2c1,
                   P = r1c0 * r2c3 - r1c3 * r2c0,
                   Q = r1c0 * r2c2 - r1c2 * r2c0,
                   R = r1c0 * r2c1 - r1c1 * r2c0;
            return new Matrix4x4(r1c1 * A - r1c2 * B + r1c3 * C, r1c2 * D - r1c3 * E - r1c0 * A, r1c0 * B - r1c1 * D + r1c3 * F, r1c1 * E - r1c2 * F - r1c0 * C,
                                 r0c2 * B - r0c3 * C - r0c1 * A, r0c0 * A - r0c2 * D + r0c3 * E, r0c1 * D - r0c3 * F - r0c0 * B, r0c0 * C - r0c1 * E + r0c2 * F,
                                 r0c1 * G - r0c2 * H + r0c3 * I, r0c2 * J - r0c3 * K - r0c0 * G, r0c0 * H - r0c1 * J + r0c3 * L, r0c1 * K - r0c2 * L - r0c0 * I,
                                 r0c2 * N - r0c3 * O - r0c1 * M, r0c0 * M - r0c2 * P + r0c3 * Q, r0c1 * P - r0c3 * R - r0c0 * N, r0c0 * O - r0c1 * Q + r0c2 * R);
        }
        public Matrix4x4 Inverse() // Adjoint / Determinant()
        {
            double invDet = 1 / Determinant(),
                   A = r2c2 * r3c3 - r2c3 * r3c2,
                   B = r2c1 * r3c3 - r2c3 * r3c1,
                   C = r2c1 * r3c2 - r2c2 * r3c1,
                   D = r2c0 * r3c3 - r2c3 * r3c0,
                   E = r2c0 * r3c2 - r2c2 * r3c0,
                   F = r2c0 * r3c1 - r2c1 * r3c0,
                   G = r1c2 * r3c3 - r1c3 * r3c2,
                   H = r1c1 * r3c3 - r1c3 * r3c1,
                   I = r1c1 * r3c2 - r1c2 * r3c1,
                   J = r1c0 * r3c3 - r1c3 * r3c0,
                   K = r1c0 * r3c2 - r1c2 * r3c0,
                   L = r1c0 * r3c1 - r1c1 * r3c0,
                   M = r1c2 * r2c3 - r1c3 * r2c2,
                   N = r1c1 * r2c3 - r1c3 * r2c1,
                   O = r1c1 * r2c2 - r1c2 * r2c1,
                   P = r1c0 * r2c3 - r1c3 * r2c0,
                   Q = r1c0 * r2c2 - r1c2 * r2c0,
                   R = r1c0 * r2c1 - r1c1 * r2c0;
            return new Matrix4x4(invDet * (r1c1 * A - r1c2 * B + r1c3 * C), invDet * (r0c2 * B - r0c3 * C - r0c1 * A), invDet * (r0c1 * G - r0c2 * H + r0c3 * I), invDet * (r0c2 * N - r0c3 * O - r0c1 * M),
                                 invDet * (r1c2 * D - r1c3 * E - r1c0 * A), invDet * (r0c0 * A - r0c2 * D + r0c3 * E), invDet * (r0c2 * J - r0c3 * K - r0c0 * G), invDet * (r0c0 * M - r0c2 * P + r0c3 * Q),
                                 invDet * (r1c0 * B - r1c1 * D + r1c3 * F), invDet * (r0c1 * D - r0c3 * F - r0c0 * B), invDet * (r0c0 * H - r0c1 * J + r0c3 * L), invDet * (r0c1 * P - r0c3 * R - r0c0 * N),
                                 invDet * (r1c1 * E - r1c2 * F - r1c0 * C), invDet * (r0c0 * C - r0c1 * E + r0c2 * F), invDet * (r0c1 * K - r0c2 * L - r0c0 * I), invDet * (r0c0 * O - r0c1 * Q + r0c2 * R));
        }
        public Matrix4x4 Transpose() =>
            new Matrix4x4(r0c0, r1c0, r2c0, r3c0,
                          r0c1, r1c1, r2c1, r3c1,
                          r0c2, r1c2, r2c2, r3c2,
                          r0c3, r1c3, r2c3, r3c3);
        public Matrix3x3 Submatrix(int row, int column)
        {
            switch (row)
            {
                case 0:
                    switch (column)
                    {
                        case 0: return new Matrix3x3(r1c1, r1c2, r1c3, r2c1, r2c2, r2c3, r3c1, r3c2, r3c3);
                        case 1: return new Matrix3x3(r1c0, r1c2, r1c3, r2c0, r2c2, r2c3, r3c0, r3c2, r3c3);
                        case 2: return new Matrix3x3(r1c0, r1c1, r1c3, r2c0, r2c1, r2c3, r3c0, r3c1, r3c3);
                        case 3: return new Matrix3x3(r1c0, r1c1, r1c2, r2c0, r2c1, r2c2, r3c0, r3c1, r3c2);
                        default: throw new ArgumentOutOfRangeException(nameof(column));
                    }
                case 1:
                    switch (column)
                    {
                        case 0: return new Matrix3x3(r0c1, r0c2, r0c3, r2c1, r2c2, r2c3, r3c1, r3c2, r3c3);
                        case 1: return new Matrix3x3(r0c0, r0c2, r0c3, r2c0, r2c2, r2c3, r3c0, r3c2, r3c3);
                        case 2: return new Matrix3x3(r0c0, r0c1, r0c3, r2c0, r2c1, r2c3, r3c0, r3c1, r3c3);
                        case 3: return new Matrix3x3(r0c0, r0c1, r0c2, r2c0, r2c1, r2c2, r3c0, r3c1, r3c2);
                        default: throw new ArgumentOutOfRangeException(nameof(column));
                    }
                case 2:
                    switch (column)
                    {
                        case 0: return new Matrix3x3(r0c1, r0c2, r0c3, r1c1, r1c2, r1c3, r3c1, r3c2, r3c3);
                        case 1: return new Matrix3x3(r0c0, r0c2, r0c3, r1c0, r1c2, r1c3, r3c0, r3c2, r3c3);
                        case 2: return new Matrix3x3(r0c0, r0c1, r0c3, r1c0, r1c1, r1c3, r3c0, r3c1, r3c3);
                        case 3: return new Matrix3x3(r0c0, r0c1, r0c2, r1c0, r1c1, r1c2, r3c0, r3c1, r3c2);
                        default: throw new ArgumentOutOfRangeException(nameof(column));
                    }
                case 3:
                    switch (column)
                    {
                        case 0: return new Matrix3x3(r0c1, r0c2, r0c3, r1c1, r1c2, r1c3, r2c1, r2c2, r2c3);
                        case 1: return new Matrix3x3(r0c0, r0c2, r0c3, r1c0, r1c2, r1c3, r2c0, r2c2, r2c3);
                        case 2: return new Matrix3x3(r0c0, r0c1, r0c3, r1c0, r1c1, r1c3, r2c0, r2c1, r2c3);
                        case 3: return new Matrix3x3(r0c0, r0c1, r0c2, r1c0, r1c1, r1c2, r2c0, r2c1, r2c2);
                        default: throw new ArgumentOutOfRangeException(nameof(column));
                    }
                default: throw new ArgumentOutOfRangeException(nameof(row));
            }
        }
        public double Trace() => r0c0 + r1c1 + r2c2 + r3c3;

        public IEnumerator<double> GetEnumerator()
        {
            yield return r0c0; yield return r0c1; yield return r0c2; yield return r0c3;
            yield return r1c0; yield return r1c1; yield return r1c2; yield return r1c3;
            yield return r2c0; yield return r2c1; yield return r2c2; yield return r2c3;
            yield return r3c0; yield return r3c1; yield return r3c2; yield return r3c3;
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

#if CS8_OR_GREATER
        public bool Equals(Matrix4x4? other) =>
#else
        public bool Equals(Matrix4x4 other) =>
#endif
            !(other is null) &&
            r0c0 == other.r0c0 && r0c1 == other.r0c1 && r0c2 == other.r0c2 && r0c3 == other.r0c3 &&
            r1c0 == other.r1c0 && r1c1 == other.r1c1 && r1c2 == other.r1c2 && r1c3 == other.r1c3 &&
            r2c0 == other.r2c0 && r2c1 == other.r2c1 && r2c2 == other.r2c2 && r2c3 == other.r2c3 &&
            r3c0 == other.r3c0 && r3c1 == other.r3c1 && r3c2 == other.r3c2 && r3c3 == other.r3c3;
#if CS8_OR_GREATER
        public override bool Equals(object? other)
#else
        public override bool Equals(object other)
#endif
        {
            if (other is null) return false;
            else if (other is Matrix4x4 otherMat) return Equals(otherMat);
            else return false;
        }
        public override int GetHashCode() =>
            (int)((uint)r0c0.GetHashCode() & 0xC0000000 |
                  (uint)r0c1.GetHashCode() & 0x30000000 |
                  (uint)r0c2.GetHashCode() & 0x0C000000 |
                  (uint)r0c3.GetHashCode() & 0x03000000 |
                  (uint)r1c0.GetHashCode() & 0x00C00000 |
                  (uint)r1c1.GetHashCode() & 0x00300000 |
                  (uint)r1c2.GetHashCode() & 0x000C0000 |
                  (uint)r1c3.GetHashCode() & 0x00030000 |
                  (uint)r2c0.GetHashCode() & 0x0000C000 |
                  (uint)r2c1.GetHashCode() & 0x00003000 |
                  (uint)r2c2.GetHashCode() & 0x00000C00 |
                  (uint)r2c3.GetHashCode() & 0x00000300 |
                  (uint)r3c0.GetHashCode() & 0x000000C0 |
                  (uint)r3c1.GetHashCode() & 0x00000030 |
                  (uint)r3c2.GetHashCode() & 0x0000000C |
                  (uint)r3c3.GetHashCode() & 0x00000003);
        public override string ToString() => ToStringHelper.MatrixToString(this, null);
#if CS8_OR_GREATER
        public string ToString(string? format) => ToStringHelper.MatrixToString(this, format);
        public string ToString(string? format, IFormatProvider? provider) => ToStringHelper.MatrixToString(this, format);
#else
        public string ToString(string format) => ToStringHelper.MatrixToString(this, format);
        public string ToString(string format, IFormatProvider provider) => ToStringHelper.MatrixToString(this, format);
#endif

        public static Matrix4x4 operator +(Matrix4x4 a) =>
            new Matrix4x4(a.r0c0, a.r0c1, a.r0c2, a.r0c3,
                          a.r1c0, a.r1c1, a.r1c2, a.r1c3,
                          a.r2c0, a.r2c1, a.r2c2, a.r2c3,
                          a.r3c0, a.r3c1, a.r3c2, a.r3c3);
        public static Matrix4x4 operator +(Matrix4x4 a, Matrix4x4 b) =>
            new Matrix4x4(a.r0c0 + b.r0c0, a.r0c1 + b.r0c1, a.r0c2 + b.r0c2, a.r0c3 + b.r0c3,
                          a.r1c0 + b.r1c0, a.r1c1 + b.r1c1, a.r1c2 + b.r1c2, a.r1c3 + b.r1c3,
                          a.r2c0 + b.r2c0, a.r2c1 + b.r2c1, a.r2c2 + b.r2c2, a.r2c3 + b.r2c3,
                          a.r3c0 + b.r3c0, a.r3c1 + b.r3c1, a.r3c2 + b.r3c2, a.r3c3 + b.r3c3);
        public static Matrix4x4 operator -(Matrix4x4 a) =>
            new Matrix4x4(-a.r0c0, -a.r0c1, -a.r0c2, -a.r0c3,
                          -a.r1c0, -a.r1c1, -a.r1c2, -a.r1c3,
                          -a.r2c0, -a.r2c1, -a.r2c2, -a.r2c3,
                          -a.r3c0, -a.r3c1, -a.r3c2, -a.r3c3);
        public static Matrix4x4 operator -(Matrix4x4 a, Matrix4x4 b) =>
            new Matrix4x4(a.r0c0 - b.r0c0, a.r0c1 - b.r0c1, a.r0c2 - b.r0c2, a.r0c3 - b.r0c3,
                          a.r1c0 - b.r1c0, a.r1c1 - b.r1c1, a.r1c2 - b.r1c2, a.r1c3 - b.r1c3,
                          a.r2c0 - b.r2c0, a.r2c1 - b.r2c1, a.r2c2 - b.r2c2, a.r2c3 - b.r2c3,
                          a.r3c0 - b.r3c0, a.r3c1 - b.r3c1, a.r3c2 - b.r3c2, a.r3c3 - b.r3c3);
        public static Matrix4x4 operator *(Matrix4x4 a, double b) =>
            new Matrix4x4(a.r0c0 * b, a.r0c1 * b, a.r0c2 * b, a.r0c3 * b,
                          a.r1c0 * b, a.r1c1 * b, a.r1c2 * b, a.r1c3 * b,
                          a.r2c0 * b, a.r2c1 * b, a.r2c2 * b, a.r2c3 * b,
                          a.r3c0 * b, a.r3c1 * b, a.r3c2 * b, a.r3c3 * b);
        public static Float4 operator *(Matrix4x4 a, Float4 b) =>
            new Float4(a.r0c0 * b.w + a.r0c1 * b.x + a.r0c2 * b.y + a.r0c3 * b.z,
                       a.r1c0 * b.w + a.r1c1 * b.x + a.r1c2 * b.y + a.r1c3 * b.z,
                       a.r2c0 * b.w + a.r2c1 * b.x + a.r2c2 * b.y + a.r2c3 * b.z,
                       a.r3c0 * b.w + a.r3c1 * b.x + a.r3c2 * b.y + a.r3c3 * b.z);
        public static Matrix4x4 operator *(Matrix4x4 a, Matrix4x4 b) =>
            new Matrix4x4(a.r0c0 * b.r0c0 + a.r0c1 * b.r1c0 + a.r0c2 * b.r2c0 + a.r0c3 * b.r3c0, a.r0c0 * b.r0c1 + a.r0c1 * b.r1c1 + a.r0c2 * b.r2c1 + a.r0c3 * b.r3c1, a.r0c0 * b.r0c2 + a.r0c1 * b.r1c2 + a.r0c2 * b.r2c2 + a.r0c3 * b.r3c2, a.r0c0 * b.r0c3 + a.r0c1 * b.r1c3 + a.r0c2 * b.r2c3 + a.r0c3 * b.r3c3,
                          a.r1c0 * b.r0c0 + a.r1c1 * b.r1c0 + a.r1c2 * b.r2c0 + a.r1c3 * b.r3c0, a.r1c0 * b.r0c1 + a.r1c1 * b.r1c1 + a.r1c2 * b.r2c1 + a.r1c3 * b.r3c1, a.r1c0 * b.r0c2 + a.r1c1 * b.r1c2 + a.r1c2 * b.r2c2 + a.r1c3 * b.r3c2, a.r1c0 * b.r0c3 + a.r1c1 * b.r1c3 + a.r1c2 * b.r2c3 + a.r1c3 * b.r3c3,
                          a.r2c0 * b.r0c0 + a.r2c1 * b.r1c0 + a.r2c2 * b.r2c0 + a.r2c3 * b.r3c0, a.r2c0 * b.r0c1 + a.r2c1 * b.r1c1 + a.r2c2 * b.r2c1 + a.r2c3 * b.r3c1, a.r2c0 * b.r0c2 + a.r2c1 * b.r1c2 + a.r2c2 * b.r2c2 + a.r2c3 * b.r3c2, a.r2c0 * b.r0c3 + a.r2c1 * b.r1c3 + a.r2c2 * b.r2c3 + a.r2c3 * b.r3c3,
                          a.r3c0 * b.r0c0 + a.r3c1 * b.r1c0 + a.r3c2 * b.r2c0 + a.r3c3 * b.r3c0, a.r3c0 * b.r0c1 + a.r3c1 * b.r1c1 + a.r3c2 * b.r2c1 + a.r3c3 * b.r3c1, a.r3c0 * b.r0c2 + a.r3c1 * b.r1c2 + a.r3c2 * b.r2c2 + a.r3c3 * b.r3c2, a.r3c0 * b.r0c3 + a.r3c1 * b.r1c3 + a.r3c2 * b.r2c3 + a.r3c3 * b.r3c3);
        public static Matrix4x4 operator /(Matrix4x4 a, double b) =>
            new Matrix4x4(a.r0c0 / b, a.r0c1 / b, a.r0c2 / b, a.r0c3 / b,
                          a.r1c0 / b, a.r1c1 / b, a.r1c2 / b, a.r1c3 / b,
                          a.r2c0 / b, a.r2c1 / b, a.r2c2 / b, a.r2c3 / b,
                          a.r3c0 / b, a.r3c1 / b, a.r3c2 / b, a.r3c3 / b);
        public static Matrix4x4 operator ^(Matrix4x4 a, Matrix4x4 b) =>
            new Matrix4x4(a.r0c0 * b.r0c0, a.r0c1 * b.r0c1, a.r0c2 * b.r0c2, a.r0c3 * b.r0c3,
                          a.r1c0 * b.r1c0, a.r1c1 * b.r1c1, a.r1c2 * b.r1c2, a.r1c3 * b.r1c3,
                          a.r2c0 * b.r2c0, a.r2c1 * b.r2c1, a.r2c2 * b.r2c2, a.r2c3 * b.r2c3,
                          a.r3c0 * b.r3c0, a.r3c1 * b.r3c1, a.r3c2 * b.r3c2, a.r3c3 * b.r3c3);
        public static Matrix4x4 operator ~(Matrix4x4 a) => a.Inverse();
        public static bool operator ==(Matrix4x4 a, Matrix4x4 b) => a.Equals(b);
        public static bool operator !=(Matrix4x4 a, Matrix4x4 b) => !a.Equals(b);

        public static explicit operator Matrix4x4(Matrix mat) =>
            new Matrix4x4(mat.TryGet(0, 0), mat.TryGet(0, 1), mat.TryGet(0, 2), mat.TryGet(0, 3),
                          mat.TryGet(1, 0), mat.TryGet(1, 1), mat.TryGet(1, 2), mat.TryGet(1, 3),
                          mat.TryGet(2, 0), mat.TryGet(2, 1), mat.TryGet(2, 2), mat.TryGet(2, 3),
                          mat.TryGet(3, 0), mat.TryGet(3, 1), mat.TryGet(3, 2), mat.TryGet(3, 3));
        public static implicit operator Matrix4x4(Matrix2x2 mat) =>
            new Matrix4x4(1, 0       , 0       , 0,
                          0, mat.r0c0, mat.r0c1, 0,
                          0, mat.r1c0, mat.r1c1, 0,
                          0, 0       , 0       , 1);
        public static implicit operator Matrix4x4(Matrix3x3 mat) =>
            new Matrix4x4(1, 0       , 0       , 0       ,
                          0, mat.r0c0, mat.r0c1, mat.r0c2,
                          0, mat.r1c0, mat.r1c1, mat.r1c2,
                          0, mat.r2c0, mat.r2c1, mat.r2c2);
    }
}
