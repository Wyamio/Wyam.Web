Title: IExecutionContext
Description: Contains all the information about the running execution and pipeline.
---
Contains all the information about the running execution and pipeline. It is passed to each module and most of the members are intended for use by module developers. Contexts are immutable so you must call the `Clone(...)` method to create a new context if you need to add metadata as part of your internal module execution. Implements `IMetadata` and all metadata calls are passed through to the context's internal `Metadata` instance.

# Members
---
## Properties

  - `byte[] RawConfigAssembly { get; }`
  
    Gets the raw configuration assembly as compiled during [configuration](/getting-started/configuration).
  
  - `IEnumerable<Assembly> Assemblies { get; }`
  
    Gets all the assemblies used by the configuration or specified by the user.
   
  - `IReadOnlyPipeline Pipeline { get; }`
  
    Gets the currently executing pipeline.
  
  - `IModule Module { get; }`
  
    Gets the currently executing module.
  
  - `IExecutionCache ExecutionCache { get; }`
  
    Gets the execution cache.
  
  - `string RootFolder { get; }`
  
    Gets the configured root folder.
  
  - `string InputFolder { get; }`
  
    Gets the configured input folder.
  
  - `string OutputFolder { get; }`
  
    Gets the configured output folder.
  
  - `ITrace Trace { get; }`
  
    Gets the trace class for outputting and logging.
  
  - `IDocumentCollection Documents { get; }`
  
    Gets the document collection.
    
  - `IMetadata Metadata { get; }`
  
    Gets the metadata associated with this context.
  
## Methods
  
  - `IReadOnlyList<IDocument> Execute(IEnumerable<IModule> modules, IEnumerable<IDocument> inputs, IEnumerable<KeyValuePair<string, object>> metadata = null)`
  
    This executes the specified modules with the specified input documents and returns the result documents. If you pass in `null` for `inputDocuments`, a new input document with the initial metadata from the engine will be used. You can also optionally pass in additional metadata which will get added to the context and passed to the child modules.
    
  - `IExecutionContext Clone(IEnumerable<KeyValuePair<string, object>> metadata)`
  
    Clones the current context with identical properties and additional metadata (all existing metadata is retained). This is useful if you need to pass the cloned context to some other library or method from your module.