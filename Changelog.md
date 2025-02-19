# Nerd_STF v3.0-beta3

Nerd_STF 3.0 is approaching a stable release, but I've still got some things that I still want to add and things that I feel like are unsatisfactory. So here's another beta release. There will probably only be one or two more before a final version is ready. This is easily the most ambitious project I've done in such a small amount of time, so I think you can bear with me as I bring this part of it to completition.

Here's what's new:

## `Fill<T>` and `Fill2d<T>` are back!

I thought the `Fill<T>` delegate was slightly redundant, since you could probably just pass an `IEnumerable` or something instead, but I've learned that it's really easy to pass a Fill delegate in places where an IEnumerable would be annoying. I might add support for Fill stuff in the future, such as an extension list, but for now I've just re-added the delegate and made most types support it in a constructor.

## Slight Matrix Constructor Change

I think I got the meaning of the `byRows` parameter mixed up in my head, so I have swapped its meaning to what it (I think) should be. The default value has also changed, so unless you've been explicitly using it you won't notice a difference.

## And Best of All: Colors!

I have had plenty of time to think about how I could have done colors better in the previous iteration of the library, and I've come up with this, which I think is slightly better.

First of all, colors derive from the `IColor<TSelf>` interface similarly to how they did before, but no more `IColorFloat`. Now, every color has double-precision channels by default. To handle specific bit sizes, the `IColorFormat` interface has been created. It can of course be derived from, and I think it's pretty easy to use and understand, but hopefully there will be enough color formats already defined that you won't even need to touch it directly. At the moment, there's only one real color format created, `R8G8B8A8`, which is what it sounds like: 8 bits for each of the RGBA channels. There will be plenty more to come.

I have been thinking about writing a stream class that is capable of having a bit-offset. I would use it in tandom with the color formats, as many of them span multiple bytes in ways that don't always align with 8-bit bytes. It seems somewhat out of place, but I think I'll go for it anyway.

There's also a color palette system now. You give it a certain number of colors and it allocates room to the nearest power of two. If you give it 6 colors, it allocates room for 8. This is to always keep the size of the palette identical to its bit depth. 6 colors needs 3 bits per color, so might as well do as much as you can with those 3 bits.

There is also an `IndexedColor` "format," which does not store its color directly. Rather, it stores its index and a reference to the color palette it came from. I understand a true "indexed color" wouldn't store a reference to its palette to save memory, but this is mostly for ease of use. Colors are passed through methods with the `ref` keyword, so you can manipulate them directly.

```csharp
void MethodA()
{
    ColorPalette<ColorRGB> palette = new(8);

    // palette[3] is currently set to black.
    MethodB(palette[3]);
    // palette[3] is now set to blue.
}

void MethodB(IndexedColor<ColorRGB> color)
{
    color.Color() = ColorRGB.Blue;

    // You could also:
    ref ColorRGB val = ref color.Color();
    val = ColorRGB.Blue;
}
```

Anyway, that's all I've got for now. I'm not sure what will be next up, but here's what's left to do:
- Complex numbers and quaternions.
- More color types and formats.
- Bit-offset compatible streams.
- Fix bugs/inconveniences I've noted.

I think the Image type will be completely reworked and might be what version 3.1 is.
