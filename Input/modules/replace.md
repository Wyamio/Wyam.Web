Title: Replace
Description: Replaces a search string in the content of each input document with new content.
---
Replaces a search string in the content of each input document with new content.

# Usage
---

  - `Content(string search, object content)`
  
    Replaces all occurrences of the search string in every input document with the string value of the specified object.

  - `Content(string search, Func<IDocument, object> content)`
  
    Replaces all occurrences of the search string in every input document with the string value of the returned object. This allows you to specify different content for each document depending on the input.

  - `Content(string search, params IModule[] modules)`
  
    The specified modules are executed against an empty initial document and the results replace all occurrences of the search string in every input document (possibly creating more than one output document for each input document).
  
## Fluent Methods

Chain these methods together after the constructor to modify behavior.

  - `ForEachDocument()`
  
    If child modules are specified in the constructor, this method indicates that the chain of child modules should be independently evaluated for each input document, otherwise the chain of child modules are evaluated once and the single result applied to each input document. The default behavior has better performance, but this provides additional control and flexibility. This has no effect if no child modules were specified in the constructor.