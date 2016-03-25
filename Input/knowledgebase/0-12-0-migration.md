Title: 0.12.0 Migration
Description: How to migrate existing sites to the new IO abstraction in 0.12.0.
---

Here are some general notes:
- All paths are now case sensitive.
- Modules now use [globbing patterns](/getting-started/io#globbing) instead of simple file search patterns.
- Methods to manually specify search depth and apply conditions that globbing can cover (such as limiting to specific extensions) have generally been removed.
- Path metadata set by modules such as `ReadFiles` is now typed as `FilePath` or `DirectoryPath`.
  - You can still easily get a `string` by using `IMetadata.String()` but direct casts to `string` (I.e., `(string)document["key"]`) may fail.

# Modules

Several modules that interact with the file system have been updated. An attempt was made to either retain backwards compatibility or provide temporary legacy modules that would work the same way the old ones did. However, the differences between the native `System.IO` classes we had been using and the new multi-provider IO abstraction were just too great. Instead of spending lots of effort writing code specifically to support backwards compatibility, the decision was made to make a clean break and update the existing modules in place. The section below describes the most important changes and how to adapt your site.

## ReadFiles

The `ReadFiles` constructor now accepts one or more [globbing patterns](/getting-started/io#globbing). This easily covers everything the old version used to, plus enables much more powerful file searching. For example, instead of using a pattern of "\*.md" and `.FromAllDirectories()` you can just use the globbing pattern "\*\*/\*.md". Because the globbing patterns cover recursion, the methods to manually specify depth have been removed. The method for limiting extensions has also been removed as that can be represented in the globbing patterns.

In addition, many situations that would have required a predicate with `.Where()` can now rely on globbing patterns. For example, `ReadFiles("*.cshtml").Where(x => System.IO.Path.GetFileName(x)[0] != '_')` can now be written as `ReadFiles("{!_,}*.cshtml")`. Because the globbing patterns work differently than the old style patterns, it's recommended that you read and understand them before migrating your modules.

`ReadFiles.Where()` has been modified to pass an `IFile` instead of just a string path. If you need your predicate to operate on the path, just look at `IFile.Path`. This will allow more complex conditional reads, such as when the file was created.

## CopyFiles

The changes to `CopyFiles` are similar to those for `ReadFiles`. The constructor now accepts one or more [globbing patterns](/getting-started/io#globbing) and the methods to manually specify depth have been removed. The method for limiting extensions has also been removed as that can be represented in the globbing patterns. `CopyFiles.Where()` has been modified to pass an `IFile` instead of just a string path.

## Rss

The main changes to the `Rss` module are that the constructor is now a little different and accepts a site root URI (which can be specified as either a `Uri` or a `string` by using different constructor overloads) and a relative path to the destination RSS file. This allows you to use the module with sites where the site exists at a subpath of the URI.

In addition, the link customizer is now a `Func<FilePath, FilePath>` instead of a `Func<string, string>`.