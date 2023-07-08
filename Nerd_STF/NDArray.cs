namespace Nerd_STF;

public class NDArray<T> : IEnumerable<T>, IEquatable<NDArray<T>>
{
    public int Dimensions => dimensions;
    public int[] Lengths => sizes;
    public long FullLength
    {
        get
        {
            long prod = 1;
            foreach (int size in sizes) prod *= size;
            return prod;
        }
    }

    protected readonly T[] arr;
    protected readonly int dimensions;
    protected readonly int[] sizes;

    public NDArray()
    {
        arr = Array.Empty<T>();
        dimensions = 0;
        sizes = Array.Empty<int>();
    }
    public NDArray(int dimensions)
    {
        arr = Array.Empty<T>();
        this.dimensions = dimensions;
        sizes = new int[dimensions];

        Array.Fill(sizes, 0);
    }
    public NDArray(int dimensions, int allLengths)
    {
        this.dimensions = dimensions;
        sizes = new int[dimensions];
        Array.Fill(sizes, allLengths);

        arr = new T[Mathf.Product(sizes)];
    }
    public NDArray(int[] lengths)
    {
        arr = new T[Mathf.Product(lengths)];
        dimensions = lengths.Length;
        sizes = lengths;
    }
    public NDArray(int dimensions, int[] lengths)
    {
        if (dimensions != lengths.Length) throw new InvalidSizeException("Dimension count doesn't match length count.");

        arr = new T[Mathf.Product(lengths)];
        this.dimensions = lengths.Length;
        sizes = lengths;
    }
    public NDArray(T[] items, int[] lengths)
    {
        arr = items;
        dimensions = lengths.Length;
        sizes = lengths;

        if (arr.Length != Mathf.Product(lengths)) throw new InvalidSizeException("Too many or too few items were provided.");
    }
    public NDArray(T[] items, int dimensions, int[] lengths)
    {
        if (dimensions != lengths.Length) throw new InvalidSizeException("Dimension count doesn't match length count.");

        arr = items;
        this.dimensions = lengths.Length;
        sizes = lengths;

        if (arr.Length != Mathf.Product(lengths)) throw new InvalidSizeException("Too many or too few items were provided.");
    }

    public T this[params int[] indexes]
    {
        get => arr[FlattenIndex(indexes)];
        set => arr[FlattenIndex(indexes)] = value;
    }

    private int FlattenIndex(params int[] indexes)
    {
        if (indexes.Length != sizes.Length) throw new InvalidSizeException("Too many or too few indexes were provided.");

        int ind = indexes[^1];
        Console.WriteLine($"Start at {ind}");
        for (int i = indexes.Length - 2; i >= 0; i--)
        {
            Console.WriteLine($"[{ind}] * {sizes[i]} + {indexes[i]}");
            ind = ind * sizes[i] + indexes[i];
        }
        return ind;
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    public IEnumerator<T> GetEnumerator()
    {
        foreach (T item in arr) yield return item;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        else if (obj is NDArray<T> arr) return Equals(arr);
        return false;
    }
    public bool Equals(NDArray<T>? obj) => obj is not null && arr.Equals(obj.arr);
    public override int GetHashCode() => base.GetHashCode();

    public static bool operator ==(NDArray<T> a, NDArray<T> b) => a.Equals(b);
    public static bool operator !=(NDArray<T> a, NDArray<T> b) => !a.Equals(b);
}
