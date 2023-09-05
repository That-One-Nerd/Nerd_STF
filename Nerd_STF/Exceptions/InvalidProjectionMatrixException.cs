namespace Nerd_STF.Exceptions;

[Serializable]
public class InvalidProjectionMatrixException : Nerd_STFException
{
    public Matrix? Matrix;

    public InvalidProjectionMatrixException() : base("This is not a projection matrix.") { }
    public InvalidProjectionMatrixException(string message) : base(message) { }
    public InvalidProjectionMatrixException(string message, Exception inner) : base(message, inner) { }
    public InvalidProjectionMatrixException(Matrix? matrix) : this() => Matrix = matrix;
    public InvalidProjectionMatrixException(Matrix? matrix, string message) : this(message) => Matrix = matrix;
    public InvalidProjectionMatrixException(Matrix? matrix, string message, Exception inner) : this(message, inner) =>
        Matrix = matrix;
    protected InvalidProjectionMatrixException(SerializationInfo info, StreamingContext context) : base(info, context) { }
}
