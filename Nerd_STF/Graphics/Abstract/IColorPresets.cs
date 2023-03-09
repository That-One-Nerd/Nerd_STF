namespace Nerd_STF.Graphics.Abstract;

public interface IColorPresets<T> where T : IColorPresets<T>
{
    public static abstract T Black { get; }
    public static abstract T Blue { get; }
    public static abstract T Clear { get; }
    public static abstract T Cyan { get; }
    public static abstract T Gray { get; }
    public static abstract T Green { get; }
    public static abstract T Magenta { get; }
    public static abstract T Orange { get; }
    public static abstract T Purple { get; }
    public static abstract T Red { get; }
    public static abstract T White { get; }
    public static abstract T Yellow { get; }
}
