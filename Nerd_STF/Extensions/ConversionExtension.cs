namespace Nerd_STF.Extensions;

public static class ConversionExtension
{
    public static Fill<T> ToFill<T>(this T[] arr) => i => arr[i];
    public static Fill<T> ToFill<T>(this T[,] arr, Int2? size) => arr.Flatten(size).ToFill();
    public static Fill2d<T> ToFill2D<T>(this T[,] arr) => (x, y) => arr[x, y];
}
