using Nerd_STF.Mathematics.Algebra;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nerd_STF.Mathematics.Discrete
{
    public class DiscreteRelation<TItem1, TItem2> : IFiniteRelation<TItem1, TItem2>,
                                                    IFiniteSet<DiscreteRelation<TItem1, TItem2>, (TItem1, TItem2)>
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
        IEnumerable<TItem2> IRelation<TItem1, TItem2>.Get(TItem1 item) => Get(item);
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
        (IEnumerable<TItem1>, IEnumerable<TItem2>) IFiniteRelation<TItem1, TItem2>.Distinct() => Distinct();

        public bool IsRelated(TItem1 item1, TItem2 item2) => relations.Contains((item1, item2));
        bool ISet<DiscreteRelation<TItem1, TItem2>, (TItem1, TItem2)>.Contains((TItem1, TItem2) rel) => relations.Contains(rel);

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

        public DiscreteRelation<TItem1, TItem2> Union(DiscreteRelation<TItem1, TItem2> other) => new DiscreteRelation<TItem1, TItem2>(relations.Union(other.relations));
        public DiscreteRelation<TItem1, TItem2> Intersection(DiscreteRelation<TItem1, TItem2> other) => new DiscreteRelation<TItem1, TItem2>(relations.Intersection(other.relations));
        public DiscreteRelation<TItem1, TItem2> Difference(DiscreteRelation<TItem1, TItem2> other) => new DiscreteRelation<TItem1, TItem2>(relations.Difference(other.relations));

        public bool IsReflexive()
        {
            if (typeof(TItem1) != typeof(TItem2)) return false; // Not the same type, cannot be reflexive.
            DiscreteSet<TItem1> item1s = Distinct().Item1;
            foreach (TItem1 item in item1s)
            {
                TItem2 cast = (TItem2)(object)item;
                if (!IsRelated(item, cast)) return false;
            }
            return true;
        }
        public bool IsSymmetric()
        {
            if (typeof(TItem1) != typeof(TItem2)) return false; // Not the same type, cannot be symmetric.
            foreach ((TItem1, TItem2) pair in relations)
            {
                TItem1 swapA = (TItem1)(object)pair.Item2;
                TItem2 swapB = (TItem2)(object)pair.Item1;
                if (!IsRelated(swapA, swapB)) return false;
            }
            return true;
        }
        public bool IsAntiSymmetric()
        {
            if (typeof(TItem1) != typeof(TItem2)) return true; // Not the same type, will always be antisymmetric.
            foreach ((TItem1, TItem2) pair in relations)
            {
                TItem1 swapA = (TItem1)(object)pair.Item2;
                TItem2 swapB = (TItem2)(object)pair.Item1;
                if (IsRelated(swapA, swapB)) return false;
            }
            return true;
        }

        public IEnumerator<(TItem1, TItem2)> GetEnumerator() => relations.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

#if CS8_OR_GREATER
        public bool Equals(DiscreteRelation<TItem1, TItem2>? other)
#else
        public bool Equals(DiscreteRelation<TItem1, TItem2> other)
#endif
        {
            if (other is null) return false;
            else if (Count != other.Count) return false;
            else return relations.Equals(other.relations);
        }
#if CS8_OR_GREATER
        public bool Equals(IFiniteRelation<TItem1, TItem2>? other)
#else
        public bool Equals(IFiniteRelation<TItem1, TItem2> other)
#endif
        {
            if (other is null) return false;
            else if (Count != other.Count) return false;
            foreach ((TItem1, TItem2) rel in relations) if (!other.IsRelated(rel.Item1, rel.Item2)) return false;
            return true;
        }
#if CS8_OR_GREATER
        public override bool Equals(object? other)
#else
        public override bool Equals(object other)
#endif
        {
            if (other is null) return false;
            else if (other is DiscreteRelation<TItem1, TItem2> discreteRel) return Equals(discreteRel);
            else if (other is IFiniteRelation<TItem1, TItem2> finiteRel) return Equals(finiteRel);
            else return false;
        }
        public override int GetHashCode() => relations.GetHashCode();
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

        public static DiscreteRelation<TItem1, TItem2> operator +(DiscreteRelation<TItem1, TItem2> a, (TItem1, TItem2) b) => a.With(b);
        public static DiscreteRelation<TItem1, TItem2> operator &(DiscreteRelation<TItem1, TItem2> a, DiscreteRelation<TItem1, TItem2> b) => a.Union(b);
        public static DiscreteRelation<TItem1, TItem2> operator |(DiscreteRelation<TItem1, TItem2> a, DiscreteRelation<TItem1, TItem2> b) => a.Intersection(b);
        public static DiscreteRelation<TItem1, TItem2> operator -(DiscreteRelation<TItem1, TItem2> a, DiscreteRelation<TItem1, TItem2> b) => a.Difference(b);
    }
}
