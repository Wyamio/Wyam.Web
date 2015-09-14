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

  - `bool TryConvert<T>(object value, out T result)`
  
    This attempts to convert `value` to type `T` using the same flexible conversion process as used for metadata. You should use this type of conversion as opposed to casting or other type conversion methods to be as lenient as possible.
    
  - `IDocument GetNewDocument(IEnumerable<KeyValuePair<string, object>> metadata = null)`
  
    Creates a new empty document with the specified metadata.
  
  - `IReadOnlyList<IDocument> Execute(IEnumerable<IModule> modules, IEnumerable<IDocument> inputs)`
  
    This executes the specified modules with the specified input documents and returns the result documents.
    
  - `IReadOnlyList<IDocument> Execute(IEnumerable<IModule> modules, IEnumerable&lt;KeyValuePair&lt;string, object&gt;&gt; metadata = null)`
  
    This executes the specified modules with an empty initial input document with optional additional metadata and returns the result documents.