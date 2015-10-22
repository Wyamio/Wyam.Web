Title: IMetadata
Description: Contains the metadata for a document.
---
Contains the metadata for a document. To make metadata as easy to work with as possible, Wyam includes a very powerful type conversion capability that lets you convert nearly any metadata value to any other compatible type. Implements `IReadOnlyDictionary<string, object>`.

# Members
---
  
## Methods
  
  - `object Get(string key, object defaultValue = null)`
    
    Returns an `object` and never throws an exception. If the key is not found in the metadata dictionary, then `defaultValue` is returned.
    
  - `T Get<T>(string key)`
  
    Attempts to convert the metadata value to type `T`. If no conversion can be performed or the metadata key is not found, `default(T)` is returned.
  
  - `T Get<T>(string key, T defaultValue)`
  
    Attempts to convert the metadata value to type `T`. If no conversion can be performed or the metadata key is not found, `defaultValue` is returned.
  
  - `string String(string key, string defaultValue = null)`
  
    Attempts to convert the metadata value to a string. If no conversion can be performed or the metadata key is not found, `defaultValue` is returned. This is equivalent to calling `IMetadata.Get<string>(key, defaultValue)`.
  
  - `string Link(string key, string defaultValue = null)`
  
    Same as `String(...)` but automatically replaces any forward-slashes with back-slashes appropriate for using in HTML links.
    
  - `IMetadata<T> MetadataAs<T>()`
  
    Returns a strongly-typed metadata object that converts values to type `T`.