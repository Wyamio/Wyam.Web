Title: ReplaceIn
Description: Replaces a search string in the specified content with the content of an input document.
---
Replaces a search string in the specified content with the content of an input document. This module is very useful for simple template substitution.

# Usage
---

  - `Content(string search, object content)`
  
  Replaces all occurrences of the search string in the string value of the specified object with the content of each input document.

  - `Content(string search, Func<IDocument, object> content)`
  
  Replaces all occurrences of the search string in the string value of the returned object with the content of each input document. This allows you to specify different content for each document depending on the input.

  - `Content(string search, params IModule[] modules)`
  
  The specified modules are executed against an empty initial document and all occurrences of the search string in the result(s) are replaced by the content of each input document (possibly creating more than one output document for each input document).
  
## Fluent Methods

Chain these methods together after the constructor to modify behavior.

  - `ForEachDocument()`
  
  If child modules are specified in the constructor, this method indicates that the chain of child modules should be independently evaluated for each input document, otherwise the chain of child modules are evaluated once and the single result applied to each input document. The default behavior has better performance, but this provides additional control and flexibility. This has no effect if no child modules were specified in the constructor.