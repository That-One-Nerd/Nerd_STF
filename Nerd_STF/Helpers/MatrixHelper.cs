using Nerd_STF.Mathematics;
using Nerd_STF.Mathematics.Algebra;
using System.Collections.Generic;

namespace Nerd_STF.Helpers
{
    internal static class MatrixHelper
    {
        public static void SetMatrixValues<TMat>(TMat matrix, IEnumerable<IEnumerable<double>> vals, bool byRows)
            where TMat : IMatrix<TMat>
        {
            Int2 size = matrix.Size;
            int x = 0;
            foreach (IEnumerable<double> part in vals)
            {
                int y = 0;
                foreach (double v in part)
                {
                    matrix[byRows ? (y, x) : (x, y)] = v;
                    y++;
                    if (byRows ? y >= size.y : y >= size.x) break;
                }
                x++;
                if (byRows ? x >= size.x : x >= size.y) break;
            }
        }
        public static void SetMatrixValues<TMat>(TMat matrix, IEnumerable<ListTuple<double>> vals, bool byRows)
            where TMat : IMatrix<TMat>
        {
            // Literally the same code. Sucks that casting doesn't work here.
            Int2 size = matrix.Size;
            int x = 0;
            foreach (IEnumerable<double> part in vals)
            {
                int y = 0;
                foreach (double v in part)
                {
                    matrix[byRows ? (y, x) : (x, y)] = v;
                    y++;
                    if (byRows ? y >= size.y : y >= size.x) break;
                }
                x++;
                if (byRows ? x >= size.x : x >= size.y) break;
            }
        }
    
        public static void SetRow<TMat>(TMat matrix, int row, IEnumerable<double> vals)
            where TMat : IMatrix<TMat>
        {
            int col = 0;
            int max = matrix.Size.y;
            foreach (double v in vals)
            {
                matrix[row, col] = v;
                col++;
                if (col >= max) return;
            }
        }
        public static void SetColumn<TMat>(TMat matrix, int col, IEnumerable<double> vals)
            where TMat : IMatrix<TMat>
        {
            int row = 0;
            int max = matrix.Size.x;
            foreach (double v in vals)
            {
                matrix[row, col] = v;
                row++;
                if (row >= max) return;
            }
        }
    }
}
