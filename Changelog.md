# Nerd_STF v2.3.1.52

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
- `Nerd_STF.Extensions.Container2DExtension`
- `Nerd_STF.Extensions.ConversionExtension`
- `Nerd_STF.Extensions.EquationExtension`
- `Nerd_STF.Extensions.ToFillExtension`
- `Nerd_STF.Graphics.CMYKA`
- `Nerd_STF.Graphics.CMYKAByte`
- `Nerd_STF.Graphics.ColorChannel`
- `Nerd_STF.Graphics.HSVA`
- `Nerd_STF.Graphics.HSVAByte`
- `Nerd_STF.Graphics.IColor`
- `Nerd_STF.Graphics.IColorByte`
- `Nerd_STF.Graphics.IlluminationFlags`
- `Nerd_STF.Graphics.IlluminationModel`
- `Nerd_STF.Graphics.Image`
- `Nerd_STF.Graphics.Material`
- `Nerd_STF.Graphics.RGBA`
- `Nerd_STF.Graphics.RGBAByte`
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
- `Nerd_STF.Mathematics.Geometry.ISubdividable`
- `Nerd_STF.Mathematics.NumberSystems.Complex`
- `Nerd_STF.Mathematics.NumberSystems.Quaternion`
- `Nerd_STF.Mathematics.Samples.Constants`
- `Nerd_STF.Mathematics.Samples.Equations`

The following types haven't been checked yet, and should still be taken with a grain of salt:
- `Nerd_STF.Mathematics.Algebra.IMatrix`
- `Nerd_STF.Mathematics.Algebra.Matrix`
- `Nerd_STF.Mathematics.Algebra.Matrix2x2`
- `Nerd_STF.Mathematics.Algebra.Matrix3x3`
- `Nerd_STF.Mathematics.Algebra.Matrix4x4`
- `Nerd_STF.Mathematics.Algebra.Vector2d`
- `Nerd_STF.Mathematics.Algebra.Vector3d`
- `Nerd_STF.Mathematics.Geometry.Box2D`
- `Nerd_STF.Mathematics.Geometry.Box3D`
- `Nerd_STF.Mathematics.Geometry.ITriangulatable`
- `Nerd_STF.Mathematics.Geometry.Line`
- `Nerd_STF.Mathematics.Geometry.Polygon`
- `Nerd_STF.Mathematics.Geometry.Quadrilateral`
- `Nerd_STF.Mathematics.Geometry.Sphere`
- `Nerd_STF.Mathematics.Geometry.Triangle`
- `Nerd_STF.Mathematics.Geometry.Vert`

16 left to go.

Honestly, most of the time taken for this update was spent on Quaternions. Turns out my multiply function was subtley wrong. Who knew!
Just a relief to be done with it. The other stuff wasn't too much of a problem. Matrixes are probably going to be a huge pain if they don't work first try, though. That'll be in the next update, probably.

I should also note that I just realized after way to long that the `.csproj` file isn't included in the Github. It's included in this new release, and sometime in the future I'll go back and add a correctly working `.csproj` file to all the other releases as well (with the exception of the legacy Nerd_STF 2021 versions likely. We'll see). I'll now not include the `/bin` build files. They'll be in the release and you can build it yourself if you need to now. Anyway, have fun.

```
* Nerd_STF
    * Extensions
        * Container2DExtension
            = Fixed `Flatten<T>(T[,], Int2)`
            = `GetColumn<T>(T[,], int, int)` and `GetRow<T>(T[,], int, int)` have been fixed (They had swapped roles)
        * ConversionExtension
            + ToFill<T>(T[])
            + ToFill<T>(T[,], Int2)
            + ToFill2D<T>(T[,])
        * EquationExtension
            = Fixed `Scale(Equation, float, ScaleType)` by swapping all instances of `x` and `value` (oops)
            = Moved `ScaleType` out of parent class `EquationExtension` and into namespace `Nerd_STF`
    * Geometry
        * ITriangulatable
            + TriangulateAll<T>(T[]) where T : ITriangulatable
    * Graphics
        + Renamed `IColor` to `IColorFloat`
        = Made IColor an object both `IColorFloat` and `IColorByte` inherit from.
        * CMYKA
            = Made `ToRGBA()` include the alpha value of the color.
        * CMYKAByte
            = In `ToHSVA()` and `ToHSVAByte()`, swapped some conversions from `RGBA` to `CMYKA`
        * HSVA
            = Made `ToRGBA()` include the alpha value of the color.
        * Image
            - Removed some useless constructors
            = Fixed some broken constructors
    * Mathematics
        * Algebra
            * Vector2d
                = Removed a default parameter value in `ToString(Angle.Type)` to prevent confusion.
            * Vector3d                
                + string ToString()
                + string ToString(Angle.Type)
                + string ToString(IFormatProvider, Angle.Type)
                = Fixed `ToXYZ()`
        * NumberSystems
            * Complex
                + operator ~(Complex)
            * Quaternion
                + Rotate(Float3)
                + Rotate(Vector3d)
                + operator ~(Quaternion)
                - ToVector()
                = Gave `IJK` proper get and set accessors.
                = Renamed some terms in `ToString()`
                = Fixed the `Quaternion.FromAngles(*)` methods to do the proper thing.
                = Fixed `Quaternion.Rotate(Quaternion)`
                = Fixed `Quaternion.Rotate(Float3)`
                = Fixed `operator *(Quaternion, Quaternion)`
                = Optimized `GetAxis()`
                = Simplified `ToXYZ()`
                = Swapped the order of `Rotate(Quaternion)`
                = Made `GetAxis()` not accidentally create an infinite vector.
        * Angle
            + Complimentary
            + Supplementary
        * Float2
            + operator *(Float2, Quaternion)
        * Float3
            + operator *(Float3, Quaternion)
            = Fixed `ToVector()`
```
