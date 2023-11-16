namespace Nerd_STF.Extensions;

public class AspectLockedException : Nerd_STFException
{
    public readonly object? ReferencedObject;

    public AspectLockedException() : base("This object has a locked aspect ratio.") { }
    public AspectLockedException(string message) : base(message) { }
    public AspectLockedException(string message, Exception inner) : base(message, inner) { }
    public AspectLockedException(string message, object? obj) : base(message)
    {
        ReferencedObject = obj;
    }
    public AspectLockedException(string message, Exception inner, object? obj)
        : base(message, inner)
    {
        ReferencedObject = obj;
    }
    protected AspectLockedException(SerializationInfo info, StreamingContext context)
        : base(info, context) { }
}
