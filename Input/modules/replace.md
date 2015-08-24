Title: Replace
Description: Replaces a search string in the content of each input document with new content.
---
Replaces a search string in the content of each input document with new content.

# Usage
---

  - `Content(string search, object content)`
  
    Replaces all occurrences of the search string in every input document with the string value of the specified object.

  - `Content(string search, Func<IDocument, object> content)`
  
    Replaces all occurrences of the search string in every input document with the string value of the returned object. This allows you to specify different content for each document depending on the input.

  - `Content(string search, params IModule[] modules)`
  
    The specified modules are executed against an empty initial document and the results replace all occurrences of the search string in every input document (possibly creating more than one output document for each input document).