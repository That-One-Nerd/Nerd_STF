namespace Nerd_STF.Mathematics.Abstract;

public interface IRound<TSelf> : IRound<TSelf, TSelf>
    where TSelf : IRound<TSelf> { } 
public interface IRound<TSelf, TRound>
    where TSelf : IRound<TSelf, TRound>
{
    public static abstract TRound Round(TSelf val);
}
