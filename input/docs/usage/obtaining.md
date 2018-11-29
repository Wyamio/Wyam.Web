Title: Obtaining
Description: How to download and install Wyam.
Order: 1
RedirectFrom:
  - getting-started/obtaining
  - knowledgebase/tools-package
---
There are several ways to download and install Wyam depending on which platform you're using and how much automation and control you want. Wyam currently requires [.NET Core 2.x](https://www.microsoft.com/net/download) to be installed on your system.

# Global Tool

The easiest way to install Wyam is via the [global tool package](https://docs.microsoft.com/en-us/dotnet/core/tools/global-tools) named [Wyam.Tool](https://www.nuget.org/packages/Wyam.Tool/). You can install it like this:

```
dotnet tool install -g Wyam.Tool
```

Then you can use it with the `wyam` command:

```
wyam ...
```

On non-Windows systems you might need to add .NET Global tool folder to the environment path variable, for your shell to find the `wyam` command. If running Bash you can add it to your `.bash_profile` like below (`echo ~/.dotnet/tools` will give your absolute path to .NET global tool folder):
```
cat << \EOF >> ~/.bash_profile
# Add .NET Core SDK tools
export PATH="$PATH:/home/{username}/.dotnet/tools"
EOF
```

# Zip File

To download Wyam as a zip file, visit the [Releases](https://github.com/Wyamio/Wyam/releases) page and download the most recent archive. Then unzip it into a folder of your choice. That's it. .NET Core [framework-dependent deployment](https://docs.microsoft.com/en-us/dotnet/core/deploying/#framework-dependent-deployments-fdd), which means it's shipped as a DLL assembly that requires the `dotnet` CLI bootstrapper to run. You may also want to add the folder where you unzipped Wyam to your path, but that step is optional.

Once you download the [ZIP archive](https://github.com/Wyamio/Wyam/releases) and extract it somewhere, running Wyam looks like:

```
dotnet /path/to/wyam/Wyam.dll ...
```

# Tools Package

[One of the NuGet packages provided by Wyam](https://www.nuget.org/packages/Wyam) is a "tools package". This means that it includes the wyam executable as well as all other required libraries in a special "tools" subfolder that gets unpacked when the package is downloaded. This is helpful in cases where you want to obtain the Wyam command line application from NuGet (as opposed to downloading it directly). For example, a tools package allows you to easily use Wyam in [Cake](http://cakebuild.net/) build scripts.

For more information about how tools packages work, see the excellent blog post *[How to use a tool installed by Nuget in your build scripts](https://lostechies.com/joshuaflanagan/2011/06/24/how-to-use-a-tool-installed-by-nuget-in-your-build-scripts/)*.

# Chocolatey

Wyam can be installed via [Chocolatey](https://chocolatey.org/packages/wyam) using the following command:

```
choco install wyam
```

A specific version can be installed with the following command:

```
choco install wyam --version 1.2.3
```

You can update Wyam with Chocolatey using the following command:

```
choco upgrade wyam
```

If you would like to bypass the Chocolatey registry and install the tools package from the NuGet gallery instead (not recommended), you can use the following command:

```
choco install Wyam -s https://www.nuget.org/api/v2/
```

# Libraries

If you're developing addins or you want to embed Wyam in your own applications you can use [Wyam.Common](https://www.nuget.org/packages/Wyam.Common) for developing addins and [Wyam.Core](https://www.nuget.org/packages/Wyam.Core) for embedding the engine. These are both available as packages on NuGet. If you're embedding Wyam and your configuration requires other libraries (such as modules), you may also have to [search NuGet for related Wyam libraries](https://www.nuget.org/packages?q=wyam) and add those too.

# Development Builds

You can also get the most recent builds at the MyGet feed:

NuGet V3 clients (VS 2015, Wyam): [https://www.myget.org/F/wyam/api/v3/index.json](https://www.myget.org/F/wyam/api/v3/index.json)

NuGet V2 clients (VS 2013, Cake): [https://www.myget.org/F/wyam/api/v2](https://www.myget.org/F/wyam/api/v2)

To use the development feed inside a Wyam configuration file for a particular module, specify it like this:

```
#n Wyam.Markdown -s https://www.myget.org/F/wyam/api/v3/index.json -l
```

The `-l` flag indicates that the latest version of the specified package should always be used.
