Title: GenerateContent
Description: Procedurally generates content using flexible text templates.
Category: Content
---
Procedurally generates content using flexible text templates.

**This module uses [Rant](http://berkin.me/rant/).**

# Usage
---

  - `GenerateContent(object template)`
  
    The specified text template is processed and added as content for every input document.

  - `GenerateContent(ContextConfig metadata)`
  
    Uses a function to determine a text template which is processed and added as content for each document. This allows you to specify different content for each document depending on the context.

  - `GenerateContent(DocumentConfig metadata)`
  
    Uses a function to determine a text template which is processed and added as content for each document. This allows you to specify different content for each document depending on the input.

  - `GenerateContent(params IModule[] modules)`
  
    The specified modules are executed against an empty initial document and the resulting content from evaluating the entire child module chain is processed as a text template and added as content to each input document.
  
## Fluent Methods

Chain these methods together after the constructor to modify behavior.
  
  - `SetSeed(long seed)`
  
    This allows you to set the seed used for text generation which can be handy for ensuring repeatable generations.
    
  - `IncrementSeed(bool increment = true)`
  
    When true, this increments the seed for each document. If false, every document will get the same content for the same template.
    
  - `IncludeNsfw(bool includeNsfw = true)`
  
    When true, the dictionary will include NSFW content.
    