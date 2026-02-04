using System;
using System.Collections.Generic;

namespace Nerd_STF.Mathematics.Algebra
{
    public interface IMatrix<TSelf> : IEnumerable<double>,
                                      IEquatable<TSelf>,
                                      IFormattable
        where TSelf : IMatrix<TSelf>
    {
        Int2 Size { get; }

        double this[int r, int c] { get; set; }
        double this[Int2 index] { get; set; }
#if CS8_OR_GREATER
        double this[Index r, Index c] { get; set; }
        Matrix this[Range r, Range c] { get; set; }
#endif
        ListTuple<double> this[int index, RowColumn direction] { get; set; }

        ListTuple<double> GetRow(int row);
        ListTuple<double> GetColumn(int column);
        void SetRow(int row, IEnumerable<double> vals);
        void SetColumn(int column, IEnumerable<double> vals);

        double Determinant();

        TSelf Adjoint();
        TSelf Cofactor();
        TSelf Transpose();
#if CS9_OR_GREATER
        TSelf? Inverse();
#else
        TSelf Inverse();
#endif

        void SwapRows(int r1, int r2);
        void ScaleRow(int row, double factor);
        void AddRow(int rDest, double factor, int rSource);

#if CS11_OR_GREATER
        static abstract TSelf operator +(TSelf a, TSelf b);
        static abstract TSelf operator *(TSelf a, TSelf b);
        static abstract TSelf operator *(TSelf a, double b);
        static abstract TSelf operator /(TSelf a, double b);
        static abstract TSelf operator ^(TSelf a, TSelf b);
        static abstract TSelf? operator ~(TSelf m);
#endif
    }
}
