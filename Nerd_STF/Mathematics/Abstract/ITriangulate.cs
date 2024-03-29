﻿namespace Nerd_STF.Mathematics.Abstract;

public interface ITriangulate
{
    public static Triangle[] TriangulateAll(params ITriangulate[] triangulatables)
    {
        List<Triangle> res = new();
        foreach (ITriangulate triangulatable in triangulatables) res.AddRange(triangulatable.Triangulate());
        return res.ToArray();
    }
    public static Triangle[] TriangulateAll<T>(params T[] triangulatables) where T : ITriangulate
    {
        List<Triangle> res = new();
        foreach (ITriangulate triangulatable in triangulatables) res.AddRange(triangulatable.Triangulate());
        return res.ToArray();
    }

    public Triangle[] Triangulate();
}
