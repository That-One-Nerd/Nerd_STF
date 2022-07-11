namespace Nerd_STF.Mathematics.Geometry;

public struct Polygon : ICloneable, IEquatable<Polygon>, IGroup<Vert>, ISubdividable<Polygon>, ITriangulatable
{
    public Line[] Lines
    {
        get => p_lines;
        set
        {
            p_lines = value;
            p_verts = GenerateVerts(value);
        }
    }
    public Vert Midpoint => Vert.Average(Verts);
    public Vert[] Verts
    {
        get => p_verts;
        set
        {
            p_verts = value;
            p_lines = GenerateLines(value);
        }
    }

    private Line[] p_lines;
    private Vert[] p_verts;

    [Obsolete("This method uses the Polygon.Triangulate() function, which has issues. It will be fixed in a " +
        "future update.")]
    public float Area
    {
        get
        {
            float val = 0;
            foreach (Triangle t in Triangulate()) val += t.Area;
            return val;
        }
    }
    public float Perimeter
        {
            get
            {
                float val = 0;
                foreach (Line l in Lines) val += l.Length;
                return val;
            }
        }

    public Polygon()
    {
        p_lines = Array.Empty<Line>();
        p_verts = Array.Empty<Vert>();
    }
    public Polygon(Fill<Vert?> fill)
        {
            List<Vert> verts = new();
            int i = 0;
            while (true)
            {
                Vert? v = fill(i);
                if (!v.HasValue) break;
                verts.Add(v.Value);
            }
            this = new(verts.ToArray());
        }
    public Polygon(Fill<Float3?> fill)
        {
            List<Vert> verts = new();
            int i = 0;
            while (true)
            {
                Vert? v = fill(i);
                if (!v.HasValue) break;
                verts.Add(v.Value);
            }
            this = new(verts.ToArray());
        }
    public Polygon(Fill<Line?> fill)
        {
            List<Line> lines = new();
            int i = 0;
            while (true)
            {
                Line? v = fill(i);
                if (!v.HasValue) break;
                lines.Add(v.Value);
            }
            this = new(lines.ToArray());
        }
    public Polygon(Fill<Vert> fill, int length)
        {
            List<Vert> verts = new();
            for (int i = 0; i < length; i++) verts.Add(fill(i));
            this = new(verts.ToArray());
        }
    public Polygon(Fill<Float3> fill, int length)
        {
            List<Vert> verts = new();
            for (int i = 0; i < length; i++) verts.Add(fill(i));
            this = new(verts.ToArray());
        }
    public Polygon(Fill<Line> fill, int length)
        {
            List<Line> lines = new();
            for (int i = 0; i < length; i++) lines.Add(fill(i));
            this = new(lines.ToArray());
        }
    public Polygon(params Float3[] verts)
        {
            p_verts = new Vert[verts.Length];
            for (int i = 0; i < verts.Length; i++) p_verts[i] = verts[i];
            p_lines = GenerateLines(p_verts);
        }
    public Polygon(params Vert[] verts)
        {
            p_verts = verts;
            p_lines = GenerateLines(verts);
        }
    public Polygon(params Line[] lines)
        {
            p_lines = lines;
            p_verts = GenerateVerts(lines);
        }

    public Vert this[int index]
        {
            get => Verts[index];
            set => Verts[index] = value;
        }

    public static Polygon CreateCircle(int vertCount)
    {
        List<Vert> parts = new();
        for (int i = 0; i < vertCount; i++)
        {
            float val = Constants.Tau * i / vertCount;
            parts.Add(new(Mathf.Cos(val), Mathf.Sin(val)));
        }
        return new(parts.ToArray());
    }

    public static Polygon Absolute(Polygon val)
        {
            Vert[] v = val.Verts;
            for (int i = 0; i < v.Length; i++) v[i] = Vert.Absolute(v[i]);
            return new(v);
        }
    public static Polygon Average(params Polygon[] vals)
        {
            if (!CheckVerts(vals)) throw new DifferingVertCountException(nameof(vals), vals);
            if (vals.Length < 1) return default;

            Line[][] lines = new Line[vals.Length][];
            for (int i = 0; i < vals.Length; i++) lines[i] = vals[i].Lines;

            Line[] res = new Line[vals[0].Lines.Length];
            for (int i = 0; i < res.Length; i++)
            {
                Line[] row = new Line[vals.Length];
                for (int j = 0; j < vals[0].Lines.Length; j++) row[j] = vals[j].Lines[i];
                res[i] = Line.Average(row);
            }

            return new(res);
        }
    public static Polygon Ceiling(Polygon val)
        {
            Vert[] v = val.Verts;
            for (int i = 0; i < v.Length; i++) v[i] = Vert.Ceiling(v[i]);
            return new(v);
        }
    public static Polygon Clamp(Polygon val, Polygon min, Polygon max)
        {
            if (!CheckVerts(val, min, max)) throw new DifferingVertCountException(val, min, max);
            Line[][] lines = new Line[3][] { val.Lines, min.Lines, max.Lines };
            Line[] res = new Line[val.Lines.Length];
            for (int i = 0; i < res.Length; i++) res[i] = Line.Clamp(lines[0][i], lines[1][i], lines[2][i]);
            return new(res);
        }
    public static Polygon Floor(Polygon val)
        {
            Vert[] v = val.Verts;
            for (int i = 0; i < v.Length; i++) v[i] = Vert.Floor(v[i]);
            return new(v);
        }
    public static Polygon Lerp(Polygon a, Polygon b, float t, bool clamp = true)
        {
            if (!CheckVerts(a, b)) throw new DifferingVertCountException(a, b);
            Line[][] lines = new Line[2][] { a.Lines, b.Lines };
            Line[] res = new Line[a.Lines.Length];
            for (int i = 0; i < res.Length; i++) res[i] = Line.Lerp(lines[0][i], lines[1][i], t, clamp);
            return new(res);
        }
    public static Polygon Max(params Polygon[] vals)
        {
            if (!CheckVerts(vals)) throw new DifferingVertCountException(nameof(vals), vals);
            if (vals.Length < 1) return default;

            Line[][] lines = new Line[vals.Length][];
            for (int i = 0; i < vals.Length; i++) lines[i] = vals[i].Lines;

            Line[] res = new Line[vals[0].Lines.Length];
            for (int i = 0; i < res.Length; i++)
            {
                Line[] row = new Line[vals.Length];
                for (int j = 0; j < vals[0].Lines.Length; j++) row[j] = vals[j].Lines[i];
                res[i] = Line.Max(row);
            }

            return new(res);
        }
    public static Polygon Median(params Polygon[] vals)
        {
            if (!CheckVerts(vals)) throw new DifferingVertCountException(nameof(vals), vals);
            if (vals.Length < 1) return default;

            Line[][] lines = new Line[vals.Length][];
            for (int i = 0; i < vals.Length; i++) lines[i] = vals[i].Lines;

            Line[] res = new Line[vals[0].Lines.Length];
            for (int i = 0; i < res.Length; i++)
            {
                Line[] row = new Line[vals.Length];
                for (int j = 0; j < vals[0].Lines.Length; j++) row[j] = vals[j].Lines[i];
                res[i] = Line.Median(row);
            }

            return new(res);
        }
    public static Polygon Min(params Polygon[] vals)
        {
            if (!CheckVerts(vals)) throw new DifferingVertCountException(nameof(vals), vals);
            if (vals.Length < 1) return default;

            Line[][] lines = new Line[vals.Length][];
            for (int i = 0; i < vals.Length; i++) lines[i] = vals[i].Lines;

            Line[] res = new Line[vals[0].Lines.Length];
            for (int i = 0; i < res.Length; i++)
            {
                Line[] row = new Line[vals.Length];
                for (int j = 0; j < vals[0].Lines.Length; j++) row[j] = vals[j].Lines[i];
                res[i] = Line.Min(row);
            }

            return new(res);
        }

    public static float[] ToFloatArrayAll(params Polygon[] polys) => ToFloatListAll(polys).ToArray();
    public static List<float> ToFloatListAll(params Polygon[] polys)
        {
            List<float> vals = new();
            foreach (Polygon poly in polys) vals.AddRange(poly.ToFloatArray());
            return vals;
        }

    public override bool Equals([NotNullWhen(true)] object? obj)
        {
            if (obj == null || obj.GetType() != typeof(Polygon)) return false;
            return Equals((Polygon)obj);
        }
    public bool Equals(Polygon other)
        {
            if (!CheckVerts(this, other)) return false;
            return Lines == other.Lines;
        }
    public override int GetHashCode() => Lines.GetHashCode();
    public override string ToString() => ToString((string?)null);
    public string ToString(string? provider)
        {
            string s = "";
            for (int i = 0; i < Lines.Length; i++) s += "L" + i + ": " + Lines[i].ToString(provider) + " ";
            return s;
        }
    public string ToString(IFormatProvider provider)
        {
            string s = "";
            for (int i = 0; i < Lines.Length; i++) s += "L" + i + ": " + Lines[i].ToString(provider) + " ";
            return s;
        }

    public object Clone() => new Polygon(Lines);

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    public IEnumerator<Vert> GetEnumerator() { foreach (Vert v in Verts) yield return v; }

    public Vert[] ToArray() => Verts;
    public List<Vert> ToList() => new(Verts);

    public float[] ToFloatArray()
        {
            float[] vals = new float[Verts.Length * 3];
            for (int i = 0; i < Verts.Length; i++)
            {
                int pos = i * 3;
                vals[pos + 0] = Verts[i].position.x;
                vals[pos + 1] = Verts[i].position.y;
                vals[pos + 2] = Verts[i].position.z;
            }
            return vals;
        }
    public List<float> ToFloatList() => new(ToFloatArray());

    public Polygon Subdivide()
    {
        Polygon poly = new();
        List<Line> lines = new();
        for (int i = 0; i < Lines.Length; i++) lines.AddRange(Lines[i].Subdivide());
        return poly;
    }
    public Polygon Subdivide(int iterations)
    {
        if (iterations < 1) return new();
        Polygon poly = this;
        for (int i = 0; i < iterations; i++) poly = poly.Subdivide();
        return poly;
    }

    public Polygon SubdivideCatmullClark(int segments)
    {
        // Thanks Saalty for making this accidentally.
        List<Vert> newVerts = new();
        for (int i = 0; i < Verts.Length; i++)
        {
            for (int factor = 0; factor < segments; factor++)
            {
                float unit = factor / (float)(segments * 2), unit2 = unit + 0.5f, lastUnit = unit * 2;
                Vert p1, p2;
                if (i == Verts.Length - 1)
                {
                    p1 = Verts[^1] + (Verts[0] - Verts[^1]) * unit2;
                    p2 = Verts[0] + (Verts[1] - Verts[0]) * unit;
                }
                else if (i == Verts.Length - 2)
                {
                    p1 = Verts[^2] + (Verts[^1] - Verts[^2]) * unit2;
                    p2 = Verts[^1] + (Verts[0] - Verts[^1]) * unit;
                }
                else
                {
                    p1 = Verts[i] + (Verts[i + 1] - Verts[i]) * unit2;
                    p2 = Verts[i + 1] + (Verts[i + 2] - Verts[i + 1]) * unit;
                }
                newVerts.Add(p1 + (p2 - p1) * lastUnit);
            }
        }
        return new(newVerts.ToArray());
    }

    [Obsolete("This method doesn't work very well, and will give very weird results in certain cases. " +
        "This will be fixed in a future update.")]
    public Triangle[] Triangulate()
        {
            // This may cause issues. FIXME
            // Tbh, not even sure if this works. This was a bit confusing.

            if (Verts.Length == 3) return new Triangle[] { new(Verts[0], Verts[1], Verts[2]) };

            (int posA, int posB, Line line)? closest = null;
            for (int i = 0; i < Verts.Length; i++)
            {
                for (int j = 0; j < Verts.Length; j++)
                {
                    if (i == j) continue;
                    Line l = new(Verts[i], Verts[j]);
                    if (Lines.Contains(l)) continue;

                    if (!closest.HasValue || closest.Value.line.Length > l.Length) closest = (i, j, l);
                }
            }

            if (closest == null) throw new("Unknown error triangulating the polygon.");

            if (closest.Value.posB > closest.Value.posA)
                closest = (closest.Value.posB, closest.Value.posA, closest.Value.line);

            List<Line> partA = new(Lines[closest.Value.posA..(closest.Value.posB - 1)]);
            partA.Add(closest.Value.line);

            Polygon pA = new(partA.ToArray());

            List<Line> partB = new(Lines[0..(closest.Value.posA - 1)]);
            partB.AddRange(Lines[closest.Value.posB..(Lines.Length - 1)]);
            partB.Add(closest.Value.line);

            Polygon pB = new(partB.ToArray());

            List<Triangle> tris = new(pA.Triangulate());
            tris.AddRange(pB.Triangulate());
            return tris.ToArray();
        }

    private static bool CheckVerts(params Polygon[] polys)
        {
            int len = -1;
            foreach (Polygon poly in polys)
            {
                if (len == -1)
                {
                    len = poly.Verts.Length;
                    continue;
                }
                if (poly.Verts.Length != len) return false;
            }
            return true;
        }
    private static Line[] GenerateLines(Vert[] verts)
        {
            Line[] lines = new Line[verts.Length];
            for (int i = 0; i < lines.Length; i++)
                lines[i] = new(verts[i], verts[i == lines.Length - 1 ? 0 : i + 1]);
            return lines;
        }
    private static Vert[] GenerateVerts(Line[] lines)
        {
            Vert[] verts = new Vert[lines.Length];
            for (int i = 0; i < verts.Length; i++)
            {
                verts[i] = lines[i].a;
                if (lines[i].b != lines[i == verts.Length - 1 ? 0 : i + 1].a)
                    throw new DisconnectedLinesException(nameof(lines), lines);
            }
            return verts;
        }

    public static Polygon operator +(Polygon a, Polygon b)
    {
        if (!CheckVerts(a, b)) throw new DifferingVertCountException(a, b);
        Line[][] lines = new Line[2][] { a.Lines, b.Lines };
        Line[] res = new Line[a.Lines.Length];
        for (int i = 0; i < res.Length; i++) res[i] = lines[0][i] + lines[1][i];
        return new(res);
    }
    public static Polygon operator +(Polygon a, Vert b)
    {
        Line[] lines = a.Lines;
        for (int i = 0; i < lines.Length; i++) lines[i] += b;
        return new(lines);
    }
    public static Polygon operator -(Polygon p)
    {
        Line[] lines = p.Lines;
        for (int i = 0; i < lines.Length; i++) lines[i] = -lines[i];
        return new(lines);
    }
    public static Polygon operator -(Polygon a, Polygon b)
    {
        if (!CheckVerts(a, b)) throw new DifferingVertCountException(a, b);
        Line[][] lines = new Line[2][] { a.Lines, b.Lines };
        Line[] res = new Line[a.Lines.Length];
        for (int i = 0; i < res.Length; i++) res[i] = lines[0][i] - lines[1][i];
        return new(res);
    }
    public static Polygon operator -(Polygon a, Vert b)
    {
        Line[] lines = a.Lines;
        for (int i = 0; i < lines.Length; i++) lines[i] -= b;
        return new(lines);
    }
    public static Polygon operator *(Polygon a, Polygon b)
    {
        if (!CheckVerts(a, b)) throw new DifferingVertCountException(a, b);
        Line[][] lines = new Line[2][] { a.Lines, b.Lines };
        Line[] res = new Line[a.Lines.Length];
        for (int i = 0; i < res.Length; i++) res[i] = lines[0][i] * lines[1][i];
        return new(res);
    }
    public static Polygon operator *(Polygon a, Vert b)
    {
        Line[] lines = a.Lines;
        for (int i = 0; i < lines.Length; i++) lines[i] *= b;
        return new(lines);
    }
    public static Polygon operator *(Polygon a, float b)
    {
        Line[] lines = a.Lines;
        for (int i = 0; i < lines.Length; i++) lines[i] *= b;
        return new(lines);
    }
    public static Polygon operator /(Polygon a, Polygon b)
    {
        if (!CheckVerts(a, b)) throw new DifferingVertCountException(a, b);
        Line[][] lines = new Line[2][] { a.Lines, b.Lines };
        Line[] res = new Line[a.Lines.Length];
        for (int i = 0; i < res.Length; i++) res[i] = lines[0][i] / lines[1][i];
        return new(res);
    }
    public static Polygon operator /(Polygon a, Vert b)
    {
        Line[] lines = a.Lines;
        for (int i = 0; i < lines.Length; i++) lines[i] /= b;
        return new(lines);
    }
    public static Polygon operator /(Polygon a, float b)
    {
        Line[] lines = a.Lines;
        for (int i = 0; i < lines.Length; i++) lines[i] /= b;
        return new(lines);
    }
    public static bool operator ==(Polygon a, Polygon b) => a.Equals(b);
    public static bool operator !=(Polygon a, Polygon b) => !a.Equals(b);

    public static implicit operator Polygon(Fill<Vert?> fill) => new(fill);
    public static implicit operator Polygon(Fill<Float3?> fill) => new(fill);
    public static implicit operator Polygon(Fill<Line?> fill) => new(fill);
    public static implicit operator Polygon(Vert[] verts) => new(verts);
    public static implicit operator Polygon(Float3[] verts) => new(verts);
    public static implicit operator Polygon(Line[] lines) => new(lines);
    public static implicit operator Polygon(Triangle tri) => new(tri.AB, tri.BC, tri.CA);
    public static implicit operator Polygon(Quadrilateral quad) => new(quad.AB, quad.BC, quad.CD, quad.DA);
}
