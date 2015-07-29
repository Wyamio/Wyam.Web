Title: IDocument
Description: The primary object in Wyam that contains content and metadata for each item as it propagates through the pipeline.
---
The primary object in Wyam that contains content and metadata for each item as it propagates through the pipeline. Documents are immutable so you must call one of the `Clone(...)` methods to create a new document. Implements `IMetadata` and all calls are passed through to the document's internal `Metadata` instance.

# Members
---
## Properties

  - `IMetadata Metadata { get; }`
  
    Gets the metadata associated with this document.
  
  - `string Content { get; }`
  
    Gets the content associated with this document.
  
## Methods
  
  - `IDocument Clone(string content, IEnumerable<KeyValuePair<string, object>> items = null)`
  
    Clones the current document with new content and additional metadata (all existing metadata is retained).
  
  - `IDocument Clone(IEnumerable<KeyValuePair<string, object>> items = null)`
  
    Clones the current document with identical content and additional metadata (all existing metadata is retained).