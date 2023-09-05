namespace Nerd_STF.Mathematics.Algebra;

public class ProjectionMatrix : Matrix3x3
{
    // TODO: i need to remove the record check from everything, add new equals comparitors
    //       and re-implement IsValidProjectionMatrix

    public ProjectionMatrix(float all) : this(all, all, all, all, all, all, all, all, all) { }
    public ProjectionMatrix(float r1c1, float r1c2, float r1c3, float r2c1,
        float r2c2, float r2c3, float r3c1, float r3c2, float r3c3) :
        base(r1c1, r1c2, r1c3, r2c1, r2c2, r2c3, r3c1, r3c2, r3c3)
    {
        //if (!IsValidProjectionMatrix) throw new InvalidProjectionMatrixException(this);
    }
    public ProjectionMatrix(float[] nums) : this(nums[0], nums[1], nums[2],
        nums[3], nums[4], nums[5], nums[6], nums[7], nums[8]) { }
    public ProjectionMatrix(int[] nums) : this(nums[0], nums[1], nums[2],
        nums[3], nums[4], nums[5], nums[6], nums[7], nums[8]) { }
    public ProjectionMatrix(Fill<float> fill) : this(fill(0), fill(1), fill(2),
        fill(3), fill(4), fill(5), fill(6), fill(7), fill(8)) { }
    public ProjectionMatrix(Fill<int> fill) : this(fill(0), fill(1), fill(2),
        fill(3), fill(4), fill(5), fill(6), fill(7), fill(8)) { }
    public ProjectionMatrix(float[,] nums) : this(nums[0, 0], nums[0, 1], nums[0, 2],
        nums[1, 0], nums[1, 1], nums[1, 2], nums[2, 0], nums[2, 1], nums[2, 2]) { }
    public ProjectionMatrix(int[,] nums) : this(nums[0, 0], nums[0, 1], nums[0, 2],
        nums[1, 0], nums[1, 1], nums[1, 2], nums[2, 0], nums[2, 1], nums[2, 2]) { }
    public ProjectionMatrix(Fill2d<float> fill) : this(fill(0, 0), fill(0, 1), fill(0, 2),
        fill(1, 0), fill(1, 1), fill(1, 2), fill(2, 0), fill(2, 1), fill(2, 2)) { }
    public ProjectionMatrix(Fill2d<int> fill) : this(fill(0, 0), fill(0, 1), fill(0, 2),
        fill(1, 0), fill(1, 1), fill(1, 2), fill(2, 0), fill(2, 1), fill(2, 2)) { }
    public ProjectionMatrix(Float3 r1, Float3 r2, Float3 r3) : this(r1.x, r1.y, r1.z, r2.x, r2.y, r2.z, r3.x, r3.y, r3.z) { }
    public ProjectionMatrix(Fill<Float3> fill) : this(fill(0), fill(1), fill(2)) { }
    public ProjectionMatrix(Fill<Int3> fill) : this((IEnumerable<int>)fill(0), fill(1), fill(2)) { }
    public ProjectionMatrix(IEnumerable<float> r1, IEnumerable<float> r2, IEnumerable<float> r3)
        : this(r1.ToFill(), r2.ToFill(), r3.ToFill()) { }
    public ProjectionMatrix(IEnumerable<int> r1, IEnumerable<int> r2, IEnumerable<int> r3)
        : this(r1.ToFill(), r2.ToFill(), r3.ToFill()) { }
    public ProjectionMatrix(Fill<float> r1, Fill<float> r2, Fill<float> r3)
        : this(r1(0), r1(1), r1(2), r2(0), r2(1), r2(2), r3(0), r3(1), r3(2)) { }
    public ProjectionMatrix(Fill<int> r1, Fill<int> r2, Fill<int> r3)
        : this(r1(0), r1(1), r1(2), r2(0), r2(1), r2(2), r3(0), r3(1), r3(2)) { }

    public static ProjectionMatrix SingleViewProjection(CrossSection2d section) => new(new[,]
    {
        { section == CrossSection2d.XY || section == CrossSection2d.ZX ? 1 : 0, 0, 0 },
        { 0, section == CrossSection2d.XY || section == CrossSection2d.YZ ? 1 : 0, 0 },
        { 0, 0, section == CrossSection2d.YZ || section == CrossSection2d.ZX ? 1 : 0 }
    });
    public static ProjectionMatrix IsometricProjection(Angle alpha, Angle beta)
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
        Matrix3x3 result = alphaMat * betaMat * flatten;
        return new(result.ToFill2D());
    }

    public Fill<Float3> Project(Fill<Float3> toProject) => i => this * toProject(i);
}
