Title: Razor
Description: Parses Razor templates and renders them to HTML.
Category: Templating
---
Razor is the templating language used by ASP.NET MVC. This module can parse and compile Razor templates and then render them to HTML. While a bit outdated, [this guide](http://haacked.com/archive/2011/01/06/razor-syntax-quick-reference.aspx/) is a good quick reference for the Razor language syntax.

This module is based on the Razor code in the forthcoming ASP.NET 5 (vNext). It was written from the ground-up and doesn't use an intermediate library like [RazorEngine](https://github.com/Antaris/RazorEngine) (which is a great library, implementing directly just provides more control). Note that for now, TagHelpers are not implemented in Wyam. Their API is still changing and it would have been too difficult to keep up. Support for TagHelpers may be introduced once ASP.NET MVC 5 becomes more stable.  

Whenever possible, the same conventions as the Razor engine in ASP.NET MVC were used. It's important to keep in mind however, that this is *not* ASP.NET MVC. Many features you may be used to will not work (like most of the `HtmlHelper` extensions) and others just don't make sense (like the concept of *actions* and *controllers*). Also, while property names and classes in the two engines have similar names (such as `HtmlHelper`) they are not the same, and code intended to extend the capabilities of Razor in ASP.NET MVC probably won't work. That said, a lot of functionality does function the same as it does in ASP.NET MVC.

# Usage
---
  - `Razor(Type basePageType = null)`
  
    Parses Razor templates in each input document and outputs documents with rendered HTML content. If `basePageType` is specified, it will be used as the base type for Razor pages (see below).
  
## Fluent Methods

Chain these methods together after the constructor to modify behavior.

  - `SetViewStart(string path)`
  
    Specifies an alternate ViewStart file to use for all Razor pages processed by this module.

  - `SetViewStart(Func<IDocument, string> path)`
  
    Specifies an alternate ViewStart file to use for all Razor pages processed by this module. This lets you specify a different ViewStart file for each document. For example, you could return a ViewStart based on document location or document metadata. Returning `null` from the function reverts back to the default ViewStart search behavior for that document.
    
  - `IgnorePrefix(string prefix)`
  
    Specifies a file prefix to ignore. If a document has a metadata value for `SourceFileName` and that metadata value starts with the specified prefix, that document will not be processed or output by the module. By default, the Razor module ignores all documents prefixed with an underscore (`_`). Specifying `null` will result in no documents being ignored.

## View Model

The view model for each page is set to the `IMetadata` of the input document. This allows you to write statements like `@@Model.Get<string>("MyMetadataKey")`.

## View Properties

You also have access to other Wyam information from your view. The following properties are available on every page:

  - `Metadata`
  
    Contains the metadata of the current document. This is the same as `Model`.
  
  - `ExecutionContext`
  
    Contains the Wyam `IExecutionContext` which has information about the currently executing Wyam pipeline.
  
  - `Documents`
    
    Contains the Wyam `IDocumentCollection` which holds all processed documents and provides various means of accessing them.

## ViewStart

You can place common Razor code to be executed at the start of every Razor page in a `_ViewStart.cshtml` file. This is typically used to specify a layout page.

## Layouts

Layouts function much the same way as they do in ASP.NET MVC. Typically you place your layout code in a `_Layout.cshtml` file (though it can be named anything) and then set it in the `_ViewStart.cshtml` file. You can use `@@RenderBody()` and `@@RenderSection()` just as you would in ASP.NET MVC.

## Partials

Partials also work as they do in ASP.NET MVC. Just use `@@Html.Partial("_PartialName")` or `@@Html.RenderPartial("_PartialName")`. Note that you can't pass a model to the partial view (since the model is always set to the current `IMetadata`). To pass data to a partial view, use the `ViewData` collection, setting a value just before rendering the partial and then getting it within the partial.

## HtmlHelpers

It's possible to define custom HtmlHelpers in your configuration file and then use them within your Razor page. This provides a great way to specify common behavior for all your views. In the [declarations section](/getting-started/configuration#declarations) of your configuration file, add a static class (the name doesn't matter, but convention suggests you call it `HtmlHelperExtensions` or `HtmlExtensions`). Then add static extension methods for `HtmlHelper`. For example:

```
// Setup code
// ...
===
// Declaration code

public static class HtmlHelperExtensions
{
  public static string HelloWorld(this HtmlHelper helper)
  {
    return "Hello World!";
  }
}
---
// Configuration code
// ...
```

Then you can use it from your Razor pages by calling `@@Html.HelloWorld()`.

## Base Page

You can also specify a different base page for your Razor views. This lets you do things like define additional properties or behavior for every page. In the [declarations section](/getting-started/configuration#declarations) of your configuration file, add an abstract class that derives from `BaseRazorPage`. Then page the `Type` of your new base class to the `Razor` module.

```
// Setup code
// ...
===
// Declaration code

public abstract class MyRazorPage : BaseRazorPage
{
  public string HelloWorld
	{
    get { return "Hello World"; }
	}
}
---
// Configuration code

Pipelines.Add("Content",
	ReadFiles("*.cshtml"),
	Razor(typeof(MyRazorPage)),
	WriteFiles(".html")
);
```

Then you can use any methods, properties, etc. you defined in your base class directly within your Razor pages - I.e., `@@HelloWorld`.