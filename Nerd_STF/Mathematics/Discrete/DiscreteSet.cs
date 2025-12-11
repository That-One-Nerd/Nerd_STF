using Nerd_STF.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Nerd_STF.Mathematics.Discrete
{
    public class DiscreteSet<T> : IFiniteSet<DiscreteSet<T>, T>
        where T : IEquatable<T>
    {
        public int Count => arr.Length;

        private readonly T[] arr;

        private DiscreteSet(T[] arr) => this.arr = arr;
        public DiscreteSet()
        {
            arr = TargetHelper.EmptyArray<T>();
        }
        public DiscreteSet(IEnumerable<T> items)
        {
            // Kinda gross, we're making a lot of references here.
            DiscreteSet<T> sub = new DiscreteSet<T>();
            foreach (T item in items) sub += item;
            arr = sub.arr;
        }
        public DiscreteSet(DiscreteSet<T> copy)
        {
            arr = copy.arr;
        }

        public bool Contains(T item)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i].Equals(item)) return true;
            }
            return false;
        }
#if CS8_OR_GREATER
        public bool Equals(DiscreteSet<T>? other)
#else
        public bool Equals(DiscreteSet<T> other)
#endif
        {
            if (other is null) return false;
            else if (arr.Length != other.arr.Length) return false;
            for (int i = 0; i < arr.Length; i++) if (!other.Contains(arr[i])) return false;
            return true;
        }
#if CS8_OR_GREATER
        public bool Equals<TSet>(IFiniteSet<TSet, T>? other)
#else
        public bool Equals<TSet>(IFiniteSet<TSet, T> other)
#endif
            where TSet : IFiniteSet<TSet, T>
        {
            if (other is null) return false;
            else if (arr.Length != other.Count) return false;
            for (int i = 0; i < arr.Length; i++) if (!other.Contains(arr[i])) return false;
            return true;
        }
#if CS8_OR_GREATER
        public override bool Equals(object? other)
#else
        public override bool Equals(object other)
#endif
        {
            // Note: This implementation won't check for IFiniteSet!
            //       In most circumstances, that's okay, because the proper
            //       method overload will be called. But sometimes this may
            //       cause weird issues.
            if (other is null) return false;
            else if (other is DiscreteSet<T> otherSet) return Equals(otherSet);
            else return false;
        }

        public DiscreteSet<T> With(T item)
        {
            if (Contains(item)) return new DiscreteSet<T>(arr);
            T[] result = new T[arr.Length + 1];
            Array.Copy(arr, result, arr.Length);
            result[arr.Length] = item;
            return new DiscreteSet<T>(result);
        }

        public DiscreteSet<T> Union(DiscreteSet<T> other)
        {
            List<T> result = new List<T>(arr);
            foreach (T item in other) if (!Contains(item)) result.Add(item);
            return new DiscreteSet<T>(result.ToArray());
        }
        public DiscreteSet<T> Intersection(DiscreteSet<T> other)
        {
            List<T> result = new List<T>();
            foreach (T item in other) if (Contains(item)) result.Add(item);
            return new DiscreteSet<T>(result.ToArray());
        }
        public DiscreteSet<T> Difference(DiscreteSet<T> other)
        {
            List<T> result = new List<T>(arr);
            foreach (T item in other) result.Remove(item);
            return new DiscreteSet<T>(result.ToArray());
        }

        public IEnumerator<T> GetEnumerator()
        {
            foreach (T item in arr) yield return item;
        }
        IEnumerator IEnumerable.GetEnumerator() => arr.GetEnumerator();

        public override int GetHashCode()
        {
            int result = 0;
            for (int i = 0; i < arr.Length; i++) result ^= arr[i].GetHashCode();
            return result;
        }
        public override string ToString()
        {
            StringBuilder result = new StringBuilder("{ ");
            for (int i = 0; i < arr.Length; i++)
            {
                result.Append(arr[i]);
                if (i < arr.Length - 1) result.Append(", ");
                else result.Append(' ');
            }
            result.Append('}');
            return result.ToString();
        }

        public static DiscreteSet<T> operator +(DiscreteSet<T> a, T b) => a.With(b);
        public static DiscreteSet<T> operator &(DiscreteSet<T> a, DiscreteSet<T> b) => a.Intersection(b);
        public static DiscreteSet<T> operator |(DiscreteSet<T> a, DiscreteSet<T> b) => a.Union(b);
        public static DiscreteSet<T> operator -(DiscreteSet<T> a, DiscreteSet<T> b) => a.Difference(b);
    }
}
