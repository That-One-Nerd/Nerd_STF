#if CS11_OR_GREATER
namespace Nerd_STF.Graphics
{
    public interface IColorOperators<TSelf> where TSelf : IColorOperators<TSelf>
    {
        static abstract TSelf operator +(TSelf a, TSelf b);
        static abstract TSelf operator *(TSelf a, TSelf b);
        static abstract TSelf operator *(TSelf a, double b);
        static abstract bool operator ==(TSelf a, IColor b);
        static abstract bool operator !=(TSelf a, IColor b);
        static abstract bool operator ==(TSelf a, TSelf b);
        static abstract bool operator !=(TSelf a, TSelf b);
    }
}
#endif
