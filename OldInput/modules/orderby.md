Title: OrderBy
Description: Orders the input documents based on a key.
Category: Control
---
Orders the input documents based on the specified key function. The ordered documents are returned as the result of this module.

# Usage
---

  - `OrderBy(DocumentConfig key)`
  
    Orders the input documents using the `key` delegate to get the ordering key.
	
## Fluent Methods

Chain these methods together after the constructor to modify behavior.
  
  - `Descending(bool descending = true)`
  
    Specifies whether the documents should be ordered in descending order (the default is ascending order).