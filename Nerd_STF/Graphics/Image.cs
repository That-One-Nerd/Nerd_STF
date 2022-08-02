namespace Nerd_STF.Graphics;

public struct Image : ICloneable, IEnumerable, IEquatable<Image>
{
    public IColor[,] Pixels { get; init; }
    public Int2 Size { get; init; }

    public Image(int width, int height)
    {
        Pixels = new IColor[width, height];
        Size = new(width, height);
        for (int y = 0; y < height; y++) for (int x = 0; x < width; x++) Pixels[x, y] = RGBA.Clear;
    }
    public Image(int width, int height, IColor[] cols)
    {
        Pixels = new IColor[width, height];
        Size = new(width, height);
        for (int y = 0; y < width; y++) for (int x = 0; x < height; x++) Pixels[x, y] = cols[y * width + x];
    }
    public Image(int width, int height, IColor[,] cols)
    {
        Pixels = cols;
        Size = new(width, height);
    }
    public Image(int width, int height, Fill<IColor> fill)
    {
        Pixels = new IColor[width, height];
        Size = new(width, height);
        for (int y = 0; y < height; y++) for (int x = 0; x < width; x++) Pixels[x, y] = fill(y * width + x);
    }
    public Image(int width, int height, Fill2D<IColor> fill)
    {
        Pixels = new IColor[width, height];
        Size = new(width, height);
        for (int y = 0; y < height; y++) for (int x = 0; x < width; x++) Pixels[x, y] = fill(x, y);
    }
    public Image(int width, int height, Fill<IColorByte> fill)
    {
        Pixels = new IColor[width, height];
        Size = new(width, height);
        for (int y = 0; y < height; y++) for (int x = 0; x < width; x++)
                Pixels[x, y] = (IColor)fill(y * width + x);
    }
    public Image(int width, int height, Fill2D<IColorByte> fill)
    {
        Pixels = new IColor[width, height];
        Size = new(width, height);
        for (int y = 0; y < height; y++) for (int x = 0; x < width; x++) Pixels[x, y] = (IColor)fill(x, y);
    }
    public Image(Int2 size) : this(size.x, size.y) { }
    public Image(Int2 size, IColor[] cols) : this(size.x, size.y, cols) { }
    public Image(Int2 size, IColor[,] cols) : this(size.x, size.y, cols) { }
    public Image(Int2 size, Fill<IColor> fill) : this(size.x, size.y, fill) { }
    public Image(Int2 size, Fill2D<IColor> fill) : this(size.x, size.y, fill) { }
    public Image(Int2 size, Fill<IColorByte> fill) : this(size.x, size.y, fill) { }
    public Image(Int2 size, Fill2D<IColorByte> fill) : this(size.x, size.y, fill) { }

    public IColor this[int indexX, int indexY]
    {
        get => Pixels[indexX, indexY];
        set => Pixels[indexX, indexY] = value;
    }
    public IColor this[Int2 index]
    {
        get => Pixels[index.x, index.y];
        set => Pixels[index.x, index.y] = value;
    }

    public static Image FromSingleColor(int width, int height, IColor col) => new(width, height, (x, y) => col);
    public static Image FromSingleColor(Int2 size, IColor col) => new(size, (x, y) => col);
    public static Image FromSingleColor(IColor col) => new(1, 1, (x, y) => col);

    public object Clone() => new Image(Size, Pixels);

    public bool Equals(Image other) => Pixels == other.Pixels;
    public override bool Equals([NotNullWhen(true)] object? obj)
    {
        if (obj == null) return base.Equals(obj);
        if (obj.GetType() == typeof(Image)) return Equals((Image)obj);
        return base.Equals(obj);
    }
    public override int GetHashCode() => Pixels.GetHashCode();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    public IEnumerator<IColor> GetEnumerator()
    {
        for (int y = 0; y < Size.y; y++) for (int x = 0; x < Size.x; x++) yield return Pixels[x, y];
    }

    public void ModifyBrightness(float value, bool set = false)
    {
        for (int y = 0; y < Size.y; y++) for (int x = 0; x < Size.x; x++)
            {
                RGBA col = Pixels[x, y].ToRGBA();
                col.R = (set ? 0 : col.R) + value;
                col.G = (set ? 0 : col.G) + value;
                col.B = (set ? 0 : col.B) + value;
                Pixels[x, y] = col;
            }
    }
    public void ModifyContrast(float value)
    {
        float factor = 259 * (255 + value) / (255 * (259 - value));
        for (int y = 0; y < Size.y; y++) for (int x = 0; x < Size.x; x++)
            {
                RGBA col = Pixels[x, y].ToRGBA();
                col.R = factor * (col.R - 0.5f) + 0.5f;
                col.G = factor * (col.G - 0.5f) + 0.5f;
                col.B = factor * (col.B - 0.5f) + 0.5f;
                Pixels[x, y] = col;
            }
    }
    public void ModifyHue(Angle value, bool set = false)
    {
        for (int y = 0; y < Size.y; y++) for (int x = 0; x < Size.x; x++)
            {
                HSVA col = Pixels[x, y].ToHSVA();
                col.H = (set ? Angle.Zero : col.H) + value;
                Pixels[x, y] = col;
            }
    }
    public void ModifySaturation(float value, bool set = false)
    {
        for (int y = 0; y < Size.y; y++) for (int x = 0; x < Size.x; x++)
            {
                HSVA col = Pixels[x, y].ToHSVA();
                col.S = (set ? 0 : col.S) + value;
                Pixels[x, y] = col;
            }
    }

    public void Paint(Int2 min, Int2 max, IColor col)
    {
        for (int y = min.y; y <= max.y; y++) for (int x = min.x; x <= max.x; x++) Pixels[x, y] = col;
    }

    public void Resize(Int2 resolution) => Scale(resolution / (Float2)Size);

    public void Scale(float factor) => Scale(Float2.One * factor);
    public void Scale(Float2 factor)
    {
        Int2 newSize = (Int2)(Size * factor);
        Image img = new(newSize);
        for (int y = 0; y < newSize.y; y++) for (int x = 0; x < newSize.x; x++)
            {
                Float2 f = new((float)x / newSize.x, (float)y / newSize.y);
                RGBA col = Pixels[Mathf.RoundInt(f.x * Size.x), Mathf.RoundInt(f.y * Size.y)].ToRGBA();
                img[x, y] = col;
            }
        this = img;
    }

    public IColor[] ToArray()
    {
        IColor[] vals = new IColor[Pixels.Length];
        for (int y = 0; y < Size.y; y++) for (int x = 0; x < Size.x; x++) vals[y * Size.x + x] = Pixels[x, y];
        return vals;
    }

    public static bool operator ==(Image a, Image b) => a.Equals(b);
    public static bool operator !=(Image a, Image b) => !a.Equals(b);
}
