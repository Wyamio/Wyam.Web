Title: CopyFiles
Description: Copies files from one location on disk to another one and sets associated metadata.
Category: Input/Output
---
Copies the content of files from the file system from one location on disk to another location. For each output document, several metadata values are set with information about the file. By default, files are copied from the input folder (or a subfolder) to the same relative location in the output folder, but this doesn't have to be the case. Also note that this module is evaluated for each input document, so it's typically used as the first (and often only) module in a pipeline. Otherwise, you would probably copy the same files multiple times (once for each input document).

# Usage
---

  - `CopyFiles(string searchPattern)`
  
    Copies all files that match the specified search pattern.
  
  - `CopyFiles(Func<IDocument, IExecutionContext, string> sourcePath)`
  
    Copies all files that match the specified path. This allows you to specify different search paths depending on the input document.
  
## Fluent Methods

Chain these methods together after the constructor to modify behavior.

  - `SearchOption(SearchOption searchOption)`
  
    Specifies whether to search all directories or just the top directory.

  - `AllDirectories()`
  
    Specifies that all directories should be searched.
  
  - `TopDirectoryOnly()`
  
    Specifies that only the top-level directory should be searched.
  
  - `Where(Func<string, bool> predicate)`
  
    Specifies a predicate that must be satisfied for the file to be copied. The input to the predicate is the full path to the source file.
  
  - `To(Func<string, string> destinationPath)`
  
    Specifies an alternate destination path for each file (by default files are copied to their same relative path in the output directory). The input to the function is the full source file path (including file name) and the output should be the full file path (including file name) of the destination file.
       
# Metadata
---

The following metadata is added to each document.
  
  - `SourceFilePath`
  
    The full path (including file name) of the source file.
  
  - `DestinationFilePath`
  
    The full path (including file name) of the destination file.