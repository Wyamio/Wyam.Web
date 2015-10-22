Title: Execute
Description: Executes custom code for each input document.
Category: Extensibility
---
This module lets you execute custom code for each input document. It's a way to customize pipeline execution without having to write an entire module.

# Usage
---
   
  - `Execute(DocumentConfig execute)`
  
    This function is evaluated once for every input document. The return value should be a `IEnumerable<IDocument>`. If you want to execute some code and just pass the documents through, you should return an array with the input document as the single value. Otherwise, use `IDocument.Clone(IEnumerable&lt;KeyValuePair&lt;string, object&gt;&gt; items = null)` to create new documents with additional metadata (and the same content) and/or `IDocument.Clone(string content, IEnumerable&lt;KeyValuePair&lt;string, object&gt;&gt; items = null)` to create new documents with new content and additional metadata.  