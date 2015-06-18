Title: Where
Description: Filters the current sequence of modules using a predicate.
---
Filters the current sequence of modules using a predicate. Only input documents that satisfy the predicate will be output.

# Usage
---
  
  - `Where(Func<IDocument, bool> predicate)`
  
    Specifies the predicate to use for filtering documents.