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
        * Geometry
                * Line
                    = Replaced all references to `Vert` with references to `Float3`
                * Polygon
                    = Replaced all references to `Vert` with references to `Float3`
                * Quadrilateral
                    = Replaced all references to `Vert` with references to `Float3`
                * Sphere
                    = Replaced all references to `Vert` with references to `Float3`
                * Triangle
                    = Replaced all references to `Vert` with references to `Float3`
                = Renamed `Box2D` to `Box2d`
                    = Replaced all references to `Vert` with references to `Float3`
                = Renamed `Box3D` to `Box3d`
                    = Replaced all references to `Vert` with references to `Float3`
            - Vert
        * NumberSystems
            * Complex
                = Replaced all references to `Vert` with references to `Float3`
            * Quaternion
                = Replaced all references to `Vert` with references to `Float3`
        * Float2
            = Replaced all references to `Vert` with references to `Float3`
        * Float3
            = Replaced all references to `Vert` with references to `Float3`
        * Float4
            = Replaced all references to `Vert` with references to `Float3`
        * Int2
            = Replaced all references to `Vert` with references to `Float3`
        * Int3
            = Replaced all references to `Vert` with references to `Float3`
        * Int4
            = Replaced all references to `Vert` with references to `Float3`
        * Vector2d
            = Replaced all references to `Vert` with references to `Float3`
        * Vector3d
            = Replaced all references to `Vert` with references to `Float3`
        * Mathf
            = Modified `InverseSqrt(float)` to use the faster unsafe inverse square root method.
```
