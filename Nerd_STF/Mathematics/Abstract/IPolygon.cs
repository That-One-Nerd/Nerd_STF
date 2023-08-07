namespace Nerd_STF.Mathematics.Abstract;

public interface IPolygon : ITriangulate
{
    public Float3[] GetAllVerts();
    public float[] ToFloatArray();
}
