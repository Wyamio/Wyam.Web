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