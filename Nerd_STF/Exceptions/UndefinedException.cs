using System.Runtime.Serialization;

namespace Nerd_STF.Exceptions;

[Serializable]
public class UndefinedException : MathException
{
    public UndefinedException() : this("The equation calculated resulted in an undefined number.") { }
    public UndefinedException(string message) : base(message) { }
    public UndefinedException(string message, Exception inner) : base(message, inner) { }
    protected UndefinedException(SerializationInfo info, StreamingContext context) : base(info, context) { }
}
