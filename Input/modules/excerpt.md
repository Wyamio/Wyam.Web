Title: Excerpt
Description: Finds the first occurrence of a specified HTML element and stores it's contents as metadata.
Category: Metadata
---
Finds the first occurrence of a specified HTML element and stores it's contents as metadata. This is useful for situations like displaying the first paragraph of your most recent blog posts or [generating RSS and Atom feeds](/knowledgebase/rss-and-atom-feeds). By default, this module looks for the first `p` (paragraph) element and places it's outer HTML content in metadata with a key of `Excerpt`.

This module uses [AngleSharp](https://github.com/FlorianRappl/AngleSharp).

# Usage
---

  - `Excerpt()`

    Creates the module with the default query selector of `p`.
  
  - `Excerpt(string querySelector)`
  
    Specifies an alternate query selector for the content.
  
## Fluent Methods

Chain these methods together after the constructor to modify behavior.

  - `SetQuerySelector(string querySelector)`
  
    Allows you to specify an alternate query selector. 

  - `SetMetadataKey(string metadataKey)`
  
    Allows you to specify an alternate metadata key.
    
  - `SetOuterHtml(bool outerHtml)`
  
    Controls whether the inner HTML (not including the containing element's HTML) or outer HTML (including the containing element's HTML) of the first result from the query selector is added to metadata. The default is to return outer HTML content.
    
 # Metadata
---

The following metadata is added to each document.

  - `Excerpt`
  
    Contains the content of the first result from the query selector (unless an alternate metadata key is specified).