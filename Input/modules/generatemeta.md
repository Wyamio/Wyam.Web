Title: GenerateMeta
Description: Procedurally generates metadata using flexible text templates.
Category: Metadata
---
Procedurally generates metadata using flexible text templates.

**This module uses [Rant](http://berkin.me/rant/).**

# Usage
---

  - `GenerateMeta(string key, object template)`
  
    The specified text template is processed and added as metadata for the specified key for every input document.

  - `GenerateMeta(string key, ContextConfig metadata)`
  
    Uses a function to determine a text template which is processed and added as metadata for each document. This allows you to specify different metadata for each document depending on the context.

  - `GenerateMeta(string key, DocumentConfig metadata)`
  
    Uses a function to determine a text template which is processed and added as metadata for each document. This allows you to specify different metadata for each document depending on the input.

  - `GenerateMeta(string key, params IModule[] modules)`
  
    The specified modules are executed against an empty initial document and the resulting content from evaluating the entire child module chain is processed as a text template and added as metadata to each input document.