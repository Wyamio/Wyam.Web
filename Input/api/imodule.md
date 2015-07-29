Title: IModule
Description: Modules must implement this interface.
---
Modules must implement this interface. At startup, all found and specified assemblies are scanned for classes that implement `IModule` and those that do are automatically registered as available modules.

# Members
---
  
## Methods
      
  - `IEnumerable<IDocument> Execute(IReadOnlyList<IDocument> inputs, IExecutionContext context)`
  
    The implementation of this method should contain the logic for the module. This method should never be called directly, instead call `IExecutionContext.Execute(...)` if you need to execute a module from within another module.