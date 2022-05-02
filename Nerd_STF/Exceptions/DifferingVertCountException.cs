using System.Runtime.Serialization;

namespace Nerd_STF.Exceptions;

[Serializable]
public class DifferingVertCountException : Nerd_STFException
{
    public string? ParamName;
    public Polygon[]? Polygons;

    public DifferingVertCountException() : base("Not all polygons have the same vert count.") { }
    public DifferingVertCountException(Exception inner) : base("Not all polygons have the same vert count.", inner) { }
    public DifferingVertCountException(string paramName) : this() => ParamName = paramName;
    public DifferingVertCountException(string paramName, Exception inner) : this(inner) => ParamName = paramName;
    public DifferingVertCountException(params Polygon[] polys) : this() => Polygons = polys;
    public DifferingVertCountException(Polygon[] polys, Exception inner) : this(inner) => Polygons = polys;
    public DifferingVertCountException(string paramName, Polygon[] polys) : this()
    {
        ParamName = paramName;
        Polygons = polys;
    }
    public DifferingVertCountException(string paramName, Polygon[] polys, Exception inner) : this(inner)
    {
        ParamName = paramName;
        Polygons = polys;
    }

    protected DifferingVertCountException(SerializationInfo info, StreamingContext context) : base(info, context) { }
}
