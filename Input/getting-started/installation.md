Title: Installation
Description: How to install Wyam.
Order: 1
---
# Executable

To download Wyam, visit the [Releases](https://github.com/Wyamio/Wyam/releases) page and download the most recent 'Wyam.zip' file. Then unzip it into a folder of your choice. That's it. You may also want to add the folder where you unzipped Wyam to your path, but this step is optional. 

**Note that you may also need to right-click the zip file after download and select "Unblock" in the Security section of the properties dialog, otherwise you could get strange errors when using the application.**

Once it becomes a little more mature, Wyam will also be added to Chocolatey.

# Libraries

If you're developing addins or you want to embed Wyam in your own applications you can use [Wyam.Common](https://www.nuget.org/packages/Wyam.Common) (for developing addins) and [Wyam.Core](https://www.nuget.org/packages/Wyam.Core) (for embedding the engine) on NuGet. If you're embedding Wyam and your configuration requires other libraries that aren't included in Wyam.Core, you may also have to [search NuGet for related Wyam libraries](https://www.nuget.org/packages?q=wyam) and add those too.

## Development Builds

You can also get the most recent build at the MyGet feed: [https://www.myget.org/feed/Packages/wyam](https://www.myget.org/feed/Packages/wyam).