Title: IMetadata<T>
Description: Contains the metadata for a document converted to a specified type.
---
Contains the metadata for a document converted to type `T`. Implements `IReadOnlyDictionary<string, T>`.

# Members
---
  
## Methods
      
  - `T Get(string key)`
  
    Attempts to convert the metadata value to type `T`. If no conversion can be performed or the metadata key is not found, `default(T)` is returned.
  
  - `T Get(string key, T defaultValue)`
  
    Attempts to convert the metadata value to type `T`. If no conversion can be performed or the metadata key is not found, `defaultValue` is returned.