namespace Nerd_STF.Mathematics.Abstract;

public interface IMax<T> where T : IMax<T>, IComparable<T>
{
    public static virtual T Max(params T[] vals) => Mathf.Max(vals);
}
