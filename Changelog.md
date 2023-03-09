# Nerd_STF v2.3.2

A bunch of stuff has changed, hasn't it?

This update was originally intended to be a support update. But among other problems, I don't want to maintain 3 slightly different versions of Nerd_STF at the same time. So I'm going to put the support update on hold for now. I won't delete my progress on it (I got about a quarter of the way done with support for .NET Standard 2.0. It's a big undertaking), I won't promise any completion date either.

Instead, this update ended up being a quality-of-life update. I already finished this part of the update before I decided to cancel the support update. This update now has lots of support for the new .NET 7 features. I'm now using a bunch of static abstract interfaces. I also added range indexing to almost all of the types available. The color byte types now have `int` fields instead of `byte` fields because `int`s are much more common. The values are automatically clamped between 0 and 255 just in case. Basically all types are records now because records are really nice. Lastly, some stuff has been marked as deprecated and to be removed in a future release because they already, exist, are kind of useless, and/or are misleading. Most of the deprecated stuff will be removed in either `2.4.0` or `2.5.0`.

***Also, just want to note that despite the `Vert` struct not being marked as deprecated, it will still be removed in `2.5.0`. I didn't mark it because that would have created a bunch of warnings in my library. And I don't like warnings.***

One final note, I've changed the description of the library and I've changed a bunch of the assembly and compilation settings.

Anyway, that's it for this update. The longest delay was just getting this project to other versions of .NET. Stay tuned for v2.4.0, the *Equations and Numbers* update!

```
* Nerd_STF
    * Exceptions
        * InvalidSizeException
            = Swapped a `this` method call for a `base` method call in `InvalidSizeException()`
        * MathException
            = Made `MathException()` have a default message
        * Nerd_STFException
            + Nerd_STFException(Exception)
            = Made `Nerd_STFException()` have a default message
            = Made `Nerd_STFException()` invoke the base constructor
        * NoInverseException
            = Changed base parent from `Exception` to `Nerd_STFException`
        * UndefinedException
            = Gave a better default message in `UndefinedException()`
    * Extensions
        * ConversionExtension
            * ToFill<T>(T[,])
                = Made the type parameter `size` nullable (if null, will be replaced with automatic size)
            = Marked `ToDictionary<TKey, TValue>(IEnumerable<KeyValuePair<TKey, Value>>)` as obsolete, as the `Dictionary` type already has a constructor for it.
    * Graphics
        + Abstract
            + IColor<T>
            + IColorByte<T>
            + IColorFloat<T>
            + IColorPresets<T>
        * CMYKA
            + Made `CMYKA` a record
            + : IAverage<CMYKA>
            + : IClamp<CMYKA>
            + : IColorFloat<CMYKA>
            + : IColorPresets<CMYKA>
            + : IIndexAll<float>
            + : IIndexRangeAll<float>
            + : ILerp<CMYKA, float>
            + : IMedian<CMYKA>
            + : ISplittable<CMYKA, (float[] Cs, float[] Ms, float[] Ys, float[] Ks, float[] As)>
            + float this[Index]
            + float[] this[Range]
            + Equals(IColor?)
            + PrintMembers(StringBuilder)
            - : IColorFloat
            - Ceiling(CMYKA)
            - Floor(CMYKA)
            - Max(CMYKA[])
            - Min(CMYKA[])
            - Round(CMYKA)
            - override Equals(object?)
            - Equals(IColorFloat?)
            - Equals(IColorByte?)
            - override ToString()
            - ToString(string?)
            - ToString(IFormatProvider)
            - Clone()
            - operator ==(CMYKA, CMYKA)
            - operator !=(CMYKA, CMYKA)
            = Made `GetHashCode()` invoke the base method
        * CMYKAByte
            + Made `CMYKAByte` a record
            + : IAverage<CMYKAByte>
            + : IClamp<CMYKAByte>
            + : IColorByte<CMYKAByte>
            + : IColorPresets<CMYKAByte>
            + : IIndexAll<int>
            + : IIndexRangeAll<int>
            + : ILerp<CMYKAByte, float>
            + : IMedian<CMYKAByte>
            + : ISplittable<CMYKAByte, (byte[] Cs, byte[] Ms, byte[] Ys, byte[] Ks, byte[] As)>
            + p_c
            + p_m
            + p_y
            + p_k
            + p_a
            + int this[Index]
            + int[] this[Range]
            + Equals(IColor?)
            + PrintMembers(StringBuilder)
            + ToArrayInt()
            + ToFillInt()
            + ToListInt()
            - : IColorByte
            - Max(CMYKAByte[])
            - Min(CMYKAByte[])
            - override Equals(object?)
            - Equals(IColorFloat?)
            - Equals(IColorByte?)
            - override ToString()
            - ToString(string?)
            - ToString(IFormatProvider)
            - Clone()
            - operator ==(CMYKAByte, CMYKAByte)
            - operator !=(CMYKAByte, CMYKAByte)
            = Changed the return type of `Black` from `CMYKA` to `CMYKAByte`
            = Changed the return type of `Blue` from `CMYKA` to `CMYKAByte`
            = Changed the return type of `Clear` from `CMYKA` to `CMYKAByte`
            = Changed the return type of `Cyan` from `CMYKA` to `CMYKAByte`
            = Changed the return type of `Gray` from `CMYKA` to `CMYKAByte`
            = Changed the return type of `Green` from `CMYKA` to `CMYKAByte`
            = Changed the return type of `Magenta` from `CMYKA` to `CMYKAByte`
            = Changed the return type of `Orange` from `CMYKA` to `CMYKAByte`
            = Changed the return type of `Purple` from `CMYKA` to `CMYKAByte`
            = Changed the return type of `Red` from `CMYKA` to `CMYKAByte`
            = Changed the return type of `White` from `CMYKA` to `CMYKAByte`
            = Changed the return type of `Yellow` from `CMYKA` to `CMYKAByte`
            = Made `C` a property that relates to `p_c`
            = Made `M` a property that relates to `p_m`
            = Made `Y` a property that relates to `p_y`
            = Made `K` a property that relates to `p_k`
            = Made `A` a property that relates to `p_a`
            = Made `this[int]` return an `int` instead of a `byte`
            = Made `SplitArray(CMYKAByte[])` use the private members of the type.
            = Made `ToArray()` use the private members of the type.
            = Made `ToList()` use the private members of the type.
            = Made `GetEnumerator()` use the private members of the type.
            = Made `GetHashCode()` invoke the base method
        * HSVA
            + Made `HSVA` a record
            + : IAverage<HSVA>
            + : IClamp<HSVA>
            + : IColorFloat<HSVA>
            + : IColorPresets<HSVA>
            + : IIndexAll<HSVA>
            + : IIndexRangeAll<HSVA>
            + : ILerp<HSVA, float>
            + : IMedian<HSVA>
            + : ISplittable<HSVA, (Angle[] Hs, float[] Ss, float[] Vs, float[] As)>
            + float this[Index]
            + float[] this[Range]
            + Equals(IColor?)
            + PrintMembers(StringBuilder)
            - : IColorFloat
            - Ceiling(HSVA)
            - Floor(HSVA)
            - Max(HSVA[])
            - Min(HSVA[])
            - Round(HSVA)
            - override Equals(object?)
            - Equals(IColorFloat?)
            - Equals(IColorByte?)
            - override ToString()
            - ToString(string?)
            - ToString(IFormatProvider)
            - Clone()
            - operator ==(HSVA, HSVA)
            - operator !=(HSVA, HSVA)
            = Made `GetHashCode()` invoke the base method
            = Optimized some clamping in `this[int]`
        * HSVAByte
            + Made `HSVAByte` a record
            + : IAverage<HSVAByte>
            + : IClamp<HSVAByte>
            + : IColorByte<HSVAByte>
            + : IColorPresets<HSVAByte>
            + : IIndexAll<int>
            + : IIndexRangeAll<int>
            + : ILerp<HSVAByte, float>
            + : IMedian<HSVAByte>
            + : ISplittable<HSVAByte, (byte[] Hs, byte[] Ss, byte[] Vs, byte[] As)>
            + p_h
            + p_s
            + p_v
            + p_a
            + int this[Index]
            + int[] this[Range]
            + Equals(IColor?)
            + PrintMembers(StringBuilder)
            + ToArrayInt()
            + ToFillInt()
            + ToListInt()
            - : IColorByte
            - Max(HSVAByte[])
            - Min(HSVAByte[])
            - override Equals(object?)
            - Equals(IColorFloat?)
            - Equals(IColorByte?)
            - override ToString()
            - ToString(string?)
            - ToString(IFormatProvider)
            - Clone()
            - operator ==(HSVAByte, HSVAByte)
            - operator !=(HSVAByte, HSVAByte)
            = Changed the return type of `Black` from `HSVA` to `HSVAByte`
            = Changed the return type of `Blue` from `HSVA` to `HSVAByte`
            = Changed the return type of `Clear` from `HSVA` to `HSVAByte`
            = Changed the return type of `Cyan` from `HSVA` to `HSVAByte`
            = Changed the return type of `Gray` from `HSVA` to `HSVAByte`
            = Changed the return type of `Green` from `HSVA` to `HSVAByte`
            = Changed the return type of `Magenta` from `HSVA` to `HSVAByte`
            = Changed the return type of `Orange` from `HSVA` to `HSVAByte`
            = Changed the return type of `Purple` from `HSVA` to `HSVAByte`
            = Changed the return type of `Red` from `HSVA` to `HSVAByte`
            = Changed the return type of `White` from `HSVA` to `HSVAByte`
            = Changed the return type of `Yellow` from `HSVA` to `HSVAByte`
            = Changed the type of the parameter `t` from a `byte` to a `float` in `Lerp(HSVAByte, HSVAByte, byte, bool)`
            = Made `H` a property that relates to `p_h`
            = Made `S` a property that relates to `p_s`
            = Made `V` a property that relates to `p_v`
            = Made `A` a property that relates to `p_a`
            = Made `this[int]` return an `int` instead of a `byte`
            = Made `SplitArray(HSVAByte[])` use the private members of the type.
            = Made `ToArray()` use the private members of the type.
            = Made `ToList()` use the private members of the type.
            = Made `GetEnumerator()` use the private members of the type.
            = Made `GetHashCode()` invoke the base method
        * Image
            + : IEnumerable<IColor>
            - : IEnumerable
            = Added a nullabilty modifier in `Equals(Image)`
            = Changed a modifier in `Pixels` from `init` to `private set`
            = Changed a modifier in `Size` from `init` to `private set`
            = Fixed some random bug in `ModifySaturation(float, bool)` where if `set` is set to `true`, the saturation is completely zeroed out.
            = Replaced a `this` assignment in `Scale(Float2)` with individual component assignments
            = Turned `Image` into a `class` (from a `struct`)
        * RGBA
            + Made `RGBA` a record
            + : IAverage<RGBA>
            + : IClamp<RGBA>
            + : IColorFloat<RGBA>
            + : IColorPresets<RGBA>
            + : IIndexAll<float>
            + : IIndexRangeAll<float>
            + : ILerp<RGBA, float>
            + : IMedian<RGBA>
            + : ISplittable<RGBA, (float[] Rs, float[] Gs, float[] Bs, float[] As)>
            + float this[Index]
            + float[] this[Range]
            + Equals(IColor?)
            + PrintMembers(StringBuilder)
            - : IColorFloat
            - Ceiling(RGBA)
            - Floor(RGBA)
            - Max(RGBA[])
            - Min(RGBA[])
            - Round(RGBA)
            - override Equals(object?)
            - Equals(IColorFloat?)
            - Equals(IColorByte?)
            - override ToString()
            - ToString(string?)
            - ToString(IFormatProvider)
            - Clone()
            - operator ==(RGBA, RGBA)
            - operator !=(RGBA, RGBA)
            = Made `GetHashCode()` invoke the base method
        * RGBAByte
            + Made `RGBAByte` a record
            + : IAverage<RGBAByte>
            + : IClamp<RGBAByte>
            + : IColorByte<RGBAByte>
            + : IColorPresets<RGBAByte>
            + : IIndexAll<int>
            + : IIndexRangeAll<int>
            + : ILerp<RGBAByte, float>
            + : IMedian<RGBAByte>
            + : ISplittable<RGBAByte, (float[] Rs, float[] Gs, float[] Bs, float[] As)>
            + p_r
            + p_g
            + p_b
            + p_a
            + int this[Index]
            + int[] this[Range]
            + Equals(IColor?)
            + PrintMembers(StringBuilder)
            + ToArrayInt()
            + ToFillInt()
            + ToListInt()
            - : IColorByte
            - Max(RGBAByte[])
            - Min(RGBAByte[])
            - override Equals(object?)
            - Equals(IColorFloat?)
            - Equals(IColorByte?)
            - override ToString()
            - ToString(string?)
            - ToString(IFormatProvider)
            - Clone()
            - operator ==(RGBAByte, RGBAByte)
            - operator !=(RGBAByte, RGBAByte)
            = Changed the type of the parameter `t` from a `byte` to a `float` in `Lerp(RGBAByte, RGBAByte, byte, bool)`
            = Made `R` a property that relates to `p_r`
            = Made `G` a property that relates to `p_g`
            = Made `B` a property that relates to `p_b`
            = Made `A` a property that relates to `p_a`
            = Made `this[int]` return an `int` instead of a `byte`
            = Made `SplitArray(RGBAByte[])` use the private members of the type.
            = Made `ToArray()` use the private members of the type.
            = Made `ToList()` use the private members of the type.
            = Made `GetEnumerator()` use the private members of the type.
            = Made `GetHashCode()` invoke the base method
            = Fixed a bug in `ToFill()` where it would return `HSVAByte` values instead
        = Moved `IColor` to `Nerd_STF.Graphics.Abstract`
            + : IEquatable<IColor>
            - : ICloneable
            - : IEquatable<IColorByte?>
            - : IEquatable<IColorFloat?>
        = Moved `IColorByte` to `Nerd_STF.Graphics.Abstract`
            + ToArrayInt()
            + ToFillInt()
            + ToListInt()
        = Moved `IColorFloat` to `Nerd_STF.Graphics.Abstract`
    * Mathematics
        + Abstract
            + IAbsolute<T>
            + IAverage<T>
            + ICeiling<TSelf>
            + ICeiling<TSelf, TRound>
            + IClamp<T>
            + IClampMagnitude<TSelf>
            + IClampMagnitude<TSelf, TNumber>
            + ICross<TSelf>
            + ICross<TSelf, TOut>
            + IDivide<T>
            + IDot<TSelf>
            + IDot<TSelf, TNumber>
            + IFloor<TSelf>
            + IFloor<TSelf, TRound>
            + IIndexAll<TSub>
            + IIndexGet<TSub>
            + IIndexRangeAll<TSub>
            + IIndexRangeGet<TSub>
            + IIndexRangeSet<TSub>
            + IIndexSet<TSub>
            + ILerp<TSelf>
            + ILerp<TSelf, TNumber>
            + IMagnitude<TNumber>
            + IMax<T>
            + IMatrixPresets<T>
            + IMedian<T>
            + IMin<T>
            + IPresets1D<T>
            + IPresets2D<T>
            + IPresets3D<T>
            + IPresets4D<T>
            + IProduct<T>
            + IRound<TSelf>
            + IRound<TSelf, TRound>
            + IShape2D<TNumber>
            + IShape3D<TNumber>
            + ISplittable<TSelf, TTuple>
            + IStaticMatrix<T>
            + ISubtract<T>
            + ISum<T>
            + IVector2<T>
        * Algebra
            * Matrix
                + this[Index, Index]
                + this[Range, Range]
                - ToString(string?)
                - ToString(IFormatProvider)
                = Added a better exception description in `Identity(Int2)`
                = Added a nullability attribute to the return type of `Inverse()`
                = Added a nullability attribute to the return type of `operator -(Matrix)`
                = Added a nullability check in `Equals(Matrix)`
                = Turned `Matrix` into a `class` (from a `struct`)
                = Changed the parameter `other` in `Equals(Matrix)` to a nullable equivalent and added a nullability check
                = Made `Inverse()` return `null` if there is no inverse instead of throwing an exception.
                = Made `operator /(Matrix, Matrix)` throw an exception if no inverse exists for matrix `b`
                = Marked `Equals(Matrix)` as virtual
            * Matrix2x2
                + Made `Matrix2x2` into a record
                + : IStaticMatrix<Matrix2x2>
                + this[Index, Index]
                + this[Range, Range]
                - : IMatrix<Matrix2x2>
                - override Equals(object?)
                - ToString(string?)
                - ToString(IFormatProvider)
                - Clone()
                - operator ==(Matrix2x2, Matrix2x2)
                - operator !=(Matrix2x2, Matrix2x2)
                = Added a nullability attribute to the return type of `Inverse()`
                = Turned `Matrix2x2` into a `class` (from a `struct`)
                = Marked `Equals(Matrix2x2)` as virtual
                = Made `GetHashCode()` invoke the base method
                = Made `Inverse()` return `null` if there is no inverse instead of throwing an exception.
                = Made `operator /(Matrix2x2, Matrix2x2)` throw an exception if no inverse exists for matrix `b`
                = Changed the parameter `other` in `Equals(Matrix2x2)` to a nullable equivalent and added a nullability check
            * Matrix3x3
                + Made `Matrix3x3` into a record
                + : IStaticMatrix<Matrix2x2>
                + this[Index, Index]
                + this[Range, Range]
                - : IMatrix<Matrix2x2>
                - override Equals(object?)
                - ToString(string?)
                - ToString(IFormatProvider)
                - Clone()
                - operator ==(Matrix3x3, Matrix3x3)
                - operator !=(Matrix3x3, Matrix3x3)
                = Turned `Matrix3x3` into a `class` (from a `struct`)
                = Added a nullability attribute to the return type of `Inverse()`
                = Added a nullability attribute to the return type of `operator -(Matrix3x3)`
                = Marked `Equals(Matrix3x3)` as virtual
                = Made `GetHashCode()` invoke the base method
                = Changed the parameter `other` in `Equals(Matrix3x3)` to a nullable equivalent and added a nullability check
                = Made `Cofactor()` use a preset rather than parameterless constructor
                = Made `operator /(Matrix3x3, Matrix3x3)` throw an exception if no inverse exists for matrix `b`
            * Matrix4x4
                + Made `Matrix4x4` into a record
                + : IStaticMatrix<Matrix2x2>
                + this[Index, Index]
                + this[Range, Range]
                - : IMatrix<Matrix2x2>
                - override Equals(object?)
                - ToString(string?)
                - ToString(IFormatProvider)
                - Clone()
                - operator ==(Matrix4x4, Matrix4x4)
                - operator !=(Matrix4x4, Matrix4x4)
                = Added a nullability attribute to the return type of `Inverse()`
                = Added a nullability attribute to the return type of `operator -(Matrix4x4)`
                = Turned `Matrix4x4` into a `class` (from a `struct`)
                = Marked `Equals(Matrix4x4)` as virtual
                = Made `GetHashCode()` invoke the base method
                = Changed the parameter `other` in `Equals(Matrix4x4)` to a nullable equivalent and added a nullability check
                = Made `Cofactor()` use a preset rather than parameterless constructor
                = Made `operator /(Matrix4x4, Matrix4x4)` throw an exception if no inverse exists for matrix `b`
            * Vector2d
                + Made `Vector2d` into a record
                + : IAbsolute<Vector2d>
                + : IAverage<Vector2d>
                + : IClampMagnitude<Vector2d>
                + : ICross<Vector2d, Vector3d>
                + : IDot<Vector2d, float>
                + : IFromTuple<Vector2d, (Angle angle, float mag)>
                + : ILerp<Vector2d, float>
                + : IMax<Vector2d>
                + : IMagnitude<float>
                + : IMedian<Vector2d>
                + : IMin<Vector2d>
                + : IPresets2D<Vector2d>
                + : ISplittable<Vector2d, (Angle[] rots, float[] mags)>
                + : ISubtract<Vector2d>
                + : ISum<Vector2d>
                + float Magnitude
                + operator Vector2d((Angle angle, float mag))
                - : ICloneable
                - Clone()
                - override Equals(object?)
                - override ToString()
                - ToString(string?, Angle.Type)
                - ToString(IFormatProvider, Angle.Type)
                - operator ==(Vector2d, Vector2d)
                - operator !=(Vector2d, Vector2d)
                = Made the tuple variable names lowercase in `SplitArray(Vector2d[])`
                = Made `GetHashCode()` invoke the base method
                = Made `ToString(Angle.Type)` resemble a record string
            * Vector3d
                + Made `Vector3d` into a record
                + : IAbsolute<Vector3d>
                + : IAverage<Vector3d>
                + : IClampMagnitude<Vector3d>
                + : ICross<Vector3d>
                + : IDot<Vector3d, float>
                + : IFromTuple<Vector3d, (Angle yaw, Angle pitch, float mag)>
                + : IIndexAll<Angle>
                + : IIndexRangeAll<Angle>
                + : ILerp<Vector3d, float>
                + : IMax<Vector3d>
                + : IMagnitude<float>
                + : IMedian<Vector3d>
                + : IMin<Vector3d>
                + : IPresets3D<Vector3d>
                + : ISplittable<Vector3d, (Angle[] yaws, Angle[] pitches, float[] mags)>
                + : ISubtract<Vector3d>
                + : ISum<Vector3d>
                + float Magnitude
                + this[Index]
                + this[Range]
                + operator Vector3d((Angle yaw, Angle pitch, float mag))
                - ICloneable
                - Clone()
                - override Equals(object?)
                - override ToString()
                - ToString(string?, Angle.Type)
                - ToString(IFormatProvider, Angle.Type)
                - operator ==(Vector3d, Vector3d)
                - operator !=(Vector3d, Vector3d)
                = Renamed the tuple variable names in `SplitArray(Vector3d[])`
                = Made `GetHashCode()` invoke the base method
                = Made `ToString(Angle.Type)` resemble a record string
            = Moved `IMatrix<T>` to `Nerd_STF.Mathematics.Abstract`
                + : IAbsolute<T>
                + : ICeiling<T>
                + : IClamp<T>
                + : IDivide<T>
                + : IFloor<T>
                + : ILerp<T, float>
                + : IProduct<T>
                + : IRound<T>
                - : ICloneable
                - : IEnumerable
                = Added a nullability attribute to the return type of `Inverse()`
        * Geometry
            * Box2D
                + Made `Box2D` into a record
                + : IAbsolute<Box2D>
                + : IAverage<Box2D>
                + : ICeiling<Box2D>
                + : IClamp<Box2D>
                + : IFloor<Box2D>
                + : ILerp<Box2D, float>
                + : IMedian<Box2D>
                + : IRound<Box2D>
                + : IShape2D<float>
                + : ISplittable<Box2D, (Vert[] centers, Float2[] sizes)>
                + Round(Box2D)
                + PrintMembers(StringBuilder)
                - : ICloneable
                - Max(Box2D[])
                - Min(Box2D[])
                - Clone()
                - override Equals(object?)
                - override ToString()
                - ToString(string?)
                - ToString(IFormatProvider)
                - operator ==(Box2D, Box2D)
                - operator !=(Box2D, Box2D)
                = Turned `Box2D` into `class` (from a `struct`)
                = Marked `Equals(Box2D)` as virtual
                = Made `GetHashCode()` invoke the base method
                = Changed the parameter `other` in `Equals(Box2D)` to a nullable equivalent and added a nullability check
            * Box3D
                + Made `Box3D` into a record
                + : IAbsolute<Box3D>
                + : IAverage<Box3D>
                + : ICeiling<Box3D>
                + : IClamp<Box3D>
                + : IFloor<Box3D>
                + : ILerp<Box3D, float>
                + : IMedian<Box3D>
                + : IRound<Box3D>
                + : IShape3D<float>
                + : ISplittable<Box3D, (Vert[] centers, Float3[] sizes)>
                + Round(Box3D)
                + PrintMembers(StringBuilder)
                - : ICloneable
                - Clone()
                - override Equals(object?)
                - override ToString()
                - ToString(string?)
                - ToString(IFormatProvider)
                - operator ==(Box3D, Box3D)
                - operator !=(Box3D, Box3D)
                = Fixed an ambiguity in `Ceiling(Box3D)`
                = Fixed an ambiguity in `Floor(Box3D)`
                = Turned `Box3D` into `class` (from a `struct`)
                = Marked `Equals(Box3D)` as virtual
                = Made `GetHashCode()` invoke the base method
                = Changed the parameter `other` in `Equals(Box3D)` to a nullable equivalent and added a nullability check
            * Line
                + Made `Line` into a record
                + : IAbsolute<Line>
                + : IAverage<Line>
                + : ICeiling<Line>
                + : IClamp<Line>
                + : IFloor<Line>
                + : IFromTuple<Line, (Vert start, Vert end)>
                + : IIndexAll<Vert>
                + : IIndexRangeAll<Vert>
                + : ILerp<Line, float>
                + : IMedian<Line>
                + : IPresets3D<Line>
                + : IRound<Line>
                + : ISplittable<Line, (Vert[] starts, Vert[] ends)>
                + Round(Line)
                + PrintMembers(StringBuilder)
                + operator Line((Vert start, Vert end))
                - : ICloneable
                - Clone()
                - Max(Line[])
                - Min(Line[])
                - override Equals(object?)
                - ToString(string?)
                - ToString(IFormatProvider)
                - operator ==(Line, Line)
                - operator !=(Line, Line)
                = Turned `Line` into `class` (from a `struct`)
                = Made `GetHashCode()` invoke the base method
                = Marked `CompareTo(Line)` as deprecated, as it's a bit confusing
                = Marked `operator >(Line, Line)` as deprecated, as it's a bit confusing
                = Marked `operator <(Line, Line)` as deprecated, as it's a bit confusing
                = Marked `operator >=(Line, Line)` as deprecated, as it's a bit confusing
                = Marked `operator <=(Line, Line)` as deprecated, as it's a bit confusing
                = Changed the parameter `other` in `Equals(Line)` to a nullable equivalent and added a nullability check
                = Changed the parameter `other` in `CompareTo(Line)` to a nullable equivalent and added a nullability check
            * Polygon
                * Triangulate
                    = Replaced all `Exception`s thrown with `Nerd_STFException`s
                - Max(Polygon[])
                - Min(Polygon[])
                - ToString(string?)
                - ToString(IFormatProvider)
                = Changed the deprecation removal notice from version 2.4.0 to 2.5.0
            * Quadrilateral
                + Made `Quadrilateral` a record
                + : IAbsolute<Quadrilateral>
                + : IAverage<Quadrilateral>
                + : ICeiling<Quadrilateral>
                + : IClamp<Quadrilateral>
                + : IFloor<Quadrilateral>
                + : IFromTuple<Quadrilateral, (Vert a, Vert b, Vert c, Vert d)>
                + : IIndexAll<Vert>
                + : IIndexRangeAll<Vert>
                + : ILerp<Quadrilateral, float>
                + : IRound<Quadrilateral>
                + : IShape2D<float>
                + Round(Quadrilateral)
                + PrintMembers(StringBuilder)
                + operator Quadrilateral((Vert a, Vert b, Vert c, Vert d))
                - : ICloneable
                - Clone()
                - override Equals(object?)
                - override ToString()
                - ToString(string?)
                - ToString(IFormatProvider)
                - operator ==(Quadrilateral, Quadrilateral)
                - operator !=(Quadrilateral, Quadrilateral)
                = Turned `Quadrilateral` into a `class` (from a `struct`)
                = Made `GetHashCode()` invoke the base method
                = Marked `Equals(Quadrilateral)` as virtual
                = Changed the parameter `other` in `Equals(Quadrilateral)` to a nullable equivalent and added a nullability check
            * Sphere
                + Made `Sphere` a record
                + : IAverage<Sphere>
                + : ICeiling<Sphere>
                + : IClamp<Sphere>
                + : IFloor<Sphere>
                + : IFromTuple<Sphere, (Vert center, float radius)>
                + : ILerp<Sphere, float>
                + : IMax<Sphere>
                + : IMedian<Sphere>
                + : IMin<Sphere>
                + : IRound<Sphere>
                + : ISplittable<Sphere, (Vert[] centers, float[] radii)>
                + Round(Sphere)
                + PrintMembers(StringBuilder)
                + operator Sphere((Vert center, float radius))
                - : ICloneable
                - Clone()
                - override Equals(object?)
                - override ToString()
                - ToString(string?)
                - ToString(IFormatProvider)
                - operator ==(Sphere, Sphere)
                - operator !=(Sphere, Sphere)
                = Made `GetHashCode()` invoke the base method
                = Marked `Equals(float)` as deprecated. It will be removed in 2.5.0
                = Marked `CompareTo(float)` as deprecated. It will be removed in 2.5.0
                = Marked `operator ==(Sphere, float)` as deprecated. It will be removed in 2.5.0
                = Marked `operator !=(Sphere, float)` as deprecated. It will be removed in 2.5.0
                = Marked `operator >(Sphere, Sphere)` as deprecated. It will be removed in 2.5.0
                = Marked `operator <(Sphere, Sphere)` as deprecated. It will be removed in 2.5.0
                = Marked `operator >(Sphere, float)` as deprecated. It will be removed in 2.5.0
                = Marked `operator <(Sphere, float)` as deprecated. It will be removed in 2.5.0
                = Marked `operator >=(Sphere, Sphere)` as deprecated. It will be removed in 2.5.0
                = Marked `operator <=(Sphere, Sphere)` as deprecated. It will be removed in 2.5.0
                = Marked `operator >=(Sphere, float)` as deprecated. It will be removed in 2.5.0
                = Marked `operator <=(Sphere, float)` as deprecated. It will be removed in 2.5.0
                = Marked `Equals(Sphere)` as virtual
                = Changed the parameter `other` in `Equals(Sphere)` to a nullable equivalent and added a nullability check
                = Changed the parameter `other` in `CompareTo(Sphere)` to a nullable equivalent and added a nullability check
                = Turned `Sphere` into a `class` (from a `struct`)
            * Triangle
                + Made `Triangle` a record
                + : IAbsolute<Triangle>
                + : IAverage<Triangle>
                + : ICeiling<Triangle>
                + : IClamp<Triangle>
                + : IFloor<Triangle>
                + : IFromTuple<Triangle, (Vert a, Vert b, Vert c)>
                + : IIndexAll<Vert>
                + : IIndexRangeAll<Vert>
                + : ILerp<Triangle, float>
                + : IRound<Triangle>
                + : IShape2D<float>
                + Round(Triangle)
                + PrintMembers(StringBuilder)
                + operator Triangle((Vert a, Vert b, Vert c))
                - : ICloneable
                - Clone()
                - override Equals(object?)
                - override ToString()
                - ToString(string?)
                - ToString(IFormatProvider)
                - operator ==(Triangle, Triangle)
                - operator !=(Triangle, Triangle)
                = Turned `Triangle` into a `class` (from a `struct`)
                = Made `GetHashCode()` invoke the base method
                = Marked `Equals(Triangle)` as virtual
                = Changed the parameter `other` in `Equals(Triangle)` to a nullable equivalent and added a nullability check
            * Vert
                + Round(Vert)
                - ToString(string?)
                - ToString(IFormatProvider)
            = Moved `ISubdividable<T>` to `Nerd_STF.Mathematics.Abstract` and renamed it to `ISubdivide<T>`
            = Moved `ITriangulatable<T>` to `Nerd_STF.Mathematics.Abstract` and renamed it to `ITriangulate<T>`
        * NumberSystems
            * Complex
                + Marked `Complex` as a record
                    + Added parameters `float`, `float`
                + : IAbsolute<Complex>
                + : IAverage<Complex>
                + : ICeiling<Complex>
                + : IClamp<Complex>
                + : IClampMagnitude<Complex, float>
                + : IDivide<Complex>
                + : IDot<Complex, float>
                + : IFloor<Complex>
                + : IIndexAll<float>
                + : IIndexRangeAll<float>
                + : ILerp<Complex, float>
                + : IMax<Complex>
                + : IMedian<Complex>
                + : IMin<Complex>
                + : IPresets2D<Complex>
                + : IProduct<Complex>
                + : IRound<Complex>
                + : ISplittable<Complex, (float[] Us, float[] Is)>
                + : ISum<Complex>
                + this[Index]
                + this[Range]
                + PrintMembers(StringBuilder)
                + operator Complex((float, float))
                - : ICloneable<Complex>
                - Complex(float, float)
                - Clone()
                - override Equals(object?)
                - override ToString()
                - ToString(string?)
                - ToString(IFormatProvider)
                - operator ==(Complex, Complex)
                - operator !=(Complex, Complex)
                = Added an assignment for `u`
                = Added an assignment for `i`
                = Marked `operator >(Complex, Complex)` as deprecated, as it's a bit confusing
                = Marked `operator <(Complex, Complex)` as deprecated, as it's a bit confusing
                = Marked `operator >=(Complex, Complex)` as deprecated, as it's a bit confusing
                = Marked `operator <=(Complex, Complex)` as deprecated, as it's a bit confusing
            * Quaternion
                + Marked `Quaternion` as a record
                    + Added parameters `float`, `float`, `float`, `float`
                + : IAbsolute<Quaternion>
                + : IAverage<Quaternion>
                + : ICeiling<Quaternion>
                + : IClamp<Quaternion>
                + : IClampMagnitude<Quaternion, float>
                + : IDivide<Quaternion>
                + : IDot<Quaternion, float>
                + : IFloor<Quaternion>
                + : IIndexAll<float>
                + : IIndexRangeAll<float>
                + : ILerp<Quaternion, float>
                + : IMax<Quaternion>
                + : IMedian<Quaternion>
                + : IMin<Quaternion>
                + : IPresets4D<Quaternion>
                + : IProduct<Quaternion>
                + : IRound<Quaternion>
                + : ISplittable<Quaternion, (float[] Us, float[] Is, float[] Js, float[] Ks)>
                + : ISum<Quaternion>
                + HighW
                + LowW
                + this[Index]
                + this[Range]
                + PrintMembers(StringBuilder)
                + operator Quaternion((float, float, float, float))
                - : ICloneable
                - Quaternion(float, float, float, float)
                - Clone()
                - override Equals(object?)
                - override ToString()
                - ToString(string?)
                - ToString(IFormatProvider)
                - operator ==(Quaternion, Quaternion)
                - operator !=(Quaternion, Quaternion)
                = Added an assignment for `u`
                = Added an assignment for `i`
                = Added an assignment for `j`
                = Added an assignment for `k`
                = Fixed a mistake in `operator -(Quaternion)` that would just return the clone of the current instance rather than the proper negative.
                = Made `GetHashCode()` invoke the base method
                = Marked `operator >(Quaternion, Quaternion)` as deprecated, as it's a bit confusing
                = Marked `operator <(Quaternion, Quaternion)` as deprecated, as it's a bit confusing
                = Marked `operator >=(Quaternion, Quaternion)` as deprecated, as it's a bit confusing
                = Marked `operator <=(Quaternion, Quaternion)` as deprecated, as it's a bit confusing
                = Marked `Near` as deprecated, as it's replaced with `LowW`.
                = Marked `Far` as deprecated, as it's replaced with `HighW`.
        * Samples
            * Constants
                = Fixed a typo and renamed `TwelthRoot2` to `TwelfthRoot2`
                = Made `EulerMascheroniConstant` reference `EulerConstant`, since they are the same
                = Made `LiebSquareIceConstant` more simplified
                = Renamed `UniversalHyperbolicConstant` to `UniversalParabolicConstant` (oops)
                = Renamed `RegularPaperfoldingSequence` to `RegularPaperfoldingConstant`
                = Simplified `SecondHermiteConstant`
        * Angle
            + : IAbsolute<Angle>
            + : IAverage<Angle>
            + : IClamp<Angle>
            + : ILerp<Angle, float>
            + : IMax<Angle>
            + : IMedian<Angle>
            + : IMin<Angle>
            + : IPresets2D<Angle>
            + operator Angle((float, Type))
            - ToString(string?, Type)
            - ToString(IFormatProvider, Type)
            = Improved the `SplitArray(Type, Angle[])` to use another function for conversion.
        * Float2
            + Marked `Float2` as a record
            + : IAbsolute<Float2>
            + : IAverage<Float2>
            + : ICeiling<Float2, Int2>
            + : IClamp<Float2>
            + : IClampMagnitude<Float2, float>
            + : ICross<Float2, Float3>
            + : IDivide<Float2>
            + : IDot<Float2, float>
            + : IFloor<Float2, Int2>
            + : IFromTuple<Float2, (float x, float y)>
            + : IIndexAll<float>
            + : IIndexRangeAll<float>
            + : ILerp<Float2, float>
            + : IMax<Float2>
            + : IMedian<Float2>
            + : IMin<Float2>
            + : IPresets2D<Float2>
            + : IProduct<Float2>
            + : IRound<Float2, Int2>
            + : ISplittable<Float2, (float[] Xs, float[] Ys)>
            + : ISubtract<Float2>
            + : ISum<Float2>
            + this[Index]
            + this[Range]
            + PrintMembers(StringBuilder)
            + operator Float2((float, float))
            - : ICloneable<Float2>
            - Clone()
            - override Equals(object?)
            - override ToString()
            - ToString(string?)
            - ToString(IFormatProvider)
            - operator ==(Float2, Float2)
            - operator !=(Float2, Float2)
            = Made `Ceiling(Float2)` return an `Int2` instead of a `Float2`
            = Made `Floor(Float2)` return an `Int2` instead of a `Float2`
            = Made `Round(Float2)` return an `Int2` instead of a `Float2`
            = Made `Max(Float2[])` compare magnitudes directly
            = Made `Min(Float2[])` compare magnitudes directly
            = Marked `CompareTo(Float2)` as deprecated, as it's a bit confusing
            = Marked `operator >(Float2, Float2)` as deprecated, as it's a bit confusing
            = Marked `operator <(Float2, Float2)` as deprecated, as it's a bit confusing
            = Marked `operator >=(Float2, Float2)` as deprecated, as it's a bit confusing
            = Marked `operator <=(Float2, Float2)` as deprecated, as it's a bit confusing
        * Float3
            + Marked `Float3` as a record
            + : IAbsolute<Float3>
            + : IAverage<Float3>
            + : ICeiling<Float3, Int3>
            + : IClamp<Float3>
            + : IClampMagnitude<Float3, float>
            + : ICross<Float3>
            + : IDivide<Float3>
            + : IDot<Float3, float>
            + : IFloor<Float3, Int3>
            + : IFromTuple<Float3, (float x, float y, float z)>
            + : IIndexAll<float>
            + : IIndexRangeAll<float>
            + : ILerp<Float3, float>
            + : IMathOperators<Float3>
            + : IMax<Float3>
            + : IMedian<Float3>
            + : IMin<Float3>
            + : IPresets3D<Float3>
            + : IProduct<Float3>
            + : IRound<Float3, Int3>
            + : ISplittable<Float3, (float[] Xs, float[] Ys, float[] Zs)>
            + : ISubtract<Float3>
            + : ISum<Float3>
            + this[Index]
            + this[Range]
            + PrintMembers(StringBuilder)
            + operator Float3((float, float, float))
            - : ICloneable
            - Float3(float, float, float)
            - Clone()
            - override Equals(object?)
            - override ToString()
            - ToString(string?)
            - ToString(IFormatProvider)
            - operator ==(Float3, Float3)
            - operator !=(Float3, Float3)
            = Made `Ceiling(Float3)` return an `Int3` instead of a `Float3`
            = Made `Floor(Float3)` return an `Int3` instead of a `Float3`
            = Made `Round(Float3)` return an `Int3` instead of a `Float3`
            = Made `Max(Float3[])` compare magnitudes directly
            = Made `Min(Float3[])` compare magnitudes directly
            = Made `GetHashCode()` invoke the base method
            = Marked `CompareTo(Float3)` as deprecated, as it's a bit confusing
            = Marked `operator >(Float3, Float3)` as deprecated, as it's a bit confusing
            = Marked `operator <(Float3, Float3)` as deprecated, as it's a bit confusing
            = Marked `operator >=(Float3, Float3)` as deprecated, as it's a bit confusing
            = Marked `operator <=(Float3, Float3)` as deprecated, as it's a bit confusing
        * Float4
            + Marked `Float4` as a record
            + : IAbsolute<Float4>
            + : IAverage<Float4>
            + : ICeiling<Float4, Int4>
            + : IClamp<Float4>
            + : IClampMagnitude<Float4, float>
            + : IDivide<Float4>
            + : IDot<Float4, float>
            + : IFloor<Float4, Int4>
            + : IFromTuple<Float4, (float x, float y, float z, float w)>
            + : IIndexAll<float>
            + : IIndexRangeAll<float>
            + : ILerp<Float4, float>
            + : IMathOperators<Float4>
            + : IMax<Float4>
            + : IMedian<Float4>
            + : IMin<Float4>
            + : IPresets4D<Float4>
            + : IProduct<Float4>
            + : IRound<Float4, Int4>
            + : ISplittable<Float4, (float[] Xs, float[] Ys, float[] Zs, float[] Ws)>
            + : ISubtract<Float4>
            + : ISum<Float4>
            + this[Index]
            + this[Range]
            + PrintMembers(StringBuilder)
            + operator Float4((float, float, float, float))
            - : ICloneable
            - Float4(float, float, float, float)
            - Clone()
            - override Equals(object?)
            - override ToString()
            - ToString(string?)
            - ToString(IFormatProvider)
            - operator ==(Float4, Float4)
            - operator !=(Float4, Float4)
            = Made `Ceiling(Float4)` return an `Int4` instead of a `Float4`
            = Made `Floor(Float4)` return an `Int4` instead of a `Float4`
            = Made `Round(Float4)` return an `Int4` instead of a `Float4`
            = Made `Max(Float4[])` compare magnitudes directly
            = Made `Min(Float4[])` compare magnitudes directly
            = Made `GetHashCode()` invoke the base method
            = Marked `CompareTo(Float4)` as deprecated, as it's a bit confusing
            = Marked `operator >(Float4, Float4)` as deprecated, as it's a bit confusing
            = Marked `operator <(Float4, Float4)` as deprecated, as it's a bit confusing
            = Marked `operator >=(Float4, Float4)` as deprecated, as it's a bit confusing
            = Marked `operator <=(Float4, Float4)` as deprecated, as it's a bit confusing
        * Int2
            + Marked `Int2` as a record
            + : IAbsolute<Int2>
            + : IAverage<Int2>
            + : IClamp<Int2>
            + : IClampMagnitude<Int2, int>
            + : ICross<Int2, Int3>
            + : IDivide<Int2>
            + : IDot<Int2, int>
            + : IFromTuple<Int2, (int x, int y)>
            + : IIndexAll<int>
            + : IIndexRangeAll<int>
            + : ILerp<Int2, float>
            + : IMathOperators<Int2>
            + : IMax<Int2>
            + : IMedian<Int2>
            + : IMin<Int2>
            + : IPresets2D<Int2>
            + : IProduct<Int2>
            + : ISplittable<Int2, (int[] Xs, int[] Ys)>
            + : ISubtract<Int2>
            + : ISum<Int2>
            + this[Index]
            + this[Range]
            + PrintMembers(StringBuilder)
            + operator Int2((int, int))
            - : ICloneable
            - Clone()
            - override Equals(object?)
            - override ToString()
            - ToString(string?)
            - ToString(IFormatProvider)
            - operator ==(Int2, Int2)
            - operator !=(Int2, Int2)
            = Made `Max(Int2[])` compare magnitudes directly
            = Made `Min(Int2[])` compare magnitudes directly
            = Made `GetHashCode()` invoke the base method
            = Marked `CompareTo(Int2)` as deprecated, as it's a bit confusing
            = Marked `operator >(Int2, Int2)` as deprecated, as it's a bit confusing
            = Marked `operator <(Int2, Int2)` as deprecated, as it's a bit confusing
            = Marked `operator >=(Int2, Int2)` as deprecated, as it's a bit confusing
            = Marked `operator <=(Int2, Int2)` as deprecated, as it's a bit confusing
        * Int3
            + Marked `Int3` as a record
            + : IAbsolute<Int3>
            + : IAverage<Int3>
            + : IClamp<Int3>
            + : IClampMagnitude<Int3, int>,
            + : ICross<Int3>
            + : IDivide<Int3>
            + : IDot<Int3, int>
            + : IFromTuple<Int3, (int x, int y, int z)>
            + : IIndexAll<int>
            + : IIndexRangeAll<int>
            + : ILerp<Int3, float>
            + : IMathOperators<Int3>
            + : IMax<Int3>
            + : IMedian<Int3>
            + : IMin<Int3>
            + : IProduct<Int3>
            + : ISplittable<Int3, (int[] Xs, int[] Ys, int[] Zs)>
            + : ISubtract<Int3>
            + : ISum<Int3>
            + this[Index]
            + this[Range]
            + PrintMembers(StringBuilder)
            + operator Int3((int, int, int))
            - : ICloneable()
            - Int3(int, int, int)
            - Clone()
            - override Equals(object?)
            - override ToString()
            - ToString(string?)
            - ToString(IFormatProvider)
            - operator ==(Int3, Int3)
            - operator !=(Int3, Int3)
            = Made `Max(Int3[])` compare magnitudes directly
            = Made `Min(Int3[])` compare magnitudes directly
            = Made `GetHashCode()` invoke the base method
            = Marked `CompareTo(Int3)` as deprecated, as it's a bit confusing
            = Marked `operator >(Int3, Int3)` as deprecated, as it's a bit confusing
            = Marked `operator <(Int3, Int3)` as deprecated, as it's a bit confusing
            = Marked `operator >=(Int3, Int3)` as deprecated, as it's a bit confusing
            = Marked `operator <=(Int3, Int3)` as deprecated, as it's a bit confusing
        * Int4
            + Marked `Int4` as a record
            + : IAbsolute<Int4>
            + : IAverage<Int4>
            + : IClamp<Int4>
            + : IClampMagnitude<Int4, int>
            + : IDivide<Int4>
            + : IDot<Int4, int>
            + : IFromTuple<Int4, (int x, int y, int z, int w)>
            + : IIndexAll<int>
            + : IIndexRangeAll<int>
            + : ILerp<Int4, float>
            + : IMathOperators<Int4>
            + : IMax<Int4>
            + : IMedian<Int4>
            + : IMin<Int4>
            + : IProduct<Int4>
            + : ISplittable<Int4, (int[] Xs, int[] Ys, int[] Zs, int[] Ws)>
            + : ISubtract<Int4>
            + : ISum<Int4>
            + this[Index]
            + this[Range]
            + PrintMembers(StringBuilder)
            + operator Int4((int, int, int, int))
            - : ICloneable
            - Int4(int, int, int, int)
            - Clone()
            - override Equals(object?)
            - override ToString()
            - ToString(string?)
            - ToString(IFormatProvider)
            - operator ==(Int4, Int4)
            - operator !=(Int4, Int4)
            = Marked `Deep` as deprecated
            = Made `Max(Int4[])` compare magnitudes directly
            = Made `Min(Int4[])` compare magnitudes directly
            = Made `GetHashCode()` invoke the base method
            = Marked `CompareTo(Int4)` as deprecated, as it's a bit confusing
            = Marked `operator >(Int4, Int4)` as deprecated, as it's a bit confusing
            = Marked `operator <(Int4, Int4)` as deprecated, as it's a bit confusing
            = Marked `operator >=(Int4, Int4)` as deprecated, as it's a bit confusing
            = Marked `operator <=(Int4, Int4)` as deprecated, as it's a bit confusing
        * Mathf
            = Forced `Max<T>(T[])` to not return a nullable object
            = Forced `Min<T>(T[])` to not return a nullable object
            = Renamed a parameter `value` in `Mathf.Lerp(int, int, float, bool)` to `t`
            = Replaced a `ContainsKey(float)` call with a `TryGetValue(float, out float)` call in `MakeEquation(Dictionary<float, float>)`
            = Removed a useless call to `Absolute(int)` in `PowerMod(int, int, int)`
            = Simplified a list initialization in `Factors(int)`
    * Miscellaneous
        + AssemblyConfig
        * GlobalUsings
            + global using Nerd_STF.Graphics.Abstract
            + global using Nerd_STF.Mathematics.Abstract
            + global using System.Text
            + global using System.Runtime.Serialization
    + Nerd_STF
    = Marked `Foreach(object)` as deprecated. Why would you even use this?
    = Marked `Foreach<T>(T)` as deprecated. Why would you even use this?
    = Moved `IClosest<T>` to `Nerd_STF.Mathematics.Abstract` and renamed it to `IClosestTo<T>`
    = Moved `IContainer<T>` to `Nerd_STF.Mathematics.Abstract` and renamed it to `IContains<T>`
```
