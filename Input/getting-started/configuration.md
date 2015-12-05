Title: Configuration
Description: Describes the format of the configuration file.
Order: 4
---
The command line Wyam application reads a configuration file typically named `config.wyam` (though you can change that with an argument) that sets up the environment and initializes metadata and pipelines. It consists of three parts, the *setup*, any *declarations*, and the *configuration*, in that order. The setup is separated by a line consisting entirely of one or more equals (`===`) and the declarations are separated by a line consisting entirely of one or more dashes (`---`).

The sections of the configuration file are evaluated as C# code, so you can make use of the full C# language and the entire .NET ecosystem. However, it's not necessary to know C# to write Wyam configuration files. The syntax has been carefully crafted to be usable by anyone no matter their level of programming experience.

A configuration file looks like this:

```
// Setup code (optional)
// ...

===

// Declaration code (optional)
// ...

---

// Configuration code (required)
// ...
```

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

## <a name="assemblies"></a>Assemblies

In addition to NuGet packages you can also load assemblies. This is accomplished by using the `Assemblies` property. You can load all the assemblies in a directory with the `LoadDirectory(string path, SearchOption searchOption = SearchOption.AllDirectories)` method. The specified directory can either be relative to the active directory or absolute. You can also load a single assembly by location with the `LoadFile(string path)` method and by full name with the `Load(string name)` method. For example:

```
Assemblies
    .LoadDirectory(@"lib")
	.LoadFile(@"foo\bar.dll")
	.Load("SampleAssembly, Version=1.0.2004.0, Culture=neutral, PublicKeyToken=8744b20f8da049e3");
```

Keep in mind that system assemblies and others located in the GAC *must* be loaded by full name (including the version, public key token, etc.).

By default, the following assemblies are already loaded so you don't need to explicity specify them:
* `System`
* `System.Collections.Generic`
* `System.Linq`
* `System.Core`
* `Microsoft.CSharp`
* `System.IO`
* `System.Diagnostics`

Also note that all assemblies from the directory containing the Wyam executable (and all subdirectories) will also be scanned for module assemblies.

## Folders

You can configure the folders Wyam uses by setting `RootFolder`, `InputFolder`, and/or `OutputFolder` in the setup script.

# <a name="declarations"></a>Declarations
---

The code in your configuration is typically executed inside the context of an "invisible" class and method. This means that you can't bring namespaces into scope, create classes, declare helper methods, etc. If you need to do any of these things, place it above the configuration code and separate it with a line consisting entirely of one or more dashes (`---`). Any code above this line will be evaluated outside the scope of the configuration method or any class. This means that you can bring namespaces into scope with `using` statements, declare helper classes, etc. Note that this code is global, so if you want to declare helper methods, they'll have to be placed within a wrapper class.

```
// Declaration code

using System.IO;

public static class Helpers
{
	public string GetWriteExtension()
	{
		return ".html";
	}
}

---

// Configuration code

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

### Pipeline Names

Pipelines should be given names, which makes them easier to identify in trace messages and also makes them easier to refer to within templates (for example, to get all the documents generated by a previous pipeline). Just pass in the name of a pipeline as the first parameter to the `Add(...)` method. If no name is provided (as above), then pipelines will implicitly be given the names `Pipeline 1`, `Pipeline 2`, etc.

```
Pipelines.Add("Markdown",
	ReadFiles("*.md"),
    FrontMatter(Yaml()),
	Markdown(),
	WriteFiles(".html")
);
```

### Child Modules

Some modules also accept child modules as part of their processing. For example, the `FrontMatter` module accepts a child module to handle parsing whatever front matter content is found. This way, the same module can be used to recognize front matter without it having to worry about what kind of content the front matter contains (such as YAML or JSON). For example:

```
Pipelines.Add("Markdown",
	ReadFiles("*.md"),
    FrontMatter(Yaml()),
	Markdown(),
	WriteFiles(".html")
);
```

### Skipping Previously Processed Documents

If you know that a given pipeline doesn't use data from other pipelines and you'd like to prevent reprocessing of documents after the first pass, you can set the `processDocumentsOnce` flag. Under the hood, this looks for the first occurrence of a given `IDocument.Source` and then caches all final result documents that have the same source. On subsequent executions, if a document with a previously seen `IDocument.Source` is found *and it has the same content*, that document is removed from the module output and therefore won't get passed to the next module. At the end of the pipeline, all the documents from the first pass that have the same source as the removed one are added back to the result set (so later pipelines can still access them in the documents collection if needed). The `processDocumentsOnce` flag can be set when creating a pipeline:

```
Pipelines.Add("Markdown", true,
	ReadFiles("*.md"),
    FrontMatter(Yaml()),
	Markdown(),
	WriteFiles(".html")
);
```

### Explicit Module Instantiation

Note that you supply the pipeline with new instances of each module. An astute reader will notice that in the example above, modules are being specified with what look like global methods. These methods are just shorthand for the actual module class constructors and this convention can be used for any module within the configuration script. The example configuration above could also have been written as:
```
Pipelines.Add("Markdown",
	new ReadFiles("*.md"),
	new Markdown(),
	new WriteFiles(".html")
);
```

### Automatic Lambda Generation

Many modules accept functions so that you can use information about the current `IExecutionContext` and/or `IDocument` when executing the module. For example, you may want to write files to disk in different locations depending on some value in each document's metadata. To make this easier in simple cases, and to assist users who may not be familiar with the [C# lambda expression syntax](https://msdn.microsoft.com/en-us/library/bb397687.aspx), the configuration file will automatically generate lambda expressions when using a special syntax. This generation will only happen for module constructors and fluent configuration methods. Any other method you use that requires a function will still have to specify it explicitly.

If the module or fluent configuration method has a `ContextConfig` delegate argument, you can instead use any variable name that starts with `@ctx`. For example:

```
Foo(@ctx2.InputFolder)
```
will be expanded to:

```
Foo(@ctx2 => @ctx2.InputFolder)
```

Likewise, any variable name that starts with `@doc` will be expanded to a `DocumentConfig` delegate. For example:

```
Foo(@doc["SomeMetadataValue"])
```
will be expanded to:

```
Foo((@doc, _) => @doc["SomeMetadataValue"])
```

If you use both `@ctx` and `@doc`, a `DocumentConfig` delegate will be generated that uses both values. For example:

```
Foo(@doc[@ctx.InputFolder])
```
will be expanded to:

```
Foo((@doc, @ctx) => @doc[@ctx.InputFolder])
```

## Folders

You can access the folders Wyam uses by getting `RootFolder`, `InputFolder`, and/or `OutputFolder` in the configuration script.

## Execution Ordering

Be aware that the configuration file only *configures* the pipelines. Each pipeline is executed in the order in which they were first added after the entire configuration file is evaluated. This means that you can't declare one pipeline, then declare another, and then add a new module to the first pipeline expecting it to reflect what happened in the second one. The second pipeline won't execute until the entire first pipeline is complete, including any modules that were added to it after the second one was declared. If you need to run some modules, switch to a different pipeline, and the perform additional processing on the first set of documents, look into the [Documents](/modules/documents) module.

# Example
---

The full configuration file for this documentation site is given below as an example:

```
// Setup code
Packages
	.Install("Twitter.Bootstrap.Less", "[3.3.5]")
	.Install("jQuery", "[2.1.1]");
	
===
// Configuration code

Pipelines.Add("Content",
	ReadFiles("*.md"),
	FrontMatter(Yaml()),
	Markdown(),
	Replace("<pre><code>", "<pre class=\"prettyprint\"><code>"),
	Concat(
		ReadFiles("*.cshtml").Where(x => Path.GetFileName(x)[0] != '_'),
		FrontMatter(Yaml())		
	),
	Razor(),
	AutoLink(c => c.Documents
		.FromPipeline("Content")
		.Where(x => x.String("RelativeFileDir") == "api" && !string.IsNullOrWhiteSpace(x.String("Title")))
		.ToDictionary(x => x.String("Title"), x => PathHelper.ToRootLink(Path.ChangeExtension(x.String("RelativeFilePath"), ".html")))),
	WriteFiles(".html")
);

Pipelines.Add("Less",
    ReadFiles("master.less"),
    Concat(ReadFiles("bootstrap.less")),
    Less(),
    WriteFiles(".css")
);

Pipelines.Add("Resources",
	CopyFiles("*").Where(x => 
		Path.GetExtension(x) != ".cshtml" 
		&& Path.GetExtension(x) != ".md"
		&& Path.GetExtension(x) != ".less")
);
```