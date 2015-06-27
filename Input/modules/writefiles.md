Title: WriteFiles
Description: Writes document content to disk.
---
Writes the content of each input document to the file system. If the metadata keys `WriteFileName` or `WriteExtension` (which require `RelativeFilePath` to be set, usually by the [ReadFiles](/modules/readfiles) module) or `WritePath` are set on an input document, that value will be used instead of what's specified in the module. For example, if you have a bunch of Razor `.cshtml` files that need to be rendered to `.html` files but one of them should be output as a `.xml` file instead, define the `WriteExtension` metadata value in the [front matter](/modules/frontmatter) of the page. 

# Usage
---

  - `WriteFiles()`
  
    Writes the document content to disk with the same file name and relative path as the input file. This requires metadata for `RelativeFilePath` to be set (which is done by default by the [ReadFiles](/modules/readfiles) module).
  
  - `WriteFiles(string extension)`

    Writes the document content to disk with the specified extension with the same base file name and relative path as the input file. This requires metadata for `RelativeFilePath` to be set (which is done by default by the [ReadFiles](/modules/readfiles) module).
  
  - `WriteFiles(Func<IDocument, string> path)`
  
    Uses a function to describe where to write the content of each document. The output of the function should be either a full path to the disk location (including file name) or a path relative to the output folder.
  
## Fluent Methods

Chain these methods together after the constructor to modify behavior.
  
  - `Where(Func<IDocument, bool> predicate)`
  
    Specifies a predicate that must be satisfied for the file to be written.
    
# Metadata
---

The following metadata is added to each document.

  - `DestinationFilePath`
  
    The full path (including file name) of the destination file.