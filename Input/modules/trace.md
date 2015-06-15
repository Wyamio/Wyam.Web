Title: Trace
Description: Outputs trace messages during execution.
---
Outputs trace messages during execution. This module has no effect on documents and the input documents are passed through to output documents.

# Usage
---

  - `Trace(object content)`
  
    Outputs the string value of the specified object to trace.

  - `Trace(Func<IDocument, object> content)`
  
    Outputs the string value of the returned object to trace. This allows you to trace different content for each document depending on the input.

  - `Trace(params IModule[] modules)`
  
    The specified modules are executed against an empty initial document and the resulting document content is output to trace.
  
## Fluent Methods

Chain these methods together after the constructor to modify behavior.

  - `ForEachDocument()`
  
    If child modules are specified in the constructor, this method indicates that the chain of child modules should be independently evaluated for each input document, otherwise the chain of child modules are evaluated once and the single result applied to each input document. The default behavior has better performance, but this provides additional control and flexibility. This has no effect if no child modules were specified in the constructor.