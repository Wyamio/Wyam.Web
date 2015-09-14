Title: Meta
Description: Adds the specified metadata to each input document.
Category: Metadata
---
Adds the specified metadata to each input document.

# Usage
---

  - `Meta(string key, object metadata)`
  
    The specified object is added as metadata for the specified key for every input document.

  - `Meta(string key, ContextConfig metadata)`
  
    Uses a function to determine an object to be added as metadata for each document. This allows you to specify different metadata for each document depending on the context.

  - `Meta(string key, DocumentConfig metadata)`
  
    Uses a function to determine an object to be added as metadata for each document. This allows you to specify different metadata for each document depending on the input.

  - `Meta(params IModule[] modules)`
  
    The specified modules are executed against an empty initial document and all metadata that exists on the result documents from evaluating the entire child module chain are added as metadata to each input document.