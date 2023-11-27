﻿namespace Nerd_STF.Mathematics.Samples;

/// <summary>
/// A container of various mathematical constants.
/// </summary>
public static class Constants
{
    /// <summary>
    /// The ratio between a degree and a radian. This constant is intended to be used
    /// as follows:
    /// <code>degrees = radians * <see cref="DegToRad"/></code>
    /// This is the reciprocal of <see cref="RadToDeg"/>.
    /// </summary>
    public const float DegToRad = Pi / 180;
    /// <summary>
    /// Exactly one half of the constant <see cref="Pi"/>. While this constant has many
    /// uses, most of them are just a shorthand for <c><see cref="Pi"/> / 2</c>, unlike
    /// <see cref="Tau"/>.
    /// </summary>
    /// <remarks>See also: <see href="https://en.wikipedia.org/wiki/Pi">Pi - Wikipedia</see></remarks>
    public const float HalfPi = Pi / 2;
    /// <summary>
    /// The ratio between a circle's circumference and its diameter. This constant has
    /// many uses.
    /// </summary>
    /// <remarks>See also: <see href="https://en.wikipedia.org/wiki/Pi">Pi - Wikipedia</see></remarks>
    public const float Pi = 3.14159265359f;
    /// <summary>
    /// The ratio between a radian and a degree. This constant is intended to be used
    /// as follows:
    /// <code>radians = degrees * <see cref="RadToDeg"/></code>
    /// This is the reciprocal of <see cref="DegToRad"/>.
    /// </summary>
    public const float RadToDeg = 180 / Pi;
    /// <summary>
    /// Exactly double the value of the constant <see cref="Pi"/>. Unlike
    /// <see cref="HalfPi"/>, there are circumstances where this constant is preferrable
    /// over its actual value of <c><see cref="Pi"/> * 2</c>.
    /// </summary>
    /// <remarks>See also: <see href="https://en.wikipedia.org/wiki/Pi">Pi - Wikipedia</see></remarks>
    public const float Tau = Pi * 2;

    /// <summary>
    /// <c>E</c> is also known as Euler's number or the natural number. This constant
    /// has a very large number of interpretations and uses.
    /// </summary>
    /// <remarks>See also: <see href="https://en.wikipedia.org/wiki/E_(mathematical_constant)">e (mathematical constant) - Wikipedia</see></remarks>
    public const float E = 2.71828182846f;
    /// <summary>
    /// The limit of the summed error between the harmonic series and the natural logarithm.
    /// </summary>
    /// <remarks>See also: <see href="https://en.wikipedia.org/wiki/Euler%27s_constant">Euler's constant - Wikipedia</see></remarks>
    public const float EulerConstant = 0.5772156649f;
    /// <summary>
    /// The natural logarithm of 2. It is a trancendental number.
    /// </summary>
    /// <remarks>
    /// See also: <see href="https://en.wikipedia.org/wiki/Natural_logarithm_of_2">Natural logarithm of 2 - Wikipedia</see><br/>
    /// See also: <see href="https://en.wikipedia.org/wiki/Natural_logarithm">Natural logarithm - Wikipedia</see>
    /// </remarks>
    public const float Ln2 = 0.69314718056f;
    /// <summary>
    /// The natural logarithm of 3. It is a trancendental number.
    /// </summary>
    /// <remarks>See also: <see href="https://en.wikipedia.org/wiki/Natural_logarithm">Natural logarithm - Wikipedia</see></remarks>
    public const float Ln3 = 1.09861228867f;
    /// <summary>
    /// The natural logarithm of 5. It is a trancendental number.
    /// </summary>
    /// <remarks>See also: <see href="https://en.wikipedia.org/wiki/Natural_logarithm">Natural logarithm - Wikipedia</see></remarks>
    public const float Ln5 = 1.60943791243f;
    /// <summary>
    /// The natural logarithm of 10. It is a trancendental number.
    /// </summary>
    /// <remarks>See also: <see href="https://en.wikipedia.org/wiki/Natural_logarithm">Natural logarithm - Wikipedia</see></remarks>
    public const float Ln10 = 2.30258509299f;

    /// <summary>
    /// The logarithm (base 10) of 2.
    /// </summary>
    public const float Log2 = 0.301029995664f;
    /// <summary>
    /// The logarithm (base 10) of 3.
    /// </summary>
    public const float Log3 = 0.47712125472f;
    /// <summary>
    /// The logarithm (base 10) of 5.
    /// </summary>
    public const float Log5 = 0.698970004336f;
    /// <summary>
    /// The logarithm (base 10) of 10.
    /// </summary>
    public const float Log10 = 1;

    /// <summary>
    /// The resulting smallest angle when you apply the golden ratio to a circle (in degrees).
    /// </summary>
    /// <remarks>See also: <see href="https://en.wikipedia.org/wiki/Golden_angle">Golden angle - Wikipedia</see></remarks>
    public const float GoldenAngle = 180 * (3 - Sqrt5);
    public const float GoldenRatio = (1 + Sqrt5) / 2;
    public const float SilverRatio = Sqrt2 + 1;
    public const float SupergoldenRatio = 1.465571232f;

    public const float Cbrt2 = 1.25992104989f;
    public const float Cbrt3 = 1.44224957031f;
    public const float Cbrt5 = 1.70997594668f;
    public const float Cbrt10 = 2.15443469003f;
    public const float HalfSqrt2 = 0.707106781187f;
    public const float Sqrt2 = 1.4142135624f;
    public const float Sqrt3 = 1.7320508076f;
    public const float Sqrt5 = 2.2360679775f;
    public const float Sqrt10 = 3.16227766017f;
    public const float TwelfthRoot2 = 1.05946309436f;

    public const float Cos0Deg = 1;
    public const float Cos30Deg = Sqrt3 / 2;
    public const float Cos45Deg = Sqrt2 / 2;
    public const float Cos60Deg = 0.5f;
    public const float Cos90Deg = 0;
    public const float Cot0Deg = float.PositiveInfinity;
    public const float Cot30Deg = Sqrt3;
    public const float Cot45Deg = 1;
    public const float Cot60Deg = Sqrt3 / 3;
    public const float Cot90Deg = 0;
    public const float Csc0Deg = float.PositiveInfinity;
    public const float Csc30Deg = 2;
    public const float Csc45Deg = Sqrt2;
    public const float Csc60Deg = 2 * Sqrt3 / 3;
    public const float Csc90Deg = 1;
    public const float MagicAngle = 54.7356103172f;
    public const float Sec0Deg = 1;
    public const float Sec30Deg = 2 * Sqrt3 / 3;
    public const float Sec45Deg = Sqrt2;
    public const float Sec60Deg = 2;
    public const float Sec90Deg = float.PositiveInfinity;
    public const float Sin0Deg = 0;
    public const float Sin30Deg = 0.5f;
    public const float Sin45Deg = Sqrt2 / 2;
    public const float Sin60Deg = Sqrt3 / 2;
    public const float Sin90Deg = 1;
    public const float Tan0Deg = 0;
    public const float Tan30Deg = Sqrt3 / 3;
    public const float Tan45Deg = 1;
    public const float Tan60Deg = Sqrt3;
    public const float Tan90Deg = float.PositiveInfinity;

    public static readonly Angle IsometricAngle = (35.2643896828f, Angle.Type.Degrees);
    public const float IsometricCos = 0.816496580928f;
    public const float IsometricSin = 0.57737026919f;

    public const float AperyConstant = 1.2020569031f;
    public const float ArtinConstant = 0.3739558136f;
    public const float AsymptoticLebesgueConstant = 0.9894312738f;
    public const float BackhouseConstant = 1.4560749485f;
    public const float Base10ChampernowneConstant = 0.1234567891f;
    public const float BernsteinConstant = 0.2801694990f;
    public const float BlochConstant = (0.4332f + 0.4719f) / 2;
    public const float BruijnNewmanConstant = (0 + 0.2f) / 2;
    public const float BrunConstant = 1.9021605831f;
    public const float CatalanConstant = 0.9159655941f;
    public const float CahenConstant = 0.6434105462f;
    public const float ChaitinConstant = 0.0078749969f;
    public const float ChvatalSankoffConstant = (0.788071f + 0.826280f) / 2;
    public const float ConwayConstant = 1.3035772690f;
    public const float CopelandErdosConstant = 0.2357111317f;
    public const float ConnectiveConstant = 1.847759065f;
    public const float DeVicciTesseractConstant = 1.0074347568f;
    public const float EmbreeTrefethenConstant = 0.70258f;
    public const float EulerMascheroniConstant = EulerConstant;
    public const float ErdosBorweinConstant = 1.6066951524f;
    public const float ErdosTenenbaumFordConstant = 0.8607133205f;
    public const float FeigenbaumConstant1 = 4.6692016091f;
    public const float FeigenbaumConstant2 = 2.5029078750f;
    public const float FellerTornierConstant = 0.6613170494f;
    public const float FoiasConstant = 1.1874523511f;
    public const float FransenRobinsonConstant = 2.8077702420f;
    public const float GaussConstant = 0.8346268416f;
    public const float GelfondConstant = 23.1406926327f;
    public const float GelfondSchneiderConstant = 2.6651441426f;
    public const float GiesekingConstant = 1.0149416064f;
    public const float GlaisherKinkelinConstant = 1.2824271291f;
    public const float GolombDickmanConstant = 0.6243299885f;
    public const float GompertzConstant = 0.5963473623f;
    public const float HafnerSarnakMcCurleyConstant = 0.3532363718f;
    public const float HeathBrownMorozConstant = 0.0013176411f;
    public const float KeplerBouwkampConstant = 0.1149420449f;
    public const float KhinchinConstant = 2.6854520010f;
    public const float KomornikLoreti = 1.7872316501f;
    public const float LandauConstant = (0.5f + 0.54326f) / 2;
    public const float LandauThirdConstant = (0.5f + 0.7853f) / 2;
    public const float LandauRamanujanConstant = 0.7642236535f;
    public const float LiebSquareIceConstant = 8 * Sqrt3 / 9;
    public const float LemniscateConstant = 2.6220575542f;
    public const float LevyConstant1 = Pi * Pi / (12 * Ln2);
    public const float LevyConstant2 = 3.2758229187f;
    public const float LiouvilleConstant = 0.110001f;
    public const float LochsConstant = 6 * Ln2 * Ln10 / (Pi * Pi);
    public const float MeisselMertensConstant = 0.2614972128f;
    public const float MillsConstant = 1.3063778838f;
    public const float MRBConstant = 0.1878596424f;
    public const float NivenConstant = 1.7052111401f;
    public const float OmegaConstant = 0.5671432904f;
    public const float PorterConstant = 1.4670780794f;
    public const float PrimeConstant = 0.4146825098f;
    public const float ProuhetThueMorseConstant = 0.4124540336f;
    public const float RamanujanConstant = 262_537_412_640_768_743.99999f;
    public const float RamanujanSoldnerConstant = 1.4513692348f;
    public const float ReciprocalFibonacciConstant = 3.3598856662f;
    public const float RobbinsConstant = 0.6617071822f;
    public const float SalemConstant = 1.1762808182f;
    public const float SierpinskiConstant = 2.5849817595f;
    public const float SomosQuadraticRecurranceConstant = 1.6616879496f;
    public const float StephensConstant = 0.5759599688f;
    public const float TaniguchiConstant = 0.6782344919f;
    public const float TribonacciConstant = 1.8392867552f;
    public const float TwinPrimesConstant = 0.6601618158f;
    public const float VanDerPauwConstant = Pi / Ln2;
    public const float ViswanathConstant = 1.1319882487f;
    public const float WeierstrassConstant = 0.4749493799f;

    public const float FirstContinuedFractionConstant = 0.6977746579f;
    public const float FirstNielsenRamanujanConstant = Pi * Pi / 12;
    public const float SecondDuBoisRaymondConstant = (E * E - 7) / 2;
    public const float SecondFavardConstant = 1.2337005501f;
    public const float SecondHermiteConstant = 2 * Sqrt3 / 9;
    public const float UniversalParabolicConstant = 2.2955871493f;

    public const float DottieNumber = 0.7390851332f;
    public const float FractalDimensionOfTheApollonianPackingOfCircles = 1.305688f;
    public const float FractalDimensionOfTheCanterSet = Log2 / Log3;
    public const float HausdorffDimensionOfTheSerpinskiTriangle = Log3 / Log2;
    public const float MagicNumber = 3; // 3 is a magic number.
    public const float PlasticNumber = 1.3247179572f;
    public const float LaplaceLimit = 0.6627434193f;
    public const float LogarithmicCapacityOfTheUnitDisk = 0.5901702995f;
    public const float RegularPaperfoldingConstant = 0.8507361882f;
}
