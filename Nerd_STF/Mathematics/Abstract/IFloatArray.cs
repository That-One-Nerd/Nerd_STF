namespace Nerd_STF.Mathematics.Abstract;

public interface IFloatArray<T> where T : IFloatArray<T>
{
    public static abstract float[] ToFloatArrayAll(params T[] vals);

    public float[] ToFloatArray();
}
