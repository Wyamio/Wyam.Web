Title: Content
Description: Sets the content of each input document.
---
Replaces the content of each input document with the string value of the specified content object. In the case where modules are provided, they are executed against an empty initial document and the results are applied to each input document.

# Usage
---

  - `Content(object content)`
  
    Uses the string value of the specified object as the new content for every input document.

  - `Content(Func<IDocument, object> content)`
  
    Uses the string value of the returned object as the new content for each document. This allows you to specify different content for each document depending on the input.

  - `Content(params IModule[] modules)`
  
    The specified modules are executed against an empty initial document and the results are applied to every input document (possibly creating more than one output document for each input document).
  
## Fluent Methods

Chain these methods together after the constructor to modify behavior.

  - `ForEachDocument()`
  
    If child modules are specified in the constructor, this method indicates that the chain of child modules should be independently evaluated for each input document, otherwise the chain of child modules are evaluated once and the single result applied to each input document. The default behavior has better performance, but this provides additional control and flexibility. This has no effect if no child modules were specified in the constructor.