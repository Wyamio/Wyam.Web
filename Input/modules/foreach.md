Title: ForEach
Description: Executes the input documents one at a time against the specified child modules.
---
Executes the input documents one at a time against the specified child modules. Normally, documents are executed in a breadth-first traversal where all documents are executed against a module before continuing with the next module. This module allows you to conduct a depth-first traversal instead by executing each document one at a time against the child modules before continuing with the next document. It can be especially helpful when trying to control memory usage for large documents such as images because it lets you move the documents through the pipeline one at a time. The aggregate outputs from each sequence of child modules executed against each document will be output.

For example:

```
Pipelines.Add("ImageProcessing",
    // ReadFiles will create N new documents with a Stream
    // (but nothing will be read into memory yet)
    ReadFiles(@@"images\*"),
    // Each document will be individually sent through the
    // sequence of ForEach child pipelines
    ForEach(
        // This will load the *current* document into memory
        // and perform image manipulations on it
        ImageProcessor()
            //...
            ,
        // and this will save the stream to disk, replacing it with
        // a file stream, thus freeing up memory for the next file
        WriteFiles()
    )
);
```

# Usage
---
  
  - `ForEach(params IModule[] modules)`
  
    Specifies the modules to run against the input document one at a time.