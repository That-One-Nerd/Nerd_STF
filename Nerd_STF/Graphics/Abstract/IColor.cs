using System.Numerics;

namespace Nerd_STF.Graphics.Abstract;

public interface IColor : IEquatable<IColor>
{
    public CMYKA ToCMYKA();
    public HSVA ToHSVA();
    public RGBA ToRGBA();

    public CMYKAByte ToCMYKAByte();
    public HSVAByte ToHSVAByte();
    public RGBAByte ToRGBAByte();
}
public interface IColor<T> : IColor where T : IColor<T>
{
    public static abstract bool operator ==(T a, CMYKA b);
    public static abstract bool operator !=(T a, CMYKA b);
    public static abstract bool operator ==(T a, CMYKAByte b);
    public static abstract bool operator !=(T a, CMYKAByte b);
    public static abstract bool operator ==(T a, HSVA b);
    public static abstract bool operator !=(T a, HSVA b);
    public static abstract bool operator ==(T a, HSVAByte b);
    public static abstract bool operator !=(T a, HSVAByte b);
    public static abstract bool operator ==(T a, RGBA b);
    public static abstract bool operator !=(T a, RGBA b);
    public static abstract bool operator ==(T a, RGBAByte b);
    public static abstract bool operator !=(T a, RGBAByte b);
}
