namespace Nerd_STF.Mathematics.Algebra
{
    public interface IMagnitudeOperators<TSelf>
        where TSelf : IMagnitudeOperators<TSelf>
    {
        double Magnitude { get; }
        double MagnitudeSqr { get; }

#if CS11_OR_GREATER
        static abstract TSelf ClampMagnitude(TSelf val, double minMag, double maxMag);
#endif
    }
}

