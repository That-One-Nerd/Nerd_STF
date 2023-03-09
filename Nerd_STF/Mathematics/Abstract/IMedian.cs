namespace Nerd_STF.Mathematics.Abstract;

public interface IMedian<T> where T : IMedian<T>
{
    public static virtual T Median(params T[] vals) => Mathf.Median(vals);
}
