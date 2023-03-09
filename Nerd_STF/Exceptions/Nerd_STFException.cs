namespace Nerd_STF.Exceptions;

[Serializable]
public class Nerd_STFException : Exception
{
    public Nerd_STFException() : base("An unknown error occured within Nerd_STF.") { }
    public Nerd_STFException(Exception inner)
        : base("An unknown error occured within Nerd_STF.", inner) { }
    public Nerd_STFException(string message) : base(message) { }
    public Nerd_STFException(string message, Exception inner) : base(message, inner) { }
    protected Nerd_STFException(SerializationInfo info, StreamingContext context) : base(info, context) { }
}
