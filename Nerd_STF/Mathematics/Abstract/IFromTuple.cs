using System.Runtime.CompilerServices;

namespace Nerd_STF.Mathematics.Abstract;

public interface IFromTuple<TSelf, TTuple> where TSelf : IFromTuple<TSelf, TTuple>
    where TTuple : ITuple
{
    public static abstract implicit operator TSelf(TTuple val);
}
