Title: I/O
Description: Explains Wyam's powerful I/O abstraction.
Order: 5
---

Wyam uses an IO abstraction layer designed to provide flexibility and consistency when dealing with files, directories, and path information.

# Paths and Files/Directories

Two types of classes are used, *paths* and *files/directories*. 

Path classes (primarily `FilePath` and `DirectoryPath`) describe the location of a file or directory. Paths can be either absolute (I.e., starting from the root of a particular file system) or relative. They can be easily joined together and otherwise manipulated. Every absolute path also contains information about what *provider* the path is intended to be used with but are not directly tied to that provider (see below for informaion about providers).

File and directory classes (primarily implementations of `IFile` and `IDirectory`) point directly to a potential file
or directory within a given file system. They are usually obtained given a path that points to them. Each provider implements their own file and directory classes as appropriate for that provider. File and directory implementations often provide functionality for reading and writing files, creating directories, and otherwise manipulating the file system.

# <a name="case-sensitivity"></a>Case Sensitivity

Different file systems have different rules about case sensitivity. Ideally, we'd be able to follow the rules of the underlying file system. However, because of the way Wyam uses virtual paths (see the discussion on input paths below), and because multiple file systems might be in use at once, we have to operate with the assumption that paths are case-sensitive. Otherwise, we would consider paths equivalent when they might not be. **All paths in Wyam are case sensitive.** That means that when you compare a `FilePath`, "foo.txt" is different from "Foo.txt". Note, however, that a given file provider *might* be case-insensitive in which case these paths would actually point to the same file.

# File Providers

A file provider acts as a factory for `IFile` and `IDirectory` instances. It's usually bundled with specific implementations of `IFile` and `IDirectory` for a given file source. For example, the default file provider operates on the local file system and returns file and directory instances that do the same. Other file providers may include ones that provide access to zip files, embedded resources, GitHub repositories, or web-based resources. A file provider may or may not require additional configuration in it's constructor (for example, the name of a user and repository for a GitHub file provider).

All absolute paths must specify a file provider intended to handle the path. If one isn't explicitly specified, it's set to the default file provider (which is typically the local file system). Providers are refered to by name, and the default provider is represented by an empty string. You can also manually specify the file provider by using an alternate constructor for a `FilePath` or `DirectoryPath` or by using the special `"provider::/my/path"` syntax.

# Virtual File System

The link between paths, file providers, and files and paths is managed by a virtual file system available through the execution context as the `FileSystem` property. The virtual file system can also be accessed in your configuration file. It stores all the registered file providers as well as the various root, input, and output paths and provides methods to join them with relative paths to get `IFile` and `IDirectory` instances.

## Root Path

The root path is an absolute path that acts as a starting point for all other relative paths. It can be changed both from the command line and from the configuration file.

By default the root path is set to the path on the underlying file system from where you execute Wyam.

## Input Paths

Wyam uses multiple input paths that together comprise a virtual aggregated set of input files. This lets us do things like specify a set of cannonical input files to use for a theme but then selectivly override the theme files by putting replacements in an alternate input path with higher precedence.

Input paths are stored in an ordered list. When checking for files, the paths at the end of the list take precedence over those at the start of the list. For example, if path "A" is at index 0, path "B" is at index 1, and they both have a file named "foo.md", the one from path "B" will be used. Further, all paths are aggregated so searching for files or evaluating globbing experessions will consider all files and directories in all input paths. In the example above, getting all input files will result in a set of files from both path "A" and path "B" (with files of the same name from path "B" replacig those from path "A").

By default, the following input paths are set:

- [path to any NuGet packages with content folders]
- "theme"
- "input"

## Output Path

The output path is where Wyam will place output files by default. Note that many modules have the ability to manually specify an output path, so this behavior can be modified on a module by module basis.

By default the output path is set to "output".

# <a name="globbing"></a>Globbing

# Testing