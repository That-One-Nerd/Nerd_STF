using Nerd_STF.Graphics.Abstract;

namespace Nerd_STF.Graphics;

public struct Material : ICloneable, IEquatable<Material>
{
    public float Alpha;
    public float Anisotropy;
    public float AnisotropyRoughness;
    public IColorFloat AmbientColor;
    public float ClearcoatRoughness;
    public float ClearcoatThickness;
    public IColorFloat DiffuseColor;
    public IColorFloat Emissive;
    public IlluminationModel IllumModel;
    public float Metallic;
    public float OpticalDensity;
    public float Roughness;
    public float Sheen;
    public IColorFloat SpecularColor;
    public float SpecularExponent;

    public (Image Image, TextureConfig Config) AlphaTexture;
    public (Image Image, TextureConfig Config) AmbientTexture;
    public (Image Image, TextureConfig Config) DiffuseTexture;
    public (Image Image, TextureConfig Config) DisplacementTexture;
    public (Image Image, TextureConfig Config) EmissiveTexture;
    public (Image Image, TextureConfig Config) MetallicTexture;
    public (Image Image, TextureConfig Config) NormalTexture;
    public (Image Image, TextureConfig Config) RoughnessTexture;
    public (Image Image, TextureConfig Config) SheenTexture;
    public (Image Image, TextureConfig Config) SpecularTexture;
    public (Image Image, TextureConfig Config) SpecularHighlightTexture;
    public (Image Image, TextureConfig Config) StencilTexture;

    public Material()
    {
        Alpha = 1;
        Anisotropy = 0;
        AnisotropyRoughness = 0;
        AmbientColor = RGBA.White;
        ClearcoatRoughness = 0;
        ClearcoatThickness = 0;
        DiffuseColor = RGBA.White;
        Emissive = RGBA.Clear;
        IllumModel = IlluminationModel.Mode0;
        Metallic = 0;
        OpticalDensity = 1.45f;
        Roughness = 1;
        Sheen = 0;
        SpecularColor = RGBA.White;
        SpecularExponent = 10;

        AlphaTexture = (Image.FromSingleColor(RGBA.White), new());
        AmbientTexture = (Image.FromSingleColor(RGBA.White), new());
        DiffuseTexture = (Image.FromSingleColor(RGBA.White), new());
        DisplacementTexture = (Image.FromSingleColor(RGBA.White), new());
        EmissiveTexture = (Image.FromSingleColor(RGBA.Clear), new());
        MetallicTexture = (Image.FromSingleColor(RGBA.Black), new());
        NormalTexture = (Image.FromSingleColor(new RGBA(0.5f, 0.5f, 1)), new());
        RoughnessTexture = (Image.FromSingleColor(RGBA.White), new());
        SheenTexture = (Image.FromSingleColor(RGBA.Black), new());
        SpecularTexture = (Image.FromSingleColor(RGBA.White), new());
        SpecularHighlightTexture = (Image.FromSingleColor(RGBA.White), new());
        StencilTexture = (Image.FromSingleColor(RGBA.White), new());
    }
    public Material(Fill<object> fill)
    {
        Alpha = (float)fill(0);
        Anisotropy = (float)fill(1);
        AnisotropyRoughness = (float)fill(2);
        AmbientColor = (IColorFloat)fill(3);
        ClearcoatRoughness = (float)fill(4);
        ClearcoatThickness = (float)fill(5);
        DiffuseColor = (IColorFloat)fill(6);
        Emissive = (IColorFloat)fill(7);
        IllumModel = (IlluminationModel)fill(8);
        Metallic = (float)fill(9);
        OpticalDensity = (float)fill(10);
        Roughness = (float)fill(11);
        Sheen = (float)fill(12);
        SpecularColor = (IColorFloat)fill(13);
        SpecularExponent = (float)fill(14);

        AlphaTexture = ((Image, TextureConfig))fill(15);
        AmbientTexture = ((Image, TextureConfig))fill(16);
        DiffuseTexture = ((Image, TextureConfig))fill(17);
        DisplacementTexture = ((Image, TextureConfig))fill(18);
        EmissiveTexture = ((Image, TextureConfig))fill(19);
        MetallicTexture = ((Image, TextureConfig))fill(20);
        NormalTexture = ((Image, TextureConfig))fill(21);
        RoughnessTexture = ((Image, TextureConfig))fill(22);
        SheenTexture = ((Image, TextureConfig))fill(23);
        SpecularTexture = ((Image, TextureConfig))fill(24);
        SpecularHighlightTexture = ((Image, TextureConfig))fill(25);
        StencilTexture = ((Image, TextureConfig))fill(26);
    }
    public Material(IlluminationModel illum, Fill<float> floats, Fill<IColorFloat> colors, Fill<(Image, TextureConfig)> images)
    {
        Alpha = floats(0);
        Anisotropy = floats(1);
        AnisotropyRoughness = floats(2);
        AmbientColor = colors(0);
        ClearcoatRoughness = floats(3);
        ClearcoatThickness = floats(4);
        DiffuseColor = colors(1);
        Emissive = colors(2);
        IllumModel = illum;
        Metallic = floats(5);
        OpticalDensity = floats(6);
        Roughness = floats(7);
        Sheen = floats(8);
        SpecularColor = colors(3);
        SpecularExponent = floats(9);

        AlphaTexture = images(0);
        AmbientTexture = images(1);
        DiffuseTexture = images(2);
        DisplacementTexture = images(3);
        EmissiveTexture = images(4);
        MetallicTexture = images(5);
        NormalTexture = images(6);
        RoughnessTexture = images(7);
        SheenTexture = images(8);
        SpecularTexture = images(9);
        SpecularHighlightTexture = images(10);
        StencilTexture = images(11);
    }

    public object Clone() => new Material()
    {
        Alpha = Alpha,
        AmbientColor = AmbientColor,
        Anisotropy = Anisotropy,
        AnisotropyRoughness = AnisotropyRoughness,
        ClearcoatRoughness = ClearcoatRoughness,
        ClearcoatThickness = ClearcoatThickness,
        DiffuseColor = DiffuseColor,
        Emissive = Emissive,
        IllumModel = IllumModel,
        Metallic = Metallic,
        OpticalDensity = OpticalDensity,
        Roughness = Roughness,
        Sheen = Sheen,
        SpecularColor = SpecularColor,
        SpecularExponent = SpecularExponent,

        AlphaTexture = AlphaTexture,
        AmbientTexture = AmbientTexture,
        DiffuseTexture = DiffuseTexture,
        DisplacementTexture = DisplacementTexture,
        EmissiveTexture = EmissiveTexture,
        MetallicTexture = MetallicTexture,
        NormalTexture = NormalTexture,
        RoughnessTexture = RoughnessTexture,
        SheenTexture = SheenTexture,
        SpecularTexture = SpecularTexture,
        SpecularHighlightTexture = SpecularHighlightTexture,
        StencilTexture = StencilTexture
    };
    public bool Equals(Material other) => Alpha.Equals(other.Alpha) && AmbientColor.Equals(other.AmbientColor) &&
        Anisotropy.Equals(other.Anisotropy) && AnisotropyRoughness.Equals(other.AnisotropyRoughness) &&
        ClearcoatRoughness.Equals(other.ClearcoatRoughness) && ClearcoatThickness.Equals(other.ClearcoatThickness) &&
        DiffuseColor.Equals(other.DiffuseColor) && Emissive.Equals(other.Emissive) &&
        IllumModel.Equals(other.IllumModel) && Metallic.Equals(other.Metallic) &&
        OpticalDensity.Equals(other.OpticalDensity) && Roughness.Equals(other.Roughness) && Sheen.Equals(other.Sheen) &&
        SpecularColor.Equals(other.SpecularColor) && SpecularExponent.Equals(other.SpecularExponent) &&
        AlphaTexture.Equals(other.AlphaTexture) && AmbientTexture.Equals(other.AmbientTexture) &&
        DiffuseTexture.Equals(other.DiffuseTexture) && DisplacementTexture.Equals(other.DisplacementTexture) &&
        EmissiveTexture.Equals(other.EmissiveTexture) && MetallicTexture.Equals(other.MetallicTexture) &&
        NormalTexture.Equals(other.NormalTexture) && RoughnessTexture.Equals(other.RoughnessTexture) &&
        SheenTexture.Equals(other.SheenTexture) && SpecularTexture.Equals(other.SheenTexture) &&
        SpecularHighlightTexture.Equals(other.SpecularHighlightTexture) && StencilTexture.Equals(other.StencilTexture);
    public override bool Equals([NotNullWhen(true)] object? obj)
    {
        if (obj == null || obj.GetType() != typeof(Material)) return base.Equals(obj);
        return Equals((Material)obj);
    }
    public override int GetHashCode() => Alpha.GetHashCode() ^ AmbientColor.GetHashCode() ^ Anisotropy.GetHashCode() ^
        AnisotropyRoughness.GetHashCode() ^ ClearcoatRoughness.GetHashCode() ^ ClearcoatThickness.GetHashCode() ^
        DiffuseColor.GetHashCode() ^ Emissive.GetHashCode() ^ IllumModel.GetHashCode() ^ Metallic.GetHashCode() ^
        OpticalDensity.GetHashCode() ^ Roughness.GetHashCode() ^ Sheen.GetHashCode() ^ SpecularColor.GetHashCode() ^
        SpecularExponent.GetHashCode() ^ AlphaTexture.GetHashCode() ^ AmbientTexture.GetHashCode() ^
        DiffuseTexture.GetHashCode() ^ DisplacementTexture.GetHashCode() ^ Emissive.GetHashCode() ^
        Metallic.GetHashCode() ^ NormalTexture.GetHashCode() ^ RoughnessTexture.GetHashCode() ^
        SheenTexture.GetHashCode() ^ SpecularTexture.GetHashCode() ^ SpecularHighlightTexture.GetHashCode() ^
        StencilTexture.GetHashCode();

    public static bool operator ==(Material a, Material b) => a.Equals(b);
    public static bool operator !=(Material a, Material b) => !a.Equals(b);
}
