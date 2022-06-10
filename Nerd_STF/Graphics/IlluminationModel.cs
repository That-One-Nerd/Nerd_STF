namespace Nerd_STF.Graphics;

public enum IlluminationModel : ushort
{
    Mode0 = IlluminationFlags.Color,
    Mode1 = IlluminationFlags.Color | IlluminationFlags.Ambient,
    Mode2 = IlluminationFlags.Highlight,
    Mode3 = IlluminationFlags.Reflection | IlluminationFlags.Raytrace,
    Mode4 = IlluminationFlags.Glass | IlluminationFlags.Raytrace,
    Mode5 = IlluminationFlags.Fresnel | IlluminationFlags.Raytrace,
    Mode6 = IlluminationFlags.Refraction | IlluminationFlags.Raytrace,
    Mode7 = IlluminationFlags.Refraction | IlluminationFlags.Fresnel | IlluminationFlags.Raytrace,
    Mode8 = IlluminationFlags.Reflection,
    Mode9 = IlluminationFlags.Glass,
    Mode10 = 256,
}
