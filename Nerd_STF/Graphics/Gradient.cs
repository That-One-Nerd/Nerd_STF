using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Nerd_STF.Graphics
{
    public class Gradient<TColor> : ICollection<KeyValuePair<double, TColor>>
        where TColor : struct, IColor<TColor>
    {
        public static readonly InterpolationFunction Lerp =
#if CS11_OR_GREATER
            (a, b, t) => TColor.Lerp(a, b, t);
#else
            // Default interpolation function for non-static generic types.
            // Convert to RGB, then cast back. Kind of rough, but that's why
            // it can be changed. For versions >= .NET 7, the proper TColor.Lerp
            // is used.
            (a, b, t) => ColorRGB.Lerp(a.AsRgb(), b.AsRgb(), t).AsColor<TColor>();
#endif
        public static readonly InterpolationFunction RoundDown = (a, b, t) => a;
        public static readonly InterpolationFunction Nearest = (a, b, t) => t >= 0.5 ? b : a;

        bool ICollection<KeyValuePair<double, TColor>>.IsReadOnly => false;
        public int Count => entries.Count;

        public InterpolationFunction Interpolation { get; set; } = Lerp;

        // I would have loved to use the SortedList object,
        // but it doesn't exist in .NET Standard 1.1. It's
        // in ALL the others (including 1.3), but whoopsies.
        protected readonly List<KeyValuePair<double, TColor>> entries;

        public Gradient() => entries = new List<KeyValuePair<double, TColor>>();
        public Gradient(TColor initial) : this()
        {
            entries.Add(new KeyValuePair<double, TColor>(0, initial));
        }
        public Gradient(params TColor[] entries) : this((IEnumerable<TColor>)entries) { }
        public Gradient(IEnumerable<TColor> entries) : this()
        {
            int count = entries.Count();
            int index = 0;
            foreach (TColor col in entries)
            {
                this.entries.Add(new KeyValuePair<double, TColor>((double)index / (count - 1), col));
                index++;
            }
        }
        public Gradient(params (double, TColor)[] entries) : this((IEnumerable<(double, TColor)>)entries) { }
        public Gradient(IEnumerable<(double, TColor)> entries) : this()
        {
            foreach ((double pos, TColor col) in entries) Add(pos, col);
        }
        public Gradient(IEnumerable<KeyValuePair<double, TColor>> entries) : this()
        {
            // We can't guarantee these are sorted.
            foreach (KeyValuePair<double, TColor> entry in entries) Add(entry);
        }

        public TColor this[double position] => Get(position);

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

        public void CopyTo(KeyValuePair<double, TColor>[] arr) => CopyTo(0, arr, 0, entries.Count);
        public void CopyTo(Span<KeyValuePair<double, TColor>> span) => CopyTo(0, span, 0, entries.Count);
        public void CopyTo(KeyValuePair<double, TColor>[] arr, int arrayIndex) => CopyTo(0, arr, arrayIndex, entries.Count);
        public void CopyTo(Span<KeyValuePair<double, TColor>> span, int offset) => CopyTo(0, span, offset, entries.Count);
        public void CopyTo(int startIndex, Span<KeyValuePair<double, TColor>> span, int offset, int length)
        {
            if (length > span.Length - offset) length = span.Length - offset;
            else if (length > entries.Count - startIndex) length = entries.Count - startIndex;
            for (int i = 0; i < length; i++) span[i + offset] = entries[i + startIndex];
        }

        public TColor Get(double position)
        {
            // Find the lower and upper bounds to interpolate between.
            KeyValuePair<double, TColor>? left = null, right = null;
            foreach (KeyValuePair<double, TColor> entry in entries)
            {
                if (entry.Key < position) left = entry;
                else if (entry.Key > position)
                {
                    right = entry;
                    break;
                }
                else return entry.Value; // Ended up exactly on a single color. Wouldntchya know!
            }

            // Now see if we're at one of the edges.
#pragma warning disable CS8629 // Stupid warning. Our null checks are exhaustive here.
            if (!left.HasValue && !right.HasValue) return default; // Empty gradient!
            else if (!left.HasValue) return right.Value.Value;     // We're after the last node.
            else if (!right.HasValue) return left.Value.Value;     // We're before the first node.
#pragma warning restore CS8629

            // Find the 't' value between left and right, then interpolate it.
            double t = (position - left.Value.Key) / (right.Value.Key - left.Value.Key);
            return Interpolation(left.Value.Value, right.Value.Value, t);
        }

        public double PositionOf(TColor color)
        {
            // Like IndexOf() but indexed as a double.
            foreach (KeyValuePair<double, TColor> entry in entries)
            {
                if (entry.Value.Equals(color)) return entry.Key;
            }
            return double.NaN;
        }

        public int Remove(Func<KeyValuePair<double, TColor>, bool> predicate, bool all = false)
        {
            int count = 0;
            for (int i = 0; i < entries.Count; i++)
            {
                if (predicate(entries[i]))
                {
                    count++;
                    entries.RemoveAt(i);
                    if (!all) break;
                    i--;
                }
            }
            return count;
        }
        public bool Remove(double entryAtPos, double tolerance = 1e-3) => Remove(entry => Math.Abs(entry.Key - entryAtPos) <= tolerance) > 0;
        public bool Remove(TColor colorEntry) => Remove(entry => entry.Value.Equals(colorEntry)) > 0;
        public int RemoveAll(TColor colorEntry) => Remove(entry => entry.Value.Equals(colorEntry), true);
        bool ICollection<KeyValuePair<double, TColor>>.Remove(KeyValuePair<double, TColor> item) => Remove(entry => entry.Equals(item)) > 0;

#if CS8_OR_GREATER
        public bool Equals(Gradient<TColor>? other)
#else
        public bool Equals(Gradient<TColor> other)
#endif
        {
            if (other is null) return false;
            else if (entries.Count != other.entries.Count) return false;
            else
            {
                for (int i = 0; i < entries.Count; i++)
                {
                    KeyValuePair<double, TColor> entry = entries[i],
                                                 otherEntry = other.entries[i];

                    if (entry.Key != otherEntry.Key || !entry.Value.Equals(otherEntry.Value)) return false;
                }
                return true;
            }
        }
#if CS8_OR_GREATER
        public override bool Equals(object? obj)
#else
        public override bool Equals(object obj)
#endif
        {
            if (obj is null) return false;
            else if (obj is Gradient<TColor> otherGradientGeneric) return Equals(otherGradientGeneric);
            else return false;
        }
        public override int GetHashCode() => entries.GetHashCode();
        public override string ToString()
        {
            StringBuilder result = new StringBuilder("{");
            for (int i = 0; i < entries.Count; i++)
            {
                KeyValuePair<double, TColor> entry = entries[i];
                result.Append($" {entry.Key:0.00}={entry.Value}");
                if (i < entries.Count - 1) result.Append(',');
            }
            return result.Append(" }").ToString();
        }
        public string ToColorString(int length = 50)
        {
            // Console codes. If you want a preview, this is a very easy way to view it.
            StringBuilder result = new StringBuilder();
            int trueLen = length * 2;
            for (int i = 0; i < length; i++)
            {
                int i1 = 2 * i, i2 = 2 * i + 1;
                ColorRGB foreColor = Get((double)i1 / (trueLen - 1)).AsRgb(),
                         backColor = Get((double)i2 / (trueLen - 1)).AsRgb();
                result.Append($"\x1b[38;2;{(int)(255 * foreColor.r)};{(int)(255 * foreColor.g)};{(int)(255 * foreColor.b)}m");
                result.Append($"\x1b[48;2;{(int)(255 * backColor.r)};{(int)(255 * backColor.g)};{(int)(255 * backColor.b)}m");
                result.Append('▌');
            }
            result.Append("\x1b[0m");
            return result.ToString();
        }

        public IEnumerator<KeyValuePair<double, TColor>> GetEnumerator() => entries.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public Fill<TColor> AsFill(int increments) => delegate (int i)
        {
            if (i < 0 || i >= increments) throw new IndexOutOfRangeException();
            else return Get((double)i / (increments - 1));
        };

        public static implicit operator Func<double, TColor>(Gradient<TColor> gradient) => gradient.Get;

        public delegate TColor InterpolationFunction(TColor a, TColor b, double t);
    }
}
