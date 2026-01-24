#if CS11_OR_GREATER
namespace Nerd_STF.Mathematics.Algebra
{
    public interface IMagnitudeOperators<TSelf>
        where TSelf : IMagnitudeOperators<TSelf>
    {
        double Magnitude { get; }
        static abstract TSelf ClampMagnitude(TSelf val, double minMag, double maxMag);
    }
}
#endif
