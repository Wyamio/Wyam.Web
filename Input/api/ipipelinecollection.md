Title: IPipelineCollection
Description: Contains a collection of all configured pipelines.
---
Contains a collection of all configured pipelines. Implements `IReadOnlyDictionary<string, IPipeline>` of pipelines keyed by pipeline name.

# Members
---
      
## Methods
      
  - `IPipeline Add(params IModule[] modules)`
  
    Adds a new pipeline with an automatically generated name and the provided modules.
    
  - `IPipeline Add(string name, params IModule[] modules)`
  
    Adds a new pipeline with the specified name and the provided modules.