namespace Nerd_STF.Mathematics.Abstract;

public interface IAverage<T> where T : IAverage<T>
{
    public static abstract T Average(params T[] vals);
}
