Title: Writing a Module
Description: How to write a module for Wyam.
---
Writing a new module is easy and consists of four steps: creating a class library for your module, adding a module class, implementing a single method, and deploying the library.

# Create a Library
---

Create a class library for your module and reference the latest `Wyam.Common` [NuGet package](https://www.nuget.org/packages/Wyam.Common). This includes all of the Wyam interfaces and should be the only package you need to include to extend Wyam.

## Library Boundaries

It is recommended that you aim for library separation based on dependencies. Wyam will eventually be packaged for multiple .NET runtimes and defining module libraries on dependency boundaries should make it easier to port those modules that support alternate platforms.

## Naming

Try to name your modules (and their containing libraries) based on *what they do* not *how they do it*. The names should also be short and concise. For example, if you write a module that manipulates Excel files, it should be called "Excel" not "ExcelDotNet" (assuming that was the name of a .NET Excel library) or "ExcelEditor".

# Create a Module Class
---

Derive your class from `IModule`.

If your module needs any information, you'll typically get it in the constructor and then save it until execution. If your module needs to collect data at runtime, use either `ContextConfig` or `DocumentConfig` as an argument in the constructor or fluent method and then evaluate it in your `Execute` method using `Invoke<T>`. Because both `ContextConfig` and `DocumentConfig` return a raw `object` to make configuration easier, be sure to document somewhere what kind of return type your module expects from these delegates.

# Implement The Execute Method
---

Implement the single method `IEnumerable<IDocument> Execute(IReadOnlyList<IDocument> inputs, IExecutionContext context)`. Your implementation should accept zero or more `IDocument` inputs and return zero or more `IDocument` outputs. To the extent you need to modify the input documents (which are immutable), use one of the `IDocument.Clone(...)` methods which return a new `IDocument` with new content and/or additional metadata items.

Inside your `Execute` implementation, iterate over the `IDocument` objects that were passed in. To access document content, use `IDocument.GetStream()` or `IDocument.Content`. Using streams is prefered when both reading and cloning documents because it can provide more direct access to the document content. However, you should prefer `IDocument.Content` over using your own `MemoryStream` to buffer content from `IDocument.GetStream()` if you need access to the content as a string. Also use one of the `IDocument.Clone(...)` overrides that takes a string instead of cloning with a `MemoryStream` if you already have the content as a string. Also note that if you use `IDocument.GetStream()` **you must dispose the returned stream**.

## Parallel Execution

For performance reasons, modules are encouraged to perform their operations in parallel if possible. That's why the entire sequence of input documents gets passed to the `Execute(...)` method instead of just giving the module one at a time. The easiest way to do this is to use `.AsParallel()` in a LINQ expression or use `Parallel.ForEach(...)`, but any method is acceptable. Keep in mind that while intra-module operations are allowed to run in parallel, the pipeline is run sequentially on the main thread. This ensures that the order of pipelines and modules remains predictable and consistent.

## Executing Child Modules

If you need to execute child modules from your module, don't call `IModule.Execute(...)` on each child module. Instead use one of the `IExecutionContext.Execute(...)` methods. They return the result documents from calling the child module chain (which can then be returned by your own module if needed).

## Other Guidelines

Here are a few other guidelines to follow so that your module matches the convention used by the built-in Wyam modules.

- Favor overloaded constructors over optional arguments. This will help avoid versioning problems in the future (see [this blog post](http://haacked.com/archive/2010/08/10/versioning-issues-with-optional-arguments.aspx/) for more details).
- Use a fluent interface for setting optional options and favor accepted fluent method naming conventions (I.e., use `WithSomeOption(...)` instead of `SetSomeOption(...)` unless you're actually setting something external to the module).
- Try to make sure fluent methods are resilient against multiple calls if appropriate. I.e., if a fluent methods defines a set of something, make sure subsequent calls add to the set instead of replacing it. If the fluent method defines a predicate, make sure subsequent calls add conditions to the predicate instead of replacing it.
- Make the module null and fault tolerant. That is, if null or other invalid values are supplied as constructor or fluent method arguments, try to work around it by using default values, etc. instead of throwing exceptions.
- Favor flexibility and try to consider all the possible uses of your module. Even if you don't think anyone would use it in a certain way, try to support as many scenarios as possible.
- Always process the input documents to a module. Don't rely on getting documents from the `IExecutionContext` except for reference or supplemental information.
- Write tests if possible and include them if submitting your module to the official repository.
- Consider using a `public static class` called `[LibraryName]Keys` with `public const string` members to store your metadata keys if your module generates metadata. This will help avoid the use of "magic strings" in configuration files and templates.
- Document your module using XML code comments. Also use the special `category` and `metadata` XML comment elements (the Wyam web site knows how to read these and they power the [modules](/modules) page).

# Deployment
---

Compile your library and place it somewhere Wyam will find it. You can manually put it in the Wyam directory (not recommended), tell Wyam to [reference your assemblies](/getting-started/configuration#assemblies), or create a NuGet package and [tell Wyam to use it](/getting-started/configuration#nuget). In all of these deployment scenarios, Wyam will scan the assembly for any modules, find your new module, and make it available.

# Example
---

As an example, here's the entirety of the [Where](/modules/where) module, which filters the documents in the pipeline:

```
public class Where : IModule
{
	private readonly DocumentConfig _predicate;

	public Where(DocumentConfig predicate)
	{
		_predicate = predicate;
	}

	public IEnumerable<IDocument> Execute(IReadOnlyList<IDocument> inputs, IExecutionContext context)
	{
		return inputs.Where(x => _predicate.Invoke<bool>(x, context));
	}
}
```