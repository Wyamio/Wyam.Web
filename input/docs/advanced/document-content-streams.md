Title: Document Content Streams
Description: How content for documents is stored and how to customize that behavior.
---
[Documents](/docs/concepts/documents) in Wyam store their content in a stream. While some modules use third-party libraries that provide their own efficient `Stream` implementation, many do not. In these cases, Wyam has to provide a stream to the module to store whatever content it produces.

There are a number of tradeoffs involved in this process. If memory is used for storage, then the content for every document needs to fit into memory. If files are used, then performance will take a hit due to slow disk I/O. This creates an opportunity for customization and for tailoring the memory backing strategy for content streams to your specific site.

By default, Wyam stores content in a pooled memory buffer. This provides a good balance between performance (since the data is still in memory) and memory usage (since the memory is pooled). The drawback is that for very simple builds the overhead of allocating and reclaiming memory from the buffer pool may outweigh the benefits.

Document content streams are provided by an implementation of the `IContentStreamFactory` interface. To change the way these streams are allocated, you just replace the interface with an alternate implementation. In addition to the default behavior, Wyam provides two additional implementations: `MemoryContentStreamFactory` and `FileContentStreamFactory`. The first allocates every content stream as a `MemoryStream` and is optimized for performance at the expense of memory consumption. The second allocates content streams as temporary files and is optimized for memory consumption at the expense of performance.

To use one of the alternate content stream providers, place the following in your [configuration file](/docs/usage/configuration):

```
ContentStreamFactory = new MemoryContentStreamFactory();
```

Or:

```
ContentStreamFactory = new FileContentStreamFactory();
```

Alternatively, for advanced scenarios, you can provide your own implementation of `IContentStreamFactory` right in the configuration file and then assign an instance of your custom factory to `ContentStreamFactory`.