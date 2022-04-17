# Nerd_STF v2.1.0

```
* Nerd_STF
    + Exceptions
        + Nerd_STFException
        + DifferingVertCountException
        + DisconnectedLinesException
    + Miscellaneous
        + `GlobalUsings.cs`
    + IClosest<T>
    + IContainer<T>
    * Logger
        * DefaultLogHandler(LogMessage)
            = Replaced a `throw new Exception` with a `throw new ArgumentException`
    * Mathematics
        + Angle
        + Calculus
        + delegate double Equation(double)
        * Double2
            = Made `CompareTo(Double2)` better
        * Double3
            = Made `CompareTo(Double3)` better
        * Double4
            = Made `CompareTo(Double4)` better
        * Int2
            + operator &(Int2, Int2)
            + operator |(Int2, Int2)
            + operator ^(Int2, Int2)
            = Made `CompareTo(Int2)` better
        * Int3
            + operator &(Int3, Int3)
            + operator |(Int3, Int3)
            + operator ^(Int3, Int3)
            = Made `CompareTo(Int3)` better
        * Int4
            + operator &(Int4, Int4)
            + operator |(Int4, Int4)
            + operator ^(Int4, Int4)
            = Made `CompareTo(Int4)` better
        * Mathf
            + Average(Equation, double, double, double)
            + GetValues(Equation)
            + MakeEquation(Dictionary<double, double>)
            + Max(Equation, double, double, double)
            + Min(Equation, double, double, double)
            = Swapped the names of "RadToDeg" and "DegToRad"
        * Geometry
            + Box2D
            + Box3D
            + Polygon
            + Quadrilateral
            + Sphere
            + ISubdividable
            * ITriangulatable
                + Triangle[] TriangulateAll(params ITriangulatable[])
            * Line
                + : IComparable<Line>
                + : IContainer<Vert>
                + : IClosest<Vert>
                + : ISubdividable<Line[]>
                + ClosestTo(Vert)
                + ClosestTo(Vert, double)
                + CompareTo(Line)
                + Contains(Vert)
                + Subdivide()
                + operator -(Line)
                + operator >(Line)
                + operator <(Line)
                + operator >=(Line)
                + operator <=(Line)
                = Renamed all instances of "start" to "a"
                = Renamed all instances of "end" to "b"
            * Triangle
                + operator -(Triangle)
                + ToDoubleArrayAll(params Triangle[])
                = Replaced the variable assignings in the Triangle to not re-assign the lines.
                = Now uses custom exception in line constructor
                = Renamed "L1" to "AB"
                = Renamed "L2" to "BC"
                = Renamed "L3" to "CA"
```
