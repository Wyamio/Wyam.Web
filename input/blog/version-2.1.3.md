Title: Version 2.1.3
Published: 1/29/2019
---
Version 2.1.3 is now available. This was another bug fix release with a few minor feature enhancements.

# Release Notes

## Features

- Updates to the globber and file system abstractions to deal with file system case sensitivity better (#771, thanks @glennawatson)

## Fixes

- Fixes a possible concurrency bug when adding/removing trace loggers and indenting
- Fixes for URL absolute link validation (#773, thanks @glennawatson)
- Allow the `If` module indexer to be accessed without casting (#769, thanks @ociaw)
- Fixed some quirks with the new diagram panning/zooming

## Refactoring

- Changed line ending behavior for files in the repository and re-normalized line endings to LF (#772, thanks @glennawatson)
- Big performance improvement to the `AutoLink` module (#766)