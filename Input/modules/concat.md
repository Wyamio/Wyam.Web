Title: Concat
Description: Evaluates a sequence of modules and concats the result documents with the input documents.
Category: Control
---
This evaluates the specified modules with an empty initial document and then outputs the original input documents without modification concatenated with the results from the specified module sequence.

# Usage
---

  - `Concat(params IModule[] modules)`
  
    Evaluates the specified modules with an empty initial input document.