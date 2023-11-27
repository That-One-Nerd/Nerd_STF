namespace Nerd_STF.Mathematics.Abstract;

/// <summary>
/// An <see langword="interface"/> that can be derived from to implement
/// absolute value functionality. This interface includes one method:
/// <list type="bullet">
///     <see cref="Absolute(T)"/>
/// </list>
/// </summary>
/// <typeparam name="T">This type.</typeparam>
public interface IAbsolute<T> where T : IAbsolute<T>
{
    /// <summary>
    /// Calculate the positive value of <typeparamref name="T"/>.
    /// I know, this isn't technically the "absolute" value but whatever.
    /// </summary>
    /// <param name="val">The value of <typeparamref name="T"/> to calculate the absolute value of.</param>
    /// <returns>The positive vlaue of <typeparamref name="T"/>.</returns>
    public static abstract T Absolute(T val);
}
