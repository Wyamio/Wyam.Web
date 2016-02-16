Title: Custom Document Types
Description: Use custom document types to add new document behavior.
---
The default `IDocument` implementation that gets passed through the pipeline exposes the document metadata as well as some default properties like `IDocument.Source`. However, there may be times that it would be handy to give your document some smarts and introduce extra properties and methods. It's easy to make your own document type and tell the engine to use it instead of the default.

# The Easy Way

The easiest way to do this is to use the built-in helpers for custom document types. This starts with the `CustomDocument` base class. Simple derive a new document class from `CustomDocument` in the [declarations part](/getting-started/configuration#declarations) of your configuration file. The base class implements `IDocument` so you will have access to all the metadata that you normally would from the default implementation. This allows you to "wrap" metadata getters and setters with your own special logic. You can also add any additional properties and methods that you need to.

The only thing that you should keep in mind is that you may need to implement the `CustomDocument.Clone()` method (its declared as `protected virtual`). The default implementation performs an `object.MemberwiseClone()` to get a new instance of your custom document class when needed. However, if you have any special cloning requirements (for example, your document class should be deep-cloned), you will need to override this method and implement it yourself. The only requirement is that it must return a new instance of your document class.

Once you've created your `CustomDocument` class, you need to tell the engine to use it instead of the default `IDocument` implementation. This can be done using the `SetCustomDocumentType<T>()` method in the [main body](/getting-started/configuration#body) of your configuration file. Just use your document type as the generic parameter and everything else will be rigged up for you.

Here is an example:

```
public class MyDocument : CustomDocument
{
    // Gets the value from metadata and converts it to lowercase
    public string LowercaseTitle
    {
        get { return String("Title").ToLower(); }
    }
    
    // Stores a value directly in the new document type
    public DateTime Published { get; set; }
    
    protected override CustomDocument Clone()
    {
        return new MyDocument
        {
            // The metadata will be automatically cloned for you,
            // all you have to do is clone any additional properties
            Published = Published
        };
    }
}

---
SetCustomDocumentType<MyDocument>();

Pipelines.Add("Posts",
    ReadFiles(@"posts\*.md"),  // Read all markdown files in the "posts" directory
    FrontMatter(Yaml()),  // Load any frontmatter and parse it as YAML markup
    Trace(((MyDocument)@doc).LowercaseTitle)  // Cast the document to the custom type and trace the LowercaseTitle property
);
```

# More Advanced

Under the hood, Wyam actually has a very flexible architecture that gives you full control over document types and construction. The approach above is designed to abstract most of this, but you can go deeper for a more custom implementation. Specifically, all your custom document type has to do is implement `IDocument`. This interface requires several document properties (such as `IDocument.Source`) as well as a corresponding implementation of `IMetadata`. How you implement these interfaces is totally up to you. The `CustomDocument` base class above is really just a thin wrapper around the underlying default implementation of `IDocument` and simply delegates the interface implementation to the wrapped document.

Once you have your fully customized `IDocument` implementation, you also need to implement `IDocumentFactory` which tells the engine how to create new instances of your custom document class. Again, the simple approach above actually uses a special factory designed specifically for `CustomDocument` classes which is swapped in during the call to `SetCustomDocumentType<T>()`. When you create your own factory, you'll need to replace it in the engine by setting `Engine.DocumentFactory` in the [main body](/getting-started/configuration#body) of your configuration file.

For more information on the API classes used in fully custom document type scenarios, see [the API documentation](/api).