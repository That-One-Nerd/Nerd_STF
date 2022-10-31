namespace Nerd_STF.Extensions;

public static class Container2DExtension
{
    public static T[] Flatten<T>(this T[,] array, Int2 size)
    {
        T[] res = new T[size.x * size.y];
        for (int x = 0; x < size.x; x++) for (int y = 0; y < size.y; y++) res[x + y * size.x] = array[y, x];
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
}
