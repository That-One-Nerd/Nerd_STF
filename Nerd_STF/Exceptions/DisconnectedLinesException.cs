namespace Nerd_STF.Exceptions;

[Serializable]
public class DisconnectedLinesException : Nerd_STFException
{
    public string? ParamName;
    public Line[]? Lines;

    public DisconnectedLinesException() : base("Lines are not connected.") { }
    public DisconnectedLinesException(Exception inner) : base("Lines are not connected.", inner) { }
    public DisconnectedLinesException(string paramName) : this() => ParamName = paramName;
    public DisconnectedLinesException(string paramName, Exception inner) : this(inner) => ParamName = paramName;
    public DisconnectedLinesException(params Line[] lines) : this() => Lines = lines;
    public DisconnectedLinesException(Line[] lines, Exception inner) : this(inner) => Lines = lines;
    public DisconnectedLinesException(string paramName, Line[] lines) : this()
        {
            ParamName = paramName;
            Lines = lines;
    }
    public DisconnectedLinesException(string paramName, Line[] lines, Exception inner) : this(inner)
        {
            ParamName = paramName;
            Lines = lines;
        }

    protected DisconnectedLinesException(SerializationInfo info, StreamingContext context) : base(info, context) { }
}
