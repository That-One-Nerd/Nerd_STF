# Nerd_STF v2.3.0

This update adds lots of linear algebra tools, like matrixes and vectors, as well as new number systems, like complex numbers and quaternions.

```
* Nerd_STF
    + Extensions
        + ConversionExtension
        + Container2DExtension
        + ToFillExtension
    + Foreach
    + IGroup2D
    + Modifier
    + Modifier2D
    * IGroup<T>
        + ToFill()
    * Exceptions
        + InvalidSizeException
        + NoInverseException
    * Graphics
        * CMYKA
            + ToFill()
            + static Round(CMYKA)
            = Replaced a false statement with `base.Equals(object?)` in `override bool Equals(object?)`
        * CMYKAByte
            + ToFill()
            = Replaced a false statement with `base.Equals(object?)` in `override bool Equals(object?)`
        * HSVA
            + ToFill()
            + static Round(HSVA, Angle.Type)
            = Replaced a false statement with `base.Equals(object?)` in `override bool Equals(object?)`
        * HSVAByte
            + ToFill()
            = Replaced a false statement with `base.Equals(object?)` in `override bool Equals(object?)`
        * Image
            = Replaced a false statement with `base.Equals(object?)` in `override bool Equals(object?)`
        * Material
            = Replaced a false statement with `base.Equals(object?)` in `override bool Equals(object?)`
            = Merged 2 if statements into 1 in `override bool Equals(object?)`
        * RGBA
            + ToFill()
            + ToVector()
            + static Round(RGBA)
            = Replaced a false statement with `base.Equals(object?)` in `override bool Equals(object?)`
        * RGBAByte
            + ToFill()
            + ToVector()
            = Replaced a false statement with `base.Equals(object?)` in `override bool Equals(object?)`
    * Mathematics
        + Algebra
            + IMatrix
            + Matrix2x2
            + Matrix3x3
            + Matrix4x4
            + Vector2d
            + Vector3d
        + NumberSystems
            + Complex
            + Quaternion
        + Samples
            + Equations
                + ScaleType
        = Moved `Constants` file to Samples folder.
        * Geometry
            * Box2D
                = Replaced a false statement with `base.Equals(object?)` in `override bool Equals(object?)`
            * Box3D
                = Replaced a false statement with `base.Equals(object?)` in `override bool Equals(object?)`
            * Line
                + ToFill()
                = Replaced a false statement with `base.Equals(object?)` in `override bool Equals(object?)`
            * Polygon
                + ToFill()
                = Replaced a false statement with `base.Equals(object?)` in `override bool Equals(object?)`
            * Quadrilateral
                + ToFill()
                = Replaced a false statement with `base.Equals(object?)` in `override bool Equals(object?)`
            * Sphere
                = Replaced a false statement with `base.Equals(object?)` in `override bool Equals(object?)`
            * Triangle
                + ToFill()
                = Replaced a false statement with `base.Equals(object?)` in `override bool Equals(object?)`
            * Vert
                + ToFill()
                + ToVector()
                = Made `Vert(Float2)` not recreate a float group, and instead use itself.
                = Replaced a false statement with `base.Equals(object?)` in `override bool Equals(object?)`
        * Angle
            + static Down
            + static Left
            + static Right
            + static Up
            + static Round(Angle, Type)
            = Replaced a false statement with `base.Equals(object?)` in `override bool Equals(object?)`
        * Float2
            + ToFill()
            + ToVector()
            + static Round(Float2)
            + implicit operator Float2(Complex)
            + explicit operator Float2(Quaternion)
            + explicit operator Float2(Matrix)
            + operator *(Float2, Matrix)
            + operator /(Float2, Matrix)
            = Made `Normalized` multiply by the inverse square root instead of dividing by the square root.
            = Replaced a false statement with `base.Equals(object?)` in `override bool Equals(object?)`
        * Float3
            + ToFill()
            + ToVector()
            + static Round(Float3)
            + implicit operator Float3(Complex)
            + explicit operator Float3(Quaternion)
            + explicit operator Float3(Matrix)
            + operator *(Float3, Matrix)
            + operator /(Float3, Matrix)
            = Made `Normalized` multiply by the inverse square root instead of dividing by the square root.
            = Replaced a false statement with `base.Equals(object?)` in `override bool Equals(object?)`
        * Float4
            + ToFill()
            + static Round(Float4)
            + implicit operator Float4(Complex)
            + implicit operator Float4(Quaternion)
            + explicit operator Float4(Matrix)
            + operator *(Float4, Matrix)
            + operator /(Float4, Matrix)
            = Made `Normalized` multiply by the inverse square root instead of dividing by the square root.
            = Replaced a false statement with `base.Equals(object?)` in `override bool Equals(object?)`
            = Renamed `Float4.Deep` to `Float4.Near`
        * Int2
            + ToFill()
            + explicit operator Int2(Complex)
            + explicit operator Int2(Quaternion)
            + explicit operator Int2(Matrix)
            + operator *(Int2, Matrix)
            + operator /(Int2, Matrix)
            = Made `Normalized` multiply by the inverse square root instead of dividing by the square root.
            = Replaced a false statement with `base.Equals(object?)` in `override bool Equals(object?)`
        * Int3
            + ToFill()
            + explicit operator Int3(Complex)
            + explicit operator Int3(Quaternion)
            + explicit operator Int3(Matrix)
            + operator *(Int3, Matrix)
            + operator /(Int3, Matrix)
            = Made `Normalized` multiply by the inverse square root instead of dividing by the square root.
            = Replaced a false statement with `base.Equals(object?)` in `override bool Equals(object?)`
        * Int4
            + explicit operator Int4(Complex)
            + explicit operator Int4(Quaternion)
            + explicit operator Int4(Matrix)
            + operator *(Int4, Matrix)
            + operator /(Int4, Matrix)
            = Made `Normalized` multiply by the inverse square root instead of dividing by the square root.
            = Replaced a false statement with `base.Equals(object?)` in `override bool Equals(object?)`
        * Mathf
            + Cos(Angle)
            + Cot(Angle)
            + Csc(Angle)
            + Dot(float[], float[])
            + Dot(float[][])
            + Max<T>(T[]) where T : IComparable<T>
            + Median<T>(T[])
            + Min<T>(T[]) where T : IComparable<T>
            + Sec(Angle)
            + Sin(Angle)
            + Tan(Angle)
    * Miscellaneous
        * GlobalUsings.cs
            + global using Nerd_STF.Collections
            + global using Nerd_STF.Extensions
            + global using Nerd_STF.Mathematics.Algebra
            + global using Nerd_STF.Mathematics.NumberSystems
            + global using Nerd_STF.Mathematics.Samples
```
