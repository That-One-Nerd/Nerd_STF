# Nerd_STF

## What is it?

Nerd_STF is a C# library that runs on .Net 6.0, and contains added structures and classes I feel would help the default C# library package. Feel free to do with it what you would like.

Nerd_STF has recently been remade, completely rebuilding many of its topics.

## What does it include?
Nerd_STF will include math structures as well as other computer science topics. Right now, it is mainly focused on mathematics, but will branch out in the future. It currently contains things like lists of 3 doubles or ints, or `Vert`, `Line`, and `Triangle` classes, that are rich in implementation.

## What about Nerd_STF Versions `2021`?
Nerd_STF `2021` used an different version scheme, based on the year, as you might have guessed (it is not the year `2` right now), and while I will be keeping the `2021` versions up, I wouldn't recommend using them, and the code is old code, written by a more naive me. Hell, I wrote an entire `List<T>` class there before I knew of the `System.Collections.Generic.List<T>` class that did literally everything for me already. Oh well. So, keep that in mind when you check out those versions of the library.

## How do I install it?
There is a nuget package for this ([here](https://www.nuget.org/packages/Nerd_STF/)), so you could install it that way.

Alternatively, you can install it with a project reference in Visual Studio, and I'll walk you through doing that here.

Step 1: Find the `.dll` for this library, found in `/Nerd_STF/Nerd_STF/bin/Release/net6.0/ref/Nerd_STF.dll`. You can either move it, or simply remember where it is.
Step 2: Search for "Add Project Reference" in the Visual Studio 2019 or 2022 search bar.
Step 3: Click the "Browse" tab on the left, then click the "Browse" button on the bottom-right.
Step 4: Select the `.dll` we found earlier.
Step 5: Click OK.

This is what happens the first time you import it. Any other times, simply go to the "Add Project Reference" Window, then the "Browse" tab, and the `.dll` will already be in the list in the middle. Find it, and click the box on its left. Then click OK.

---

I hope you enjoy using Nerd_STF!
