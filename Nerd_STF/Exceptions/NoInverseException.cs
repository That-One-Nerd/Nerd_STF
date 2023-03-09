namespace Nerd_STF.Exceptions;

[Serializable]
public class NoInverseException : Nerd_STFException
{
    public Matrix? Matrix;

    public NoInverseException() : base("This matrix does not have an inverse.") { }
    public NoInverseException(string message) : base(message) { }
    public NoInverseException(string message, Exception inner) : base(message, inner) { }
    public NoInverseException(Matrix? matrix) : this() => Matrix = matrix;
    public NoInverseException(Matrix? matrix, string message) : this(message) => Matrix = matrix;
    public NoInverseException(Matrix? matrix, string message, Exception inner) : this(message, inner) =>
        Matrix = matrix;
    protected NoInverseException(SerializationInfo info, StreamingContext context) : base(info, context) { }
}
