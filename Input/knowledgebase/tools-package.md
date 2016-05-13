Title: How To Use The Tools Package
Description: Obtaining Wyam for use in build scripts, unit tests, etc.
---
[One of the NuGet packages provided by Wyam](https://www.nuget.org/packages/Wyam) is a "tools package". This means that it includes the wyam executable as well as all other required libraries in a special "tools" subfolder that gets unpacked when the package is downloaded. This is helpful in cases where you want to obtain the Wyam command line application from NuGet (as opposed to downloading it directly). For example, a tools package allows you to easily use Wyam in [Cake](http://cakebuild.net/) build scripts.

For more information about how tools packages work, see the excellent blog post *[How to use a tool installed by Nuget in your build scripts](https://lostechies.com/joshuaflanagan/2011/06/24/how-to-use-a-tool-installed-by-nuget-in-your-build-scripts/)*.