Title: Documents
Description: Replaces pipeline documents with previously processed ones
Category: Control
---
This module *copies* documents from other pipelines and places them into the current pipeline. Note that because this module does not remove the documents from their original pipeline it's likely you will end up with documents that have the same content and metadata in two different pipelines. This module does not include the input documents are part of it's output. If you want to concatenate the result of this module with the input documents, wrap it in the [Concat](/modules/concat) module. 

# Usage
---

  - `Document()`
  
    This outputs all existing documents from all pipelines (except the current one).
    
  - `Documents(string pipeline = null)`
  
    This outputs the documents from the specified pipeline.
    
  - `Documents(ContextConfig documents)`
  
    This will get documents based on the context. The delegate will only be called once, regardless of the number of input documents. The return value is expected to be a `IEnumerable<IDocument>`.
    
  - `Documents(DocumentConfig documents)`
  
    This will get documents based on each input document. The output will be the aggregate of all returned documents for each input document. The return value is expected to be a `IEnumerable<IDocument>`.
    
## Fluent Methods

Chain these methods together after the constructor to modify behavior.
  
  - `Where(DocumentConfig predicate)`
  
    Only documents that satisfy the predicate will be output. The return value is expected to be a `bool`.