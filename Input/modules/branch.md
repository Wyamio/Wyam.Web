Title: Branch
Description: Evaluates a sequence of modules without impacting the primary module flow.
Category: Control
---
Branch evaluates the specified modules with each input document as the initial document and then outputs the original input documents without modification. This allows a sequence of modules to execute without impacting the "main" module sequence. In other words, Branch executes it's child modules as if there were no Branch module in the sequence, but then when it's child modules are done, the main sequence of modules is executed as if there were no Branch. The flow is probably best explained with an example. Assume you have a module, AddOne, that just adds 1 to whatever numeric value is in the content of the input document(s). The input and output content of the following pipeline should demonstrate what Branch does:

```
                    // Input Content      // Output Content
Pipelines.Add(
    AddOne(),       // [Empty]            // 0
    AddOne(),       // 0                  // 1
    AddOne(),       // 1                  // 2
    Branch(
        AddOne(),   // 2                  // 3
        AddOne()    // 3                  // 4
    ),
    AddOne(),       // 2                  // 3
    AddOne()        // 3                  // 4
);
```

You can see that the input content to the AddOne modules after the Branch is the same as the input content to the AddOne modules inside the branch. The result of the modules in the Branch had no impact on those modules that run after the Branch. This is true for both content and metadata. If any modules inside the Branch created or changed metadata, it would be forgotten once the Branch was done.

# Usage
---

  - `Branch(params IModule[] modules)`
  
    Evaluates the specified modules with each input document as the initial document and then outputs the original input documents without modification.
    
## Fluent Methods

Chain these methods together after the constructor to modify behavior.
    
  - `Where(Func<IDocument, IExecutionContext, bool> predicate)`
  
    Limits the documents passed to the child modules to those that satisfy the supplied predicate. All original input documents are output without modification regardless of whether they satisfy the predicate.