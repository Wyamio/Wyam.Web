Title: IDocumentCollection
Description: Allows you to get sequences of documents from the current and previous pipelines.
---
Allows you to get sequences of documents from the current and previous pipelines. Note that only the documents output at the end of the pipeline are available. Implements `IEnumerable<IDocument>` which iterates over all documents in the collection.

# Members
---
  
## Methods
  
  - `IReadOnlyDictionary&lt;string, IEnumerable&lt;IDocument&gt;&gt; ByPipeline()`
  
    Returns a dictionary containing all the documents processed by each pipeline keyed by pipeline name.
  
  - `IEnumerable<IDocument> FromPipeline(string pipeline)`
  
    Returns all the documents output by the specified pipeline.
  
  - `IEnumerable<IDocument> ExceptPipeline(string pipeline)`
  
    Returns all the documents output by all pipelines except the specified one.