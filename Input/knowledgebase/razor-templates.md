Title: Razor Templates
Description: Details on how to get the most from your Razor templates.
---
Razor is an extremly powerful templating language, especially if you already know C#. Writing a simple Razor template is easy, but there are a lot of ways they can be customized and controlled. If you're not familiar with Razor templates, [see here](http://haacked.com/archive/2011/01/06/razor-syntax-quick-reference.aspx/) for a great introduction to the syntax and be sure to read the [Razor module](/modules/Razor) documentation.

# View Model
---

The view model for each page is set to the `IMetadata` of the input document. This allows you to write statements like `@@Model.Get<string>("MyMetadataKey")`.

# View Properties
---

You also have access to other Wyam information from your view. The following properties are available on every page:

  - `Metadata`
  
    Contains the metadata of the current document. This is the same as `Model`.
  
  - `ExecutionContext`
  
    Contains the Wyam `IExecutionContext` which has information about the currently executing Wyam pipeline.
  
  - `Documents`
    
    Contains the Wyam `IDocumentCollection` which holds all processed documents and provides various means of accessing them.

# ViewStart
---

You can place common Razor code to be executed at the start of every Razor page in a `_ViewStart.cshtml` file. This is typically used to specify a layout page.

# Layouts
---

Layouts function much the same way as they do in ASP.NET MVC. Typically you place your layout code in a `_Layout.cshtml` file (though it can be named anything) and then set it in the `_ViewStart.cshtml` file. You can use `@@RenderBody()` and `@@RenderSection()` just as you would in ASP.NET MVC.

# Partials
---

Partials also work as they do in ASP.NET MVC. Just use `@@Html.Partial("_PartialName")` or `@@Html.RenderPartial("_PartialName")`. Note that you can't pass a model to the partial view (since the model is always set to the current `IMetadata`). To pass data to a partial view, use the `ViewData` collection, setting a value just before rendering the partial and then getting it within the partial.

# HtmlHelpers
---

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

# Base Page
---

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
