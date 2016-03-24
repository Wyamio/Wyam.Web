Title: I/O
Description: Explains Wyam's powerful I/O abstraction.
Order: 5
---

Wyam uses an IO abstraction layer designed to provide flexibility and consistency when dealing with files, directories, and path information.

# Paths vs. Files/Directories

Two types of classes are used, *paths* and *files/directories*. 

Path classes (primarily `FilePath` and `DirectoryPath`) describe the location of a file or directory. Paths can be either absolute (I.e., starting from the root of a particular file system) or relative. They can be easily joined together and otherwise manipulated. Every absolute path also contains information about what *provider* the path is intended to be used with but are not directly tied to that provider (see below for informaion about providers).

File and directory classes (primarily implementations of `IFile` and `IDirectory`) point directly to a potential file
or directory within a given file system. They are usually obtained given a path that points to them. Each provider implements their own file and directory classes as appropriate for that provider. File and directory implementations often provide functionality for reading and writing files, creating directories, and otherwise manipulating the file system.

# Case Sensitivity

# File Providers

# Virtual File System

The link between paths, file providers, and files and paths is managed by a virtual file system abailable through the execution context as a `FileSystem` property. The virtual file system can also be accessed in your configuration file. It stores all the registered file providers as well as the various root, input, and output paths and provides methods to join them with relative paths to get `IFile` and `IDirectory` instances.

## Root Path

## Input Paths

## Output Path

# <a name="globbing"></a>Globbing

# Testing