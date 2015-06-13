Title: ReadFiles
Description: Reads files from disk and sets associated metadata.
---
Reads the content of files from the file system into the content of new documents.

# Usage
---

  - `ReadFiles(string searchPattern, SearchOption searchOption = SearchOption.AllDirectories)`

  Reads all files that match the specified search pattern with the specified `SearchOption`. If the input metadata contains the key `InputPath` (which is usually set by default), it is used as the root path of the search.
  
  - `ReadFiles(Func<IDocument, string> path, SearchOption searchOption = SearchOption.AllDirectories)`
  
  Reads all files that match the specified search pattern with the specified `SearchOption`. This allows you to specify different search paths depending on the input.
  
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

The following metadata is added to each new document:

  - `FileRoot`
  
  The root search path without any nested directories (useful for outputting documents at the same location relative to the root path).
  
  - `FilePath`
  
  The full path to the file.
  
  - `FileBase`

  The file name without any extension. Equivalent to `Path.GetFileNameWithoutExtension(FilePath)`.

  - `FileExt`

  The extension of the file. Equivalent to `Path.GetExtension(FilePath)`.

  - `FileName`

  The full file name. Equivalent to `Path.GetFileName(FilePath)`.

  - `FileDir`

  The full directory of the file. Equivalent to `Path.GetDirectoryName(FilePath)`.
  
  - `FileRelative`
  
  The relative path to the file (including file name) from the Wyam input folder.