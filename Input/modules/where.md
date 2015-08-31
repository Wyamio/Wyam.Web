Title: Where
Description: Filters the current sequence of modules using a predicate.
Category: Control
---
Filters the current sequence of modules using a predicate. Only input documents that satisfy the predicate will be output.

# Usage
---
    
  - `Where(Func<IDocument, IExecutionContext, bool> predicate)`
  
    Specifies the predicate to use for filtering documents.