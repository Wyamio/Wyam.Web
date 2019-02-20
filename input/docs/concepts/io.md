Title: I/O
Description: Explains Wyam's powerful I/O abstraction.
Order: 8
RedirectFrom: getting-started/io
---

Wyam uses an IO abstraction layer designed to provide flexibility and consistency when dealing with files, directories, and path information.

# Paths and Files/Directories

Two types of classes are used, *paths* and *files/directories*. 

Path classes (`FilePath` and `DirectoryPath`) describe the location of a file or directory. Paths can be either absolute (I.e., starting from the root of a particular file system) or relative. They can be easily joined together and otherwise manipulated. Every absolute path also contains information about what *provider* the path is intended to be used with but are not directly tied to that provider (see below for information about providers).

File and directory classes (primarily implementations of `IFile` and `IDirectory`) point directly to a potential file
or directory within a given file system. They are usually obtained given a path that points to them. Each provider implements their own file and directory classes as appropriate for that provider. File and directory implementations often provide functionality for reading and writing files, creating directories, and otherwise manipulating the file system.

# Case Sensitivity

Different file systems have different rules about case sensitivity. Ideally, we'd be able to follow the rules of the underlying file system. However, because of the way Wyam uses virtual paths (see the discussion on input paths below), and because multiple file systems might be in use at once, we have to operate with the assumption that paths are case-sensitive. Otherwise, we would consider paths equivalent when they might not be. **All paths in Wyam are case sensitive.** That means that when you compare a `FilePath`, "foo.txt" is different from "Foo.txt". Note, however, that a given file provider *might* be case-insensitive in which case these paths would actually point to the same file.

# File Providers

A file provider acts as a factory for `IFile` and `IDirectory` instances. It's usually bundled with specific implementations of `IFile` and `IDirectory` for a given file source. For example, the default file provider operates on the local file system and returns file and directory instances that do the same. Other file providers may eventually include ones that provide access to zip files, embedded resources, GitHub repositories, or web-based resources.

All absolute `FilePath` and `DirectoryPath` paths must specify a file provider intended to handle the path and return `IFile` and `IDirectory` instances when requested. If one isn't explicitly specified, it's set to the default file provider. File providers are generally specified using a URI. The URI scheme indicates which provider should be used and any additional URI information like the host, query string, etc. is passed to the file provider to use as appropriate. Not all file providers use this extra information. For example, the default provider uses the `file` scheme but doesn't require or accept any additional information. For these kinds of providers, the URI should look like `file:///` (notice *three* slashes, the first two separate the scheme from the host information and the last one separates the host from the path and query and completes the URI). 

In addition to using a URI directly in the path constructor, there are other ways to specify which provider to use (though they all end up creating a URI under the hood). For one, you can delimit the provider from the path with a `|` character. You can also use a single URI for both the provider and the path and the two will be automatically separated when creating a `FilePath` or `DirectoryPath` (this is useful when relying on the implicit string-to-path conversion).

For example, consider a file provider designed to get information from a GitHub Gist. This example provider doesn't require any URI information beyond the `gist` scheme, so it's similar to our default `file` scheme. The provider then looks at the absolute path to determine which specific Gist to retrieve. All of the following ways of specifying the `gist` provider and the Gist ID of `9dac65b1ce1707550cb5f3bd9f7f9998` are equivalent:

- `"gist|/2721371adf6bf69fa833"` - provider portion is implicitly understood to be a scheme without a host or any other components if not a URI (translates to "gist:///").
- `"gist:///2721371adf6bf69fa833"` - if no provider is specified explicitly but the path itself is a valid URI, then the scheme, host, etc. become the provider and the path and query become the path. Note again that `///` is used and not `//` because a double slash would have indicated the gist ID was a host name and it would have become part of the provider URI, leaving an empty path.
- `new FilePath("gist", "/2721371adf6bf69fa833")`
- `new FilePath("gist:///", "/2721371adf6bf69fa833")`

Here are equivalent examples for a pretend `http` file provider that uses host names:
- `"http://foo.com|/a/b/c.txt"`
- `"http://foo.com/a/b/c.txt"`
- `new FilePath("http://foo.com", "/a/b/c.txt")`

And here are a couple more equivalent examples that don't use host names but do require a path as part of the provider:
- `"zip:///C:/foo/my.zip|/a/b/c.txt"`
- `new FilePath("zip:///C:/foo/my.zip", "/a/b/c.txt")`

Notice that any time the provider itself requires a path, you must either use the `|` delimiter or construct a path object. It's impossible to infer where the provider path ends and the actual path begins in a single URI in this case.

# Virtual File System

The link between paths, file providers, and files and paths is managed by a virtual file system available through the execution context as the `IExecutionContext.FileSystem` property. The virtual file system can also be accessed in your configuration file. It stores all the registered file providers as well as the various root, input, and output paths and provides methods to join them with relative paths to get `IFile` and `IDirectory` instances.

## Root Path

The root path is an absolute path that acts as a starting point for all other relative paths. It can be changed both from the command line and from the configuration file.

By default the root path is set to the path on the underlying file system from where you execute Wyam.

## Input Paths

Wyam uses multiple input paths that together comprise a virtual aggregated set of input files. This lets us do things like specify a set of canonical input files to use for a theme but then selectively override the theme files by putting replacements in an alternate input path with higher precedence.

Input paths are stored in an ordered list. When checking for files, the paths at the end of the list take precedence over those at the start of the list. For example, if path "A" is at index 0, path "B" is at index 1, and they both have a file named "foo.md", the one from path "B" will be used. Further, all paths are aggregated so searching for files or evaluating globbing expressions will consider all files and directories in all input paths. In the example above, getting all input files will result in a set of files from both path "A" and path "B" (with files of the same name from path "B" replacing those from path "A").

By default, the following input paths are set:

- [path to any NuGet packages with content folders]
- "theme"
- "input"

## Output Path

The output path is where Wyam will place output files by default. Note that many modules have the ability to manually specify an output path, so this behavior can be modified on a module by module basis.

By default the output path is set to "output".

# Globbing

Globbing (or globs) is a particular syntax for specifying files or directories using wildcards and other path-based search criteria. Wyam uses a sophisticated globbing engine to give you a lot of flexibility when searching for files. Even better, the globbing engine works with any file provider, be it the local file system or something else.

To demonstrate, let's assume the following files exist in our file provider:
- /a/x.txt
- /a/b/x.txt
- /a/b/y.md
- /c/z.txt
- /d/x.txt

The globbing engine supports the following syntax:
- `*`

  This represents any number of characters at a specific depth. For example, `/*/x.txt` will find:
  - /a/x.txt
  - /d/x.txt

  Note that a wildcard can also be used in the file name. For example, `/*/*.txt` will find:
  - /a/x.txt
  - /c/z.txt
  - /d/x.txt
  
- `**`

  This represents any number of characters at multiple depths. For example, `/**/x.txt` will find:
  - /a/x.txt
  - /a/b/x.txt
  - /d/x.txt
  
- `{,}`

  This represents multiple expansions for the pattern. For example, `/**/{y,z}.*` will find:
  - /a/b/y.md
  - /c/z.txt
  
  Leaving the last option blank indicates any match at that position. For example, `/{a,}/**/x.txt` will find:
  - /a/x.txt
  - /a/b/x.txt
  - /d/x.txt
  
- `!`

  This represents exclusion and is useful in combination with multiple expansions. For example, `/**/{*,!x}.txt` will find:
  - /c/z.txt

Note that relative globbing patterns are often evaluated from the perspective of your `input` folder and not necessarily from where your config file resides, especially if the pattern is telling Wyam where to find certain files for processing. If you're having problems and a globbing pattern isn't returning the files you think it should, try adjusting it to start from the `input` folder.

# Testing

Because the new IO abstraction includes support for virtual file systems, it can be used to greatly simplify testing your custom modules by providing files that don't actually have to exist on disk. Several classes in the `Wyam.Testing` library in the `Wyam.Testing.IO` namespace are provided to help with this.
