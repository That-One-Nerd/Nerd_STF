using Nerd_STF.Mathematics;
using Nerd_STF.Mathematics.Algebra;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nerd_STF.UnitTests.Mathematics.Algebra;

[TestClass]
public sealed class MatrixTests
{
    private static Fill<double> Count => i => i + 1;

    [TestMethod] public void TestMatrixConstructionByFill()
    {
        // Specific case, byRows=default (false)
        Matrix m = new((3, 5), Count);
        Assert.AreEqual(new Matrix((3, 5), new double[,]
        {
            {  1,  2,  3,  4,  5 },
            {  6,  7,  8,  9, 10 },
            { 11, 12, 13, 14, 15 }
        }), m, $"{nameof(Matrix)}({nameof(Fill<>)}) construction failed.");

        // Specific case, byRows=true
        m = new((3, 5), Count, true);
        Assert.AreEqual(new Matrix((3, 5), new double[,]
        {
            {  1,  4,  7, 10, 13 },
            {  2,  5,  8, 11, 14 },
            {  3,  6,  9, 12, 15 }
        }), m, $"{nameof(Matrix)}({nameof(Fill<>)}) construction failed.");
    }

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

    [TestMethod] public void TestIndexingMatrix2x2() => TestIndexing(new Matrix2x2(Count));
    [TestMethod] public void TestIndexingMatrix3x3() => TestIndexing(new Matrix3x3(Count));
    [TestMethod] public void TestIndexingMatrix4x4() => TestIndexing(new Matrix4x4(Count));
    [TestMethod] public void TestIndexingDynamicMatrix()
    {
        TestIndexing(new Matrix((5, 5), Count));
        TestIndexing(new Matrix((3, 5), Count));
        TestIndexing(new Matrix((5, 3), Count));
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

    [TestMethod] public void TestRangeMatrix2x2() => TestRange(new Matrix2x2(Count));
    [TestMethod] public void TestRangeMatrix3x3() => TestRange(new Matrix3x3(Count));
    [TestMethod] public void TestRangeMatrix4x4() => TestRange(new Matrix4x4(Count));
    [TestMethod] public void TestRangeDynamicMatrix()
    {
        TestRange(new Matrix((5, 5), Count));
        TestRange(new Matrix((3, 5), Count));
        TestRange(new Matrix((5, 3), Count));
    }
    private static void TestRange<T>(T matrix) where T : IMatrix<T>
    {
        // General test
        Random rand = new();
        for (int rMin = 0; rMin <= matrix.Size.x; rMin++)
        {
            for (int rMax = rMin; rMax <= matrix.Size.x; rMax++)
            {
                for (int cMin = 0; cMin <= matrix.Size.y; cMin++)
                {
                    for (int cMax = cMin; cMax <= matrix.Size.y; cMax++)
                    {
                        Matrix subWrite = new((rMax - rMin, cMax - cMin), (r, c) => rand.NextDouble());
                        matrix[rMin..rMax, cMin..cMax] = subWrite;

                        Matrix subRead = matrix[rMin..rMax, cMin..cMax];
                        Assert.AreEqual(subWrite, subRead, "Submatrices failed: read and write disagree.");

                        int invRmin = matrix.Size.x - rMin,
                            invRmax = matrix.Size.x - rMax,
                            invCmin = matrix.Size.y - cMin,
                            invCmax = matrix.Size.y - cMax;

                        // Probably don't need all 15, 3 would probably work.
                        Assert.AreEqual(subRead, matrix[rMin..rMax, cMin..^invCmax]);
                        Assert.AreEqual(subRead, matrix[rMin..rMax, ^invCmin..cMax]);
                        Assert.AreEqual(subRead, matrix[rMin..rMax, ^invCmin..^invCmax]);
                        Assert.AreEqual(subRead, matrix[rMin..^invRmax, cMin..cMax]);
                        Assert.AreEqual(subRead, matrix[rMin..^invRmax, cMin..^invCmax]);
                        Assert.AreEqual(subRead, matrix[rMin..^invRmax, ^invCmin..cMax]);
                        Assert.AreEqual(subRead, matrix[rMin..^invRmax, ^invCmin..^invCmax]);
                        Assert.AreEqual(subRead, matrix[^invRmin..rMax, cMin..cMax]);
                        Assert.AreEqual(subRead, matrix[^invRmin..rMax, cMin..^invCmax]);
                        Assert.AreEqual(subRead, matrix[^invRmin..rMax, ^invCmin..cMax]);
                        Assert.AreEqual(subRead, matrix[^invRmin..rMax, ^invCmin..^invCmax]);
                        Assert.AreEqual(subRead, matrix[^invRmin..^invRmax, cMin..cMax]);
                        Assert.AreEqual(subRead, matrix[^invRmin..^invRmax, cMin..^invCmax]);
                        Assert.AreEqual(subRead, matrix[^invRmin..^invRmax, ^invCmin..cMax]);
                        Assert.AreEqual(subRead, matrix[^invRmin..^invRmax, ^invCmin..^invCmax]);
                    }
                }
            }
        }
    }

    [TestMethod] public void TestGaussElimination()
    {
        // Specific case. Not super sure how this could be generalized.
        Matrix m = new((4, 4), new double[,]
        {
            { 1, 0, 4, 2 },
            { 1, 2, 6, 2 },
            { 2, 0, 8, 8 },
            { 2, 1, 9, 4 }
        });

        m.GaussElimination();
        Assert.AreEqual(new Matrix((4, 4), new double[,]
        {
            { 1, 0, 4, 2 },
            { 0, 1, 1, 0 },
            { 0, 0, 0, 4 },
            { 0, 0, 0, 0 }
        }), m, "Gaussian elimination failure.");
    }

    [TestMethod] public void TestMatrixBarString()
    {
        // Specific case.
        Matrix m = new((4, 5), (r, c) => 5 * r + c + 1);
        Assert.AreEqual("┌                  ┐" + Environment.NewLine +
                        "│  1  2  3  4 │  5 │" + Environment.NewLine +
                        "│  6  7  8  9 │ 10 │" + Environment.NewLine +
                        "│ 11 12 13 14 │ 15 │" + Environment.NewLine +
                        "│ 16 17 18 19 │ 20 │" + Environment.NewLine +
                        "└                  ┘", m.ToString(bar: true));

        // General case. There should always be 3 line symbols for each row.
        //               One for the bar and two for the edges.
        for (int rows = 0; rows <= 10; rows++)
        {
            for (int cols = 0; cols <= 10; cols++)
            {
                m = new((rows, cols), (r, c) => cols * r + c + 1);
                int lines = m.ToString(bar: true).Count('│');
                if (cols >= 2) Assert.AreEqual(3 * rows, lines);
                else Assert.AreEqual(2 * rows, lines);
            }
        }
    }

    [TestMethod] public void TestMultiplication2x2()
    {
        // General test.
        TestGeneralMultiplication<Matrix2x2>();

        // Specific test: (2x2)x(2x2)
        Matrix2x2 m1 = new(new double[,]
        {
            { 7, 1 },
            { 6, 5 }
        });
        Matrix2x2 m2 = new(new double[,]
        {
            { 2, 7 },
            { 5, 0 }
        });
        Assert.AreEqual(new Matrix2x2(new double[,]
        {
            { 19, 49 },
            { 37, 42 }
        }), m1 * m2);
        Assert.AreEqual(new Matrix2x2(new double[,]
        {
            { 56, 37 },
            { 35,  5 }
        }), m2 * m1);

        Assert.AreEqual((44, 46), m1 * (6, 2)); // Specific test: (2x2)x(2x1)
        Assert.AreEqual((41, 15), m2 * (3, 5)); // ...
    }
    [TestMethod] public void TestMultiplication3x3()
    {
        // General test.
        TestGeneralMultiplication<Matrix3x3>();

        // Specific test: (3x3)x(3x3)
        Matrix3x3 m1 = new(new double[,]
        {
            { 4, 8, 3 },
            { 2, 9, 5 },
            { 9, 2, 8 }
        });
        Matrix3x3 m2 = new(new double[,]
        {
            { 5, 4, 2 },
            { 1, 7, 6 },
            { 1, 5, 5 }
        });
        Assert.AreEqual(new Matrix3x3(new double[,]
        {
            { 31, 87, 71 },
            { 24, 96, 83 },
            { 55, 90, 70 }
        }), m1 * m2);
        Assert.AreEqual(new Matrix3x3(new double[,]
        {
            { 46, 80, 51 },
            { 72, 83, 86 },
            { 59, 63, 68 }
        }), m2 * m1);

        Assert.AreEqual((105, 122, 101), m1 * (3, 9, 7)); // Specific test: (3x3)x(3x1)
        Assert.AreEqual(( 58,  83,  64), m2 * (4, 7, 5)); // ...
    }
    [TestMethod] public void TestMultiplication4x4()
    {
        // General test.
        TestGeneralMultiplication<Matrix4x4>();

        // Specific test: (4x4)x(4x4)
        Matrix4x4 m1 = new(new double[,]
        {
            { 1, 6, 5, 3 },
            { 5, 7, 3, 1 },
            { 4, 4, 9, 3 },
            { 2, 1, 6, 5 }
        });
        Matrix4x4 m2 = new(new double[,]
        {
            { 4, 6, 9, 5 },
            { 8, 5, 6, 9 },
            { 1, 6, 9, 3 },
            { 5, 1, 6, 2 }
        });
        Assert.AreEqual(new Matrix4x4(new double[,]
        {
            {  72,  69, 108,  80 },
            {  84,  84, 120,  99 },
            {  72, 101, 159,  89 },
            {  47,  58, 108,  47 }
        }), m1 * m2);
        Assert.AreEqual(new Matrix4x4(new double[,]
        {
            {  80, 107, 149,  70 },
            {  75, 116, 163,  92 },
            {  73,  87, 122,  51 },
            {  38,  63,  94,  44 }
        }), m2 * m1);

        Assert.AreEqual(( 48,  62,  63,  41), m1 * (5, 4, 2, 3)); // Specific test: (4x4)x(4x1)
        Assert.AreEqual(( 88, 137,  54,  59), m2 * (6, 1, 2, 8)); // ...
    }
    private static void TestGeneralMultiplication<T>() where T : IStaticMatrix<T>
    {
        T mat = T.Identity;
        for (int r = 0; r < T.Size.x; r++)
        {
            for (int c = 0; c < T.Size.y; c++)
            {
                mat[r, c] = r * T.Size.y + c + 1;
            }
        }

        // Scalar multiplication.
        AssertMatrix((r, c) => mat[r, c] * 2, mat * 2);
        AssertMatrix((r, c) => mat[r, c] * -3.5, mat * -3.5);

        Assert.AreEqual(mat, mat * T.Identity); // A * I = A
        Assert.AreEqual(T.Zero, mat * T.Zero);  // A * 0 = 0

        Assert.AreEqual(mat * 2.3, mat * (T.Identity * 2.3)); // A * 2.3I = 2.3A

        // Multiply by a matrix that's all ones.
        // When you do that, the result is a matrix where
        // all elements of a row are equal to the sum of
        // the elements in that row of the previous matrix.
        // For example:
        // [ 1 2 ]   [ 1 1 ]   [ 3 3 ]
        // [ 3 4 ] * [ 1 1 ] = [ 7 7 ]
        AssertMatrix((r, c) => mat.GetRow(r).Sum(), mat * T.One);
        AssertMatrix((r, c) => mat.GetRow(r).Sum() * 3.4, mat * (T.One * 3.4), delta: 1e-4);

        // One more true test. Cast to a dynamic matrix and multiply.
        // The two results should always be the same.
        T m1 = T.Identity, m2 = T.Identity;
        Random rand = new();
        for (int i = 0; i < 100; i++)
        {
            for (int r = 0; r < T.Size.x; r++)
            {
                for (int c = 0; c < T.Size.y; c++)
                {
                    m1[r, c] = rand.NextDouble();
                    m2[r, c] = rand.NextDouble();
                }
            }
            Assert.AreEqual(m1 * m2, (Matrix)m1 * (Matrix)m2);
        }
    }

    [TestMethod] public void TestRotation2x2()
    {
        double rad;
        Angle rot;
        Matrix2x2 mat;
        Random rand = new();

        for (int i = 0; i < 1000; i++)
        {
            rad = rand.NextDouble() * Math.PI * 2;
            rot = Angle.FromRadians(rad);
            mat = Matrix2x2.Rotation(rot);

            Float2 input = (10 * rand.NextDouble(), 10 * rand.NextDouble());

            Float2 expected = (input.x * Math.Cos(rad) - input.y * Math.Sin(rad), input.y * Math.Cos(rad) + input.x * Math.Sin(rad));
            Float2 actual = mat * input;

            AssertVals(expected, actual, 1e-4);
        }
    }
    [TestMethod] public void TestRotation3x3()
    {
        // General test. Rotation along a specific axis should be the same
        // as the Matrix2x2 rotation.
        Random rand = new();
        for (double r = 0; r < 1; r += 1e-3)
        {
            Angle rot2d = Angle.FromRevolutions(r);
            Float2 input2d = (rand.NextDouble(), rand.NextDouble());
            Matrix2x2 mat2d = Matrix2x2.Rotation(rot2d);

            Float2 expected2d = mat2d * input2d;

            AssertVals(expected2d, (Matrix3x3.Rotation(Angle.Zero, Angle.Zero, rot2d) * (input2d.x, input2d.y, 0))["xy"], 1e-4, $"Rotation around Z doesn't line up.");
            AssertVals(expected2d, (Matrix3x3.Rotation(Angle.Zero, rot2d, Angle.Zero) * (input2d.x, 0, input2d.y))["xz"], 1e-4, $"Rotation around Y doesn't line up.");
            AssertVals(expected2d, (Matrix3x3.Rotation(rot2d, Angle.Zero, Angle.Zero) * (0, input2d.x, input2d.y))["yz"], 1e-4, $"Rotation around X doesn't line up.");
        }

        // I'll need a specific test sometime.
        // I also want to visualize this and make sure I'm on the right track.
    }

    private static void AssertMatrix<T>(Fill2d<double> expected, T actual, double delta = 0, string message = "") where T : IMatrix<T>
    {
        for (int r = 0; r < actual.Size.x; r++)
        {
            for (int c = 0; c < actual.Size.y; c++)
            {
                double valExpected = expected(r, c);
                double valActual = actual[r, c];
                Assert.AreEqual(valExpected, valActual, delta, message);
            }
        }
    }
    private static void AssertVals(IEnumerable<double> expected, IEnumerable<double> actual, double delta = 0, string message = "")
    {
        IEnumerator<double> expectedEnum = expected.GetEnumerator();
        IEnumerator<double> actualEnum = actual.GetEnumerator();
        while (expectedEnum.MoveNext() && actualEnum.MoveNext())
        {
            Assert.AreEqual(expectedEnum.Current, actualEnum.Current, delta, message, expected.ToString()!, actual.ToString()!);
        }

        if (expectedEnum.MoveNext() || actualEnum.MoveNext()) Assert.Fail(message);
    }
}
