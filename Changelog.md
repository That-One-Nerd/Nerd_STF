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
            - Triangle
            - Vert
            = Rewrote all of `Line`
                TODO: Compare me to the original line and note changes.
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
