namespace Nerd_STF.Mathematics.Geometry.Abstract;

public interface IContainsGeometry2d<T> : IContains<Float2>, IContains<Box2d>, IContains<Line>,
    IContains<Triangle>, IIntersect<Box2d>, IIntersect<Line>, IIntersect<Triangle>
    where T : IContainsGeometry2d<T>, IEquatable<T>
{
    public bool Contains(T obj);
    public bool Intersects(T obj);

    public bool Contains(IEnumerable<Float2> points);
    public bool Contains(Fill<Float2> points, int count);
    public bool Intersects(IEnumerable<Line> lines);
    public bool Intersects(Fill<Line> lines, int count);

    public bool Contains<TOther>(TOther obj) where TOther : IPolygon<TOther>;
    public bool Intersects<TOther>(TOther obj) where TOther : IPolygon<TOther>;
}
