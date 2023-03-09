namespace Nerd_STF.Graphics.Abstract;

public interface IColorFloat : IColor, IGroup<float> { }
public interface IColorFloat<T> : IColor<T>, IColorFloat where T : struct, IColorFloat<T> { }
