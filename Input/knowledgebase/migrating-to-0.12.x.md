Title: Migrating to 0.12.x
Description: How to migrate existing sites to the new IO abstraction in 0.12.x.
---

The 0.12.x series of releases is a chance to "reset" some things that worked well when this was just a smaller project but need to change as Wyam serves more use cases. These changes will set the project up for a number of new features in the 0.13.x releases such as recipes and themes.

# <a name="0.12.1"></a>0.12.1
---

The major breaking change in 0.12.1 is that every module is no longer included with the core distribution. This was necessary because as the number of modules has increased, the distribution size has as well, and it was getting to the point that the core distribution was becoming larger than desired for simple use cases. You will now need to declare any non-core modules using preprocessor directives in your configuration file:

```
#n -p Wyam.Modules.Markdown
```

Alternatively, you can download a meta-package that contains references to all official Wyam module packages:

```
#n -p Wyam.Modules.All
```

# <a name="0.12.0"></a>0.12.0
---

In general, the steps to migrate look like the following:
- Modify the paths used with `ReadFiles` and `CopyFiles` to use [globbing patterns](/getting-started/io#globbing) and honor [case sensitivity](/getting-started/io#case-sensitivity).
- Change uses of `IMetadata.Link()` to `IExecutionContext.GetLink()` in templates and helper methods.
- Change uses of `PathHelper` to instance methods of `FilePath` and `DirectoryPath`.

Here is a little more detail about the changes:
- All paths are now case sensitive (see [case sensitivity](/getting-started/io#case-sensitivity) for a discussion of why).
- Modules now use [globbing patterns](/getting-started/io#globbing) instead of simple file search patterns.
- Methods to manually specify search depth and apply conditions that globbing can cover (such as limiting to specific extensions) have generally been removed.
- Path metadata set by modules such as `ReadFiles` is now typed as `FilePath` or `DirectoryPath`.
  - You can still easily get a `string` by using `IMetadata.String()` but direct casts to `string` (I.e., `(string)document["key"]`) may fail.
- `IDocument.Source` is now a `FilePath` and must be absolute (modules should use the static string `NormalizedPath.AbstractProvider` as the provider for a source path if the document was generated internally).
- `PathHelper` is no longer available, use instance methods of `FilePath` and `DirectoryPath` instead.
- `IMetadata.Link()` has been moved to `ExecutionContext.GetLink()` and link settings like host and root path can be globally controlled with `OutputSettings`

## Modules

Several modules that interact with the file system have been updated. An attempt was made to either retain backwards compatibility or provide temporary legacy modules that would work the same way the old ones did. However, the differences between the native `System.IO` classes we had been using and the new multi-provider IO abstraction were just too great. Instead of spending lots of effort writing code specifically to support backwards compatibility, the decision was made to make a clean break and update the existing modules in place. The section below describes the most important changes and how to adapt your site.

### ReadFiles

The `ReadFiles` constructor now accepts one or more [globbing patterns](/getting-started/io#globbing). This easily covers everything the old version used to, plus enables much more powerful file searching. For example, instead of using a pattern of "\*.md" and `.FromAllDirectories()` you can just use the globbing pattern "\*\*/\*.md". Because the globbing patterns cover recursion, the methods to manually specify depth have been removed. The method for limiting extensions has also been removed as that can be represented in the globbing patterns.

In addition, many situations that would have required a predicate with `.Where()` can now rely on globbing patterns. For example, `ReadFiles("*.cshtml").Where(x => System.IO.Path.GetFileName(x)[0] != '_')` can now be written as `ReadFiles("{!_,}*.cshtml")`. Because the globbing patterns work differently than the old style patterns, it's recommended that you read and understand them before migrating your modules.

`ReadFiles.Where()` has been modified to pass an `IFile` instead of just a string path. If you need your predicate to operate on the path, just look at `IFile.Path`. This will allow more complex conditional reads, such as when the file was created.

### CopyFiles

The changes to `CopyFiles` are similar to those for `ReadFiles`. The constructor now accepts one or more [globbing patterns](/getting-started/io#globbing) and the methods to manually specify depth have been removed. The method for limiting extensions has also been removed as that can be represented in the globbing patterns. `CopyFiles.Where()` has been modified to pass an `IFile` instead of just a string path.

### Rss and Sitemap

The link/location customizers are now `Func<FilePath, FilePath>` instead of a `Func<string, string>`.