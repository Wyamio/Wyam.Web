Title: Content
Description: Sets the content of each input document.
Category: Content
---
Replaces the content of each input document with the string value of the specified content object. In the case where modules are provided, they are executed against an empty initial document and the results are applied to each input document.

# Usage
---

  - `Content(object content)`
  
    Uses the string value of the specified object as the new content for every input document.

  - `Content(Func<IExecutionContext, object> content)`
  
    Uses the string value of the returned object as the new content for each document. This allows you to specify different content depending on the execution context.

  - `Content(Func<IDocument, IExecutionContext, object> content)`
  
    Uses the string value of the returned object as the new content for each document. This allows you to specify different content for each document depending on the input document.

  - `Content(params IModule[] modules)`
  
    The specified modules are executed against an empty initial document and the results are applied to every input document (possibly creating more than one output document for each input document).