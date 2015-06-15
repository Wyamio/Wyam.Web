Title: Branch
Description: Allows you to evaluate a series of modules without impacting the primary module flow.
---
This evaluates the specified modules with each input document as the initial document and then outputs the original input documents without modification. In other words, the branch does affect the primary pipeline module flow.

# Usage
---

  - `Branch(params IModule[] modules)`
  
    Evaluates the specified modules with each input document as the initial document and then outputs the original input documents without modification.
  
  - `Branch(Func<IDocument, bool> predicate, params IModule[] modules)`
  
    Evaluates the specified modules with each input document that satisfies the supplied predicate as the initial document and then outputs the original input documents without modification (all input documents are output regardless of whether they satisfy the predicate).