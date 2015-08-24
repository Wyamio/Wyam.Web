Title: Helpers
Description: Describes the available helper methods.
---
The `Wyam.Common` assembly contains several static helper classes that have useful methods.

# PathHelper
---

- `string NormalizePath(string path)`

  Converts both types of slashes in `path` to `Path.DirectorySeparatorChar`.
  
- `string ToLink(string path)`

  Converts `path` into a string suitable for use in HTML by replacing forward slashes with backslashes.
  
- `static string ToRootLink(string path)`

  Converts `path` into a string suitable for use in HTML by replacing forward slashes with backslashes and also prepending a backslash.

- `string GetRelativePath(string fromPath, string toPath)`
  
  This computes a relative path between the supplied paths. If both paths are rooted or they have no common root, the `toPath` is returned.
  
- `string RemoveExtension(string path)`
  
  This removes the extension from a path or file. It returns the input path if no extension is present.