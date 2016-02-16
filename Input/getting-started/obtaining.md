Title: Obtaining
Description: How to obtain and install Wyam.
Order: 1
---
# Executable

To download Wyam, visit the [Releases](https://github.com/Wyamio/Wyam/releases) page and download the most recent zip archive. Then unzip it into a folder of your choice. That's it. You may also want to add the folder where you unzipped Wyam to your path, but that step is optional. 

**Note that you may also need to right-click the zip file after download and select "Unblock" in the Security section of the properties dialog, otherwise you could get strange errors when using the application.**

Wyam will also be added to Chocolatey soon.

# Tools Package

Wyam is also available as a tools package for inclusion in build systems (such as the excellent [Cake](http://cakebuild.net/), which powers the Wyam build). You can [find the tools package on NuGet](https://www.nuget.org/packages/Wyam/0.11.2-beta).

# Libraries

If you're developing addins or you want to embed Wyam in your own applications you can use [Wyam.Common](https://www.nuget.org/packages/Wyam.Common) (for developing addins) and [Wyam.Core](https://www.nuget.org/packages/Wyam.Core) (for embedding the engine) on NuGet. If you're embedding Wyam and your configuration requires other libraries that aren't included in Wyam.Core, you may also have to [search NuGet for related Wyam libraries](https://www.nuget.org/packages?q=wyam) and add those too.

# Development Builds

You can also get the most recent builds at the MyGet feed: [https://www.myget.org/feed/Packages/wyam](https://www.myget.org/feed/Packages/wyam).