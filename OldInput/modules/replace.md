Title: Replace
Description: Replaces a search string in the content of each input document with new content.
Category: Content
---
Replaces a search string in the content of each input document with new content.

# Usage
---

  - `Replace(string search, object content)`
  
    Replaces all occurrences of the search string in every input document with the string value of the specified object.

  - `Replace(string search, ContextConfig content)`
  
    Replaces all occurrences of the search string in every input document with the string value of the returned object. This allows you to specify different content depending on the execution context.

  - `Replace(string search, DocumentConfig content)`
  
    Replaces all occurrences of the search string in every input document with the string value of the returned object. This allows you to specify different content for each document depending on the input document.

  - `Replace(string search, params IModule[] modules)`
  
    The specified modules are executed against an empty initial document and the results replace all occurrences of the search string in every input document (possibly creating more than one output document for each input document).