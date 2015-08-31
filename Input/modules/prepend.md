Title: Prepend
Description: Prepends content to the document.
Category: Content
---
Prepends the specified content to the existing content of each document.

# Usage
---

  - `Prepend(object content)`
  
    Prepends the string value of the specified object to the content of every input document.
    
  - `Prepend(Func<IExecutionContext, object> content)`
  
    Prepends the string value of the returned object to to content of each document. This allows you to specify different content to prepend depending on the execution context.

  - `Prepend(Func<IDocument, IExecutionContext, object> content)`
  
    Prepends the string value of the returned object to to content of each document. This allows you to specify different content to prepend for each document depending on the input document.

  - `Prepend(params IModule[] modules)`
  
    The specified modules are executed against an empty initial document and the results are prepended to the content of every input document (possibly creating more than one output document for each input document).