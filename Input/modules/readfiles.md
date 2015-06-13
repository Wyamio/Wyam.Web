Title: ReadFiles
Description: Reads files from disk and sets associated metadata.
---
Reads the content of files from the file system into the content of new documents.

## Usage

- `ReadFiles(string searchPattern, SearchOption searchOption = SearchOption.AllDirectories)`
Reads all files that match the specified search pattern with the specified `SearchOption`. If the input metadata contains the key `InputPath` (which is usually set by default), it is used as the root path of the search.
- `ReadFiles(Func<IDocument, string> path, SearchOption searchOption = SearchOption.AllDirectories)`
Reads all files that match the specified search pattern with the specified `SearchOption`. This allows you to specify different search paths depending on the input.
    
## Metadata

The following metadata is added to each new document:

- `FileRoot`
The root search path without any nested directories (useful for outputting documents at the same location relative to the root path).
- `FilePath`
The full path to the file.
- `FileBase`
Equivalent to `Path.GetFileNameWithoutExtension(FilePath)`.
- `FileExt`
Equivalent to `Path.GetExtension(FilePath)`.
- `FileName`
Equivalent to `Path.GetFileName(FilePath)`.
- `FileDir`
Equivalent to `Path.GetDirectoryName(FilePath)`.