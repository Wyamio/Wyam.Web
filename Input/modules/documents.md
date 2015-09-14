Title: Documents
Description: Inserts documents into the current pipeline, either by replacing pipeline documents with previously processed ones or by creating new ones.
Category: Control
---
This module inserts documents into the current pipeline, either by replacing pipeline documents with previously processed ones or by creating new ones. If getting previously processed documents from another pipeline, this module *copies* the documents and places them into the current pipeline. Note that because this module does not remove the documents from their original pipeline it's likely you will end up with documents that have the same content and metadata in two different pipelines. This module does not include the input documents as part of it's output. If you want to concatenate the result of this module with the input documents, wrap it with the [Concat](/modules/concat) module. 

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
    
  - `Documents(int count)`
  
    This generates `count` number of new empty documents.
    
  - `Documents(params string[] content)`
  
    This generates new documents with the specified content.
    
  - `Documents(params IEnumerable<KeyValuePair<string, object>>[] metadata)`
  
    This generates new documents with the specified metadata.
    
  - `Documents(params Tuple<string, IEnumerable<KeyValuePair<string, object>>>[] contentAndMetadata)`
  
    This generates new documents with the specified content and metadata.
    
## Fluent Methods

Chain these methods together after the constructor to modify behavior.
  
  - `Where(DocumentConfig predicate)`
  
    Only documents that satisfy the predicate will be output. The return value is expected to be a `bool`.