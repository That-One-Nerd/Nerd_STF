# Nerd_STF v2.5.0

TODO:
- added better inv sqrt

Here's the full changelog:
```
* Nerd_STF
    * Helpers
        * UnsafeHelper
            + Q_rsqrt
    * Mathematics
        * Abstract
            + IPolygon<T>h
            * ITriangulate
                - TriangulateAll(ITriangulate[])
                = Marked the method `TriangulateAll<T>(T[])` as virtual
        * Geometry
            - Box2d (REMEMBER: name change)
            - Box3d (REMEMBER: name change)
            - Polygon
            - Quadrilateral
            - Sphere
            - Vert
            = Rewrote all of `Triangle`
                TODO: Compare me to the original triangle and note changes.
            = Rewrote all of `Line`
                + : IFromTuple<Line, (Float3 a, Float3 b)>
                + : IIndexAll<Float3>
                + : IIndexRangeAll<Float3>
                + : ISplittable<Line, (Float3[] As, Float3[] Bs)>
                + IWithinRange<Float3, float>
                + Slope
                + Line()
                + ToFloatArrayAll(params Line[])
                + WithinRange(Float3, float)
                + WithinRange(Float3, float, float)
                + operator +(Line, Float3)
                + operator -(Line, Float3)
                + operator *(Line, Float3)
                + operator /(Line, Float3)
                + operator ==(Line, Line)
                + operator !=(Line, Line)
                + implicit operator Line((Float3 a, Float3 b))
                - : IAbsolute<Line>
                - : ICeiling<Line>
                - : IClamp<Line>
                - : IComparable<Line>
                - : IFloor<Line>
                - : IFromTuple<Line, (Vert start, Vert end)>
                - : IIndexAll<Vert>
                - : IIndexRangeAll<Vert>
                - : IRound<Line>
                - : ISplittable<Line, (Vert[] starts, Vert[] ends)>
                - Line(Fill<Vert>)
                - Absolute(Line)
                - Ceiling(Line)
                - Clamp(Line, Line, Line)
                - Floor(Line)
                - Round(Line)
                - CompareTo(Line?)
                - ToFloatList()
                - PrintMembers(StringBuilder)
                - operator +(Line, Line)
                - operator +(Line, Vert)
                - operator -(Line)
                - operator -(Line, Line)
                - operator -(Line, Vert)
                - operator *(Line, Line)
                - operator *(Line, Vert)
                - operator /(Line, Line)
                - operator /(Line, Vert)
                - operator >(Line, Line)
                - operator <(Line, Line)
                - operator >=(Line, Line)
                - operator <=(Line, Line)
                - implicit operator Line(Fill<Vert>)
                - implicit operator Line((Vert start, Vert end))
                = Changed the parameter type of the parameter `vert` in `Contains(Vert)` from `Vert` to `Float3` and renamed it to `point`
                = Changed the parameter type of the parameter `vert` in `ClosestTo(Vert)` from `Vert` to `Float3` and renamed it to `point`
                = Changed the parameter type of the parameter `vert` in `ClosestTo(Vert, float)` from `Vert` to `Float3` and renamed it to `point`
                = Changed the return type of `Midpoint` from `Vert` to `Float3`
                = Changed the return type of `this[int]` from `Vert` to `Float3`
                = Changed the return type of `this[Index]` from `Vert` to `Float3`
                = Changed the return type of `this[Range]` from `Vert` to `Float3`
                = Changed the return type of `SplitArray(params Line[])` from `(Vert[] starts, Vert[] ends)` to `(Float3[] As, Float3[] Bs)`
                = Changed the return type of `ToArray()` from `Vert[]` to `Float3[]`
                = Changed the return type of `ToFill()` from `Fill<Vert>` to `Fill<Float3>`
                = Changed the return type of `ToList()` from `List<Vert>` to `List<Float3>`
                = Changed the return type of `ClosestTo(Vert)` from `Vert` to `Float3`
                = Changed the return type of `ClosestTo(Vert, float)` from `Vert` to `Float3`
                = Changed the return type of `GetEnumerator()` from `IEnumerator<Vert>` to `IEnumerator<Float3>`
                = Changed the variable type of `a` from `Vert` to `Float3`
                = Changed the variable type of `b` from `Vert` to `Float3`
                = Expanded `Line(float, float, float, float)` so as to reduce confusion
                = Expanded `Line(float, float, float, float, float, float)` so as to reduce confusion
                = Improved the `Contains(Vert)` function
                = Improved the `ClosestTo(Vert, float)` function
                = Made the `Subdivide(int)` function split into n+1 lines rather than 2^(n-1) lines
                = Renamed a parameter of `Average(params Line[])` from `vals` to `lines`
                = Renamed a parameter of `Median(params Line[])` from `vals` to `lines`
                = Renamed a parameter of `SplitArray(params Line[])` from `vals` to `lines`
                = Replaced references to `Vert` with references to `Float3` in `Back`
                = Replaced references to `Vert` with references to `Float3` in `Down`
                = Replaced references to `Vert` with references to `Float3` in `Forward`
                = Replaced references to `Vert` with references to `Float3` in `Left`
                = Replaced references to `Vert` with references to `Float3` in `Right`
                = Replaced references to `Vert` with references to `Float3` in `Up`
                = Replaced references to `Vert` with references to `Float3` in `One`
                = Replaced references to `Vert` with references to `Float3` in `Zero`
                = Replaced references to `Vert` with references to `Float3` in `Line(Vert, Vert)`
                = Replaced references to `Vert` with references to `Float3` in `Average(params Line[])`
                = Replaced references to `Vert` with references to `Float3` in `Lerp(Line, Line, float, bool)`
                = Replaced references to `Vert` with references to `Float3` in `Median(params Line[])`
                = Replaced references to `Vert` with references to `Float3` in `SplitArray(params Line[])`
                = Replaced references to `Vert` with references to `Float3` in `ClosestTo(Vert, float)`
                = Replaced references to `Vert` with references to `Float3` in `Subdivide()`
                = Replaced references to `Vert` with references to `Float3` in `Subdivide(int)`
                = Simplified `Equals(Line?)` and removed the `virtual` keyword
        * NumberSystems
            * Complex
                = Replaced all references to `Vert` with references to `Float3`
            * Quaternion
                = Replaced all references to `Vert` with references to `Float3`
        * Angle
            + FromVerts(Float3, Float3, Float3)
            + GetCoterminalAngles()
            + GetCoterminalAngles(Angle, Angle)
        * Float2
            + InverseMagnitude
            = Replaced all references to `Vert` with references to `Float3`
        * Float3
            + InverseMagnitude
            = Replaced all references to `Vert` with references to `Float3`
        * Float4
            + InverseMagnitude
            = Replaced all references to `Vert` with references to `Float3`
        * Int2
            + InverseMagnitude
            = Replaced all references to `Vert` with references to `Float3`
        * Int3
            + InverseMagnitude
            = Replaced all references to `Vert` with references to `Float3`
        * Int4
            + InverseMagnitude
            = Replaced all references to `Vert` with references to `Float3`
        * Vector2d
            = Replaced all references to `Vert` with references to `Float3`
        * Vector3d
            = Replaced all references to `Vert` with references to `Float3`
        * Mathf
            = Modified `InverseSqrt(float)` to use the faster unsafe inverse square root method.
```
