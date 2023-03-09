namespace Nerd_STF.Mathematics.Abstract;

public interface ICross<TSelf> : ICross<TSelf, TSelf>
    where TSelf : ICross<TSelf> { }
public interface ICross<TSelf, TOut> where TSelf : ICross<TSelf, TOut>
{
    public static abstract TOut Cross(TSelf a, TSelf b, bool normalized = false);
}
