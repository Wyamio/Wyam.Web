Title: Metadata
Description: Metadata is the primary means of passing information between modules and pipelines.
Order: 2
---
Along with it's content, every document contains *metadata*. As with documents, metadata is immutable and you must clone a document to add additional metadata. Several modules, such as [Meta](/modules/meta), are designed to allow you to manipulate document metadata as part of your pipeline.

Metadata is the primary means of passing information between modules and pipelines. For example, when a file is [read from disk](/modules/readfiles), metadata is set that records where on disk the file came from, it's file name, and other information. When the file is later [written back to disk](/modules/writefiles), this metadata is used to determine where the file should go and what filename to use.

# Front Matter

One of the more common ways to set metadata that's intended to be read by modules (as opposed to written by them) is to use *front matter*. Front matter is a common concept in static generators and gets around the problem of how to define metadata for a file without creating a whole separate file. Typically front matter is placed at the top of an input file and uses some sort of format (like YAML or JSON) to define key/value pairs. In Wyam, front matter can be extracted using the [FrontMatter](/modules/frontmatter) module. That module accepts child modules that process whatever content is contained in the front matter block, such as the [Yaml](/modules/yaml) or [Json](/modules/json) module. Often front matter is delimited from the actual file content by a series of dashes such as `---`, though an alternate delimiter can be specified in the [FrontMatter](/modules/frontmatter) module.

A file that contains front matter might look like this:

```
Title: Some Title
Description: This is a description.
Date: 5/25/2016
---
This is the content of the file.
```

An example of front matter usage would be using metadata to define tags for your blog posts. You could create a "Tags" metadata field in the front matter of your post file and then read that metadata later to create tag clouds, lists of similar posts, etc.

Practically, the [Yaml](/modules/yaml) module is usually used as the child of the [FrontMatter](/modules/frontmatter) module. However, like most things in Wyam this is designed to be flexible. You could process any type of front matter (JSON, etc.) with this setup by passing different child modules. You could even replicate the behavior of generators like [Hexo](https://hexo.io/docs/front-matter.html) and [Hugo](https://gohugo.io/content/front-matter/) that accept multiple front matter formats by putting multiple [FrontMatter](/modules/frontmatter) modules in a row with different delimiters and child modules.

# Accessing Metadata

Metadata is available via the `Metadata` property of every `IDocument`. The `IMetadata` interface implements `IReadOnlyDictionary<string, object>` for easy access. Every `IDocument` also implements `IReadOnlyDictionary<string, object>` and passes any calls through to it's metadata (so you'll rarely actually use the `Metadata` property and just access metadata directly through the document).

# Metadata Type Conversion 

All metadata is represented internally as raw objects. This allows you to store not just strings, but more complex data as well. However, when you access metadata you probably don't want to think about how it's stored or what the orignal type was. For example, [YAML](/modules/yaml) doesn't really distinguish between numbers and strings when it reads data, it's only when getting a value that you care. To make metadata as easy to work with as possible, Wyam includes a very powerful type conversion capability that lets you convert nearly any metadata value to any other compatible type.
    
When converting metadata values, all .NET type conversion techniques are checked including `TypeConverter`, `IConvertible`, casting, etc. The conversion support is provided by the [UniversalTypeConverter](http://www.codeproject.com/Articles/248440/Universal-Type-Converter) library.

If you request an `IList<T>`, `IEnumerable<T>`, or array of `T` and the metadata value is also enumerable, all elements will be converted to the requested type `T` and those that cannot be converted will be omitted from the result. If the metadata value is not enumerable, it will be returned as a single element of the requested enumerable type. 
    
# Metadata Lookup

There are several extensions to make working with documents and metadata easier. One of the more powerful ones lets you generate an `ILookup<T, IDocument>` from a sequence of documents based on a metadata key. The signature of the extension method is `ILookup<T, IDocument> ToLookup<T>(this IEnumerable<IDocument> documents, string key)` where `key` is the metadata key that you want to generate a lookup for.

For example, say you have a sequence of documents, some of which contain metadata for the key "Tags". Also, assume that some of the documents with "Tags" metadata contain a single value some contain arrays. If you simply call `Documents.ToLookup<string>("Tags")` you will get back an `ILookup<T, IDocument>` keyed by each possible tag string with a sequence of the documents that contain that tag as the value.