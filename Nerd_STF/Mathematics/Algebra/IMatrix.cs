using System;
using System.Collections.Generic;

namespace Nerd_STF.Mathematics.Algebra
{
    public interface IMatrix<TSelf> : IEnumerable<double>,
                                      IEquatable<TSelf>,
                                      IFormattable
#if CS11_OR_GREATER
                                     ,IMatrixOperations<TSelf>
#endif
        where TSelf : IMatrix<TSelf>
    {
        Int2 Size { get; }

        double this[int r, int c] { get; set; }
        double this[Int2 index] { get; set; }
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
    }
}
