namespace Nerd_STF.Extensions;

public static class Container2DExtension
{
    public static T[] Flatten<T>(this T[,] array, Int2? size = null)
    {
        size ??= GetSize(array);
        T[] res = new T[size.Value.x * size.Value.y];
        for (int x = 0; x < size.Value.x; x++) for (int y = 0; y < size.Value.y; y++)
                res[x + y * size.Value.x] = array[y, x];
        return res;
    }

    public static T[] GetColumn<T>(this T[,] array, int column, int length)
    {
        T[] res = new T[length];
        for (int i = 0; i < length; i++) res[i] = array[i, column];
        return res;
    }
    public static T[] GetRow<T>(this T[,] array, int row, int length)
    {
        T[] res = new T[length];
        for (int i = 0; i < length; i++) res[i] = array[row, i];
        return res;
    }

    public static Int2 GetSize<T>(this T[,] array)
    {
        Int2 size = Int2.Zero;

        try
        {
            while (true)
            {
                _ = array[size.x, 0];
                size.x++;
            }
        }
        catch (IndexOutOfRangeException) { }

        try
        {
            while (true)
            {
                _ = array[0, size.y];
                size.y++;
            }
        }
        catch (IndexOutOfRangeException) { }

        return size;
    }

    public static T[,] SwapDimensions<T>(this T[,] array, Int2? size = null)
    {
        size ??= GetSize(array);
        T[,] vals = new T[size.Value.y, size.Value.x];
        for (int x = 0; x < size.Value.y; x++) for (int y = 0; y < size.Value.x; y++) vals[x, y] = array[y, x];
        return vals;
    }
}
