Title: Append
Description: Appends content to the document.
Category: Content
---
Appends the specified content to the existing content of each document.

# Usage
---

  - `Append(object content)`
  
    Appends the string value of the specified object to the content of every input document.

  - `Append(ContextConfig content)`
  
    Appends the string value of the returned object to to content of each document. This allows you to specify different content to append depending on the execution context.

  - `Append(DocumentConfig content)`
  
    Appends the string value of the returned object to to content of each document. This allows you to specify different content to append for each document depending on the input document.

  - `Append(params IModule[] modules)`
  
    The specified modules are executed against an empty initial document and the results are appended to the content of every input document (possibly creating more than one output document for each input document).