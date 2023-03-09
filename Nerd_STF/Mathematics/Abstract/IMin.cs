namespace Nerd_STF.Mathematics.Abstract;

public interface IMin<T> where T : IMin<T>, IComparable<T>
{
    public static virtual T Min(params T[] vals) => Mathf.Min(vals);
}
