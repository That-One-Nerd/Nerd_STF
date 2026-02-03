using Nerd_STF.Mathematics;
using Nerd_STF.Mathematics.Algebra;
using System;

namespace Nerd_STF.UnitTests;

[TestClass]
public sealed class MatrixTests
{
    [TestMethod] public void TestRowOperationsAcrossMatrix2x2() => TestRowOperationsAcrossMatrixTypes<Matrix2x2>();
    [TestMethod] public void TestRowOperationsAcrossMatrix3x3() => TestRowOperationsAcrossMatrixTypes<Matrix3x3>();
    [TestMethod] public void TestRowOperationsAcrossMatrix4x4() => TestRowOperationsAcrossMatrixTypes<Matrix4x4>();
    private static void TestRowOperationsAcrossMatrixTypes<T>() where T : IStaticMatrix<T>
    {
        // If I did my row operations correctly, there should be no difference
        // in the results for a static matrix and a casted matrix. This tests that.

        T mat = T.Identity;
        double val = 1;
        for (int r = 0; r < T.Size.x; r++)
        {
            for (int c = 0; c < T.Size.y; c++)
            {
                mat[r, c] = val;
                val++;
            }
        }

        Matrix casted = mat;
        Assert.AreEqual(mat, casted, $"Casting {typeof(T).Name} to {nameof(Matrix)} has failed!");

        // First, try swapping a bunch of rows.
        for (int r1 = 0; r1 < T.Size.x; r1++)
        {
            for (int r2 = 0; r2 < T.Size.x; r2++)
            {
                mat.SwapRows(r1, r2);
                casted.SwapRows(r1, r2);
                Assert.AreEqual(casted, mat, $"{nameof(Matrix.SwapRows)} for {nameof(Matrix)} and {typeof(T).Name} do not agree when swapping rows r{r1} <-> r{r2}");
            }
        }

        // Now scale all the rows by some factor.
        Random rand = new();
        for (int r = 0; r < T.Size.x; r++)
        {
            double factor = rand.NextDouble();
            mat.ScaleRow(r, factor);
            casted.ScaleRow(r, factor);
            Assert.AreEqual(casted, mat, $"{nameof(Matrix.ScaleRow)} for {nameof(Matrix)} and {typeof(T).Name} do not agree when scaling row r{r} * {factor}");
        }

        // Now add all the rows onto each other.
        for (int r1 = 0; r1 < T.Size.x; r1++)
        {
            for (int r2 = 0; r2 < T.Size.x; r2++)
            {
                double factor = rand.NextDouble();
                mat.AddRow(r1, factor, r2);
                casted.AddRow(r1, factor, r2);
                Assert.AreEqual(casted, mat, $"{nameof(Matrix.AddRow)} for {nameof(Matrix)} and {typeof(T).Name} do not agree when adding rows r{r1} += {factor} * r{r2}");
            }
        }
    }

    [TestMethod] public void TestIndexingMatrix2x2() => TestIndexing(new Matrix2x2(new double[,] { { 1, 2 }, { 3, 4 } }));
    [TestMethod] public void TestIndexingMatrix3x3() => TestIndexing(new Matrix3x3(new double[,] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } }));
    [TestMethod] public void TestIndexingMatrix4x4() => TestIndexing(new Matrix4x4(new double[,] { { 1, 2, 3, 4 }, { 5, 6, 7, 8 }, { 9, 10, 11, 12 }, { 13, 14, 15, 16 } }));
    [TestMethod] public void TestIndexingDynamicMatrix()
    {
        TestIndexing(new Matrix((5, 5), (r, c) => r * 5 + c + 1));
        TestIndexing(new Matrix((2, 5), (r, c) => r * 5 + c + 1));
        TestIndexing(new Matrix((5, 2), (r, c) => r * 2 + c + 1));
    }
    private static void TestIndexing<T>(T matrix) where T : IMatrix<T>
    {
        int r = 0, c = 0;
        foreach (double expected in matrix)
        {
            int rR = matrix.Size.x - r,
                rC = matrix.Size.y - c;

            Assert.AreEqual(expected, matrix[r, c], 0, $"Indexing is invalid for {typeof(T).Name}[{r}, {c}]");
            Assert.AreEqual(expected, matrix[(r, c)], 0, $"Indexing is invalid for {typeof(T).Name}[{nameof(Int2)}({r}, {c})]");
            Assert.AreEqual(expected, matrix[r, RowColumn.Row][c], 0, $"Indexing is invalid for {typeof(T).Name}[{r}, {nameof(RowColumn)}.{nameof(RowColumn.Row)}][{c}]");
            Assert.AreEqual(expected, matrix[c, RowColumn.Column][r], 0, $"Indexing is invalid for {typeof(T).Name}[{c}, {nameof(RowColumn)}.{nameof(RowColumn.Column)}][{r}]");
            Assert.AreEqual(expected, matrix[(Index)r, (Index)c], 0, $"Indexing is invalid for {typeof(T).Name}[{nameof(Index)}({r}), {nameof(Index)}({c})]");
            Assert.AreEqual(expected, matrix[^rR, ^rC], 0, $"Indexing is invalid for {typeof(T).Name}[{nameof(Index)}(^{rR}), {nameof(Index)}(^{rC})]");

            c++;
            if (c == matrix.Size.y)
            {
                r++;
                c = 0;
            }
        }
    }
}
