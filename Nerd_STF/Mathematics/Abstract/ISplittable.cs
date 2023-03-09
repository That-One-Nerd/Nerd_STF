using System.Runtime.CompilerServices;

namespace Nerd_STF.Mathematics.Abstract;

public interface ISplittable<TSelf, TTuple> where TTuple : ITuple
{
    public static abstract TTuple SplitArray(params TSelf[] vals);
}
