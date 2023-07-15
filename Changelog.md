# Nerd_STF v2.4.1

TODO

added row operations, fixed cofactor bugs, added setters and more stuff to imatrix

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
