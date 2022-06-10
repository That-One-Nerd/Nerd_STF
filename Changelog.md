# Nerd_STF v2.2.0

This update adds many types of graphics-based objects, as well as some math functions and constants.

```
* Nerd_STF
    + delegate Fill2D<T>(int, int)
    * Exceptions
        + FileParsingException
    + FileType
    + Graphics
        + ColorChannel
        + CMYKA
        + CMYKAByte
        + HSVA
        + HSVAByte
        + IColor
        + IColorByte
        + IlluminationFlags
        + IlluminationModel
        + Image
        + Material
        + RGBA
        + RGBAByte
    * Mathematics
        * Angle
            + Normalized
            = Made fancier `ToString()` formatting
            * Type
                + Normalized
        + Constants
        * Float2
            + static SplitArray(params Float2[])
            = Renamed `static Multiply(params Float2[])` to `Product`
        * Float3
            + static SplitArray(params Float3[])
            + explicit operator Float3(RGBA)
            + explicit operator Float3(HSVA)
            + explicit operator Float3(RGBAByte)
            + explicit operator Float3(HSVAByte)
            = Renamed `static Multiply(params Float3[])` to `Product`
        * Float4
            + static SplitArray(params Float4[])
            + implicit operator Float4(RGBA)
            + explicit operator Float4(CMYKA)
            + implicit operator Float4(HSVA)
            + implicit operator Float4(RGBAByte)
            + explicit operator Float4(CMYKAByte)
            + implicit operator Float4(HSVAByte)
            = Renamed `static Multiply(params Float4[])` to `Product`
        * Geometry
            * Line
                + Midpoint
                = Renamed `ToDoubleArray()` to `ToFloatArray`
                = Renamed `ToDoubleList()` to `ToFloatList`
            * Polygon
                + Midpoint
                = Renamed `ToDoubleArray()` to `ToFloatArray`
                = Renamed `ToDoubleList()` to `ToFloatList`
                = Renamed `static ToDoubleArrayAll(params Triangle[])` to `ToFloatArrayAll`
                = Renamed `static ToDoubleListAll(params Triangle[])` to `ToFloatListAll`
            * Quadrilateral
                + Midpoint
                = Renamed `ToDoubleArray()` to `ToFloatArray`
                = Renamed `ToDoubleList()` to `ToFloatList`
                = Renamed `static ToDoubleArrayAll(params Triangle[])` to `ToFloatArrayAll`
                = Renamed `static ToDoubleListAll(params Triangle[])` to `ToFloatListAll`
            * Triangle
                + Midpoint
                = Renamed `ToDoubleArray()` to `ToFloatArray`
                = Renamed `ToDoubleList()` to `ToFloatList`
                = Renamed `static ToDoubleArrayAll(params Triangle[])` to `ToFloatArrayAll`
                = Renamed `static ToDoubleListAll(params Triangle[])` to `ToFloatListAll`
            * Vert
                = Renamed `static ToDouble3Array(params Vert[])` to `ToFloat3Array`
                = Renamed `static ToDouble3List(params Vert[])` to `ToFloat3List`
        * Int2
            + static SplitArray(params Int[])
            = Renamed `static Multiply(params Int2[])` to `Product`
        * Int3
            + static SplitArray(params Int3[])
            + explicit operator Int3(RGBA)
            + explicit operator Int3(HSVA)
            + explicit operator Int3(RGBAByte)
            + explicit operator Int3(HSVAByte)
            = Renamed `static Multiply(params Int3[])` to `Product`
        * Int4
            + static SplitArray(params Int4[])
            + explicit operator Int4(RGBA)
            + explicit operator Int4(CMYKA)
            + explicit operator Int4(HSVA)
            + implicit operator Int4(RGBAByte)
            + explicit operator Int4(CMYKAByte)
            + implicit operator Int4(HSVAByte)
            = Renamed `static Multiply(params Int4[])` to `Product`
        * Mathf
            + static Combinations(int, int)
            + static GreatestCommonFactor(params int[])
            + static InverseSqrt(float)
            + static LeastCommonMultiple(params int[])
            + static Mode<T>(params T[]) where T : IEquatable<T>
            + static Permutations(int, int)
            + static Pow(float, int)
            + static Product(Equation, float, float, float)
            + static Sum(Equation, float, float, float)
            + static UniqueItems<T>(params T[]) where T : IEquatable<T>
            + static ZScore(float, params float[])
            + static ZScore(float, float, float)
            - const RadToDeg
            - const E
            - const GoldenRatio
            - const HalfPi
            - const Pi
            - const DegToRad
            - const Tau
            = GreatestCommonFactor actually works now
            = Pow has been fixed
            = Mode actually works
            * static Average(params int[])
                = Replaced its `int` return type with `float`
    * Miscellaneous
        * GlobalUsings.cs
            + global using Nerd_STF.Graphics;
```
