Title: Writing a Module
Description: How to write a module for Wyam.
---
Writing a new module is easy:

1. Create a library and reference the latest `Wyam.Abstractions` [NuGet package](https://www.nuget.org/packages/Wyam.Abstractions). This includes all of the Wyam interfaces and should be the only package you need to include to extend Wyam.

2. Derive your class from `IModule`.

3. Implement the single method `IEnumerable<IDocument> Execute(IReadOnlyList<IDocument> inputs, IExecutionContext context)`. Your implementation should accept zero or more `IDocument` inputs and return zero or more `IDocument` outputs. To the extent you need to modify the input documents (which are immutable), use `IDocument.Clone(string content, IEnumerable<KeyValuePair<string, object>> items = null)` which returns a new `IDocument` with new content and additional metadata items. If you only need to add metadata items, use `IDocument.Clone(IEnumerable<KeyValuePair<string, object>> items = null)`.

4. Compile your library and place it somewhere Wyam will find it. You can manually put it in the Wyam directory (not recommended), tell Wyam to [reference your assemblies](/getting-started/configuration#assemblies), or create a NuGet package and [tell Wyam to use it](/getting-started/configuration#nuget). In all of these deployment scenarios, Wyam will scan the assembly for any modules, find your new module, and make it available.

# Executing Child Modules

If you need to execute child modules from your module, don't call `IModule.Execute(...)` on each child module. Instead use the `IExecutionContext.Execute(IEnumerable<IModule> modules, IEnumerable<IDocument> inputDocuments)` method. It returns the resultant documents from calling the child module chain (which can also be returned by your own module). If you pass `null` for `inputDocuments` a new initial document will be used.