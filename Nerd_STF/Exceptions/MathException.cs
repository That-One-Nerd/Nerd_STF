using System.Runtime.Serialization;

namespace Nerd_STF.Exceptions;

[Serializable]
public class MathException : Nerd_STFException
{
    public MathException() : base() { }
    public MathException(string message) : base(message) { }
    public MathException(string message, Exception inner) : base(message, inner) { }
    protected MathException(SerializationInfo info, StreamingContext context) : base(info, context) { }
}
