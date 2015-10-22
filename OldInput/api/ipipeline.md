Title: IPipeline
Description: Contains information about a pipeline.
---
Contains information about a pipeline. Implements `IList<IModule>`.

# Members
---
  
## Properties

  - `string Name { get; }`
  
    Returns the name of the pipeline.
    
## Methods
      
  - `void Add(params IModule[] items)`
  
    Adds modules to the pipeline.
    
  - `void Insert(int index, params IModule[] items)`
  
    Inserts modules into the pipeline at a specified index.