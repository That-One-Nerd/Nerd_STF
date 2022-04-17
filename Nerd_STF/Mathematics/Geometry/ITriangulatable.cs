using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nerd_STF.Mathematics.Geometry
{
    public interface ITriangulatable
    {
        public static Triangle[] TriangulateAll(params ITriangulatable[] triangulatables)
        {
            List<Triangle> res = new();
            foreach (ITriangulatable triangulatable in triangulatables) res.AddRange(triangulatable.Triangulate());
            return res.ToArray();
        }

        public Triangle[] Triangulate();
    }
}
