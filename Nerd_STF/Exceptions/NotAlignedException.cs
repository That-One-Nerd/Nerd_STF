namespace Nerd_STF.Extensions;

public class NotAlignedException : Nerd_STFException
{
    public NotAlignedException() : base("Points are not aligned.") { }
    public NotAlignedException(string message) : base(message) { }
    public NotAlignedException(string message, Exception inner) : base(message, inner) { }
    protected NotAlignedException(SerializationInfo info, StreamingContext context)
        : base(info, context) { }
}
