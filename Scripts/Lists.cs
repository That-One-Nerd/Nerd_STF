using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Nerd_STF.Mathematics;

namespace Nerd_STF.Lists
{
    [Serializable]
    public class List<T> : IEnumerable, IEnumerable<T>
    {
        public static List<T> Empty => new();

        internal T[] array;

        public int this[T item]
        {
            get => FindIndex(item);
            set => Insert(value, item);
        }
        public T this[int index]
        {
            get => array[index]; 
            set => array[index] = value;
        }

        public List<T> Duplicate => new(array);
        public bool IsEmpty => array == Array.Empty<T>();
        public bool IsNull => array == null;
        public bool IsNullOrEmpty => IsNull || IsEmpty;
        public int Length => array.Length;

        public List() => array = Array.Empty<T>();
        public List(params T[] items) => array = items;
        public List(int length) => array = new T[length];
        public List(int length, T itemAll)
        {
            array = new T[length];
            for (int i = 0; i < array.Length; i++) array[i] = itemAll;
        }
        public List(IEnumerable items)
        {
            int length = 0;
            foreach (object _ in items) length++;
            array = new T[length];

            AddRange(items);
        }
        public List(IEnumerable<T> items)
        {
            int length = 0;
            foreach (object _ in items) length++;
            if (length == 0) array = Array.Empty<T>();

            AddRange(items);
        }
        public List(List<T> list) => array = list.array;

        public void Add() => Add(default);
        public void Add(T item)
        {
            if (array == null || array == Array.Empty<T>()) array = new T[1] { item };
            else
            {
                T[] old = array;
                array = new T[old.Length + 1];
                for (int i = 0; i < old.Length; i++) array[i] = old[i];
                array[old.Length] = item;
            }
        }
        public void AddRange(IEnumerable items) { foreach (T t in items) Add(t); }
        public void AddRange(IEnumerable<T> items) { foreach (T t in items) Add(t); }
        public void AddRange(List<T> items) { foreach (T t in items) Add(t); }
        public void AddRange(params T[] items) { foreach (T t in items) Add(t); }
        public bool Any(Predicate<T> predicate)
        {
            foreach (T t in array) if (predicate.Invoke(t)) return true;
            return false;
        }
        public bool Any(T match)
        {
            foreach (T t in array) if (t.Equals(match)) return true;
            return false;
        }
        public void Clear(bool resetSize = false)
        {
            if (resetSize) array = Array.Empty<T>();
            else { for (int i = 0; i < array.Length; i++) array[i] = default; }
        }
        public bool Contains(Predicate<T> predicate)
        {
            foreach (T t in array) if (predicate.Invoke(t)) return true;
            return false;
        }
        public bool Contains(T match)
        {
            foreach (T t in array) if (t.Equals(match)) return true;
            return false;
        }
        public int Count()
        {
            int r = 0;
            foreach (T _ in array) r++;
            return r;
        }
        public int Count(Predicate<T> predicate)
        {
            if (!Contains(predicate)) return 0;
            int r = 0;
            foreach (T t in array) if (predicate.Invoke(t)) r++;
            return r;
        }
        public int Count(T match)
        {
            if (!Contains(match)) return 0;
            int r = 0;
            foreach (T t in array) if (t.Equals(match)) r++;
            return r;
        }
        public void Fill(T item) { for (int i = 0; i < Length; i++) array[i] = item; }
        public T Find(Predicate<T> predicate)
        {
            foreach (T t in array) if (predicate.Invoke(t)) return t;
            throw new Exception("Parameter " + nameof(predicate) + " does not exist in the list.");
        }
        public T Find(T match)
        {
            foreach (T t in array) if (t.Equals(match)) return t;
            throw new Exception("Parameter " + nameof(match) + " does not exist in the list.");
        }
        public T Find(Predicate<T> predicate, int start)
        {
            for (int i = start; i < array.Length; i++) if (predicate.Invoke(array[i])) return array[i];
            throw new Exception("Parameter " + nameof(predicate) + " does not exist in the list.");
        }
        public T Find(T match, int start)
        {
            for (int i = start; i < array.Length; i++) if (array[i].Equals(match)) return array[i];
            throw new Exception("Parameter " + nameof(match) + " does not exist in the list.");
        }
        public T Find(Predicate<T> predicate, int start, int max)
        {
            for (int i = start; i <= max; i++) if (predicate.Invoke(array[i])) return array[i];
            throw new Exception("Parameter " + nameof(predicate) + " does not exist in the list.");
        }
        public T Find(T match, int start, int max)
        {
            for (int i = start; i <= max; i++) if (array[i].Equals(match)) return array[i];
            throw new Exception("Parameter " + nameof(match) + " does not exist in the list.");
        }
        public T FindOrDefault(Predicate<T> predicate)
        {
            foreach (T t in array) if (predicate.Invoke(t)) return t;
            return default;
        }
        public T FindOrDefault(T match)
        {
            foreach (T t in array) if (t.Equals(match)) return t;
            return default;
        }
        public T FindOrDefault(Predicate<T> predicate, int start)
        {
            for (int i = start; i < array.Length; i++) if (predicate.Invoke(array[i])) return array[i];
            return default;
        }
        public T FindOrDefault(T match, int start)
        {
            for (int i = start; i < array.Length; i++) if (array[i].Equals(match)) return array[i];
            return default;
        }
        public T FindOrDefault(Predicate<T> predicate, int start, int max)
        {
            for (int i = start; i <= max; i++) if (predicate.Invoke(array[i])) return array[i];
            return default;
        }
        public T FindOrDefault(T match, int start, int max)
        {
            for (int i = start; i <= max; i++) if (array[i].Equals(match)) return array[i];
            return default;
        }
        public List<T> FindAll(Predicate<T> predicate)
        {
            List<T> r = new();
            foreach (T t in array) if (predicate.Invoke(t)) r.Add(t);
            return r;
        }
        public List<T> FindAll(T match)
        {
            List<T> r = new();
            foreach (T t in array) if (t.Equals(match)) r.Add(t);
            return r;
        }
        public List<T> FindAll(Predicate<T> predicate, int start)
        {
            List<T> r = new();
            for (int i = start; i < array.Length; i++) if (predicate.Invoke(array[i])) r.Add(array[i]);
            return r;
        }
        public List<T> FindAll(T match, int start)
        {
            List<T> r = new();
            for (int i = start; i < array.Length; i++) if (array[i].Equals(match)) r.Add(array[i]);
            return r;
        }
        public List<T> FindAll(Predicate<T> predicate, int start, int max)
        {
            List<T> r = new();
            for (int i = start; i <= max; i++) if (predicate.Invoke(array[i])) r.Add(array[i]);
            return r;
        }
        public List<T> FindAll(T match, int start, int max)
        {
            List<T> r = new();
            for (int i = start; i <= max; i++) if (array[i].Equals(match)) r.Add(array[i]);
            return r;
        }
        public T FindLast(Predicate<T> predicate)
        {
            for (int i = array.Length - 1; i >= 0; i--) if (predicate.Invoke(array[i])) return array[i];
            throw new Exception("Parameter " + nameof(predicate) + " does not exist in the list.");
        }
        public T FindLast(T match)
        {
            for (int i = array.Length - 1; i >= 0; i--) if (array[i].Equals(match)) return array[i];
            throw new Exception("Parameter " + nameof(match) + " does not exist in the list.");
        }
        public T FindLast(Predicate<T> predicate, int start)
        {
            for (int i = array.Length - 1; i >= start; i--) if (predicate.Invoke(array[i])) return array[i];
            throw new Exception("Parameter " + nameof(predicate) + " does not exist in the list.");
        }
        public T FindLast(T match, int start)
        {
            for (int i = array.Length - 1; i >= start; i--) if (array[i].Equals(match)) return array[i];
            throw new Exception("Parameter " + nameof(match) + " does not exist in the list.");
        }
        public T FindLast(Predicate<T> predicate, int start, int max)
        {
            for (int i = max; i >= start; i--) if (predicate.Invoke(array[i])) return array[i];
            throw new Exception("Parameter " + nameof(predicate) + " does not exist in the list.");
        }
        public T FindLast(T match, int start, int max)
        {
            for (int i = max; i >= start; i--) if (array[i].Equals(match)) return array[i];
            throw new Exception("Parameter " + nameof(match) + " does not exist in the list.");
        }
        public T FindLastOrDefault(Predicate<T> predicate)
        {
            for (int i = array.Length - 1; i >= 0; i--) if (predicate.Invoke(array[i])) return array[i];
            return default;
        }
        public T FindLastOrDefault(T match)
        {
            for (int i = array.Length - 1; i >= 0; i--) if (array[i].Equals(match)) return array[i];
            return default;
        }
        public T FindLastOrDefault(Predicate<T> predicate, int start)
        {
            for (int i = array.Length - 1; i >= start; i--) if (predicate.Invoke(array[i])) return array[i];
            return default;
        }
        public T FindLastOrDefault(T match, int start)
        {
            for (int i = array.Length - 1; i >= start; i--) if (array[i].Equals(match)) return array[i];
            return default;
        }
        public T FindLastOrDefault(Predicate<T> predicate, int start, int max)
        {
            for (int i = max; i >= start; i--) if (predicate.Invoke(array[i])) return array[i];
            return default;
        }
        public T FindLastOrDefault(T match, int start, int max)
        {
            for (int i = max; i >= start; i--) if (array[i].Equals(match)) return array[i];
            return default;
        }
        public int FindIndex(Predicate<T> predicate)
        {
            for (int i = 0; i < array.Length; i++) if (predicate.Invoke(array[i])) return i;
            return -1;
        }
        public int FindIndex(T match)
        {
            for (int i = 0; i < array.Length; i++) if (array[i].Equals(match)) return i;
            return -1;
        }
        public int FindIndex(Predicate<T> predicate, int start)
        {
            for (int i = start; i < array.Length; i++) if (predicate.Invoke(array[i])) return i;
            return -1;
        }
        public int FindIndex(T match, int start)
        {
            for (int i = start; i < array.Length; i++) if (array[i].Equals(match)) return i;
            return -1;
        }
        public int FindIndex(Predicate<T> predicate, int start, int max)
        {
            for (int i = start; i <= max; i++) if (predicate.Invoke(array[i])) return i;
            return -1;
        }
        public int FindIndex(T match, int start, int max)
        {
            for (int i = start; i <= max; i++) if (array[i].Equals(match)) return i;
            return -1;
        }
        public List<int> FindAllIndex()
        {
            List<int> ret = new();
            for (int i = 0; i < array.Length; i++) ret.Add(i);
            return ret;
        }
        public List<int> FindAllIndex(Predicate<T> predicate)
        {
            List<int> r = new();
            for (int i = 0; i < array.Length; i++) if (predicate.Invoke(array[i])) r.Add(i);
            return r;
        }
        public List<int> FindAllIndex(T match)
        {
            List<int> r = new();
            for (int i = 0; i < array.Length; i++) if (array[i].Equals(match)) r.Add(i);
            return r;
        }
        public List<int> FindAllIndex(int start)
        {
            List<int> ret = new();
            for (int i = start; i < array.Length; i++) ret.Add(i);
            return ret;
        }
        public List<int> FindAllIndex(Predicate<T> predicate, int start)
        {
            List<int> r = new();
            for (int i = start; i < array.Length; i++) if (predicate.Invoke(array[i])) r.Add(i);
            return r;
        }
        public List<int> FindAllIndex(T match, int start)
        {
            List<int> r = new();
            for (int i = start; i < array.Length; i++) if (array[i].Equals(match)) r.Add(i);
            return r;
        }
        public List<int> FindAllIndex(int start, int max)
        {
            List<int> ret = new();
            for (int i = start; i <= max; i++) ret.Add(i);
            return ret;
        }
        public List<int> FindAllIndex(Predicate<T> predicate, int start, int max)
        {
            List<int> r = new();
            for (int i = start; i <= max; i++) if (predicate.Invoke(array[i])) r.Add(i);
            return r;
        }
        public List<int> FindAllIndex(T match, int start, int max)
        {
            List<int> r = new();
            for (int i = start; i <= max; i++) if (array[i].Equals(match)) r.Add(i);
            return r;
        }
        public int FindLastIndex(Predicate<T> predicate)
        {
            for (int i = array.Length - 1; i >= 0; i--) if (predicate.Invoke(array[i])) return i;
            return -1;
        }
        public int FindLastIndex(T match)
        {
            for (int i = array.Length - 1; i >= 0; i--) if (array[i].Equals(match)) return i;
            return -1;
        }
        public int FindLastIndex(Predicate<T> predicate, int start)
        {
            for (int i = array.Length - 1; i >= start; i--) if (predicate.Invoke(array[i])) return i;
            return -1;
        }
        public int FindLastIndex(T match, int start)
        {
            for (int i = array.Length - 1; i >= start; i--) if (array[i].Equals(match)) return i;
            return -1;
        }
        public int FindLastIndex(Predicate<T> predicate, int start, int max)
        {
            for (int i = max; i >= start; i--) if (predicate.Invoke(array[i])) return i;
            return -1;
        }
        public int FindLastIndex(T match, int start, int max)
        {
            for (int i = max; i >= start; i--) if (array[i].Equals(match)) return i;
            return -1;
        }
        public IEnumerator GetEnumerator() => array.GetEnumerator();
        IEnumerator<T> IEnumerable<T>.GetEnumerator() => (IEnumerator<T>)array.GetEnumerator();
        public List<T> GetRange(int start, int max)
        {
            List<T> r = new();
            for (int i = start; i <= max; i++) r.Add(array[i]);
            return r;
        }
        public void Insert(int index, T item)
        {
            T[] old = array;
            array = new T[old.Length + 1];
            for (int i = 0; i < index; i++) array[i] = old[i];
            array[index] = item;
            for (int i = index + 1; i < array.Length; i++) array[i] = old[i - 1];
        }
        public void InsertRange(int index, IEnumerable<T> items)
        {
            List<T> list = new(items);

            T[] old = array;
            array = new T[old.Length + list.Length];
            for (int i = 0; i < index; i++) array[i] = old[i];
            for (int i = 0; i < list.Length; i++) array[index + i] = list[i];
            for (int i = index + list.Length; i < array.Length; i++) array[i] = old[i - list.Length];
        }
        public bool MatchesAll(Predicate<T> predicate) => FindAll(predicate).array == array;
        public bool MatchesAll(T match) => FindAll(match).array == array;
        public void Randomize()
        {
            List<T> newL = new();
            List<int> possibleIndexes = FindAllIndex();
            for (int i = 0; i < possibleIndexes.Length; i++)
            {
                int index = possibleIndexes[new Random().Next(0, possibleIndexes.Length)];
                newL.Add(array[index]);
                possibleIndexes.Remove(x => x == index);
            }
            array = newL.ToArray();
        }
        public void Remove(Predicate<T> predicate) => Remove(FindIndex(predicate));
        public void Remove(T item) => Remove(FindIndex(item));
        public void Remove(int index)
        {
            List<T> newList = new();
            for (int i = 0; i < array.Length; i++) if (i != index) newList.Add(array[i]);
            array = newList.array;
        }
        public void RemoveAll(Predicate<T> predicate) { foreach (int i in FindAllIndex(predicate)) Remove(i); }
        public void RemoveAll(T match) { foreach (int i in FindAllIndex(match)) Remove(i); }
        public void RemoveLast(Predicate<T> predicate) => Remove(FindLastIndex(predicate));
        public void RemoveLast(T item) => Remove(FindLastIndex(item));
        public void RemoveRange(int index, int max)
        {
            List<T> newList = new();
            for (int i = 0; i < array.Length; i++) if (i < index || i > max) newList.Add(array[i]);
            array = newList.array;
        }
        public void Reverse()
        {
            T[] old = array;
            array = new T[old.Length];

            for (int i = old.Length - 1; i >= 0; i--) array[i] = old[i];
        }
        public void Shuffle() => Randomize();
        public T[] ToArray() => array;
        public ReadOnlyList<T> ToReadOnly() { return new ReadOnlyList<T>(array); }
        public ReadOnlyCollection<T> ToSystemReadOnly() { return new ReadOnlyCollection<T>(array); }
        public System.Collections.Generic.List<T> ToSystemList() { return new System.Collections.Generic.List<T>(array); }

        public override bool Equals(object obj) => base.Equals(obj);
        public bool Equals(T[] other)
        {
            bool returned = true;
            if (Length == other.Length)
            {
                for (int i = 0; i < Length; i++)
                {
                    returned &= array[i].Equals(other[i]);
                }
            }
            return returned;
        }
        public bool Equals(List<T> list)
        {
            if (Length != list.Length) return false;
            bool equal = true;
            for (int i = 0; i < Length; i++) equal &= array[i].Equals(list[i]);
            return equal;
        }
        public bool Equals(IEnumerable<T> list) => Equals(new List<T>(list));
        public override int GetHashCode() => base.GetHashCode();
        public override string ToString() => ToString(false);
        public string ToString(bool showAll = false)
        {
            string ret = "List of " + Length + " Elements (" + typeof(T).ToString() + ")";
            if (showAll) for (int i = 0; i < Length; i++) ret += "\n" + i + ": " + array[i];
            return ret;
        }

        public static List<T> operator +(List<T> a, T b)
        {
            a.Add(b);
            return a;
        }
        public static List<T> operator +(List<T> a, IEnumerable<T> b)
        {
            a.AddRange(b);
            return a;
        }
        public static List<T> operator +(List<T> a, List<T> b)
        {
            a.AddRange(b);
            return a;
        }
        public static List<T> operator +(List<T> a, T[] b)
        {
            a.AddRange(b);
            return a;
        }
        public static List<T> operator -(List<T> a, int remove)
        {
            List<T> returned = new(a.Length - remove);
            for (int i = 0; i < returned.Length; i++)
            {
                returned[i] = a[i];
            }
            return returned;
        }
        public static List<T> operator -(List<T> a, int[] removes)
        {
            foreach (int remove in removes)
            {
                a.Remove(remove);
            }
            return a;
        }
        public static List<T> operator -(List<T> a, List<int> removes)
        {
            foreach (int remove in removes)
            {
                a.Remove(remove);
            }
            return a;
        }
        public static List<T> operator -(List<T> a, T b)
        {
            a.Remove(b);
            return a;
        }
        public static bool operator ==(List<T> a, List<T> b) => a.Equals(b);
        public static bool operator ==(List<T> a, T[] b)
        {
            return a.Equals(b);
        }
        public static bool operator ==(T[] a, List<T> b)
        {
            return b.Equals(a);
        }
        public static bool operator !=(List<T> a, List<T> b) => !a.Equals(b);
        public static bool operator !=(List<T> a, T[] b)
        {
            return !a.Equals(b);
        }
        public static bool operator !=(T[] a, List<T> b)
        {
            return !b.Equals(a);
        }
    }

    [Serializable]
    [Obsolete("This class will be removed or heavily modified in a future release.")]
    public class Matrix<T>
    {
        internal List<List<T>> lists;

        public Vector2 Length => new(lists[0].Length, lists.Length); 
        public int LengthX => lists[0].Length; 
        public int LengthY => lists.Length; 

        public static Matrix<T> Empty => new() { lists = List<List<T>>.Empty };

        public Matrix() => lists = new List<List<T>>(1, new List<T>(1, default));
        public Matrix(int lengthX, int lengthY)
        {
            if (lengthX < 1) throw new ArgumentOutOfRangeException(nameof(lengthX), "Do not include a length of less than 1");
            if (lengthY < 1) throw new ArgumentOutOfRangeException(nameof(lengthY), "Do not include a width of less than 1");
            lists = new List<List<T>>(lengthY, new List<T>(lengthX));
        }
        public Matrix(int lengthX, int lengthY, T inputAll)
        {
            if (lengthX < 1) throw new ArgumentOutOfRangeException(nameof(lengthX), "Do not include a length of less than 1");
            if (lengthY < 1) throw new ArgumentOutOfRangeException(nameof(lengthY), "Do not include a width of less than 1");
            lists = new List<List<T>>(lengthY, new List<T>(lengthX, inputAll));
        }
        public T this[int indexX, int indexY]
        {
            get => Get(indexX, indexY);
            set => Set(indexX, indexY, value);
        }

        public void Add(Matrix<T> input, DirectionType addDir)
        {
            if (addDir == DirectionType.y)
            {
                foreach (List<T> list in input.lists) AddY(list);
                return;
            }

            foreach (List<T> list in input.lists) AddX(list);
        }
        public void AddX() { foreach (List<T> list in lists) list.Add(); }
        public void AddX(T input) { foreach (List<T> list in lists) list.Add(input); }
        public void AddX(T[] input) { foreach (T t in input) AddX(t); }
        public void AddX(List<T> input) { foreach (T t in input) AddX(t); }
        public void AddY() => lists.Add(new List<T>(lists[0].Length));
        public void AddY(T input) => lists.Add(new List<T>(lists[0].Length, input));
        public void AddY(T[] input)
        {
            if (input.Length > lists[0].Length) throw new OverflowException();
            lists.Add(new List<T>(input));
        }
        public void AddY(List<T> input)
        {
            if (input.Length > lists[0].Length) throw new OverflowException();
            lists.Add(input);
        }
        public bool Check(int placeX, int placeY) => lists[placeY][placeX] != null;
        public bool Compare(T input)
        {
            foreach (List<T> list in lists) if (list.Contains(input)) return true;

            return false;
        }
        public void Convert(T input) { for (int i = 0; i < lists.Length; i++) lists[i] = new(input); }
        public void Convert(T[] input) { for (int i = 0; i < lists.Length; i++) lists[i] = new(input); }
        public void Convert(List<T> input) { for (int i = 0; i < lists.Length; i++) lists[i] = input; }
        public void Convert(Matrix<T> input) => lists = input.lists;
        public int Count()
        {
            int returned = 0;

            foreach (List<T> list in lists) returned += list.Count();

            return returned;
        }
        public int Count(DirectionType type)
        {
            if (type == DirectionType.y) return LengthY;
            return LengthX;
        }
        public Vector2 CountXY() => Length;
        public T Get(int placeX, int placeY) => lists[placeY][placeX];
        public void Get(int placeX, int placeY, out T output) => output = Get(placeX, placeY);
        public List<T> GetAll()
        {
            List<T> returned = new();

            foreach (List<T> list in lists) returned.AddRange(list);

            return returned;
        }
        public void GetAll(out List<T> output)
        {
            List<T> returned = new();

            foreach (List<T> list in lists) returned.AddRange(list);

            output = returned;
        }
        public IEnumerator GetEnumerator() { foreach (List<T> list in lists) foreach (T t in list) yield return t; }
        public void Remove(int placeX, int placeY) => lists[placeY].Remove(placeX);
        public void Set(int placeX, int placeY, T input) => lists[placeY][placeX] = input;
        public void SetAll(T input) { for (int i = 0; i < lists.Length; i++) for (int j = 0; j < lists[i].Length; j++) lists[i][j] = input; }

        public static Matrix<T> AllDefault(int lengthX, int lengthY) => new(lengthX, lengthY, default);

        public override bool Equals(object obj) => base.Equals(obj);
        public bool Equals(Matrix<T> other) => GetAll() == other.GetAll();
        public override int GetHashCode() => base.GetHashCode();
        public override string ToString() => lists.ToString();
        public string ToString(bool showAll)
        {
            if (showAll)
            {
                string r = "";
                for (int i = 0; i < lists.Length; i++)
                {
                    for (int j = 0; j < lists[i].Length; j++)
                    {
                        r += lists[i][j];
                        if (j != lists[i].Length - 1) r += ", ";
                    }
                    if (i != lists.Length - 1) r += "\n";
                }
                return r;
            }
            else return ToString();
        }

        public static bool operator ==(Matrix<T> a, Matrix<T> b) => a.Equals(b);
        public static bool operator !=(Matrix<T> a, Matrix<T> b) => !a.Equals(b);

        public enum DirectionType
        {
            x,
            y,
        }
    }

    [Serializable]
    public class ReadOnlyList<T> : IEnumerable, IEnumerable<T>
    {
        public static ReadOnlyList<T> Empty => new();

        internal T[] array;

        public T this[int index] => array[index];

        public List<T> Duplicate => new(array);
        public bool IsEmpty => array == Array.Empty<T>();
        public bool IsNull => array == null;
        public bool IsNullOrEmpty => IsNull || IsEmpty;
        public int Length => array.Length;

        public ReadOnlyList() => array = Array.Empty<T>();
        public ReadOnlyList(params T[] items) => array = items;
        public ReadOnlyList(int length) => array = new T[length];
        public ReadOnlyList(int length, T itemAll)
        {
            array = new T[length];
            for (int i = 0; i < array.Length; i++) array[i] = itemAll;
        }
        public ReadOnlyList(IEnumerable<T> items) => array = new List<T>(items).ToArray();
        
        public bool Contains(Predicate<T> predicate)
        {
            foreach (T t in array) if (predicate.Invoke(t)) return true;
            return false;
        }
        public bool Contains(T match)
        {
            foreach (T t in array) if (t.Equals(match)) return true;
            return false;
        }
        public int Count()
        {
            int r = 0;
            foreach (T _ in array) r++;
            return r;
        }
        public int Count(Predicate<T> predicate)
        {
            if (!Contains(predicate)) return 0;
            int r = 0;
            foreach (T t in array) if (predicate.Invoke(t)) r++;
            return r;
        }
        public int Count(T match)
        {
            if (!Contains(match)) return 0;
            int r = 0;
            foreach (T t in array) if (t.Equals(match)) r++;
            return r;
        }
        public T Find(Predicate<T> predicate)
        {
            foreach (T t in array) if (predicate.Invoke(t)) return t;
            throw new Exception("Parameter " + nameof(predicate) + " does not exist in the list.");
        }
        public T Find(T match)
        {
            foreach (T t in array) if (t.Equals(match)) return t;
            throw new Exception("Parameter " + nameof(match) + " does not exist in the list.");
        }
        public T Find(Predicate<T> predicate, int start)
        {
            for (int i = start; i < array.Length; i++) if (predicate.Invoke(array[i])) return array[i];
            throw new Exception("Parameter " + nameof(predicate) + " does not exist in the list.");
        }
        public T Find(T match, int start)
        {
            for (int i = start; i < array.Length; i++) if (array[i].Equals(match)) return array[i];
            throw new Exception("Parameter " + nameof(match) + " does not exist in the list.");
        }
        public T Find(Predicate<T> predicate, int start, int max)
        {
            for (int i = start; i <= max; i++) if (predicate.Invoke(array[i])) return array[i];
            throw new Exception("Parameter " + nameof(predicate) + " does not exist in the list.");
        }
        public T Find(T match, int start, int max)
        {
            for (int i = start; i <= max; i++) if (array[i].Equals(match)) return array[i];
            throw new Exception("Parameter " + nameof(match) + " does not exist in the list.");
        }
        public T FindOrDefault(Predicate<T> predicate)
        {
            foreach (T t in array) if (predicate.Invoke(t)) return t;
            return default;
        }
        public T FindOrDefault(T match)
        {
            foreach (T t in array) if (t.Equals(match)) return t;
            return default;
        }
        public T FindOrDefault(Predicate<T> predicate, int start)
        {
            for (int i = start; i < array.Length; i++) if (predicate.Invoke(array[i])) return array[i];
            return default;
        }
        public T FindOrDefault(T match, int start)
        {
            for (int i = start; i < array.Length; i++) if (array[i].Equals(match)) return array[i];
            return default;
        }
        public T FindOrDefault(Predicate<T> predicate, int start, int max)
        {
            for (int i = start; i <= max; i++) if (predicate.Invoke(array[i])) return array[i];
            return default;
        }
        public T FindOrDefault(T match, int start, int max)
        {
            for (int i = start; i <= max; i++) if (array[i].Equals(match)) return array[i];
            return default;
        }
        public List<T> FindAll(Predicate<T> predicate)
        {
            List<T> r = new();
            foreach (T t in array) if (predicate.Invoke(t)) r.Add(t);
            return r;
        }
        public List<T> FindAll(T match)
        {
            List<T> r = new();
            foreach (T t in array) if (t.Equals(match)) r.Add(t);
            return r;
        }
        public List<T> FindAll(Predicate<T> predicate, int start)
        {
            List<T> r = new();
            for (int i = start; i < array.Length; i++) if (predicate.Invoke(array[i])) r.Add(array[i]);
            return r;
        }
        public List<T> FindAll(T match, int start)
        {
            List<T> r = new();
            for (int i = start; i < array.Length; i++) if (array[i].Equals(match)) r.Add(array[i]);
            return r;
        }
        public List<T> FindAll(Predicate<T> predicate, int start, int max)
        {
            List<T> r = new();
            for (int i = start; i <= max; i++) if (predicate.Invoke(array[i])) r.Add(array[i]);
            return r;
        }
        public List<T> FindAll(T match, int start, int max)
        {
            List<T> r = new();
            for (int i = start; i <= max; i++) if (array[i].Equals(match)) r.Add(array[i]);
            return r;
        }
        public T FindLast(Predicate<T> predicate)
        {
            for (int i = array.Length - 1; i >= 0; i--) if (predicate.Invoke(array[i])) return array[i];
            throw new Exception("Parameter " + nameof(predicate) + " does not exist in the list.");
        }
        public T FindLast(T match)
        {
            for (int i = array.Length - 1; i >= 0; i--) if (array[i].Equals(match)) return array[i];
            throw new Exception("Parameter " + nameof(match) + " does not exist in the list.");
        }
        public T FindLast(Predicate<T> predicate, int start)
        {
            for (int i = array.Length - 1; i >= start; i--) if (predicate.Invoke(array[i])) return array[i];
            throw new Exception("Parameter " + nameof(predicate) + " does not exist in the list.");
        }
        public T FindLast(T match, int start)
        {
            for (int i = array.Length - 1; i >= start; i--) if (array[i].Equals(match)) return array[i];
            throw new Exception("Parameter " + nameof(match) + " does not exist in the list.");
        }
        public T FindLast(Predicate<T> predicate, int start, int max)
        {
            for (int i = max; i >= start; i--) if (predicate.Invoke(array[i])) return array[i];
            throw new Exception("Parameter " + nameof(predicate) + " does not exist in the list.");
        }
        public T FindLast(T match, int start, int max)
        {
            for (int i = max; i >= start; i--) if (array[i].Equals(match)) return array[i];
            throw new Exception("Parameter " + nameof(match) + " does not exist in the list.");
        }
        public T FindLastOrDefault(Predicate<T> predicate)
        {
            for (int i = array.Length - 1; i >= 0; i--) if (predicate.Invoke(array[i])) return array[i];
            return default;
        }
        public T FindLastOrDefault(T match)
        {
            for (int i = array.Length - 1; i >= 0; i--) if (array[i].Equals(match)) return array[i];
            return default;
        }
        public T FindLastOrDefault(Predicate<T> predicate, int start)
        {
            for (int i = array.Length - 1; i >= start; i--) if (predicate.Invoke(array[i])) return array[i];
            return default;
        }
        public T FindLastOrDefault(T match, int start)
        {
            for (int i = array.Length - 1; i >= start; i--) if (array[i].Equals(match)) return array[i];
            return default;
        }
        public T FindLastOrDefault(Predicate<T> predicate, int start, int max)
        {
            for (int i = max; i >= start; i--) if (predicate.Invoke(array[i])) return array[i];
            return default;
        }
        public T FindLastOrDefault(T match, int start, int max)
        {
            for (int i = max; i >= start; i--) if (array[i].Equals(match)) return array[i];
            return default;
        }
        public int FindIndex(Predicate<T> predicate)
        {
            for (int i = 0; i < array.Length; i++) if (predicate.Invoke(array[i])) return i;
            return -1;
        }
        public int FindIndex(T match)
        {
            for (int i = 0; i < array.Length; i++) if (array[i].Equals(match)) return i;
            return -1;
        }
        public int FindIndex(Predicate<T> predicate, int start)
        {
            for (int i = start; i < array.Length; i++) if (predicate.Invoke(array[i])) return i;
            return -1;
        }
        public int FindIndex(T match, int start)
        {
            for (int i = start; i < array.Length; i++) if (array[i].Equals(match)) return i;
            return -1;
        }
        public int FindIndex(Predicate<T> predicate, int start, int max)
        {
            for (int i = start; i <= max; i++) if (predicate.Invoke(array[i])) return i;
            return -1;
        }
        public int FindIndex(T match, int start, int max)
        {
            for (int i = start; i <= max; i++) if (array[i].Equals(match)) return i;
            return -1;
        }
        public List<int> FindAllIndex()
        {
            List<int> ret = new();
            for (int i = 0; i < array.Length; i++) ret.Add(i);
            return ret;
        }
        public List<int> FindAllIndex(Predicate<T> predicate)
        {
            List<int> r = new();
            for (int i = 0; i < array.Length; i++) if (predicate.Invoke(array[i])) r.Add(i);
            return r;
        }
        public List<int> FindAllIndex(T match)
        {
            List<int> r = new();
            for (int i = 0; i < array.Length; i++) if (array[i].Equals(match)) r.Add(i);
            return r;
        }
        public List<int> FindAllIndex(int start)
        {
            List<int> ret = new();
            for (int i = start; i < array.Length; i++) ret.Add(i);
            return ret;
        }
        public List<int> FindAllIndex(Predicate<T> predicate, int start)
        {
            List<int> r = new();
            for (int i = start; i < array.Length; i++) if (predicate.Invoke(array[i])) r.Add(i);
            return r;
        }
        public List<int> FindAllIndex(T match, int start)
        {
            List<int> r = new();
            for (int i = start; i < array.Length; i++) if (array[i].Equals(match)) r.Add(i);
            return r;
        }
        public List<int> FindAllIndex(int start, int max)
        {
            List<int> ret = new();
            for (int i = start; i <= max; i++) ret.Add(i);
            return ret;
        }
        public List<int> FindAllIndex(Predicate<T> predicate, int start, int max)
        {
            List<int> r = new();
            for (int i = start; i <= max; i++) if (predicate.Invoke(array[i])) r.Add(i);
            return r;
        }
        public List<int> FindAllIndex(T match, int start, int max)
        {
            List<int> r = new();
            for (int i = start; i <= max; i++) if (array[i].Equals(match)) r.Add(i);
            return r;
        }
        public int FindLastIndex(Predicate<T> predicate)
        {
            for (int i = array.Length - 1; i >= 0; i--) if (predicate.Invoke(array[i])) return i;
            return -1;
        }
        public int FindLastIndex(T match)
        {
            for (int i = array.Length - 1; i >= 0; i--) if (array[i].Equals(match)) return i;
            return -1;
        }
        public int FindLastIndex(Predicate<T> predicate, int start)
        {
            for (int i = array.Length - 1; i >= start; i--) if (predicate.Invoke(array[i])) return i;
            return -1;
        }
        public int FindLastIndex(T match, int start)
        {
            for (int i = array.Length - 1; i >= start; i--) if (array[i].Equals(match)) return i;
            return -1;
        }
        public int FindLastIndex(Predicate<T> predicate, int start, int max)
        {
            for (int i = max; i >= start; i--) if (predicate.Invoke(array[i])) return i;
            return -1;
        }
        public int FindLastIndex(T match, int start, int max)
        {
            for (int i = max; i >= start; i--) if (array[i].Equals(match)) return i;
            return -1;
        }
        public IEnumerator GetEnumerator() => array.GetEnumerator();
        IEnumerator<T> IEnumerable<T>.GetEnumerator() => (IEnumerator<T>)array.GetEnumerator();
        public List<T> GetRange(int start, int max)
        {
            List<T> r = new();
            for (int i = start; i <= max; i++) r.Add(array[i]);
            return r;
        }
        public bool MatchesAll(Predicate<T> predicate) => FindAll(predicate).array == array;
        public bool MatchesAll(T match) => FindAll(match).array == array;
        public T[] ToArray() => array;
        public List<T> ToList() => new(array);
        public System.Collections.Generic.List<T> ToSystemList() => new(array);
        public ReadOnlyCollection<T> ToSystemReadOnly() => new(array);

        public override bool Equals(object obj) => base.Equals(obj);
        public bool Equals(T[] other)
        {
            bool returned = true;
            if (Length == other.Length)
            {
                for (int i = 0; i < Length; i++)
                {
                    returned &= array[i].Equals(other[i]);
                }
            }
            return returned;
        }
        public bool Equals(ReadOnlyList<T> list)
        {
            if (Length != list.Length) return false;
            bool equal = true;
            for (int i = 0; i < Length; i++) equal &= array[i].Equals(list[i]);
            return equal;
        }
        public bool Equals(IEnumerable<T> list) => Equals(new ReadOnlyList<T>(list));
        public override int GetHashCode() => base.GetHashCode();
        public override string ToString() => ToString(false);
        public string ToString(bool showAll = false)
        {
            string ret = "List of " + Length + " Elements (" + typeof(T).ToString() + ")";
            if (showAll) for (int i = 0; i < Length; i++) ret += "\n" + i + ": " + array[i];
            return ret;
        }

        public static bool operator ==(ReadOnlyList<T> a, ReadOnlyList<T> b) => a.Equals(b);
        public static bool operator ==(ReadOnlyList<T> a, T[] b)
        {
            return a.Equals(b);
        }
        public static bool operator ==(T[] a, ReadOnlyList<T> b)
        {
            return b.Equals(a);
        }
        public static bool operator ==(IEnumerable<T> a, ReadOnlyList<T> b) => a.Equals(b);
        public static bool operator ==(ReadOnlyList<T> a, IEnumerable<T> b) => a.Equals(b);
        public static bool operator !=(ReadOnlyList<T> a, ReadOnlyList<T> b) => !a.Equals(b);
        public static bool operator !=(ReadOnlyList<T> a, T[] b)
        {
            return !a.Equals(b);
        }
        public static bool operator !=(T[] a, ReadOnlyList<T> b)
        {
            return !b.Equals(a);
        }
        public static bool operator !=(IEnumerable<T> a, ReadOnlyList<T> b) => !a.Equals(b);
        public static bool operator !=(ReadOnlyList<T> a, IEnumerable<T> b) => !a.Equals(b);
    }
}