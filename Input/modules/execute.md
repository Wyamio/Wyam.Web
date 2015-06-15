Title: Execute
Description: Executes custom code for each input document.
---
This module lets you execute custom code for each input document. It's a way to customize pipeline execution without having to write an entire module.

# Usage
---

  - `Execute(Func<IDocument, IEnumerable<IDocument>> execute)`
  
    This function is evaluated once for every input document. The return value should be an enumerable of output documents for each input document. If you want to execute some code and just pass the documents through, you should return an array with the input document as the single value. Otherwise, use `IDocument.Clone(IEnumerable<KeyValuePair<string, object>> items = null)` to create new documents with additional metadata (and the same content) and/or `IDocument.Clone(string content, IEnumerable<KeyValuePair<string, object>> items = null)` to create new documents with new content and additional metadata. 