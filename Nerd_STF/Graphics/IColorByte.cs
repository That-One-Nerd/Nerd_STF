namespace Nerd_STF.Graphics;

public interface IColorByte : ICloneable, IEquatable<IColor?>, IEquatable<IColorByte?>, IGroup<byte>
{
    public RGBA ToRGBA();
    public CMYKA ToCMYKA();
    public HSVA ToHSVA();

    public RGBAByte ToRGBAByte();
    public CMYKAByte ToCMYKAByte();
    public HSVAByte ToHSVAByte();
}
