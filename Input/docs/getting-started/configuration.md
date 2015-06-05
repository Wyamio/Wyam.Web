Title: Configuration
Order: 4
---
The command line Wyam application reads a configuration file typically named `config.wyam` (though you can change that with an argument) that sets up the environment and initializes metadata and pipelines. It consists of two parts, the *setup* and the *configuration*. These two sections are separated by a line consisting entirely of one or more dashes.

Both sections of the configuration file are evaluated as C# code, so you can make use of the full C# language and the entire .NET ecosystem. However, it's not necessary to know C# to write Wyam configuration files. The syntax has been carefully crafted to be usable by anyone no matter their level of programming experience.

# Setup

The setup helps establish the Wyam environment and gets evaluated before the rest of the configuration file. It's generally responsible for declaring NuGet packages, assemblies, and namespaces that should be made available to the main configuration body. Any modules that exist in referenced NuGet packages or assemblies will get automatically imported for use in pipelines. Additionally, the objects in NuGet packages and assemblies will be available from the configuration body and any specified namespaces will be brought into scope. Note that while the setup portion of the configuration file is evaluated as C# code just like the configuration body, it only references a small subset of the .NET class library (basically just enough to set things up).

## NuGet

Wyam can automatically download any NuGet packages you declare in the setup portion of the configuration file. These will then be scanned for modules and be made available to the main configuration body. NuGet packages are configured using the global `Packages` object and a fluent interface.

`Packages.Add(string packageId, string versionSpec = null, bool allowPrereleaseVersions = false, bool allowUnlisted = false)` will let you specify a NuGet package to download from the default package source, which is the main NuGet.org feed. `Packages.AddRepository(string packageSource)` will let you define alternate package sources (such as an internal NuGet feed or a feed on MyGet). You can then continue to chain additional `Add(...)` calls as before. For example:

```
Packages
    .Add("Humanizer")
    .Add("Newtonsoft.Json");
Packages
    .AddRepository("https://www.myget.org/F/roslyn-nightly/")
    .Add("Microsoft.CodeAnalysis")
    .Add("Microsoft.CodeAnalysis.Scripting");
```

From the `Add(...)` method you can also specify the acceptable package version(s) and whether prerelease and/or unlisted packages should be allowed. The `versionSpec` argument takes a string that matches the standard NuGet version specifications defined at https://docs.nuget.org/create/versioning

By default, packages are downloaded to `\packages`. If you want to change this, set `Packages.Path` to the relative folder where you want packages to be downloaded.

## Assemblies

In addition to NuGet packages you can also import assemblies. This is accomplished... TODO

## Namespaces

To make working with your imported packages and assemblies from the configuration portion of the file easier, you'll probably want to bring their namespaces into scope using the `Namespace` property. TODO

# Configuration

## Initial Metadata

TODO

## Pipelines

Configuring a pipeline is easy, and Wyam configuration files are designed to be simple and straightforward:
```
Pipelines.Add(
	ReadFiles("*.md"),
	Markdown(),
	WriteFiles(".html")
);
```

However, don't let the simplicity fool you. Wyam configuration files are C# scripts and as such can make use of the full C# language and the entire .NET ecosystem (including the built-in support for NuGet and other assemblies as explained above). One of the core modules even lets you write a delegate right in your configuration file for extreme flexibility.

Note that you supply the pipeline with new instances of each module. An astute reader will notice that in the example above, modules are being specified with what look like global methods. These methods are just shorthand for the actual module class constructors and this convention can be used for any module within the configuration script. The example configuration above could also have been written as:
```
Pipelines.Add(
	new ReadFiles("*.md"),
	new Markdown(),
	new WriteFiles(".html")
);
```

Some modules also accept child modules as part of their processing. For example, the `FrontMatter` module accepts a child module to handle parsing whatever front matter content is found. This way, the same module can be used to recognize front matter without it having to worry about what kind of content the front matter contains (such as YAML or JSON). For example:

```
Pipelines.Add(
	ReadFiles("*.md"),
       FrontMatter(Yaml()),
	Markdown(),
	WriteFiles(".html")
);
```