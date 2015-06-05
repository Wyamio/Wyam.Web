Title: Helpers
---
The `Wyam.Core.Helpers` namespaces contains several static helper classes that have useful methods.

# PathHelper
---

* `string GetRelativePath(string fromPath, string toPath)`
  
  This computes a relative path between the supplied paths. If both paths are rooted or they have no common root, the `toPath` is returned.
  
* `string RemoveExtension(string path)`
  
  This removes the extension from a path or file. It returns the input path if no extension is present.