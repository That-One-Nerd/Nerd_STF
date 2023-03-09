namespace Nerd_STF.Exceptions;

[Serializable]
public class InvalidSizeException : Nerd_STFException
{
    public InvalidSizeException() : base("Argument size is invalid.") { }
    public InvalidSizeException(string message) : base(message) { }
    public InvalidSizeException(string message, Exception inner) : base(message, inner) { }
    protected InvalidSizeException(SerializationInfo info, StreamingContext context) : base(info, context) { }
}
