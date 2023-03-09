namespace Nerd_STF.Mathematics.Abstract;

public interface ICeiling<TSelf> : ICeiling<TSelf, TSelf> where TSelf : ICeiling<TSelf> { }
public interface ICeiling<TSelf, TRound> where TSelf : ICeiling<TSelf, TRound>
{
    public static abstract TRound Ceiling(TSelf val);
}
