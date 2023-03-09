namespace Nerd_STF.Mathematics.Abstract;

public interface IFloor<TSelf> : IFloor<TSelf, TSelf>
    where TSelf : IFloor<TSelf> { } 
public interface IFloor<TSelf, TRound>
    where TSelf : IFloor<TSelf, TRound>
{
    public static abstract TRound Floor(TSelf val);
}
