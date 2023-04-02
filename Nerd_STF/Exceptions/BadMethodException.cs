namespace Nerd_STF.Exceptions;

[Serializable]
public class BadMethodException : Nerd_STFException
{
    public MethodInfo? MethodInfo;

    public BadMethodException() : base("The method or delegate provided is invalid for this operation.") { }
    public BadMethodException(string message) : base(message) { }
    public BadMethodException(Exception inner) : base("The method or delegate provided is invalid for this operation.", inner) { }
    public BadMethodException(MethodInfo method) : this() => MethodInfo = method;
    public BadMethodException(MethodInfo method, Exception inner) : this(inner) => MethodInfo = method;
    public BadMethodException(string message, Exception inner) : base(message, inner) { }
    public BadMethodException(string message, MethodInfo method) : this(message) => MethodInfo = method;
    public BadMethodException(string message, MethodInfo method, Exception inner) : this(message, inner) => MethodInfo = method;

    protected BadMethodException(SerializationInfo info, StreamingContext context) : base(info, context) { }
}
