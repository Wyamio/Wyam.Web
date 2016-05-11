Title: Obtaining
Description: How to download and install Wyam.
Order: 1
---
There are several ways to download and install Wyam depending on which platform you're using and how much automation and control you want.

# Zip File

To download Wyam as a zip file, visit the [Releases](https://github.com/Wyamio/Wyam/releases) page and download the most recent archive. Then unzip it into a folder of your choice. That's it. You may also want to add the folder where you unzipped Wyam to your path, but that step is optional.

**Note that you may also need to right-click the zip file after download and select "Unblock" in the Security section of the properties dialog, otherwise you could get strange errors when using the application.**

# Windows Installer

To download Wyam as an installable Windows application, visit the [Releases](https://github.com/Wyamio/Wyam/releases) page and download the most recent `Setup.exe`. That will install Wyam along with a utility application called Wyam.Windows. It will also add Wyam to the Programs and Features control panel and create a Wyam.Windows Start Menu group. It also creates a shortcut that opens a command prompt with the current version of Wyam in the path.

Note that when you install Wyam from the installer, it is placed on your system at `%LocalAppData%\Wyam` (on most systems this is located at `C:\Users\username\AppData\Local\Wyam`).

The Wyam.Windows application contains some helpful commands to configure and control your environment:

```
usage:  <command> [<args>]

    update         Update to the latest version.
    add-path       Add the installation path to the PATH system
                   environment variable.
    remove-path    Remove the installation path from the PATH system
                   environment variable.
```

Of particular note, run `wyam.windows.exe update` to perform an update of Wyam to the latest version.

# Tools Package

Wyam is also available as a tools package for inclusion in build systems (such as the excellent [Cake](http://cakebuild.net/), which powers the Wyam build). You can [find the tools package on NuGet](https://www.nuget.org/packages/Wyam).

# Chocolatey

Wyam will be added to Chocolatey soon, see [issue #95](https://github.com/Wyamio/Wyam/issues/95) for more details.

# Libraries

If you're developing addins or you want to embed Wyam in your own applications you can use [Wyam.Common](https://www.nuget.org/packages/Wyam.Common) for developing addins and [Wyam.Core](https://www.nuget.org/packages/Wyam.Core) for embedding the engine. These are both available as packages on NuGet. If you're embedding Wyam and your configuration requires other libraries (such as modules), you may also have to [search NuGet for related Wyam libraries](https://www.nuget.org/packages?q=wyam) and add those too.

# Development Builds

You can also get the most recent builds at the MyGet feed: [https://www.myget.org/feed/Packages/wyam](https://www.myget.org/feed/Packages/wyam).