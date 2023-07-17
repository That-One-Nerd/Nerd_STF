# Nerd_STF v2.4.1

Hey everyone! This is one of the larger small updates, and I'm pretty proud of what I got done in a week.

Along with adding setters to parts like `Float3.XY` and fixing a few bugs, almost all improvements in this update are related to matricies. First of all, I've added a bunch of new items to the `IMatrix` interface. Now, any deriving matrix has more requirements that fit the regular `Matrix` type. I don't know why one would use the `IMatrix` interface rather than a specific matrix type, but now the options are more sophisticated.

I've added some new stuff to all the matrix types, including row operations. You can now scale a row, add a row to another, and swap two rows. If I become aware of any more commonly-used row operations, I'll add then in a `2.4.2` update. But I think I've got all the good ones. There is also a mutable version of each operation which, rather than returning a new matrix with changes made, instead applies the changes to itself.

Did you know I made two seperate blunders in the `Cofactor()` method? For the `Matrix2x2` version of the `Cofactor()` method, I had the diagonal elements swapped. Whoops. For the `Matrix` version of the `Cofactor()` method, matricies with even column count would break because of the alternating sign pattern I was using. Now, as far as I know, that bug is fixed.

The last thing I did was add the ability to turn a matrix into its equivalent row-echelon form. This is applicable only to the `Matrix` type (the dynamic one), and works with some levels of success. It's a little weird and tends to give results with lots of negative zeroes, but overall it's fine, I think. As far as I know there aren't any obvious bugs. We'll see though.

Anyway, that's everything in this update. Again, pretty small, but meaningful nonetheless. Unless I haven't screwed anything up, the next update I work on will be `2.5`, so I'll see you then!

Here's the full changelog:
```
* Nerd_STF
    * Mathematics
        * Abstract
            * IMatrix
                + AddRow(int, int, float)
                + AddRowMutable(int, int, float)
                + Cofactor()
                + GetColumn(int)
                + GetRow(int)
                + ScaleRow(int, float)
                + ScaleRowMutable(int, float)
                + SetColumn(int, float[])
                + SetRow(int, float[])
                + Size
                + SwapRows(int, int)
                + SwapRowsMutable(int, int)
                + this[int, int]
                + this[Index, Index]
        * Algebra
            * Matrix
                + AddRow(int, int, float)
                + AddRowMutable(int, int, float)
                + ScaleRow(int, float)
                + ScaleRowMutable(int, float)
                + SwapRows(int, int)
                + SwapRowsMutable(int, int)
                = Fixed a blunder in `SignGrid(Int2)` with signs being incorrectly placed on matrixes with even column count.
            * Matrix2x2
                + AddRow(int, int, float)
                + AddRowMutable(int, int, float)
                + GetColumn(int)
                + GetRow(int)
                + ScaleRow(int, float)
                + ScaleRowMutable(int, float)
                + SetColumn(int, float[])
                + SetRow(int, float[])
                + Size
                + SwapRows(int, int)
                + SwapRowsMutable(int, int)
                = Fixed a blunder in `Cofactor()` with the position of elements.
            * Matrix3x3
                + AddRow(int, int, float)
                + AddRowMutable(int, int, float)
                + GetColumn(int)
                + GetRow(int)
                + ScaleRow(int, float)
                + ScaleRowMutable(int, float)
                + SetColumn(int, float[])
                + SetRow(int, float[])
                + Size
                + SwapRows(int, int)
                + SwapRowsMutable(int, int)
            * Matrix4x4
                + AddRow(int, int, float)
                + AddRowMutable(int, int, float)
                + GetColumn(int)
                + GetRow(int)
                + ScaleRow(int, float)
                + ScaleRowMutable(int, float)
                + SetColumn(int, float[])
                + SetRow(int, float[])
                + Size
                + SwapRows(int, int)
                + SwapRowsMutable(int, int)
        * NumberSystems
            * Complex
                + operator Complex(SystemComplex)
                + operator SystemComplex(Complex)
            * Quaternion
                + operator Quaternion(SystemQuaternion)
                + operator SystemQuaternion(Quaternion)
        * Float3
            = Added a setter to `XY`
            = Added a setter to `XZ`
            = Added a setter to `YZ`
        * Float4
            = Added a setter to `XW`
            = Added a setter to `XY`
            = Added a setter to `XZ`
            = Added a setter to `YW`
            = Added a setter to `YZ`
            = Added a setter to `ZW`
            = Added a setter to `XYW`
            = Added a setter to `XYZ`
            = Added a setter to `XZW`
            = Added a setter to `YZW`
        * Int3
            = Added a setter to `XY`
            = Added a setter to `XZ`
            = Added a setter to `YZ`
        * Int4
            = Added a setter to `XW`
            = Added a setter to `XY`
            = Added a setter to `XZ`
            = Added a setter to `YW`
            = Added a setter to `YZ`
            = Added a setter to `ZW`
            = Added a setter to `XYW`
            = Added a setter to `XYZ`
            = Added a setter to `XZW`
            = Added a setter to `YZW`
```
