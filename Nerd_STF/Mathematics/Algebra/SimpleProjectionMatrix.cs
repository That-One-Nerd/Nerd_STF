using Nerd_STF.Mathematics.Algebra.Abstract;

namespace Nerd_STF.Mathematics.Algebra;

public class SimpleProjectionMatrix : Matrix3x3,
    IProjectionMatrix<SimpleProjectionMatrix, Matrix3x3, Float3>
{
    public SimpleProjectionMatrix(Matrix3x3 nonProjection) : this(nonProjection.ToFill2D()) { }
    public SimpleProjectionMatrix(float all) : this(all, all, all, all, all, all, all, all, all) { }
    public SimpleProjectionMatrix(float r1c1, float r1c2, float r1c3, float r2c1,
        float r2c2, float r2c3, float r3c1, float r3c2, float r3c3) :
        base(r1c1, r1c2, r1c3, r2c1, r2c2, r2c3, r3c1, r3c2, r3c3) { }
    public SimpleProjectionMatrix(float[] nums) : this(nums[0], nums[1], nums[2],
        nums[3], nums[4], nums[5], nums[6], nums[7], nums[8]) { }
    public SimpleProjectionMatrix(int[] nums) : this(nums[0], nums[1], nums[2],
        nums[3], nums[4], nums[5], nums[6], nums[7], nums[8]) { }
    public SimpleProjectionMatrix(Fill<float> fill) : this(fill(0), fill(1), fill(2),
        fill(3), fill(4), fill(5), fill(6), fill(7), fill(8)) { }
    public SimpleProjectionMatrix(Fill<int> fill) : this(fill(0), fill(1), fill(2),
        fill(3), fill(4), fill(5), fill(6), fill(7), fill(8)) { }
    public SimpleProjectionMatrix(float[,] nums) : this(nums[0, 0], nums[0, 1], nums[0, 2],
        nums[1, 0], nums[1, 1], nums[1, 2], nums[2, 0], nums[2, 1], nums[2, 2]) { }
    public SimpleProjectionMatrix(int[,] nums) : this(nums[0, 0], nums[0, 1], nums[0, 2],
        nums[1, 0], nums[1, 1], nums[1, 2], nums[2, 0], nums[2, 1], nums[2, 2]) { }
    public SimpleProjectionMatrix(Fill2d<float> fill) : this(fill(0, 0), fill(0, 1), fill(0, 2),
        fill(1, 0), fill(1, 1), fill(1, 2), fill(2, 0), fill(2, 1), fill(2, 2)) { }
    public SimpleProjectionMatrix(Fill2d<int> fill) : this(fill(0, 0), fill(0, 1), fill(0, 2),
        fill(1, 0), fill(1, 1), fill(1, 2), fill(2, 0), fill(2, 1), fill(2, 2)) { }
    public SimpleProjectionMatrix(Float3 r1, Float3 r2, Float3 r3) : this(r1.x, r1.y, r1.z, r2.x, r2.y, r2.z, r3.x, r3.y, r3.z) { }
    public SimpleProjectionMatrix(Fill<Float3> fill) : this(fill(0), fill(1), fill(2)) { }
    public SimpleProjectionMatrix(Fill<Int3> fill) : this((IEnumerable<int>)fill(0), fill(1), fill(2)) { }
    public SimpleProjectionMatrix(IEnumerable<float> r1, IEnumerable<float> r2, IEnumerable<float> r3)
        : this(r1.ToFill(), r2.ToFill(), r3.ToFill()) { }
    public SimpleProjectionMatrix(IEnumerable<int> r1, IEnumerable<int> r2, IEnumerable<int> r3)
        : this(r1.ToFill(), r2.ToFill(), r3.ToFill()) { }
    public SimpleProjectionMatrix(Fill<float> r1, Fill<float> r2, Fill<float> r3)
        : this(r1(0), r1(1), r1(2), r2(0), r2(1), r2(2), r3(0), r3(1), r3(2)) { }
    public SimpleProjectionMatrix(Fill<int> r1, Fill<int> r2, Fill<int> r3)
        : this(r1(0), r1(1), r1(2), r2(0), r2(1), r2(2), r3(0), r3(1), r3(2)) { }

    public static SimpleProjectionMatrix SingleView(CrossSection2d section) => new(new[,]
    {
        { section == CrossSection2d.XY || section == CrossSection2d.ZX ? 1 : 0, 0, 0 },
        { 0, section == CrossSection2d.XY || section == CrossSection2d.YZ ? 1 : 0, 0 },
        { 0, 0, section == CrossSection2d.YZ || section == CrossSection2d.ZX ? 1 : 0 }
    });

    public static SimpleProjectionMatrix Isometric()
    {
        // Hand-calculated optimization of an axonometric projection
        // with alpha set to arcsin(tan(30deg)) and beta set to 45deg.

        const float invSqrt2   = 0.707106781187f,
                    invSqrt6   = 0.408248290464f,
                    invSqrt6_2 = 0.816496580928f;
        return new(new[,]
        {
            { invSqrt2,          0, -invSqrt2 },
            { invSqrt6, invSqrt6_2,  invSqrt6 },
            {        0,          0,         0 },
        });
    }
    public static SimpleProjectionMatrix Axonometric(Angle alpha, Angle beta)
    {
        Matrix3x3 alphaMat = new(new[,]
        {
            { 1,                 0,                0 },
            { 0,  Mathf.Cos(alpha), Mathf.Sin(alpha) },
            { 0, -Mathf.Sin(alpha), Mathf.Cos(alpha) }
        });
        Matrix3x3 betaMat = new(new[,]
        {
            { Mathf.Cos(beta), 0, -Mathf.Sin(beta) },
            {               0, 1,                0 },
            { Mathf.Sin(beta), 0,  Mathf.Cos(beta) }
        });
        Matrix3x3 flatten = new(new[,]
        {
            { 1, 0, 0 },
            { 0, 1, 0 },
            { 0, 0, 0 }
        });
        Matrix3x3 result = (alphaMat * betaMat).Transpose() * flatten;
        return new(result.Transpose().ToFill2D());
    }

    public Fill<Float3> Project(Fill<Float3> toProject) => i => this * toProject(i);

    public static SimpleProjectionMatrix Absolute(SimpleProjectionMatrix val) =>
        new(Matrix3x3.Absolute(val));
    public static SimpleProjectionMatrix Average(SimpleProjectionMatrix val) =>
        new(Matrix3x3.Average(val));
    public static SimpleProjectionMatrix Ceiling(SimpleProjectionMatrix val) =>
        new(Matrix3x3.Ceiling(val));
    public static SimpleProjectionMatrix Clamp(SimpleProjectionMatrix val,
        SimpleProjectionMatrix min, SimpleProjectionMatrix max) =>
        new(Matrix3x3.Clamp(val, min, max));
    public static SimpleProjectionMatrix Floor(SimpleProjectionMatrix val) =>
        new(Matrix3x3.Floor(val));
    public static SimpleProjectionMatrix Lerp(SimpleProjectionMatrix a, SimpleProjectionMatrix b,
        float t, bool clamp = true) =>
        new(Matrix3x3.Lerp(a, b, t, clamp));
    public static SimpleProjectionMatrix Median(params SimpleProjectionMatrix[] vals) =>
        new(Matrix3x3.Median(vals));
    public static SimpleProjectionMatrix Round(SimpleProjectionMatrix val) =>
        new(Matrix3x3.Round(val));

    public static (float[] r1c1s, float[] r1c2s, float[] r1c3s, float[] r2c1s, float[] r2c2s,
        float[] r2c3s, float[] r3c1s, float[] r3c2s, float[] r3c3s)
        SplitArray(params SimpleProjectionMatrix[] vals) => Matrix3x3.SplitArray(vals);

    new public SimpleProjectionMatrix Adjugate() => new(base.Adjugate());
    new public SimpleProjectionMatrix Cofactor() => new(base.Cofactor());
    new public SimpleProjectionMatrix? Inverse()
    {
        Matrix3x3? mInverse = base.Inverse();
        if (mInverse is null) return null;
        return new(mInverse);
    }
    new public Matrix2x2[,] Minors() => base.Minors();
    new public SimpleProjectionMatrix Transpose() => new(base.Transpose());

    new public SimpleProjectionMatrix AddRow(int rowToChange, int referenceRow, float factor = 1) =>
        new(base.AddRow(rowToChange, referenceRow, factor));
    new public SimpleProjectionMatrix ScaleRow(int rowIndex, float factor) =>
        new(base.ScaleRow(rowIndex, factor));
    new public SimpleProjectionMatrix SwapRows(int rowA, int rowB) =>
        new(base.SwapRows(rowA, rowB));

    public override bool Equals(object? obj) => base.Equals(obj);
    public override int GetHashCode() => base.GetHashCode();
    public bool Equals(SimpleProjectionMatrix? other) => base.Equals(other);

    new public object Clone() => new SimpleProjectionMatrix(r1c1, r1c2, r1c3,
                                                            r2c1, r2c2, r2c3,
                                                            r3c1, r3c2, r3c3);

    public static SimpleProjectionMatrix operator +(SimpleProjectionMatrix a,
        SimpleProjectionMatrix b) => new((Matrix3x3)a + b);
    public static SimpleProjectionMatrix? operator -(SimpleProjectionMatrix m) => m.Inverse();
    public static SimpleProjectionMatrix operator -(SimpleProjectionMatrix a,
        SimpleProjectionMatrix b) => new((Matrix3x3)a - b);
    public static SimpleProjectionMatrix operator *(SimpleProjectionMatrix a, float b) =>
        new((Matrix3x3)a * b);
    public static SimpleProjectionMatrix operator *(SimpleProjectionMatrix a,
        SimpleProjectionMatrix b) => new((Matrix3x3)a * b);
    public static Float3 operator *(SimpleProjectionMatrix a, Float3 b) => (Matrix3x3)a * b;
    public static SimpleProjectionMatrix operator /(SimpleProjectionMatrix a, float b) =>
        new((Matrix3x3)a / b);
    public static SimpleProjectionMatrix operator /(SimpleProjectionMatrix a,
        SimpleProjectionMatrix b) => new((Matrix3x3)a / b);
    public static Float3 operator /(SimpleProjectionMatrix a, Float3 b) => (Matrix3x3)a / b;
    public static SimpleProjectionMatrix operator ^(SimpleProjectionMatrix a,
        SimpleProjectionMatrix b) => new((Matrix3x3)a ^ b);
    public static bool operator ==(SimpleProjectionMatrix a, SimpleProjectionMatrix b) =>
        a.Equals(b);
    public static bool operator !=(SimpleProjectionMatrix a, SimpleProjectionMatrix b) =>
        !a.Equals(b);
}
