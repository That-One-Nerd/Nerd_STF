using System;
using System.Numerics;

namespace Nerd_STF.Helpers;

internal static class GeometryHelper
{ 
    public static Float2 Box2dAlongRay(in Box2d box, Float2 p)
    {
        // This is an approximate system. It's good enough for most purposes
        // but maybe not all of them. Basically it just makes a ray through the point
        // and sees where it intersects the box.

        if (box.Contains(p)) return p;

        Float2 c = box.center, m1 = box.Max, m2 = box.Min;

        // Calculates the coordinates of the ray along the points `p` and `c`
        // for both a reference `x` and a reference `y`.
        float rayRefX(float x) => c.y - (p.y - c.y) * (c.x - x) / (p.x - c.x);
        float rayRefY(float y) => c.x - (p.x - c.x) * (c.y - y) / (p.y - c.y);

        // Calculates all possible intersections for the rectangle at once, then
        // eliminates invalid options.
        float xSol1 = (m1.y - c.y) * (p.x - c.x) / (p.y - c.y) + c.x,
              xSol2 = (m2.y - c.y) * (p.x - c.x) / (p.y - c.y) + c.x,
              ySol1 = (m1.x - c.x) * (p.y - c.y) / (p.x - c.x) + c.y,
              ySol2 = (m2.x - c.x) * (p.y - c.y) / (p.x - c.x) + c.y;

        const float tolerance = 0.0005f;

        Float2 option1, option2;
        float dist1, dist2;

        if (float.IsNormal(xSol1) && m1.x - xSol1 >= -tolerance && xSol1 - m2.x >= -tolerance)
        {
            // The valid solutions are xSol1 and xSol2.
            option1 = (xSol1, rayRefX(xSol1));
            option2 = (xSol2, rayRefX(xSol2));
        }
        else
        {
            // The valid solutions are ySol1 and ySol2.
            option1 = (rayRefY(ySol1), ySol1);
            option2 = (rayRefY(ySol2), ySol2);
        }

        dist1 = (p - option1).Magnitude;
        dist2 = (p - option2).Magnitude;

        // Pick the closest option.
        if (dist1 < dist2) return option1;
        else return option2;
    }

    public static bool LineIntersects(in Line a, in Line b) =>
        LineIntersects2d(a, b, CrossSection2d.XY) &&
        LineIntersects2d(a, b, CrossSection2d.YZ) &&
        LineIntersects2d(a, b, CrossSection2d.ZX);
    public static bool LineIntersects2d(in Line a, in Line b, CrossSection2d plane)
    {
        Float2 p1 = a.a.GetCrossSection(plane), q1 = a.b.GetCrossSection(plane),
               p2 = b.a.GetCrossSection(plane), q2 = b.b.GetCrossSection(plane);

        OrientationType o1 = GetOrientation(p1, q1, p2),
                        o2 = GetOrientation(p1, q1, q2),
                        o3 = GetOrientation(p2, q2, p1),
                        o4 = GetOrientation(p2, q2, q1);

        return (o1 != o2 && o3 != o4) ||
               (o1 == OrientationType.Colinear && PointOnSegmentCo(p1, p2, q1)) ||
               (o2 == OrientationType.Colinear && PointOnSegmentCo(p1, q2, q1)) ||
               (o3 == OrientationType.Colinear && PointOnSegmentCo(p2, p1, q2)) ||
               (o4 == OrientationType.Colinear && PointOnSegmentCo(p2, q1, q2));
    }

    public static float EllipsePerimeterInfiniteSeries(in Ellipse ellipse, int steps)
    {
        // Not gonna write out the infinite series because it's pretty big.
        float a = ellipse.Radius.x,
              e = ellipse.Eccentricity;

        decimal sumPart = 0;
        for (int i = 1; i <= steps; i++)
        {
            BigInteger num = Mathf.Factorial(2 * i);
            num = num * num;

            BigInteger den = Mathf.Power(2, i);
            den *= Mathf.Factorial(i);
            den = den * den * den * den;
            den *= 2 * i - 1;

            Rational ePart = Rational.FromFloat(Mathf.Power(e, 2 * i));
            num *= ePart.numerator;
            den *= ePart.denominator;

            num *= 1_000_000_000;
            sumPart += (decimal)(num / den) / 1_000_000_000;
        }

        return 2 * a * Constants.Pi * (1 - (float)sumPart);
    }
    public static float EllipsePerimeterParker1(in Ellipse ellipse)
    {
        // pi*( 53a/3 + 717b/35 - sqrt( 269a^2 + 667ab + 371b^2 ) )
        // a must be larger than b
        float a = float.Max(ellipse.Radius.x, ellipse.Radius.y),
              b = float.Min(ellipse.Radius.x, ellipse.Radius.y);

        float part1 = 53 * a / 3,
              part2 = 717 * b / 35,
              part3 = Mathf.Sqrt(269 * a * a + 667 * a * b + 371 * b * b);
        return Constants.Pi * (part1 + part2 - part3);
    }
    public static float EllipsePerimeterParker2(in Ellipse ellipse)
    {
        // pi*( 6a/5 + 3b/4 )
        // a must be larger than b
        float a = float.Max(ellipse.Radius.x, ellipse.Radius.y),
              b = float.Min(ellipse.Radius.x, ellipse.Radius.y);

        return Constants.Pi * ((6 * a / 5) + (3 * b / 4));
    }
    public static float EllipsePerimeterRamanujan1(in Ellipse ellipse)
    {
        // pi*( 3*(a+b) - sqrt( (3a + b)(a + 3b) ) )
        float a = ellipse.Radius.x,
              b = ellipse.Radius.y;

        float sqrtPart = Mathf.Sqrt((3 * a + b) * (a + 3 * b));
        return Constants.Pi * (3 * (a + b) - sqrtPart);
    }
    public static float EllipsePerimeterRamanujan2(in Ellipse ellipse)
    {
        // pi*(a+b)*( 1 + 3h/(10 + sqrt(4 - 3h)) )
        float a = ellipse.Radius.x,
              b = ellipse.Radius.y,
              h = ellipse.H;

        float part1 = a + b,
              part2a = Mathf.Sqrt(4 - 3 * h),
              part2b = 10 + part2a,
              part2 = 3 * h / part2b;
        return Constants.Pi * part1 * (1 + part2);
    }

    private static bool PointOnSegmentCo(Float2 a, Float2 r, Float2 b)
    {
        // Just checks the box. Points must be colinear.
        return r.x <= Mathf.Max(a.x, b.x) && r.x >= Mathf.Min(a.x, b.x) &&
               r.y <= Mathf.Max(a.y, b.y) && r.y >= Mathf.Min(a.y, b.y);
    }
    private static OrientationType GetOrientation(Float2 a, Float2 b, Float2 c)
    {
        // Rotation type between the three points.
        float rot = (b.y - a.y) * (c.x - b.x) -
                    (b.x - a.x) * (c.y - b.y);

        if (rot > 0) return OrientationType.Clockwise;
        else if (rot < 0) return OrientationType.CounterClockwise;
        else return OrientationType.Colinear;
    }

    private enum OrientationType
    {
        Colinear,
        Clockwise,
        CounterClockwise
    }

    public static Triangle[] EllipseTriangulateFan(in Ellipse ellipse, float step)
    {
        Float2[] points = new Float2[(int)(1 / step)];
        float position = 0;
        for (int i = 0; i < points.Length; i++)
        {
            points[i] = ellipse.LerpAcrossOutline(position);
            position += step;
        }

        Triangle[] tris = new Triangle[points.Length];
        for (int i = 0; i < tris.Length; i++)
        {
            int i1 = i, i2 = (i + 1) % points.Length;
            tris[i] = (ellipse.Position, points[i1], points[i2]);
        }
        return tris;
    }
}
