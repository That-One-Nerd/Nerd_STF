# Nerd_STF

Nerd_STF is a multi-purpose library for .NET focused primarily on mathematics. It supports basically all versions of .NET has plenty of flexibility to let you do what you please with it.

**Contents**
- [Examples](#examples)
- [How to Install](#how-to-install)
    - [NuGet](#nuget)
    - [Manual Reference](#manual-reference)
- [I found a bug!](#i-found-a-bug)
- [I'd like to contribute!](#id-like-to-contribute)
- [When's your next release?](#whens-your-next-release)
- [Older Versions](#older-versions)

## Examples

Here's how to derive a polynomial in Nerd_STF:
```csharp
using Nerd_STF.Mathematics.Equations;

Polynomial poly = new(2, 1, 3); // 2x^2 + x + 3
Polynomial derivative = poly.Derive();

Console.WriteLine(derivative);  // Output: 4x + 1
```

Here's how to get or set part of a number group:
```csharp
Float3 xyz = (1, 2, 3);

Float2 xy = new(xyz["xy"]);
Float2 zy = new(xyz["zy"]);
double[] yxzy = [.. xyz["yxzy"]];

xyz["yz"] = [7, 8];
Console.WriteLine(xyz); // Output: (1, 7, 8)
```

Pretty easy, right?

## How to Install

### NuGet

You can install the package very easily with the NuGet Package Manager. The link to its NuGet page is [here](https://www.nuget.org/packages/Nerd_STF/). You could install it by running a command:
```shell
# Do not include version flag to download the latest release.
dotnet add package Nerd_STF --version 3.0
```
or by including a package reference element in your project file:
```xml
<!-- Do not include the version tag to download the latest release. -->
<PackageReference Include="Nerd_STF" Version="3.0" />
```

### Manual Reference

You could also manually reference the DLL for the project. Go to the [releases page](https://github.com/That-One-Nerd/Nerd_STF/releases) and select the library version and .NET target of your choice. At the time of writing, this project compiles to .NET Standard 1.3, 2.1, and .NET 7.0, but more may be added in the future.

Place the DLL somewhere you'll be able to reference later.

If you're using Visual Studio 2019 or 2022:
- Right click the project icon in the hierarchy.
- Go to **Add** > **Project Reference**
- Click **Browse** and locate the DLL you saved earlier.
- Click **OK**. You should be good to go!

Otherwise, you'll have to add a project reference element in your project file somewhere.
```xml
<Reference Include="Nerd_STF">
  <HintPath>path\to\your\download\Nerd_STF.3.0.NET7.0.dll</HintPath>
</Reference>
```

## I found a bug!

I'm not surprised, I'm only one guy. Feel free to make an issue in the [repository](https://github.com/That-One-Nerd/Nerd_STF) and I'll get to it when I can!

## I'd like to contribute!

Well, I'd prefer to do most of the programming myself, but if anyone wants to submit a pull request, feel free! Stick to the version-specific branches, try not to make commits on the main branch.

## When's your next release?

No idea. This is a pet project, so progress on this library will come and go. It was more than a year between versions 2.4.1 and 3.0. It's really whenever mood strikes, so just watch the [project](https://github.com/users/That-One-Nerd/projects/8) to see what the current status is.

## Older Versions

3.0 has plenty of breaking changes from 2.4.1 (and the rest of the 2.x updates), so it's totally fine if you don't want to update. I'll keep the 2.x updates up for the long-term, no reason to remove them. As for the versions before 2.0, well, it wasn't even really a library at that point. Do with it what you will.
