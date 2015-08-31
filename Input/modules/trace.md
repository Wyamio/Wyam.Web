Title: Trace
Description: Outputs trace messages during execution.
Category: Extensibility
---
Outputs trace messages during execution. This module has no effect on documents and the input documents are passed through to output documents.

# Usage
---

  - `Trace(object content)`
  
    Outputs the string value of the specified object to trace.

  - `Trace(Func<IExecutionContext, object> content)`
  
    Outputs the string value of the returned object to trace. This allows you to trace different content depending on the execution context.

  - `Trace(Func<IDocument, IExecutionContext, object> content)`
  
    Outputs the string value of the returned object to trace. This allows you to trace different content for each document depending on the input document.

  - `Trace(params IModule[] modules)`
  
    The specified modules are executed against an empty initial document and the resulting document content is output to trace.