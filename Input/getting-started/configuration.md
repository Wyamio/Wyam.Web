Title: Configuration
Order: 4
---
The command line Wyam application reads a configuration file typically named `config.wyam` (though you can change that with an argument) that sets up the environment and initializes metadata and pipelines. It consists of two parts, the *setup* and the *configuration*. These two sections are separated by a line consisting entirely of one or more equals (`===`).

Both sections of the configuration file are evaluated as C# code, so you can make use of the full C# language and the entire .NET ecosystem. However, it's not necessary to know C# to write Wyam configuration files. The syntax has been carefully crafted to be usable by anyone no matter their level of programming experience.

# Setup
---

The setup helps establish the Wyam environment and gets evaluated before the rest of the configuration file. It's generally responsible for declaring NuGet packages, assemblies, and namespaces that should be made available to the main configuration body. Any modules that exist in referenced NuGet packages or assemblies will get automatically imported for use in pipelines. Additionally, the objects in NuGet packages and assemblies will be available from the configuration body and any specified namespaces will be brought into scope. Note that while the setup portion of the configuration file is evaluated as C# code just like the configuration body, it only references a small subset of the .NET class library (basically just enough to set things up).

## <a name="nuget"></a>NuGet

Wyam can automatically install any NuGet packages you declare in the setup portion of the configuration file. These will then be scanned for modules and be made available to the main configuration body. NuGet packages are configured using the global `Packages` object and a fluent interface.

`Packages.Install(string packageId, string versionSpec = null, bool allowPrereleaseVersions = false, bool allowUnlisted = false)` will let you specify a NuGet package to download from the default package source, which is the main NuGet.org feed. `Packages.Repository(string packageSource)` will let you define alternate package sources (such as an internal NuGet feed or a feed on MyGet). You can then continue to chain additional `Install(...)` calls as before. For example:

```
Packages
    .Install("Humanizer")
    .Install("Newtonsoft.Json");
Packages.Repository("https://www.myget.org/F/roslyn-nightly/")
    .Install("Microsoft.CodeAnalysis")
    .Install("Microsoft.CodeAnalysis.Scripting");
```

From the `Install(...)` method you can also specify the acceptable package version(s) and whether prerelease and/or unlisted packages should be allowed. The `versionSpec` argument takes a string that matches the standard NuGet version specifications defined at https://docs.nuget.org/create/versioning

By default, packages are downloaded to `\packages`. If you want to change this, set `Packages.Path` to the relative folder where you want packages to be downloaded. For example, you could set this to a system-wide folder if you have several scripts that share the same packages.

Note that some commonly-used module libraries are made available in the command line application by default and don't need to be explicitly specified in the configuration file unless you're using the embedded version of Wyam. These are:
* `Wyam.Modules.Markdown`
* `Wyam.Modules.Razor`
* `Wyam.Modules.Yaml`

## Assemblies

In addition to NuGet packages you can also load assemblies. This is accomplished by using the `Assemblies` property. You can load all the assemblies in a directory with the `LoadDirectory(string path, SearchOption searchOption = SearchOption.AllDirectories)` method. The specified directory can either be relative to the active directory or absolute. You can also load a single assembly by location with the `LoadFile(string path)` method and by full name with the `Load(string name)` method. For example:

```
Assemblies
    .LoadDirectory(@@"lib")
	.LoadFile(@@"foo\bar.dll")
	.Load("SampleAssembly, Version=1.0.2004.0, Culture=neutral, PublicKeyToken=8744b20f8da049e3");
```

By default, the following assemblies are already loaded so you don't need to explicity specify them:
* `System`
* `System.Collections.Generic`
* `System.Linq`
* `System.Core`
* `Microsoft.CSharp`
* `System.IO`
* `System.Diagnostics`

Also note that all assemblies from the directory containing the Wyam executable (and all subdirectories) will also be scanned for module assemblies.

# Configuration
---

## Initial Metadata

Each pipeline starts with a single document prepopulated with initial metadata you specify in the configuration file. You can use this facility to introduce variables that can influence the way the pipeline behaves. To set initial metadata, just add values to the `Metadata` property (it's a `IDictionary<string, object>`). Note that the dictionary values are objects, so you can store simple primitive values or complex structure. For example:

```
Metadata["Foo"] = "Bar";
Metadata.Add("Baz", new KeyValuePair<string, string>("abc", "xyz")); 
```

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

### Explicit Module Instantiation

Note that you supply the pipeline with new instances of each module. An astute reader will notice that in the example above, modules are being specified with what look like global methods. These methods are just shorthand for the actual module class constructors and this convention can be used for any module within the configuration script. The example configuration above could also have been written as:
```
Pipelines.Add(
	new ReadFiles("*.md"),
	new Markdown(),
	new WriteFiles(".html")
);
```

### Child Modules

Some modules also accept child modules as part of their processing. For example, the `FrontMatter` module accepts a child module to handle parsing whatever front matter content is found. This way, the same module can be used to recognize front matter without it having to worry about what kind of content the front matter contains (such as YAML or JSON). For example:

```
Pipelines.Add(
	ReadFiles("*.md"),
    FrontMatter(Yaml()),
	Markdown(),
	WriteFiles(".html")
);
```

### Pipeline Names

Pipelines can be given names, which makes them easier to identify in trace messages and also makes them easier to refer to within templates (for example, to get all the documents generated by a previous pipeline). Just pass in the name of a pipeline as the first parameter to the `Add(...)` method. If no name is provided (as above), then pipelines will implicitly be given the names `Pipeline 1`, `Pipeline 2`, etc.

```
Pipelines.Add("Markdown",
	ReadFiles("*.md"),
    FrontMatter(Yaml()),
	Markdown(),
	WriteFiles(".html")
);
```

## Declarations

The code in your configuration is typically executed inside the context of an "invisible" class and method. This means that you can't bring namespaces into scope, create classes, declare helper methods, etc. If you need to do any of these things, place it above the configuration code and separate it with a line consisting entirely of one or more dashes (`---`). Any code above this line will be evaluated outside the scope of the configuration method or any class. This means that you can bring namespaces into scope with `using` statements, declare helper classes, etc. Note that this code is global, so if you want to declare helper methods, they'll have to be placed within a wrapper class.

```
using System.IO;

public static class Helpers
{
	public string GetWriteExtension()
	{
		return ".html";
	}
}

---

Pipelines.Add("Markdown",
	ReadFiles("*.md"),
    FrontMatter(Yaml()),
	Markdown(),
	WriteFiles(Helpers.GetWriteExtension())
);
```

Note that namespaces for all found modules as well as the following namespaces are automatically brought into scope for every configuration script so you won't need to explicitly add them:

* `System`
* `System.Collections.Generic`
* `System.Linq`
* `System.IO`
* `System.Diagnostics`

# Example
---

The full configuration file for this site is given below as an example:

```
Assemblies
	.LoadDirectory(@@"..\Wyam\Wyam.Modules.Markdown\bin\Debug")
	.LoadDirectory(@@"..\Wyam\Wyam.Modules.Yaml\bin\Debug")
	.LoadDirectory(@@"..\Wyam\Wyam.Modules.Razor\bin\Debug");
Packages
	.Install("Bootstrap")
	.Install("jQuery", "[2.1.1]");
	
===

Pipelines.Add("Markdown",
	ReadFiles(@@"*.md"),
	FrontMatter(Yaml()),
	Markdown(),
	Razor(),
	WriteFiles(".html")
);

Pipelines.Add("Razor",
	ReadFiles(@@"*.cshtml").Where(x => Path.GetFileName(x)[0] != '_'),
	FrontMatter(Yaml()),
	Razor(),
	WriteFiles(".html")
);

Pipelines.Add("Resources",
	CopyFiles(@@"*").Where(x => Path.GetExtension(x) != ".cshtml" && Path.GetExtension(x) != ".md")
);
```