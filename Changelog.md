# Nerd_STF v2.3.1

***Everything has been tested and most things work!***

**WARNING:**
All of the matrix classes have had all of their constructors' row and column variables swapped. You'll have to switch all your variables around.
Sorry for the inconvenience :(.

The `v2.3.1.x` updates go through every single field and method in Nerd_STF to make sure it works correctly.
You see, up until now I haven't actually tested literally anything at all. Partly because I didn't have the tools to and partly because I was lazy. But now, it's guarenteed to work in most cases (unless I like don't pick up some bug, you know).

Hi everyone! Everything has been checked now and most stuff works! Not everything, like the triangle and quadrilateral area stuff, but for the most part, it's all cool and good.

I've just now remembered how bad the Polygon struct was. It's getting remade. The v2.4.0 update will be mostly geometry focused, so that'll be the best time to figure out how to fix this stuff. The new Polygon struct will be much better, trust me.

But all the matrix structs work like charm now! I'm honestly suprised they worked as well as they did before (especially the dynamic matrix). They can now be relied on.

Next up is the documentation update. Stay tuned!
(This may be another update with beta parts, but I'm not sure yet. We'll see).

```
* Nerd_STF
    * Exceptions
        * DifferingVertCountException
            = Marked as deprecated (uses deprecated struct `Polygon`)
    * Extensions
        * Container2DExtension
            + GetSize<T>(T[,])
            + SwapDimensions<T>(T[,], Int2?)
            * Flatten<T>(T[,])
                = Replaced a `size` parameter from an `Int2` to an `Int2?`
    * Mathematics
        * Algebra
            * IMatrix
                - ToDictionary()
            * Matrix
                + Cofactor()
                + IdentityIsh(Int2)
                + MinorOf(Int2)
                - ToDictionary()
                = Fixed `Determinant()`
                = Fixed `Minors()`
                = Fixed `Inverse()`
                = Made `Identity(Int2)` only work with square matricies (since that's only when an identity exists)
                = Marked the struct as `readonly`
                = Simplified `Transpose()`
                = Swapped row variables with column variables in all constructors (and methods that require those constructors).
                = Swapped code for `Adjugate()` with `Cofactor()`
            * Matrix2x2
                + Cofactor()
                + operator *(Matrix2x2, Float2)
                + operator /(Matrix2x2, Float2)
                - ToDictionary()
                = Swapped code for `Adjugate()` with `Cofactor()`
                = Swapped row variables with column variables in all constructors (and methods that require those constructors).
                = Fixed `this[int, int]` to compensate for the swapped variables.
                = Fixed `operator -(Matrix2x2, Matrix2x2)` to not have an addition in one of the variables (fun).
                = Fixed `Inverse()`
                = Fixed `explicit operator Matrix2x2(Matrix)`
            * Matrix3x3
                + Cofactor()
                + operator *(Matrix3x3, Float3)
                + operator /(Matrix3x3, Float3)
                - ToDictionary()
                = Swapped code for `Adjugate()` with `Cofactor()`
                = Swapped row variables with column variables in all constructors (and methods that require those constructors).
                = Fixed `this[int, int]` to compensate for the swapped variables.
                = Fixed `Determinant()`
                = Fixed `Inverse()`
                = Fixed `explicit operator Matrix3x3(Matrix)`
            * Matrix4x4
                + Cofactor()
                + override string ToString()
                + operator *(Matrix4x4, Float4)
                + operator /(Matrix4x4, Float4)
                - ToDictionary()
                = Swapped code for `Adjugate()` with `Cofactor()`
                = Swapped row variables with column variables in all constructors (and methods that require those constructors).
                = Fixed `this[int, int]` to compensate for the swapped variables.
                = Fixed `Determinant()`
                = Fixed a typo in `Absolute(Matrix4x4)`, `Ceiling(Matrix4x4)`, `Floor(Matrix4x4)`, and `Round(Matrix4x4)`
                = Fixed a typo in `Row1`, `Row2`, `Row3`, and `Row4`. Oops.
                = Fixed some missing elements in `SplitArray(Matrix4x4[])`
                = Fixed `explicit operator Matrix4x4(Matrix)`
        * Geometry
            * Box2D
                = Simplified some code in `Perimeter`
            * Box3D
                + SurfaceArea
                = Renamed `Area` to `Volume`
                = Simplified some code in `Perimeter`
            * Polygon
                = Marked as deprecated (will be redone in v2.4.0)
                = Simplified collection initialization in `Triangulate()`
            * Quadrilateral
                - explicit operator Triangle(Polygon)
                = Marked `Area` as deprecated (uses deprecated `Triangle.Area` field)
            * Sphere
                = Fixed `ClosestTo(Vert)`
            * Triangle
                - explicit operator Triangle(Polygon)
                = Marked `Area` as deprecated (will be fixed in v2.4.0)
            * Vert
                = Marked as deprecated (will be removed in v2.4.0).
                = Optimized `Normalized` to not clone more than required.
```
