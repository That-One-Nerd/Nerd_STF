using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace Nerd_STF
{
    public readonly struct ListTuple<T> : IEnumerable<T>,
                                          IEquatable<ListTuple<T>>
#if NET471_OR_GREATER || NETCOREAPP2_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
                                         ,ITuple
#endif
    {
        public int Length => items.Length;

        private readonly T[] items;

        public ListTuple(IEnumerable<T> items)
        {
            this.items = items.ToArray();
        }
        public ListTuple(params T[] items)
        {
            this.items = items;
        }
        public ListTuple(Fill<T> items, int length)
        {
            this.items = new T[length];
            for (int i = 0; i < length; i++) this.items[i] = items(i);
        }

        public T this[int index]
        {
            get => items[index];
            set => items[index] = value;
        }
#if NET471_OR_GREATER || NETCOREAPP2_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
#if CS8_OR_GREATER
        object? ITuple.this[int index] => this[index];
#else
        object ITuple.this[int index] => this[index];
#endif
#endif

        public Enumerator GetEnumerator() => new Enumerator(this);
        IEnumerator<T> IEnumerable<T>.GetEnumerator() => GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public bool Equals(ListTuple<T> other)
        {
            if (Length != other.Length) return false;
            for (int i = 0; i < Length; i++)
            {
                T itemA = items[i], itemB = other.items[i];
                if (itemA == null || itemB == null)
                {
                    if (itemA == null && itemB == null) continue;
                    else return false;
                }
                if (!itemA.Equals(itemB)) return false;
            }
            return true;
        }
#if CS8_OR_GREATER
        public override bool Equals(object? other)
#else
        public override bool Equals(object other)
#endif
        {
            if (other is null) return false;
            else if (other is ListTuple<T> otherTuple) return Equals(otherTuple);
            else return false;
        }
        public override int GetHashCode() => items.GetHashCode();
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder("(");
            for (int i = 0; i < items.Length; i++)
            {
                builder.Append(items[i]);
                if (i != items.Length - 1) builder.Append(", ");
            }
            builder.Append(')');
            return builder.ToString();
        }

        public Fill<T> ToFill()
        {
            T[] items = this.items;
            return i => items[i];
        }

        public static bool operator ==(ListTuple<T> a, ListTuple<T> b) => a.Equals(b);
        public static bool operator !=(ListTuple<T> a, ListTuple<T> b) => !a.Equals(b);

        public static implicit operator ValueTuple<T>(ListTuple<T> tuple) => new ValueTuple<T>(tuple[0]);
        public static implicit operator ValueTuple<T, T>(ListTuple<T> tuple) => (tuple[0], tuple[1]);
        public static implicit operator ValueTuple<T, T, T>(ListTuple<T> tuple) => (tuple[0], tuple[1], tuple[2]);
        public static implicit operator ValueTuple<T, T, T, T>(ListTuple<T> tuple) => (tuple[0], tuple[1], tuple[2], tuple[3]);
        public static implicit operator ValueTuple<T, T, T, T, T>(ListTuple<T> tuple) => (tuple[0], tuple[1], tuple[2], tuple[3], tuple[4]);
        public static implicit operator ValueTuple<T, T, T, T, T, T>(ListTuple<T> tuple) => (tuple[0], tuple[1], tuple[2], tuple[3], tuple[4], tuple[5]);
        public static implicit operator ValueTuple<T, T, T, T, T, T, T>(ListTuple<T> tuple) => (tuple[0], tuple[1], tuple[2], tuple[3], tuple[4], tuple[5], tuple[6]);
        public static implicit operator T[](ListTuple<T> tuple) => tuple.items;

        public static implicit operator ListTuple<T>(ValueTuple<T> tuple) => new ListTuple<T>(tuple.Item1);
        public static implicit operator ListTuple<T>((T, T) tuple) => new ListTuple<T>(tuple.Item1, tuple.Item2);
        public static implicit operator ListTuple<T>((T, T, T) tuple) => new ListTuple<T>(tuple.Item1, tuple.Item2, tuple.Item3);
        public static implicit operator ListTuple<T>((T, T, T, T) tuple) => new ListTuple<T>(tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4);
        public static implicit operator ListTuple<T>((T, T, T, T, T) tuple) => new ListTuple<T>(tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4, tuple.Item5);
        public static implicit operator ListTuple<T>((T, T, T, T, T, T) tuple) => new ListTuple<T>(tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4, tuple.Item5, tuple.Item6);
        public static implicit operator ListTuple<T>((T, T, T, T, T, T, T) tuple) => new ListTuple<T>(tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4, tuple.Item5, tuple.Item6, tuple.Item7);
        public static implicit operator ListTuple<T>(T[] array) => new ListTuple<T>(array);

        public struct Enumerator : IEnumerator<T>
        {
            private int index;
            private readonly ListTuple<T> tuple;

            public T Current => tuple.items[index];
#if CS8_OR_GREATER
            object? IEnumerator.Current => Current;
#else
            object IEnumerator.Current => Current;
#endif
            public bool MoveNext()
            {
                index++;
                return index < tuple.items.Length;
            }
            public void Reset()
            {
                index = -1;
            }
            public void Dispose() { }

            internal Enumerator(ListTuple<T> tuple)
            {
                index = -1;
                this.tuple = tuple;
            }
        }
    }
}
