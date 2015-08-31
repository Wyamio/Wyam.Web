Title: If
Description: Provides conditional logic for your pipeline.
Category: Control
---
Evaluates a series of child modules for each input document if a specified condition is met. Any result documents from the child modules will be returned as the result of the If module. Any input modules that don't match a predicate will be returned as outputs without modification.

# Usage
---
    
  - `If(Func<IDocument, IExecutionContext, bool> predicate, params IModule[] modules)`
  
    Specifies a predicate and a series of child modules to be evaluated if the predicate returns true.
  
## Fluent Methods

Chain these methods together after the constructor to modify behavior.
    
  - `ElseIf(Func<IDocument, IExecutionContext, bool> predicate, params IModule[] modules)`
  
    Specifies an alternate condition to be tested on documents that did not satisfy previous conditions. You can chain together as many `ElseIf` calls as needed.
  
  - `Else(params IModule[] modules)`
  
    This should be at the end of your fluent method chain and will evaluate the specified child modules on all documents that did not satisfy previous predicates.