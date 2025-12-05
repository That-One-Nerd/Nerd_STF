using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nerd_STF.Graphics
{
    public class Gradient<TColor> : ICollection<KeyValuePair<double, TColor>>
        where TColor : struct, IColor<TColor>
    {
        bool ICollection<KeyValuePair<double, TColor>>.IsReadOnly => false;
        public int Count => entries.Count;

        // I would have loved to use the SortedList object,
        // but it doesn't exist in .NET Standard 1.1. It's
        // in ALL the others (including 1.3), but whoopsies.
        protected readonly List<KeyValuePair<double, TColor>> entries;

        public Gradient() => entries = new List<KeyValuePair<double, TColor>>();
        public Gradient(TColor initial) : this()
        {
            entries.Add(new KeyValuePair<double, TColor>(0, initial));
        }
        public Gradient(IEnumerable<TColor> entries) : this()
        {
            int count = entries.Count();
            int index = 0;
            foreach (TColor col in entries)
            {
                this.entries.Add(new KeyValuePair<double, TColor>((double)index / count, col));
                index++;
            }
        }
        public Gradient(IEnumerable<(double, TColor)> entries) : this()
        {
            foreach ((double pos, TColor col) in entries) Add(pos, col);
        }
        public Gradient(IEnumerable<KeyValuePair<double, TColor>> entries) : this()
        {
            // We can't guarantee these are sorted.
            foreach (KeyValuePair<double, TColor> entry in entries) Add(entry);
        }

        public void Add(double position, TColor color)
        {
            // Insert the entry in its specific place 
            KeyValuePair<double, TColor> entry = new KeyValuePair<double, TColor>(position, color);

            int index = 0;
            while (index < entries.Count)
            {
                if (entries[index].Key == position)
                {
                    // Exactly equal, replace.
                    entries[index] = entry;
                    return;
                }
                else if (entries[index].Key > position) break;
            }

            entries.Insert(index, entry);
        }
        public void Add(KeyValuePair<double, TColor> entry) => Add(entry.Key, entry.Value);
        public void Clear() => entries.Clear();
        public bool Contains(double entryAtPos, double tolerance = 1e-3)
        {
            foreach (KeyValuePair<double, TColor> entry in entries)
            {
                double diff = Math.Abs(entry.Key - entryAtPos);
                if (diff <= tolerance) return true;
            }
            return false;
        }
        public bool Contains(TColor color)
        {
            foreach (KeyValuePair<double, TColor> entry in entries)
            {
                if (entry.Key.Equals(color)) return true;
            }
            return false;
        }
        bool ICollection<KeyValuePair<double, TColor>>.Contains(KeyValuePair<double, TColor> item)
        {
            foreach (KeyValuePair<double, TColor> entry in entries)
            {
                if (entry.Key == item.Key &&
                    entry.Value.Equals(item.Value)) return true;
            }
            return false;
        }
        public void CopyTo(int startIndex, Span<KeyValuePair<double, TColor>> span, int offset, int length)
        {
            // TODO
        }

        // What still needs to be declared:
        // - Remove()
        // - PositionOf(TColor)
        // - Get(double) and this[double]
        //   - Which will require lerp.
        //
        // - Equals(object) and Equals(Gradient)
        // - GetHashCode()
        // - ToString()
        //
        // - ToArray()
        // - ToList()
        // - ToFill()
        //
        // ... casts

        public IEnumerator<KeyValuePair<double, TColor>> GetEnumerator() => entries.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
