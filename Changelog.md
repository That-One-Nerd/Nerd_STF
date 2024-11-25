# Nerd_STF v3.0-beta2

I've added a substantial number of things in this update, mostly matrix related.

## List Tuples

In the previous beta, I introduced **Combination Indexers** for the double and int groups, however a problem was that they only returned `IEnumerable`s. So while some interesting things were supported, some things were not.

```csharp
Float4 wxyz = (1, 2, 3, 4);
IEnumerable<double> vals1 = wxyz["xy"]; // Yields [2, 3]

Float2 vals2 = wxyz["xy"];              // Not allowed!
```

And that kind of sucked. So I created the `ListTuple<T>` type. It's job is to act like a regular tuple, but be able to impliclty convert to either an `IEnumerable` or a regular `ValueTuple<T>`, thus allowing conversions to the double and int groups indirectly. Now, all combination indexers return a `ListTuple` instead of an `IEnumerable`.

Under the hood, the `ListTuple` actually uses an array, but you get the idea.

```csharp
Float4 wxyz = (1, 2, 3, 4);
ListTuple<double> vals1 = wxyz["xy"]; // Yields (2, 3)

Float2 vals2 = vals1;                 // Yields (2, 3)
IEnumerable<double> vals3 = vals1;    // Yields [2, 3]
```

Problem is, now the names have the potential to make much less sense.
```csharp
Float4 wxyz = (1, 2, 3, 4);
Float2 xy = wxyz["xy"];     // x <- x, y <- y
Float2 wz = wxyz["wz"];     // x <- w, y <- z
```

But whatever. You can always stick to using `IEnumerable`s if you want.

## No More `*.Abstract`

I got rid of all the `Abstract` namespaces, since they don't really make much sense in the grand scheme of things. They've all been moved to the namespace that applies to them most (eg. `INumberGroup` went to `Nerd_STF.Mathematics`, `ICombinationIndexer` went to `Nerd_STF` since it applies to more than just mathematics).

## The `Fraction` Type

This type originally went under the name of `Rational` in Nerd_STF 2.x, but that name is actually incorrect, right? So in the rework, it changed names. But it also can do much more now thanks to the `INumber` interface added in .NET 7.0. If you're using that framework or above, the fraction type is fully compatible with that type, and all the math functions in `MathE` and elsewhere that use `INumber` will work with it.

Can I just say that the `INumber` interface is really annoying to write a type for? There's so many weird casting functions and a whole lot of methods that even the .NET developers will hide in public declarations. Why have them at all?

---

And I want to change the name of the `MathE` class. I'm thinking `Math2`, but I'm open to suggestions.

## And Best of All, Matrices

Oh yeah, we're adding those things again. I haven't completed (or even started) the dynamic `Matrix` class, that will arrive in beta3. But I have the `Matrix2x2`, `Matrix3x3`, and `Matrix4x4` fully implemented. The `ToString()` methods are much better with the new implementation than previously, and the `GetHashCode()` methods give different results even if the numbers have their positions swapped (which they originally didn't do).

And it's much faster. Much, much faster. Don't get me wrong, multiplying a 4x4 matrix still requires 64 multiplications and 48 additions, which is quite a lot, but my original implementation was littered with many method calls, easily doubling the runtime. I have now super-inlined basically all of the static matrix code. And I mean, replacing all method calls with nothing but multiplication and addition for things like the determinants, the cofactors, the inverses, and more. Don't look at the source, it's really ugly.

---

That's all the major stuff in this update! I'll see you guys in beta3!

P.S. I know that the System library also includes `Vector2`, `Vector3`, and `Vector4` types. I'll add casting support for them soon.
