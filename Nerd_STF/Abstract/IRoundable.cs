#if CS11_OR_GREATER
namespace Nerd_STF.Abstract
{
    public interface IRoundable<TSelf> : IRoundable<TSelf, TSelf>
        where TSelf : IRoundable<TSelf> { }

    public interface IRoundable<TSelf, TOut>
        where TSelf : IRoundable<TSelf, TOut>
    {
        static abstract TOut Ceiling(TSelf val);
        static abstract TOut Floor(TSelf val);
        static abstract TOut Round(TSelf val);

        static abstract void Ceiling(ref TSelf val);
        static abstract void Floor(ref TSelf val);
        static abstract void Round(ref TSelf val);
    }
}
#endif
