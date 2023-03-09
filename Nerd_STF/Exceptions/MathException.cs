namespace Nerd_STF.Exceptions;

[Serializable]
public class MathException : Nerd_STFException
{
    public MathException() : base("A calculation error occured.") { }
    public MathException(string message) : base(message) { }
    public MathException(string message, Exception inner) : base(message, inner) { }
    protected MathException(SerializationInfo info, StreamingContext context) : base(info, context) { }
}
