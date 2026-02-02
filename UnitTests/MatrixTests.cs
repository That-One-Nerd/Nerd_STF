using Nerd_STF.Mathematics.Algebra;
using System;

namespace Nerd_STF.UnitTests;

[TestClass]
public sealed class MatrixTests
{
    [TestMethod] public void TestRowOperationsAcrossMatrix2x2() => TestRowOperationsAcrossMatrixTypes<Matrix2x2>();
    [TestMethod] public void TestRowOperationsAcrossMatrix3x3() => TestRowOperationsAcrossMatrixTypes<Matrix3x3>();
    [TestMethod] public void TestRowOperationsAcrossMatrix4x4() => TestRowOperationsAcrossMatrixTypes<Matrix4x4>();
    private void TestRowOperationsAcrossMatrixTypes<T>() where T : IStaticMatrix<T>
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
}
