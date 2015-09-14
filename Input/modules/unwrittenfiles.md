Title: UnwrittenFiles
Description: Outputs only those documents that have not yet been written to the file system.
Category: Input/Output
---
Outputs only those documents that have not yet been written to the file system. The constructors and file resolution logic follows the same semantics as [WriteFiles](/modules/writefiles). This is useful for eliminating documents from the pipeline on subsequent runs depending on if they've already been written to disk. For example, you might want to put this module right after [ReadFiles](/modules/readfiles) for a pipeline that does a lot of expensive image processing since there's no use in processing images that have already been processed. Note that only the file name is checked and that this module cannot determine if the content would have been the same had a document not been removed from the pipeline. Also note that **you should only use this module if you're sure that no other pipelines rely on the output documents**. Because this module removes documents from the pipeline, those documents will never reach the end of the pipeline and any other modules or pages that rely on them (for example, an image directory) will not be correct. 

# Usage
---

  - `UnwrittenFiles()`
  
  - `UnwrittenFiles(string extension)`
  
  - `UnwrittenFiles(DocumentConfig path)`
  
## Fluent Methods

Chain these methods together after the constructor to modify behavior.
  
  - `Where(Func<IDocument, bool> predicate)`
    
  - `Where(DocumentConfig predicate)`