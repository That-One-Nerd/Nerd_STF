namespace Nerd_STF.Mathematics.Geometry.Abstract;

public interface IContainsGeometry3d<T> : IContains<Float3>, IContains<Box2d>, IContains<Line>,
    IContains<Triangle>, IIntersect<Box2d>, IIntersect<Line>, IIntersect<Triangle>
    where T : IContainsGeometry3d<T>, IEquatable<T>
{
    public bool Contains(T obj);
    public bool Intersects(T obj);

    public bool Contains(IEnumerable<Float3> points);
    public bool Contains(Fill<Float3> points, int count);
    public bool Intersects(IEnumerable<Line> lines);
    public bool Intersects(Fill<Line> lines, int count);

    public bool Contains<TOther>(TOther obj) where TOther : IPolygon<TOther>;
    public bool Intersects<TOther>(TOther obj) where TOther : IPolygon<TOther>;
}
