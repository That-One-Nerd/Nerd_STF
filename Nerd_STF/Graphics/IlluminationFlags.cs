namespace Nerd_STF.Graphics;

[Flags]
public enum IlluminationFlags : byte
{
    Color = 1,
    Ambient = 2,
    Highlight = 4,
    Reflection = 8,
    Raytrace = 16,
    Fresnel = 32,
    Refraction = 64,
    Glass = 128,
}
