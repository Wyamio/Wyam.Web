Title: Razor
Description: Parses Razor templates and renders them to HTML.
---
Razor is the templating language used by ASP.NET MVC. This module can parse and compile Razor templates and then render them to HTML.

This module is based on the Razor code in the forthcoming ASP.NET 5 (vNext). It was written from the ground-up and doesn't use an intermediate library like [RazorEngine](https://github.com/Antaris/RazorEngine) (which is a great library, I just wanted more control).

Whenever possible, the same conventions as the Razor engine in ASP.NET MVC were used. It's important to keep in mind however, that this is *not* ASP.NET MVC. Many features you may be used to will not work (like most of the `HtmlHelper` extensions) and others just don't make sense (like the concept of *actions* and *controllers*). Also, while property names and classes in the two engines have similar names (such as `HtmlHelper`) they are not the same, and code intended to extend the capabilities of Razor in ASP.NET MVC probably won't work. That said, a lot of functionality does function the same as it does in ASP.NET MVC.

# Usage
---
  - `Razor()`
  
    Parses Razor templates in each input document and outputs documents with rendered HTML content.

## View Model

The view model for each page is set to the `IMetadata` of the input document. This allows you to write statements like `@@Model.Get<string>("MyMetadataKey")`.

## View Properties

You also have access to other Wyam information from your view. The following properties are available on every page:

  - `Metadata`
  
    Contains the metadata of the current document. This is the same as `Model`.
  
  - `ExecutionContext`
  
    Contains the Wyam `IExecutionContext` which has information about the currently executing Wyam pipeline.
  
  - `Documents`
    
    Contains the Wyam documents collection as a `IReadOnlyDictionary<string, IReadOnlyList<IDocument>>`. The keys are pipeline names and the values are the list of documents for a given pipeline.
  
  - `AllDocuments`
  
    Contains an aggregated sequence of all documents across all pipelines. This is useful for things like generating a list of blog posts, etc.

## ViewStart

You can place common Razor code to be executed at the start of every Razor page in a `_ViewStart.cshtml` file. This is typically used to specify a layout page.

## Layouts

Layouts function much the same way as they do in ASP.NET MVC. Typically you place your layout code in a `_Layout.cshtml` file (though it can be named anything) and then set it in the `_ViewStart.cshtml` file. You can use `@@RenderBody()` and `@@RenderSection()` just as you would in ASP.NET MVC.

## Partials

Partials also work as they do in ASP.NET MVC. Just use `@@Html.Partial("_PartialName")` or `@@Html.RenderPartial("_PartialName")`.