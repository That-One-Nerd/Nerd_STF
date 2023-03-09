namespace Nerd_STF.Exceptions;

[Serializable]
public class UndefinedException : MathException
{
    public UndefinedException() : this("A calculation has produced an undefined number.") { }
    public UndefinedException(string message) : base(message) { }
    public UndefinedException(string message, Exception inner) : base(message, inner) { }
    protected UndefinedException(SerializationInfo info, StreamingContext context) : base(info, context) { }
}
