Title: Branch
Description: Evaluates a sequence of modules without impacting the primary module flow.
---
This evaluates the specified modules with each input document as the initial document and then outputs the original input documents without modification. In other words, the branch does not affect the primary pipeline module flow.

# Usage
---

  - `Branch(params IModule[] modules)`
  
    Evaluates the specified modules with each input document as the initial document and then outputs the original input documents without modification.
    
## Fluent Methods

Chain these methods together after the constructor to modify behavior.
  
  - `Where(Func<IDocument, bool> predicate)`
  
    Limits the documents passed to the child modules to those that satisfy the supplied predicate. All original input documents are output without modification regardless of whether they satisfy the predicate.