# Nerd_STF v2.3.1.39

***Read this to know what works and what doesn't!***

The `v2.3.1.x` updates go through every single field and method in Nerd_STF to make sure it works correctly.
You see, up until now I haven't actually tested literally anything at all. Partly because I didn't have the tools to and partly because I was lazy. But now, it's guarenteed to work in most cases (unless I like don't pick up some bug, you know).

The following types have been checked for the most part and can be safe to use:
- `Nerd_STF.FileType`
- `Nerd_STF.Fill`
- `Nerd_STF.Fill2D`
- `Nerd_STF.Foreach`
- `Nerd_STF.IClosest`
- `Nerd_STF.IContainer`
- `Nerd_STF.IEncapsulator`
- `Nerd_STF.IGroup`
- `Nerd_STF.IGroup2D`
- `Nerd_STF.Logger`
- `Nerd_STF.LogMessage`
- `Nerd_STF.LogSeverity`
- `Nerd_STF.Modifier`
- `Nerd_STF.Modifier2D`
- `Nerd_STF.Exceptions.DifferingVertCountException`
- `Nerd_STF.Exceptions.DisconnectedLinesException`
- `Nerd_STF.Exceptions.InvalidSizeException`
- `Nerd_STF.Exceptions.Nerd_STFException`
- `Nerd_STF.Exceptions.NoInverseException`
- `Nerd_STF.Extensions.ToFillExtension`
- `Nerd_STF.Graphics.ColorChannel`
- `Nerd_STF.Graphics.IlluminationFlags`
- `Nerd_STF.Graphics.IlluminationModel`
- `Nerd_STF.Graphics.Material`
- `Nerd_STF.Graphics.TextureConfig`
- `Nerd_STF.Mathematics.Angle`
- `Nerd_STF.Mathematics.Calculus`
- `Nerd_STF.Mathematics.Equation`
- `Nerd_STF.Mathematics.Float2`
- `Nerd_STF.Mathematics.Float3`
- `Nerd_STF.Mathematics.Float4`
- `Nerd_STF.Mathematics.Int2`
- `Nerd_STF.Mathematics.Int3`
- `Nerd_STF.Mathematics.Int4`
- `Nerd_STF.Mathematics.Mathf`
- `Nerd_STF.Mathematics.NumberSystems.Complex`
- `Nerd_STF.Mathematics.NumberSystems.Quaternion`
    - With the exception of the following methods:
        - `Quaternion.FromAngles(Angle, Angle, Angle?)`
        - `Quaternion.FromAngles(Float3, Angle.Type)`
        - `Quaternion.FromVector(Vector3d)`
        - `GetAngle()`
        - `GetAxis()`
        - `ToAngles()`
        - `ToVector()`
- `Nerd_STF.Mathematics.Samples.Constants`
- `Nerd_STF.Mathematics.Samples.Equations`

The following types haven't been checked yet, and should still be taken with a grain of salt:
- `Nerd_STF.Extensions.Container2DExtension`
- `Nerd_STF.Extensions.ConversionExtension`
- `Nerd_STF.Extensions.EquationExtension`
- `Nerd_STF.Graphics.CMYKA`
- `Nerd_STF.Graphics.CMYKAByte`
- `Nerd_STF.Graphics.HSVA`
- `Nerd_STF.Graphics.HSVAByte`
- `Nerd_STF.Graphics.IColor`
- `Nerd_STF.Graphics.IColorByte`
- `Nerd_STF.Graphics.Image`
- `Nerd_STF.Graphics.RGBA`
- `Nerd_STF.Graphics.RGBAByte`
- `Nerd_STF.Mathematics.Algebra.IMatrix`
- `Nerd_STF.Mathematics.Algebra.Matrix`
- `Nerd_STF.Mathematics.Algebra.Matrix2x2`
- `Nerd_STF.Mathematics.Algebra.Matrix3x3`
- `Nerd_STF.Mathematics.Algebra.Matrix4x4`
- `Nerd_STF.Mathematics.Algebra.Vector2d`
- `Nerd_STF.Mathematics.Algebra.Vector3d`
- `Nerd_STF.Mathematics.Geometry.Box2D`
- `Nerd_STF.Mathematics.Geometry.Box3D`
- `Nerd_STF.Mathematics.Geometry.ISubdividable`
- `Nerd_STF.Mathematics.Geometry.ITriangulatable`
- `Nerd_STF.Mathematics.Geometry.Line`
- `Nerd_STF.Mathematics.Geometry.Polygon`
- `Nerd_STF.Mathematics.Geometry.Quadrilateral`
- `Nerd_STF.Mathematics.Geometry.Sphere`
- `Nerd_STF.Mathematics.Geometry.Triangle`
- `Nerd_STF.Mathematics.Geometry.Vert`

I've checked a total of 39 types, with 29 left to go. It sounds like I'm over halfway, but really I've saved all of the annoying ones that are likely wrong to be checked next. My 39 done includes a bunch of really simple types that never needed checking in the first place too. But there are some done, like any type in the Mathematics directory.

```
* Nerd_STF
    * Exceptions
        + MathException
        + UndefinedException
    * Extensions
        + EquationExtension
    * Mathematics
        * NumberSystems
            * Complex
                + Inverse
                + GetAngle()
                = Fixed `ToString()` and all overloads from adding a redundant negative sign sometimes in the imaginary component.
                = Replaced the `/` operator with a multiplication by its inverse.
            * Quaternion
                + Inverse
                = Fixed `ToString()` and all overloads from adding a redundant negative sign sometimes in the i, j, and k components.
                = Replaced the `/` operator with a multiplication by its inverse.
        * Algebra
            * Vector2d
                - static Product(params Vector2d[])
                - static Divide(Vector2d, params Vector2d[])
                - operator *(Vector2d, Angle)
                - operator *(Vector2d, Vector2d)
                - operator /(Vector2d, Angle)
                - operator /(Vector2d, Vector2d)
                = Made `ToXYZ()` actually work
            * Vector3d
                - static Product(params Vector3d[])
                - static Divide(Vector3d, params Vector3d[])
                - operator *(Vector3d, Angle)
                - operator *(Vector3d, Vector3d)
                - operator /(Vector3d, Angle)
                - operator /(Vector3d, Vector3d)
        * Calculus
            + GetDynamicIntegral(Equation, Equation, Equation, float)
            = Made `GetDerivative(Equation, float)` calculate specific values on the fly, and removed the min/max.
        * Graphics
            * HSVA
                - static LerpSquared(HSVA, HSVA, float, Angle.Type, bool = true)
                - operator *(HSVA, HSVA)
                - operator /(HSVA, HSVA)
            * HSVAByte
                - static LerpSquared(HSVAByte, HSVAByte, float, Angle.Type, bool = true)
        * Angle
            + Reflected
            + static Max(bool, params Angle[])
            + static Min(bool, params Angle[])
            - operator *(Angle, Angle)
            - operator /(Angle, Angle)
            = Made `Angle(float, Type)` use a lambda expression
            = Made `Bounded` use the `AbsoluteMod()` function instead of the default `%` expression
            = Replaced an accidental `Mathf.Floor()` call with a `Mathf.Round()` call in `Floor()`
            = Made `operator -(Angle)` actually give you the reverse angle (not the reflected angle)
        * Float2
            = Made `Median(params Float2)` not use an unneccesary average
            = Made `ToVector()` not create an angle out of `ArcTan(float)`, as it already is one
            = Optimized `Divide(Float2, params Float2[])` to divide a product instead of individually dividing
            = Optimized `Subtract(Float2, params Float2[])` to subtract a sum instead of individually subtracting
        * Float3
            = Made `Median(params Float3)` not use an unneccesary average
            = Made `ToVector()` not create an angle out of the arc trig functions, as they already are angles
            = Optimized `Divide(Float3, params Float3[])` to divide a product instead of individually dividing
            = Optimized `Subtract(Float3, params Float3[])` to subtract a sum instead of individually subtracting
        * Float4
            = Marked `Far` and `Near` as obsolete/deprecated, as they have been replaced by `HighW` and `LowW`
            = Made `Median(params Float4)` not use an unneccesary average
            = Optimized `Divide(Float4, params Float4[])` to divide a product instead of individually dividing
            = Optimized `Subtract(Float4, params Float4[])` to subtract a sum instead of individually subtracting
        * Int2
            = Made `Median(params Float4)` not use an unneccesary average
            = Optimized `Divide(Float4, params Float4[])` to divide a product instead of individually dividing
            = Optimized `Subtract(Float4, params Float4[])` to subtract a sum instead of individually subtracting
        * Int3
            = Made `Median(params Float4)` not use an unneccesary average
            = Optimized `Divide(Float4, params Float4[])` to divide a product instead of individually dividing
            = Optimized `Subtract(Float4, params Float4[])` to subtract a sum instead of individually subtracting
        * Int4
            + LowW
            = Marked `Far` as obsolete/deprecated, as it has been replaced by `HighW`
            = Made `Median(params Float4)` not use an unneccesary average
            = Optimized `Divide(Float4, params Float4[])` to divide a product instead of individually dividing
            = Optimized `Subtract(Float4, params Float4[])` to subtract a sum instead of individually subtracting
        * Mathf
            + AbsoluteMod(float)
            + Binomial(int, int, float)
            + Factors(int)
            + PowerMod(int, int, int)
            = Fixed `Ceiling(float)` from rounding up when it shouldn't
            = Made `Median(params float[])` actually calculate the right midpoint
            = Removed two unneeded `Average(float, float)` calls in `Median(params float[])`
            = Replaced the `Floor(float)` and `Ceiling(float)` calls in `Median(params float[])` with a better alternative
            = Replaced the mod in `Sin(float)` with an `AbsoluteMod(float, float)` call
            = Made `MadeEquation(Dictionary<float, float>)` actually work.
            = Added caching to Power(float, int) for the absolute max
            = Fixed an if statement and added another one to prevent extra computation in `Power(float, int)`
            = Added caching to Power(int, int) for the absolute max
            = Fixed an if statement and added another one to prevent extra computation in `Power(int, int)`
            = Made the `Lerp(int, int, float, bool)` function not call itself infinitely many times.
            = Replaced a multiplication with a division in `Root(float, float)`
            = Made the `Average(int[])` function give you the int average instead of the float average (unlike `Average(float[])`)
            = Replaced the float return statement with an Angle in `ArcCos(float)`
            = Replaced the float return statement with an Angle in `ArcCot(float)`
            = Replaced the float return statement with an Angle in `ArcCsc(float)`
            = Replaced the float return statement with an Angle in `ArcSec(float)`
            = Replaced the float return statement with an Angle in `ArcSin(float)`
            = Replaced the float return statement with an Angle in `ArcTan(float)`
            = Replaced the float return statement with an Angle in `ArcTan2(float, float)`
            = Optimized `ArcCos(float)` to use presets
            = Optimized `Divide(float, params float[])` to use other functions.
            = Optimized `Divide(int, params int[])` to use other functions.
            = Optimized `Subtract(float, params float[])` to subtract a sum instead of individually subtracting
            = Optimized `Subtract(int, params int[])` to subtract a sum instead of individually subtracting
            = Made `Clamp(int, int, int)` not remake code and use `Clamp(float, float, float)`
            = Made `Lerp(int, int, float, bool)` cast to an int instead of flooring
            = Made `Lerp(float, float, float, bool)` not break when clamping and A is greater than B
            = Made `Median<T>(params T[])` not use an unneccesary average
            = Made `Power(int, int)` not care about absolutes
            = Made `Variance(params float[])` correctly calculate variance.
        * Quaternion
            = Made `GetAngle()` not create an angle out of `ArcCos(float)`, as it already is one
            = Made `ToAngles()` not create angles out of arc trig functions, as they are already angles
        * Samples
            * Constants
                = Swapped `DegToRad` and `RadToDeg`
            * Equations
                = Moved `static Scale(Equation, float, ScaleType)` to `EquationExtension`
                = Moved `ScaleType` to `EquationExtension`
```
