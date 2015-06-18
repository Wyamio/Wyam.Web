Title: Meta
Description: Sets metadata in each input document.
---
Adds the specified metadata to each input document.

# Usage
---

  - `Meta(string key, object metadata)`
  
    The specified object is added as metadata for the specified key for every input document.

  - `Meta(string key, Func<IDocument, object> metadata)`
  
    Uses a function to determine an object to be added as metadata for each document. This allows you to specify different metadata for each document depending on the input.

  - `Meta(params IModule[] modules)`
  
    The specified modules are executed against an empty initial document and all metadata that exists on the result documents from evaluating the entire child module chain are added as metadata to each input document.
  
## Fluent Methods

Chain these methods together after the constructor to modify behavior.

  - `ForEachDocument()`
  
    If child modules are specified in the constructor, this method indicates that the chain of child modules should be independently evaluated for each input document, otherwise the chain of child modules are evaluated once and the single result applied to each input document. The default behavior has better performance, but this provides additional control and flexibility. This has no effect if no child modules were specified in the constructor.