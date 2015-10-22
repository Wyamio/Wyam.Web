Title: PathHelper
Description: Static methods to make working with paths easier.
---
This is a helper class that contains static methods to make working with paths easier.

# Members
---

## Methods

- `static string NormalizePath(string path)`

  Converts both types of slashes in `path` to `Path.DirectorySeparatorChar`.
  
- `static string ToLink(string path)`

  Converts `path` into a string suitable for use in HTML by replacing forward slashes with backslashes.
  
- `static static string ToRootLink(string path)`

  Converts `path` into a string suitable for use in HTML by replacing forward slashes with backslashes and also prepending a backslash.

- `static string GetRelativePath(string fromPath, string toPath)`
  
  This computes a relative path between the supplied paths. If both paths are rooted or they have no common root, the `toPath` is returned.
  
- `static string RemoveExtension(string path)`
  
  This removes the extension from a path or file. It returns the input path if no extension is present.