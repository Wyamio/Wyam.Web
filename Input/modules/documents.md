Title: Documents
Description: Replaces pipeline documents with previously processed ones
Category: Control
---
This module *copies* documents from other pipelines and places them into the current pipeline. Note that because this module does not remove the documents from their original pipeline it's likely you will end up with documents that have the same content and metadata in two different pipelines. Unlike [ConcatDocuments](/modules/concatdocuments), this module does not include the input documents are part of it's output.

# Usage
---

  - `Documents(string pipeline = null)`
  
    If `pipeline` is null then all existing documents from all pipelines (except the current one) will be output. Otherwise, the documents from the specified pipeline will be output.
    
## Fluent Methods

Chain these methods together after the constructor to modify behavior.
  
  - `Where(Func<IDocument, IExecutionContext, bool> predicate)`
  
    Only documents that satisfy the predicate will be output.