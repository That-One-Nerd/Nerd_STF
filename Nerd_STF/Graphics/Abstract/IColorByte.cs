namespace Nerd_STF.Graphics.Abstract;

public interface IColorByte : IColor, IGroup<byte>
{
    public int[] ToArrayInt();
    public Fill<int> ToFillInt();
    public List<int> ToListInt();
}
public interface IColorByte<T> : IColor<T>, IColorByte where T : struct, IColorByte<T> { }
