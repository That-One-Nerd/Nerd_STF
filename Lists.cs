using System;
using System.Collections;
using Nerd_STF.Mathematics;

namespace Nerd_STF.Lists
{
    public class Matrix<T>
    {
        internal List<List<T>> lists;

        public Vector2 Length
        {
            get
            {
                return new(lists.Get(0).Length, lists.Length);
            }
        }
        public int LengthX
        {
            get
            {
                return lists.Get(0).Length;
            }
        }
        public int LengthY
        {
            get
            {
                return lists.Length;
            }
        }

        public static Matrix<T> Empty
        {
            get
            {
                return new Matrix<T> { lists = List<List<T>>.Empty };
            }
        }

        public Matrix()
        {
            lists = new List<List<T>>(1, new List<T>(1, default));
        }
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
            get
            {
                return Get(indexX, indexY);
            }

            set
            {
                Set(indexX, indexY, value);
            }
        }

        public void Add(Matrix<T> input, DirectionType addDir)
        {
            if (addDir == DirectionType.y)
            {
                foreach (List<T> list in input.lists)
                {
                    AddY(list);
                }
                return;
            }

            foreach (List<T> list in input.lists)
            {
                AddX(list);
            }
        }
        public void AddX()
        {
            foreach (List<T> list in lists)
            {
                list.Add();
            }
        }
        public void AddX(T input)
        {
            foreach (List<T> list in lists)
            {
                list.Add(input);
            }
        }
        public void AddX(T[] input)
        {
            foreach (T t in input)
            {
                AddX(t);
            }
        }
        public void AddX(List<T> input)
        {
            foreach (T t in input)
            {
                AddX(t);
            }
        }
        public void AddY()
        {
            lists.Add(new List<T>(lists.Get(0).Length));
        }
        public void AddY(T input)
        {
            lists.Add(new List<T>(lists.Get(0).Length, input));
        }
        public void AddY(T[] input)
        {
            if (input.Length > lists.Get(0).Length) throw new OverflowException();
            lists.Add(new List<T>(input));
        }
        public void AddY(List<T> input)
        {
            if (input.Length > lists.Get(0).Length) throw new OverflowException();
            lists.Add(input);
        }
        public bool Check(int placeX, int placeY)
        {
            return lists.Get(placeY).Get(placeX) != null;
        }
        public bool Compare(T input)
        {
            foreach (List<T> list in lists)
            {
                if (list.Compare(input)) return true;
            }

            return false;
        }
        public void Convert(T input)
        {
            foreach (List<T> list in lists)
            {
                list.Convert(input);
            }
        }
        public void Convert(T[] input)
        {
            foreach (List<T> list in lists)
            {
                list.Convert(input);
            }
        }
        public void Convert(List<T> input)
        {
            foreach (List<T> list in lists)
            {
                list.Convert(input);
            }
        }
        public void Convert(Matrix<T> input)
        {
            lists = input.lists;
        }
        public int Count()
        {
            int returned = 0;

            foreach (List<T> list in lists)
            {
                returned += list.Count();
            }

            return returned;
        }
        public int Count(DirectionType type)
        {
            if (type == DirectionType.y) return LengthY;
            return LengthX;
        }
        public Vector2 CountXY()
        {
            return Length;
        }
        public T Get(int placeX, int placeY)
        {
            return lists.Get(placeY).Get(placeX);
        }
        public void Get(int placeX, int placeY, out T output)
        {
            output = Get(placeX, placeY);
        }
        public List<T> GetAll()
        {
            List<T> returned = new();

            foreach (List<T> list in lists)
            {
                returned.Add(list);
            }

            return returned;
        }
        public void GetAll(out List<T> output)
        {
            List<T> returned = new();

            foreach (List<T> list in lists)
            {
                returned.Add(list);
            }

            output = returned;
        }
        public IEnumerator GetEnumerator()
        {
            foreach (List<T> list in lists)
            {
                foreach (T t in list)
                {
                    yield return t;
                }
            }
        }
        public void Remove(int placeX, int placeY)
        {
            lists.Get(placeY).Remove(placeX);
        }
        public void Set(int placeX, int placeY, T input)
        {
            lists.Get(placeY).Set(placeX, input);
        }
        public void SetAll(T input)
        {
            for (int i = 0; i < lists.Length; i++)
            {
                for (int j = 0; j < lists.Get(i).Length; j++)
                {
                    lists.Get(i).Set(j, input);
                }
            }
        }

        public static Matrix<T> AllDefault(int lengthX, int lengthY)
        {
            return new(lengthX, lengthY, default);
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
        public bool Equals(Matrix<T> other)
        {
            return GetAll() == other.GetAll();
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public override string ToString()
        {
            return lists.ToString();
        }
        public string ToString(bool showAll)
        {
            if (showAll)
            {
                string r = "";
                for (int i = 0; i < lists.Length; i++)
                {
                    for (int j = 0; j < lists.Get(i).Length; j++)
                    {
                        r += lists.Get(i).Get(j);
                        if (j != lists.Get(i).Length - 1) r += ", ";
                    }
                    if (i != lists.Length - 1) r += "\n";
                }
                return r;
            }
            else
            {
                return ToString();
            }
        }

        public static bool operator ==(Matrix<T> a, Matrix<T> b)
        {
            return a.Equals(b);
        }
        public static bool operator !=(Matrix<T> a, Matrix<T> b)
        {
            return !a.Equals(b);
        }

        public enum DirectionType
        {
            x,
            y,
        }
    }

    public class List<T>
    {
        internal T[] list;

        public int Length
        {
            get
            {
                return list.Length;
            }
        }
        public T this[int index]
        {
            get
            {
                return Get(index);
            }
            set
            {
                Set(index, value);
            }
        }

        public static List<T> Empty
        {
            get
            {
                return new List<T>(Array.Empty<T>());
            }
        }

        public List()
        {
            list = Array.Empty<T>();
        }
        public List(int length)
        {
            list = new T[length];
            for (int i = 0; i < length; i++)
            {
                list[i] = default;
            }
        }
        public List(int length, T inputAll)
        {
            list = new T[length];
            for (int i = 0; i < list.Length; i++)
            {
                list[i] = inputAll;
            }
        }
        public List(T[] input)
        {
            list = Array.Empty<T>();
            if (input != default) list = input;
        }
        public List(List<T> input)
        {
            list = Array.Empty<T>();
            if (input.list != default) list = input.list;
        }

        public void Add()
        {
            T[] before = list;

            if (before.Length == 0)
            {
                list = new T[1];
                list[0] = default;
            }
            else
            {
                list = new T[before.Length + 1];
                int place = 0;
                while (place < before.Length)
                {
                    list[place] = before[place];
                    place++;
                }
                list[place] = default;
            }
        }
        public void Add(T add)
        {
            T[] before = list;

            if (before.Length == 0)
            {
                list = new T[1];
                list[0] = add;
            }
            else
            {
                list = new T[before.Length + 1];
                int place = 0;
                while (place < before.Length)
                {
                    list[place] = before[place];
                    place++;
                }
                list[place] = add;
            }
        }
        public void Add(T[] add)
        {
            foreach (T input in add)
            {
                Add(input);
            }
        }
        public void Add(List<T> add)
        {
            Add(add.list);
        }
        public bool Check(int place)
        {
            return list[place] != null;
        }
        public bool Compare(T input)
        {
            foreach (T place in list)
            {
                if (place == null) continue;
                if (place.Equals(input)) return true;
            }

            return false;
        }
        public void Convert(T input)
        {
            for (int i = 0; i < list.Length; i++)
            {
                list[i] = input;
            }
        }
        public void Convert(T[] input)
        {
            list = input;
        }
        public void Convert(List<T> input)
        {
            Convert(input.list);
        }
        public int Count()
        {
            int returned = 0;
            foreach (T _ in list)
            {
                returned++;
            }
            return returned;
        }
        public T Get(int place)
        {
            return list[place];
        }
        public void Get(int place, out T output)
        {
            output = Get(place);
        }
        public T[] GetAll()
        {
            return list;
        }
        public IEnumerator GetEnumerator()
        {
            return list.GetEnumerator();
        }
        public void Remove(int place)
        {
            list[place] = default;
        }
        public void Remove(int place, bool shift)
        {
            list[place] = default;
            if (shift)
            {
                for (int i = place; i < list.Length - 1; i++)
                {
                    list[i] = list[i + 1];
                }
                T[] save = list;
                list = new T[save.Length - 1];
                for (int i = 0; i < save.Length - 1; i++)
                {
                    list[i] = save[i];
                }
            }
        }
        public void Set(int place, T input)
        {
            list[place] = input;
        }
        public void Set(T[] input)
        {
            list = input;
        }
        public void Set(List<T> input)
        {
            Set(input.list);
        }
        public void SetAll(T input)
        {
            for (int i = 0; i < list.Length; i++)
            {
                list[i] = input;
            }
        }

        public static List<T> AllDefault(int length)
        {
            return new(length, default);
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
        public bool Equals(List<T> other)
        {
            bool returned = true;
            if (Length == other.Length)
            {
                for (int i = 0; i < Length; i++)
                {
                    returned &= Get(i).Equals(other.Get(i));
                }
            }
            return returned;
        }
        public bool Equals(T[] other)
        {
            bool returned = true;
            if (Length == other.Length)
            {
                for (int i = 0; i < Length; i++)
                {
                    returned &= Get(i).Equals(other[i]);
                }
            }
            return returned;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public override string ToString()
        {
            return list.ToString();
        }
        public string ToString(bool showAll)
        {
            if (showAll)
            {
                string r = "";
                for (int i = 0; i < list.Length; i++)
                {
                    r += list[i].ToString();
                    if (i != list.Length - 1) r += "\n";
                }
                return r;
            }
            else
            {
                return ToString();
            }
        }

        public static List<T> operator +(List<T> a, List<T> b)
        {
            a.Add(b);
            return a;
        }
        public static List<T> operator +(List<T> a, T[] b)
        {
            a.Add(b);
            return a;
        }
        public static List<T> operator +(T[] a, List<T> b)
        {
            List<T> returned = new(a);
            returned.Add(b);
            return returned;
        }
        public static List<T> operator +(List<T> a, int add)
        {
            List<T> returned = new(a.Length + add);
            int i = 0;
            while (i < a.Length)
            {
                returned.Set(i, a.Get(i));
                i++;
            }
            while (i < returned.Length)
            {
                returned.Set(i, default);
            }
            return returned;
        }
        public static List<T> operator +(List<T> a, T b)
        {
            a.Add(b);

            return a;
        }
        public static List<T> operator +(T a, List<T> b)
        {
            b.Add(a);

            return b;
        }
        public static List<T> operator -(List<T> a, int remove)
        {
            List<T> returned = new(a.Length - remove);
            for (int i = 0; i < returned.Length; i++)
            {
                returned.Set(i, a.Get(i));
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
        public static List<T> operator *(List<T> a, int multiplier)
        {
            List<T> returned = new(a.Length * multiplier);
            int i = 0;
            while (i < a.Length)
            {
                returned.Set(i, a.Get(i));
                i++;
            }
            while (i < returned.Length)
            {
                returned.Set(i, default);
            }
            return returned;
        }
        public static bool operator ==(List<T> a, List<T> b)
        {
            return a.Equals(b);
        }
        public static bool operator ==(List<T> a, T[] b)
        {
            return a.Equals(b);
        }
        public static bool operator ==(T[] a, List<T> b)
        {
            return b.Equals(a);
        }
        public static bool operator !=(List<T> a, List<T> b)
        {
            return !a.Equals(b);
        }
        public static bool operator !=(List<T> a, T[] b)
        {
            return !a.Equals(b);
        }
        public static bool operator !=(T[] a, List<T> b)
        {
            return !b.Equals(a);
        }

        public static explicit operator T[](List<T> list)
        {
            return list.list;
        }
        public static explicit operator List<T>(T[] list)
        {
            return new List<T>(list);
        }
    }
}