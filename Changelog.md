# Nerd_STF v2.4.0 (Equations and Numbers)

I've done a pretty good amount of stuff in this update, and I'm pretty proud of it. Good improvement.

First of all, I've gone and added all of the applicable `Mathf` functions as an extension to the `Equation` delegate. That way if you want to say, calculate the square root of an entire equation, rather than going:
```csharp
Equation result = x => Mathf.Sqrt(equ(x));
// Where `equ` is an `Equation` that represents the function we want to take the square root of.
```
you can shorten it down and remove some of the weirdness.
```csharp
Equation result = equ.Sqrt();
// Where `equ` is an `Equation` that represents the function we want to take the square root of.
```

It works for any common function you'd want to apply to an equation.

---

Speaking of math functions (I guess that's the whole update, given the name), I'm now utilizing an implementation of CORDIC I made to calculate all trigonometric functions, hyperbolic trig functions, exponents, and logs. It's quite a neat process, and I could also have it completely wrong, but whatever I have, CORDIC or something else, works wonders, and is considerably faster than some other methods I tried. Of course, it's still nowhere near as fast as the built-in math functions, but they will always be on a whole other level. Maybe in the optimization update I'll bother to improve them, but they work quite well as-is for most use cases.

I've also implemented taylor series into the `Calculus` class. It's not particularly useful is most cases, so I wouldn't bother. The only time it would be a good idea to use it is if you've got some incredibly slow to calculate equation and want to optimize it. The function will take a long time at first, as it will have to generate second-derivatives and beyond, but afterwards the output equation will just be a simple-to-calculate polynomial (though the approximation gets worse the further you are from the reference point). If you've got an equation that's already fast to calculate, using the taylor series approximation will only be a negative. So take it with a grain of salt.

That's mostly it. I've fixed some issues/bugs here and there, renamed some small stuff (check the full changelog for more information), removed all the stuff marked obsolete and to be removed in this update, added some more math stuff like prime calculators, and other tiny changes. The next update will be focused on reworking the badly made geometry stuff I did a while back ([Version 2.1](https://github.com/That-One-Nerd/Nerd_STF/releases/tag/v2.1.0)). I know I say this all the time, but version 2.5 should be substantially bigger than this one. I'm going to be reworking the `Polygon` object entirely and will be improving quite a lot of other things in the whole `Nerd_STF.Mathematics.Geometry` namespace. Stay tuned!

Here's the full changelog:
```
* Nerd_STF
    * Exceptions
        + BadMethodException
    * Extensions
        * ConversionExtension
            - ToDictionary<TKey, TValue>(IEnumerable<KeyValuePair<TKey, TValue>>)
        * EquationExtension
            + ValidNumberTypes
            + Absolute(Equation)
            + AbsoluteMod(Equation, float)
            + ArcCos(Equation)
            + ArcCot(Equation)
            + ArcCsc(Equation)
            + ArcSec(Equation)
            + ArcSin(Equation)
            + ArcTan(Equation)
            + ArcCosh(Equation)
            + ArcCoth(Equation)
            + ArcCsch(Equation)
            + ArcSech(Equation)
            + ArcSinh(Equation)
            + ArcTanh(Equation)
            + Average(Equation, float, float, float)
            + Average(Equation, Equation, Equation, float)
            + Binomial(Equation, int, float)
            + Binomial(Equation, Equation, Equation)
            + Cbrt(Equation)
            + Ceiling(Equation)
            + Clamp(Equation, float, float)
            + Clamp(Equation, Equation, Equation)
            + Combinations(Equation, int)
            + Combinations(Equation, Equation)
            + Cos(Equation)
            + Cosh(Equation)
            + Cot(Equation)
            + Coth(Equation)
            + Csc(Equation)
            + Csch(Equation)
            + Divide(Equation, float[])
            + Divide(Equation, Equation[])
            + Factorial(Equation)
            + Floor(Equation)
            + GetDerivative(Equation, float)
            + GetDerivativeAtPoint(Equation, float, float)
            + GetIntegral(Equation, float, float, float)
            + GetDynamicIntegral(Equation, Equation, Equation, float)
            + GetTaylorSeries(Equation, float, int, float)
            + GetValues(Equation, float, float, float)
            + GradientDescent(Equation, float, float, int, float)
            + InverseSqrt(Equation)
            + Log(Equation, float)
            + Max(Equation, float, float, float)
            + Min(Equation, float, float, float)
            + Permutations(Equation, int)
            + Permutations(Equation, Equation)
            + Power(Equation, float)
            + Power(Equation, Equation)
            + Product(Equation, float[])
            + Product(Equation, Equation[])
            + Root(Equation, float)
            + Root(Equation, Equation)
            + Round(Equation)
            + Sec(Equation)
            + Sech(Equation)
            + Sin(Equation)
            + Sinh(Equation)
            + SolveBisection(Equation, float, float, float, float, int)
            + SolveEquation(Equation, float, float, float, int)
            + SolveNewton(Equation, float, float, float, int)
            + Sqrt(Equation)
            + Subtract(Equation, float[])
            + Subtract(Equation, Equation[])
            + Sum(Equation, float[])
            + Sum(Equation, Equation[])
            + Tan(Equation)
            + Tanh(Equation)
            + ZScore(Equation, float[])
            + ZScore(Equation, Equation[])
            + ZScore(Equation, float, float)
            + ZScore(Equation, Equation, Equation)
            + InvokeMethod(Equation, MethodInfo, object?[]?)
            + InvokeMathMethod(Equation, string, object?[]?)
        + StringExtension
    + Helpers
        + CordicHelper
        + MathfHelper
        + RationalHelper
        + UnsafeHelper
    * Mathematics
        * Abstract
            = Renamed `IPresets1D<T>` to `IPresets1d<T>`
            = Renamed `IPresets2D<T>` to `IPresets2d<T>`
            = Renamed `IPresets3D<T>` to `IPresets3d<T>`
            = Renamed `IPresets4D<T>` to `IPresets4d<T>`
            = Renamed `IShape2D<T>` to `IShape2d<T>`
            = Renamed `IShape3D<T>` to `IShape3d<T>`
        * NumberSystems
            * Complex
                - operator >(Complex, Complex)
                - operator <(Complex, Complex)
                - operator >=(Complex, Complex)
                - operator <=(Complex, Complex)
            * Quaternion
                - Far
                - Near
                - operator >(Complex, Complex)
                - operator <(Complex, Complex)
                - operator >=(Complex, Complex)
                - operator <=(Complex, Complex)
        * Samples
            + Fills
            * Equations
                + FlatLine
                + XLine
                = Simplified `CosWave`
                = Simplified `SinWave`
                = Replaced a `readonly` term with a generating field in `CosWave`
                = Replaced a `readonly` term with a generating field in `SinWave`
                = Replaced a `readonly` term with a generating field in `SawWave`
                = Replaced a `readonly` term with a generating field in `SquareWave`
                = Replaced a `readonly` term with a generating field in `SgnFill`
                = Moved `SgnFill` to `Fills` and renamed it to `SignFill`
        * Calculus
            + GetTaylorSeries(Equation, float, int, float)
            = Fixed a blunder in `GetDerivativeAtPoint(Equation, float, float)`
            = Renamed the `stepCount` parameter in `GradientDescent(Equation, float, float, float, float)` to "iterations" and changed its type from `float` to `int`
        * Float2
            - Removed the `Obsolete` attribute from `CompareTo(Float2)`
            - operator >(Float2, Float2)
            - operator <(Float2, Float2)
            - operator >=(Float2, Float2)
            - operator <=(Float2, Float2)
        * Float3
            - Removed the `Obsolete` attribute from `CompareTo(Float3)`
            - operator >(Float3, Float3)
            - operator <(Float3, Float3)
            - operator >=(Float3, Float3)
            - operator <=(Float3, Float3)
        * Float4
            - Far
            - Near
            - Removed the `Obsolete` attribute from `CompareTo(Float4)`
            - operator >(Float4, Float4)
            - operator <(Float4, Float4)
            - operator >=(Float4, Float4)
            - operator <=(Float4, Float4)
        * Int2
            - Removed the `Obsolete` attribute from `CompareTo(Int2)`
            - operator >(Int2, Int2)
            - operator <(Int2, Int2)
            - operator >=(Int2, Int2)
            - operator <=(Int2, Int2)
        * Int3
            - Removed the `Obsolete` attribute from `CompareTo(Int3)`
            - operator >(Int3, Int3)
            - operator <(Int3, Int3)
            - operator >=(Int3, Int3)
            - operator <=(Int3, Int3)
        * Int4
            - Far
            - Deep
            - Removed the `Obsolete` attribute from `CompareTo(Int4)`
            - operator >(Int4, Int4)
            - operator <(Int4, Int4)
            - operator >=(Int4, Int4)
            - operator <=(Int4, Int4)
        * Mathf
            + ArcCosh(float)
            + ArcCoth(float)
            + ArcCsch(float)
            + ArcSech(float)
            + ArcSinh(float)
            + ArcTanh(float)
            + ArcTanh2(float, float)
            + Cbrt(float)
            + Cosh(float)
            + Coth(float)
            + Csch(float)
            + IsPrime(int, PrimeCheckMethod)
            + Lerp(float, float, Equation, bool)
            + Lerp(Equation, Equation, float, bool)
            + Lerp(Equation, Equation, Equation, bool)
            + Log(float, float)
            + PrimeFactors(int)
            + PowerMod(long, long, long)
            + Sech(float)
            + Sinh(float)
            + SharedItems<T>(T[][])
            + SolveBisection(Equation, float, float, float, float, int)
            + SolveEquation(Equation, float, float, float, int)
            + SolveNewton(Equation, float, float, float, int)
            + Tanh(float)
            = Improved the `Sqrt(float)` method by using a solution finder
            = The `ArcSin(float)` method now uses a solution finder rather than the base math library
            = The `Power(float, float)` method now utilizes a custom CORDIC implementation rather than the base math library
        + PrimeCheckMethod
        + Equation2d
        + Rational
        + SimplificationMethod
    * Miscellaneous
        * AssemblyConfig
            - using System.Reflection
        * GlobalUsings
            + global using Nerd_STF.Helpers
            + global using System.Reflection
    - Foreach(object)
    - Foreach<T>(T)
    = Moved `IEncapsulator<T, TE>` to `Nerd_STF.Mathematics.Abstract` and renamed it to `IEncapsulate<T, TE>`
    = Renamed `Fill2D<T>` to `Fill2d<T>`
    = Renamed `IGroup2D<T>` to `IGroup2d<T>`
    = Renamed `Modifier2D` to `IModifier2d`
    = Renamed `Modifier2D<T>` to `IModifier2d<T>`
    = Renamed `Modifier2D<IT, VT>` to `IModifier2d<IT, VT>`
= Made `Nerd_STF` allow unsafe code blocks
```
