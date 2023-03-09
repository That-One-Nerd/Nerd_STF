# Nerd_STF

## Table of Contents
- [What is it?](#what-is-it)
- [What about Nerd_STF Versions `2021`?](#what-about-nerd_stf-versions-2021)
- [How do I install it?](#how-do-i-install-it)
- [I've found a bug!](#ive-found-a-bug)
- [I'd like to contribute.](#id-like-to-contribute)

## What is it?

Nerd_STF is a multi-purpose .NET 7.0 library that contains many objects I feel would help the default C# library package. Feel free to do with it what you'd like.

Nerd_STF includes some math as well as many other computer science topics. It contains types like groups of floats/ints, geometry types like `Vert`, `Line`, and `Triangle`, and color types like `RGBA`, `CMYKA`, `HSVA`, and their byte equivalents, all of which can convert seamlessly between each other.

## What about Nerd_STF Versions `2021`?
Nerd_STF `2021` used an different version scheme, based on the year, as you might have guessed (it is not the year `2` right now), and while I will be keeping the `2021` versions up, I wouldn't recommend using them, and the code is old code, written by a more naive me. Hell, I wrote an entire `List<T>` class there before I knew of the `System.Collections.Generic.List<T>` class that did literally everything for me already. Oh well. So, keep that in mind when you check out those versions of the library.

## How do I install it?
The NuGet package for this library is [here](https://www.nuget.org/packages/Nerd_STF/), so you could always install it that way using the .NET CLI:
1. Open your terminal in your project directory.
2. Enter this command: `dotnet add package Nerd_STF`
3. There is no step 3.

You can also include the NuGet package via a package reference in your project file:
1. Open your project file.
2. Add this to the XML data: `<PackageReference Include="Nerd_STF" />`
3. There is no step 3.

Alternatively, you can install it via a project reference in Visual Studio 2019 and 2022. Here's how:
1. Download the latest library release from the [GitHub repository](https://github.com/That-One-Nerd/Nerd_STF/releases), extract the files, and save them somewhere you can find later. The files must all be in the same direcrtory/folder together.
2. Open Visual Studio 2019 or 2022.
3. Right-click your .NET project in the Solution Explorer.
4. Hover over "Add >" list and then click "Project Reference..."
5. Click "Browse," and locate the `.dll` file you previously extracted (the full name is "Nerd_STF.dll").
6. Click "Add," then "OK," and the library is now imported!

## I've found a bug!

I'm not suprised, there are definitely a bunch of undiscovered bugs in Nerd_STF simply because I'm just one person. If you've found one, please do me a favor and create an issue about it in the [GitHub repository](https://github.com/That-One-Nerd/Nerd_STF). They likely aren't difficult fixes, they are just hard to spot even with some level of testing.

## I'd like to contribute.

I would love some contributions! I probably won't accept drastic pull requests or merges with the library, but small changes are quite welcome! I'm just one person and I can only do so much work by myself. If you want to contribute, please only edit the current version branch (eg. if we are on version `2.3.1`, edit the `v2.3` branch). Please do not edit the main branch. I will merge the changes with main myself.

Try to follow a similar style to the current library, but otherwise I'm open to changes. Thank you to those who contribute!
