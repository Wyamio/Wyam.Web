Title: IExecutionContext
Description: Contains all the information about the running execution and pipeline.
---
Contains all the information about the running execution and pipeline. It is passed to each module and most of the members are intended for use by module developers.

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
  
## Methods
  
  - `IReadOnlyList<IDocument> Execute(IEnumerable<IModule> modules, IEnumerable<IDocument> inputs)`
  
    This executes the specified modules with the specified input documents and returns the result documents. If you pass in `null` for `inputDocuments`, a new input document with the initial metadata from the engine will be used.