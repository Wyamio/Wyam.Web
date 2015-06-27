Title: ReadFiles
Description: Reads files from disk and sets associated metadata.
---
Reads the content of files from the file system into the content of new documents. For each output document, several metadata values are set with information about the file. Note that this module is best at the beginning of a pipeline because it will be executed once for each input document, even if you only specify a search path. If you want to add additional files to a current pipeline, you should enclose your ReadFiles modules with [Concat](/modules/concat).

# Usage
---

  - `ReadFiles(string searchPattern)`

    Reads all files that match the specified search pattern.
  
  - `ReadFiles(Func<IDocument, string> path)`
  
    Reads all files that match the specified path. This allows you to specify different search paths depending on the input.
  
## Fluent Methods

Chain these methods together after the constructor to modify behavior.

  - `SearchOption(SearchOption searchOption)`
  
    Specifies whether to search all directories or just the top directory.

  - `AllDirectories()`
  
    Specifies that all directories should be searched.
  
  - `TopDirectoryOnly()`
  
    Specifies that only the top-level directory should be searched.
  
  - `Where(Func<string, bool> predicate)`
  
    Specifies a predicate that must be satisfied for the file to be read. The input to the predicate is the full path to the file.
       
# Metadata
---

The following metadata is added to each document.

  - `SourceFileRoot`
  
    The root search path without any nested directories (useful for outputting documents at the same location relative to the root path).

  - `SourceFilePath`
  
    The full path of the file (including file name).
  
  - `SourceFileBase`

    The file name without any extension. Equivalent to `Path.GetFileNameWithoutExtension(SourceFilePath)`.

  - `SourceFileExt`

    The extension of the file. Equivalent to `Path.GetExtension(SourceFilePath)`.

  - `SourceFileName`

    The full file name. Equivalent to `Path.GetFileName(SourceFilePath)`.

  - `SourceFileDir`

    The full directory of the file. Equivalent to `Path.GetDirectoryName(SourceFilePath)`.
  
  - `RelativeFilePath`
  
    The relative path to the file (including file name) from the Wyam input folder.