namespace Nerd_STF.Graphics;

public struct TextureConfig
{
    public (bool U, bool V) BlendUV;
    public float Boost;
    public ColorChannel Channel;
    public bool Clamp;
    public float NormalStrength;
    public Float3 Offset;
    public Float3 Scale;
    public Float3 Turbulance;

    public TextureConfig()
    {
        BlendUV = (true, true);
        Boost = 0;
        Channel = ColorChannel.Red;
        Clamp = false;
        NormalStrength = 1;
        Offset = Float3.Zero;
        Scale = Float3.One;
        Turbulance = Float3.Zero;
    }
}
