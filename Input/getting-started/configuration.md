Title: Configuration
Description: Describes the format of the configuration file.
Order: 4
---
The command line Wyam application reads a configuration file typically named `config.wyam` (though you can change that with an argument) that sets up the environment and configures the pipelines. It consists of two sections: The *declaration* section and the *body*, in that order. The declaration section is optional and is separated by a line consisting entirely of one or more dashes (`---`). Preprocessor directives can also appear anywhere in the configuration file, though they are always evaluated before processing the rest of the file (by convention they're usually at the top of the file).

The sections of the configuration file are evaluated as C# code, so you can make use of the full C# language and the entire .NET ecosystem. However, it's not necessary to know C# to write Wyam configuration files. The syntax has been carefully crafted to be usable by anyone no matter their level of programming experience. Some extra pre-processing is also done to the file to make certain code easier to write (which actually makes the syntax a superset of C#, though this extra magic is entirely optional).

A configuration file looks like this:

```
// Preprocessor directives (optional)

// Declaration code (optional)
// ...

---

// Body code (required)
// ...
```

# <a name="Preprocessor"></a>Preprocessor Directives
---

Preprocessor directives establish the Wyam environment and get evaluated before the rest of the configuration file. They're typically responsible for declaring things like NuGet packages and assemblies. Every preprocessor directive starts with `#` at the beginning of a line and extend for the rest of the line. The following directives are available (the current set of directives can always be seen by calling `wyam.exe --help-directives`).

```
Adds a reference to an assembly by name.:
#assembly-name, #an
    <assembly>    The assembly to load by name.

Adds a reference to an assembly by file name or globbing pattern.:
#assembly, #a
    <assembly>    The assembly to load by file or globbing pattern.

Specifies an additional package source to use.:
#nuget-source, #ns
    <source>    The package source to add.

Adds a NuGet package (downloading and installing it if needed).:
#nuget, #n
    -p, --prerelease         Specifies that prerelease packages are
                             allowed.
    -u, --unlisted           Specifies that unlisted packages are
                             allowed.
    -v, --version <arg>      Specifies the version of the package to
                             use.
    -s, --source <arg>...    Specifies the package source(s) to get the
                             package from.
    -e, --exclusive          Indicates that only the specified package
                             source(s) should be used to find the
                             package.
    <package>                The package to install.
```

## <a name="nuget"></a>NuGet Packages

Any NuGet packages you specify in preprocessor directives are installed and then scanned for modules which are made available to the main configuration body.

Note that many modules require their package to be installed before they can be used. For example, to make use of the [Markdown](/modules/markdown) module, you must install the `Wyam.Modules.Markdown` package. To do this, you would add the following to your configuration file (the `-p` indicates this is a prerelease package, which currently applies to all the Wyam packages):

```
#n -p Wyam.Modules.Markdown
``` 

You can also specify the special `Wyam.Modules.All` package which will download all of the official Wyam module packages at once:

```
#n -p Wyam.Modules.All
```

## <a name="assemblies"></a>Assemblies

In addition to NuGet packages you can also load assemblies. You can load all the assemblies in a directory by using a [globbing pattern](/getting-started/io#globbing) with the `#assembly` directive, or by specifying a relative or absolute path to the assembly. You can also load assemblies by name with the `#assembly-name` directive. Keep in mind that system assemblies and others located in the GAC *must* be loaded by full name (including the version, public key token, etc.).

By default, the following assemblies are already loaded so you don't need to explicitly specify them:
* `System`
* `System.Collections.Generic`
* `System.Linq`
* `System.Core`
* `Microsoft.CSharp`
* `System.IO`
* `System.Diagnostics`

# <a name="declarations"></a>Declarations
---

The code in your configuration is typically executed inside the context of an "invisible" class and method. This means that you can't bring namespaces into scope, create classes, declare helper methods, etc. If you need to do any of these things, place it above the configuration code and separate it with a line consisting entirely of one or more dashes (`---`). Any code above this line will be evaluated outside the scope of the configuration method or any class. This means that you can bring namespaces into scope with `using` statements, declare helper classes, etc. Note that this code is global, so if you want to declare helper methods, they'll have to be placed within a wrapper class.

```
// Declaration code

using System.IO;

public static class Helpers
{
    public static string GetWriteExtension()
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

# <a name="body"></a>Body
---

## Initial Metadata

Each pipeline starts with a single document prepopulated with initial metadata you specify in the configuration file. You can use this facility to introduce variables that can influence the way the pipeline behaves. To set initial metadata, just add values to the `InitialMetadata` property (it's of type `IInitialMetadata` which implements `IDictionary<string, object>`). Note that the dictionary values are objects, so you can store simple primitive values or complex structure. For example:

```
InitialMetadata["Foo"] = "Bar";
InitialMetadata.Add("Baz", new KeyValuePair<string, string>("abc", "xyz")); 
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

Pipelines should be given names, which makes them easier to identify in trace messages and also makes them easier to refer to within templates (for example, to get all the documents generated by a previous pipeline). Just pass in the name of a pipeline as the first parameter to the `Add()` method. If no name is provided (as above), then pipelines will implicitly be given the names `Pipeline 1`, `Pipeline 2`, etc.

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

## Execution Ordering

Be aware that the configuration file only *configures* the pipelines. Each pipeline is executed in the order in which they were first added after the entire configuration file is evaluated. This means that you can't declare one pipeline, then declare another, and then add a new module to the first pipeline expecting it to reflect what happened in the second one. The second pipeline won't execute until the entire first pipeline is complete, including any modules that were added to it after the second one was declared. If you need to run some modules, switch to a different pipeline, and the perform additional processing on the first set of documents, look into the [Documents](/modules/documents) module.
