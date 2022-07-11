namespace Nerd_STF.Graphics;

public interface IColor : ICloneable, IEquatable<IColor?>, IEquatable<IColorByte?>, IGroup<float>
{
    public RGBA ToRGBA();
    public CMYKA ToCMYKA();
    public HSVA ToHSVA();

    public RGBAByte ToRGBAByte();
    public CMYKAByte ToCMYKAByte();
    public HSVAByte ToHSVAByte();
}
