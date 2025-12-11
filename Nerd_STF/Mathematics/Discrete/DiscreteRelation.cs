using Nerd_STF.Mathematics.Algebra;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nerd_STF.Mathematics.Discrete
{
    public class DiscreteRelation<TItem1, TItem2>// : IFiniteRelation<TItem1, TItem2>,
                                                 //   IFiniteSet<DiscreteRelation<TItem1, TItem2>, (TItem1, TItem2)>
        where TItem1 : IEquatable<TItem1>
        where TItem2 : IEquatable<TItem2>
    {
        public int Count => relations.Count;

        private readonly DiscreteSet<(TItem1, TItem2)> relations;

        public DiscreteRelation()
        {
            relations = new DiscreteSet<(TItem1, TItem2)>();
        }
        public DiscreteRelation(DiscreteSet<(TItem1, TItem2)> set)
        {
            relations = set;
        }
        public DiscreteRelation(IEnumerable<(TItem1, TItem2)> items) : this(new DiscreteSet<(TItem1, TItem2)>(items)) { }

        public DiscreteSet<TItem2> Get(TItem1 item)
        {
            DiscreteSet<TItem2> result = new DiscreteSet<TItem2>();
            foreach ((TItem1, TItem2) rel in relations)
            {
                if (rel.Item1.Equals(item)) result += rel.Item2;
            }
            return result;
        }
        //IEnumerable<TItem2> IRelation<TItem1, TItem2>.Get(TItem1 item) => Get(item);
        public (DiscreteSet<TItem1>, DiscreteSet<TItem2>) Distinct()
        {
            DiscreteSet<TItem1> item1s = new DiscreteSet<TItem1>();
            DiscreteSet<TItem2> item2s = new DiscreteSet<TItem2>();
            foreach ((TItem1, TItem2) rel in relations)
            {
                item1s += rel.Item1;
                item2s += rel.Item2;
            }
            return (item1s, item2s);
        }
        //(IEnumerable<TItem1>, IEnumerable<TItem2>) IFiniteRelation<TItem1, TItem2>.Distinct() => Distinct();

        public bool IsRelated(TItem1 item1, TItem2 item2) => relations.Contains((item1, item2));

        public Matrix GetMatrix()
        {
            (DiscreteSet<TItem1>, DiscreteSet<TItem2>) sets = Distinct();
            (TItem1[] item1s, TItem2[] item2s) = (sets.Item1.ToArray(), sets.Item2.ToArray());

            Matrix result = new Matrix((item1s.Length, item2s.Length));
            for (int r = 0; r < item1s.Length; r++)
            {
                foreach (TItem2 related in Get(item1s[r]))
                {
                    for (int c = 0; c < item2s.Length; c++)
                    {
                        if (item2s[c].Equals(related))
                        {
                            result[r, c] = 1;
                            break;
                        }
                    }
                }
            }
            return result;
        }

        public DiscreteRelation<TItem1, TItem2> With(TItem1 item1, TItem2 item2) => new DiscreteRelation<TItem1, TItem2>(relations.With((item1, item2)));
        public DiscreteRelation<TItem1, TItem2> With((TItem1, TItem2) rel) => new DiscreteRelation<TItem1, TItem2>(relations.With(rel));

        public IEnumerator<(TItem1, TItem2)> GetEnumerator() => relations.GetEnumerator();
        //IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public override string ToString()
        {
            StringBuilder result = new StringBuilder("{ ");
            int i = 0;
            foreach ((TItem1, TItem2) rel in relations)
            {
                result.Append($"({rel.Item1}, {rel.Item2})");
                if (i < relations.Count - 1) result.Append(", ");
                else result.Append(' ');
                i++;
            }
            result.Append('}');
            return result.ToString();
        }

        public static DiscreteRelation<TItem1, TItem2> operator +(DiscreteRelation<TItem1, TItem2> relation, (TItem1, TItem2) rel) => relation.With(rel);
    }
}
