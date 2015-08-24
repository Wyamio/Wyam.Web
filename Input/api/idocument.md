Title: IDocument
Description: The primary object in Wyam that contains content and metadata for each item as it propagates through the pipeline.
---
The primary object in Wyam that contains content and metadata for each item as it propagates through the pipeline. Documents are immutable so you must call one of the `Clone(...)` methods to create a new document. Implements `IMetadata` and all metadata calls are passed through to the document's internal `Metadata` instance. Note that both `Content` and `Stream` properties are guaranteed not to be `null`. When a document is created, either a `string` or a `Stream` is provided. Whenever the other of the two is requested, the system will convert the current representation for you.

# Members
---
## Properties

  - `string Source { get; }`
  
    An identifier for the document meant to reflect the source of the data. These should be unique (such as file name).
  
  - `IMetadata Metadata { get; }`
  
    Gets the metadata associated with this document.
  
  - `string Content { get; }`
  
    Gets the content associated with this document.
    
  - `Stream Stream { get; }`
  
    Gets a stream for the document content. **Take care not to dispose the returned `Stream` object as it may be reused later.**
  
## Methods
  
  - `IDocument Clone(string source, string content, IEnumerable<KeyValuePair<string, object>> items = null)`
  
    Clones the current document with a new source, new content, and additional metadata (all existing metadata is retained).
    
  - `IDocument Clone(string content, IEnumerable<KeyValuePair<string, object>> items = null)`
  
    Clones the current document with new content and additional metadata (all existing metadata is retained).
    
  - `IDocument Clone(string source, Stream stream, IEnumerable<KeyValuePair<string, object>> items = null, bool disposeStream = true)`
  
    Clones the current document with a new source, new stream, and additional metadata (all existing metadata is retained). If `disposeStream` is true (which it is by default), the provided stream will automatically be disposed when the document is disposed (I.e., the cloned document takes ownership of the stream).
    
  - `IDocument Clone(Stream stream, IEnumerable<KeyValuePair<string, object>> items = null, bool disposeStream = true)`
  
    Clones the current document with a new stream and additional metadata (all existing metadata is retained). If `disposeStream` is true (which it is by default), the provided stream will automatically be disposed when the document is disposed (I.e., the cloned document takes ownership of the stream).
  
  - `IDocument Clone(IEnumerable<KeyValuePair<string, object>> items = null)`
  
    Clones the current document with identical content and additional metadata (all existing metadata is retained).
    