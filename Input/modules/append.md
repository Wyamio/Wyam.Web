Title: Append
Description: Appends content to the document.
---
Appends the specified content to the existing content of each document.

# Usage
---

  - `Content(object content)`
  
    Appends the string value of the specified object to the content of every input document.

  - `Content(Func<IDocument, object> content)`
  
    Appends the string value of the returned object to to content of each document. This allows you to specify different content to append for each document depending on the input.

  - `Content(params IModule[] modules)`
  
    The specified modules are executed against an empty initial document and the results are appended to the content of every input document (possibly creating more than one output document for each input document).
  
## Fluent Methods

Chain these methods together after the constructor to modify behavior.

  - `ForEachDocument()`
  
    If child modules are specified in the constructor, this method indicates that the chain of child modules should be independently evaluated for each input document, otherwise the chain of child modules are evaluated once and the single result applied to each input document. The default behavior has better performance, but this provides additional control and flexibility. This has no effect if no child modules were specified in the constructor.