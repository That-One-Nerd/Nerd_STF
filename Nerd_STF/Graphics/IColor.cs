namespace Nerd_STF.Graphics;

public interface IColor : ICloneable, IEquatable<IColorFloat?>, IEquatable<IColorByte?>
{
    public RGBA ToRGBA();
    public CMYKA ToCMYKA();
    public HSVA ToHSVA();

    public RGBAByte ToRGBAByte();
    public CMYKAByte ToCMYKAByte();
    public HSVAByte ToHSVAByte();
}
