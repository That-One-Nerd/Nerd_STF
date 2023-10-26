namespace Nerd_STF.Mathematics.Abstract;

public interface IPolygon<T> : IAverage<T>, IContainsGeometry<T>, IEquatable<T>,
    IFloatArray<T>, IGroup<Float3>, IIndexAll<Float3>, IIndexRangeAll<Float3>,
    ILerp<T, float>, IMedian<T>, ITriangulate
    where T : IPolygon<T>
{
    public float Area { get; }
    public Float3 Midpoint { get; }
    public float Perimeter { get; }

    public Float3[] GetAllVerts();
    public Line[] GetOutlines();

    public static abstract T operator +(T poly, Float3 offset);
    public static abstract T operator -(T poly, Float3 offset);
    public static abstract T operator *(T poly, float scale);
    public static abstract T operator *(T poly, Float3 scale3);
    public static abstract T operator /(T poly, float scale);
    public static abstract T operator /(T poly, Float3 scale3);

    public static abstract bool operator ==(T a, T b);
    public static abstract bool operator !=(T a, T b);

    public static abstract implicit operator T(Fill<Float3> fill);
    public static abstract implicit operator T(Fill<Int3> fill);
    public static abstract implicit operator T(Fill<Line> fill);
    public static abstract implicit operator T(Fill<float> fill);
    public static abstract implicit operator T(Fill<int> fill);
}
