﻿namespace Nerd_STF.Mathematics.Geometry.Abstract;

public interface ITriangulate
{
    public static virtual Triangle[] TriangulateAll<T>(params T[] triangulatables) where T : ITriangulate
    {
        List<Triangle> res = new();
        foreach (ITriangulate triangulatable in triangulatables) res.AddRange(triangulatable.Triangulate());
        return res.ToArray();
    }

    public Triangle[] Triangulate();
}