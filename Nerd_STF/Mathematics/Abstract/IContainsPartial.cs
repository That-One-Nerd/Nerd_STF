namespace Nerd_STF.Mathematics.Abstract;

public interface IContainsPartial<T> : IContains<T>
    where T : IEquatable<T>
{
    public bool ContainsPartially(Line other);
}
